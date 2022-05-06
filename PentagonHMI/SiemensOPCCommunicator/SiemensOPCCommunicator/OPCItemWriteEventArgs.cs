using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiemensOPCCommunicator
{
    public class OPCItemWriteEventArgs
    {
        #region Properties
        private bool _boolError;
        public bool Error
        {
            get { return _boolError; }
            set { _boolError = value; }
        }

        private string _strMessage;
        public string Message
        {
            get { return _strMessage; }
            set { _strMessage = value; }
        }

        private string _Address;
        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        private string _ConnectString;
        public string ConnectString
        {
            get { return _ConnectString; }
            set { _ConnectString = value; }
        }
        #endregion

        #region Constructor
        public OPCItemWriteEventArgs(string Message, bool Error, string Address, string ConnectString)
        {
            this.Message = Message;
            this.Error = Error;
            this.Address = Address;
            this.ConnectString = ConnectString;
        }  
        #endregion
    }
}
