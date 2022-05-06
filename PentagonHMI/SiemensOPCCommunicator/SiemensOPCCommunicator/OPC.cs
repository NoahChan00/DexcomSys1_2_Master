using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpcRcw.Comn;
using OpcRcw.Da;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Collections;
using System.Threading;
using System.Windows;

namespace SiemensOPCCommunicator
{
    
    
    public class OPC: IOPCDataCallback
    {
        #region Variable
        /* Event */
        public delegate void OnUpdateHandler(object o, OnUpdateEventArgs e);
        public event OnUpdateHandler OnUpdateEvent;

        public delegate void OnWriteCompleteHandler(object o, OnWriteCompleteEventArgs e);
        public event OnWriteCompleteHandler OnWriteCompleteEvent;

        /* Constants */
        internal const string SERVER_NAME = "OPC.SimaticNET";       // local server name
        internal const string ITEM1_NAME = "S7:[S7 Connection_1]MX200.0";          // 1st item name
        internal const string ITEM2_NAME = "S7:[S7 Connection_1]MX301.3";          // 2nd item name
        internal const string GROUP_NAME = "grp1";                  // Group name
        internal const int LOCALE_ID = 0x409;                       // LOCALE FOR ENGLISH.

        /* Global variables */
        IOPCServer pIOPCServer;
        IOPCAsyncIO2 pIOPCAsyncIO2 = null;                          // instance pointer for asynchronous IO.
        IOPCGroupStateMgt pIOPCGroupStateMgt = null;
        IConnectionPointContainer pIConnectionPointContainer = null;
        IConnectionPoint pIConnectionPoint = null;

        ArrayList _arrResult = null;
        bool _boolAck = false;
        int _intWriteCount = 0;
        bool _boolWrite = true;

        Object pobjGroup1 = null;
        int pSvrGroupHandle = 0;                                    // server group handle for the added group
        int nTransactionID = 0;
        int[] ItemSvrHandleArray;
        Int32 dwCookie = 0;
        #endregion 

        #region Properties
        ArrayList _arrAddresses;
        private ArrayList OPCAddresses
        {
            get {return _arrAddresses;}
            set {_arrAddresses = value;}
        }

        string _strACKAddress;
        private string strOPCACKAddress
        {
            get { return _strACKAddress; }
            set { _strACKAddress = value; }
        }

        bool _boolIsDB;
        private bool boolIsDB
        {
            get { return _boolIsDB; }
            set { _boolIsDB = value; }
        }
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public OPC()
        {
            
        }

        /// <summary>
        /// Constructor for async reading
        /// </summary>
        /// <param name="arrAddresses">Collection of the async address for OPC</param>
        /// <param name="strACKAddress">Acknowledge address; System will return value if acknowledge address being updated</param>
        public OPC(ArrayList arrAddresses, string strACKAddress, bool isDB = true)
        {
            OPCAddresses = arrAddresses;
            if (!string.IsNullOrEmpty(strACKAddress))
            {
                OPCAddresses.Add(strACKAddress);
            }            
            strOPCACKAddress = strACKAddress;
            boolIsDB = isDB;
        }
        #endregion

