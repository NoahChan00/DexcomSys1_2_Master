using System;
using System.Collections.Generic;
using System.Text;
using Logix;
using System.Windows.Forms;
using System.Linq;

namespace Utilities
{
    public class TcpAllenB
    {
        // Allen Bradley PLC TCP Connection
        // ////////////////////////////////
        public static Logix.Controller MyPLCShr { get; set; }
        Controller MyPLC = new Controller();
        public ListBox _values = new ListBox();
        Tag MyTag = new Tag();
        //public string PLC_ERROR = null;
        public string ERROR_MSG { get; set; }
        public string RTN_MSG { get; set; }
        public string RTN_STR { get; set; }
        public bool RTN_BOOL { get; set; }
        public bool RTN_SINT { get; set; }
        public int RTN_INT { get; set; }
        public Int32 RTN_DINT { get; set; }
        public Int64 RTN_LINT { get; set; }


        public TcpAllenB()
        {

        }
        public bool PLC_Connect(string ipAddress, string path, int timeout)
        {
            bool rtnFlag = true;

            MyPLC.IPAddress = ipAddress;
            MyPLC.Path = path;
            MyPLC.Timeout = timeout;

            if (MyPLC.Connect() != ResultCode.E_SUCCESS)
            {
                ERROR_MSG = "ERROR - " + MyPLC.ErrorCode + " : " + MyPLC.ErrorString;
                rtnFlag = false;
            }

            return rtnFlag;
        }
        public bool PLC_Connect()
        {
            bool rtnFlag = true;

            MyPLC.IPAddress = "191.168.3.21";
            MyPLC.Path = "0";
            MyPLC.Timeout = 3000;

            if (MyPLC.Connect() != ResultCode.E_SUCCESS)
            {
                ERROR_MSG = "ERROR - " + MyPLC.ErrorCode + " : " + MyPLC.ErrorString;
                rtnFlag = false;
                MyPLCShr = MyPLC;
            }
            return rtnFlag;
        }
        public bool PLC_Disconnect()
        {
            bool rtnFlag = true;

            if(MyPLC != null)
            MyPLC.Disconnect();

            return rtnFlag;
        }
        public string ReadArray(string tagname)
        {
            PLC_Connect();
            MyTag.Name = tagname;
            MyPLC.Simulate = false;
            MyTag.Length = 500; //length of array
            MyPLC.CPUType = Controller.CPU.LOGIX;
            MyTag.NetType = typeof(System.Boolean);
            MyPLC.ReadTag(MyTag);
            _values.Items.Clear();
            if (MyPLC.ErrorCode == ResultCode.E_SUCCESS)
            {
                Array theData = (Array)MyTag.Value;
                Boolean[] CtheData = theData.OfType<object>().Select(o => Convert.ToBoolean(o)).ToArray();
                int[] array2 = FindAllIndex(CtheData, element => element == true);
                string strAlarm = string.Join(",", array2);
                return strAlarm;
                //for (int i = 0; i < theData.Length; i++)
                //{ _values.Items.Add(Convert.ToBoolean(theData.GetValue(i))); }
                //return _values;
            }
            else
            { return null; }
        }

        public int[] FindAllIndex<T>(T[] array, Predicate<T> match)
        {
            return array.Select((value, index) => match(value) ? index : -1)
                    .Where(index => index != -1).ToArray();
        }
        
        public object PLC_Read(string tagName, Tag.ATOMIC tagType)
        {
            ERROR_MSG = "";

            MyTag = new Tag(tagName);
            MyTag.DataType = tagType;

            if (MyPLC.ReadTag(MyTag) != ResultCode.E_SUCCESS)
            {
                //MessageBox.Show("ERROR - " + MyPLC.ErrorCode + " : " + MyPLC.ErrorString);
                ERROR_MSG = "ERROR - " + MyPLC.ErrorCode + " : " + MyPLC.ErrorString;
                return null;
            }

            if (ResultCode.QUAL_GOOD == MyTag.QualityCode)
            {
                RTN_MSG = "RTN : " + MyTag.Value.ToString() + "\n" + MyTag.TimeStamp.ToString();
                return MyTag.Value;
            }

            return null;
        }
        
        public string PLC_Write(string tagName, string value, Tag.ATOMIC tagType)
        {
            string rtnString = null;
            MyTag = new Tag(tagName);
            MyTag.DataType = tagType;
            MyTag.Value = value;

            if (MyPLC.WriteTag(MyTag) != ResultCode.E_SUCCESS)
            {
                rtnString = "ERROR";
                ERROR_MSG = "ERROR-" + MyPLC.ErrorCode + " : " + MyPLC.ErrorString;
                return rtnString;
            }

            if (Logix.ResultCode.QUAL_GOOD == MyTag.QualityCode)
            {
                rtnString = MyTag.TimeStamp.ToString();
            }
            else
            {
                rtnString = "ERROR";
                ERROR_MSG = "ERROR-" + MyPLC.ErrorCode + " : " + MyPLC.ErrorString;
            }
            return rtnString;
        }

        ~TcpAllenB()
        {
            PLC_Disconnect();
        }

    }
}
