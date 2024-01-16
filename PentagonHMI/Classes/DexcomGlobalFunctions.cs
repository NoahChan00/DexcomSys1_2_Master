using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace PentagonHMI.Classes
{
    public class DexcomGlobalFunctions
    {
        // 20230925
        public static void LogAlarmDuration(DataTable dt, DataTable prevdt, string ErrorCode, string ErrorAction)
        {
            try
            {
                if (ErrorAction.Length == 0)
                    return;

                string[] ErrorActionString = ErrorAction.Split(';');
                DataRow[] rows = prevdt.Select("msgErrorCode = '" + ErrorCode + "'");
                DataRow dr = dt.NewRow();
                if (rows.Length != 0)
                {
                    dr["msgDatetime"] = rows[0]["msgDatetime"];
                    dr["msgErrorCode"] = ErrorCode;
                    dr["msgModule"] = ErrorActionString[0];
                    dr["msgType"] = ErrorActionString[1];
                    dr["msgError"] = ErrorActionString[2];
                    dr["msgAction"] = ErrorActionString[3];
                    dr["msgStation"] = "";
                }
                else
                {
                    dr["msgDatetime"] = DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff");
                    dr["msgErrorCode"] = ErrorCode;
                    dr["msgModule"] = ErrorActionString[0];
                    dr["msgType"] = ErrorActionString[1];
                    dr["msgError"] = ErrorActionString[2];
                    dr["msgAction"] = ErrorActionString[3];
                    dr["msgStation"] = "";

                    string alamMsg = dr["msgErrorCode"].ToString().Replace(',', ' ') + ",";
                    alamMsg += dr["msgModule"].ToString().Replace(',', ' ') + ",";
                    alamMsg += dr["msgError"].ToString().Replace(',', ' ') + ",";
                    alamMsg += dr["msgType"].ToString().Replace(',', ' ');
                }
                dt.Rows.Add(dr);


                if (dt.Rows.Count > 31)
                {
                    dt.Rows.RemoveAt(dt.Rows.Count - 1);
                }

                if (dr != null)
                {
                    dr = null;
                }
            }
            catch (Exception ex)
            {
                // temp
                //FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        // 20230925
        public static void logDexcomDur(string logType, string StartDT, string EndTime, string Duration, string message)
        {
            string strPath = FileLogger.DefaultLocation_Time + Path.DirectorySeparatorChar + @"AlarmDuration";
            string Filename = "AlarmDuration_" + DateTime.Now.ToString("yyyy-MMM-dd") + ".csv";

            if (!Directory.Exists(strPath))
                Directory.CreateDirectory(strPath);

            if (!File.Exists(strPath + Path.DirectorySeparatorChar + Filename))
            {
                using (FileStream stream = new FileStream(strPath + Path.DirectorySeparatorChar + Filename, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine("DateTime,Type,Alarm:Start Time,Alarm: End Time,Duration (hh:mm:ss),Message");
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + "," + logType + "," + StartDT + "," + EndTime + "," + Duration + "," + message);

                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }
            else
            {
                using (FileStream stream = new FileStream(strPath + Path.DirectorySeparatorChar + Filename, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff") + "," + logType + "," + StartDT + "," + EndTime + "," + Duration + "," + message);

                    writer.Flush();
                    writer.Close();
                    stream.Close();
                }
            }

        }
    }
}
