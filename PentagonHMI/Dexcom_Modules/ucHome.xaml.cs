using System;
using System.Windows.Media;
using System.Windows.Controls;
using Utilities;
using System.Windows;
using System.Collections.Generic;
using SimpleDatabase;
using System.Reflection;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media.Imaging;
using System.Data;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using PentagonHMI.Info;

namespace PentagonHMI.ChildControls
{
    public partial class ucHome : UserControl, IDisposable
    {
        SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc();
        SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);

        private LogicClasses.Main _Main;
        
        //Show Turret Image
        public BitmapImage ImageToShow { get; set; }

        //General
        const string Home = "Home";

        //Lot 
        const string Tag_PurgeLot_bool = "Lot_Info.HMI_Purge_Lot_Bit";
        const string Tag_EndLot_bool = "Lot_Info.HMI_End_Lot_Bit";
        const string Tag_LotMode_int = "Lot_Info.HMI_Lot_Mode";
        Dictionary<int, string> dic_LotMode = new Dictionary<int, string>
        {
            [1] = "Commercial",
            [2] = "Non-Commercial"
        };

        //PromptBox
        const string Tag_PromptBox_bool = "Lot_Info.HMI_Pause_Lot_Prompt";

        //Lot info
        const string Tag_LotID_str = "Lot_Info.HMI_LotID";
        const string Tag_LotSize_str = "Lot_Info.HMI_Lot_Quantity";
        const string Tag_OprID_str20 = "Lot_Info.HMI_OperatorID";
        const string Tag_DUTID_str20 = "RecipeParams.DUTid";
        const string Tag_BatteryType_dint = "Lot_Info.HMI_BatteryType";
        const string Tag_Firmware_str20 = "RecipeParams.FirmwareVersion";
        const string Tag_DaysExpire_int = "Lot_Info.HMI_DaysToExpired";
        const string Tag_ManufactureDate_str20 = "Lot_Info.HMI_ManufactureDate";
        const string Tag_Expiration_str20 = "Lot_Info.HMI_Expiration_Date";

        //Quality
        const string Tag_Quality = "Lot_OEE_Tags.dint_Quality";

        //TnR
        const string Tag_TnR1Sts_int = "HMI_Tags.TnR1_Status";
        const string Tag_TnR2Sts_int = "HMI_Tags.TnR2_Status";
        Dictionary<int, string> dic_TnRStatus = new Dictionary<int, string>
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
        const string Tag_LdPnPEmpty_bool = "ISL0_Rbt1DUT_EMPTY";
        const string Tag_GantryEmpty_bool = "ISL0_Gantry_EMPTY";
        const string Tag_TestShuttleEmpty_bool = "ISL0_TestStationDUT_EMPTY";
        const string Tag_TopLaserShuttleEmpty_bool = "ISL0_TopShuttleDUT_EMPTY";
        const string Tag_BottomLaserShuttleEmpty_bool = "ISL0_BottomShuttleDUT_EMPTY";
        const string Tag_UldPnPEmpty_bool = "ISL0_Rbt2DUT_EMPTY";
        const string Tag_LeftTrayTransferEmpty_bool = "ISL0_LeftTrayShuttle_EMPTY";
        const string Tag_RightTrayTransferEmpty_bool = "ISL0_RightTrayShuttle_EMPTY";
        Dictionary<bool, Brush> dic_DUTcolor = new Dictionary<bool, Brush>
        {
            [true] = Brushes.Gray,
            [false] = Brushes.Yellow,
        };

        //Top6 Status
        const string Tag_Top1Sts_int = "HMI_TOPM1_Status";
        const string Tag_Top2Sts_int = "HMI_TOPM2_Status";
        const string Tag_Top3Sts_int = "HMI_TOPM3_Status";
        const string Tag_Top4Sts_int = "HMI_TOPM4_Status";
        const string Tag_Top5Sts_int = "HMI_TOPM5_Status";
        const string Tag_Top6Sts_int = "HMI_TOPM6_Status";

        const string Tag_Top1ErrCode_int = "HMI_TOPM1_ErrorCode";
        const string Tag_Top2ErrCode_int = "HMI_TOPM2_ErrorCode";
        const string Tag_Top3ErrCode_int = "HMI_TOPM3_ErrorCode";
        const string Tag_Top4ErrCode_int = "HMI_TOPM4_ErrorCode";
        const string Tag_Top5ErrCode_int = "HMI_TOPM5_ErrorCode";
        const string Tag_Top6ErrCode_int = "HMI_TOPM6_ErrorCode";
        Dictionary<int, string> dic_TopStatus = new Dictionary<int, string>
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
        const string Tag_Station1_Stat = "Station_1.DUT_Status";
        const string Tag_Station2_Stat = "Station_2_DUT_Status";
        const string Tag_Station3_Stat = "Station_3_DUT_Status";
        const string Tag_Station4_Stat = "Station_4_DUT_Status";
        const string Tag_Station5_Stat = "Station_5_DUT_Status";
        const string Tag_Station6_Stat = "Station_6_DUT_Status";
        const string Tag_Station7_Stat = "Station_7_DUT_Status";
        const string Tag_Station8_Stat = "Station_8_DUT_Status";
        ObservableCollection<DUTStationStatModel> DUTs = new ObservableCollection<DUTStationStatModel>();
        Dictionary<int, Brush> Dic_StationStatColor = new Dictionary<int, Brush>();

