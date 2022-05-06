using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace SiemensOPCCommunicator
{
    public class OnUpdateEventArgs : EventArgs
    {
        #region Properties
        ArrayList _arrAddresses;
        public ArrayList OPCAddresses
        {
            get { return _arrAddresses; }
            set { _arrAddresses = value; }
        }
        #endregion

        #region Constructor
        public OnUpdateEventArgs(ArrayList e)
        {
            OPCAddresses = e;
        }  
        #endregion
    }
}
