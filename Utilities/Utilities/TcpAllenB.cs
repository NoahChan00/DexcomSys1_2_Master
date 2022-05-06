using System;
using Logix;
using System.Windows.Forms;
using System.Linq;

namespace Utilities
{
    public class TcpAllenB
    {
        // Allen Bradley PLC TCP Connection
        // ////////////////////////////////
        Controller MyPLC = new Controller();
        public string ERROR_MSG { get; set; }

        public Controller GetController()
        {
            return MyPLC;
        }

        public bool PLC_Connect(string ipAddress, string path, int timeout)
        {
            bool rtnFlag = true;

            MyPLC.IPAddress = ipAddress;
            MyPLC.Path = path;
            MyPLC.Timeout = timeout;

            int i = 0;
            do
            {
                if (MyPLC.Connect() != ResultCode.E_SUCCESS)
                {
                    ERROR_MSG = $"ERROR - PLC_Connect ({i})\nIP= {ipAddress}, Path={path}, Timeout={timeout}\n {MyPLC.ErrorCode} : {MyPLC.ErrorString}";

                    if (MyPLC.IsConnected)
                    {
                        i = 99;
                        rtnFlag = true;
                    }
                    else
                    {
                        i++;
                        rtnFlag = false;
                    }
                }
                else
                {
                    string logMsg = $"PLC_Connect(TcpAB) OK\nIP={ipAddress}, Path={path}, timeout={timeout}\n";
                    Utilities.FileLogger.logError(logMsg, "Tracking Good TCP PLC_Connect");
                    rtnFlag = true;
                    i = 99;
                }

            } while (i < 5);

            return rtnFlag;
        }
    }
}
