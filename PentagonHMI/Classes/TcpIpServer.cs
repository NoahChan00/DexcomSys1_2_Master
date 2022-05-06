using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace PentagonHMI.Classes
{
    public class TcpIpServer
    {
        public TcpIpServer()
        {
            Task.Factory.StartNew(Listen, TaskCreationOptions.LongRunning);
        }

        public void Listen()
        {
            TcpListener server = null;
            try
            {
                // Set the TcpListener on port.
                Int32 port = 11000;

                var IP = Dns.GetHostEntry(Dns.GetHostName()).AddressList.Where(x => x.ToString().Contains("168.3")).FirstOrDefault();
                if (IP == null) return;
                server = new TcpListener(IP, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];


                // Enter the listening loop.
                while (true)
                {

                    // Perform a blocking call to accept requests.
                    // You could also use server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    int i;

                    // Loop to receive all the data sent by the client.
                    while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        // Translate data bytes to a ASCII string.
                        Utilities.FileLogger.logPlcEvent(Encoding.ASCII.GetString(bytes, 0, i));
                    }

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException ex)
            {
                Utilities.FileLogger.logError(ex.Message, "PLC Event Log (Listen)");
            }
            finally
            {
                // Stop listening for new clients.
                server?.Stop();
            }
        }
    }
}