        #region Methods
        /// <summary>
        /// This function is called to establish the connection
        /// </summary>
        public bool Connect()
        {
            // Local variables
            bool boolResult = true;
            Type svrComponenttyp;
            OPCITEMDEF[] ItemDeffArray;

            // Group properties
            Int32 dwRequestedUpdateRate = 250;
            Int32 hClientGroup = 1;
            Int32 pRevUpdateRate;
            float deadband = 0;

            int TimeBias = 0;

            GCHandle hTimeBias, hDeadband;
            hTimeBias = GCHandle.Alloc(TimeBias, GCHandleType.Pinned);
            hDeadband = GCHandle.Alloc(deadband, GCHandleType.Pinned);


            // 1. Get the Type from the progID and create instance of the OPC Server COM component
            Guid iidRequiredInterface = typeof(IOPCItemMgt).GUID;
            svrComponenttyp = Type.GetTypeFromProgID(SERVER_NAME);
            try
            {
                // Connect to the local server.
                pIOPCServer = (IOPCServer)Activator.CreateInstance(svrComponenttyp);
                try
                {
                    /* 2. Add a new group
                        Add a group object and querry for interface IOPCItemMgt
                        Parameter as following:
                        [in] not active, so no OnDataChange callback
                        [in] Request this Update Rate from Server
                        [in] Client Handle, not necessary in this sample
                        [in] No time interval to system UTC time
                        [in] No Deadband, so all data changes are reported
                        [in] Server uses english language to for text values
                        [out] Server handle to identify this group in later calls
                        [out] The answer from Server to the requested Update Rate
                        [in] requested interface type of the group object
                        [out] pointer to the requested interface
                    */

                    pIOPCServer.AddGroup(GROUP_NAME,
                        0,
                        dwRequestedUpdateRate,
                        hClientGroup,
                        hTimeBias.AddrOfPinnedObject(),
                        hDeadband.AddrOfPinnedObject(),
                        LOCALE_ID,
                        out pSvrGroupHandle,
                        out pRevUpdateRate,
                        ref iidRequiredInterface,
                        out pobjGroup1);

                    // Initialize all IO interface pointers.
                    InitReqIOInterfaces();

                    //ItemDeffArray = new OPCITEMDEF[2];

                    //ItemDeffArray[0].szAccessPath = "";                   // Accesspath not needed for this sample
                    //ItemDeffArray[0].szItemID = ITEM1_NAME;          // Item ID,
                    //ItemDeffArray[0].bActive = 1;                    // item is active
                    //ItemDeffArray[0].hClient = 1;                    // client handle
                    //ItemDeffArray[0].dwBlobSize = 0;                    // blob size
                    //ItemDeffArray[0].pBlob = IntPtr.Zero;          // pointer to blob
                    //ItemDeffArray[0].vtRequestedDataType = 2;                    // return values in native (cannonical) datatype

                    //ItemDeffArray[1].szAccessPath = "";                   // Accesspath not needed for this sample
                    //ItemDeffArray[1].szItemID = ITEM2_NAME;          // Item ID,
                    //ItemDeffArray[1].bActive = 1;                    // item is active
                    //ItemDeffArray[1].hClient = 2;                    // client handle
                    //ItemDeffArray[1].dwBlobSize = 0;                    // blob size
                    //ItemDeffArray[1].pBlob = IntPtr.Zero;          // pointer to blob
                    //ItemDeffArray[1].vtRequestedDataType = 2;                    // return values in native (cannonical) datatype


                    //Assigning the addresses
                    ItemDeffArray = new OPCITEMDEF[OPCAddresses.Count];

                    for (int i = 0; i < OPCAddresses.Count ; i++)
                    {
                        ItemDeffArray[i].szAccessPath = "";                                 // Accesspath not needed for this sample
                        ItemDeffArray[i].szItemID = OPCAddresses[i].ToString();             // Item ID,
                        ItemDeffArray[i].bActive = 1;                                       // item is active
                        ItemDeffArray[i].hClient = i + 1;                                   // client handle
                        ItemDeffArray[i].dwBlobSize = 0;                                    // blob size
                        ItemDeffArray[i].pBlob = IntPtr.Zero;                               // pointer to blob   
                        //split the address to check the datatype
                        // return values in native (cannonical) datatype
                        string[] str = OPCAddresses[i].ToString().Split(',');
                        if (str.Length > 1)
                        {
                            if (str[1].ToUpper().StartsWith("REAL"))
                            {
                                ItemDeffArray[i].vtRequestedDataType = (short)Enumeration.CanonicalDataType.ctReal;
                            }
                            else if (str[1].ToUpper().StartsWith("STRING"))
                            {
                                ItemDeffArray[i].vtRequestedDataType = (short)Enumeration.CanonicalDataType.ctString;
                            }
                        }
                        else
                        {
                            if (str[0].ToUpper().Contains('X'))
                            {
                                ItemDeffArray[i].vtRequestedDataType = (short)Enumeration.CanonicalDataType.ctBoolean;
                            }
                        }

                        if (str != null)
                        {
                            str = null;
                        }
                                              
                    }

                    
                    IntPtr pResults = IntPtr.Zero;
                    IntPtr pErrors = IntPtr.Zero;

                    try
                    {
                        // Add items to group
                        ((IOPCItemMgt)pobjGroup1).AddItems(OPCAddresses.Count, ItemDeffArray, out pResults, out pErrors);

                        // Unmarshal to get the server handles out fom the m_pItemResult
                        // after checking the errors
                        int[] errors = new int[OPCAddresses.Count];
                        IntPtr pos = pResults;

                        ItemSvrHandleArray = new int[OPCAddresses.Count];
                        Marshal.Copy(pErrors, errors, 0, OPCAddresses.Count);

                        for (int i = 0; i < OPCAddresses.Count ; i++)
                        {
                            if (errors[i] == 0)
                            {
                                if (i != 0)
                                {
                                    pos = new IntPtr(pos.ToInt32() + Marshal.SizeOf(typeof(OPCITEMRESULT)));
                                }
                                OPCITEMRESULT result = (OPCITEMRESULT)Marshal.PtrToStructure(pos, typeof(OPCITEMRESULT));
                                ItemSvrHandleArray[i] = result.hServer;
                            }
                            else
                            {
                                String strError;
                                pIOPCServer.GetErrorString(errors[i], LOCALE_ID, out strError);
                                throw new Exception(strError + " Result - Adding Item 0");
                                //MessageBox.Show(strError,
                                //    "Result - Adding Item " + i.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                                //boolResult = false;
                                //break;
                            }
                            // Destroy indirect structure elements
                            Marshal.DestroyStructure(pos, typeof(OPCITEMRESULT));
                        }

                        //if (errors[0] == 0)
                        //{
                        //    OPCITEMRESULT result = (OPCITEMRESULT)Marshal.PtrToStructure(pos, typeof(OPCITEMRESULT));
                        //    ItemSvrHandleArray[0] = result.hServer;
                        //}
                        //else
                        //{
                        //    String strError;
                        //    pIOPCServer.GetErrorString(errors[0], LOCALE_ID, out strError);
                        //    //throw new Exception(strError + " Result - Adding Item 0");
                        //    MessageBox.Show(strError,
                        //        "Result - Adding Item 0", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        //}
                        //// Destroy indirect structure elements
                        //Marshal.DestroyStructure(pos, typeof(OPCITEMRESULT));

                        //if (errors[1] == 0)
                        //{
                        //    pos = new IntPtr(pos.ToInt32() + Marshal.SizeOf(typeof(OPCITEMRESULT)));
                        //    OPCITEMRESULT result = (OPCITEMRESULT)Marshal.PtrToStructure(pos, typeof(OPCITEMRESULT));
                        //    ItemSvrHandleArray[1] = result.hServer;
                        //}
                        //else
                        //{
                        //    String strError;
                        //    pIOPCServer.GetErrorString(errors[1], LOCALE_ID, out strError);
                        //    //throw new Exception(strError + " Result - Adding Item 1");
                        //    MessageBox.Show(strError,
                        //        "Result - Adding Item 1", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //}
                        //// Destroy indirect structure elements
                        //Marshal.DestroyStructure(pos, typeof(OPCITEMRESULT));

                        //// if no errors
                        //// disable or enable controls.
                        //if ((errors[0] == 0) && (errors[1] == 0))
                        //{
                        //    //Frame1.Enabled = true;
                        //    //btnRead.Enabled = true;
                        //    //btnWrite.Enabled = true;
                        //    //btnStopConnection.Enabled = true;
                        //    //btnStartConnection.Enabled = false;
                        //    //strAWriteVal.Enabled = true;
                        //}
                    }
                    catch (System.Exception error) // catch for add items
                    {
                        throw new Exception(error.Message + " Result - Adding Items");
                        //MessageBox.Show(error.Message, "Result - Adding Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //boolResult = false;
                    }
                    finally
                    {
                        // Free the unmanaged COM memory
                        if (pResults != IntPtr.Zero)
                        {
                            Marshal.FreeCoTaskMem(pResults);
                            pResults = IntPtr.Zero;
                        }
                        if (pErrors != IntPtr.Zero)
                        {
                            Marshal.FreeCoTaskMem(pErrors);
                            pErrors = IntPtr.Zero;
                        }
                    }
                }
                catch (System.Exception error) // catch for group adding
                {
                    throw new Exception(String.Format("Error while creating group object:-{0}", error.Message) + " Result - Add group");
                    //MessageBox.Show(String.Format("Error while creating group object:-{0}", error.Message),
                    //    "Result - Add group", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //boolResult = false;
                }
                finally
                {
                    if (hDeadband.IsAllocated) hDeadband.Free();
                    if (hTimeBias.IsAllocated) hTimeBias.Free();
                }
            }
            catch (System.Exception error) // catch for server instance creation
            {
                throw new Exception(String.Format("Error while creating group object:-{0}", error.Message) + " Result - Create Server");
                //MessageBox.Show(String.Format("Error while creating server object:-{0}", error.Message),
                //    "Result - Create Server", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //boolResult = false;
            }
            return boolResult;
        }

