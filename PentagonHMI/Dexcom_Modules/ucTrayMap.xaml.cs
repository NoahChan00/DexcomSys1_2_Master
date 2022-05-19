using System;
using System.Windows.Media;
using System.Windows.Controls;
using Utilities;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using SimpleDatabase;
using System.Data;

namespace PentagonHMI.ChildControls
{
    public partial class ucTrayMap : UserControl, IDisposable
    {
        SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc();
        SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName);

        private LogicClasses.Main _Main;

        const string Tag_L_Reinspect = "ISL0_LeftRack_HMIReq_InspectTray";
        const string Tag_R_Reinspect = "ISL0_RightRack_HMIReq_InspectTray";
        const string Tag_L_ChangeTray = "ISL0_HMI_LeftRack_ReqChangeTray";
        const string Tag_R_ChangeTray = "ISL0_HMI_RightRack_ReqChangeTray";
        const string Tag_PCBA_ChangeTray = "HMI_PCBA_ReqChangeTray";
        const string Tag_Battery_ChangeTray = "HMI_Battery_ReqChangeTray";
        const string Tag_L_PurgeRack = "ISL0_HMI_LeftRack_PurgeReq";
        const string Tag_R_PurgeRack = "ISL0_HMI_RightRack_PurgeReq";
        const string Tag_TrayRow = "HMI_Tags.TrayRowNo";
        const string Tag_TrayCol = "HMI_Tags.TrayColumnNo";
        const string Tag_L_TopVision = "ISL0_TopVision_LeftTrayMap[0]";//Length 100;
        const string Tag_R_TopVision = "ISL0_TopVision_LeftTrayMap[0]";//Length 100;
        const string Tag_Btry_dint = "Lot_Info.HMI_BatteryType";
        const string Tag_L_Slot = "Tray_Shuttle_Left_Slot_Tracking";
        const string Tag_R_Slot = "Tray_Shuttle_Right_Slot_Tracking";

        int Row = 0;
        int Col = 0;
        int PRow = 0;
        int PCol = 0;
        int BRow = 0;
        int BCol = 0;
        int BatType = 0;

        Dictionary<string, Brush> Dic_ResultColor = new Dictionary<string, Brush>();

        public ucTrayMap(LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;
#if !DEBUG
            if (!OPCore.Connect(Info.OPC.IP)) return;
#endif
            Initialize();
            main.OnTrayMapUpdate += TrayMapUpdate;
        }

        private void Initialize()
        {
#if !DEBUG
            Row = OPCore.Read<int>(Tag_TrayRow);
            Col = OPCore.Read<int>(Tag_TrayCol);
            PRow = OPCore.Read<int>(Tag_TrayRow);
            PCol = OPCore.Read<int>(Tag_TrayCol);
            BRow = OPCore.Read<int>(Tag_TrayRow);
            BCol = OPCore.Read<int>(Tag_TrayCol);
            BatType = OPCore.Read<int>(Tag_Btry_dint);
#else
            Row = 4;
            Col = 4;
            PRow = 9;
            PCol = 10;

            var Battery_Type = 6;  

            switch(Battery_Type)
            {
                //Maxell
                case 2:
                    BRow = 10;
                    BCol = 10;
                    break;
                //Panasonic
                case 5:
                    BRow = 4;
                    BCol = 10;
                    break;
                //Murata
                case 6:
                    BRow = 9;
                    BCol = 10;
                    break;
                default:
                    MessageBox.Show("Failed to read battery type from PLC.");
                    break;
            }

#endif

            BrushConverter bc = new BrushConverter();
            DataTable dt = SQLer.Exec_DTSelect("Select * From TrayMapColor");
            foreach (DataRow dr in dt.Rows)
            {
                Dic_ResultColor.Add(dr["result"].ToString(), (Brush)bc.ConvertFromString(dr["color"].ToString()));
                Legends.Children.Add(new TextBlock
                {
                    FontSize = 15,
                    Text = dr["result"].ToString() + " " + dr["description"].ToString(),
                    Background = Dic_ResultColor[dr["result"].ToString()]
                });
            }

            ugrd_LeftTray.Rows = Row;
            ugrd_LeftTray.Columns = Col;
            ugrd_RightTray.Rows = Row;
            ugrd_RightTray.Columns = Col;
            pcba_SlotTray.Rows = PRow;
            pcba_SlotTray.Columns = PCol;
            bat_SlotTray.Rows = BRow;
            bat_SlotTray.Columns = BCol;


            int total = Row * Col;
            for (int t = 1; t <= total; t++)
            {
                ugrd_LeftTray.Children.Add(new TextBlock { Tag = t, Text = "-" });
                ugrd_RightTray.Children.Add(new TextBlock { Tag = t, Text = "-" });
            }

            int totalPCBA = PRow * PCol;
            for (int tp = 1; tp <= totalPCBA; tp++)
            {
                pcba_SlotTray.Children.Add(new TextBlock { Tag = tp, Text = "-" });
            }

            int totalBattery = BRow * BCol;
            for (int tb = 1; tb <= totalBattery; tb++)
            {
                bat_SlotTray.Children.Add(new TextBlock { Tag = tb, Text = "-" });
            }
        }


