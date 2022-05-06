using System;
using System.Linq;
using System.IO;
using System.Threading;
using Utilities;
using System.Data;
using Logix;

namespace PentagonHMI.Classes
{
    public class Tcpip_ArcadiaTorqueDriver
    {
        private TorqueDriverModel torqueDriverModel = new TorqueDriverModel();
        private TcpIpClient tcpIpClient = null;
        private LogicClasses.Main _Main;
        private const string peakTorqueID = "30230";
        private const string totalAngleID = "30231";
        private const string totalDuration = "30232";
        
        public string ConnectionStatus = "";
        
        public Tcpip_ArcadiaTorqueDriver(ref LogicClasses.Main main)
        {
            try
            {
                _Main = main;
                initializeTorqueDriverModel();
                initializeTcpIp();
                _Main.OnFastUpdate += _Main_OnFastUpdate;
            }
            catch (Exception exception)
            {
                LogInfo(exception.ToString());
            }
        }

        /// <summary>
        /// INSERT INTO [gdb].[dbo].[TorqueDriverModel] (Name,Value,DataType)
        /// 
        /// VALUES
        /// 
        /// ('TriggerTorqueDriverResultTagName','HMI_Trigger_TorqueDrive_Result','BOOL'),
        /// ('AssetTagTagName','HMI_Log_TorqueDrive_AssetTag','STRING'),
        /// ('ZoneTagName','HMI_Log_TorqueDrive_ServingZone','INT'),
        /// ('TorqueDriverTcpDisconnectedTagName','HMI_TorqueDriver_TCP_Disconnected','BOOL')
        /// </summary>
        private void initializeTorqueDriverModel()
        {
            try
            {
                torqueDriverModel = new TorqueDriverModel();

                DataTable dataTable = _Main.SQLer.Exec_DTSelect($"SELECT * FROM [{nameof(TorqueDriverModel)}]");
                if (dataTable != null)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        object Name = dataRow[nameof(Name)];
                        object Value = dataRow[nameof(Value)];
                        object DataType = dataRow[nameof(DataType)];

                        if (Name != null && Value != null)
                        {
                            if (!string.IsNullOrEmpty(Name.ToString()) &&
                                !string.IsNullOrEmpty(Value.ToString()))
                            {
                                if (torqueDriverModel.GetType().GetFields().ToList().Exists(x => x.Name == Name.ToString()))
                                {
                                    if (Name.ToString().Contains("TagName"))
                                    {
                                        if (DataType != null)
                                        {
                                            if (!string.IsNullOrEmpty(DataType.ToString()))
                                            {
                                                Tag.ATOMIC dataType = (Tag.ATOMIC)System.Enum.Parse(typeof(Tag.ATOMIC), DataType.ToString().ToUpper(), true);

                                                torqueDriverModel.GetType().GetField(Name.ToString()).SetValue(torqueDriverModel, new Tag
                                                {
                                                    Name = Value.ToString(),
                                                    DataType = dataType
                                                });
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogInfo(exception.ToString());
            }
        }

        private void initializeTcpIp()
        {
            try
            {
                Func<string, object> GetItem = (x) => _Main.dt_Config.Select().Where(y => y["Item"].ToString().ToUpper() == x.ToUpper()).First()["Info"];
                tcpIpClient = new TcpIpClient(GetItem("LocalIP").ToString(), GetItem("LocalPort").ToString(),
                    GetItem("ServerIP").ToString(), GetItem("ServerPort").ToString(), 1000, false, false, LineTerminator.NA);
                Thread.Sleep(1000);
                tcpIpClient.Connect();
                tcpIpClient.OnConnectionStatus += tcpIpClient_OnConnectionStatus;
                tcpIpClient.OnDataReceived += tcpIpClient_OnDataReceived;
            }
            catch (Exception exception)
            {
                LogInfo(exception.ToString());
            }
        }

        private void tcpIpClient_OnConnectionStatus(string connectionStatus, string iP)
        {
            try
            {
                LogInfo($"[{iP}]=[{connectionStatus}].");
                ConnectionStatus = connectionStatus;
                if (connectionStatus == "Connected")
                {
                    if (tcpIpClient.Send("00200001006         $00"))
                    {
                        LogInfo($"Send >>> [00200001006         $00].");
                    }
                }
                else
                {
                    torqueDriverModel.TorqueDriverTcpDisconnectedTagName.Value = true;
                    
                    _Main.MyPLC.WriteTag(torqueDriverModel.TorqueDriverTcpDisconnectedTagName);

                    tcpIpClient.Connect();
                }
            }
            catch (Exception exception)
            {
                LogInfo(exception.ToString());
            }
        }

        public void tcpIpClient_OnDataReceived(string message)
        {
            try
            {
                LogInfo($"Received <<< [{message}].");
                
                if (message.Contains(peakTorqueID) && message.Contains(totalAngleID) && message.Contains(totalDuration))
                {
                    _Main.connectionAttempt = 0;
                    
                    int peakTorqueStartIndex = message.IndexOf(peakTorqueID) + 17;
                    int peakTorqueEndIndex = message.IndexOf(totalAngleID);
                    string peakTorque = message.Substring(peakTorqueStartIndex, peakTorqueEndIndex - peakTorqueStartIndex);
                    LogInfo($"[Peak Torque Value]=[{peakTorque}].");
                    double peakTorqueAfterConvert = Convert.ToDouble(peakTorque);
                    

                    int totalAngleStartIndex = message.IndexOf(totalAngleID) + 17;
                    int totalAngleEndIndex = message.IndexOf(totalDuration);
                    string totalAngle = message.Substring(totalAngleStartIndex, totalAngleEndIndex - totalAngleStartIndex);
                    LogInfo($"[Total Angle Value]=[{totalAngle}].");
                    double totalAngleAfterConvert = Convert.ToDouble(totalAngle);
                    
                    if (!string.IsNullOrEmpty(torqueDriverModel.AssetTagTagName.Name) &&
                        !string.IsNullOrEmpty(torqueDriverModel.ZoneTagName.Name))
                    {
                        _Main.MyPLC.ReadTag(torqueDriverModel.AssetTagTagName);
                        _Main.MyPLC.ReadTag(torqueDriverModel.ZoneTagName);

                        if (torqueDriverModel.AssetTagTagName.Value != null &&
                            torqueDriverModel.ZoneTagName.Value != null)
                        {
                            LogTorqueDriverResult(torqueDriverModel.AssetTagTagName.Value.ToString(),
                                torqueDriverModel.ZoneTagName.Value.ToString(), peakTorqueAfterConvert, totalAngleAfterConvert);
                        }
                    }

                    _Main.OPC.Write("Torque_DriverData.Torque_Value", peakTorqueAfterConvert, typeof(string));
                    _Main.OPC.Write("Torque_DriverData.Angle_Value", totalAngleAfterConvert, typeof(string));
                }
            }
            catch (Exception exception)
            {
                LogInfo(exception.ToString());
            }
        }

        private void _Main_OnFastUpdate()
        {
            try
            {
                if (!string.IsNullOrEmpty(torqueDriverModel.TriggerTorqueDriverResultTagName.Name))
                {
                    _Main.MyPLC.ReadTag(torqueDriverModel.TriggerTorqueDriverResultTagName);

                    if (torqueDriverModel.TriggerTorqueDriverResultTagName.Value != null)
                    {
                        bool value = Convert.ToBoolean(torqueDriverModel.TriggerTorqueDriverResultTagName.Value);

                        if (torqueDriverModel.TriggerTorqueDriver != value)
                        {
                            torqueDriverModel.TriggerTorqueDriver = value;

                            if (torqueDriverModel.TriggerTorqueDriver)
                            {
                                Thread.Sleep(100);

                                torqueDriverModel.TriggerTorqueDriverResultTagName.Value = false;
                                _Main.MyPLC.WriteTag(torqueDriverModel.TriggerTorqueDriverResultTagName);

                                if (tcpIpClient.Send("006000080010        1201001310000000000000000000000000000001   "))
                                {
                                    LogInfo($"Send >>> [006000080010        1201001310000000000000000000000000000001   ].");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LogInfo(exception.ToString());
            }
        }

        public static void LogInfo(string Description)
        {
            string strPath = Path.Combine(FileLogger.DefaultLocation_Time, "Tcpip_TorqueDriver");
            string Filename = "Tcpip_TorqueDriver" + DateTime.Now.ToString("yyyy-MMM-dd") + ".txt";

            bool FileExist = File.Exists(strPath + Path.DirectorySeparatorChar + Filename);

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename),
                FileExist ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                if (!FileExist) { writer.WriteLine("DateTime,Message"); }
                writer.WriteLine($"{DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff")},{Description}");
                writer.Flush();
                writer.Close();
                stream.Close();
            }
        }

        public static void LogTorqueDriverResult(string assetTag, string zone, double peakTorqueValue, double totalAngleValue)
        {
            string strPath = Path.Combine(FileLogger.DefaultLocation_Time, "TorqueDriverResult");
            string Filename = "TorqueDriverResult_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";

            bool FileExist = File.Exists(strPath + Path.DirectorySeparatorChar + Filename);

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename),
                FileExist ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                if (!FileExist)
                {
                    writer.WriteLine($"DateTime,AssetTag,Zone,Peak Torque Value (mNm),Total Angle Value (Degree)");
                }

                writer.WriteLine($"{DateTime.Now:yyyy-MMM-dd_HH:mm:ss.fff},{assetTag},Z{zone},{peakTorqueValue},{totalAngleValue}");
                writer.Flush();
                writer.Close();
                stream.Close();
            }
        }

        public bool Reconnect()
        {
            bool result = false;
            try
            {
                if (ConnectionStatus == "Conncted")
                    tcpIpClient.Disconnect();
                tcpIpClient.Connect();
                result = true;
                LogInfo("Manual Reconnect Trigger");
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
                result = false;
            }
            return result;
        }

        public bool SendMessage(string message)
        {
            bool result = false;

            try
            {
                if (ConnectionStatus == "Connected")
                {
                    tcpIpClient.Send(message);
                    LogInfo($"Send >>> [{message}]");
                    result = true;
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
                result = false;
            }
            return result;
        }

        public bool Disconnect()
        {
            bool result = false;

            try
            {
                if (ConnectionStatus == "Connected")
                {
                    tcpIpClient.Disconnect();
                    LogInfo($"Manual disconnect");
                    result = true;
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
                result = false;
            }
            return result;
        }

        public bool Connect()
        {
            bool result = false;

            try
            {
                if (ConnectionStatus != "Connected")
                {
                    tcpIpClient.Connect();
                    LogInfo($"Manual connect");
                    result = true;
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
                result = false;
            }
            return result;
        }

    }
}
