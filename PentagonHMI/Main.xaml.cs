using GalaSoft.MvvmLight.Messaging;
using Logix;
using NLog;
using PentagonHMI.Classes;
using PentagonHMI.LogicClasses;
using PentagonHMI.Views.Main;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Utilities;
using Shp = System.Windows.Shapes;

namespace PentagonHMI
{
    public enum ProjectType
    {
        DEXCOM,
        ARCADIA,
        DIMM,
        HDD,
        LIFTER,
        TLA,
        VTC,
    }

    public enum StationType
    {
        SYSTEM_01,
        SYSTEM_02,
        ARCADIA_Main,
        VISION,
        NA,
        CSSD
    }

    public partial class Main : Window, IDisposable
    {
        private List<Image> LstImg_PageDisIcon;
        private const string Yellow = "YellowBorderBackground";
        private const string Green = "GreenBorderBackground";
        private const string Blue = "BlueBorderBackground";
        private const string Red = "RedBorderBackground";

        public Tcpip_ArcadiaTorqueDriver TorqueDriver;
        public string alarmLst = string.Empty;
        public string warningLst = string.Empty;
        public string prevalarmLst = string.Empty;
        public string prevwarningLst = string.Empty;
        public int WarningStartsAt = 0;
        private bool NeedTorqueDriver = false;
        private bool HasPrinter = false;
        public int LifterZone = 0;
        public string LifterPosition = "";
        private string NonScheduledOEETagName = "OEE_Tags.bool_NonScheduled";
        private int logoutCount = 0;

        public List<int> AlarmIncluded = new List<int>();
        public List<int> WarningIncluded = new List<int>();
        #region 20230925 added for AlarmDuration log (refer to Island 0)
        public DataTable CurAlarmDt = new DataTable { Columns = { "msgDatetime", "msgErrorCode", "msgError", "msgAction", "msgStation", "msgModule", "msgView", "msgType" } };
        public DataTable PreAlarmDt = new DataTable();
        #endregion

        private string[] ErrorCode;
        private string[] WarningCode;
        public static DataTable AlertWarningDt = new DataTable { Columns = { "msgDatetime", "msgErrorCode", "msgError", "msgAction", "msgModule", "msgView", "msgType" } };
        public DataTable dt_AlarmTable;
        private static DataTable dt_PreAlarmTable;

        public Tag EMtag;

        #region 20230928 added for AlarmDuration log (refer to Island 0)
        public static DateTime DTChgTimeMachineStatus;
        public static string PreviousMachineStatus;
        #endregion

        #region Pages

        internal LogicClasses.Main _Main;
        //private ChildControls.ucOEE_Gen2 OEEChild;
        private ChildControls.ucDexcom_OEE OEEShiftView;
        private ChildControls.ucDexcom_OEE OEELotView;
        private ChildControls.ucHome HomeView;
        private ChildControls.ucSetting SettingChild;
        private ChildControls.ucUserAccount UserAcountChild;
        private ChildControls.ucAlertList AlertChild;
        private ChildControls.ucEventLog LogChild;
        private ChildControls.ucMotor MotorChild;
        private DryRunView dryRunView;
        private ucDexcomEngineering DexcomEngineeringView;
        private ChildControls.ucTrayMap TrayMapView;
        private ChildControls.ucStacker StackerView;
        private ChildControls.ucDUT DUTView;
        private ChildControls.ucLotEntry LotView;
        private ChildControls.ucIOLocation IOLocationChild;
        private Arcadia_Modules.ucLifterTab LifterTabChild;
        private VTC_Modules.ucVTC_Main VTCMainPageChild;
        private PopUpView popUpView = null;
        private PrinterService printerService = null;
        private SignInView signInView = null;
        private MainView mainView = null;
        private EngineeringView engineeringView = null;
        private ControlPanelView controlPanelView = null;
        private RejectBinDisplayView rejectBinDisplayView = null;
        private RackConfigurationView rackConfigurationView = null;
        private RecipeConfigurationView recipeConfigurationView = null;
        private DirectorySizeService directorySizeService = null;
        private IOView iOView = null;
        private TLA_MainConveyorView tLA_MainConveyorView = null;

        #endregion Pages

        private UserControl ucChild
        {
            get; set;
        }

        private UserControl ucChildDUT
        {
            get; set;
        }

        //For Auto Logout
        internal struct LASTINPUTINFO
        {
            public uint cbSize;
            public uint dwTime;
        }

        [DllImport("User32.dll")]
        private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

        private uint idleTime = 0;
        private static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private LASTINPUTINFO lastInputInfo = new LASTINPUTINFO();
        private int currentMonth = 0;
        //End of For Auto Logout

        #region Constructor

        public Main()
        {
            InitializeComponent();
            try
            {
                if(GlobalFunctions.IsSystem1)
                {
                    sp_Stacker.Visibility = Visibility.Collapsed;
                }
                else
                {
                    sp_Lot.Visibility = Visibility.Collapsed;
                }
                FileLogger.DefaultLocation_Time = Properties.Settings.Default.LogLocation.ToString() + "Logs_" + DateTime.Now.ToString("yyyy-MMM");
                FileLogger.DefaultLocation = Properties.Settings.Default.LogLocation.ToString();

                Messenger.Default.Register<Tuple<SignInView, string>>(this, signIn);

                LogFileManagement();

                _Main = new LogicClasses.Main();

                //lbl_HMIVer.Content = "HMI " + Assembly.GetExecutingAssembly().GetName().Version;
                //string PLCversion = _Main.OPC.Read<string>("HMI_PLCVersion");
                //string Visversion = _Main.OPC.Read<string>("HMI_VisionVersion");

                //lbl_PLCVersion.Content = string.IsNullOrWhiteSpace(PLCversion)? "V1.0.0.0": PLCversion;
                //lbl_VisionVersion.Content = string.IsNullOrWhiteSpace(Visversion) ? "V1.0.0.0" : Visversion;

                string HMIVersion = _Main.OPC.Read<string>("HMI_str20_HMI_Version");
                string PLCVersion = _Main.OPC.Read<string>("HMI_str20_PLC_Version");
                string Robot1Version = _Main.OPC.Read<string>("HMI_str20_Robot1_Version");
                string Robot2Version = _Main.OPC.Read<string>("HMI_str20_Robot2_Version");

                lbl_HMIVer.Content = "HMI " + HMIVersion;
                lbl_PLCVer.Content = "PLC " + PLCVersion;
          
                if (GlobalFunctions.IsSystem1)
                {
                    string Robot3Version = _Main.OPC.Read<string>("HMI_str20_Robot3_Version");

                    lbl_Robot1Ver.Content = "PCBA Rbt " + Robot1Version;
                    lbl_Robot2Ver.Content = "Battery Rbt " + Robot2Version;
                    lbl_Robot3Ver.Content = "Unload Rbt " + Robot3Version;
                }
                else
                {
                    string VisionVersion = _Main.OPC.Read<string>("HMI_str20_Vision_Version");

                    lbl_Robot1Ver.Content = "Load Rbt " + Robot1Version;
                    lbl_Robot2Ver.Content = "Unload Rbt " + Robot2Version;
                    lbl_Robot3Ver.Content = "Vision " + VisionVersion;
                }

                LstImg_PageDisIcon = new List<Image>
                {
                    Img_AlmDis, Img_IOListDis, Img_IOLocDis, Img_Login, Img_MotorDis, Img_SettingDis,
                    Img_UserDis, Img_ClcDis, Img_DryrunDis, Img_EngineeringDis,
                    RackConfigurationDisableImage, RecipeConfigurationDisableImage
                };

                GlobalFunctions.LoadErrorList();
                //GlobalFunctions.LoadMachineName();

                UpdateConfig();

                if(NeedTorqueDriver)
                {
                    _Main.HasTorqueDriver = true;
                    TorqueDriver = new Tcpip_ArcadiaTorqueDriver(ref _Main);

                    DataTable dtDriver = _Main.SQLer.Exec_DTSelect($"SELECT TagName FROM [{Info.SQL.DatabaseName}].[dbo].[IO] WHERE DisplayName LIKE '%Driver Start'");
                    if(dtDriver.Rows.Count > 0)
                        _Main.TorqueDriverStartTag = dtDriver.Rows[0][0].ToString();

                    _Main.Tcpip_ArcadiaTorqueDriver = TorqueDriver;
                }

                PreLoadPages();

                //v1.0.32.3 - critical change: always change penta user access to admin
                _Main.SQLer.Exec_NonQuery("UPDATE [Users] SET [Level] = 'admin' WHERE [UserName] = 'penta';");
                //_Main.SQLer.Exec_NonQuery("DELETE FROM [Users] WHERE [Level] = 'admin' OR [UserName] = 'penta';"); //<- change to delete according to the request (Na) 15/7/2024

                EMtag = new Tag(_Main.TN.Dic_Key_Address[TagName.Key.EngineeringMode]);

                dgAlertWarning.ItemsSource = AlertWarningDt.DefaultView;

                _Main.OnAlwaysUpdate += _Main_OnAlwaysUpdate;
                this.Closed += new EventHandler(Main_Closed);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error Creating Page: " + ex.Message);
            }
        }

