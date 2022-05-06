using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Logix;

namespace PentagonHMI.ChildControls
{
    public partial class ucEngineering : UserControl, IDisposable
    {
        LogicClasses.Main _Main;
        private string Tag_EnMode = "MC_System_Tags.EngineeringMode";
        private string Tag_MachineRunning = "MC_System_Tags.MachineRunning";
        Brush Brush_Default;
        bool? PreEN = null;
        bool? PreRun = null;
        Button Dumb = new Button();
        Dictionary<string, string> Dic_Key_TagName = new Dictionary<string, string>
        {
            ["T_Reset"] = Info.OPC.Tags.Engineering.Taping_Reset_bool,
            ["T_Home"] = Info.OPC.Tags.Engineering.Taping_Home_bool,
            ["T_Cycle"] = Info.OPC.Tags.Engineering.Taping_Cycle_bool,
            ["T_Cut"] = Info.OPC.Tags.Engineering.Taping_Cut_bool,

            ["P_Reset"] = Info.OPC.Tags.Engineering.Program_Reset_bool,
            ["P_Home"] = Info.OPC.Tags.Engineering.Program_Home_bool,
            ["P_Cycle"] = Info.OPC.Tags.Engineering.Program_Cycle_bool,

            ["I_Reset"] = Info.OPC.Tags.Engineering.Index_Reset_bool,
            ["I_Home"] = Info.OPC.Tags.Engineering.Index_Home_bool,
            ["I_Cycle"] = Info.OPC.Tags.Engineering.Index_Cycle_bool,
            ["I_Left2Right"] = Info.OPC.Tags.Engineering.Index_Left2Right_bool,
            ["I_LeftGripper"] = Info.OPC.Tags.Engineering.Index_2LeftGripper_bool,
            ["I_RightGripper"] = Info.OPC.Tags.Engineering.Index_2RightGripper_bool,
            ["I_Correction"] = Info.OPC.Tags.Engineering.Index_Correction_bool,

            ["V_Reset"] = Info.OPC.Tags.Engineering.Vision_Reset_bool,
            ["V_Home"] = Info.OPC.Tags.Engineering.Vision_Home_bool,
            ["V_Cycle"] = Info.OPC.Tags.Engineering.Vision_Cycle_bool,
            ["V_FirstChipDetect"] = Info.OPC.Tags.Engineering.Vision_FirstChipDetect_bool,
            ["V_Calibrate"] = Info.OPC.Tags.Engineering.Vision_Calibrate_bool,
        };

        public ucEngineering(ref LogicClasses.Main main)
        {
            InitializeComponent();
            Brush_Default = btn_Index_2LeftGripper.Background;
            _Main = main;
            _Main.EN_OnUpdate += new LogicClasses.Main.onENUpdateHandler(Update);
        }

        private void Update()
        {
            bool EN = _Main.OPC.Read<bool>(Tag_EnMode);
            if (EN != PreEN)
            {
                PreEN = EN;
                Dispatcher.Invoke(() => tgb_EN.IsChecked = EN);
                Dispatcher.Invoke(() => EngineeringTabControl.IsEnabled = EN);
            }

            bool Run = _Main.OPC.Read<bool>(Tag_MachineRunning);
            if (Run != PreRun)
            {
                PreRun = Run;
                Dispatcher.Invoke(() => tgb_EN.IsEnabled = !Run);
            }


            foreach (Tag _tag in Info.OPC.TagGroups.Engineering.Tags)
            {
                Dispatcher.Invoke(() =>
                {
                    var item = typeof(Info.OPC.Tags.Engineering).GetFields().Where(x => x.GetValue(null).ToString() == _tag.Name).First();
                    Button btn = (FindName("btn_" + item.Name.Replace("_bool", string.Empty)) ?? Dumb) as Button;
                    btn.Background = Convert.ToBoolean(_tag?.Value ?? false) ? Brushes.SpringGreen : Brush_Default;
                });
            }
        }

        private void EM_MouseClick(object sender, RoutedEventArgs e)
        {
            ToggleButton tgb = sender as ToggleButton;
            if (tgb.IsChecked != null)
                _Main.OPC.Write(Tag_EnMode, tgb.IsChecked);
        }

        private void General_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string Key = btn.Tag?.ToString() ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(Key))
                _Main.OPC.Write(Dic_Key_TagName[Key], true);
        }

        public void Dispose()
        {
            _Main.EnPageOn = false;
            Info.OPC.TagGroups.Engineering.Active = false;
        }
    }
}
