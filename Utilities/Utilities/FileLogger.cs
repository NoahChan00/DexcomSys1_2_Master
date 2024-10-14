using System;
using System.Data;
using System.IO;

namespace Utilities
{
    public static class FileLogger
    {
        public static string DefaultLocation_Time = @"D:\HMI\Logs";
        public static string DefaultLocation = @"D:\HMI\Logs";

        public static void logEvent(string type, string descriptions)
        {
            string strPath = DefaultLocation_Time + Path.DirectorySeparatorChar + "Event";
            string Filename = "Event_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".txt";

            if(descriptions.Length == 0)
                return;

            if(!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            if(!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("DateTime,Module,Message");
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + "," + descriptions);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            else
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + ", " + descriptions);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
        }

        public static void DryRun(string RunTime, string IdleTime, string DownTime, string MTBA, string MTBF, string SoftJam, string HardJam)
        {
            string strPath = DefaultLocation_Time + Path.DirectorySeparatorChar + "Dryrun";
            string Filename = "Dryrun_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".txt";

            if(!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            bool FileExist = File.Exists(strPath + Path.DirectorySeparatorChar + Filename);

            using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename),
                FileExist ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            using(StreamWriter writer = new StreamWriter(stream))
            {
                if(!FileExist)
                {
                    writer.WriteLine("Remark: 1. All Timespan are Logged as Second; 2. Log When Reset Button Triggered at Dryrun Page;");
                    writer.WriteLine("DateTime,RunTime,IdleTime,DownTime,MTBA,MTBF,SoftJam,HardJam");
                }
                writer.WriteLine($"{DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff")},{RunTime},{IdleTime},{DownTime},{MTBA},{MTBF},{SoftJam},{HardJam}");
                writer.Flush();
                writer.Close();
                stream.Close();
            }
        }

        public static void logButton(string Page, string Description, string Method)
        {
            string strPath = DefaultLocation_Time + Path.DirectorySeparatorChar + "Event";
            string Filename = "Event_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".txt";

            bool FileExist = File.Exists(strPath + Path.DirectorySeparatorChar + Filename);

            if(!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename),
                FileExist ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            using(StreamWriter writer = new StreamWriter(stream))
            {
                //,Information,{Method}
                if(!FileExist)
                { writer.WriteLine("DateTime,Module,Message"); }
                writer.WriteLine($"{DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff")},{Page},{Description}");
                writer.Flush();
                writer.Close();
                stream.Close();
            }
        }

        public static void logSetting(string type, string descriptions)
        {
            string strPath = DefaultLocation_Time + Path.DirectorySeparatorChar + "Settings";
            string Filename = "Settings_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".csv";

            if (descriptions.Length == 0)
                return;

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            if (!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {
                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("DateTime,Message");
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + "," + descriptions);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            else
            {
                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + ", " + descriptions);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
        }

        public static void logUser(string type, string descriptions)
        {
            string strPath = DefaultLocation_Time + Path.DirectorySeparatorChar + "Users";
            string Filename = "Users_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".csv";

            if (descriptions.Length == 0)
                return;

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            if (!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {
                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("DateTime,Message");
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + "," + descriptions);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            else
            {
                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + ", " + descriptions);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
        }

        public static void LogAlertWarning(DataTable dt, DataTable prevdt, string ErrorCode, string ErrorAction)
        {
            //Log to Log & DT
            try
            {
                if(ErrorAction.Length == 0)
                    return;

                string[] ErrorActionString = ErrorAction.Split(';');
                DataRow[] rows = prevdt.Select("msgErrorCode = '" + ErrorCode + "'");
                DataRow dr = dt.NewRow();

                dr["msgErrorCode"] = ErrorCode;
                dr["msgError"] = ErrorActionString[2];
                dr["msgAction"] = ErrorActionString[3];
                dr["msgModule"] = ErrorActionString[0];
                //dr["msgType"] = int.Parse(ErrorCode) >= 2000 ? "Warning" : "Error";
                dr["msgType"] = ErrorActionString[1];
                if (rows.Length != 0)
                {
                    dr["msgDatetime"] = rows[0]["msgDatetime"];
                }
                else
                {
                    dr["msgDatetime"] = DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff");

                    string alamMsg = dr["msgErrorCode"].ToString().Replace(',', ' ') + ",";
                    alamMsg += dr["msgModule"].ToString().Replace(',', ' ') + ",";
                    alamMsg += dr["msgError"].ToString().Replace(',', ' ');

                    logAlarm(alamMsg, dr["msgAction"].ToString().Replace(',', ' '));
                }

                dt.Rows.Add(dr);// InsertAt(dr, 0);

                if(dt.Rows.Count > 31)
                {
                    dt.Rows.RemoveAt(dt.Rows.Count - 1);
                }

                if(dr != null)
                {
                    dr = null;
                }
            }
            catch(Exception)
            {
            }
        }

        public static void LogEvent(DataTable dt, string User, string type, string descriptions)
        {
            try
            {
                if(dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["LogDatetime"] = DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss.fff");
                    dr["LogUser"] = User;
                    dr["LogType"] = type;
                    dr["LogDescriptions"] = descriptions;

                    dt.Rows.InsertAt(dr, 0);

                    if(dt.Rows.Count > 100000)
                    {
                        dt.Rows.RemoveAt(dt.Rows.Count - 1);
                    }
                }

                Utilities.FileLogger.logEvent(type, descriptions);
            }
            catch(Exception error)
            {
                logError(error.Message, error.ToString());
            }
        }

        public static void logInfo(string Message)
        {
            string strPath = DefaultLocation_Time + Path.DirectorySeparatorChar + "Timesync";
            string Filename = DateTime.Now.ToString("yyyy-MMM-dd") + ".txt";

            if(!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            if(!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss.fff,") + Message);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            else
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss.fff,") + Message);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
        }

        public static void logError(string message, string descriptions)
        {
            string strPath = DefaultLocation_Time + Path.DirectorySeparatorChar + "Error_logs";
            string Filename = "Error_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".txt";

            if(descriptions.Length == 0)
                return;

            if(!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            if(!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + ", " + message + ", " + descriptions);
                    writer.WriteLine("");
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            else
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + ", " + message + ", " + descriptions);
                    writer.WriteLine("");
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }

            return;
        }

        public static void logAnything(string message, string descriptions)
        {
            string strPath = DefaultLocation_Time + Path.DirectorySeparatorChar + "Others";
            string Filename = "Else_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".txt";

            if(descriptions.Length == 0)
                return;

            if(!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            if(!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + ", " + message + ", " + descriptions);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            else
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + ", " + message + ", " + descriptions);
                    writer.WriteLine("");
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            return;
        }

        public static void logAlarm(string ErrorMsg, string Action)
        {
            string strPath = DefaultLocation_Time + Path.DirectorySeparatorChar + "Alarm";
            string Filename = "Alarm_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".csv";

            //ErrorMsg = ErrorMsg.Replace(',', ' ');
            //Action = Action.Replace(',', ' ');
            if(!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            if(!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("DateTime,ErrorCode,Module,Message,Action");
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + "," + ErrorMsg + "," + Action);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            else
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + "," + ErrorMsg + "," + Action);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
        }

        public static void logPlcEvent(string msg)
        {
            string strPath = DefaultLocation_Time + Path.DirectorySeparatorChar + @"PLCEvent";
            string Filename = "PlcEvent_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".txt";

            if(!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            if(!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("Date,Time,PLC Event");
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss.fff,") + msg);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            else
            {
                using(FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss.fff,") + msg);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
        }
    }
}