        private void _Main_OnAlwaysUpdate()
        {
            try
            {
                //if (ProjectType.LIFTER != GlobalFunctions.ProjectType)
                //{
                if(_Main.HasStationStatusCheck)
                    Update_Station_Status();
                else
                    Dispatcher.Invoke(() => Status_Border.Visibility = Visibility.Collapsed);
                if(_Main.HasBreakTimeCheck)
                    BreakTimeOffDay_Check();
                if(_Main.HasLogManagement)
                    LogFileManagement();
                if(_Main.HasErrorCheck)
                    CheckErrorList();
                Dispatcher.Invoke(CheckPickFail);
                Dispatcher.Invoke(() => VersionUpdate());

                //}
                const string Tag_LockScreen_str = "Lot_Info.HMI_LockScreen_Info";
                try
                {
                    //Lock Screen
                    if(!string.IsNullOrEmpty(_Main.OPC.Read<string>(Tag_LockScreen_str)))
                    {
                        Dispatcher.Invoke(() =>
                        {
                            lblScreeLock.Content = _Main.OPC.Read<string>(Tag_LockScreen_str);
                            ChildContainer.Visibility = Visibility.Collapsed;
                            //ChildContainer.IsEnabled = false;
                            vbScreenLock.Visibility = Visibility.Visible;
                            vbScreenLock.BringIntoView();
                        });
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            ChildContainer.Visibility = Visibility.Visible;
                            ChildContainer.IsEnabled = true;
                            vbScreenLock.Visibility = Visibility.Collapsed;
                        });
                    }
                }
                catch(Exception e)
                {
                    FileLogger.logError(e.Message, e.StackTrace);
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private PickFailPrompt pickFailPrompt;
        private SelectedTray selectedTray;
        //private Dictionary<SelectedTray, PickFailPrompt> pickFailPromptDict = new Dictionary<SelectedTray, PickFailPrompt>();

        private void CheckPickFail()
        {
            bool allFalse = true;
            foreach(var s in GlobalFunctions.IsSystem1 ? new List<string> { Tags.MainPage.PickFailPromptWindowPCBA.Name, /*Tags.MainPage.PickFailPromptWindowBattery.Name,*/ } : new List<string> { Tags.MainPage.PickFailPromptWindowLShuttle.Name, Tags.MainPage.PickFailPromptWindowRShuttle.Name, Tags.MainPage.PickFailPRomptWindowUnloadShuttle.Name })
            {
                bool readedState = _Main.OPC.Read<bool>(s);
                if(readedState)
                {
                    allFalse = false;

                    if(pickFailPrompt == null)
                    {
                        if(s == Tags.MainPage.PickFailPromptWindowPCBA.Name)
                        {
                            selectedTray = SelectedTray.S1PCBA;
                        }
                        else if(s == Tags.MainPage.PickFailPromptWindowBattery.Name)
                        {
                            selectedTray = SelectedTray.S1BATTERY;
                        }
                        else if(s == Tags.MainPage.PickFailPromptWindowLShuttle.Name)
                        {
                            selectedTray = SelectedTray.S2LEFT;
                        }
                        else if(s == Tags.MainPage.PickFailPromptWindowRShuttle.Name)
                        {
                            selectedTray = SelectedTray.S2RIGHT;
                        }
                        else if(s == Tags.MainPage.PickFailPRomptWindowUnloadShuttle.Name)
                        {
                            selectedTray = SelectedTray.S2OUTPUT;
                        }

                        _Main.TrayMapPageOn = true;
                        //if(!pickFailPromptDict.ContainsKey(selectedTray))
                        //{
                        //    pickFailPromptDict[selectedTray] = new PickFailPrompt(_Main, selectedTray);
                        //}
                        //pickFailPrompt = pickFailPromptDict[selectedTray];
                        pickFailPrompt = new PickFailPrompt(_Main, selectedTray,this);

                        pickFailPrompt.Show();
                        MainGrid.IsEnabled = false;
                        //PickFailCanva.Visibility = Visibility.Visible;
                        //PickFailContentControl.Content = pickFailPrompt;
                        break;
                    }
                }
            }
            if(allFalse && pickFailPrompt != null)
            {
                pickFailPrompt.Close();
                //ClosePickFailPrompt();
                //PickFailCanva.Visibility = Visibility.Collapsed;
                //PickFailContentControl.Content = null;
            }
        }

        public void ClosePickFailPrompt()
        {
            _Main.TrayMapPageOn = false;
            //pickFailPrompt.Close();
            pickFailPrompt = null;
            MainGrid.IsEnabled = true;
        }

        //Temp!!!
        private void VersionUpdate()
        {
            //string PLCversion = _Main.OPC.Read<string>("HMI_PLCVersion");
            //string Visversion = _Main.OPC.Read<string>("HMI_VisionVersion");

            //lbl_PLCVersion.Content = string.IsNullOrWhiteSpace(PLCversion) ? "V1.0.0.0" : PLCversion;
            //lbl_VisionVersion.Content = string.IsNullOrWhiteSpace(Visversion) ? "V1.0.0.0" : Visversion;

            //lbl_PLCVersion.Content = _Main.OPC.Read<string>("HMI_PLCVersion");
            //lbl_VisionVersion.Content = _Main.OPC.Read<string>("HMI_VisionVersion");
        }

        private void UpdateConfig()
        {
            try
            {
                //lblHMIVersion.Content = $"HMI : v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}";
                //lblVersion.Content = $"PLC : v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}\tVision : v{Assembly.GetExecutingAssembly().GetName().Version.ToString()}";

                Func<string, object> GetItem = (x) => _Main.dt_Config.Select().Where(y => y["Item"].ToString().ToUpper() == x.ToUpper()).First()["Info"];

                _Main.MachineName = GetItem("MachineName").ToString();

                Dispatcher.Invoke(new Action(() => lbl_MachineName.Content = _Main.MachineName == "PCIE" ? "PCI" : _Main.MachineName));

                _Main.VisionIP = GetItem("VisionIP").ToString();

                //_Main.CheckPath = GetItem("CheckPath").ToString();

                WarningStartsAt = Convert.ToInt32(GetItem("WarningStartsAt").ToString());
                HasPrinter = Convert.ToBoolean(GetItem("HasPrinter").ToString());
                // Not found from Db
                //_Main.HasRackStatus = Convert.ToBoolean(GetItem("HasRackStatus").ToString());

                if(ProjectType.LIFTER == GlobalFunctions.ProjectType ||
                    (ProjectType.ARCADIA == GlobalFunctions.ProjectType && StationType.VISION == GlobalFunctions.StationType) ||
                    _Main.MachineName.Contains("Power") || _Main.MachineName.Contains("Temperature"))
                {
                    string[] alarmSplit = GetItem("AlarmIncluded").ToString().Split(';');
                    foreach(string x in alarmSplit)
                    {
                        // if its a range
                        if(x.Replace(" ", "").Contains('-'))
                        {
                            int from = Convert.ToInt32(x.Split('-')[0]);
                            int to = Convert.ToInt32(x.Split('-')[1]);

                            for(int i = from; i <= to; i++)
                            {
                                AlarmIncluded.Add(i);
                            }
                        }
                        else
                        {
                            AlarmIncluded.Add(Convert.ToInt32(x));
                        }
                    }

                    string[] warningSplit = GetItem("WarningIncluded").ToString().Split(';');
                    foreach(string x in warningSplit)
                    {
                        // if its a range
                        if(x.Replace(" ", "").Contains('-'))
                        {
                            int from = Convert.ToInt32(x.Split('-')[0]);
                            int to = Convert.ToInt32(x.Split('-')[1]);

                            for(int i = from; i <= to; i++)
                            {
                                WarningIncluded.Add(i);
                            }
                        }
                        else
                        {
                            WarningIncluded.Add(Convert.ToInt32(x));
                        }
                    }
                }

                if(ProjectType.ARCADIA == GlobalFunctions.ProjectType ||
                    GlobalFunctions.ProjectType == ProjectType.TLA)
                {
                    NeedTorqueDriver = Convert.ToBoolean(GetItem("TorqueDriver").ToString());
                }

                if(ProjectType.LIFTER == GlobalFunctions.ProjectType)
                {
                    LifterZone = Convert.ToInt32(GetItem("LifterZone").ToString());
                    LifterPosition = GetItem("LifterPosition").ToString();
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void PreLoadPages()
        {
            //General
            bool HasIO = false, HasUserAccount = false, HasDryrun = false, HasOEE = false, HasSOEE = false, HasLOEE = false, HasSetting = false,
                 HasLog = false, HasMotor = false, HasEng = false, HasIOLoc = false, HasLot = false;
            //Essential
            bool HasPopUp = true, HasSignin = true, HasMachineInfo = true, HasControlPanel = true, HasPLCEvent = true;
            //Custom
            bool HasZoneLeftVisionRightMainPage = false, HasArcadiaConveyorMain = false, HasZoneLeftRackRightMainPage = false,
                 HasRejectBinDisplay = false, HasRackConfig = false, HasRecipeConfig = false, HasLifterTab = false, HasTrayMap = false, HasStacker = false,
                 HasDUT = false;

            int rejectBinCount = 8;
            string rejectBinDisplayStatusTagKey = "DIMM_Data_Tracking_Barcd_RejBin";

            switch(GlobalFunctions.ProjectType)
            {
                case ProjectType.DEXCOM:
                    HasIO = HasUserAccount = HasDryrun = HasSOEE = HasLOEE = HasSetting =
                    HasLog = HasMotor = HasEng = HasTrayMap = HasStacker = HasDUT = HasLot = HasPopUp = HasEng = true;
                    HomeView = new ChildControls.ucHome(_Main);
                    break;

                default:
                    break;
            }

            if(HasRejectBinDisplay)
                rejectBinDisplayView = new RejectBinDisplayView(ref _Main, rejectBinCount, rejectBinDisplayStatusTagKey);
            RejectBinDisplayStackPanel.Visibility = HasRejectBinDisplay ? Visibility.Visible : Visibility.Collapsed;

            if(HasRackConfig)
                rackConfigurationView = new RackConfigurationView(ref _Main);
            RackConfigurationStackPanel.Visibility = HasRackConfig ? Visibility.Visible : Visibility.Collapsed;

            if(HasRecipeConfig)
                recipeConfigurationView = new RecipeConfigurationView(_Main.OPC);
            RecipeConfigurationStackPanel.Visibility = HasRecipeConfig ? Visibility.Visible : Visibility.Collapsed;

            if(HasUserAccount)
                UserAcountChild = new ChildControls.ucUserAccount(ref _Main);
            SP_User.Visibility = HasUserAccount ? Visibility.Visible : Visibility.Collapsed;

            //if (HasDryrun) DryrunChild = new ChildControls.ucDryrun(ref _Main);
            if(HasDryrun)
                dryRunView = new DryRunView(_Main);
            DryrunStackPanel.Visibility = HasDryrun ? Visibility.Visible : Visibility.Collapsed;

            if(HasSOEE)
                OEEShiftView = new ChildControls.ucDexcom_OEE(ref _Main, ChildControls.ucDexcom_OEE.OEEType.Shift);
            Sp_OEEShift.Visibility = HasSOEE ? Visibility.Visible : Visibility.Collapsed;

            if(HasLOEE)
                OEELotView = new ChildControls.ucDexcom_OEE(ref _Main, ChildControls.ucDexcom_OEE.OEEType.Lot);
            Sp_OEELot.Visibility = HasLOEE ? Visibility.Visible : Visibility.Collapsed;

            if(HasSetting)
                SettingChild = new ChildControls.ucSetting(ref _Main);
            Sp_Setting.Visibility = HasSetting ? Visibility.Visible : Visibility.Collapsed;

            if(HasLog)
                LogChild = new ChildControls.ucEventLog(ref _Main);
            Sp_Log.Visibility = HasLog ? Visibility.Visible : Visibility.Collapsed;

            if(HasMotor)
                MotorChild = new ChildControls.ucMotor(ref _Main);
            Sp_Motor.Visibility = HasMotor ? Visibility.Visible : Visibility.Collapsed;

            //if (HasIO) IOInputChild = new ChildControls.ucIOInput(ref _Main, ref _PreLoadIO);
            if(HasIO)
                iOView = new IOView(ref _Main);
            Sp_IOList.Visibility = HasIO ? Visibility.Visible : Visibility.Collapsed;

            if(HasIOLoc)
                IOLocationChild = new ChildControls.ucIOLocation(ref _Main);
            Sp_IOLoc.Visibility = HasIOLoc ? Visibility.Visible : Visibility.Collapsed;

            if(HasEng)
                DexcomEngineeringView = new ucDexcomEngineering(_Main); //engineeringView = new EngineeringView(ref _Main);
            EngineeringStackPanel.Visibility = HasEng ? Visibility.Visible : Visibility.Collapsed;

            //Home Pages
            if(HasZoneLeftVisionRightMainPage)
                mainView = new MainView(ref _Main);
            if(HasLifterTab)
                LifterTabChild = new Arcadia_Modules.ucLifterTab(ref _Main, LifterZone, LifterPosition);
            if(HasArcadiaConveyorMain)
                InitializeArcadiaMainLine();
            if(HasZoneLeftRackRightMainPage)
                VTCMainPageChild = new VTC_Modules.ucVTC_Main(ref _Main);

            //Custom & Frame
            if(HasPrinter)
                printerService = new PrinterService(ref _Main);
            if(HasPLCEvent)
                _Main.PLCEventLogListener = new TcpIpServer();
            if(HasPopUp)
                popUpView = new PopUpView(ref _Main);

            if(HasSignin)
                signInView = new SignInView(ref _Main);
            SP_Login.Visibility = HasSignin ? Visibility.Visible : Visibility.Collapsed;
            lblCurrUser.Visibility = HasSignin ? Visibility.Visible : Visibility.Collapsed;

            if(HasMachineInfo)
                _Main.machineInformationView = new MachineInformationView(_Main);
            if(HasControlPanel)
                controlPanelView = new ControlPanelView(_Main);
            if(HasTrayMap)
                TrayMapView = new ChildControls.ucTrayMap(_Main);
            if(HasStacker)
                StackerView = new ChildControls.ucStacker(_Main);
            if(HasDUT)
                DUTView = new ChildControls.ucDUT(_Main);
            if(HasLot)
                LotView = new ChildControls.ucLotEntry(_Main);

            //directorySizeService = new DirectorySizeService(_Main);

            AddComponent();

            SwapPage("HOME");
        }

        private void AddComponent()
        {
            try
            {
                TopPanelGrid.Children.Add(_Main.machineInformationView);
                Grid.SetRow(_Main.machineInformationView, 0);
                Grid.SetColumn(_Main.machineInformationView, 1);

                TopPanelGrid.Children.Add(controlPanelView);
                Grid.SetRow(controlPanelView, 0);
                Grid.SetColumn(controlPanelView, 2);
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void AddChild(UserControl _Child)
        {
            this.ChildContainer.Children.Clear();
            this.ChildContainer.Children.Add(_Child);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            uint envTicks = (uint)Environment.TickCount;
            if(GetLastInputInfo(ref lastInputInfo))
            {
                uint lastInputTick = lastInputInfo.dwTime;
                idleTime = envTicks - lastInputTick;
                idleTime /= 1000;
            }
            int value = Int32.Parse(Classes.GlobalFunctions.login_Timeout);
            if(idleTime > value)
            {
                idleTime = 0;
                LogIN_OUT(false);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            ucUsername.Text = "";
            ucPassword.Text = "";
            loginScreen.Visibility = Visibility.Collapsed;
        }

        private bool LocalAccountCheck(string StrName, string strPassword)
        {
            if(StrName == "admin" && strPassword == "#admin123")
                return true;
            return false;
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            if(PreCheck() == true)
            {
                _Main.UserAccessLevel = _Main.SQLer.Exec_Scalar<string>($"SELECT [LEVEL] FROM USERS WHERE USERNAME = '{ucUsername.Text}' AND PASSWORD = '{ucPassword.Text}'");

                if(string.IsNullOrWhiteSpace(_Main.UserAccessLevel))
                    if(LocalAccountCheck(ucUsername.Text, ucPassword.Text))
                        _Main.UserAccessLevel = "Administrator";

                if(!string.IsNullOrWhiteSpace(_Main.UserAccessLevel))
                {
                    lblCurrUser.Content = "Current User :  " + _Main.UserAccessLevel;
                    ucUsername.Text = "";
                    ucPassword.Text = "";
                    loginScreen.Visibility = Visibility.Collapsed;

                    lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
                    lastInputInfo.dwTime = 0;

                    timer.Enabled = true;
                    timer.Tick += new EventHandler(timer_Tick); // Everytime timer ticks, timer_Tick will be called
                    timer.Interval = (1000) * (1);              // Timer will tick every second
                                                                // Enable the timer
                    timer.Start();

                    LogIN_OUT(true);
                }
                else
                    this.ucPassword.ucLabelErrorContent = this.TryFindResource("LOGIN_TEXTBLOCK_INVALID").ToString();
            }
        }

        #endregion Constructor

        #region Event

        public int[] FindAllIndex<T>(T[] array, Predicate<T> match)
        {
            return array.Select((value, index) => match(value) ? index : -1)
                    .Where(index => index != -1).ToArray();
        }

        private void CheckErrorList()
        {
            try
            {
#if !DEBUG
                var EnumBool = _Main.OPC.Read<bool[]>(GlobalFunctions.IsSystem1 ?
                    "System1_MC_Tag.ErrorBit[0]" : "System2_MC_Tag.ErrorCondBit[0]", typeof(bool), 512);
                if(EnumBool != null)
                    alarmLst = string.Join(",", FindAllIndex(EnumBool, x => x == true));

                var EnumWBool = _Main.OPC.Read<bool[]>(GlobalFunctions.IsSystem1 ?
                    "System1_MC_Tag.WarningBit[0]" : "System2_MC_Tag.WarningCondBit[0]", typeof(bool), 512);
                if(EnumWBool != null)
                    warningLst = string.Join(",", FindAllIndex(EnumWBool, x => x == true));
#else
                alarmLst = "1,2";
                warningLst = "1,4";
#endif
                if(prevalarmLst != alarmLst || prevwarningLst != warningLst)
                {
                    _Main_OnError();
                    prevwarningLst = warningLst;
                    prevalarmLst = alarmLst;
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        //LogDuration
        //string LogSingleBit = "HMI_Tags.Triger_RecordErrorDuration[{0}]";
        //var LogBit = OPC.Read<bool[]>("HMI_Tags.Triger_RecordErrorDuration[0]", typeof(bool), 20); //new bool[20]; LogBit[2] = LogBit[3] = LogBit[7] = true;
        //var LogError = OPC.Read<short[]>("HMI_Tags.ErrorDuration_Log_ErrorNumbers[0]", typeof(int), 20);
        //var LogSec = OPC.Read<int[]>("HMI_Tags.ErrorDuration_Sec[0]", typeof(int), 20);
        //foreach (var bit in FindAllIndex(LogBit, x => x == true))
        //{
        //    int Errcode = LogError[bit];
        //    int ErrDurationSec = LogSec[bit];
        //    if (GlobalFunctions.ErrorListDict.ContainsKey(Errcode))
        //    {
        //        LogAlarmDuration(Errcode, ErrDurationSec, GlobalFunctions.ErrorListDict[Errcode].Split(';'));
        //        OPC.Write(string.Format(LogSingleBit, bit), false);
        //    }
        //}

        //string LogSingleBitW = "HMI_Tags.Triger_RecordWarningDuration[{0}]";
        //var LogBitW = OPC.Read<bool[]>("HMI_Tags.Triger_RecordWarningDuration[1]", typeof(bool), 20); //new bool[20]; LogBitW[2] = LogBitW[3] = LogBitW[7] = true;
        //var LogErrorW = OPC.Read<short[]>("HMI_Tags.WarningDuration_Log_WarningNumbers[0]", typeof(int), 20);
        //var LogSecW = OPC.Read<int[]>("HMI_Tags.WarningDuration_Sec[0]", typeof(int), 20);
        //foreach (var bit in FindAllIndex(LogBitW, x => x == true))
        //{
        //    int Errcode = LogErrorW[bit];
        //    int ErrDurationSec = LogSecW[bit];
        //    if (GlobalFunctions.ErrorListDict.ContainsKey(Errcode))
        //    {
        //        LogWarningDuration(Errcode, ErrDurationSec, GlobalFunctions.ErrorListDict[Errcode].Split(';'));
        //        OPC.Write(string.Format(LogSingleBitW, bit), false);
        //    }
        //}

        private void LogWarningDuration(int ErrCode, int Duration, string[] ErrInfo)
        {
            string LogsPath = Path.Combine(FileLogger.DefaultLocation_Time, "AlarmWarningDuration");
            if(!Directory.Exists(LogsPath))
            {
                Directory.CreateDirectory(LogsPath);
            }
            string _Path = Path.Combine(LogsPath, $"WarningDuration_{DateTime.Today.ToString("yyyy-MM-dd")}.txt");
            bool HasFile = File.Exists(_Path);
            using(FileStream stream = new FileStream(_Path,
               HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            {
                using(var writer = new StreamWriter(stream))
                {
                    using(var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                    {
                        if(!HasFile)
                        {
                            csv.WriteField("DateTime");
                            csv.WriteField("ErrCode");
                            csv.WriteField("Module");
                            csv.WriteField("Message");
                            csv.WriteField("Action");
                            csv.WriteField("Duration (hh: mm: ss)");
                            csv.NextRecord();
                        }

                        TimeSpan T = TimeSpan.FromSeconds(Duration);
                        csv.WriteField(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff"));
                        csv.WriteField(ErrCode);
                        csv.WriteField(ErrInfo[0]);
                        csv.WriteField(ErrInfo[1]);
                        csv.WriteField(ErrInfo[2]);
                        csv.WriteField(T.ToString(@"hh\:mm\:ss"));
                        csv.NextRecord();
                    }
                }
            }
        }

        private void LogAlarmDuration(int ErrCode, int Duration, string[] ErrInfo)
        {
            string LogsPath = Path.Combine(FileLogger.DefaultLocation_Time, "AlarmWarningDuration");
            if(!Directory.Exists(LogsPath))
            {
                Directory.CreateDirectory(LogsPath);
            }
            string _Path = System.IO.Path.Combine(LogsPath, $"AlarmDuration_{DateTime.Today.ToString("yyyyMMdd")}.txt");
            bool HasFile = File.Exists(_Path);
            using(FileStream stream = new FileStream(_Path,
               HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
            {
                using(var writer = new StreamWriter(stream))
                {
                    using(var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                    {
                        if(!HasFile)
                        {
                            csv.WriteField("DateTime");
                            csv.WriteField("ErrCode");
                            csv.WriteField("Module");
                            csv.WriteField("Message");
                            csv.WriteField("Action");
                            csv.WriteField("Duration (hh: mm: ss)");
                            csv.NextRecord();
                        }

                        TimeSpan T = TimeSpan.FromSeconds(Duration);
                        csv.WriteField(DateTime.Now);
                        csv.WriteField(ErrCode);
                        csv.WriteField(ErrInfo[0]);
                        csv.WriteField(ErrInfo[1]);
                        csv.WriteField(ErrInfo[2]);
                        csv.WriteField(T.ToString(@"hh\:mm\:ss"));
                        csv.NextRecord();
                    }
                }
            }
        }

        private void Update_Station_Status()
        {
            //string statustag = "Machine_Status.str_MachineStatus";
            string statustag = Tags.MainPage.MachineStatus.Name;
            //if (GlobalFunctions.StationType == StationType.VISION && _Main.MachineName.ToUpper().Contains("FINAL"))
            //    statustag = "OutPNP_Machine_Status.str_MachineStatus";
            //else if (GlobalFunctions.StationType == StationType.VISION && _Main.MachineName.ToUpper().Contains("CENTRAL"))
            //    statustag = "CenVis_Machine_Status.str_MachineStatus";

            string _Content = _Main.OPC.Read<string>(statustag) ?? "ERROR";

            #region 28/9/2023 add AlarmDuration Log (refer Island 0)
            if (PreviousMachineStatus is null)
            {
                PreviousMachineStatus = _Content;
            }
            else
            {
                var cmpResult = string.Equals(PreviousMachineStatus, _Content, StringComparison.OrdinalIgnoreCase);
                if (!cmpResult)
                {
                    TimeSpan duration = DateTime.Now - DTChgTimeMachineStatus;
                    string formattedDuration = $"{duration.Hours:00}:{duration.Minutes:00}:{duration.Seconds:00}";

                    Dispatcher.Invoke(new Action(() => DexcomGlobalFunctions.logDexcomDur($"Machine Status", DTChgTimeMachineStatus.ToString("yyyy-MMM-dd_HH:mm:ss.fff"), DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff"), formattedDuration, $"Machine Status: {PreviousMachineStatus}")));
                    DTChgTimeMachineStatus = DateTime.Now;
                    PreviousMachineStatus = _Content;
                }
            }
            #endregion

            _Main.MachineStatus = _Content;
            string _Color = Yellow;
            switch(_Content.ToUpper())
            {
                case "STOPPED":
                case "STOP":
                    //_Content = "Waiting";
                    goto default;
                case "RUNNING":
                case "RUN":
                    _Color = Green;
                    break;

                case "DRY RUN":
                case "DRY-RUN":
                case "INITIAL":
                case "INITIALIZE":
                case "INITIALISE":
                case "INITIALIZING":
                case "IDLING":
                case "IDLE":
                    _Color = Blue;
                    break;

                case "ERROR":
                    _Color = Red;
                    break;

                case "WARNING":
                case "DISABLED":
                default:
                    _Color = Yellow;
                    break;
            }
            this.Dispatcher.Invoke(new Action(() =>
            {
                this.Status_Border.SetResourceReference(BackgroundProperty, _Color);
                this.txtStatus.Content = _Content;
                this.txtStatus.Foreground = _Color.Equals(Yellow) ? Brushes.Black : _Color.Equals(Red) ? Brushes.Yellow : Brushes.Snow;
            }));

            //this.Status_Border.Dispatcher.Invoke(new Action(() => this.Status_Border.SetResourceReference(BackgroundProperty, _Color)));
            //this.txtStatus.Dispatcher.Invoke(new Action(() => this.txtStatus.Content = _Content));
            //this.txtStatus.Dispatcher.Invoke(new Action(() =>this.txtStatus.Foreground =
            //_Color.Equals(Yellow) ? Brushes.Black :
            //_Color.Equals(Red) ? Brushes.Yellow : Brushes.Snow));
        }

        private void _Main_OnError()
        {
            try
            {
                string errMsg = "";
                int errornum = 0;
                dt_PreAlarmTable = AlertWarningDt.Copy();   //Only copy works, if not will clear all data, Hao(12072018)
                AlertWarningDt.Rows.Clear();

                this.Dispatcher.Invoke(new Action(() => dgAlertWarning.Items.Refresh()));

                //20230925
                PreAlarmDt = CurAlarmDt.Copy();
                CurAlarmDt.Rows.Clear();

                if(alarmLst.Contains(","))
                {
                    ErrorCode = alarmLst.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);  //don't need split as it is in array
                                                                                                            // var ErrorCode1 = ErrorCode.Except(prevErrorCode);
                                                                                                            //ErrorCode.Reverse().Reverse();
                    foreach(var Error in ErrorCode)
                    {
                        errornum = Convert.ToInt32(Error);
                        if(AlarmIncluded.Count > 0)
                        {
                            if(AlarmIncluded.Exists(x => x == errornum))
                            {
                                ErrorMessage(errornum, out errMsg);
                                this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
                                Dispatcher.Invoke(new Action(() => DexcomGlobalFunctions.LogAlarmDuration(CurAlarmDt, PreAlarmDt, errornum.ToString(),errMsg)));
                            }
                        }
                        else
                        {
                            ErrorMessage(errornum, out errMsg);
                            this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
                            Dispatcher.Invoke(new Action(() => DexcomGlobalFunctions.LogAlarmDuration(CurAlarmDt, PreAlarmDt, errornum.ToString(), errMsg)));
                        }
                    }
                }
                else
                {
                    if(alarmLst != "")
                    {
                        string Error = alarmLst;
                        errornum = Convert.ToInt32(Error);

                        if(AlarmIncluded.Count > 0)
                        {
                            if(AlarmIncluded.Exists(x => x == errornum))
                            {
                                ErrorMessage(errornum, out errMsg);
                                this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
                                Dispatcher.Invoke(new Action(() => DexcomGlobalFunctions.LogAlarmDuration(CurAlarmDt, PreAlarmDt, errornum.ToString(), errMsg)));
                            }
                        }
                        else
                        {
                            ErrorMessage(errornum, out errMsg);
                            this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
                            Dispatcher.Invoke(new Action(() => DexcomGlobalFunctions.LogAlarmDuration(CurAlarmDt, PreAlarmDt, errornum.ToString(), errMsg)));
                        }
                    }
                }

                if(warningLst.Contains(","))
                {
                    WarningCode = warningLst.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    //WarningCode.Reverse().Reverse();
                    foreach(string Error in WarningCode)
                    {
                        errornum = Convert.ToInt32(Error) + WarningStartsAt;

                        if(WarningIncluded.Count > 0)
                        {
                            if(WarningIncluded.Exists(x => x == errornum))
                            {
                                ErrorMessage(errornum, out errMsg);
                                this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
                                Dispatcher.Invoke(new Action(() => DexcomGlobalFunctions.LogAlarmDuration(CurAlarmDt, PreAlarmDt, errornum.ToString(), errMsg)));
                            }
                        }
                        else
                        {
                            ErrorMessage(errornum, out errMsg);
                            this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
                            Dispatcher.Invoke(new Action(() => DexcomGlobalFunctions.LogAlarmDuration(CurAlarmDt, PreAlarmDt, errornum.ToString(), errMsg)));
                        }
                    }
                }
                else
                {
                    if(warningLst != "")
                    {
                        string Error = warningLst;
                        errornum = Convert.ToInt32(Error) + WarningStartsAt;

                        if(WarningIncluded.Count > 0)
                        {
                            if(WarningIncluded.Exists(x => x == errornum))
                            {
                                ErrorMessage(errornum, out errMsg);
                                this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
                                Dispatcher.Invoke(new Action(() => DexcomGlobalFunctions.LogAlarmDuration(CurAlarmDt, PreAlarmDt, errornum.ToString(), errMsg)));
                            }
                        }
                        else
                        {
                            ErrorMessage(errornum, out errMsg);
                            this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
                            Dispatcher.Invoke(new Action(() => DexcomGlobalFunctions.LogAlarmDuration(CurAlarmDt, PreAlarmDt, errornum.ToString(), errMsg)));
                        }
                    }
                }

                try
                {
                    foreach (DataRow dr in PreAlarmDt.Rows)
                    {
                        DataRow[] rows = CurAlarmDt.Select("msgErrorCode = '" + dr["msgErrorCode"] + "'");
                        if (rows.Length == 0)
                        {
                            DateTime currentDT = DateTime.Now;
                            TimeSpan duration = currentDT - Convert.ToDateTime(dr["msgDatetime"].ToString().Replace('_', ' '));
                            string formattedDuration = $"{duration.Hours:00}:{duration.Minutes:00}:{duration.Seconds:00}";

                            string alarmMsg = dr["msgErrorCode"].ToString().Replace(',', ' ') + "|";
                            alarmMsg += dr["msgModule"].ToString().Replace(',', ' ') + "|";
                            alarmMsg += dr["msgError"].ToString().Replace(',', ' ') + "|";
                            alarmMsg += dr["msgAction"].ToString().Replace(',', ' ');

                            Dispatcher.Invoke(new Action(() => DexcomGlobalFunctions.logDexcomDur($"Alarm|{dr["msgType"]}", dr["msgDatetime"].ToString(), DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss.fff"), formattedDuration, alarmMsg)));
                        }
                    }
                }
                catch (Exception ex)
                {
                    FileLogger.logError(ex.Message, $"{ex.InnerException}" + $"{ex}");
                }

                dt_AlarmTable = AlertWarningDt;
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        //void _Main_OnError()
        //{
        //    try
        //    {
        //        string errMsg = "";
        //        int errornum = 0;
        //        dt_PreAlarmTable = AlertWarningDt.Copy();   //Only copy works, if not will clear all data, Hao(12072018)
        //        AlertWarningDt.Rows.Clear();

        //        this.Dispatcher.Invoke(new Action(() => dgAlertWarning.Items.Refresh()));

        //        if (alarmLst.Contains(","))
        //        {
        //            ErrorCode = alarmLst.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);  //don't need split as it is in array
        //                                                                                                    // var ErrorCode1 = ErrorCode.Except(prevErrorCode);
        //                                                                                                    //ErrorCode.Reverse().Reverse();
        //            foreach (var Error in ErrorCode)
        //            {
        //                errornum = Convert.ToInt32(Error);
        //                ErrorMessage(errornum, out errMsg);
        //                this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
        //            }
        //        }
        //        else
        //        {
        //            if (alarmLst != "")
        //            {
        //                string Error = alarmLst;
        //                errornum = Convert.ToInt32(Error);
        //                ErrorMessage(errornum, out errMsg);
        //                this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
        //            }
        //        }

        //        if (warningLst.Contains(","))
        //        {
        //            WarningCode = warningLst.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        //            //WarningCode.Reverse().Reverse();
        //            foreach (string Error in WarningCode)
        //            {
        //                errornum = Convert.ToInt32(Error) + WarningStartsAt;
        //                ErrorMessage(errornum, out errMsg);
        //                this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
        //            }
        //        }
        //        else
        //        {
        //            if (warningLst != "")
        //            {
        //                string Error = warningLst;

        //                errornum = Convert.ToInt32(Error) + WarningStartsAt;
        //                ErrorMessage(errornum, out errMsg);
        //                this.Dispatcher.Invoke(new Action(() => Utilities.FileLogger.LogAlertWarning(AlertWarningDt, dt_PreAlarmTable, errornum.ToString(), errMsg)));
        //            }
        //        }
        //        dt_AlarmTable = AlertWarningDt;
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.FileLogger.logError(ex.Message, ex.ToString());
        //    }
        //}

        #endregion Event

        #region Method

        private void LogFileManagement()
        {
            try
            {
                string LogsPrefix = "LogsBackUp_";

                //If Month Add, Path Add
                string PathThisMonth = Properties.Settings.Default.LogLocation.ToString() + "Logs_" + DateTime.Now.ToString("yyyy-MMM");
                string LogsPath = @"D:\HMI\";
                if(FileLogger.DefaultLocation_Time != PathThisMonth)
                    FileLogger.DefaultLocation_Time = PathThisMonth;

                if(!Directory.Exists(FileLogger.DefaultLocation_Time))
                    Directory.CreateDirectory(FileLogger.DefaultLocation_Time);

                //Zip per Month
                string ZipFileName = $@"{LogsPrefix}{DateTime.Now.AddMonths(-1).ToString("yyyy-MMM")}";
                string ZipPath = Path.Combine(FileLogger.DefaultLocation, ZipFileName);
                string LastMonthLog = FileLogger.DefaultLocation + "Logs_" + DateTime.Now.AddMonths(-1).ToString("yyyy-MMM");
                if(!File.Exists(ZipPath) && Directory.Exists(LastMonthLog))
                    ZipFile.CreateFromDirectory(LastMonthLog, ZipPath);

                //FileSize > 90mb split log file
                string[] folderPaths = Directory.GetDirectories(FileLogger.DefaultLocation_Time);
                foreach(string folder in folderPaths)
                    foreach(string file in Directory.GetFiles(folder))
                        if(!file.Contains("_BK_"))
                        {
                            FileInfo info = new FileInfo(file);
                            if((info.Length / 1024 / 1024) > 90)
                            {
                                string ext = Path.GetExtension(file);
                                File.Move(file, Path.ChangeExtension(file, null) + $"_BK_{DateTime.Now.ToString("yyyy-MMM-dd_HHmm.ss.fff")}{ext}");
                            }
                        }

                //Delete Log Folder if more than 3 month
                string[] filePaths = Directory.GetDirectories(LogsPath);
                foreach(string file in filePaths.Where(x => x.Contains("Logs_")))
                {
                    FileInfo info = new FileInfo(file);
                    if(Convert.ToDateTime(file.Substring(file.Length - 8)) < DateTime.Now.AddMonths(-4))
                    {
                        Directory.Delete(file, true);
                    }
                }

                //Duration, delete ZipFile if longer than a year
                filePaths = Directory.GetFiles(LogsPath);
                foreach(string file in filePaths.Where(x => x.Contains(LogsPrefix)))
                {
                    if(Convert.ToDateTime(file.Substring(file.Length - 8)) < DateTime.Now.AddYears(-1))
                    {
                        File.Delete(file);
                    }
                }

                //Space not enough delete
                DriveInfo Ddrive = DriveInfo.GetDrives().Where(x => x.Name.Contains("D")).FirstOrDefault();
                if(Ddrive != null)
                {
                    var totalspace = Ddrive.TotalSize;
                    var freespace = Ddrive.AvailableFreeSpace;

                    if((Convert.ToDouble(freespace) / totalspace * 100) < 20)
                    {
                        filePaths = Directory.GetFiles(LogsPath);
                        if(filePaths.Where(x => x.Contains(LogsPrefix)).Count() > 3)
                        {
                            foreach(string file in filePaths.Where(x => x.Contains(LogsPrefix)))
                            {
                                if(Convert.ToDateTime(file.Substring(file.Length - 8)) < DateTime.Now.AddMonths(-4))
                                {
                                    File.Delete(file);
                                }
                            }
                        }
                    }
                }

                if (currentMonth != Convert.ToInt32(DateTime.Now.Month))
                {
                    //clearing for opc info log - newly added due to too large in file size - 5 Apr 2023
                    string opcInfo = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\";
                    string[] path = Directory.GetDirectories(opcInfo);
                    foreach (string folder in path.Where(x => x.Contains("Logs_")))
                    {
                        FileInfo info = new FileInfo(folder);
                        if (Convert.ToDateTime(folder.Substring(folder.Length - 8)) < DateTime.Now.AddMonths(-1))
                        {
                            Directory.Delete(folder, true);
                        }
                    }

                    //5 Apr 2023 Khaw Wan Yen - Vision Fail Image Folder Auto Deletion
                    if (GlobalFunctions.IsSystem1)
                    {
                        string[] failImageInsertionPath = Directory.GetDirectories(GlobalFunctions.System1Insertion);
                        string[] failImageBatteryPath = Directory.GetDirectories(GlobalFunctions.System1Battery);

                        foreach (string folder in failImageInsertionPath.Where(x => x.Contains("Logs_")))
                        {
                            FileInfo info = new FileInfo(folder);
                            if (Convert.ToDateTime(folder.Substring(folder.Length - 8).Replace("_", "-")) < DateTime.Now.AddMonths(-4))
                            {
                                Directory.Delete(folder, true);
                            }
                        }

                        foreach (string folder in failImageBatteryPath.Where(x => x.Contains("Logs_")))
                        {
                            FileInfo info = new FileInfo(folder);
                            if (Convert.ToDateTime(folder.Substring(folder.Length - 8).Replace("_", "-")) < DateTime.Now.AddMonths(-4))
                            {
                                Directory.Delete(folder, true);
                            }
                        }
                    }
                    else
                    {
                        string[] system2failImagePath = Directory.GetDirectories(GlobalFunctions.System2FailImage);
                        //string[] system2failImagePath = Directory.GetDirectories("D:\\System2\\hist\\"); //testing purpose
                        foreach (string folder in system2failImagePath.Where(x => x.Contains("0_")))
                        {
                            FileInfo info = new FileInfo(folder);
                            var split = folder.Split('_');
                            if (DateTime.ParseExact(split[1].Substring(0, 4), "yyMM", CultureInfo.InvariantCulture) < DateTime.Now.AddMonths(-4))
                            {
                                Directory.Delete(folder, true);
                            }
                        }
                    }
                    currentMonth = Convert.ToInt32(DateTime.Now.Month);
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void BreakTimeOffDay_Check()
        {
            int WithinBreak = 0;

            DataTable DT_Day;

            if(GlobalFunctions.ProjectType == ProjectType.ARCADIA)
            {
                DT_Day = _Main.MainSQLer.Exec_DTSelect($"SELECT [StartDay] as 'Start'," +
                    "[EndDay] as 'End' FROM [NONSCHEDULEDDOWNTIME] WHERE [TYPE] = 'Day'");
            }
            else
            {
                DT_Day = _Main.SQLer.Exec_DTSelect($"SELECT [StartDay] as 'Start'," +
                    "[EndDay] as 'End' FROM [NONSCHEDULEDDOWNTIME] WHERE [TYPE] = 'Day'");
            }

            if(DT_Day != null)
            {
                foreach(DataRow dr in DT_Day.Rows)
                {
                    DateTime start = Convert.ToDateTime(dr["Start"].ToString());
                    DateTime end = Convert.ToDateTime(dr["End"].ToString());
                    TimeSpan ts = new TimeSpan(23, 59, 59);
                    if((DateTime.Now >= start) &&
                      (DateTime.Now <= end.Add(ts)))
                        ++WithinBreak;
                }
            }

            if(WithinBreak == 0)
            {
                DataTable DT;
                if(GlobalFunctions.ProjectType == ProjectType.ARCADIA)
                {
                    DT = _Main.MainSQLer.Exec_DTSelect($"SELECT SUBSTRING(CONVERT(VARCHAR, [STARTTIME],108),1,5) AS 'Start'," +
                        $"SUBSTRING(CONVERT(VARCHAR,[ENDTIME],108),1,5) AS 'End' FROM [NONSCHEDULEDDOWNTIME] WHERE [TYPE] = 'TIME'");
                }
                else
                {
                    DT = _Main.SQLer.Exec_DTSelect($"SELECT SUBSTRING(CONVERT(VARCHAR, [STARTTIME],108),1,5) AS 'Start'," +
                        $"SUBSTRING(CONVERT(VARCHAR,[ENDTIME],108),1,5) AS 'End' FROM [NONSCHEDULEDDOWNTIME] WHERE [TYPE] = 'TIME'");
                }

                if(DT != null)
                    foreach(DataRow dr in DT.Rows)
                    {
                        string[] StartTime = dr["Start"].ToString().Split(':');
                        string[] EndTime = dr["End"].ToString().Split(':');
                        TimeSpan ts_Start = new TimeSpan(Convert.ToInt32(StartTime[0]), Convert.ToInt32(StartTime[1]), 0);
                        TimeSpan ts_End = new TimeSpan(Convert.ToInt32(EndTime[0]), Convert.ToInt32(EndTime[1]), 0);
                        TimeSpan now = DateTime.Now.TimeOfDay;
                        if((now > ts_Start) && (now < ts_End))
                            ++WithinBreak;
                    }
            }

            //_Main.OPC.Write("OEE_Tags.bool_NonScheduled", WithinBreak > 0);
            _Main.OPC.Write(NonScheduledOEETagName, WithinBreak > 0);
        }

        private bool ErrorMessage(int ErrorCode, out string _strErrorMessage)
        {
            bool success = false;
            _strErrorMessage = TryFindResource("ERR_UNKNOWN").ToString();

            if(GlobalFunctions.ErrorListDict.ContainsKey(ErrorCode))
            {
                _strErrorMessage = GlobalFunctions.ErrorListDict[ErrorCode];
                success = true;
            }
            return success;
        }

        private bool PreCheck()
        {
            bool Check = true;

            if(String.IsNullOrEmpty(ucPassword.Text))
            {
                if(String.IsNullOrEmpty(ucPassword.ucLabelErrorContent))
                {
                    this.ucPassword.ucLabelErrorContent = this.TryFindResource("LOGIN_ERROR_MSG_PASSWORD_IS_EMPTY").ToString();
                    Check = false;
                }
            }
            else if(!String.IsNullOrEmpty(ucPassword.Text))
            {
                if(!String.IsNullOrEmpty(ucPassword.ucLabelErrorContent))
                {
                    this.ucPassword.ucLabelErrorContent = String.Empty;
                }
            }
            return Check;
        }

        private void Main_Closed(object sender, EventArgs e)
        {
            Dispose();
        }

        public void Dispose()
        {
            try
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion Method

        #region Destructor

        ~Main()
        {
            Dispose();
        }

        #endregion Destructor

        private void LogIN_OUT(bool LogIN)
        {
            try
            {
                idleTime = 0;
                if (!LogIN)
                {
                    _Main.UserAccessLevel = "Operator";
                    lblCurrUser.Content = "Current User :  " + _Main.UserAccessLevel;
                }

                if (LogIN)
                {
                    logoutCount = 0;
                    FileLogger.logEvent("[HMI]", $"[HMI] UserAccessLevel: {_Main.UserAccessLevel} | {_Main.UserName} Login Success"); 
                }
                else
                {
                    _Main.UserName = string.Empty;
                    logoutCount++;
                    if (logoutCount == 1)
                    {
                        FileLogger.logEvent("[HMI]", $"[HMI] Logout Success");
                    }
                }

                Img_Logout.Visibility = LogIN ? Visibility.Visible : Visibility.Collapsed;
                Img_Login.Visibility = LogIN ? Visibility.Collapsed : Visibility.Visible;
                TXT_Login.Visibility = LogIN ? Visibility.Collapsed : Visibility.Visible;
                TXT_Logout.Visibility = LogIN ? Visibility.Visible : Visibility.Collapsed;

                foreach (Image img in LstImg_PageDisIcon)
                {
                    if (_Main.UserAccessLevel != "Operator")
                    {
                        if (_Main.UserAccessLevel == "Technician")
                        {
                            if (img == Img_DryrunDis || img == Img_IOListDis /*|| img == Img_EngineeringDis || img == Img_DryrunDis*/)
                            {
                                img.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                img.Visibility = Visibility.Visible;
                            }
                        }

                        if (_Main.UserAccessLevel == "Engineer")
                        {
                            if (img == Img_EngineeringDis || img == Img_MotorDis || img == Img_IOListDis || img == Img_DryrunDis)
                            {
                                img.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                img.Visibility = Visibility.Visible;
                            }
                        }

                        if (_Main.UserAccessLevel == "admin" || _Main.UserAccessLevel == "Penta")
                        {
                            img.Visibility = LogIN ? Visibility.Collapsed : Visibility.Visible;
                        }
                    }
                    else
                    {
                        img.Visibility = Visibility.Visible;
                    }
                    //if (img == Img_DryrunDis || img == Img_UserDis || img == Img_EngineeringDis)
                    //{
                    //    if (_Main.UserAccessLevel == "Engineer")
                    //    {
                    //        img.Visibility = Visibility.Collapsed;
                    //    }
                    //    else
                    //    {
                    //        img.Visibility = Visibility.Visible;
                    //    }
                    //}

                    //if (img == Img_SettingDis || img == Img_MotorDis || img == Img_IOListDis /*|| img == Img_EngineeringDis || img == Img_DryrunDis*/)
                    //{
                    //    if (_Main.UserAccessLevel == "Technician")
                    //    {
                    //        img.Visibility = Visibility.Collapsed;
                    //    }
                    //    else
                    //    {
                    //        img.Visibility = Visibility.Visible;
                    //    }
                    //}

                    //if (_Main.UserAccessLevel == "Engineer" || _Main.UserAccessLevel == "admin" || _Main.UserAccessLevel == "Penta")
                    //{
                    //    img.Visibility = LogIN ? Visibility.Collapsed : Visibility.Visible;
                    //}
                    //img.Visibility = LogIN ? Visibility.Collapsed : Visibility.Visible;
                }

                if (!LogIN)
                    SwapPage("HOME");
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private string CurPage = string.Empty; private StackPanel SP_Colored;

        private void SwapPage(string Page)
        {
            try
            {
                if(Page.Equals(CurPage))
                    return;

                if(ProjectType.ARCADIA == GlobalFunctions.ProjectType && StationType.ARCADIA_Main == GlobalFunctions.StationType && Page.Equals("HOME"))
                {
                    Grd_FullScreenPage.Visibility = Visibility.Visible;
                    _Main.HomePageON = true;
                    return;
                }
                else if(GlobalFunctions.ProjectType == ProjectType.TLA &&
                    GlobalFunctions.StationType == StationType.ARCADIA_Main && Page.Equals("HOME"))
                {
                    TLA_MainConveyorGrid.Visibility = Visibility.Visible;
                    _Main.HomePageON = true;
                    return;
                }
                else if(!Page.Equals("LOGIN") && !Page.Equals("LOGOUT"))
                    if(ucChild != null)
                    {
                        ((IDisposable)ucChild).Dispose();
                        ucChild = null;
                        if(ucChildDUT != null)
                        {
                            ((IDisposable)ucChildDUT).Dispose();
                        }
                    }

                if(SP_Colored != null)
                    SP_Colored.Background = Brushes.Transparent;

                FileLogger.logEvent("[HMI]", $"[HMI] UserID: {_Main.UserName} | Access Level: {_Main.UserAccessLevel} | Page: {Page}");

                // https://en.wikipedia.org/wiki/Goto#Criticism, suspect below logic written way before 21st century
                // https://en.wikipedia.org/wiki/Structured_programming Please study this before using goto
                switch (Page)
                {
                    case "HOME":
                    case "DUT":
                        _Main.HomePageON = true;
                        SP_Colored = SP_Home;

                        ucChild = HomeView;
                        _Main.DUTPageOn = true;
                        //SP_Colored = sp_DUT;
                        ucChildDUT = DUTView;
                        goto default;

                    case "IO":
                        _Main.IOPageON = true;
                        SP_Colored = Sp_IOList;
                        //ucChild = IOInputChild;
                        ucChild = iOView;
                        goto default;
                    case "IOLOC":
                        _Main.IOLocPageON = true;
                        SP_Colored = Sp_IOLoc;
                        ucChild = IOLocationChild;
                        goto default;
                    case "SOEE":
                        Info.OPC.TagGroups.OEE.Active = true;
                        _Main.OEEPageON = true;
                        SP_Colored = Sp_OEEShift;
                        ucChild = OEEShiftView;
                        goto default;
                    case "LOEE":
                        Info.OPC.TagGroups.OEE.Active = true;
                        _Main.OEEPageON = true;
                        SP_Colored = Sp_OEELot;
                        ucChild = OEELotView;
                        goto default;
                    case "MOTOR":
                        _Main.MotorPageON = true;
                        SP_Colored = Sp_Motor;
                        ucChild = MotorChild;
                        goto default;
                    case "DRYRUN":
                        _Main.DryRunPageON = true;
                        SP_Colored = DryrunStackPanel;
                        ucChild = dryRunView;
                        goto default;
                    case "SETTING":
                        _Main.SettingPageON = true;
                        Info.OPC.TagGroups.Setting.Active = true;
                        SP_Colored = Sp_Setting;
                        ucChild = SettingChild;
                        goto default;
                    case "LOG":
                        SP_Colored = Sp_Log;
                        ucChild = LogChild;
                        goto default;
                    case "USER":
                        SP_Colored = SP_User;
                        ucChild = UserAcountChild;
                        goto default;
                    case "ALARM":
                        SP_Colored = Sp_AlmCfg;
                        ucChild = AlertChild;
                        goto default;
                    case "ENGINEERING":
                        _Main.EngineeringPageOn = true;
                        SP_Colored = EngineeringStackPanel;
                        ucChild = DexcomEngineeringView; //engineeringView;
                        goto default;
                    case "REJECTBINDISPLAY":
                        _Main.RejectBinDisplayPageOn = true;
                        SP_Colored = RejectBinDisplayStackPanel;
                        ucChild = rejectBinDisplayView;
                        goto default;
                    case "TRAYMAP":
                        _Main.TrayMapPageOn = true;
                        SP_Colored = sp_TrayMap;
                        ucChild = TrayMapView;
                        goto default;
                    case "STACKER":
                        _Main.StackerPageOn = true;
                        SP_Colored = sp_Stacker;
                        ucChild = StackerView;
                        goto default;
                    case "LOT":
                        _Main.LotPageOn = true;
                        SP_Colored = sp_Lot;
                        ucChild = LotView;
                        goto default;
                    // DUT Move to main
                    //case "DUT":
                    //    _Main.DUTPageOn = true;
                    //    SP_Colored = sp_DUT;
                    //    ucChild = DUTView;
                    //    goto default;
                    case "RACKCONFIGURATION":
                        _Main.RackConfigurationPageOn = true;
                        SP_Colored = RackConfigurationStackPanel;
                        ucChild = rackConfigurationView;
                        goto default;
                    case "RECIPECONFIGURATION":
                        SP_Colored = RecipeConfigurationStackPanel;
                        ucChild = recipeConfigurationView;
                        goto default;
                    case "ABOUT":
                        SP_Colored = AboutStackPanel;
                        ucChild = new AboutView();
                        goto default;
                    case "LOGIN":
                        signInView.Show();
                        goto case "COLORING";
                    case "LOGOUT":
                        LogIN_OUT(false);
                        if(_Main.OPC.Read<bool>(EMtag.Name))
                            if(MessageBox.Show("Engineering Mode On, \n" +
                                "Do you want to Turn Off the Engineering Mode Before Log out?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                                _Main.OPC.Write(EMtag.Name, false);
                        goto case "COLORING";
                    default:
                        CurPage = Page;
                        goto case "ADDCHILD";
                    case "ADDCHILD":
                        AddChild(ucChild);
                        goto case "COLORING";
                    case "COLORING":
                        SP_Colored.Background = Brushes.DeepSkyBlue;
                        break;
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void Page_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Image Page = sender as Image;
                if(Page == null || Page.Tag == null)
                    return;

                SwapPage(Page.Tag.ToString().ToUpper());
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void Reinit_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show($"Proceed to Reinitiate Machine?", "Comfirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                {
                    if(_Main.MachineStatus.ToUpper() == "IDLE" || _Main.MachineStatus.ToUpper() == "IDLING")
                    {
                        _Main.OPC.Write(GlobalFunctions.IsSystem1 ? "System1_MC_Tag.MachineInit" : "System2_MC_Tag.MachineInit", false);
                    }
                    else
                    {
                        MessageBox.Show("Cannot re-initialise, machine is not idling.");
                    }
                }
            }
        }

        #region PrivateMessengerMethods

        private void signIn(Tuple<SignInView, string> tuple)
        {
            try
            {
                lblCurrUser.Content = "Current User :  " + tuple.Item2;

                lastInputInfo.cbSize = (uint)Marshal.SizeOf(lastInputInfo);
                lastInputInfo.dwTime = 0;

                timer.Enabled = true;
                timer.Tick += timer_Tick; // Everytime timer ticks, timer_Tick will be called
                timer.Interval = 1000 * 1;              // Timer will tick evert second
                                                        // Enable the timer
                timer.Start();

                LogIN_OUT(true);
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        //Main Line Layout
        private Dictionary<string, Tuple<Shp.Rectangle, TextBlock>> Dic_EstopName_Tuple_RecTbk;

        private Dictionary<string, TextBlock> Dic_Tagname_TextBlockToFill;

        private Dictionary<string, Brush> Dic_Status_Color = new Dictionary<string, Brush>
        {
            ["RUNNING"] = Brushes.MediumSeaGreen,
            ["RUN"] = Brushes.MediumSeaGreen,
            ["IDLE"] = Brushes.DeepSkyBlue,
            ["IDLING"] = Brushes.DeepSkyBlue,
            ["INITIALIZE"] = Brushes.DeepSkyBlue,
            ["INITIALIZING"] = Brushes.DeepSkyBlue,
            ["INITIAL"] = Brushes.DeepSkyBlue,
            ["ALARM"] = Brushes.Red,
            ["ERROR"] = Brushes.Red,
            ["DISABLED"] = Brushes.DimGray,
        };

        private TagGroup tgp_Status = new TagGroup();
        private TagGroup tgp_Estop = new TagGroup();
        private TagGroup tgp_General = new TagGroup();

        private void InitializeArcadiaMainLine()
        {
            List<Tuple<string, Shp.Rectangle, TextBlock>> LstTuple_Tagname_Rec_Tbk = new List<Tuple<string, Shp.Rectangle, TextBlock>>
            {
                Tuple.Create("TrayPlacement_HS_FromRBC.MachineStatus",Rec_TrayPlacement,Tbk_TrayPlacement),
                Tuple.Create("Labelling_HS_FromRBC.MachineStatus",Rec_Label, Tbk_Label),
                Tuple.Create("MOBO_HS_FromRBC.MachineStatus",Rec_Mobo, Tbk_Mobo),
                Tuple.Create("CenVis_HS_FromRBC.MachineStatus",Rec_CentralVision, Tbk_CentralVision),
                Tuple.Create("PowerCable_HS_FromRBC.MachineStatus",Rec_Power, Tbk_Power),
                Tuple.Create("Fan40mm_HS_FromRBC.MachineStatus",Rec_40mmFan, Tbk_40mmFan),
                Tuple.Create("Heatsink1_HS_FromRBC.MachineStatus",Rec_Heatsink1, Tbk_Heatsink1),
                Tuple.Create("Heatsink2_HS_FromRBC.MachineStatus",Rec_Heatsink2, Tbk_Heatsink2),
                Tuple.Create("AffixFan_HS_FromRBC.MachineStatus",Rec_Fan, Tbk_Fan),
                Tuple.Create("BIOSCover_HS_FromRBC.MachineStatus",Rec_BiosCover, Tbk_BiosCover),
                Tuple.Create("BIOS_HS_FromRBC.MachineStatus",Rec_Bios, Tbk_Bios),
                Tuple.Create("TempSens_HS_FromRBC.MachineStatus",Rec_Temp, Tbk_Temp),
                Tuple.Create("OutputPnP_HS_FromRBC.MachineStatus",Rec_OutputVision, Tbk_OutputVision),

                Tuple.Create("FrontEnd_Lifter.str_MachineStatusZ1",Rec_FrontendLifter_Zone1, Tbk_FrontendLifter_Zone1),
                Tuple.Create("FrontEnd_Lifter.str_MachineStatusZ2",Rec_FrontendLifter_Zone2, Tbk_FrontendLifter_Zone2),
                Tuple.Create("BackEnd_Lifter.str_MachineStatusZ1",Rec_BackendLifter_Zone1, Tbk_BackendLifter_Zone1),
                Tuple.Create("BackEnd_Lifter.str_MachineStatusZ2",Rec_BackendLifter_Zone2, Tbk_BackendLifter_Zone2),
            };

            foreach(var item in LstTuple_Tagname_Rec_Tbk)
                tgp_Status.AddTag(new Tag { Name = item.Item1, DataType = Logix.Tag.ATOMIC.STRING, MyObject = Tuple.Create(item.Item2, item.Item3) });

            List<Tag> lst_EstopTag = new List<Tag>
            {
                new Tag{MyObject = "Z1E1", Name = "PalletConv_PIO201:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z1E2", Name = "PalletConv_PIO201:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z3E1", Name = "PalletConv_PIO203:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z3E2", Name = "PalletConv_PIO203:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z4E1", Name = "PalletConv_PIO204:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z4E2", Name = "PalletConv_PIO204:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z51",  Name = "PalletConv_PIO205:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z5E2", Name = "PalletConv_PIO205:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z6E1", Name = "PalletConv_PIO206:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z6E2", Name = "PalletConv_PIO206:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z7E1", Name = "PalletConv_PIO207:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z7E2", Name = "PalletConv_PIO207:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z8E1", Name = "PalletConv_PIO208:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z8E2", Name = "PalletConv_PIO208:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z9E1", Name = "PalletConv_PIO209:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z9E2", Name = "PalletConv_PIO209:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z10E1", Name = "PalletConv_PIO210:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z10E2", Name = "PalletConv_PIO210:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z11E1", Name = "PalletConv_PIO211:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z11E2", Name = "PalletConv_PIO211:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z12E1", Name = "PalletConv_PIO212:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z12E2", Name = "PalletConv_PIO212:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z13E1", Name = "PalletConv_PIO213:2:I.Data.2", DataType = Logix.Tag.ATOMIC.BOOL},
                new Tag{MyObject = "Z13E2", Name = "PalletConv_PIO213:2:I.Data.3", DataType = Logix.Tag.ATOMIC.BOOL},
            };

            foreach(var item in lst_EstopTag)
                tgp_Estop.AddTag(item);

            Dic_EstopName_Tuple_RecTbk = new Dictionary<string, Tuple<Shp.Rectangle, TextBlock>>
            {
                ["Z1E1"] = Tuple.Create(Rec_Z1E1, Tbk_Z1E1),
                ["Z1E2"] = Tuple.Create(Rec_Z1E2, Tbk_Z1E2),
                ["Z3E1"] = Tuple.Create(Rec_Z3E1, Tbk_Z3E1),
                ["Z3E2"] = Tuple.Create(Rec_Z3E2, Tbk_Z3E2),
                ["Z4E1"] = Tuple.Create(Rec_Z4E1, Tbk_Z4E1),
                ["Z4E2"] = Tuple.Create(Rec_Z4E2, Tbk_Z4E2),
                ["Z5E1"] = Tuple.Create(Rec_Z5E1, Tbk_Z5E1),
                ["Z5E2"] = Tuple.Create(Rec_Z5E2, Tbk_Z5E2),
                ["Z6E1"] = Tuple.Create(Rec_Z6E1, Tbk_Z6E1),
                ["Z6E2"] = Tuple.Create(Rec_Z6E2, Tbk_Z6E2),
                ["Z7E1"] = Tuple.Create(Rec_Z7E1, Tbk_Z7E1),
                ["Z7E2"] = Tuple.Create(Rec_Z7E2, Tbk_Z7E2),
                ["Z8E1"] = Tuple.Create(Rec_Z8E1, Tbk_Z8E1),
                ["Z8E2"] = Tuple.Create(Rec_Z8E2, Tbk_Z8E2),
                ["Z9E1"] = Tuple.Create(Rec_Z9E1, Tbk_Z9E1),
                ["Z9E2"] = Tuple.Create(Rec_Z9E2, Tbk_Z9E2),
                ["Z10E1"] = Tuple.Create(Rec_Z10E1, Tbk_Z10E1),
                ["Z10E2"] = Tuple.Create(Rec_Z10E2, Tbk_Z10E2),
                ["Z11E1"] = Tuple.Create(Rec_Z11E1, Tbk_Z11E1),
                ["Z11E2"] = Tuple.Create(Rec_Z11E2, Tbk_Z11E2),
                ["Z12E1"] = Tuple.Create(Rec_Z12E1, Tbk_Z12E1),
                ["Z12E2"] = Tuple.Create(Rec_Z12E2, Tbk_Z12E2),
                ["Z13E1"] = Tuple.Create(Rec_Z13E1, Tbk_Z13E1),
                ["Z13E2"] = Tuple.Create(Rec_Z13E2, Tbk_Z13E2),
            };

            foreach(var item in Dic_EstopName_Tuple_RecTbk.Values)
            {
                item.Item1.Visibility = Visibility.Collapsed;
                item.Item2.Visibility = Visibility.Collapsed;
            }

            //Dic_Tagname_TextBlockToFill = new Dictionary<string, TextBlock>
            //{
            //    ["FrontEnd_Lifter.str_RackRFIDTagZ1"] = Tbk_FrontendLifter_Zone1,
            //    ["FrontEnd_Lifter.str_RackRFIDTagZ2"] = Tbk_FrontendLifter_Zone2,
            //    ["BackEnd_Lifter.str_RackRFIDTagZ1"] = Tbk_BackendLifter_Zone1,
            //    ["BackEnd_Lifter.str_RackRFIDTagZ2"] = Tbk_BackendLifter_Zone2,
            //};

            //foreach (var item in Dic_Tagname_TextBlockToFill)
            //    tgp_General.AddTag(new Tag { Name = item.Key, DataType = Logix.Tag.ATOMIC.STRING, MyObject = item.Value });

            _Main.Home_OnUpdate += new LogicClasses.Main.onHomeUpdateHandler(MainUpdate);
        }

        private IEnumerable<string> IEnum_LastEstopon;

        private void MainUpdate()
        {
            try
            {
                _Main.MyPLC.GroupRead(tgp_Status, tgp_Estop, tgp_General);

                foreach(Tag item in tgp_Status.Tags)
                {
                    Brush color;
                    string Status = (item.Value?.ToString() ?? "ERROR").ToUpper();
                    if(!Dic_Status_Color.TryGetValue(Status, out color))
                        color = Brushes.Orange;
                    Tuple<Shp.Rectangle, TextBlock> tuple_Rec_Tbk = item.MyObject as Tuple<Shp.Rectangle, TextBlock>;
                    Dispatcher.Invoke(() => tuple_Rec_Tbk.Item1.Fill = color);
                    Dispatcher.Invoke(() => tuple_Rec_Tbk.Item2.Text = Status);
                    Dispatcher.Invoke(() => tuple_Rec_Tbk.Item2.Foreground = color);
                }

                var IEnum_EstopOn = tgp_Estop.Tags.ToArray().Where(x => Convert.ToBoolean(((Tag)x)?.Value ?? false))?.Select(x => ((Tag)x).MyObject.ToString());
                if(IEnum_LastEstopon != IEnum_EstopOn)
                {
                    if(IEnum_LastEstopon != null)
                    {
                        foreach(var estop in IEnum_LastEstopon)
                        {
                            Dispatcher.Invoke(() => Dic_EstopName_Tuple_RecTbk[estop].Item1.Visibility = Visibility.Collapsed);
                            Dispatcher.Invoke(() => Dic_EstopName_Tuple_RecTbk[estop].Item2.Visibility = Visibility.Collapsed);
                        }
                    }
                    IEnum_LastEstopon = IEnum_EstopOn;
                    if(IEnum_LastEstopon != null)
                    {
                        foreach(var estop in IEnum_LastEstopon)
                        {
                            Dispatcher.Invoke(() => Dic_EstopName_Tuple_RecTbk[estop].Item1.Visibility = Visibility.Visible);
                            Dispatcher.Invoke(() => Dic_EstopName_Tuple_RecTbk[estop].Item2.Visibility = Visibility.Visible);
                        }
                    }
                }
                foreach(Tag item in tgp_General.Tags)
                {
                    if(item.MyObject.GetType() == typeof(TextBlock))
                        Dispatcher.Invoke(() => ((TextBlock)item.MyObject).Text = item.Value?.ToString() ?? string.Empty);
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void btn_HideMainConveyorLineLayout_Click(object sender, RoutedEventArgs e)
        {
            Grd_FullScreenPage.Visibility = Visibility.Collapsed;
            _Main.HomePageON = false;
            SwapPage("OEE");
        }

        #endregion PrivateMessengerMethods

        private void hideTLA_MainConveyorViewButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TLA_MainConveyorGrid.Visibility = Visibility.Collapsed;
                _Main.HomePageON = false;
                SwapPage("OEE");
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
    }
}