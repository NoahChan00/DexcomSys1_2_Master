using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiemensOPCCommunicator
{
    public class OPCItemUpdateEventArgs : EventArgs
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

        private System.Collections.IList _item;
        public System.Collections.IList Item
        {
            get { return _item; }
            set { _item = value; }
        }

        private string _ConnectString;
        public string ConnectString
        {
            get { return _ConnectString; }
            set { _ConnectString = value; }
        }
        #endregion

        #region Constructor
        public OPCItemUpdateEventArgs(string Message, bool Error, System.Collections.IList Item,string ConnectString)
        {
            this.Message = Message;
            this.Error = Error;
            this.Item = Item;
            this.ConnectString = ConnectString;
        }  
        #endregion
    }
}
