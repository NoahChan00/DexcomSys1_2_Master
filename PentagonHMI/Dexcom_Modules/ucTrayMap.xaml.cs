using GalaSoft.MvvmLight.Command;
using PentagonHMI.Classes;
using SimpleDatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Utilities;

namespace PentagonHMI.ChildControls
{
    public partial class ucTrayMap : UserControl, IDisposable
    {
        private SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc(Info.OPC.IP);
        private SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);

        private LogicClasses.Main _Main;

        // Island 0 tag
        //const string Tag_L_Reinspect = "ISL0_LeftRack_HMIReq_InspectTray";
        //const string Tag_R_Reinspect = "ISL0_RightRack_HMIReq_InspectTray";
        //const string Tag_L_ChangeTray = "ISL0_HMI_LeftRack_ReqChangeTray";
        //const string Tag_R_ChangeTray = "ISL0_HMI_RightRack_ReqChangeTray";
        //const string Tag_L_PurgeRack = "ISL0_HMI_LeftRack_PurgeReq";
        //const string Tag_R_PurgeRack = "ISL0_HMI_RightRack_PurgeReq";
        //const string Tag_L_TopVision = "ISL0_TopVision_LeftTrayMap[0]";//Length 100;
        //const string Tag_R_TopVision = "ISL0_TopVision_LeftTrayMap[0]";//Length 100;

        //const string Tag_PCBA_ChangeTray = "HMI_PCBA_ReqChangeTray";
        //const string Tag_Battery_ChangeTray = "HMI_Battery_ReqChangeTray";
        //const string Tag_TrayRow = "HMI_Tags.TrayRowNo";
        //const string Tag_TrayCol = "HMI_Tags.TrayColumnNo";
        private const string Tag_Btry_dint = "Lot_Info.HMI_BatteryType";

        // Dexcom 1 Tag ?
        private const string Tag_L_Shuttle_Slot = "Tray_Shuttle_Left_Slot_Tracking[1]";//Length 16
        private const string Tag_R_Shuttle_Slot = "Tray_Shuttle_Right_Slot_Tracking[1]";//Length 16
        private const string Tag_Btry_Slot = "Tray_Battery_Slot_Tracking[1]";//Length (Panasonic: 40, Maxell: 100, Murata: 50)
        private const string Tag_PCBA_Slot = "Tray_PCBA_Slot_Tracking[1]"; //Length 90

        //System 2 - Tray Map HMI
        // New tag from 1.3
        // PLC tag need index operator, they start from, ignore 0 for this project
        private const string Tag_L_Input_Slot = "LShuttle_Slot_Tracking[1]";//Length 16
        private const string Tag_R_Input_Slot = "RShuttle_Slot_Tracking[1]";//Length 16
        private const string Tag_Output_Slot = "UnloadTray_Slot_Tracking[1]";//Length 60

        private int Row = 0;
        private int Col = 0;
        private int PRow = 0;
        private int PCol = 0;
        private int BRow = 0;
        private int BCol = 0;
        private int ORow = 0;
        private int OCol = 0;
        private int BatType = 0;

        private Dictionary<string, Brush> Dic_ResultColor = new Dictionary<string, Brush>();
        private Dictionary<int, string> Dic_TrayMapSize = new Dictionary<int, string>();

        public ucTrayMap(LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;
#if !DEBUG
            if (!OPCore.Connect(Info.OPC.IP))
                return;
#endif
            Initialize();
            main.OnTrayMapUpdate += TrayMapUpdate;
        }

