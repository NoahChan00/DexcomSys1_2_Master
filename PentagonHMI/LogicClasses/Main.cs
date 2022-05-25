using Logix;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using PentagonHMI.Classes;
using System.Linq;
using SimpleOPC;
using System.IO;
using SimpleDatabase;
using Utilities;

namespace PentagonHMI.LogicClasses
{
    public class Main : IDisposable
    {
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private VanillaDB.DataDBCall DBCall;
        public TagName TN = new TagName();
        public Main_Custom Custom;
        public INGEAR_Opc OPC = new INGEAR_Opc();
        public SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName);
        public SQLCarrier MainSQLer;
        public DataTable dt_Config;
        public MachineInformationView machineInformationView = null;
        public TcpIpServer PLCEventLogListener = null;

        public string UserAccessLevel = "Operator";
        public string MachineStatus = "N/A";
        public string MachineName = "N/A";
        public double IdealCycleTime = 0;
        public string VisionIP = string.Empty;
        public string CheckPath = "";
        public string Cur_ShiftID = "N/A";
        public Dictionary<int, string> Dic_Reject_Index_Reason = new Dictionary<int, string>();
        public bool OFFDay = false;
        private string StationUPHTag = "OEE_Tags.dint_Hourly_UPH[0]";
        public List<bool> IValue { get; set; } = new List<bool>();
        public List<bool> OValue { get; set; } = new List<bool>();
        public bool HasTorqueDriver = false;
        public Tcpip_ArcadiaTorqueDriver Tcpip_ArcadiaTorqueDriver;
        public int connectionAttempt = 0;
        public int zoneCount = 0;
        public string TorqueDriverStartTag = string.Empty;

        public string SyncYearTag = "Shift_OEE_Tags.str_SyncYear";
        public string SyncMonthTag = "Shift_OEE_Tags.str_SyncMonth";
        public string SyncDayTag = "Shift_OEE_Tags.str_SyncDay";
        public string SyncHourTag = "Shift_OEE_Tags.str_SyncHour";
        public string SyncMinTag = "Shift_OEE_Tags.str_SyncMin";
        public string SyncSecTag = "Shift_OEE_Tags.str_SyncSec";
        public string SyncNanoSecTag = "Shift_OEE_Tags.str_SyncNanoSec";
        public string SyncTimeTag = "Shift_OEE_Tags.bool_SyncTime";

        public Controller CSSDController = null;
        public Controller DIMMController = null;
        public Controller PCIEController = null;


        #region Variables

        public string Z1_OrderRjtReason = string.Empty;
        public string Z2_OrderRjtReason = string.Empty;
        public string PartRjtReason = string.Empty;

        //public delegate void onUpdateHandler();
        //public event onUpdateHandler OnUpdate;

        public delegate void onHomeUpdateHandler();
        public event onHomeUpdateHandler Home_OnUpdate;

        //public delegate void onComponentLifeCycleUpdateHandler();
        //public event onComponentLifeCycleUpdateHandler ComponentLifeCycle_OnUpdate;

        public delegate void onENUpdateHandler();
        public event onENUpdateHandler EN_OnUpdate;

        //public delegate void onMainHMIUpdateHandler();
        //public event onMainHMIUpdateHandler MainHMI_OnUpdate;

        public delegate void onIOUpdateHandler();
        public event onIOUpdateHandler OnIOUpdate;

        public delegate void onIOLocUpdateHandler();
        public event onIOLocUpdateHandler OnIOLocUpdate;

        public delegate void onOEEUpdateHandler();
        public event onOEEUpdateHandler OnOEEUpdate;

        public delegate void onSettingUpdateHandler();
        public event onSettingUpdateHandler OnSettingUpdate;

        public delegate void onDryrunHandler();
        public event onDryrunHandler OnDryrunUpdate;
        
        public delegate void onTrayMapHandler();
        public event onTrayMapHandler OnTrayMapUpdate;

        public delegate void onDUTHandler();
        public event onDUTHandler OnDUTUpdate;

        public delegate void onLotHandler();
        public event onLotHandler OnLotUpdate;

        public delegate void onMotorHandler();
        public event onMotorHandler OnMotorUpdate;

        public delegate void OnAlwaysUpdateHandler();
        public event OnAlwaysUpdateHandler OnAlwaysUpdate;

        public delegate void OnFastUpdateHandler();
        public event OnFastUpdateHandler OnFastUpdate;

        public delegate void OnEngineeringHandler();
        public event OnEngineeringHandler OnEngineeringUpdate;

        public delegate void OnRejectBinDisplayHandler();
        public event OnRejectBinDisplayHandler OnRejectBinDisplayUpdate;

        public delegate void OnRackConfigurationHandler();
        public event OnRackConfigurationHandler OnRackConfigurationUpdate;

        public Utilities.TcpAllenB plc = new Utilities.TcpAllenB();

        public bool IOPageON = false, OEEPageON = false, MotorPageON = false, SettingPageON = false,
            IOLocPageON = false, HomePageON = false, EnPageOn = false, DryRunPageON = false,
            EngineeringPageOn = false, RejectBinDisplayPageOn = false, RackConfigurationPageOn = false,
            TrayMapPageOn = false, DUTPageOn = false, LotPageOn  = false;

        public bool HasErrorCheck = false, HasStationStatusCheck = false, HasBreakTimeCheck = false, HasLogManagement = false;
        
        public bool HasRackStatus = false;
        #endregion

        #region Constructor
        public Main()
        {
            Initialization();
        }

        #endregion

