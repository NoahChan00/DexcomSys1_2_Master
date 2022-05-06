using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiemensOPCCommunicator
{
    public class OnWriteCompleteEventArgs : EventArgs
    {
        #region Properties
        //string _strOutput;
        //public string WriteOutput
        //{
        //    get { return _strOutput; }
        //    set { _strOutput = value; }
        //}

        //int[] _pErrort = new int[1];
        //public int[] WriteError
        //{
        //    get { return _pErrort; }
        //    set { _pErrort = value; }
        //}

        //string _strAddress = "";
        //public string WriteStrAddress
        //{
        //    get { return _strAddress; }
        //    set { _strAddress = value; }
        //}

        bool _boolResult = false;
        public bool WriteResult
        {
            get { return _boolResult; }
            set { _boolResult = value; }
        }
        #endregion

        #region EventArgs
        public OnWriteCompleteEventArgs(bool boolResult)
        {
            //WriteOutput = e;
            //WriteError = pError;
            //WriteStrAddress = strAddress;
            WriteResult = boolResult;
        }  
        #endregion
    }
}