        private void Initialize()
        {
            if(GlobalFunctions.IsSystem1)
            {
                ScrollViewerSystem1.Visibility = Visibility.Visible;
                ScrollViewerSystem2.Visibility = Visibility.Collapsed;
            }
            else
            {
                ScrollViewerSystem1.Visibility = Visibility.Collapsed;
                ScrollViewerSystem2.Visibility = Visibility.Visible;
            }

            // Comment due to only island Read the tray size from tag, hmi list not provided tag for System 1 and 2
            //#if !DEBUG
            //            Row = OPCore.Read<int>(Tag_TrayRow);
            //            Col = OPCore.Read<int>(Tag_TrayCol);
            //            PRow = OPCore.Read<int>(Tag_TrayRow);
            //            PCol = OPCore.Read<int>(Tag_TrayCol);
            //            ORow = 10;
            //            OCol = 6;
            //            BatType = OPCore.Read<int>(Tag_Btry_dint);
            //            BRow = OPCore.Read<int>(Tag_TrayRow);
            //            BCol = OPCore.Read<int>(Tag_TrayCol);

            //#else

            Row = 4;
            Col = 4;
            PRow = 9;
            PCol = 10;
            ORow = 10;
            OCol = 6;
#if !DEBUG
            if (GlobalFunctions.IsSystem1)
                BatType = OPCore.Read<int>(Tag_Btry_dint);
            else
                BatType = 2;
#else

            BatType = 2;
#endif

            switch(BatType)
            {
                // Maxell
                case 2:
                    BRow = 10;
                    BCol = 10;
                    bat_SlotTray.Margin = new Thickness(0, 10, 0, 0);
                    break;
                // Panasonic
                case 5:
                    BRow = 4;
                    BCol = 10;
                    break;
                // Murata
                case 6:
                    BRow = 10;
                    BCol = 5;
                    bat_SlotTray.Margin = new Thickness(0, 10, 0, 0);
                    break;

                default:
                    MessageBox.Show("Failed to read battery type from PLC.");
                    break;
            }
            //#endif

            BrushConverter bc = new BrushConverter();
            DataTable dt = SQLer.Exec_DTSelect("SELECT * FROM TrayMapColor");
            foreach(DataRow dr in dt.Rows)
            {
                //dr["result"] using nchar instead of varchar, trailing white space x 9
                Dic_ResultColor.Add(dr["result"].ToString().Trim(), (Brush)bc.ConvertFromString(dr["color"].ToString()));
                Legends.Children.Add(new Button
                {
                    Background = Dic_ResultColor[dr["result"].ToString().Trim()],
                    BorderBrush = Brushes.Black,
                    Content = dr["result"].ToString().Trim() + ": " + dr["description"].ToString(),
                    FontSize = 15,
                    FontWeight = FontWeights.Bold,
                    Height = 40
                });
            }

            DataTable datatable = SQLer.Exec_DTSelect("SELECT * FROM TrayMap");

            if(GlobalFunctions.IsSystem1)
            {
                ugrd_LeftTray.Rows = Row;
                ugrd_LeftTray.Columns = Col;
                ugrd_RightTray.Rows = Row;
                ugrd_RightTray.Columns = Col;
                pcba_SlotTray.Rows = PRow;
                pcba_SlotTray.Columns = PCol;
                bat_SlotTray.Rows = BRow;
                bat_SlotTray.Columns = BCol;
            }
            else
            {
                input_LeftSlot.Rows = Row;
                input_LeftSlot.Columns = Col;
                input_RightSlot.Rows = Row;
                input_RightSlot.Columns = Col;
                output_Slot.Rows = ORow;
                output_Slot.Columns = OCol;
            }

            if(GlobalFunctions.IsSystem1)
            {
                int total = Row * Col;
                for(int i = 1; i <= total; i++)
                {
                    ugrd_LeftTray.Children.Add(new Button
                    {
                        Tag = i.ToString(),
                        Height = 25,
                        Width = 25,
                        Margin = new Thickness(1),
                        Command = new RelayCommand<string>(ButtonToggleSlotCommand),
                        CommandParameter = Tag_L_Shuttle_Slot.Replace("1", i.ToString())
                    });
                    ugrd_RightTray.Children.Add(new Button
                    {
                        Tag = i.ToString(),
                        Height = 25,
                        Width = 25,
                        Margin = new Thickness(1),
                        Command = new RelayCommand<string>(ButtonToggleSlotCommand),
                        CommandParameter = Tag_R_Shuttle_Slot.Replace("1", i.ToString())
                    });
                }
                int totalPCBA = PRow * PCol;
                for(int i = 1; i <= totalPCBA; i++)
                {
                    pcba_SlotTray.Children.Add(new Button
                    {
                        Tag = i.ToString(),
                        Height = 25,
                        Width = 25,
                        Margin = new Thickness(1),
                        Command = new RelayCommand<string>(ButtonToggleSlotCommand),
                        CommandParameter = Tag_PCBA_Slot.Replace("1", i.ToString())
                    });
                }
                int totalBattery = BRow * BCol;
                for(int i = 1; i <= totalBattery; i++)
                {
                    bat_SlotTray.Children.Add(new Button
                    {
                        Tag = i.ToString(),
                        Height = 25,
                        Width = 25,
                        Margin = new Thickness(1),
                        Command = new RelayCommand<string>(ButtonToggleSlotCommand),
                        CommandParameter = Tag_Btry_Slot.Replace("1", i.ToString())
                    });
                }
            }
            else
            {
                int total = Row * Col;
                for(int i = 1; i <= total; i++)
                {
                    input_LeftSlot.Children.Add(new Button
                    {
                        Tag = i.ToString(),
                        Height = 25,
                        Width = 25,
                        Margin = new Thickness(1),
                        Command = new RelayCommand<string>(ButtonToggleSlotCommand),
                        CommandParameter = Tag_L_Shuttle_Slot.Replace("1", i.ToString())
                    });
                    input_RightSlot.Children.Add(new Button
                    {
                        Tag = i.ToString(),
                        Height = 25,
                        Width = 25,
                        Margin = new Thickness(1),
                        Command = new RelayCommand<string>(ButtonToggleSlotCommand),
                        CommandParameter = Tag_R_Input_Slot.Replace("1", i.ToString())
                    });
                }
                int totalOutput = ORow * OCol;

                for(int i = 1; i <= totalOutput; i++)
                {
                    output_Slot.Children.Add(new Button
                    {
                        Tag = i.ToString(),
                        Height = 25,
                        Width = 25,
                        Margin = new Thickness(1),
                        Command = new RelayCommand<string>(ButtonToggleSlotCommand),
                        CommandParameter = Tag_Output_Slot.Replace("1", i.ToString())
                    });
                }
            }
        }

