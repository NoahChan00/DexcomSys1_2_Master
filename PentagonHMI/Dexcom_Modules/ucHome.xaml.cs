using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using SimpleDatabase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
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

        private SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc();
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

        //Station Status
        private const string Tag_Station1_Stat = "Station_1.DUT_Status";

        private const string Tag_Station2_Stat = "Station_2.DUT_Status";
        private const string Tag_Station3_Stat = "Station_3.DUT_Status";
        private const string Tag_Station4_Stat = "Station_4.DUT_Status";
        private const string Tag_Station5_Stat = "Station_5.DUT_Status";
        private const string Tag_Station6_Stat = "Station_6.DUT_Status";
        private const string Tag_Station7_Stat = "Station_7.DUT_Status";
        private const string Tag_Station8_Stat = "Station_8.DUT_Status";
        private ObservableCollection<DUTStationStatModel> DUTs = new ObservableCollection<DUTStationStatModel>();
        private Dictionary<int, Brush> Dic_StationStatColor = new Dictionary<int, Brush>();

        public ucHome(LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;

#if !DEBUG
            if (!OPCore.Connect(Info.OPC.IP))
                return;
#endif
            Initialize();
            main.Home_OnUpdate += HomeUpdate;
            // Had to call below statement from here, because we like dependency injection :)
            GridForDUT.Children.Add(new ucDUT(_Main));
        }

        private void Initialize()
        {
            Func<ChartPoint, string> PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            Pie_Quality.Series = new SeriesCollection
            {
              new PieSeries { Title = "Quality", Fill = Brushes.SpringGreen,  Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true, LabelPoint = PointLabel },
              new PieSeries { Title = "_", Fill = Brushes.Gray, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
            };

#if !DEBUG
            //grd_KeyenceVision.Children.Add(new ucKeyenceVision(_Main));
#else
            //Quality
            double QualityPercent = 0.6 * 100;
            Pie_Quality.Series[0].Values[0] = QualityPercent.To2Dcml();
            Pie_Quality.Series[1].Values[0] = 100 - QualityPercent.To2Dcml();
#endif

            //BrushConverter bc = new BrushConverter();
            //DataTable dtDUTLegends = SQLer.Exec_DTSelect("SELECT * FROM DUTStation_Status");
            //foreach (DataRow dr in dtDUTLegends.Rows)
            //{
            //    Dic_StationStatColor.Add(Convert.ToInt32(dr["Status"]), (Brush)bc.ConvertFromString(dr["Colour"].ToString()));
            //    StationStatLegends.Children.Add(new TextBlock
            //    {
            //        FontSize = 15,
            //        Text = dr["Status"].ToString() + " " + dr["Description"].ToString(),
            //        Background = Dic_StationStatColor[Convert.ToInt32(dr["Status"])],
            //        Padding = new Thickness(15, 2, 15, 2),
            //        Margin = new Thickness(2)
            //    });
            //}
        }

        private void HomeUpdate()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    ////Lot
                    //if (dic_LotMode.TryGetValue(OPCore.Read<int>(Tag_LotMode_int), out string lotmode))
                    //    tbk_Lotmode.Text = lotmode;

                    ////Lot info
                    //lbl_lot_LotID.Content = OPCore.Read<string>(Tag_LotID_str);
                    //lbl_lot_LotSize.Content = OPCore.Read<string>(Tag_LotSize_str);
                    //lbl_lot_OprID.Content = OPCore.Read<string>(Tag_OprID_str20);
                    //lbl_lot_DUTID.Content = OPCore.Read<string>(Tag_DUTID_str20);
                    //lbl_lot_BtyType.Content = OPCore.Read<string>(Tag_BatteryType_dint);
                    //lbl_lot_Firmware.Content = OPCore.Read<string>(Tag_Firmware_str20);
                    //lbl_lot_Days2Exp.Content = OPCore.Read<string>(Tag_DaysExpire_int);
                    //lbl_lot_MDate.Content = OPCore.Read<string>(Tag_ManufactureDate_str20);
                    //lbl_lot_Exp.Content = OPCore.Read<string>(Tag_Expiration_str20);

                    //Quality
                    double QualityPercent = OPCore.Read<int>(Tag_Quality) * 100;
                    Pie_Quality.Series[0].Values[0] = QualityPercent.To2Dcml();
                    Pie_Quality.Series[1].Values[0] = 100 - QualityPercent.To2Dcml();

                    ////TnR
                    //if (dic_TnRStatus.TryGetValue(OPCore.Read<int>(Tag_TnR1Sts_int), out string status))
                    //    lbl_tnr1_Sts.Text = status;
                    //if (dic_TnRStatus.TryGetValue(OPCore.Read<int>(Tag_TnR2Sts_int), out string status2))
                    //    lbl_tnr2_Sts.Text = status2;

                    ////DUT Presence
                    //lbl_dut_LdPnP.Background = dic_DUTcolor[OPCore.Read<bool>(Tag_LdPnPEmpty_bool)];
                    //lbl_dut_Gantry.Background = dic_DUTcolor[OPCore.Read<bool>(Tag_GantryEmpty_bool)];
                    //lbl_dut_TestShuttle.Background = dic_DUTcolor[OPCore.Read<bool>(Tag_TestShuttleEmpty_bool)];
                    //lbl_dut_TopLaserShuttle.Background = dic_DUTcolor[OPCore.Read<bool>(Tag_TopLaserShuttleEmpty_bool)];
                    //lbl_dut_BtmLaserShuttle.Background = dic_DUTcolor[OPCore.Read<bool>(Tag_BottomLaserShuttleEmpty_bool)];
                    //lbl_dut_UnldPnP.Background = dic_DUTcolor[OPCore.Read<bool>(Tag_UldPnPEmpty_bool)];
                    //lbl_dut_LTrayTransfer.Background = dic_DUTcolor[OPCore.Read<bool>(Tag_LeftTrayTransferEmpty_bool)];
                    //lbl_dut_RTrayTransfer.Background = dic_DUTcolor[OPCore.Read<bool>(Tag_RightTrayTransferEmpty_bool)];

                    ////Top6 Status
                    //if (dic_TopStatus.TryGetValue(OPCore.Read<int>(Tag_Top1Sts_int), out string s1))
                    //    tbk_top1_Sts.Text = s1;
                    //if (dic_TopStatus.TryGetValue(OPCore.Read<int>(Tag_Top2Sts_int), out string s2))
                    //    tbk_top2_Sts.Text = s2;
                    //if (dic_TopStatus.TryGetValue(OPCore.Read<int>(Tag_Top3Sts_int), out string s3))
                    //    tbk_top3_Sts.Text = s3;
                    //if (dic_TopStatus.TryGetValue(OPCore.Read<int>(Tag_Top4Sts_int), out string s4))
                    //    tbk_top4_Sts.Text = s4;
                    //if (dic_TopStatus.TryGetValue(OPCore.Read<int>(Tag_Top5Sts_int), out string s5))
                    //    tbk_top5_Sts.Text = s5;
                    //if (dic_TopStatus.TryGetValue(OPCore.Read<int>(Tag_Top6Sts_int), out string s6))
                    //    tbk_top6_Sts.Text = s6;

                    //tbk_top1_Err.Text = OPCore.Read<int>(Tag_Top1ErrCode_int).ToString();
                    //tbk_top2_Err.Text = OPCore.Read<int>(Tag_Top2ErrCode_int).ToString();
                    //tbk_top3_Err.Text = OPCore.Read<int>(Tag_Top3ErrCode_int).ToString();
                    //tbk_top4_Err.Text = OPCore.Read<int>(Tag_Top4ErrCode_int).ToString();
                    //tbk_top5_Err.Text = OPCore.Read<int>(Tag_Top5ErrCode_int).ToString();
                    //tbk_top6_Err.Text = OPCore.Read<int>(Tag_Top6ErrCode_int).ToString();

                    #region Station Status Read

                    var stats = new List<int>();
#if !DEBUG
                    stats.AddRange(new List<int>{
                        OPCore.Read<int>(Tag_Station1_Stat),
                        OPCore.Read<int>(Tag_Station2_Stat),
                        OPCore.Read<int>(Tag_Station3_Stat),
                        OPCore.Read<int>(Tag_Station4_Stat),
                        OPCore.Read<int>(Tag_Station5_Stat),
                        OPCore.Read<int>(Tag_Station6_Stat),
                        OPCore.Read<int>(Tag_Station7_Stat),
                        OPCore.Read<int>(Tag_Station8_Stat)
                    });
#else
                    stats.AddRange(new List<int> { 0, 1, 10, 11, 0, 1, 10, 11 });
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

                    for (int i = 0; i < stats.Count; i++)
                    {
                        switch (stats[i])
                        {
                            case 0:
                                stationsStatus[i].Background = Brushes.Gray;
                                break;

                            case 1:
                                stationsStatus[i].Background = Brushes.Blue;
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
                    }

                    #endregion Station Status Read
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
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
                    if (background != value)
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

        private void promptToggleButton_Click(object sender, RoutedEventArgs e)
        {
            bool check = OPCore.Read<bool>(Tag_PurgeLot_bool, typeof(bool));

            if (check != false)
            {
                FileLogger.logButton(Home, "End Lot", MethodBase.GetCurrentMethod().ToString());
                OPCore.Write(Tag_PromptBox_bool, true);
            }
            else
            {
                FileLogger.logButton(Home, "Continue", MethodBase.GetCurrentMethod().ToString());
                OPCore.Write(Tag_PromptBox_bool, false);
            }
        }
    }
}