        public ucHome(LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;

#if !DEBUG
            if (!OPCore.Connect(Info.OPC.IP)) return;
#endif
            Initialize();
            main.Home_OnUpdate += HomeUpdate;


        }

        private void Initialize()
        {
            Func<ChartPoint, string> PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            Pie_Quality.Series = new SeriesCollection
            {
              new PieSeries { Title = "Quality", Fill = Brushes.SpringGreen,  Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true, LabelPoint = PointLabel },
              new PieSeries { Title = "_", Fill = Brushes.Gray, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
            };

#if DEBUG
            //Quality
            double QualityPercent = 0.6 * 100;
            Pie_Quality.Series[0].Values[0] = QualityPercent.To2Dcml();
            Pie_Quality.Series[1].Values[0] = 100 - QualityPercent.To2Dcml();

#else
            //grd_KeyenceVision.Children.Add(new ucKeyenceVision(_Main));
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

                    //Station Status
                    var Stat1 = OPCore.Read<int>(Tag_Station1_Stat, typeof(int));
                    var Stat2 = OPCore.Read<int>(Tag_Station2_Stat, typeof(int));
                    var Stat3 = OPCore.Read<int>(Tag_Station3_Stat, typeof(int));
                    var Stat4 = OPCore.Read<int>(Tag_Station4_Stat, typeof(int));
                    var Stat5 = OPCore.Read<int>(Tag_Station5_Stat, typeof(int));
                    var Stat6 = OPCore.Read<int>(Tag_Station6_Stat, typeof(int));
                    var Stat7 = OPCore.Read<int>(Tag_Station7_Stat, typeof(int));
                    var Stat8 = OPCore.Read<int>(Tag_Station8_Stat, typeof(int));

                    //    if (Stat1 != null)
                    //        foreach (var item in ugrd_LeftTray.Children)
                    //        {
                    //            TextBlock tb = item as TextBlock;
                    //            string status = Stat1[Convert.ToInt32(tb.Tag) - 1].ToString();
                    //            tb.Text = status;
                    //            tb.Background = Dic_StatusColor[status];
                    //        }

                    //    if (Stat2 != null)
                    //        foreach (var item in ugrd_RightTray.Children)
                    //        {
                    //            TextBlock tb = item as TextBlock;
                    //            string status = Stat2[Convert.ToInt32(tb.Tag) - 1].ToString();
                    //            tb.Text = status;
                    //            tb.Background = Dic_StatusColor[status];
                    //        }

                    //    if (Stat3 != null)
                    //        foreach (var item in pcba_SlotTray.Children)
                    //        {
                    //            TextBlock tb = item as TextBlock;
                    //            string status = Stat3[Convert.ToInt32(tb.Tag) - 1].ToString();
                    //            tb.Text = status;
                    //            tb.Background = Dic_StatusColor[status];
                    //        }

                    //    if (Stat4 != null)
                    //        foreach (var item in bat_SlotTray.Children)
                    //        {
                    //            TextBlock tb = item as TextBlock;
                    //            string status = Stat4[Convert.ToInt32(tb.Tag) - 1].ToString();
                    //            tb.Text = status;
                    //            tb.Background = Dic_StatusColor[status];
                    //        }

                    //    if (Stat5 != null)
                    //        foreach (var item in bat_SlotTray.Children)
                    //        {
                    //            TextBlock tb = item as TextBlock;
                    //            string status = Stat5[Convert.ToInt32(tb.Tag) - 1].ToString();
                    //            tb.Text = status;
                    //            tb.Background = Dic_StatusColor[status];
                    //        }

                    //    if (Stat6 != null)
                    //        foreach (var item in bat_SlotTray.Children)
                    //        {
                    //            TextBlock tb = item as TextBlock;
                    //            string status = Stat6[Convert.ToInt32(tb.Tag) - 1].ToString();
                    //            tb.Text = status;
                    //            tb.Background = Dic_StatusColor[status];
                    //        }

                    //    if (Stat7 != null)
                    //        foreach (var item in bat_SlotTray.Children)
                    //        {
                    //            TextBlock tb = item as TextBlock;
                    //            string status = Stat7[Convert.ToInt32(tb.Tag) - 1].ToString();
                    //            tb.Text = status;
                    //            tb.Background = Dic_StatusColor[status];
                    //        }

                    //    if (Stat8 != null)
                    //        foreach (var item in bat_SlotTray.Children)
                    //        {
                    //            TextBlock tb = item as TextBlock;
                    //            string status = Stat8[Convert.ToInt32(tb.Tag) - 1].ToString();
                    //            tb.Text = status;
                    //            tb.Background = Dic_StatusColor[status];
                    //        }
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
                get { return background; }
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
            FileLogger.logButton(Home, "Prompt", MethodBase.GetCurrentMethod().ToString());
            OPCore.Write(Tag_PromptBox_bool, true);
        }

    }
}
