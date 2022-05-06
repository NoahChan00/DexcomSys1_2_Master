using System;
using System.Windows;
using Logix;
using System.Threading;


namespace PentagonHMI.LogicClasses
{
    class OEE : IDisposable
    //class OEE : VirtualClient.VirOEE, IDisposable
    {
        private VanillaDB.DataDBCall DBCall;
        DateTime Date;
        #region Variables
        LogicClasses.Main _MainConnection;
        //  TCPCommunicator.TCPOEE TcpOEE;
        public delegate void onUpdateHandler();
        public event onUpdateHandler OnUpdate;

        //Utilities.TcpAllenB plc = new Utilities.TcpAllenB();
        //Controller oeePLC = new Controller();
        // Change to connect to Main PLC
        //Utilities.TcpAllenB oeePLC;

        Controller myplc = new Controller();
        string ERROR_MSG = "";
        string RTN_MSG = "";
        int DataTotal { get; set; } = 0;
        int DataGood { get; set; } = 0;
        int DataBad { get; set; } = 0;

        ////TagGroup OeeTagGroup = new TagGroup();
        ////TagGroup OeeTagGroup2 = new TagGroup();
        ////public string PlcError { get; set; }
        ////public string PlcRtnMsg { get; set; }
        Tag Tag_str_HMIOEEStartDateTime;
        Tag Tag_Dint_TotalPass;
        Tag Tag_Dint_TotalFail;
        Tag Tag_int_SoftJamCount;
        Tag Tag_int_HardJamCount;
        Tag Tag_Dint_MachineUpTimeMin;
        Tag Tag_Dint_MachineOperationMin;
        Tag Tag_Dint_MachineDownTimeMin;
        Tag Tag_Dint_MachineIdleTimeMin;
        Tag Tag_Dint_MTBA;
        Tag Tag_Dint_MTBF;
        Tag Tag_NoMaterialTime;
        #endregion

        #region Constructor
        /*
        public OEE(ref LogicClasses.Main MainConnection)
        {
            _MainConnection = MainConnection;
        }
        */
        public OEE(ref LogicClasses.Main MainConnection)
        {
            string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
            DBCall = new VanillaDB.DataDBCall(Connstr);


            //Tag_str_HMIOEEStartDateTime = new Tag("str_HMIOEEStartDateTime");
            //Tag_Dint_TotalPass = new Tag("Dint_TotalPass");
            //Tag_Dint_TotalFail = new Tag("Dint_TotalFail");
            //Tag_int_SoftJamCount = new Tag("int_SoftJamCount");
            //Tag_int_HardJamCount = new Tag("int_HardJamCount");
            //Tag_Dint_MachineUpTimeMin = new Tag("Dint_MachineUpTimeSec");
            //Tag_Dint_MachineOperationMin = new Tag("Dint_MachineOperationSec");
            //Tag_Dint_MachineDownTimeMin = new Tag("Dint_MachineDownTimeSec");
            //Tag_Dint_MachineIdleTimeMin = new Tag("Dint_MachineIdleTimeSec");
            //Tag_Dint_MTBA = new Tag("Dint_MTBA");
            //Tag_Dint_MTBF = new Tag("Dint_MTBF");

            Tag_str_HMIOEEStartDateTime = new Tag("OEE_Tags.str_HMI_OEEStartDateTime");
            Tag_Dint_TotalPass = new Tag("OEE_Tags.dint_Total_Pass");
            Tag_Dint_TotalFail = new Tag("OEE_Tags.dint_Total_Fail");
            Tag_int_SoftJamCount = new Tag("OEE_Tags.dint_SoftJam");
            Tag_int_HardJamCount = new Tag("OEE_Tags.dint_HardJam");
            Tag_Dint_MachineUpTimeMin = new Tag("OEE_Tags.dint_MachineUpTimeAccSec");
            Tag_Dint_MachineOperationMin = new Tag("OEE_Tags.dint_MachineOperationTimeAccSec");
            Tag_Dint_MachineDownTimeMin = new Tag("OEE_Tags.dint_MachineDownTimeAccSec");
            Tag_Dint_MachineIdleTimeMin = new Tag("OEE_Tags.dint_MachineIdlingTimeAccSec");
            Tag_Dint_MTBA = new Tag("OEE_Tags.dint_MTBASec");
            Tag_Dint_MTBF = new Tag("OEE_Tags.dint_MTBFSec");

            Tag_NoMaterialTime = new Tag("OEE_Tags.dint_MachineNoMaterialTimeAccSec");


            _MainConnection = MainConnection;
            //oeePLC = MainConnection.plc;

            _MainConnection.OnOEEUpdate += new LogicClasses.Main.onOEEUpdateHandler(TcpOEE_OnUpdate);

            myplc = _MainConnection.MyPLC;


            //////Change to connect to Main PLC
            ////int i = 0;
            ////do
            ////{
            ////    if (!PLC_Connect())
            ////    {
            ////        string logMsg = $"Fail Connect PLC : ({i})\n{PlcError}";
            ////        if (i > 3)
            ////            MessageBox.Show(logMsg);
            ////        Utilities.FileLogger.logError(logMsg, "OEE->PLC_Connect");
            ////        if (oeePLC.IsConnected)
            ////        {
            ////            i = 99;
            ////        }
            ////        else
            ////        {
            ////            i++;
            ////        }
            ////    }
            ////    else
            ////    {
            ////        //TcpOEE_OnUpdate();
            ////        //PLC_GetOEEdata();


            ////        //Change to data pulling. By SA:19-Jul-2018 
            ////        //OeeTagGroup.Controller = oeePLC;
            ////        //OeeTagGroup.ScanStart();
            ////        //Add_PLC_TagGroup();
            ////        i = 99;
            ////    }

            ////} while (i < 5);

        }
        ////private void Add_PLC_TagGroup()
        ////{
        ////    PLC_AddTag("str_HMIOEEStartDateTime", Tag.ATOMIC.STRING, OeeTagGroup, new EventHandler(tagValueChange));
        ////    PLC_AddTag("int_SoftJamCount", Tag.ATOMIC.INT, OeeTagGroup, new EventHandler(tagValueChange));
        ////    PLC_AddTag("int_HardJamCount", Tag.ATOMIC.INT, OeeTagGroup, new EventHandler(tagValueChange));
        ////    PLC_AddTag("Dint_MachineUpTimeMin", Tag.ATOMIC.DINT, OeeTagGroup, new EventHandler(tagValueChange));
        ////    PLC_AddTag("Dint_MachineOperationMin", Tag.ATOMIC.DINT, OeeTagGroup, new EventHandler(tagValueChange));
        ////    PLC_AddTag("Dint_MachineDownTimeMin", Tag.ATOMIC.DINT, OeeTagGroup, new EventHandler(tagValueChange));
        ////    PLC_AddTag("Dint_MachineIdleTimeMin", Tag.ATOMIC.DINT, OeeTagGroup, new EventHandler(tagValueChange));
        ////    PLC_AddTag("Dint_MTBA", Tag.ATOMIC.DINT, OeeTagGroup, new EventHandler(tagValueChange));
        ////    PLC_AddTag("Dint_MTBF", Tag.ATOMIC.DINT, OeeTagGroup, new EventHandler(tagValueChange));
        ////    PLC_AddTag("Dint_TotalPass", Tag.ATOMIC.DINT, OeeTagGroup, new EventHandler(tagValueChange));
        ////    PLC_AddTag("Dint_TotalFail", Tag.ATOMIC.DINT, OeeTagGroup, new EventHandler(tagValueChange));

        ////    //OeeTagGroup2
        ////    //PLC_AddTag("str_OEEStartYear", Tag.ATOMIC.STRING, OeeTagGroup2, new EventHandler(tagValueChange2));
        ////    //PLC_AddTag("str_OEEStartMonth", Tag.ATOMIC.STRING, OeeTagGroup2, new EventHandler(tagValueChange2));
        ////    //PLC_AddTag("str_OEEStartDay", Tag.ATOMIC.STRING, OeeTagGroup2, new EventHandler(tagValueChange2));
        ////    //PLC_AddTag("str_OEEStartHour", Tag.ATOMIC.STRING, OeeTagGroup2, new EventHandler(tagValueChange2));
        ////    //PLC_AddTag("str_OEEStartMin", Tag.ATOMIC.STRING, OeeTagGroup2, new EventHandler(tagValueChange2));
        ////}
        ////private void tagValueChange(Object send, EventArgs e)
        ////{
        ////    DataChangeEventArgs args = (DataChangeEventArgs)e;
        ////    //MessageBox.Show("From Group 1\nArg Value : " + args.Value + "\nObject : " + args.MyObject.ToString());
        ////    string logMsg = "From Group 1\nArg Value : " + args.Value + "\nObject : " + args.MyObject.ToString();
        ////    Utilities.FileLogger.logError(logMsg, "TagValueChange");
        ////    try
        ////    {
        ////        switch (args.MyObject.ToString())
        ////        {
        ////            case "str_HMIOEEStartDateTime":  // StartDateTime bool_HMI_Selection str_HMIOEEStartDateTime
        ////                if (args.Value != null)
        ////                {
        ////                    string sTime = args.Value.ToString();
        ////                    if (sTime != "")
        ////                    {
        ////                        //StartDateTime = Convert.ToDateTime(sTime.ToString());
        ////                        StartDateTime = new DateTime(Convert.ToInt16(sTime.Substring(0, 4)),
        ////                                                     Convert.ToInt16(sTime.Substring(4, 2)),
        ////                                                     Convert.ToInt16(sTime.Substring(6, 2)),
        ////                                                     Convert.ToInt16(sTime.Substring(8, 2)),
        ////                                                     Convert.ToInt16(sTime.Substring(10, 2)), 0);
        ////                    }
        ////                    else
        ////                        StartDateTime = DateTime.Now.AddYears(-100);
        ////                }
        ////                break;

        ////            case "int_SoftJamCount":    // SoftJam
        ////                if (args.Value != null)
        ////                    SoftJam = Convert.ToInt16(args.Value.ToString());
        ////                break;
        ////            case "int_HardJamCount":    // HardJam
        ////                if (args.Value != null)
        ////                    HardJam = Convert.ToInt16(args.Value.ToString());
        ////                break;

        ////            case "Dint_MachineUpTimeMin":   // SystemUpTime
        ////                if (args.Value != null)
        ////                    SystemUpTime = new TimeSpan(0, 0, Convert.ToInt32(args.Value.ToString()), 0, 0);
        ////                break;

        ////            case "Dint_MachineOperationMin":    // OperationTime
        ////                if (args.Value != null)
        ////                    OperationTime = new TimeSpan(0, 0, Convert.ToInt32(args.Value.ToString()), 0, 0);
        ////                break;

        ////            case "Dint_MachineDownTimeMin":     // DownTime
        ////                if (args.Value != null)
        ////                    DownTime = new TimeSpan(0, 0, Convert.ToInt32(args.Value), 0, 0);
        ////                break;

        ////            case "Dint_MachineIdleTimeMin": // IdlingTime, IdealCycleTime
        ////                if (args.Value != null)
        ////                {
        ////                    IdlingTime = new TimeSpan(0, 0, Convert.ToInt32(args.Value), 0, 0);
        ////                    IdealCycleTime = new TimeSpan(0, 0, Convert.ToInt32(args.Value), 0, 0);
        ////                }
        ////                break;

        ////            case "Dint_MTBA":   //MTBA
        ////                if (args.Value != null)
        ////                    MTBA = new TimeSpan(0, 0, Convert.ToInt32(args.Value), 0, 0);
        ////                break;

        ////            case "Dint_MTBF":   // MTBF
        ////                if (args.Value != null)
        ////                    MTBF = new TimeSpan(0, 0, Convert.ToInt32(args.Value), 0, 0);
        ////                break;

        ////            case "Dint_TotalPass":   // Total Pass
        ////                if (args.Value != null)
        ////                    TotalPass = Convert.ToInt32(args.Value);
        ////                break;

        ////            case "Dint_TotalFail":   // Total Pass
        ////                if (args.Value != null)
        ////                    TotalFail = Convert.ToInt32(args.Value);
        ////                break;

        ////            default:
        ////                MessageBox.Show("Return Undefine");
        ////                Utilities.FileLogger.logError("Group Memeber Undefine", $"tagValueChange: {args.MyObject.ToString()} = {args.Value}");
        ////                break;
        ////        }
        ////    }
        ////    catch(Exception ex)
        ////    {
        ////        Utilities.FileLogger.logError(ex.Message, $"tagValueChange: {args.MyObject.ToString()} = {args.Value}");
        ////    }
        ////}
        private void tagValueChange2(Object send, EventArgs e)
        {
            DataChangeEventArgs args = (DataChangeEventArgs)e;
            //MessageBox.Show("From Group 2\nArg Value : " + args.Value + "\nObject : " + args.MyObject.ToString());

            switch (args.MyObject.ToString())
            {
                case "str_OEEStartYear":    // Year
                    if (args.Value.ToString().Length != 0)
                    {
                        int pYear = Convert.ToInt32(args.Value.ToString());
                        StartDateTime = new DateTime(pYear, StartDateTime.Month, StartDateTime.Day, StartDateTime.Hour, StartDateTime.Minute, 0);
                    }
                    break;

                case "str_OEEStartMonth":   // Month
                    if (args.Value.ToString().Length != 0)
                    {
                        int pMonth = Convert.ToInt32(args.Value.ToString());
                        StartDateTime = new DateTime(StartDateTime.Year, pMonth, StartDateTime.Day, StartDateTime.Hour, StartDateTime.Minute, 0);
                    }
                    break;

                case "str_OEEStartDay":    // Day
                    if (args.Value.ToString().Length != 0)
                    {
                        int pDay = Convert.ToInt32(args.Value.ToString());
                        StartDateTime = new DateTime(StartDateTime.Year, StartDateTime.Month, pDay, StartDateTime.Hour, StartDateTime.Minute, 0);
                    }
                    break;

                case "str_OEEStartHour":     // Hour
                    if (args.Value.ToString().Length != 0)
                    {
                        int pHour = Convert.ToInt32(args.Value.ToString());
                        StartDateTime = new DateTime(StartDateTime.Year, StartDateTime.Month, StartDateTime.Day, pHour, StartDateTime.Minute, 0);
                    }
                    break;

                case "str_OEEStartMin": // Min
                    if (args.Value.ToString().Length != 0)
                    {
                        int pMin = Convert.ToInt32(args.Value.ToString());
                        StartDateTime = new DateTime(StartDateTime.Year, StartDateTime.Month, StartDateTime.Day, StartDateTime.Hour, pMin, 0);
                    }
                    break;

                default:
                    // TotalFail ?
                    // TotalPass ?
                    //MessageBox.Show("Return Undefine");
                    Utilities.FileLogger.logError("Group Memeber Undefine", $"tagValueChange: {args.MyObject.ToString()} = {args.Value}");
                    break;
            }
        }

        private void PLC_GetOEEdata()
        {
            object rtnObj = null;
            string rdTag = "";

            //StartDateTime bool_HMI_Selection str_HMIOEEStartDateTime
            try
            {
                rdTag = "str_HMIOEEStartDateTime";
                rtnObj = oeePLC_Read(ref Tag_str_HMIOEEStartDateTime, Logix.Tag.ATOMIC.STRING);
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    string sTime = rtnObj.ToString();
                    if (sTime != "")
                    {
                        //StartDateTime = Convert.ToDateTime(sTime.ToString());
                        StartDateTime = new DateTime(Convert.ToInt32(sTime.Substring(0, 4)),
                                                     Convert.ToInt32(sTime.Substring(4, 2)),
                                                     Convert.ToInt32(sTime.Substring(6, 2)),
                                                     Convert.ToInt32(sTime.Substring(8, 2)),
                                                     Convert.ToInt32(sTime.Substring(10, 2)), 0);
                    }
                    else
                        StartDateTime = DateTime.Now.AddYears(-100);
                }
                else
                {
                    ////string logMsg = $"PLC_GetOEEdata: Read StartDateTime Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read StartDateTime Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(str_HMIOEEStartDateTime)");
                }


                //////// Replacing str_HMIOEEStartDateTime on StartDateTime by str_OEEStartYear/Month/Day/Hour/Min
                //////int iYear = DateTime.Now.Year, iMonth = DateTime.Now.Month, iDay = DateTime.Now.Day;
                //////int iHour = DateTime.Now.Hour, iMin = DateTime.Now.Minute;

                //////rtnObj = PLC_Read("str_OEEStartYear", Tag.ATOMIC.STRING);
                //////if (PlcError == "")
                //////{
                //////    if (rtnObj.ToString().Length != 0)
                //////        iYear = Convert.ToInt16(rtnObj.ToString());
                //////}
                //////rtnObj = PLC_Read("str_OEEStartMonth", Tag.ATOMIC.STRING);
                //////if (PlcError == "")
                //////{
                //////    if (rtnObj.ToString().Length != 0)
                //////        iMonth = Convert.ToInt16(rtnObj.ToString());
                //////}
                //////rtnObj = PLC_Read("str_OEEStartDay", Tag.ATOMIC.STRING);
                //////if (PlcError == "")
                //////{
                //////    if (rtnObj.ToString().Length != 0)
                //////        iDay = Convert.ToInt16(rtnObj.ToString());
                //////}
                //////rtnObj = PLC_Read("str_OEEStartHour", Tag.ATOMIC.STRING);
                //////if (PlcError == "")
                //////{
                //////    if (rtnObj.ToString().Length != 0)
                //////        iHour = Convert.ToInt16(rtnObj.ToString());
                //////}
                //////rtnObj = PLC_Read("str_OEEStartMin", Tag.ATOMIC.STRING);
                //////if (PlcError == "")
                //////{
                //////    if (rtnObj.ToString().Length != 0)
                //////        iMin = Convert.ToInt16(rtnObj.ToString());
                //////}
                //////StartDateTime = new DateTime(iYear, iMonth, iDay, iHour, iMin, 0);

                // TotalPass


                rdTag = "Dint_TotalPass";
                rtnObj = oeePLC_Read(ref Tag_Dint_TotalPass, Logix.Tag.ATOMIC.DINT);    // Was INT
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    TotalPass = Convert.ToInt32(rtnObj.ToString());
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read TotalPass Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read TotalPass Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(Dint_TotalPass)");
                }

                // TotalFail

                rdTag = "Dint_TotalFail";

                rtnObj = oeePLC_Read(ref Tag_Dint_TotalFail, Logix.Tag.ATOMIC.DINT);    //Was INT
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    TotalFail = Convert.ToInt32(rtnObj.ToString());
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read TotalFail Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read TotalFail Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(Dint_TotalFail)");
                }

