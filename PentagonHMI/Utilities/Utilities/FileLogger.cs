using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;

namespace Utilities
{

    public static class FileLogger
    {

        public static string prevStartDateTime;
        public static string[] lines;
        public static string location = "D:\\HMI Logs";
        public static string location2 = "D:\\Barcode";
        public static void logEvent(string type, string descriptions)
        {
            string strPath = location + Path.DirectorySeparatorChar + "Event";
            string Filename = "Event_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".csv";

          //  string u = String.Format("{0, 15}", user);
            string t = String.Format("{0, 15}", type);


            if (descriptions.Length == 0)
                return;

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);


            if (!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {

                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {

                    writer.WriteLine("DateTime,UserID,Information,Message");
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss") + ", " + t + ", " + descriptions);
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
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss") +", " + t + ", " + descriptions);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }



        }

        public static void LogAlertWarning(DataTable dt, DataTable prevdt, string ErrorCode,  string ErrorAction)
        {
            //Log to Log & DT
            try
            {
                if (ErrorAction.Length == 0)
                    return;

                //string[] ErrorActionString = ErrorAction.Split(new char[] {'|' });
                string[] ErrorActionString = ErrorAction.Split(';');
                //ErrorCode = "500";
                DataRow[] rows = prevdt.Select("msgErrorCode = '" + ErrorCode + "'");
                DataRow dr = dt.NewRow();
                if (rows.Length>0)
                {  
                    dr["msgDatetime"] = rows[0]["msgDatetime"];
                    dr["msgErrorCode"] = ErrorCode;
                    dr["msgError"] = ErrorActionString[1];
                    dr["msgAction"] = ErrorActionString[2];
                    dr["msgStation"] = "";
                    dr["msgModule"] = ErrorActionString[0];
                }
                else
                {
                    dr["msgDatetime"] = DateTime.Now.ToString();
                    dr["msgErrorCode"] = ErrorCode;
                    dr["msgError"] = ErrorActionString[1];
                    dr["msgAction"] = ErrorActionString[2];
                    dr["msgStation"] = "";
                    dr["msgModule"] = ErrorActionString[0];
                }


               // logAlarm(ErrorActionString[1], ErrorActionString[2]);

                dt.Rows.InsertAt(dr, 0);


                if (dt.Rows.Count > 11)
                {
                    dt.Rows.RemoveAt(dt.Rows.Count - 1);
                }

                if (dr != null)
                {
                    dr = null;
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        public static void LogEvent(DataTable dt, string User,string type, string descriptions)
        {


            try
            {
                if (dt != null)
                {
                    DataRow dr = dt.NewRow();
                    dr["LogDatetime"] = DateTime.Now.ToString("yyyy-MMM-dd,HH:mm:ss");
                    dr["LogUser"] = User;
                    dr["LogType"] = type;
                    dr["LogDescriptions"] = descriptions;

                    dt.Rows.InsertAt(dr, 0);

                    if (dt.Rows.Count > 100000)
                    {
                        dt.Rows.RemoveAt(dt.Rows.Count - 1);
                    }
                }


                Utilities.FileLogger.logEvent(type, descriptions);

            }
            catch (Exception error)
            {

                logError(error.Message, error.ToString());
            }
        }

        public static void logInfo(string Message)
        {
            string strPath = location + Path.DirectorySeparatorChar + @"Logs\LowLevelLog";
            string Filename = DateTime.Now.ToString("yyyy-MMM-dd") + ".log";

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);


            if (!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {

                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd,HH:mm:ss,") + Message);
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
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd,HH:mm:ss,") + Message);
                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }


            }





        }

        public static void logError(string message, string descriptions)
        {
            string strPath = location + Path.DirectorySeparatorChar + "error_logs";
            string Filename = "Error_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".csv";


            if (descriptions.Length == 0)
                return;

            return;
            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);


            if (!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {

                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss") + ", " + message + ", " + descriptions);
                    writer.WriteLine("");
                }
            }
            else
            {
                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss") + ", " + message + ", " + descriptions);
                    writer.WriteLine("");
                }
            }

        }

        public static void logAlarm(string ErrorMsg, string Action)
        {
            string strPath = location + Path.DirectorySeparatorChar + "Alarm";
            string Filename = "Alarm_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".csv";


            //if (descriptions.Length == 0)
            //    return;


            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);


            if (!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {

                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("DateTime,Message,Action");
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss") + "," + ErrorMsg + "," + Action);
                }
            }
            else
            {
                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss") +  "," + ErrorMsg + "," + Action);
                }
            }

        }

        public static void logHandlerUPH(int value)
        {
            string directory = location + Path.DirectorySeparatorChar + "HandlerUPH" + Path.DirectorySeparatorChar + "Process" + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyMMdd");
            string SpecName = DateTime.Now.Hour.ToString("00") + "_" + DateTime.Now.ToString("yyMMdd") + ".log";
            string strPath = directory + Path.DirectorySeparatorChar + SpecName;


            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            using (FileStream stream = new FileStream((strPath), FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(stream))
            {

                writer.Write(DateTime.Now.Hour.ToString() + "," + value);
            }

          //  LogUPHInOneFile(directory);
        }

        public static void LogUPHInOneFile(List<KeyValuePair<string, long>> UPHItems)
        {
            try
            {

                string directory = @""+location+"UPH";

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);


                using (FileStream stream = new FileStream((directory + Path.DirectorySeparatorChar + "UPH_" + DateTime.Now.ToString("yyyyMMdd") + ".csv"), FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("Hours,UPH");
                    foreach (KeyValuePair<string, long> item in UPHItems)
                    {
                        writer.WriteLine(item.Key + "," + item.Value);
                    }

                }

            }
            catch
            {
            }


        }

        public static void logEng2TrayInfo(string Message, string fileName, DateTime LotStartTime)
        {
            string strPath = location + Path.DirectorySeparatorChar + "Tray" + Path.DirectorySeparatorChar + LotStartTime.ToString("dd_MMM_yyyy") + Path.DirectorySeparatorChar + "Engineering_2";
            string Filename = fileName + ".csv";

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);


            if (!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {

                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("Date,Time,Serial Number,Result,Reason,Tray Row,Tray Column,Tray Number");
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd,HH:mm:ss") + "," + Message);
                }
            }
            else
            {

                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {

                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd,HH:mm:ss") + ", " + Message);
                }



            }


        }

        public static void logOEE(string Message)
        {
            string strPath = location+ Path.DirectorySeparatorChar + @"OEE";
            string Filename = "OEE_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".csv";

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);


            if (!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {

                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("Date,Time,StartDateTime,System Uptime,System Downtime,Idling Time,Operation Time,SoftJam,HardJam,MTBA,MTBF,Total Pass,Total Fail,Availability(%),Quality(%),Performance(%),OEE(%)");
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd,HH:mm:ss,") + Message);

                }
            }
            else
            {

                using (FileStream stream = new FileStream((strPath + Path.DirectorySeparatorChar + Filename), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd,HH:mm:ss,") + Message);

                }



            }





        }

        public static void LogBarcode(string Barcode)
        {
            try
            {
                string directory = @""+location2+"\\Mapping";

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);


                using (FileStream stream = new FileStream((directory + Path.DirectorySeparatorChar + "Position1" + ".txt"), FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(Barcode);
                  
                }

                using (FileStream stream = new FileStream((directory + Path.DirectorySeparatorChar + "Position2" + ".txt"), FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(Barcode);

                }

            }
            catch
            {
            }


        }
    }

}
