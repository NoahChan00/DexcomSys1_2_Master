using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using PentagonHMI.Classes;
using SimpleDatabase;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Utilities;

namespace PentagonHMI.ChildControls
{
    public partial class ucHome : UserControl, IDisposable
    {
        #region PrivateFields

        private ControlPanelModel promptPanelModel = new ControlPanelModel();
        private LogicClasses.Main _Main;

        #endregion PrivateFields

        private SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc(Info.OPC.IP);
        private SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);

        //Show Turret Image
        public BitmapImage ImageToShow
        {
            get; set;
        }

        //General
        private const string Home = "Home";

        //Lot
        private const string Tag_PurgeLot_bool = "Lot_Info.HMI_Purge_Lot_Bit";

        private const string Tag_EndLot_bool = "Lot_Info.HMI_End_Lot_Bit";
        private const string Tag_LotMode_int = "Lot_Info.HMI_Lot_Mode";

        private Dictionary<int, string> dic_LotMode = new Dictionary<int, string>
        {
            [1] = "Commercial",
            [2] = "Non-Commercial"
        };

        //PromptBox
        private const string Tag_PromptBox_bool = "Lot_Info.HMI_Pause_Lot_Prompt";

        //Lot info
        private const string Tag_LotID_str = "Lot_Info.HMI_LotID";

        private const string Tag_LotSize_str = "Lot_Info.HMI_Lot_Quantity";
        private const string Tag_OprID_str20 = "Lot_Info.HMI_OperatorID";
        private const string Tag_DUTID_str20 = "RecipeParams.DUTid";
        private const string Tag_BatteryType_dint = "Lot_Info.HMI_BatteryType";
        private const string Tag_Firmware_str20 = "RecipeParams.FirmwareVersion";
        private const string Tag_DaysExpire_int = "Lot_Info.HMI_DaysToExpired";
        private const string Tag_ManufactureDate_str20 = "Lot_Info.HMI_ManufactureDate";
        private const string Tag_Expiration_str20 = "Lot_Info.HMI_Expiration_Date";

        //Quality
        private const string Tag_Quality = "Lot_OEE_Tags.dint_Quality";

        //TnR
        private const string Tag_TnR1Sts_int = "HMI_Tags.TnR1_Status";

        private const string Tag_TnR2Sts_int = "HMI_Tags.TnR2_Status";

        private Dictionary<int, string> dic_TnRStatus = new Dictionary<int, string>
        {
            [0] = "Not Init Done",
            [1] = "Init Done",
            [2] = "Idle",
            [3] = "Running",
            [4] = "Disabled",
            [5] = "Leader Processing",
            [6] = "Post Leader Processing",
            [7] = "Trailer Processing",
            [8] = "Peel Test",
            [9] = "Abort",
            [10] = "Reel completed"
        };

        //DUT Presence
        private const string Tag_LdPnPEmpty_bool = "ISL0_Rbt1DUT_EMPTY";

        private const string Tag_GantryEmpty_bool = "ISL0_Gantry_EMPTY";
        private const string Tag_TestShuttleEmpty_bool = "ISL0_TestStationDUT_EMPTY";
        private const string Tag_TopLaserShuttleEmpty_bool = "ISL0_TopShuttleDUT_EMPTY";
        private const string Tag_BottomLaserShuttleEmpty_bool = "ISL0_BottomShuttleDUT_EMPTY";
        private const string Tag_UldPnPEmpty_bool = "ISL0_Rbt2DUT_EMPTY";
        private const string Tag_LeftTrayTransferEmpty_bool = "ISL0_LeftTrayShuttle_EMPTY";
        private const string Tag_RightTrayTransferEmpty_bool = "ISL0_RightTrayShuttle_EMPTY";

        private Dictionary<bool, Brush> dic_DUTcolor = new Dictionary<bool, Brush>
        {
            [true] = Brushes.Gray,
            [false] = Brushes.Yellow,
        };

        //Top6 Status
        private const string Tag_Top1Sts_int = "HMI_TOPM1_Status";

        private const string Tag_Top2Sts_int = "HMI_TOPM2_Status";
        private const string Tag_Top3Sts_int = "HMI_TOPM3_Status";
        private const string Tag_Top4Sts_int = "HMI_TOPM4_Status";
        private const string Tag_Top5Sts_int = "HMI_TOPM5_Status";
        private const string Tag_Top6Sts_int = "HMI_TOPM6_Status";