        private void TrayMapUpdate()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    short[] BatAry = default;
                    short[] PCBAry = default;
                    short[] LAry = default;
                    short[] RAry = default;

                    short[] InputL = default;
                    short[] InputR = default;
                    short[] OutputAry = default;

                    bool isAdminOrPenta = new List<string>() { "ADMIN", "PENTA" }.Contains(_Main.UserAccessLevel.ToUpper());
                    if(GlobalFunctions.IsSystem1)
                    {
                        BatAry = OPCore.Read<short[]>(Tag_Btry_Slot, typeof(short), BRow * BCol);
                        PCBAry = OPCore.Read<short[]>(Tag_PCBA_Slot, typeof(short), PRow * PCol);
                        LAry = OPCore.Read<short[]>(Tag_L_Shuttle_Slot, typeof(short), Row * Col);
                        RAry = OPCore.Read<short[]>(Tag_R_Shuttle_Slot, typeof(short), Row * Col);
                        PCBASlotButton.IsEnabled = BatterySlotButton.IsEnabled = isAdminOrPenta;
                    }
                    else
                    {
                        InputL = OPCore.Read<short[]>(Tag_L_Input_Slot, typeof(short), Row * Col);
                        InputR = OPCore.Read<short[]>(Tag_R_Input_Slot, typeof(short), Row * Col);
                        OutputAry = OPCore.Read<short[]>(Tag_Output_Slot, typeof(short), ORow * OCol);
                        InputLeftSlotButton.IsEnabled = InputRightSlotButton.IsEnabled = OutputSlotButton.IsEnabled = isAdminOrPenta;

                        if(GlobalFunctions.IsSystem1)
                        {
                            if(LAry != null)
                                foreach(var item in ugrd_LeftTray.Children)
                                {
                                    Button tb = item as Button;
                                    string result = LAry[Convert.ToInt32(tb.Tag) - 1].ToString();
                                    tb.Background = Dic_ResultColor[result];
                                }
                            if(RAry != null)
                                foreach(var item in ugrd_RightTray.Children)
                                {
                                    Button tb = item as Button;
                                    string result = RAry[Convert.ToInt32(tb.Tag) - 1].ToString();
                                    tb.Background = Dic_ResultColor[result];
                                }
                            if(PCBAry != null)
                                foreach(var item in pcba_SlotTray.Children)
                                {
                                    Button tb = item as Button;
                                    string result = PCBAry[Convert.ToInt32(tb.Tag) - 1].ToString();
                                    tb.Background = Dic_ResultColor[result];
                                }
                            if(BatAry != null)
                                foreach(var item in bat_SlotTray.Children)
                                {
                                    Button tb = item as Button;
                                    string result = BatAry[Convert.ToInt32(tb.Tag) - 1].ToString();
                                    tb.Background = Dic_ResultColor[result];
                                }
                        }
                        else
                        {
                            if(InputL != null)
                                foreach(var item in input_LeftSlot.Children)
                                {
                                    Button tb = item as Button;
                                    string result = InputL[Convert.ToInt32(tb.Tag) - 1].ToString();
                                    tb.Background = Dic_ResultColor[result];
                                }
                            if(InputR != null)
                                foreach(var item in input_RightSlot.Children)
                                {
                                    Button tb = item as Button;
                                    string result = InputR[Convert.ToInt32(tb.Tag) - 1].ToString();
                                    tb.Background = Dic_ResultColor[result];
                                }
                            if(OutputAry != null)
                                foreach(var item in output_Slot.Children)
                                {
                                    Button tb = item as Button;
                                    // Somehow tb.Tag start from 0 unlike above 2
                                    string result = OutputAry[Convert.ToInt32(tb.Tag) - 1].ToString();
                                    tb.Background = Dic_ResultColor[result];
                                }
                        }
                    }
                }
                catch(Exception exception)
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
            //OPCore.Write(tag == "L" ? Tag_L_Reinspect : tag == "R" ? Tag_R_Reinspect : "", true);
        }

        private void ChangeTray_Click(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();

            // Just change tag to PLC address. WHY SO HASSLE!
            OPCore.Write(tag, true, typeof(bool));
            //OPCore.Write(tag == "L" ? Tag_L_ChangeTray : tag == "R" ? Tag_R_ChangeTray : tag == "pcba" ? Tag_PCBA_ChangeTray : tag == "battery" ? Tag_Battery_ChangeTray : tag == "inputLeft_Change" ? Tag_L_Input_Slot : tag == "inputRight_Change" ? Tag_R_Input_Slot : tag == "Output_Change" ? Output_Change_Tray : "", true);
        }

        private void Purge_Click(object sender, DependencyPropertyChangedEventArgs e)
        {
            ToggleButton tbtn = sender as ToggleButton;
            string tag = tbtn.Tag.ToString();
            // Tag from 0
            //OPCore.Write(tag == "L" ? Tag_L_PurgeRack : tag == "R" ? Tag_R_PurgeRack : "", tbtn.IsChecked);
        }

        private void ButtonToggleSlotCommand(string tagName)
        {
            bool engineeringMode = OPCore.Read<bool>(Tags.MainPage.EngineeringMode.Name);

            if(engineeringMode)
            {
                int currentValue = OPCore.Read<short>(tagName);
                if(currentValue != 0)
                {
                    if(MessageBox.Show($"Turn off {tagName}?", "Toggle Slot", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        OPCore.Write(tagName, 0, typeof(short));
                }
                else
                {
                    if(MessageBox.Show($"Turn on {tagName}?", "Toggle Slot", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        OPCore.Write(tagName, 1, typeof(short));
                }
            }
        }

        public void RemoveParent()
        {
            // Use for other view, have to disconnect then only can utilize.
            new List<Grid> { input_LeftSlotGrid, input_RightSlotGrid, pcba_SlotTrayGrid, bat_SlotTrayGrid }.ForEach(x => x.Children.Clear());
        }
    }
}