        #region Properties
        public string gUPHprevStr = "";
        public string gUPHcurrStr = "";
        public ProjectType StnType = GlobalFunctions.ProjectType;
        public virtual string StationID { get; set; }
        public int UPH { get; set; }
        public string[] Lst_UPH { get; set; }
        public Controller MyPLC { get; set; }
        #endregion

        #region Methods

        public void Dispose()
        {
            try
            {
                cancellationTokenSource.Cancel();
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        public void Initialization()
        {
            try
            {
                dt_Config = SQLer.Exec_DTSelect("SELECT * FROM [CONFIG]");
                Func<string, object> GetItem = (x) => dt_Config.Select().Where(y => y["Item"].ToString().ToUpper() == x.ToUpper()).First()["Info"];

                MachineName = GetItem("MachineName").ToString();

                StationID = GlobalFunctions.StationName.ToString();
                string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
                DBCall = new VanillaDB.DataDBCall(Connstr);
#if !DEBUG
                if (!plc.PLC_Connect(Classes.GlobalFunctions.PLC_IPAddress,
                                    Classes.GlobalFunctions.PLC_Path,
                                    Convert.ToInt32(Classes.GlobalFunctions.PLC_Timeout.ToString())))
                {
                    string logMsg = "Fail Connect PLC :\n" + plc.ERROR_MSG;
                    Utilities.FileLogger.logError(logMsg, "Main->PLC_Connect");
                }
#endif
                MyPLC = plc.GetController();


                //if (GlobalFunctions.ProjectType == ProjectType.ARCADIA && GlobalFunctions.StationType == StationType.ARCADIA_Main)
                if (GlobalFunctions.ProjectType == ProjectType.ARCADIA || GlobalFunctions.ProjectType == ProjectType.TLA)
                {
                    MainSQLer = new SQLCarrier(GlobalFunctions.ServerName, "gdb_DexcomSystem1", false);
                    //MainSQLer = new SQLCarrier("DESKTOP-MAIN", "gdb", true, "123321", "sa");
                }

                Info.OPC.Initialize();

                OPC.Connect(GlobalFunctions.PLC_IPAddress);

                dtNextTimeUpdate = DateTime.Now.AddMinutes(0);

                Custom = new Main_Custom(this);

                DataTable DT = SQLer.Exec_DTSelect("SELECT [REJECTCODE], [DESCRIPTION] FROM [REJECTREASON];");
                foreach (DataRow dr in DT.Rows)
                    Dic_Reject_Index_Reason.Add(Convert.ToInt32(dr["REJECTCODE"]), dr["DESCRIPTION"].ToString());

                //try
                //{
                //    if (GlobalFunctions.ProjectType == ProjectType.TLA && GlobalFunctions.StationType == StationType.ARCADIA_Main)
                //    {
                //        CSSDController = new Controller("194.168.3.12")
                //        {
                //            Path = GlobalFunctions.PLC_Path,
                //            Timeout = Convert.ToInt32(GlobalFunctions.PLC_Timeout)
                //        };

                //        DIMMController = new Controller("194.168.3.22")
                //        {
                //            Path = GlobalFunctions.PLC_Path,
                //            Timeout = Convert.ToInt32(GlobalFunctions.PLC_Timeout)
                //        };

                //        PCIEController = new Controller("194.168.3.32")
                //        {
                //            Path = GlobalFunctions.PLC_Path,
                //            Timeout = Convert.ToInt32(GlobalFunctions.PLC_Timeout)
                //        };

                //        try
                //        {
                //            for (int i = 0; i < 5; i++)
                //            {
                //                if (CSSDController.Connect() != Logix.ResultCode.E_SUCCESS)
                //                {
                //                    FileLogger.logError($"ERROR - PLC_Connect ({i})\nIP= {CSSDController.IPAddress}, Path={CSSDController.Path}, Timeout={CSSDController.Timeout}\n {CSSDController.ErrorCode} : {CSSDController.ErrorString}", "Tracking Bad TCP PLC_Connect");

                //                    if (CSSDController.IsConnected)
                //                    {
                //                        break;
                //                    }
                //                }
                //                else
                //                {
                //                    FileLogger.logError($"PLC_Connect(TcpAB) OK\nIP={CSSDController.IPAddress}, Path={CSSDController.Path}, timeout={CSSDController.Timeout}\n", "Tracking Good TCP PLC_Connect");
                //                    break;
                //                }
                //            }
                //        }
                //        catch (Exception exception)
                //        {
                //            FileLogger.logError(exception.Message, exception.ToString());
                //        }

                //        try
                //        {
                //            for (int i = 0; i < 5; i++)
                //            {
                //                if (DIMMController.Connect() != Logix.ResultCode.E_SUCCESS)
                //                {
                //                    FileLogger.logError($"ERROR - PLC_Connect ({i})\nIP= {DIMMController.IPAddress}, Path={DIMMController.Path}, Timeout={DIMMController.Timeout}\n {DIMMController.ErrorCode} : {DIMMController.ErrorString}", "Tracking Bad TCP PLC_Connect");

                //                    if (DIMMController.IsConnected)
                //                    {
                //                        break;
                //                    }
                //                }
                //                else
                //                {
                //                    FileLogger.logError($"PLC_Connect(TcpAB) OK\nIP={DIMMController.IPAddress}, Path={DIMMController.Path}, timeout={DIMMController.Timeout}\n", "Tracking Good TCP PLC_Connect");
                //                    break;
                //                }
                //            }
                //        }
                //        catch (Exception exception)
                //        {
                //            FileLogger.logError(exception.Message, exception.ToString());
                //        }

                //        try
                //        {
                //            for (int i = 0; i < 5; i++)
                //            {
                //                if (PCIEController.Connect() != Logix.ResultCode.E_SUCCESS)
                //                {
                //                    FileLogger.logError($"ERROR - PLC_Connect ({i})\nIP= {PCIEController.IPAddress}, Path={PCIEController.Path}, Timeout={PCIEController.Timeout}\n {PCIEController.ErrorCode} : {PCIEController.ErrorString}", "Tracking Bad TCP PLC_Connect");

                //                    if (PCIEController.IsConnected)
                //                    {
                //                        break;
                //                    }
                //                }
                //                else
                //                {
                //                    FileLogger.logError($"PLC_Connect(TcpAB) OK\nIP={PCIEController.IPAddress}, Path={PCIEController.Path}, timeout={PCIEController.Timeout}\n", "Tracking Good TCP PLC_Connect");
                //                    break;
                //                }
                //            }
                //        }
                //        catch (Exception exception)
                //        {
                //            FileLogger.logError(exception.Message, exception.ToString());
                //        }
                //    }
                //}
                //catch (Exception exception)
                //{
                //    FileLogger.logError(exception.Message, exception.ToString());
                //}

                new Task(() => GeneralLane(), cancellationTokenSource.Token, TaskCreationOptions.LongRunning).Start();
                new Task(() => FastLane(), cancellationTokenSource.Token, TaskCreationOptions.LongRunning).Start();
                new Task(() => PageLane(), cancellationTokenSource.Token, TaskCreationOptions.LongRunning).Start();

            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Main Page Initialization Failed");
            }

        }
#endregion

#region Events
        private void PageLane()
        {
            try
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    if (HomePageON && Home_OnUpdate != null)
                        Home_OnUpdate?.Invoke();

                    if (IOPageON && OnIOUpdate != null)
                        OnIOUpdate?.Invoke();

                    if (SettingPageON && OnSettingUpdate != null)
                        OnSettingUpdate?.Invoke();

                    if (IOLocPageON && OnIOLocUpdate != null)
                        OnIOLocUpdate?.Invoke();

                    if (MotorPageON && OnMotorUpdate != null)
                        OnMotorUpdate?.Invoke();

                    if (OEEPageON && OnOEEUpdate != null)
                        OnOEEUpdate?.Invoke();

                    if (DryRunPageON && OnDryrunUpdate != null)
                        OnDryrunUpdate?.Invoke();
                    
                    if (TrayMapPageOn)
                        OnTrayMapUpdate?.Invoke();
                    
                    if (DUTPageOn)
                        OnDUTUpdate?.Invoke();

                    if (LotPageOn)
                        OnLotUpdate?.Invoke();

                    if (EnPageOn && EN_OnUpdate != null)
                        EN_OnUpdate?.Invoke();

                    if (EngineeringPageOn && OnEngineeringUpdate != null)
                        OnEngineeringUpdate?.Invoke();

                    if (RejectBinDisplayPageOn && OnRejectBinDisplayUpdate != null)
                        OnRejectBinDisplayUpdate?.Invoke();

                    if (RackConfigurationPageOn && OnRackConfigurationUpdate != null)
                        OnRackConfigurationUpdate?.Invoke();

                    cancellationTokenSource.Token.WaitHandle.WaitOne(500);
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "PageLane");
            }
        }

