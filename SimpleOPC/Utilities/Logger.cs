using System;
using System.IO;

namespace SimpleOPC.Utilities
{
    public class Logger
    {
        private static object _Lock = new object();
        public static string LogPath;

        public struct Msg
        {
            public const string Write = "OPC Write. TagName: {0}. Value: {1}.";
            public const string Read = "OPC Read. TagName: {0}. Value: {1}.";
            public const string WriteFail = "OPC Write Fail. TagName: {0}. Current Value: {1}.";
            public const string ReadFail = "OPC Read Fail. TagName: {0}. Current Value: {1}.";
            public const string ControllerConnectionFail = "TagGroup Controller Connection Fail";
        }

        public static void SetPath()
        {
            LogPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Logs_" + DateTime.Now.ToString("yyyy-MMM") + @"\OPC\";
            //LogPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\Logs\OPC\";
            if(!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);
        }

        public static void Info(string Message)
        {
            try
            {
                lock(_Lock)
                {
                    SetPath();
                    string LogPath_FileName = LogPath + $"OPCInfo_{DateTime.Now.ToString("yMMdd")}.txt";
                    using(FileStream stream = new FileStream(LogPath_FileName,
                        File.Exists(LogPath_FileName) ? FileMode.Append : FileMode.CreateNew,
                        FileAccess.Write, FileShare.ReadWrite))
                    using(StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine($"{DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff")},{Message}");
                        writer.Flush();
                        writer.Close();
                        stream.Close();
                    }
                }
            }
            catch(Exception ex) { throw ex; }
        }

        public static void Warn(string Message)
        {
            try
            {
                lock(_Lock)
                {
                    SetPath();
                    string LogPath_FileName = LogPath + $"OPCWarn_{DateTime.Now.ToString("yMMdd")}.txt";
                    using(FileStream stream = new FileStream(LogPath_FileName,
                        File.Exists(LogPath_FileName) ? FileMode.Append : FileMode.CreateNew,
                        FileAccess.Write, FileShare.ReadWrite))
                    using(StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine($"{DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff")},{Message}");
                        writer.Flush();
                        writer.Close();
                        stream.Close();
                    }
                }
            }
            catch(Exception ex) { throw ex; }
        }
    }
}