        private void TrayMapUpdate()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    var LAry = OPCore.Read<int[]>(Tag_L_TopVision, typeof(int), 100);
                    var RAry = OPCore.Read<int[]>(Tag_R_TopVision, typeof(int), 100);
                    var PCBAray = OPCore.Read<int[]>(Tag_L_TopVision, typeof(int), 100);
                    var BatAray = OPCore.Read<int[]>(Tag_R_TopVision, typeof(int), 100);


                    if (LAry != null)
                        foreach (var item in ugrd_LeftTray.Children)
                        {
                            TextBlock tb = item as TextBlock;
                            string result = LAry[Convert.ToInt32(tb.Tag) - 1].ToString();
                            tb.Text = result;
                            tb.Background = Dic_ResultColor[result];
                        }
                    if (RAry != null)
                        foreach (var item in ugrd_RightTray.Children)
                        {
                            TextBlock tb = item as TextBlock;
                            string result = RAry[Convert.ToInt32(tb.Tag) - 1].ToString();
                            tb.Text = result;
                            tb.Background = Dic_ResultColor[result];
                        }
                    if (PCBAray != null)
                        foreach (var item in pcba_SlotTray.Children)
                        {
                            TextBlock tb = item as TextBlock;
                            string result = PCBAray[Convert.ToInt32(tb.Tag) - 1].ToString();
                            tb.Text = result;
                            tb.Background = Dic_ResultColor[result];
                        }
                    if (BatAray != null)
                        foreach (var item in bat_SlotTray.Children)
                        {
                            TextBlock tb = item as TextBlock;
                            string result = BatAray[Convert.ToInt32(tb.Tag) - 1].ToString();
                            tb.Text = result;
                            tb.Background = Dic_ResultColor[result];
                        }
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            });
        }

        ~ucTrayMap()
        {
            Dispose();
        }

        public void Dispose()
        {
            _Main.TrayMapPageOn = false;
        }

        private void Reinspect_Click(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();
            OPCore.Write(tag == "L" ? Tag_L_Reinspect : tag == "R" ? Tag_R_Reinspect : "", true);
        }

        private void ChangeTray_Click(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();
            OPCore.Write(tag == "L" ? Tag_L_ChangeTray : tag == "R" ? Tag_R_ChangeTray : tag == "pcba" ? Tag_PCBA_ChangeTray : tag == "battery" ? Tag_Battery_ChangeTray : "", true);
        }

        private void Purge_Click(object sender, DependencyPropertyChangedEventArgs e)
        {
            ToggleButton tbtn = sender as ToggleButton;
            string tag = tbtn.Tag.ToString();
            OPCore.Write(tag == "L" ? Tag_L_PurgeRack : tag == "R" ? Tag_R_PurgeRack : "", tbtn.IsChecked);

        }
    }
}