        private void FastLane()
        {
            try
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    OnFastUpdate?.Invoke();
                    cancellationTokenSource.Token.WaitHandle.WaitOne(1);
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "FastLane");
            }
        }

        private void GeneralLane()
        {
            try
            {
                bool HasVisionResultLog = false, HasBarcodeResult1Log = false, HasBarcodeResult2Log = false,
                     HasOperatorLoadTime = false, HasInfoTagUpdate = false, HasUpdateOEEShift = false,
                     HasRejectReason = false, HasUPH = false, HasPLCSetTime = false, HasVTCBarcodeInputLog = false,
                     HasVTCRejectReason = false, HasVTCRackBarcodeLog = false, HasTestCSV = false, HasTesterStnUnitTracker = false, HasLaserStnUnitTracker = false, 
                     HasUnldStnUnitTracker = false, HasTnRStnUnitTracker = false, HasLotSummary = false;

                switch (GlobalFunctions.ProjectType)
                {
                    case ProjectType.DEXCOM:
                        HasErrorCheck = HasStationStatusCheck = HasLogManagement = HasBreakTimeCheck  = HasUpdateOEEShift = HasPLCSetTime = true;
                        HasTestCSV = HasTesterStnUnitTracker = HasLaserStnUnitTracker = HasUnldStnUnitTracker = HasTnRStnUnitTracker = HasLotSummary = true;
                        break;
                    case ProjectType.ARCADIA:
                        if (StationType.ARCADIA_Main == GlobalFunctions.StationType)
                        {
                            HasLogManagement = HasErrorCheck = HasStationStatusCheck = HasUpdateOEEShift = true;
                        }
                        else if (StationType.VISION == GlobalFunctions.StationType)
                        {
                            HasInfoTagUpdate = HasErrorCheck = HasStationStatusCheck = HasUPH = HasRejectReason = true;
                            HasUpdateOEEShift = HasBreakTimeCheck = true;
                            if (MachineName.ToUpper().Contains("FINAL"))
                                StationUPHTag = "OutputPnP_OEE_Tags.dint_Hourly_UPH[0]";
                            else if (MachineName.ToUpper().Contains("CENTRAL"))
                                StationUPHTag = "CenVis_OEE_Tags.dint_Hourly_UPH[0]";
                        }
                        else
                        {
                            HasInfoTagUpdate = HasUpdateOEEShift = HasRejectReason = HasUPH = HasPLCSetTime = true;
                            HasBreakTimeCheck = HasErrorCheck = HasStationStatusCheck = HasLogManagement = true;
                        }
                        break;
                    case ProjectType.DIMM:
                        HasVisionResultLog = HasBarcodeResult1Log = HasBarcodeResult2Log = HasInfoTagUpdate = HasUpdateOEEShift =
                        HasRejectReason = HasUPH = HasPLCSetTime = true;
                        HasBreakTimeCheck = HasErrorCheck = HasStationStatusCheck = HasLogManagement = true;
                        break;
                    case ProjectType.HDD:
                        HasOperatorLoadTime = HasInfoTagUpdate = HasUpdateOEEShift =
                        HasRejectReason = HasUPH = HasPLCSetTime = true;
                        HasBreakTimeCheck = HasErrorCheck = HasStationStatusCheck = HasLogManagement = true;
                        break;
                    case ProjectType.TLA:
                        HasVisionResultLog = HasInfoTagUpdate = HasUpdateOEEShift =
                        HasRejectReason = HasUPH = HasPLCSetTime = true;
                        HasBreakTimeCheck = HasErrorCheck = HasStationStatusCheck = HasLogManagement = true;

                        if (GlobalFunctions.StationType == StationType.ARCADIA_Main)
                        {
                            StationUPHTag = "Labelling_OEE_Tags.dint_Hourly_UPH[0]";
                        }

                        if (MachineName.ToUpper().Contains("FINAL"))
                        {
                            SyncYearTag = "OutputPnP_OEE_Tags.str_SyncYear";
                            SyncMonthTag = "OutputPnP_OEE_Tags.str_SyncMonth";
                            SyncDayTag = "OutputPnP_OEE_Tags.str_SyncDay";
                            SyncHourTag = "OutputPnP_OEE_Tags.str_SyncHour";
                            SyncMinTag = "OutputPnP_OEE_Tags.str_SyncMin";
                            SyncSecTag = "OutputPnP_OEE_Tags.str_SyncSec";
                            SyncNanoSecTag = "OutputPnP_OEE_Tags.str_SyncNanoSec";
                            SyncTimeTag = "OutputPnP_OEE_Tags.bool_SyncTime";

                            StationUPHTag = "OutputPnP_OEE_Tags.dint_Hourly_UPH[0]";
                        }
                        else if (MachineName.ToUpper().Contains("CENTRAL"))
                        {
                            SyncYearTag = "CenVis_OEE_Tags.str_SyncYear";
                            SyncMonthTag = "CenVis_OEE_Tags.str_SyncMonth";
                            SyncDayTag = "CenVis_OEE_Tags.str_SyncDay";
                            SyncHourTag = "CenVis_OEE_Tags.str_SyncHour";
                            SyncMinTag = "CenVis_OEE_Tags.str_SyncMin";
                            SyncSecTag = "CenVis_OEE_Tags.str_SyncSec";
                            SyncNanoSecTag = "CenVis_OEE_Tags.str_SyncNanoSec";
                            SyncTimeTag = "CenVis_OEE_Tags.bool_SyncTime";

                            StationUPHTag = "CenVis_OEE_Tags.dint_Hourly_UPH[0]";
                        }
                        break;
                    case ProjectType.VTC:
                        HasInfoTagUpdate = HasUpdateOEEShift = HasVTCRejectReason = HasVTCBarcodeInputLog = HasVTCRackBarcodeLog = HasUPH = HasPLCSetTime = true;
                        HasBreakTimeCheck = HasErrorCheck = HasStationStatusCheck = HasLogManagement = true;
                        break;
                    case ProjectType.LIFTER:
                        HasStationStatusCheck = HasErrorCheck = true;
                        break;
                    default:
                        break;
                }

                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    try
                    {
                        cancellationTokenSource.Token.WaitHandle.WaitOne(500);

                        OnAlwaysUpdate?.Invoke();

                        if (HasPLCSetTime) PLC_SET_TIME();
                        if (HasUpdateOEEShift) Custom.UpdateOEEShift();
                        if (HasUPH) Main_GetStationUPH();
                        if (HasRejectReason) LogRejectReason();
                        if (HasInfoTagUpdate) Info.OPC.Update();

                        if (HasBarcodeResult1Log) Custom.BarcodeResult(1);
                        if (HasBarcodeResult2Log) Custom.BarcodeResult(2);
                        if (HasOperatorLoadTime) Custom.HDD_CheckAndLogOperatorLoadTimeTaken();
                        
                        if (HasTorqueDriver) TorqueDriverConnectionCheck();
                        if (HasVTCBarcodeInputLog) Custom.VTCInputBarcode();
                        if (HasVTCRejectReason) Custom.VTCRejectReason();
                        if (HasVTCRackBarcodeLog) Custom.VTCRackBarcodeLog();

                        if (HasVisionResultLog) VisionResult();
                        if (HasLotSummary) Custom.LotSummaryCheck();
                        if (HasTestCSV) Custom.TestCSVCheck();
                        if (HasTesterStnUnitTracker) Custom.TesterStnUnitTrackerCheck();
                        if (HasLaserStnUnitTracker) Custom.LaserStnUnitTrackerCheck();
                        if (HasUnldStnUnitTracker) Custom.UnldStnUnitTrackerCheck();
                        if (HasTnRStnUnitTracker) Custom.TnRStnUnitTrackerCheck();
                    }
                    catch (Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void VisionResult()
        {
            try
            {
                const string Tag_VisionResult = "HMI_PLCTrigger_Record_VisRslt";
                if (OPC.Read<bool>(Tag_VisionResult))
                {
                    string LogsPath = Path.Combine(FileLogger.DefaultLocation_Time, "VisionResult");
                    if (!Directory.Exists(LogsPath)) { Directory.CreateDirectory(LogsPath); }
                    string _Path = Path.Combine(LogsPath, $"VisionResult_{DateTime.Today.ToString("yyyyMMdd")}.txt");
                    bool HasFile = File.Exists(_Path);
                    using (FileStream stream = new FileStream(_Path,
                       HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using (var writer = new StreamWriter(stream))
                        {
                            using (var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                            {
                                if (!HasFile)
                                {
                                    //csv.WriteField("Legend: " +
                                    //    "1 = Pass; " +
                                    //    "2 = Hinge not fully locked; " +
                                    //    "3 = Ram Missing; " +
                                    //    "4 = Ram Should Not Appear; " +
                                    //    "5 = MES Feedback Slot Empty; " +
                                    //    "9 = Unknown result from vision ");
                                    //csv.NextRecord();
                                    //csv.NextRecord();
                                    //csv.WriteField(string.Empty);
                                    //csv.WriteField(string.Empty);
                                    //csv.WriteField(string.Empty);
                                    //for (int n1 = 1; n1 <= 4; n1++)
                                    //{
                                    //    csv.WriteField($"G{n1}");
                                    //    for (int n = 1; n < 8; n++)
                                    //        csv.WriteField(string.Empty);
                                    //}
                                    //csv.NextRecord();
                                    csv.WriteField("DateTime");
                                    csv.WriteField("Asset Tag");
                                    csv.WriteField("Zone Num");
                                    for (int n = 1; n <= 4; n++)
                                        for (int ns = 1; ns <= 8; ns++)
                                            csv.WriteField($"G{n} Slot{ns}");
                                    csv.NextRecord();
                                }

                                csv.WriteField(DateTime.Now);

                                csv.WriteField(OPC.Read<string>("HMI_PLCTrigger_Record_VisRslt_AssetTag"));
                                csv.WriteField(OPC.Read<int>("HMI_PLCTrigger_Record_VisRslt_ZoneNum"));
                                for (int ng = 1; ng <= 4; ng++)
                                    foreach (int result in OPC.Read<short[]>($"HMI_Record_VisionRslt_G{ng}[0]", typeof(int), 8))
                                        csv.WriteField(result);

                                csv.NextRecord();
                            }
                        }
                    }
                    OPC.Write(Tag_VisionResult, false);
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "VisionResult");
            }
        }

        private void LogRejectReason()
        {
            try
            {
                int PartCount = 8;
                string LogsPath = Path.Combine(FileLogger.DefaultLocation_Time, "RejectReason");
                if (!Directory.Exists(LogsPath)) { Directory.CreateDirectory(LogsPath); }
                string _Path = Path.Combine(LogsPath, $"RejectReason_{DateTime.Today.ToString("yyyyMMdd")}.txt");
                bool HasFile = File.Exists(_Path);
                using (FileStream stream = new FileStream(_Path,
                   HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                {
                    using (var writer = new StreamWriter(stream))
                    {
                        using (var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                        {
                            if ((Classes.GlobalFunctions.StationType == StationType.VISION &&
                                Classes.GlobalFunctions.ProjectType == ProjectType.ARCADIA) ||
                                (GlobalFunctions.ProjectType == ProjectType.TLA &&
                                (GlobalFunctions.StationType == StationType.ARCADIA_Main ||
                                MachineName.ToUpper().Contains("FINAL") ||
                                MachineName.ToUpper().Contains("CENTRAL"))))
                            {
                                if (!HasFile)
                                {
                                    //csv.WriteField("Station ID");
                                    csv.WriteField("Updated On");
                                    csv.WriteField("Type");
                                    csv.WriteField("Reason");
                                    csv.WriteField("AssetTag");
                                    
                                    if (GlobalFunctions.ProjectType == ProjectType.TLA && MachineName.ToUpper().Contains("FINAL"))
                                    {
                                        csv.WriteField("Barcode");
                                    }

                                    csv.WriteField("Year");
                                    csv.WriteField("Month");
                                    csv.WriteField("Day");
                                    csv.WriteField("Hrs");
                                    csv.WriteField("Min");
                                    csv.WriteField("Sec");
                                    csv.NextRecord();
                                }

                                string Tag_IsZoneFail = string.Empty;
                                string Tag_Zone_AssetTag = string.Empty;
                                string Tag_RejectReason = string.Empty;

                                if (GlobalFunctions.ProjectType == ProjectType.ARCADIA)
                                {
                                    Tag_IsZoneFail = "OutputPnP_HMI_PLCTrigger_Record_Z1_Fail";
                                    Tag_Zone_AssetTag = "Zone_LastAssetTag_OutputPnP";
                                    Tag_RejectReason = "str_OutputPnP_Zone_RejectReason_Record";

                                    if (MachineName.ToUpper().Contains("CENTRAL"))
                                    {
                                        Tag_IsZoneFail = "HMI_CenVis_PLCTrigger_Record_Fail";
                                        Tag_Zone_AssetTag = "CenVis_Zone_AssetTag";
                                        Tag_RejectReason = "_str_CenVis_Zone_RejectReason";
                                    }
                                }
                                else if (GlobalFunctions.ProjectType == ProjectType.TLA)
                                {
                                    if (GlobalFunctions.StationType == StationType.ARCADIA_Main)
                                    {
                                        Tag_IsZoneFail = "HMI_PLCTrigger_Record_Labelling_Fail";
                                        Tag_Zone_AssetTag = "Zone_LastAssetTag_Labelling";
                                        Tag_RejectReason = "HMI_PLCTrigger_Record_Label_Fail_Reason";
                                    }
                                    else if (MachineName.ToUpper().Contains("FINAL"))
                                    {
                                        Tag_IsZoneFail = "HMI_PLCTrigger_Record_OutPnP_Fail";
                                        Tag_Zone_AssetTag = "Zone_LastAssetTag_OutPnP";
                                        Tag_RejectReason = "HMI_PLCTrigger_Record_OutPnP_Fail_Reason";
                                    }
                                    else if (MachineName.ToUpper().Contains("CENTRAL"))
                                    {
                                        Tag_IsZoneFail = "HMI_PLCTrigger_Record_CenVis_Fail";
                                        Tag_Zone_AssetTag = "Zone_LastAssetTag_CenVis";
                                        Tag_RejectReason = "HMI_PLCTrigger_Record_CenVis_Fail_Reason";
                                    }
                                }

                                string strRejectReason;
                                
                                if (OPC.Read<bool>(Tag_IsZoneFail))
                                {
                                    string rejectConcatString = OPC.Read<string>(Tag_RejectReason);
                                    Utilities.FileLogger.logAnything("rejectConcatString", rejectConcatString);
                                    //if (rejectConcatString.Contains(';'))
                                    {
                                        string[] concatStringSplit = rejectConcatString.Split(';');
                                        for (int i = 0; i < concatStringSplit.Length; i++)
                                        {
                                            if (string.IsNullOrEmpty(concatStringSplit[i]))
                                                break;

                                            int RejectReasonCode = Convert.ToInt32(concatStringSplit[i]);
                                            if (RejectReasonCode == 0)
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                if (!Dic_Reject_Index_Reason.TryGetValue(RejectReasonCode, out strRejectReason))
                                                    strRejectReason = RejectReasonCode.ToString();

                                                csv.WriteField(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff"));
                                                csv.WriteField("Zone");
                                                csv.WriteField(strRejectReason);
                                                csv.WriteField(OPC.Read<string>(Tag_Zone_AssetTag));

                                                if (GlobalFunctions.ProjectType == ProjectType.TLA && MachineName.ToUpper().Contains("FINAL"))
                                                {
                                                    csv.WriteField(OPC.Read<string>("OutputPnP_BCD_Content"));
                                                }

                                                csv.WriteField(DateTime.Now.Year);
                                                csv.WriteField(DateTime.Now.Month);
                                                csv.WriteField(DateTime.Now.Day);
                                                csv.WriteField(DateTime.Now.Hour);
                                                csv.WriteField(DateTime.Now.Minute);
                                                csv.WriteField(DateTime.Now.Second);
                                                csv.NextRecord();
                                            }
                                        }
                                        OPC.Write(Tag_IsZoneFail, false);
                                    }
                                }
                            }
                            else
                            {
                                if (!HasFile)
                                {
                                    //csv.WriteField("Station ID");
                                    csv.WriteField("Updated On");
                                    csv.WriteField("Type");
                                    csv.WriteField("Reason");
                                    if (HasTorqueDriver)
                                    {
                                        csv.WriteField("Peak Torque Value");
                                        csv.WriteField("Total Angle Value");
                                    }
                                    csv.WriteField("AssetTag");
                                    for (int n = 1; n <= PartCount; n++)
                                        csv.WriteField($"Part{n} SN");
                                    csv.WriteField("Year");
                                    csv.WriteField("Month");
                                    csv.WriteField("Day");
                                    csv.WriteField("Hrs");
                                    csv.WriteField("Min");
                                    csv.WriteField("Sec");
                                    csv.NextRecord();
                                }

                                string Tag_IsPartReject = "HMI_PLCTrigger_Record_Part{0}Reject";
                                string Tag_Part_SN = "HMI_PLCTrigger_Record_Part{0}_SN";
                                string Tag_Part_FailReason = "HMI_PLCTrigger_Record_Part{0}_Fail_Reason";
                                string Tag_PeakTorqueValue = "Torque_DriverData.Torque_Value";
                                string Tag_TotalAngleValue = "Torque_DriverData.Angle_Value";
                                string Tag_ScrewTorqueValue = "LM_Data.strScrewTorqueValue";

                                for (int n = 1; n <= 4; n++)
                                {
                                    string strRejectReason;

                                    if (OPC.Read<bool>(string.Format(Tag_IsPartReject, n)))
                                    {
                                        if (MachineName.ToUpper().Contains("HEATSINK"))
                                        {
                                            string torqueConcatString = OPC.Read<string>(Tag_ScrewTorqueValue);
                                            Utilities.FileLogger.logAnything("Tag_ScrewTorqueValue", torqueConcatString);
                                            if (torqueConcatString.Contains(';'))
                                            {
                                                string[] concatStringSplit = torqueConcatString.Split(';');
                                                for (int i = 0; i < concatStringSplit.Length; i = i + 3)
                                                {
                                                    //0,3,6,9
                                                    int RejectReasonCode = Convert.ToInt32(concatStringSplit[i]);
                                                    if (RejectReasonCode == 0)
                                                    {
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        if (!Dic_Reject_Index_Reason.TryGetValue(RejectReasonCode, out strRejectReason))
                                                            strRejectReason = RejectReasonCode.ToString();

                                                        csv.WriteField(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff"));
                                                        csv.WriteField("Part");
                                                        csv.WriteField(strRejectReason);
                                                        csv.WriteField(concatStringSplit[i + 1]);
                                                        csv.WriteField(concatStringSplit[i + 2]);
                                                        csv.WriteField("N/A");
                                                        csv.WriteField(OPC.Read<string>(string.Format(Tag_Part_SN, n)));
                                                        for (int n2 = 1; n2 < PartCount; n2++)
                                                            csv.WriteField("N/A");
                                                        csv.WriteField(DateTime.Now.Year);
                                                        csv.WriteField(DateTime.Now.Month);
                                                        csv.WriteField(DateTime.Now.Day);
                                                        csv.WriteField(DateTime.Now.Hour);
                                                        csv.WriteField(DateTime.Now.Minute);
                                                        csv.WriteField(DateTime.Now.Second);
                                                        csv.NextRecord();
                                                    }
                                                }
                                                OPC.Write(string.Format(Tag_IsPartReject, n), false);
                                            }
                                        }
                                        else
                                        {
                                            int RejectReasonCode = OPC.Read<int>(string.Format(Tag_Part_FailReason, n));
                                            if (!Dic_Reject_Index_Reason.TryGetValue(RejectReasonCode, out strRejectReason))
                                                strRejectReason = RejectReasonCode.ToString();

                                            csv.WriteField(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff"));
                                            csv.WriteField("Part");
                                            csv.WriteField(strRejectReason);
                                            if (HasTorqueDriver)
                                            {
                                                // Requested by YK. 1 Dec 2020
                                                if (strRejectReason.ToUpper().Contains("SCREW")
                                                    && strRejectReason.ToUpper().Contains("TIGHTEN")
                                                    && strRejectReason.ToUpper().Contains("FAIL"))
                                                {
                                                    csv.WriteField(OPC.Read<string>(Tag_PeakTorqueValue));
                                                    csv.WriteField(OPC.Read<string>(Tag_TotalAngleValue));
                                                }
                                                else
                                                {
                                                    csv.WriteField("N/A");
                                                    csv.WriteField("N/A");
                                                }
                                            }
                                            csv.WriteField("N/A");
                                            csv.WriteField(OPC.Read<string>(string.Format(Tag_Part_SN, n)));
                                            for (int n2 = 1; n2 < PartCount; n2++)
                                                csv.WriteField("N/A");
                                            csv.WriteField(DateTime.Now.Year);
                                            csv.WriteField(DateTime.Now.Month);
                                            csv.WriteField(DateTime.Now.Day);
                                            csv.WriteField(DateTime.Now.Hour);
                                            csv.WriteField(DateTime.Now.Minute);
                                            csv.WriteField(DateTime.Now.Second);
                                            csv.NextRecord();

                                            OPC.Write(string.Format(Tag_IsPartReject, n), false);
                                        }
                                    }
                                }

                                string Tag_IsZoneFail = "HMI_PLCTrigger_Record_Z{0}_Fail";
                                string Tag_Zone_AssetTag = "Zone_LastAssetTag_Z{0}";
                                string Tag_Zone_FailReason = "HMI_PLCTrigger_Record_Z{0}_Fail_Reason";
                                string Tag_Zone_Last_SN = "Zone_LastPart{0}_SN_Z{1}";

                                for (int z = 1; z <= 2; z++)
                                {
                                    string strRejectReason;

                                    if (OPC.Read<bool>(string.Format(Tag_IsZoneFail, z)))
                                    {
                                        if (MachineName.ToUpper().Contains("HEATSINK"))
                                        {
                                            string torqueConcatString = OPC.Read<string>(Tag_ScrewTorqueValue);
                                            Utilities.FileLogger.logAnything("Tag_ScrewTorqueValue", torqueConcatString);
                                            if (torqueConcatString.Contains(';'))
                                            {
                                                string[] concatStringSplit = torqueConcatString.Split(';');
                                                for (int i = 0; i < concatStringSplit.Length - 1; i = i + 3)
                                                {
                                                    //0,3,6,9
                                                    int RejectReasonCode = Convert.ToInt32(concatStringSplit[i]);
                                                    if (RejectReasonCode == 0)
                                                    {
                                                        continue;
                                                    }
                                                    else
                                                    {
                                                        if (!Dic_Reject_Index_Reason.TryGetValue(RejectReasonCode, out strRejectReason))
                                                            strRejectReason = RejectReasonCode.ToString();

                                                        csv.WriteField(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff"));
                                                        csv.WriteField("Zone");
                                                        csv.WriteField(strRejectReason);
                                                        csv.WriteField(concatStringSplit[i + 1]);
                                                        csv.WriteField(concatStringSplit[i + 2]);
                                                        csv.WriteField(OPC.Read<string>(string.Format(Tag_Zone_AssetTag, z)));
                                                        for (int n3 = 1; n3 <= PartCount; n3++)
                                                            csv.WriteField(OPC.Read<string>(string.Format(Tag_Zone_Last_SN, n3, z)));
                                                        csv.WriteField(DateTime.Now.Year);
                                                        csv.WriteField(DateTime.Now.Month);
                                                        csv.WriteField(DateTime.Now.Day);
                                                        csv.WriteField(DateTime.Now.Hour);
                                                        csv.WriteField(DateTime.Now.Minute);
                                                        csv.WriteField(DateTime.Now.Second);
                                                        csv.NextRecord();
                                                    }
                                                }
                                                OPC.Write(string.Format(Tag_IsZoneFail, z), false);
                                            }
                                        }
                                        else
                                        {
                                            int RejectReasonCode = OPC.Read<int>(string.Format(Tag_Zone_FailReason, z));
                                            if (!Dic_Reject_Index_Reason.TryGetValue(RejectReasonCode, out strRejectReason))
                                                strRejectReason = RejectReasonCode.ToString();

                                            csv.WriteField(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff"));
                                            csv.WriteField("Zone");
                                            csv.WriteField(strRejectReason);
                                            if (HasTorqueDriver)
                                            {
                                                if (strRejectReason.ToUpper().Contains("SCREW")
                                                    && strRejectReason.ToUpper().Contains("TIGHTEN")
                                                    && strRejectReason.ToUpper().Contains("FAIL"))
                                                {
                                                    csv.WriteField(OPC.Read<string>(Tag_PeakTorqueValue));
                                                    csv.WriteField(OPC.Read<string>(Tag_TotalAngleValue));
                                                }
                                                else
                                                {
                                                    csv.WriteField("N/A");
                                                    csv.WriteField("N/A");
                                                }
                                            }
                                            csv.WriteField(OPC.Read<string>(string.Format(Tag_Zone_AssetTag, z)));
                                            for (int n3 = 1; n3 <= PartCount; n3++)
                                                csv.WriteField(OPC.Read<string>(string.Format(Tag_Zone_Last_SN, n3, z)));
                                            csv.WriteField(DateTime.Now.Year);
                                            csv.WriteField(DateTime.Now.Month);
                                            csv.WriteField(DateTime.Now.Day);
                                            csv.WriteField(DateTime.Now.Hour);
                                            csv.WriteField(DateTime.Now.Minute);
                                            csv.WriteField(DateTime.Now.Second);
                                            csv.NextRecord();

                                            Utilities.FileLogger.logAnything(string.Format(Tag_IsZoneFail, z), $"Write Value : {OPC.Write(string.Format(Tag_IsZoneFail, z), false)}");
                                            //OPC.Write(string.Format(Tag_IsZoneFail, z), false);
                                            Utilities.FileLogger.logAnything(string.Format(Tag_IsZoneFail, z), $"Read Value : {OPC.Read<string>(string.Format(Tag_IsZoneFail, z))}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //Utilities.FileLogger.logError(ex.ToString(), "RejectReasonLog");
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void TorqueDriverConnectionCheck()
        {
            try
            {
                Utilities.FileLogger.logAnything("TorqueDriverConnectionCheck", $"Attempt: {connectionAttempt}");

                if (connectionAttempt <= 5)
                {
                    if (!string.IsNullOrEmpty(TorqueDriverStartTag))
                    {
                        if (OPC.Read<bool>(TorqueDriverStartTag))
                        {
                            connectionAttempt++;
                        }
                    }
                }
                else
                {
                    Utilities.FileLogger.logAnything("TorqueDriverConnectionCheck", $"ConnectionError: True");
                    OPC.Write("HMI_FrmHMI_TorqueDrive_TCP_Disconnected", true);
                    Thread.Sleep(200);
                    OPC.Write("HMI_FrmHMI_TorqueDrive_TCP_Disconnected", false);
                    connectionAttempt = 0;
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError("TorqueDriverConnectionCheck", ex.Message);
            }
        }

        int PreHrs = -1;
        public double[] UPHWidth = new double[4];
        string ErrMsg;
        void Main_GetStationUPH()
        {
            try
            {
                int Now = DateTime.Now.Hour;
                string str_LineofUPH = string.Empty;
                str_LineofUPH = string.Join(",", OPC.Read<int[]>(StationUPHTag, typeof(Int32), 24));

                if (string.IsNullOrWhiteSpace(str_LineofUPH))
                    return;

                Lst_UPH = str_LineofUPH.Split(',');

                UPH = Convert.ToInt32(Lst_UPH[Now]);

                if (Now != PreHrs && DateTime.Now.Minute > 1)
                {
                    DBCall.Log_UPH(DateTime.Now.AddHours(-1).Hour, StationID, Lst_UPH[DateTime.Now.AddHours(-1).Hour], ref ErrMsg);//PastUPH[0].ToString(), ref ErrMsg);
                    PreHrs = Now;
                }
            }
            catch (Exception ex)
            { Utilities.FileLogger.logError(ex.ToString(), "Main_GetStationUPH"); }
        }


        int intNextTimeUpdate = 5;
        //DateTime dateCurrentTime;
        DateTime dtNextTimeUpdate;
        int intError = 0;
        private void PLC_SET_TIME()
        {
            try
            {
                if (DateTime.Now > dtNextTimeUpdate)
                {
                    ErrMsg = "";
                    dtNextTimeUpdate = DateTime.Now;

                    intError = 0;
                    if (!OPC.Write(SyncYearTag, dtNextTimeUpdate.Year.ToString("00"), typeof(string)))
                        intError++;
                    if (!OPC.Write(SyncMonthTag, dtNextTimeUpdate.Month.ToString("00"), typeof(string)))
                        intError++;
                    if (!OPC.Write(SyncDayTag, dtNextTimeUpdate.Day.ToString("00"), typeof(string)))
                        intError++;
                    if (!OPC.Write(SyncHourTag, dtNextTimeUpdate.Hour.ToString("00"), typeof(string)))
                        intError++;
                    if (!OPC.Write(SyncMinTag, dtNextTimeUpdate.Minute.ToString("00"), typeof(string)))
                        intError++;
                    if (!OPC.Write(SyncSecTag, dtNextTimeUpdate.Second.ToString("00"), typeof(string)))
                        intError++;
                    if (!OPC.Write(SyncNanoSecTag, dtNextTimeUpdate.Millisecond.ToString("00"), typeof(string)))
                        intError++;

                    if (intError == 0)
                        OPC.Write(SyncTimeTag, true);
                    
                    dtNextTimeUpdate = DateTime.Now.AddSeconds(intNextTimeUpdate);
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.ToString(), "Update Datetime Error");
            }
        }
#endregion

        ~Main()
        {
            Dispose();
        }
    }
}
