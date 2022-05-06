using System;
using System.Windows.Controls;
using Utilities;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using PentagonHMI.Modules.NumUpDown;
using System.Linq;

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
                "Left Rack","Right Rack",
            };
            cbx_Selected.ItemsSource = ModuleNames;

            dic_EngMajor = new Dictionary<object, Control>();

            //Left Rack
            dic_EngMajor.Add(LR_tg_StationJogMode, new Control
            {
                AccessGroups = new string[] { "technician", "egineer" },
                Conditions = new string[] { "" },
                DisplayPanel = LR_tg_StationJogMode,
                Module = ModuleNames[0],
                PLCAddress = "HMI_StationJogMode.LeftRack",
            });
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

        private void Control_Click(object sender, RoutedEventArgs e)
        {
            if (dic_EngMajor.TryGetValue(sender, out var model))
                OPCore.Write(model.PLCAddress, true);
        }
        private void TControl_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dic_EngMajor.TryGetValue(sender, out var model))
                OPCore.Write(model.PLCAddress, (sender as ToggleButton).IsChecked);
        }

        private void NControl_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(dic_EngMajor.TryGetValue(sender,out var model))
                OPCore.Write(model.PLCAddress, (sender as NumUpDown).Value, typeof(int));
        }

        private void ModuleSeletion(object sender, SelectionChangedEventArgs e)
        {
            ModuleNow = cbx_Selected?.SelectedValue?.ToString() ?? "";
            tbc_Main.SelectedIndex = cbx_Selected.SelectedIndex;
        }

    }
}
