using Logix;
using PentagonHMI.Classes;
using PentagonHMI.Modules.NumUpDown;
using PentagonHMI.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Utilities;

namespace PentagonHMI
{
    public partial class ucDexcomEngineering : UserControl, IDisposable
    {
        private SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc(Info.OPC.IP);

        //SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName);
        private LogicClasses.Main _Main;

        private struct Control
        {
            public string[] Conditions;
            public string[] AccessGroups;
            public string PLCAddress;
            public UIElement DisplayPanel;
            public string Module;
        }

        private Dictionary<object, Control> dic_EngMajor;
        private string ModuleNow = "";
        private List<Tag> engr_tagList = new List<Tag>();

        public ucDexcomEngineering(LogicClasses.Main main)
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
            CBXBatteryRobotBatteryType.Items.Add(new BatteryType("Maxell", 2));
            CBXBatteryRobotBatteryType.Items.Add(new BatteryType("Panasonic", 5));
            CBXBatteryRobotBatteryType.Items.Add(new BatteryType("Murata", 6));

            dic_EngMajor = new Dictionary<object, Control>();
            ModuleSelection();

            //PCBA Robot
            dic_EngMajor.Add(InputRobot_tg_StationJogMode, new Control
            {
                AccessGroups = new string[] { "technician", "engineer" },
                Conditions = new string[] { "" },
                DisplayPanel = InputRobot_tg_StationJogMode,
                Module = GlobalFunctions.IsSystem1 ? "System_01" : "System_02",
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

                        if (control.Module != ModuleNow)
                            continue;

                        bool disable = false;

#if !DEBUG
                        foreach (var condition in control.Conditions)
                            if (!OPCore.Read<bool>(condition))
                                disable = true;
#endif

                        disable = disable || !control.AccessGroups.Contains(_Main.UserAccessLevel.ToLower());

                        control.DisplayPanel.IsEnabled = !disable;

                        if (UI is ToggleButton)
                        {
                            var tbtn = UI as ToggleButton;
                            tbtn.IsChecked = OPCore.Read<bool>(control.PLCAddress);
                        }
                    }

                    // Read require plc 
                    if (GlobalFunctions.IsSystem1)
                    {
                        System.Windows.Controls.Control[] controls = new System.Windows.Controls.Control[]
                        {
                            BtryRack_btn_StartInit, BtryRack_tg_StationCycleMode, BtryRack_tg_StationInitDone, BtryRack_tg_StationJogMode, BtryRobot_btn_PickTray,
                            BtryRobot_btn_PlaceTurret, BtryRobot_btn_StartInit, BtryRobot_num_PickTray_Col, BtryRobot_num_PickTray_Row, BtryRobot_tg_StationCycleMode,
                            BtryRobot_tg_StationInitDone, BtryRobot_tg_StationJogMode, PCBARack_btn_StartInit, PCBARack_tg_StationCycleMode, PCBARack_tg_StationInitDone,
                            PCBARack_tg_StationJogMode, PCBARobotPickTray, PCBARobotPickTrayRow, PCBARobotStationCycleMode, PCBARobotStationJogMode, PCBARobot_btn_PlaceTurret,
                            PCBARobot_btn_StartInit, PCBARobot_num_PickTray_Col, PCBARobot_tg_StationInitDone, S1_RotaryTable_btn_TurretIndex, UnloadRobot_btn_PickTurret,
                            UnloadRobot_btn_PlaceLeft, UnloadRobot_btn_PlaceRight, UnloadRobot_btn_StartInit, UnloadRobot_num_PickTray_Col,
                            UnloadRobot_num_PickTray_Row, UnloadRobot_tg_StationCycleMode, UnloadRobot_tg_StationInitDone, UnloadRobot_tg_StationJogMode
                        };

                        foreach(var c in controls) 
                        {
                            ReadPlcToControl(c);
                        }
                    }
                    else
                    {
                        System.Windows.Controls.Control[] controls = new System.Windows.Controls.Control[] 
                        { 
                            InputRack_btn_StartInit, InputRack_tg_StationCycleMode, InputRack_tg_StationInitDone, InputRack_tg_StationJogMode, InputRobot_btn_PickFromLeft,
                            InputRobot_btn_PickFromRight, InputRobot_btn_PlaceTurret, InputRobot_btn_StartInit, InputRobot_num_ColNo, InputRobot_num_RowNo,
                            InputRobot_tg_StationInitDone, InputRobot_tg_StationJogMode, InputRobot_tg_StationJogMode, OutputRack_btn_StartInit, OutputRack_tg_StationCycleMode,
                            OutputRack_tg_StationInitDone, OutputRack_tg_StationJogMode, OutputRobot_btn_PickFromTurret, OutputRobot_btn_PlaceToTray, OutputRobot_btn_StartInit,
                            OutputRobot_num_PickTray_Col, OutputRobot_num_PickTray_Row, OutputRobot_tg_StationCycleMode, OutputRobot_tg_StationInitDone, OutputRobot_tg_StationJogMode,
                            S2_RotaryTable_btn_TurretIndex
                        };

                        foreach(var c in controls) 
                        {
                            ReadPlcToControl(c);
                        }
                    }


                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            });
        }

        ~ucDexcomEngineering()
        {
            Dispose();
        }

        public void Dispose()
        {
            _Main.EngineeringPageOn = false;
        }

        private void ModuleSelection()
        {
            if (GlobalFunctions.IsSystem1)
            {
                tbc_System1.Visibility = Visibility.Visible;
                tbc_System2.Visibility = Visibility.Collapsed;
            }
            else
            {
                tbc_System1.Visibility = Visibility.Collapsed;
                tbc_System2.Visibility = Visibility.Visible;
            }
        }

        private void NumUpDownValueChangedHandler(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is NumUpDown numUpDown && numUpDown.Tag is string tagName)
                {
                    OPCore.Write(tagName, numUpDown.Value, typeof(short));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ToogleButtonHandler(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is ToggleButton toggleButton && toggleButton.Tag is string tagName)
                {
                    OPCore.Write(tagName, toggleButton.IsChecked, typeof(bool));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ButtonHandler(object sender, RoutedEventArgs e)
        {
            try
            {
                if (sender is Button button && button.Tag is string tagName)
                {
                    OPCore.Write(tagName, true, typeof(bool));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private class BatteryType
        {
            public string Text
            {
                get; set;
            }

            public int Value
            {
                get; set;
            }

            public BatteryType(string text, int value)
            {
                Text = text;
                Value = value;
            }

            public override string ToString()
            {
                return Text;
            }
        }

        private void BatteryTypeComboBoxHandler(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (sender is ComboBox comboBox && comboBox.Tag is string tagName && comboBox.SelectedItem is BatteryType batteryType)
                {
                    OPCore.Write(tagName, batteryType.Value, typeof(int));
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void ReadPlcToControl(System.Windows.Controls.Control control)
        {
            try
            {
                if (control.Tag is string tag)
                {
                    if (control is ToggleButton tb)
                    {
                        tb.IsChecked = OPCore.Read<bool>(tag);
                    }
                    else if (control is NumUpDown nud)
                    {
                        nud.TextBoxValue.Text = OPCore.Read<short>(tag).ToString();
                    }
                    else if (control is Button b)
                    {
                        b.IsEnabled = !OPCore.Read<bool>(tag);
                        b.Opacity = b.IsEnabled ? 1 : 0.5;
                        b.Background = b.IsEnabled ? System.Windows.Media.Brushes.LimeGreen : System.Windows.Media.Brushes.Gray;
                    }
                }
            }
            catch { throw; }
        }
    }
}
