using System;
using System.Windows.Controls;
using Utilities;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using PentagonHMI.Modules.NumUpDown;
using System.Linq;
using PentagonHMI.Tags;
using static PentagonHMI.Enum.Enumeration;
using Logix;

namespace PentagonHMI
{
    public partial class ucDexcom_Engineering : UserControl, IDisposable
    {
        SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc();
        //SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName);
        LogicClasses.Main _Main;
        private struct Control
        {
            public string[] Conditions;
            public string[] AccessGroups;
            public string PLCAddress;
            public UIElement DisplayPanel;
            public string Module;
        }

        private Dictionary<object, Control> dic_EngMajor;
        string ModuleNow = "";
        List<string> ModuleNames;
        private List<Tag> engr_tagList = new List<Tag>();  

        public ucDexcom_Engineering(LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;

#if !DEBUG
            if (!OPCore.Connect(Info.OPC.IP)) return;
#endif
            Initialize();
            main.OnEngineeringUpdate += Update;
        }

        private void Initialize()
        {
            ModuleNames = new List<string>()
            {
                "System_01","System_02",
            };
            cbx_Selected.ItemsSource = ModuleNames;

            dic_EngMajor = new Dictionary<object, Control>();

            //PCBA Robot
            dic_EngMajor.Add(InputRobot_tg_StationJogMode, new Control
            {
                AccessGroups = new string[] { "technician", "engineer" },
                Conditions = new string[] { "" },
                DisplayPanel = InputRobot_tg_StationJogMode,
                Module = ModuleNames[0],
                PLCAddress = "HMI_StationJogMode.LeftRack",
            });

            engr_tagList = new List<Tag>();
            EngineeringPLCTags engineeringPLCTags = new EngineeringPLCTags();
            engr_tagList = engineeringPLCTags.GetType().GetFields().Select(y => (Tag)y.GetValue(engineeringPLCTags)).ToList();
        }