        private const string Tag_Top1ErrCode_int = "HMI_TOPM1_ErrorCode";
        private const string Tag_Top2ErrCode_int = "HMI_TOPM2_ErrorCode";
        private const string Tag_Top3ErrCode_int = "HMI_TOPM3_ErrorCode";
        private const string Tag_Top4ErrCode_int = "HMI_TOPM4_ErrorCode";
        private const string Tag_Top5ErrCode_int = "HMI_TOPM5_ErrorCode";
        private const string Tag_Top6ErrCode_int = "HMI_TOPM6_ErrorCode";

        private Dictionary<int, string> dic_TopStatus = new Dictionary<int, string>
        {
            [1] = "Disabled",
            [2] = "Self test Fail",
            [3] = "TestInProgress",
            [4] = "Error",
            [5] = "HB Lost",
            [6] = "Pass",
            [7] = "Fail",
            [8] = "Idle",
        };


        public ucHome(LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;

#if !DEBUG
            if(!OPCore.Connect(Info.OPC.IP))
                return;
#endif
            Initialize();
            main.Home_OnUpdate += HomeUpdate;
            // Had to call below statement from here, because we like dependency injection :)
            GridForDUT.Children.Add(new ucDUT(_Main));
        }

        private void Initialize()
        {
            void SetCanvasTopLeft(StackPanel cc, double t, double l)
            {
                Canvas.SetTop(cc, t);
                Canvas.SetLeft(cc, l);
            }
            if(GlobalFunctions.IsSystem1)
            {
                LegendUniformGrid.Children.Insert(2, new Button() { Background = Brushes.Magenta, Content = "Battery Present" });
                BlueButton.Content = "PCBA Present";
                System1_Stuff.Visibility = Visibility.Visible;
                System2_Stuff.Visibility = Visibility.Collapsed;
                TurnStepImage.Source = new BitmapImage(new Uri("/HMI;component/Images/MainHMI/S1TurnTable.jpeg", UriKind.Relative));


                Station1Tb.Text = "Station 5\r\nVISION CHECK";
                Station1DutStatus.Tag = ("Station_5.DUT_Status", "Station_5.Nest_Code");
                SetCanvasTopLeft(SP1, 0, 430);

                Station2Tb.Text = "Station 6\r\nBUFFER";
                Station2DutStatus.Tag = ("Station_6.DUT_Status", "Station_6.Nest_Code");
                SetCanvasTopLeft(SP2, 80, 580);

                Station3Tb.Text = "Station 7\r\nROBOT UNLOAD";
                Station3DutStatus.Tag = ("Station_7.DUT_Status", "Station_7.Nest_Code");
                SetCanvasTopLeft(SP3, 260, 650);

                Station4Tb.Text = "Station 8\r\nEMPTY CHECK";
                Station4DutStatus.Tag = ("Station_8.DUT_Status", "Station_8.Nest_Code");
                SetCanvasTopLeft(SP4, 400, 620);

                Station5Tb.Text = "Station 1\r\nPCBA LOAD";
                Station5DutStatus.Tag = ("Station_1.DUT_Status", "Station_1.Nest_Code");
                SetCanvasTopLeft(SP5, 565, 400);

                Station6Tb.Text = "Station 2\r\nBUFFER";
                Station6DutStatus.Tag = ("Station_2.DUT_Status", "Station_2.Nest_Code");
                SetCanvasTopLeft(SP6, 440, 210);

                Station7Tb.Text = "Station 3\r\nBATTERY LOAD";
                Station7DutStatus.Tag = ("Station_3.DUT_Status", "Station_3.Nest_Code");
                SetCanvasTopLeft(SP7, 260, 75);

                Station8Tb.Text = "Station 4\r\nBUFFER";
                Station8DutStatus.Tag = ("Station_4.DUT_Status", "Station_4.Nest_Code");
                SetCanvasTopLeft(SP8, 60, 200);
            }
            else
            {
                System2_Stuff.Visibility = Visibility.Visible;
                System1_Stuff.Visibility = Visibility.Collapsed;
                VisionControlTC.Items.Add(new TabItem() { Header = "Vision", Content = new VisionView(_Main) });
                TurnStepImage.Source = new BitmapImage(new Uri("/HMI;component/Images/MainHMI/S2TurnTable.jpeg", UriKind.Relative));

                Station1Tb.Text = "Station 7\r\nEMPTY Check";
                Station1DutStatus.Tag = ("Station_7.DUT_Status", "Station_7.Nest_Code");
                SetCanvasTopLeft(SP1, 15, 360);

                Station2Tb.Text = "Station 8\r\nBUFFER";
                Station2DutStatus.Tag = ("Station_8.DUT_Status", "Station_8.Nest_Code");
                SetCanvasTopLeft(SP2, 80, 560);

                Station3Tb.Text = "Station 1\r\nROBOT LOAD";
                Station3DutStatus.Tag = ("Station_1.DUT_Status", "Station_1.Nest_Code");
                SetCanvasTopLeft(SP3, 240, 630);

                Station4Tb.Text = "Station 2\r\nBUFFER";
                Station4DutStatus.Tag = ("Station_2.DUT_Status", "Station_2.Nest_Code");
                SetCanvasTopLeft(SP4, 420, 580);

                Station5Tb.Text = "Station 3\r\nVISION CHECK";
                Station5DutStatus.Tag = ("Station_3.DUT_Status", "Station_3.Nest_Code");
                SetCanvasTopLeft(SP5, 500, 330);

                Station6Tb.Text = "Station 4\r\nBUFFER";
                Station6DutStatus.Tag = ("Station_4.DUT_Status", "Station_4.Nest_Code");
                SetCanvasTopLeft(SP6, 420, 230);

                Station7Tb.Text = "Station 5\r\nROBOT UNLOAD";
                Station7DutStatus.Tag = ("Station_5.DUT_Status", "Station_5.Nest_Code");
                SetCanvasTopLeft(SP7, 250, 120);

                Station8Tb.Text = "Station 6\r\nBUFFER";
                Station8DutStatus.Tag = ("Station_6.DUT_Status", "Station_6.Nest_Code");
                SetCanvasTopLeft(SP8, 80, 240);
            }
        }

