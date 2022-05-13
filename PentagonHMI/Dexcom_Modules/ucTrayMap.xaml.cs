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
        const string Tag_L_PurgeRack = "ISL0_HMI_LeftRack_PurgeReq";
        const string Tag_R_PurgeRack = "ISL0_HMI_RightRack_PurgeReq";
        const string Tag_TrayRow = "HMI_Tags.TrayRowNo";
        const string Tag_TrayCol = "HMI_Tags.TrayColumnNo";
        const string Tag_L_TopVision = "ISL0_TopVision_LeftTrayMap[0]";//Length 100;
        const string Tag_R_TopVision = "ISL0_TopVision_LeftTrayMap[0]";//Length 100;
        int Row = 0;
        int Col = 0;

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
#else
            Row = 10;
            Col = 10;
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
            pcba_lot_track.Rows = Row;
            pcba_lot_track.Columns = Col;
            bat_slot_track.Rows = Row;
            bat_slot_track.Columns = Col;


            int total = Row * Col;
            for (int t = 1; t <= total; t++)
            {
                ugrd_LeftTray.Children.Add(new TextBlock { Tag = t, Text = "-" });
                ugrd_RightTray.Children.Add(new TextBlock { Tag = t, Text = "-" });
                pcba_SlotTray.Children.Add(new TextBlock { Tag = t, Text = "-" });
                bat_SlotTray.Children.Add(new TextBlock { Tag = t, Text = "-" });
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
                        foreach (var item in ugrd_LeftTray.Children)
                        {
                            TextBlock tb = item as TextBlock;
                            string result = LAry[Convert.ToInt32(tb.Tag) - 1].ToString();
                            tb.Text = result;
                            tb.Background = Dic_ResultColor[result];
                        }
                    if (BatAray != null)
                        foreach (var item in ugrd_RightTray.Children)
                        {
                            TextBlock tb = item as TextBlock;
                            string result = RAry[Convert.ToInt32(tb.Tag) - 1].ToString();
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
            OPCore.Write(tag == "L" ? Tag_L_ChangeTray : tag == "R" ? Tag_R_ChangeTray : "", true);
        }

        private void Purge_Click(object sender, DependencyPropertyChangedEventArgs e)
        {
            ToggleButton tbtn = sender as ToggleButton;
            string tag = tbtn.Tag.ToString();
            OPCore.Write(tag == "L" ? Tag_L_PurgeRack : tag == "R" ? Tag_R_PurgeRack : "", tbtn.IsChecked);

        }
    }
}