                // SoftJam
                rdTag = "int_SoftJamCount";
                rtnObj = oeePLC_Read(ref Tag_int_SoftJamCount, Logix.Tag.ATOMIC.INT);
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    SoftJam = Convert.ToInt32(rtnObj.ToString());
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read SoftJam Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read SoftJam Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(int_SoftJamCount)");
                }

                // HardJam
                rdTag = "int_HardJamCount";
                rtnObj = oeePLC_Read(ref Tag_int_HardJamCount, Logix.Tag.ATOMIC.INT);
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    HardJam = Convert.ToInt32(rtnObj.ToString());
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read HardJam Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read HardJam Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(int_HardJamCount)");
                }


                // SystemUpTime
                rdTag = "Dint_MachineUpTimeMin";
                rtnObj = oeePLC_Read(ref Tag_Dint_MachineUpTimeMin, Logix.Tag.ATOMIC.DINT);
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    SystemUpTime = new TimeSpan(0, 0, 0, Convert.ToInt32(rtnObj.ToString()), 0);
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read SystemUpTime Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read SystemUpTime Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(Dint_MachineUpTimeMin)");
                }

                // OperationTime
                rdTag = "Dint_MachineOperationMin";
                rtnObj = oeePLC_Read(ref Tag_Dint_MachineOperationMin, Logix.Tag.ATOMIC.DINT);
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    OperationTime = new TimeSpan(0, 0, 0, Convert.ToInt32(rtnObj.ToString()), 0);
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read OperationTime Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read OperationTime Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(Dint_MachineOperationMin)");
                }

                // DownTime
                rdTag = "Dint_MachineDownTimeMin";
                rtnObj = oeePLC_Read(ref Tag_Dint_MachineDownTimeMin, Logix.Tag.ATOMIC.DINT);
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    DownTime = new TimeSpan(0, 0, 0, Convert.ToInt32(rtnObj.ToString()), 0);
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read DownTime Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read DownTime Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(Dint_MachineDownTimeMin)");
                }

                // IdealCycleTime
                //IdealCycleTime = new TimeSpan(0, 0, Convert.ToInt32(rtnObj.ToString()), 0, 0);
                IdealCycleTime = new TimeSpan(0, 0, 0, Convert.ToInt32(Classes.GlobalFunctions.OEEIdealCycleTimeSec.ToString()), 0);

                // IdlingTime
                rdTag = "Dint_MachineIdleTimeMin";
                rtnObj = oeePLC_Read(ref Tag_Dint_MachineIdleTimeMin, Logix.Tag.ATOMIC.DINT);
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    IdlingTime = new TimeSpan(0, 0, 0, Convert.ToInt32(rtnObj.ToString()), 0);
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read IdlingTime Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read IdlingTime Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(Dint_MachineIdleTimeMin)");
                }

                //MTBA
                rdTag = "Dint_MTBA";
                rtnObj = oeePLC_Read(ref Tag_Dint_MTBA, Logix.Tag.ATOMIC.DINT);
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    MTBA = new TimeSpan(0, 0, 0,Convert.ToInt32(rtnObj.ToString()),0);
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read MTBA Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read MTBA Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(Dint_MTBA)");
                }

                // MTBF
                rdTag = "Dint_MTBF";
                rtnObj = oeePLC_Read(ref Tag_Dint_MTBF, Logix.Tag.ATOMIC.DINT);
                //if (PlcError == "")
                if (ERROR_MSG == "")
                {
                    MTBF = new TimeSpan(0, 0, 0, Convert.ToInt32(rtnObj.ToString()), 0);
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read MTBF Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read MTBF Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "PLC_Read(Dint_MTBF)");
                }

                //MaterialNo
                rdTag = "NoMaterial";
                rtnObj = oeePLC_Read(ref Tag_NoMaterialTime, Tag.ATOMIC.DINT);
                if(ERROR_MSG == "")
                {
                    NoMaterial = new TimeSpan(0, 0, 0,Convert.ToInt32(rtnObj.ToString()), 0);
                }
                else
                {
                    //string logMsg = $"PLC_GetOEEdata: Read MTBF Error\n{PlcError}";
                    string logMsg = $"PLC_GetOEEdata: Read NoMaterial Error\n{ERROR_MSG}";
                    //MessageBox.Show(logMsg);
                    Utilities.FileLogger.logError(logMsg, "NoMaterial");
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, $"PLC_GetOEEdata : {rdTag}");
            }

        }

        private object oeePLC_Read(ref Tag myTag, Tag.ATOMIC tagType)
        {
            ERROR_MSG = "";
            if (DataTotal > 10000)
            {
                DataTotal = 1;
                DataBad = 0;
                DataGood = 1;
            }
            else
                DataTotal++;

            try
            {
                myTag.DataType = tagType;

                if (myplc.ReadTag(myTag) != ResultCode.E_SUCCESS)
                {
                    //MessageBox.Show("ERROR - " + MyPLC.ErrorCode + " : " + MyPLC.ErrorString);
                    ERROR_MSG = "ERROR - " + myplc.ErrorCode + " : " + myplc.ErrorString + " ; " + myTag.Name.ToString();
                    //MessageBox.Show(ERROR_MSG + " ; " + tagName);
                    Utilities.FileLogger.logError(ERROR_MSG, "PLC_Read Error");
                    DataBad++;
                    return null;
                }

                if (ResultCode.QUAL_GOOD == myTag.QualityCode)
                {
                    RTN_MSG = "RTN : " + myTag.Value.ToString() + "\n" + myTag.TimeStamp.ToString();
                    DataGood++;
                    return myTag.Value;
                }
                else
                {
                    ERROR_MSG = "ERROR = QUAL_BAD";
                    DataBad++;
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, $"oeePLC_Read : {myTag.Name.ToString()}");
            }
            return null;
        }

        #endregion

        #region Properties

        TimeSpan _tsNoMaterial;
        public TimeSpan NoMaterial
        {
            get
            {
                return _tsNoMaterial;
            }
            set
            {
                _tsNoMaterial = value;
            }
        }


        TimeSpan _tsMTBF;
        public TimeSpan MTBF
        {
            get
            {
                return _tsMTBF;
            }
            set
            {
                _tsMTBF = value;
            }
        }

        TimeSpan _tsMTBA;
        public TimeSpan MTBA
        {
            get
            {
                return _tsMTBA;
            }
            set
            {
                _tsMTBA = value;
            }
        }

        double _dblAvailability = 0.00;
        public double Availability
        {
            get
            {
                return _dblAvailability;
            }
            set
            {
                _dblAvailability = value;
            }
        }

        double _dblPerformanceRate = 0.00;
        public double PerformanceRate
        {
            get
            {
                return _dblPerformanceRate;
            }
            set
            {
                _dblPerformanceRate = value;
            }
        }

        double _dblEfficiencyRate = 0.00;
        public double EfficiencyRate
        {
            get
            {
                return _dblEfficiencyRate;
            }
            set
            {
                _dblEfficiencyRate = value;
            }
        }

        double _dblUPH = 0;
        public double UPH
        {
            get
            {
                return _dblUPH;
            }
            set
            {
                _dblUPH = value;
            }
        }

        public long TotalIn
        {
            get;
            set;
        }

        public long TotalOut
        {
            get;
            set;
        }

        public long TotalPass
        {
            get;
            set;
        }

        public bool StartLot
        {
            get;
            set;
        }

        public string LotID
        {
            get;
            set;
        }

        public long TotalFail
        {
            get;
            set;
        }

        public DateTime StartDateTime
        {
            get;
            set;
        }

        public TimeSpan SystemUpTime
        {
            get;
            set;
        }

        public TimeSpan OperationTime
        {
            get;
            set;
        }

        public TimeSpan DownTime
        {
            get;
            set;
        }

        public TimeSpan IdlingTime
        {
            get;
            set;
        }

        public TimeSpan IdealCycleTime
        {
            get;
            set;
        }

        public int SoftJam
        {
            get;
            set;
        }

        public int HardJam
        {
            get;
            set;
        }

        /*
         * Date: 2013-02-05
         * Author: Tammie
         * Descriptiom: Declare virtual method for calcOEE
         */
        public double calcOEE
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            ////PLC_Disconnect();
            //if (_MainConnection != null)
            //{
            //    _MainConnection.OnOEEUpdate -= new LogicClasses.Main.onOEEUpdateHandler(TcpOEE_OnUpdate);
            _MainConnection.OEEPageON = false;
            //}

            //if(plc!= null)
            //{
            //    plc = null;
            //}

            //if (TcpOEE != null)
            //{
            //    TcpOEE.OnUpdate -= new onUpdateHandler(TcpOEE_OnUpdate);
            //    TcpOEE.Dispose();
            // //   Classes.GlobalFunctions.Wait(200);
            //    TcpOEE = null;
            //}
        }

        public void Initialization()
        {
            //StartDateTime = DateTime.Now.AddDays(-2);   //Dummy for Testing only. By SA:26-Apr-2018

            //  TcpOEE.Initialization();
            /*
             * Date: 2013-02-04
             * Author: Jimmy
             * Description: Update OEE page on click.
             */

            //TcpOEE_OnUpdate();

            //  TcpOEE.Send("OEEP;");
        }

        public void Send(string Message)
        {
            //  TcpOEE.Send(Message);
        }

        public void OPCWrite(string TagName, string value)
        {
            //   TcpOEE.OPCWrite(TagName, value);
        }

        #endregion

        #region Events

        void TcpOEE_OnUpdate()
        {
            try
            {

                PLC_GetOEEdata();

                TotalIn = TotalPass + TotalFail;

                /*
                 * Description: Calculate Availability
                 * Formula: Availability = Operating Time / Planned Production Time
                 */
                if (this.SystemUpTime.TotalSeconds > 0)
                {
                    this.Availability = (this.OperationTime.TotalSeconds) / (this.SystemUpTime.TotalSeconds) * 100;
                }
                else
                {
                    this.Availability = 0;
                }

                /*
                 * Date: 2013-02-05
                 * Author: Tammie
                 * Description: Calculate Performance Rate
                 * Formula: Performance = Ideal Cycle Time / (Operating Time / Total Pieces)
                 */
                if (this.OperationTime.TotalSeconds > 0)
                {
                    this.PerformanceRate = (this.TotalIn * this.IdealCycleTime.TotalSeconds) / (this.OperationTime.TotalSeconds) * 100.0;
                    //this.PerformanceRate = (this.TotalOut * this.IdealCycleTime.TotalSeconds) / (this.OperationTime.TotalSeconds) * 100.0;
                }
                else
                {
                    this.PerformanceRate = 0;
                }

                //UPH
                //this.UPH = (this.TotalOut) / (this.OperationTime.TotalSeconds) * 3600;

                /*
                 * Date: 2013-02-05
                 * Author: Tammie
                 * Description: Calculate Efficiency Rate (Quality)
                 * Formula: Quality = Good Pieces / Total Pieces
                 */

                if (this.TotalIn > 0)
                {
                    this.EfficiencyRate = (Convert.ToDouble(this.TotalPass) / Convert.ToDouble(this.TotalIn)) * 100.0;
                    //this.EfficiencyRate = (this.TotalIn - this.TotalFail) / this.TotalIn * 100;                
                }
                else
                {
                    this.EfficiencyRate = 0;
                }
                /*
                 * Date: 2013-02-05
                 * Author: Tammie
                 * Description: Calculate OEE
                 * Formula: Quality = OEE = Availability x Performance x Quality
                 */
                this.calcOEE = ((Availability / 100.0) * (PerformanceRate / 100.0) * (EfficiencyRate / 100.0)) * 100.0;

                if (OnUpdate != null)
                    OnUpdate();

                Thread.Sleep(100);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "OEE Refresh Failed");
            }
        }

        void TcpOEE_OnUpdate_DB()
        {
            //string errMsg = "";
            //LEK 20180403 Get OEE Data

            //this.TotalFail = Utilities.Converter.ToInt32(TcpOEE.TotalFail);
            //this.TotalPass = Utilities.Converter.ToInt32(TcpOEE.TotalPass);
            //this.SystemUpTime = Utilities.Converter.ToTimeSpan(TcpOEE.SystemUpTime);
            //this.IdealCycleTime = Utilities.Converter.ToTimeSpan(TcpOEE.IdealCycleTime);
            //this.IdlingTime = Utilities.Converter.ToTimeSpan(TcpOEE.IdlingTime);
            //this.DownTime = Utilities.Converter.ToTimeSpan(TcpOEE.DownTime);
            //this.SoftJam = Utilities.Converter.ToInt32(TcpOEE.SoftJam);
            //this.HardJam = Utilities.Converter.ToInt32(TcpOEE.HardJam);
            //this.OperationTime = Utilities.Converter.ToTimeSpan(TcpOEE.OperationTime);
            //this.StartDateTime = Utilities.Converter.ToDateTime(TcpOEE.StartDateTime);


            /////PLC_GetOEEdata();

            //DB_GetOEEdata();
            //////For Testing. By SA:26-Apr-2018
            ////DataTable dt = DBCall.OEE_Select(Classes.GlobalFunctions.StationName.ToString(),ref errMsg);
            ////if(errMsg == "")
            ////{
            ////    if (dt.Rows.Count != 0)
            ////    {
            ////        if(!(string.IsNullOrWhiteSpace(dt.Rows[0]["StartDateTime"].ToString())) && DateTime.TryParse(dt.Rows[0]["StartDateTime"].ToString(),out Date))
            ////        StartDateTime = Convert.ToDateTime(dt.Rows[0]["StartDateTime"].ToString());

            ////        TotalFail = Convert.ToInt32(dt.Rows[0]["TotalFailed"].ToString());
            ////        TotalPass = Convert.ToInt32(dt.Rows[0]["TotalPassed"].ToString());
            ////        SoftJam = Convert.ToInt32(dt.Rows[0]["SoftJam"].ToString());
            ////        HardJam = Convert.ToInt32(dt.Rows[0]["HardJam"].ToString());

            ////        SystemUpTime = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["SystemUpTime"].ToString()));
            ////        OperationTime = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["OperationTime"].ToString()));
            ////        DownTime = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["DownTime"].ToString()));
            ////        IdlingTime = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["IdleTime"].ToString()));
            ////        IdealCycleTime = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["IdealTime"].ToString()));

            ////        MTBA = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["MTBA"].ToString()));
            ////        MTBF = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["MTBF"].ToString()));

            ////        //this.IdealCycleTime = Utilities.Converter.ToTimeSpan(TcpOEE.IdealCycleTime);
            ////        //this.SoftJam = Utilities.Converter.ToInt32(TcpOEE.SoftJam);
            ////        //this.HardJam = Utilities.Converter.ToInt32(TcpOEE.HardJam);
            ////    }
            ////}

            TotalIn = TotalPass + TotalFail;

            //MTBF
            //this.MTBF = this.DownTime;
            //if (this.MTBF.TotalSeconds > 0 && this.SoftJam > 0)
            //{
            //    this.MTBF = TimeSpan.FromTicks(this.MTBF.Ticks / this.HardJam);
            //}

            ////MTBA            
            //this.MTBA = this.SystemUpTime.Subtract(this.IdlingTime).Subtract(this.DownTime);

            //if (this.MTBA.TotalSeconds > 0 && this.SoftJam > 0)
            //{
            //    this.MTBA = TimeSpan.FromTicks(this.MTBA.Ticks / this.SoftJam);
            //}


            /*
             * Description: Calculate Availability
             * Formula: Availability = Operating Time / Planned Production Time
             */
            if (this.SystemUpTime.TotalSeconds > 0)
            {
                this.Availability = (this.OperationTime.TotalSeconds) / (this.SystemUpTime.TotalSeconds) * 100;
            }
            else
            {
                this.Availability = 0;
            }

            /*
             * Date: 2013-02-05
             * Author: Tammie
             * Description: Calculate Performance Rate
             * Formula: Performance = Ideal Cycle Time / (Operating Time / Total Pieces)
             */
            if (this.OperationTime.TotalSeconds > 0)
            {
                this.PerformanceRate = (this.TotalIn * this.IdealCycleTime.TotalSeconds) / (this.OperationTime.TotalSeconds) * 100;
                //this.PerformanceRate = (this.TotalOut * this.IdealCycleTime.TotalSeconds) / (this.OperationTime.TotalSeconds) * 100;
            }
            else
            {
                this.PerformanceRate = 0;
            }

            //UPH
            //this.UPH = (this.TotalOut) / (this.OperationTime.TotalSeconds) * 3600;

            /*
             * Date: 2013-02-05
             * Author: Tammie
             * Description: Calculate Efficiency Rate (Quality)
             * Formula: Quality = Good Pieces / Total Pieces
             */

            if (this.TotalIn > 0)
            {
                this.EfficiencyRate = (Convert.ToDouble(this.TotalPass) / Convert.ToDouble(this.TotalIn)) * 100.0;
                //this.EfficiencyRate = (this.TotalIn - this.TotalFail) / this.TotalIn * 100;                
            }
            else
            {
                this.EfficiencyRate = 0;
            }
            /*
             * Date: 2013-02-05
             * Author: Tammie
             * Description: Calculate OEE
             * Formula: Quality = OEE = Availability x Performance x Quality
             */
            this.calcOEE = ((Availability / 100.0) * (PerformanceRate / 100.0) * (EfficiencyRate / 100.0)) * 100.0;

            if (OnUpdate != null)
                OnUpdate();
        }

        private void DB_GetOEEdata()
        {

            //string errMsg = "";

            //////////Not valid due to PLC changes. SA:4-Jul-2018
            ////////DataTable dt = DBCall.OEE_Select(Classes.GlobalFunctions.StationName.ToString(), ref errMsg);
            ////////if (errMsg == "")
            ////////{
            ////////    if (dt.Rows.Count != 0)
            ////////    {
            ////////        if (!(string.IsNullOrWhiteSpace(dt.Rows[0]["StartDateTime"].ToString())) && DateTime.TryParse(dt.Rows[0]["StartDateTime"].ToString(), out Date))
            ////////            StartDateTime = Convert.ToDateTime(dt.Rows[0]["StartDateTime"].ToString());

            ////////        TotalFail = Convert.ToInt32(dt.Rows[0]["TotalFailed"].ToString());
            ////////        TotalPass = Convert.ToInt32(dt.Rows[0]["TotalPassed"].ToString());
            ////////        SoftJam = Convert.ToInt32(dt.Rows[0]["SoftJam"].ToString());
            ////////        HardJam = Convert.ToInt32(dt.Rows[0]["HardJam"].ToString());

            ////////        SystemUpTime = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["SystemUpTime"].ToString()));
            ////////        OperationTime = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["OperationTime"].ToString()));
            ////////        DownTime = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["DownTime"].ToString()));
            ////////        IdlingTime = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["IdleTime"].ToString()));
            ////////        //IdealCycleTime = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["IdealTime"].ToString()));

            ////////        MTBA = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["MTBA"].ToString()));
            ////////        MTBF = TimeSpan.FromSeconds(Convert.ToDouble(dt.Rows[0]["MTBF"].ToString()));

            ////////    }
            ////////}
        }



        ////private bool PLC_Connect()
        ////{
        ////    bool rtnFlag = true;

        ////    oeePLC.IPAddress = Classes.GlobalFunctions.PLC_IPAddress.ToString();
        ////    oeePLC.Path = Classes.GlobalFunctions.PLC_Path.ToString();
        ////    oeePLC.Timeout = Convert.ToUInt16(Classes.GlobalFunctions.PLC_Timeout.ToString());

        ////    if (oeePLC.Connect() != ResultCode.E_SUCCESS)
        ////    {
        ////        PlcError = $"ERROR - PLC_Connect(oee)\nIP={oeePLC.IPAddress}, Path={oeePLC.Path}, timeour={oeePLC.Timeout}\n { oeePLC.ErrorCode} : { oeePLC.ErrorString} ";
        ////        rtnFlag = false;
        ////    }
        ////    else
        ////    {
        ////        string logMsg = $"PLC_Connect(oee) OK\nIP={oeePLC.IPAddress}, Path={oeePLC.Path}, timeout={oeePLC.Timeout}\n";
        ////        Utilities.FileLogger.logError(logMsg, "Tracking Good OEE PLC_Connect");
        ////    }

        ////    return rtnFlag;
        ////}
        ////public bool PLC_Disconnect()
        ////{
        ////    bool rtnFlag = true;

        ////    oeePLC.Disconnect();

        ////    return rtnFlag;
        ////}
        ////public object PLC_Read(string tagName, Tag.ATOMIC tagType)
        ////{
        ////    PlcError = "";

        ////    Tag myTag = new Tag(tagName);
        ////    myTag.DataType = tagType;

        ////    if (oeePLC.ReadTag(myTag) != ResultCode.E_SUCCESS)
        ////    {
        ////        PlcError = $"PLC_Read TagName : [{tagName}] \nERROR : {oeePLC.ErrorCode} - { oeePLC.ErrorString}";
        ////        return null;
        ////    }

        ////    if (ResultCode.QUAL_GOOD == myTag.QualityCode)
        ////    {
        ////        PlcRtnMsg = "RTN : " + myTag.Value.ToString() + "\n" + myTag.TimeStamp.ToString();
        ////        return myTag.Value;
        ////    }

        ////    return null;
        ////}
        ////public bool PLC_AddTag(string tagName, Tag.ATOMIC tagType, TagGroup tGroup, EventHandler evenH)
        ////{
        ////    PlcError = "";

        ////    Tag myTag = new Tag(tagName);
        ////    myTag.DataType = tagType;

        ////    if (oeePLC.ReadTag(myTag) != ResultCode.E_SUCCESS)
        ////    {
        ////        PlcError = "ERROR - " + oeePLC.ErrorCode + " : " + oeePLC.ErrorString;
        ////        return false;
        ////    }

        ////    if (ResultCode.QUAL_GOOD == myTag.QualityCode)
        ////    {
        ////        myTag.Changed += evenH;
        ////        myTag.MyObject = myTag.Name.ToString();
        ////        tGroup.AddTag(myTag);
        ////        return true;
        ////    }
        ////    else
        ////    {
        ////        PlcError = "ERROR - " + oeePLC.ErrorCode + " : " + oeePLC.ErrorString;
        ////        return false;
        ////    }

        ////}

        #endregion

        #region Destructor
        ~OEE()
        {
            Dispose();
        }
        #endregion
    }
}