        private void HomeUpdate()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    if(!GlobalFunctions.IsSystem1)
                    {
                        LabelLotID.Content = _Main.OPC.Read<string>(Tags.MainPage.LotID.Name, typeof(string)) ?? "Nan";
                    }


                    #region Station Status Read

                    var stats = new List<int>();
                    var innertTxt = new List<string>();
#if !DEBUG
                    stats.AddRange(new List<int> {
                        OPCore.Read<int>((((string, string))Station1DutStatus.Tag).Item1),
                        OPCore.Read<int>((((string, string))Station2DutStatus.Tag).Item1),
                        OPCore.Read<int>((((string, string))Station3DutStatus.Tag).Item1),
                        OPCore.Read<int>((((string, string))Station4DutStatus.Tag).Item1),
                        OPCore.Read<int>((((string, string))Station5DutStatus.Tag).Item1),
                        OPCore.Read<int>((((string, string))Station6DutStatus.Tag).Item1),
                        OPCore.Read<int>((((string, string))Station7DutStatus.Tag).Item1),
                        OPCore.Read<int>((((string, string))Station8DutStatus.Tag).Item1),
                    });

                    innertTxt.AddRange(new List<string> {
                        OPCore.Read<string>((((string,string))Station1DutStatus.Tag).Item2),
                        OPCore.Read<string>((((string,string))Station2DutStatus.Tag).Item2),
                        OPCore.Read<string>((((string,string))Station3DutStatus.Tag).Item2),
                        OPCore.Read<string>((((string,string))Station4DutStatus.Tag).Item2),
                        OPCore.Read<string>((((string,string))Station5DutStatus.Tag).Item2),
                        OPCore.Read<string>((((string,string))Station6DutStatus.Tag).Item2),
                        OPCore.Read<string>((((string,string))Station7DutStatus.Tag).Item2),
                        OPCore.Read<string>((((string,string))Station8DutStatus.Tag).Item2),
                    });
#else
                    stats.AddRange(new List<int> { 0, 1, 10, 11, 0, 2, 10, 11 });
                    innertTxt.AddRange(new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" });
#endif

                    var stationsStatus = new List<Button>(){
                        Station1DutStatus,
                        Station2DutStatus,
                        Station3DutStatus,
                        Station4DutStatus,
                        Station5DutStatus,
                        Station6DutStatus,
                        Station7DutStatus,
                        Station8DutStatus
                    };

                    for(int i = 0; i < stats.Count; i++)
                    {
                        switch(stats[i])
                        {
                            case 0:
                                stationsStatus[i].Background = Brushes.Gray;
                                break;

                            case 1:
                                stationsStatus[i].Background = Brushes.Blue;
                                break;
                            // Only for system 1, since 2 not use. Assume wont read 2
                            case 2:
                                stationsStatus[i].Background = Brushes.Magenta;
                                break;
                            case 10:
                                stationsStatus[i].Background = Brushes.Green;
                                break;

                            case 11:
                                stationsStatus[i].Background = Brushes.Red;
                                break;

                            default:
                                // not sure what to do
                                break;

                        }
                        stationsStatus[i].Content = innertTxt[i];
                        MuteToogleButton.IsChecked = OPCore.Read<bool>(Tags.MainPage.Mute.Name, typeof(bool));
                    }
                    UpdateLotInfo();

                    #endregion Station Status Read
                }
                catch(Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            });
        }
        void UpdateLotInfo()
        {
            Dispatcher.Invoke(() =>
            {
                if(GlobalFunctions.IsSystem1)
                {
                    LotIDTextBlock.Text = OPCore.Read<string>(Tags.MainPage.LotIDLotID1.Name);
                    OperatorIDTextBlock.Text = OPCore.Read<string>(Tags.MainPage.LotIDOperatorID1.Name);
                    LotQuatityTextBlock.Text = OPCore.Read<int>(Tags.MainPage.LotIDLotQuantity1.Name).ToString();
                    int batteryTypeId = OPCore.Read<int>(Tags.MainPage.LotIDLotBatteryType1.Name);
                    BatteryTypeTextBlock.Text = batteryTypeId == 2 ? "Maxell" : batteryTypeId == 5 ? "Panasonic" : "Murata";
                }
                else
                {
                    LotIDTextBlock.Text = OPCore.Read<string>(Tags.MainPage.LotIDLotID2.Name);
                    OperatorIDTextBlock.Text = OPCore.Read<string>(Tags.MainPage.LotIDOperatorID2.Name);
                    LotQuatityTextBlock.Text = OPCore.Read<int>(Tags.MainPage.LotIDLotQuantity2.Name).ToString();
                    int batteryTypeId = OPCore.Read<int>(Tags.MainPage.LotIDLotBatteryType2.Name);
                    BatteryTypeTextBlock.Text = batteryTypeId == 2 ? "Maxell" : batteryTypeId == 5 ? "Panasonic" : "Murata";
                }
                TotalUnitPassTextBlock.Text = OPCore.Read<int>(Tags.MainPage.TotalUnitPass.Name).ToString();
                TotalUnitFailTextBlock.Text = OPCore.Read<int>(Tags.MainPage.TotalUnitFail.Name).ToString();
                TotalProductionUnitTextBlock.Text = OPCore.Read<int>(Tags.MainPage.TotalProductionUnit.Name).ToString();
                CurrentYieldTextBlock.Text = OPCore.Read<int>(Tags.MainPage.Quality.Name).ToString();
            });
        }

        ~ucHome()
        {
            Dispose();
        }

        public void Dispose()
        {
            _Main.HomePageON = false;
        }

        public class DUTStationStatModel : ViewModelBase
        {
            private Brush background = Brushes.Gray;

            public Brush Background
            {
                get
                {
                    return background;
                }
                set
                {
                    if(background != value)
                    {
                        background = value;
                        RaisePropertyChanged(nameof(Background));
                    }
                }
            }
        }

        private void End_Click(object sender, RoutedEventArgs e)
        {
            FileLogger.logButton(Home, "End Lot", MethodBase.GetCurrentMethod().ToString());
            OPCore.Write(Tag_EndLot_bool, true);
        }

        private void Purge_Click(object sender, RoutedEventArgs e)
        {
            FileLogger.logButton(Home, "Purge Lot", MethodBase.GetCurrentMethod().ToString());
            OPCore.Write(Tag_PurgeLot_bool, true);
        }

        private void MuteToggleButton_Click(object sender, RoutedEventArgs e)
        {
            if(sender is ToggleButton tb)
            {
                FileLogger.logButton(Home, "Mute", MethodBase.GetCurrentMethod().ToString());
                OPCore.Write(Tags.MainPage.Mute.Name, tb.IsChecked);
            }
        }
    }
}