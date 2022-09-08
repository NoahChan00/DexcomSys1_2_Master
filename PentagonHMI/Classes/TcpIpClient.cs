using System;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PentagonHMI.Classes
{
    #region PublicEnums

    public enum LineTerminator
    {
        NA,
        CR,
        LF,
        CRLF
    }

    #endregion PublicEnums

    public class TcpIpClient : IDisposable
    {
        #region PrivateFields

        private string serverIP_ = string.Empty;
        private bool checkConnectionFlag = false;
        private bool autoReconnect = false;
        private Socket socket = null;
        private int reconnectInterval = 0;
        private IPEndPoint remoteServerEndPoint = null;
        private IPEndPoint localEndPoint = null;
        private byte[] byteBuffer = new byte[1024];
        private bool enableHeartBeat = true;
        private bool runHeartBeat = true;
        private bool receivedHeartBeat = false;
        private LineTerminator lineTerminator;

        #endregion PrivateFields

        #region PublicEvents

        public delegate void ConnectionStatusEvent(string connectionStatus, string iP);

        public event ConnectionStatusEvent OnConnectionStatus;

        private void UpdateConnectionStatus(string connectionStatus)
        {
            if(OnConnectionStatus != null)
            {
                OnConnectionStatus(connectionStatus, Convert.ToString(remoteServerEndPoint));
                if(connectionStatus.Equals("Connected"))
                { runHeartBeat = true; }
                else if(connectionStatus.Equals("Disconnected"))
                { runHeartBeat = false; }
            }
        }

        public delegate void DataReceivedEvent(string message);

        public event DataReceivedEvent OnDataReceived;

        private void UpdateDataReceived(string message)
        {
            if(OnDataReceived != null)
            {
                OnDataReceived(message);
                receivedHeartBeat = true;
            }
        }

        #endregion PublicEvents

        #region Constructor

        public TcpIpClient(string localIP, string localPort, string serverIP, string serverPort, int reconnectInterval_, bool enableHeartBeat_, bool connectWhenConstruct, LineTerminator lineTerminator_)
        {
            if(String.IsNullOrEmpty(localIP))
            { throw new ArgumentException("Value cannot be null or empty.", "localIP"); }
            if(String.IsNullOrEmpty(localPort))
            { throw new ArgumentException("Value cannot be null or empty.", "localPort"); }
            if(String.IsNullOrEmpty(serverIP))
            { throw new ArgumentException("Value cannot be null or empty.", "serverIP"); }
            if(String.IsNullOrEmpty(serverPort))
            { throw new ArgumentException("Value cannot be null or empty.", "serverPort"); }
            serverIP_ = serverIP;

            checkConnectionFlag = false;
            autoReconnect = true;
            Task.Factory.StartNew(() => checkConnection());

            localEndPoint = new IPEndPoint(IPAddress.Parse(localIP), Convert.ToInt16(localPort));
            remoteServerEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), Convert.ToInt16(serverPort));
            if(connectWhenConstruct)
            { Connect(); }
            reconnectInterval = reconnectInterval_;
            enableHeartBeat = enableHeartBeat_;
            lineTerminator = lineTerminator_;
            Task.Factory.StartNew(() => heartBeat());
        }

        #endregion Constructor

        #region PrivateMethods

        /// <summary>
        /// Connect to server.
        /// </summary>
        private void checkConnection()
        {
            while(true)
            {
                if(checkConnectionFlag)
                {
                    if(autoReconnect)
                    {
                        Connect();
                    }
                }
                Thread.Sleep(800);
            }
        }

        private void setTcpKeepAlive(Socket socket_, uint keepAliveTime, uint keepAliveInterval)
        {
            // Marshal the equivalent of the native structure into a byte array.
            uint dummy = 0;
            byte[] inOptionValues = new byte[(Marshal.SizeOf(dummy) * 3)];
            BitConverter.GetBytes((uint)keepAliveTime).CopyTo(inOptionValues, 0);
            BitConverter.GetBytes((uint)keepAliveTime).CopyTo(inOptionValues, Marshal.SizeOf(dummy));
            BitConverter.GetBytes((uint)keepAliveInterval).CopyTo(inOptionValues, (Marshal.SizeOf(dummy) * 2));
            // Write SIO_VALS to Socket IOControl.
            socket_.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
        }

        private void onConnect(IAsyncResult asyncResult)
        {
            // Socket was the passed in object.
            Socket socket_ = (Socket)asyncResult.AsyncState;
            // Check if we were successfull.
            try
            {
                socket_.EndConnect(asyncResult);
                if(socket_.Connected)
                {
                    setupReceiveCallBack(socket_);
                    UpdateConnectionStatus("Connected");
                }
                else
                {
                    throw new Exception("Unable to connect to remote machine");
                }
            }
            catch(ObjectDisposedException)
            {
                UpdateConnectionStatus("Disconnected");
            }
            catch(Exception ex)
            {
                checkConnectionFlag = true;
                if(autoReconnect)
                {
                    UpdateConnectionStatus(ex.Message);
                    UpdateConnectionStatus("Reconnecting");
                    Connect();
                }
                else
                {
                    UpdateConnectionStatus(ex.Message);
                    UpdateConnectionStatus("Disconnected");
                }
            }
        }

        /// <summary>
        /// Setup the callback for received data and loss of connection.
        /// </summary>
        /// <param name="socket_"></param>
        private void setupReceiveCallBack(Socket socket_)
        {
            try
            {
                socket_.BeginReceive(byteBuffer, 0, byteBuffer.Length, SocketFlags.None, new AsyncCallback(onReceivedData), socket_);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the new data and send it out to all other connections.
        /// Note: If not data was received the connection has probably died.
        /// </summary>
        /// <param name="asyncResult"></param>
        private void onReceivedData(IAsyncResult asyncResult)
        {
            // Socket was the passed in object.
            Socket socket_ = (Socket)asyncResult.AsyncState;
            // Check if we got any data.
            try
            {
                int bytesRec = 0;
                if(socket_ != null && socket_.Connected)
                { bytesRec = socket_.EndReceive(asyncResult); }
                if(bytesRec > 0)
                {
                    // Wrote the data to the list.
                    string received = Encoding.ASCII.GetString(byteBuffer, 0, bytesRec);
                    if(byteBuffer != null)
                    { byteBuffer = null; }
                    byteBuffer = new byte[1024];
                    // If the connection is still usable restablish the callback.
                    setupReceiveCallBack(socket_);
                    // Raise OnDataReceived event.
                    UpdateDataReceived(received);
                }
            }
            catch(Exception ex)
            {
                checkConnectionFlag = true;
                if(autoReconnect)
                { UpdateConnectionStatus(ex.Message); }
            }
        }

        private void heartBeat()
        {
            Stopwatch sw = new Stopwatch();
            sw.Reset();
            sw.Start();
            while(true)
            {
                if(runHeartBeat == true && enableHeartBeat == true)
                {
                    if(receivedHeartBeat)
                    {
                        receivedHeartBeat = false;
                        sw.Reset();
                        sw.Start();
                    }
                    if(sw.ElapsedMilliseconds >= 5000)
                    {
                        Send("SERVER ALIVE?");
                        receivedHeartBeat = false;
                        sw.Reset();
                        sw.Start();
                    }
                }
                Thread.Sleep(1000);
            }
        }

        #endregion PrivateMethods

        #region PublicMethods

        public void Connect()
        {
            try
            {
                if(socket != null && socket.Connected == true)
                { return; }
                using(Ping ping = new Ping())
                {
                    PingReply pingResult = ping.Send(serverIP_);
                    if(pingResult.Status != IPStatus.Success)
                    { throw new PingException(string.Format("Failed to ping {0}.", serverIP_)); }
                }
                checkConnectionFlag = false;
                Thread.Sleep(reconnectInterval);
                // Update status.
                UpdateConnectionStatus("Connecting");
                // Create the socket object.
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //if (localEndPoint != null) { socket.Bind(localEndPoint); }
                socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);
                socket.LingerState.Enabled = false;
                setTcpKeepAlive(socket, 20, 20);
                // Connect to server non-blocking method
                socket.BeginConnect(remoteServerEndPoint, new AsyncCallback(onConnect), socket);
            }
            catch(Exception ex)
            {
                checkConnectionFlag = true;
                if(autoReconnect)
                {
                    UpdateConnectionStatus(ex.Message);
                    UpdateConnectionStatus("Reconnecting");
                    Connect();
                }
            }
        }

        public void Reconnect()
        {
            // Connect to server non-blocking method
            socket.BeginConnect(remoteServerEndPoint, new AsyncCallback(onConnect), socket);
        }

        /// <summary>
        /// Close the socket connection.
        /// </summary>
        public void Disconnect()
        {
            if(socket != null)
            {
                socket.Close();
                UpdateConnectionStatus("Disconnected");
            }
            GC.Collect();
        }

        /// <summary>
        /// Send the message in the message area. Only do this if we are connected.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool Send(string message)
        {
            bool sent = false;
            try
            {
                switch(lineTerminator)
                {
                    case LineTerminator.CR:
                        message = string.Format("{0}{1}", message, "\r");
                        break;

                    case LineTerminator.LF:
                        message = string.Format("{0}{1}", message, "\n");
                        break;

                    case LineTerminator.CRLF:
                        message = string.Format("{0}{1}", message, "\r\n");
                        break;
                }
                // Check we are connected.
                if(socket == null || !socket.Connected)
                { throw new Exception("Must be connected to send a message"); }
                try
                {
                    // Convert to byte array and send.
                    Byte[] byteDateLine = Encoding.ASCII.GetBytes(message.ToCharArray());
                    socket.Send(byteDateLine, byteDateLine.Length, 0);
                    sent = true;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            catch(Exception ex)
            {
                checkConnectionFlag = true;
                if(autoReconnect)
                { UpdateConnectionStatus(ex.Message); }
            }
            return sent;
        }

        #endregion PublicMethods

        #region IDisposableMembers

        public void Dispose()
        {
            autoReconnect = false;
            Disconnect();
            socket.Close();
            socket = null;
        }

        #endregion IDisposableMembers
    }
}