        private void Update()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    foreach (var item in dic_EngMajor)
                    {
                        var control = item.Value;
                        var UI = item.Key;

                        if (control.Module != ModuleNow) continue;

                        bool disable = false;

                        foreach (var condition in control.Conditions)
                            if (!OPCore.Read<bool>(condition))
                                disable = true;

                        disable = disable || !control.AccessGroups.Contains(_Main.UserAccessLevel.ToLower());

                        control.DisplayPanel.IsEnabled = !disable;

                        if (UI is ToggleButton)
                        {
                            var tbtn = UI as ToggleButton;
                            tbtn.IsChecked = OPCore.Read<bool>(control.PLCAddress);
                        }
                    }
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            });
        }   

        ~ucDexcom_Engineering()
        {
            Dispose();
        }

        public void Dispose()
        {
            _Main.EngineeringPageOn = false;
        }

        private void checkStationChangeInit(EngineeringTabs tab, bool initDone)
        {
            try
            {
                switch (tab)
                {
                    case EngineeringTabs.PCBARobot:
                        PCBARobot_tg_StationInitDone.IsChecked = initDone;
                        break;

                    case EngineeringTabs.BatteryRobot:
                        BtryRobot_tg_StationInitDone.IsChecked = initDone;
                        break;

                    case EngineeringTabs.UnloadRobot:
                        UnloadRobot_tg_StationInitDone.IsChecked = initDone;
                        break;

                    case EngineeringTabs.PCBARack:
                        PCBARack_tg_StationInitDone.IsChecked = initDone;
                        break;

                    case EngineeringTabs.BatteryRack:
                        BtryRack_tg_StationInitDone.IsChecked = initDone;
                        break;

                    case EngineeringTabs.InputRobot:
                        InputRobot_tg_StationInitDone.IsChecked = initDone;
                        break;

                    case EngineeringTabs.OutputRobot:
                        OutputRobot_tg_StationInitDone.IsChecked = initDone;
                        break;

                    case EngineeringTabs.InputRack:
                        InputRack_tg_StationInitDone.IsChecked = initDone;
                        break;

                    case EngineeringTabs.OutputRack:
                        OutputRack_tg_StationInitDone.IsChecked = initDone;
                        break;
                }
            }

            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.StackTrace);
            }
        }

        #region PCBA_Robot
        private void PCBARobot_StationJogMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_PCBARobot_JogMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void PCBARobot_StationCycleMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_PCBARobot_StationCycleMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void PCBARobot_PickTray_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_PCBARobot_PickFromTray.Name, true);
        }

        private void PCBARobot_num_RowNo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_PCBARobot_RowNo.Name, (sender as NumUpDown).Value);
        }

        private void PCBARobot_num_ColNo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_PCBARobot_ColumnNo.Name, (sender as NumUpDown).Value);
        }

        private void PCBARobot_PlaceTurret_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_PCBARobot_PlaceToTurret.Name, true);
        }

        private void PCBARobot_StartInit_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_PCBARobot_StationStartInit.Name, true);
        }
        #endregion


        #region Battery_Robot
        private void BtryRobot_StationJogMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRobot_JogMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void BtryRobot_StationCycleMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRobot_StationCycleMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void BtryRobot_PickTray_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRobot_PickFromTray.Name, true);
        }

        private void BtryRobot_num_RowNo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRobot_RowNo.Name, (sender as NumUpDown).Value);
        }

        private void BtryRobot_num_ColNo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRobot_ColumnNo.Name, (sender as NumUpDown).Value);
        }

        private void BtryRobot_cmb_BtryType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRobot_BatteryType.Name, true);
        }

        private void BtryRobot_PlaceTurret_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRobot_PlaceToTurret.Name, true);
        }

        private void BtryRobot_StartInit_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRobot_StationStartInit.Name, true);
        }
        #endregion


        #region Unload_Robot
        private void UnloadRobot_StationJogMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_UnloadRobot_JogMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void UnloadRobot_StationCycleMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_UnloadRobot_StationCycleMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void UnloadRobot_PickTurret_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_UnloadRobot_PickFromTurret.Name, true);
        }    

        private void UnloadRobot_PlaceLeft_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_UnloadRobot_L_PlaceToTurret.Name, true);
        }

        private void UnloadRobot_PlaceRight_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_UnloadRobot_R_PlaceToTurret.Name, true);
        }

        private void UnloadRobot_num_RowNo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_UnloadRobot_RowNo.Name, (sender as NumUpDown).Value);
        }

        private void UnloadRobot_num_ColNo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_UnloadRobot_ColumnNo.Name, (sender as NumUpDown).Value);
        }

        private void UnloadRobot_StartInit_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_UnloadRobot_StationStartInit.Name, true);
        }
        #endregion


        #region PCBA_Rack
        private void PCBARack_StationJogMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_PCBARack_JogMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void PCBARack_StationCycleMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_PCBARack_StationCycleMode.Name, (sender as ToggleButton).IsChecked);
        }       

        private void PCBARack_StartInit_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_PCBARack_StationStartInit.Name, true);
        }
        #endregion


        #region Battery_Rack
        private void BtryRack_StationJogMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRack_JogMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void BtryRack_StationCycleMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRack_StationCycleMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void BtryRack_StartInit_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_BtryRack_StationStartInit.Name, true);
        }
        #endregion


        #region Rotary_Table
        private void RotaryTable_TurretIndex_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_RotaryTable_TurretIndex.Name, true);
        }
        #endregion


        #region Input_Robot
        private void InputRobot_StationJogMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRobot_JogMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void InputRobot_StationCycleMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRobot_StationCycleMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void InputRobot_PickFromLeft_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRobot_PickFromLeft.Name, true);
        }

        private void InputRobot_PickFromRight_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRobot_PickFromRight.Name, true);
        }

        private void InputRobot_num_RowNo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRobot_RowNo.Name, (sender as NumUpDown).Value);
        }

        private void InputRobot_num_ColNo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRobot_ColumnNo.Name, (sender as NumUpDown).Value);
        }

        private void InputRobot_PlaceTurret_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRobot_PlaceToTurret.Name, true);
        }

        private void InputRobot_StartInit_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRobot_StationStartInit.Name, true);
        }
        #endregion


        #region Output_Robot
        private void OutputRobot_StationJogMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_OutputRobot_JogMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void OutputRobot_StationCycleMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_OutputRobot_StationCycleMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void OutputRobot_PickFromTurret_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_OutputRobot_PickFromTurret.Name, true);
        }

        private void OutputRobot_num_RowNo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_OutputRobot_RowNo.Name, (sender as NumUpDown).Value);
        }

        private void OutputRobot_num_ColNo_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_OutputRobot_ColumnNo.Name, (sender as NumUpDown).Value);
        }

        private void OutputRobot_PlaceToTray_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_OutputRobot_PlaceToTray.Name, true);
        }

        private void OutputRobot_StartInit_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_OutputRobot_StationStartInit.Name, true);
        }
        #endregion


        #region Input_Rack
        private void InputRack_StationJogMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRack_JogMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void InputRack_StationCycleMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRack_StationCycleMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void InputRack_StartInit_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_InputRack_StationStartInit.Name, true);
        }
        #endregion


        #region Output_Rack
        private void OutputRack_StationJogMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_OutputRack_JogMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void OutputRack_StationCycleMode_Control_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_OutputRack_StationCycleMode.Name, (sender as ToggleButton).IsChecked);
        }

        private void OutputRack_StartInit_Click(object sender, RoutedEventArgs e)
        {
            OPCore.Write(EngineeringPLCTags.Engr_OutputRack_StationStartInit.Name, true);
        }
        #endregion


        //private void Control_Click(object sender, RoutedEventArgs e)
        //{
        //    if (dic_EngMajor.TryGetValue(sender, out var model))
        //        OPCore.Write(model.PLCAddress, true);
        //}
        //private void TControl_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    if (dic_EngMajor.TryGetValue(sender, out var model))
        //        OPCore.Write(model.PLCAddress, (sender as ToggleButton).IsChecked);
        //}

        //private void NControl_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    if (dic_EngMajor.TryGetValue(sender, out var model))
        //        OPCore.Write(model.PLCAddress, (sender as NumUpDown).Value, typeof(int));
        //}

        private void ModuleSelection(object sender, SelectionChangedEventArgs e)
        {
            ModuleNow = cbx_Selected?.SelectedValue?.ToString() ?? "";

            if (ModuleNow != "System_01")
            {
                tbc_Sub.SelectedIndex = cbx_Selected.SelectedIndex;
                tbc_Sub.Visibility = Visibility.Visible;
                tbc_Main.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbc_Main.SelectedIndex = cbx_Selected.SelectedIndex;
                tbc_Main.Visibility = Visibility.Visible;
                tbc_Sub.Visibility = Visibility.Collapsed;

            }
        }
    }
}