        /// <summary>
        /// This function is called to initialise the async IO interface pointer
        /// </summary>
        private void InitReqIOInterfaces()
        {
            try
            {
                //Query interface for async calls on group object
                pIOPCAsyncIO2 = (IOPCAsyncIO2)pobjGroup1;

                pIOPCGroupStateMgt = (IOPCGroupStateMgt)pobjGroup1;

                // Query interface for callbacks on group object
                pIConnectionPointContainer = (IConnectionPointContainer)pobjGroup1;

                // Establish Callback for all async operations
                Guid iid = typeof(IOPCDataCallback).GUID;
                pIConnectionPointContainer.FindConnectionPoint(ref iid, out pIConnectionPoint);

                // Creates a connection between the OPC servers's connection point and
                // this client's sink (the callback object).
                pIConnectionPoint.Advise(this, out dwCookie);
            }
            catch (System.Exception error) // catch for group adding
            {
                throw new Exception(String.Format("Error while advising callbacks:-{0}", error.Message) + " Result - Add group");
                //MessageBox.Show(String.Format("Error while advising callbacks:-{0}", error.Message),
                //    "Result - Add group", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Callback IOPCDataCallback OnDataChange event handler implementation.
        /// This callback function is call by the opc server when any item value is changed.
        /// </summary>
        public virtual void OnDataChange(Int32 dwTransid,
            Int32 hGroup,
            Int32 hrMasterquality,
            Int32 hrMastererror,
            Int32 dwCount,
            int[] phClientItems,
            object[] pvValues,
            short[] pwQualities,
            OpcRcw.Da.FILETIME[] pftTimeStamps,
            int[] pErrors)
        {

            try
            {
                if (_arrResult == null)
                {
                    _arrResult = new ArrayList();
                    //_arrResult = _arrAddresses;
                    foreach (string str in _arrAddresses)
                    {
                        _arrResult.Add(str);
                    }
                }
                for (int nCount = 0; nCount < dwCount; nCount++)
                {
                    //get the values of item 0
                    if (pErrors[nCount] == 0)
                    {   
                        ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = false;
                        // Value
                        //_arrResult.Add(_arrAddresses[phClientItems[nCount] - 1] + "@" + pvValues[nCount]); 
                        //_arrResult[phClientItems[nCount] - 1] += "@" + pvValues[nCount];
                        
                        string[] strSplited = _arrResult[phClientItems[nCount] - 1].ToString().Split('@');
                        if (strSplited.Length > 0)
                        {
                            _arrResult[phClientItems[nCount] - 1] = strSplited[0] + "@" + pvValues[nCount];
                        }
                        else
                        {
                            _arrResult[phClientItems[nCount] - 1] = _arrResult[phClientItems[nCount] - 1] + "@" + pvValues[nCount];
                        }
                        if (GetQuality(pwQualities[nCount]) != "Good")
                        {
                            _arrResult[phClientItems[nCount] - 1] += "@" + GetQuality(pwQualities[nCount]);
                        }
                        ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = true;
                    }
                    else
                    {
                        String strItemErr;
                        pIOPCServer.GetErrorString(pErrors[0], LOCALE_ID, out strItemErr);
                        throw new Exception(strItemErr);
                        //MessageBox.Show(strItemErr,
                        //    "OnDataChange-Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    if (_arrAddresses[phClientItems[nCount] - 1].ToString().StartsWith(strOPCACKAddress))
                    {
                        _boolAck = true;
                    }
                }
                if (_boolAck && boolIsDB == true)
                {
                    ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = false;
                    OnUpdateEvent(this, new OnUpdateEventArgs(_arrResult));
                    _boolAck = false;
                    _arrResult = null;
                    ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = true;
                }
                if (boolIsDB == false)
                {
                    ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = false;
                    //Dispatcher.Invoke(new Action(OnUpdateEvent(this, new OnUpdateEventArgs(_arrResult))));
                    //ThreadStart start = delegate()
                    //{
                    OnUpdateEvent(this, new OnUpdateEventArgs(_arrResult));
                    //};
                    //new Thread(start).Start();
                    _arrResult = null;
                    ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = true;
                }
            }
            catch 
            {
                for (int nCount = 0; nCount < _arrResult.Count; nCount++)
                {
                    string[] strSplited = _arrResult[nCount].ToString().Split('@');
                    if (strSplited.Length > 0)
                    {
                        _arrResult[nCount] = strSplited[0] + "@@Bad";
                    }
                    else
                    {
                        _arrResult[nCount] = _arrResult[nCount] + "@@Bad";
                    }
                }

                OnUpdateEvent(this, new OnUpdateEventArgs(_arrResult));
                //throw exp;
                //MessageBox.Show(exp.Message,
                //    "OnDataChange-Runtime Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }

        /// <summary>
        /// Callback IOPCDataCallback OnReadComplete event handler implementation.
        /// This callback function is call by the opc server when async read is compleat.
        /// </summary>
        public virtual void OnReadComplete(System.Int32 dwTransid,
            System.Int32 hGroup,
            System.Int32 hrMasterquality,
            System.Int32 hrMastererror,
            System.Int32 dwCount,
            int[] phClientItems,
            object[] pvValues,
            short[] pwQualities,
            OpcRcw.Da.FILETIME[] pftTimeStamps,
            int[] pErrors)
        {
            try
            {
                if (pErrors[0] == 0)
                {
                    ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = false;
                    //// Value
                    //strAReadVal.Text = String.Format("{0}", pvValues[0]);
                    //// Quality
                    //strAReadQuality.Text = GetQuality(pwQualities[0]);
                    //// Timestamp
                    //DateTime dt = ToDateTime(pftTimeStamps[0]);
                    //strAReadTimeStp.Text = dt.ToString();
                    ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = true;
                }
                else
                {
                    String strResult = "";
                    pIOPCServer.GetErrorString(pErrors[0], LOCALE_ID, out strResult);
                    MessageBox.Show(strResult,
                        "Result - OnReadCOmpleate", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (System.Exception exp)
            {
                MessageBox.Show(exp.Message,
                    "OnReadComplete-Runtime Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

      
        /// <summary>
        /// Callback IOPCDataCallback OnWriteComplete event handler implementation.
        /// This callback function is call by the opc server when async write is compleat.
        /// </summary>
        public virtual void OnWriteComplete(System.Int32 dwTransid,
            System.Int32 hGroup,
            System.Int32 hrMastererr,
            System.Int32 dwCount,
            int[] pClienthandles,
            int[] pErrors)
        {
            if (_arrResult == null)
            {
                _intWriteCount = 0;
                _boolWrite = true;
                _arrResult = new ArrayList();
                foreach (string str in _arrAddresses)
                {
                    _arrResult.Add(str);
                }
            }
            _intWriteCount++;
            ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = false;
            String strResult = "";
            pIOPCServer.GetErrorString(pErrors[0], LOCALE_ID, out strResult);

            if (pErrors[0] != 0)
            {
                _arrResult[pClienthandles[0] - 1] += "@";
            }

            if (OPCAddresses[pClienthandles[0] - 1].ToString().StartsWith(strOPCACKAddress))
            {
                _boolAck = true;
            }

            if (_boolAck && _intWriteCount == _arrResult.Count)
            {
                foreach (string str in _arrResult)
                {
                    if (str.Contains('@'))
                    {
                        _boolWrite = false;
                        break;
                    }
                }
                OnWriteCompleteEvent(this, new OnWriteCompleteEventArgs(_boolWrite));
                _arrResult = null;
            }

            //OnWriteCompleteEvent(this, new OnWriteCompleteEventArgs(strResult, pErrors, OPCAddresses[pClienthandles[0] - 1].ToString()));
            ThreadExceptionDialog.CheckForIllegalCrossThreadCalls = true;
            Dispose(true);
        }

        /// <summary>
        /// Callback IOPCDataCallback OnCancelComplete event handler implementation.
        /// <summary>
        public virtual void OnCancelComplete(System.Int32 dwTransid, System.Int32 hGroup)
        {
            // Not implemented in this sample.
        }

        /// <summary>
        /// This is enable async the value
        /// </summary>
        public void AsyncReading()
        {
            IntPtr pRequestedUpdateRate = IntPtr.Zero;
            int nRevUpdateRate = 0;
            IntPtr hClientGroup = IntPtr.Zero;
            IntPtr pTimeBias = IntPtr.Zero;
            IntPtr pDeadband = IntPtr.Zero;
            IntPtr pLCID = IntPtr.Zero;
            int nActive = 0;

            // activates or deactivates group according to checkbox status
            GCHandle hActive = GCHandle.Alloc(nActive, GCHandleType.Pinned);
            hActive.Target = 1;

            try
            {
                pIOPCGroupStateMgt.SetState(pRequestedUpdateRate, out nRevUpdateRate,
                    hActive.AddrOfPinnedObject(), pTimeBias, pDeadband, pLCID, hClientGroup);
            }
            catch (System.Exception error)
            {
                throw new Exception(error.Message + " Result-Change Group State");
                //MessageBox.Show(error.Message,
                //    "Result-Change Group State", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                hActive.Free();
            }
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        public void Dispose(bool disposing)
        {
            // disposing -- True to release both managed and unmanaged resources;
            //              False to release only unmanaged resources.
            if (disposing)
            {
                
                // Un advice the connection point.
                if (dwCookie != 0)
                {
                    pIConnectionPoint.Unadvise(dwCookie);
                    dwCookie = 0;
                }
                if (pIConnectionPoint != null)
                {
                    Marshal.ReleaseComObject(pIConnectionPoint);
                    pIConnectionPoint = null;
                }
                if (pIConnectionPointContainer != null)
                {
                    Marshal.ReleaseComObject(pIConnectionPointContainer);
                    pIConnectionPointContainer = null;
                }
                if (pIOPCAsyncIO2 != null)
                {
                    Marshal.ReleaseComObject(pIOPCAsyncIO2);
                    pIOPCAsyncIO2 = null;
                }
                if (pIOPCGroupStateMgt != null)
                {
                    Marshal.ReleaseComObject(pIOPCGroupStateMgt);
                    pIOPCGroupStateMgt = null;
                }
                if (pobjGroup1 != null)
                {
                    Marshal.ReleaseComObject(pobjGroup1);
                    pobjGroup1 = null;
                }
                if (pIOPCServer != null)
                {
                    Marshal.ReleaseComObject(pIOPCServer);
                    pIOPCServer = null;
                }
                if (_arrAddresses != null)
                {
                    _arrAddresses = null;
                }
            }
            else
            {
                // Un advice the connection point.
                if (dwCookie != 0)
                {
                    pIConnectionPoint.Unadvise(dwCookie);
                    dwCookie = 0;
                }

                Marshal.ReleaseComObject(pIConnectionPoint);
                pIConnectionPoint = null;

                Marshal.ReleaseComObject(pIConnectionPointContainer);
                pIConnectionPointContainer = null;

                if (pIOPCAsyncIO2 != null)
                {
                    Marshal.ReleaseComObject(pIOPCAsyncIO2);
                    pIOPCAsyncIO2 = null;
                }
                if (pIOPCGroupStateMgt != null)
                {
                    Marshal.ReleaseComObject(pIOPCGroupStateMgt);
                    pIOPCGroupStateMgt = null;
                }
                if (pobjGroup1 != null)
                {
                    Marshal.ReleaseComObject(pobjGroup1);
                    pobjGroup1 = null;
                }
                if (pIOPCServer != null)
                {
                    Marshal.ReleaseComObject(pIOPCServer);
                    pIOPCServer = null;
                }
                if (_arrAddresses != null)
                {
                    _arrAddresses = null;
                }
            }
            //base.Dispose(disposing);
        }

        /// <summary>
        /// This function is called when the user clicks the write button.
        /// Async. Write function is called.
        /// </summary>
        /// <param name="Address">OPC address(string).</param>
        /// <param name="Value">Value(string).</param>
        public void Write(string strAddress, string strValue)
        {
            //OPCAddresses = new ArrayList();
            //OPCAddresses.Add(strAddress);
            //MessageBox.Show(strAddress + "  " +OPCAddresses.IndexOf(strAddress).ToString());
            

            //return;

            int nCancelid;
            IntPtr pErrors = IntPtr.Zero;
            object[] values = new object[1];
            values[0] = strValue;

            if (pIOPCAsyncIO2 != null)
            {
                try
                {   // Async write                    
                    pIOPCAsyncIO2.Write(2, new int[1] { OPCAddresses.IndexOf(strAddress) + 1}, values, nTransactionID + 1, out nCancelid, out pErrors);
                    //pIOPCAsyncIO2.Write(2, ItemSvrHandleArray, values, nTransactionID + 1, out nCancelid, out pErrors);
                    int[] errors = new int[1];
                    Marshal.Copy(pErrors, errors, 0, 1);
                    if (errors[0] != 0)
                    {
                        System.Exception ex = new Exception("Error in reading item");
                        throw ex;                        
                    }
                }
                catch (System.Exception error)
                {
                    //MessageBox.Show(error.Message,
                    //    "Result-Async Read", MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                    throw error;
                }
                finally
                {
                    // Free the unmanaged COM memory
                    if (pErrors != IntPtr.Zero)
                    {
                        Marshal.FreeCoTaskMem(pErrors);
                        pErrors = IntPtr.Zero;
                    }
                    //Dispose(true);                    
                }                
            }
        }

        /// <summary>
        /// This function extructs the quality value. The Quality values are as specified by OPC foundation.
        /// </summary>
        private String GetQuality(long wQuality)
        {
            String strQuality = "";
            switch (wQuality)
            {
                case Qualities.OPC_QUALITY_GOOD:
                    strQuality = "Good";
                    break;
                case Qualities.OPC_QUALITY_BAD:
                    strQuality = "Bad";
                    break;
                case Qualities.OPC_QUALITY_CONFIG_ERROR:
                    strQuality = "BadConfigurationError";
                    break;
                case Qualities.OPC_QUALITY_NOT_CONNECTED:
                    strQuality = "BadNotConnected";
                    break;
                case Qualities.OPC_QUALITY_DEVICE_FAILURE:
                    strQuality = "BadDeviceFailure";
                    break;
                case Qualities.OPC_QUALITY_SENSOR_FAILURE:
                    strQuality = "BadSensorFailure";
                    break;
                case Qualities.OPC_QUALITY_COMM_FAILURE:
                    strQuality = "BadCommFailure";
                    break;
                case Qualities.OPC_QUALITY_OUT_OF_SERVICE:
                    strQuality = "BadOutOfService";
                    break;
                case Qualities.OPC_QUALITY_WAITING_FOR_INITIAL_DATA:
                    strQuality = "BadWaitingForInitialData";
                    break;
                case Qualities.OPC_QUALITY_EGU_EXCEEDED:
                    strQuality = "UncertainEGUExceeded";
                    break;
                case Qualities.OPC_QUALITY_SUB_NORMAL:
                    strQuality = "UncertainSubNormal";
                    break;
                default:
                    strQuality = "Not handled";
                    break;
            }
            return strQuality;
        }
        #endregion
    }
}
