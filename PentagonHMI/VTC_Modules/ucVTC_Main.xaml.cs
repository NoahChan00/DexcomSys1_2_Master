using System;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using Logix;
using GalaSoft.MvvmLight;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Wpf;
using System.Linq;
using Frm = System.Windows.Forms;

namespace PentagonHMI.VTC_Modules
{
    public partial class ucVTC_Main : UserControl, IDisposable
    {
        private LogicClasses.Main _Main;
        private const int Row = 15;
        private List<RackSlotModel> lst_RSM = new List<RackSlotModel>();
        private TagGroup Tgp_Conveyor = new TagGroup();
        private TagGroup Tgp_General = new TagGroup();
        private TagGroup Tgp_Rack = new TagGroup();
        private Dictionary<Context, Tuple<Tag, TextBlock>> Dic_Conveyor_Context_Tuple_Tag_Tbk = new Dictionary<Context, Tuple<Tag, TextBlock>>();
        private Dictionary<Context, Tuple<Tag, TextBlock>> Dic_Rack_Context_Tuple_Tag_Tbk = new Dictionary<Context, Tuple<Tag, TextBlock>>();
        private Dictionary<SlotStatus, Brush> Dic_Mode_Color = new Dictionary<SlotStatus, Brush>
        {
            [SlotStatus.Disable] = Brushes.DimGray,
            [SlotStatus.Empty] = Brushes.Yellow,
            [SlotStatus.Present] = Brushes.MediumSeaGreen,
            [SlotStatus.Working] = Brushes.DeepSkyBlue,
            [SlotStatus.Default] = Brushes.Snow,
        };

        private Dictionary<string, SlotStatus> Dic_Code_SlotStatus = new Dictionary<string, SlotStatus>
        {
            ["0"] = SlotStatus.Empty,
            ["1"] = SlotStatus.Present,
            ["2"] = SlotStatus.Disable,
        };

        private Dictionary<string, string> Dic_Code_Status = new Dictionary<string, string>
        {
            ["0"] = "Zone empty",
            ["1"] = "Tray pop up done",
            ["2"] = "Place all tray in progress",
            ["3"] = "Lifter move done",
            ["4"] = "Roller move out all tray",
            ["5"] = "Lifter pusher cylinder push all tray",
            ["6"] = "Lifter pusher motor push all tray",
            ["7"] = "Place all tray done",
            ["8"] = "Place Zone B tray in progress",
            ["9"] = "Lifter move done",
            ["10"] = "Roller move out Zone B tray",
            ["11"] = "Lifter pusher cylinder push Zone B tray",
            ["12"] = "Lifter pusher motor push Zone B tray",
            ["13"] = "Place Zone B tray done",
            ["14"] = "Place Zone A tray in progress",
            ["15"] = "Lifter move done",
            ["16"] = "Roller move out Zone A tray",
            ["17"] = "Lifter pusher cylinder push Zone A tray",
            ["18"] = "Lifter pusher motor push Zone A tray",
            ["19"] = "Place Zone A tray done",
        };

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<int, string> YFormatter { get; set; }
        enum Context
        {
            Loading, Turn, Buffer, LifterZone1, LifterZone2, RejectZone1, RejectZone2,
            TrayLifterStatus,
        }

        enum SlotStatus
        {
            Empty, Present, Disable, Working, Default
        }

        public ucVTC_Main(ref LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;
            Setup();
            _Main.OnAlwaysUpdate += new LogicClasses.Main.OnAlwaysUpdateHandler(Update);
            DataContext = this;
        }

        public void Setup()
        {
            try
            {
                SetupConveyorStatus();
                SetupRack();
                SetupUPHGraph();
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            };
        }

        private void SetupConveyorStatus()
        {
            Dic_Conveyor_Context_Tuple_Tag_Tbk = new Dictionary<Context, Tuple<Tag, TextBlock>>
            {
                //Conveyor Status
                [Context.Loading] = Tuple.Create<Tag, TextBlock>(new Tag { Name = "VTC_Z1A_Convy_AssetTag", MyObject = Context.Loading, DataType = Logix.Tag.ATOMIC.STRING }, tbk_Loading_AssetTag),
                [Context.Turn] = Tuple.Create<Tag, TextBlock>(new Tag { Name = "VTC_Z1B_Rotary_AssetTag", MyObject = Context.Turn, DataType = Logix.Tag.ATOMIC.STRING }, tbk_Turn_AssetTag),
                [Context.Buffer] = Tuple.Create<Tag, TextBlock>(new Tag { Name = "VTC_Z1C_Convy_AssetTag", MyObject = Context.Buffer, DataType = Logix.Tag.ATOMIC.STRING }, tbk_Buffer_AssetTag),
                [Context.LifterZone1] = Tuple.Create<Tag, TextBlock>(new Tag { Name = "VTC_LifterZoneA_Convy_AssetTag", MyObject = Context.LifterZone1, DataType = Logix.Tag.ATOMIC.STRING }, tbk_Zone1_AssetTag),
                [Context.LifterZone2] = Tuple.Create<Tag, TextBlock>(new Tag { Name = "VTC_LifterZoneB_Convy_AssetTag", MyObject = Context.LifterZone2, DataType = Logix.Tag.ATOMIC.STRING }, tbk_Zone2_AssetTag),
                [Context.RejectZone1] = Tuple.Create<Tag, TextBlock>(new Tag { Name = "VTC_Z1E_Convy_AssetTag", MyObject = Context.RejectZone1, DataType = Logix.Tag.ATOMIC.STRING }, tbk_Reject1_AssetTag),
                [Context.RejectZone2] = Tuple.Create<Tag, TextBlock>(new Tag { Name = "VTC_Z1D_Convy_AssetTag", MyObject = Context.RejectZone2, DataType = Logix.Tag.ATOMIC.STRING }, tbk_Reject2_AssetTag),

                //Tray Lifter Status
                [Context.TrayLifterStatus] = Tuple.Create<Tag, TextBlock>(new Tag { Name = "VTC_TrayLifter_Status", MyObject = Context.TrayLifterStatus, DataType = Logix.Tag.ATOMIC.DINT }, tbk_TrayLifterStatus),
            };

            foreach (var tuple_Tag_Tbk in Dic_Conveyor_Context_Tuple_Tag_Tbk.Values)
                Tgp_Conveyor.AddTag(tuple_Tag_Tbk.Item1);
        }

        Tag Tag_ZoneLeft_PlaceinProgress = new Tag { Name = "VTC_Rack_ZoneA_PlaceInProgress", DataType = Logix.Tag.ATOMIC.BOOL };
        Tag Tag_ZoneRight_PlaceinProgress = new Tag { Name = "VTC_Rack_ZoneB_PlaceInProgress", DataType = Logix.Tag.ATOMIC.BOOL };
        Tag Tag_ZoneLeft_Row = new Tag { Name = "VTC_Lifter_ZoneB_Current_PlaceNum", DataType = Logix.Tag.ATOMIC.BOOL };
        Tag Tag_ZoneRight_Row = new Tag { Name = "VTC_Lifter_ZoneB_Current_PlaceNum", DataType = Logix.Tag.ATOMIC.BOOL };
        string TagName_SlotStatus = "VTC_Rack_Matrix[{0},0]";
        string TagName_SlotAssetTag = "VTC_Rack_AssetTag_Matrix[{0},0]";

        private void SetupRack()
        {
            itmctrl_Legend.ItemsSource = new List<Legend>
            {
                new Legend{ Mode = "In Progress" ,  StatusColor = Dic_Mode_Color[SlotStatus.Working], FontColor = Brushes.Black},
                new Legend{ Mode = "Occupied" ,  StatusColor = Dic_Mode_Color[SlotStatus.Present], FontColor = Brushes.Black},
                new Legend{ Mode = "Empty" ,  StatusColor = Dic_Mode_Color[SlotStatus.Empty], FontColor = Brushes.Black},
                new Legend{ Mode = "Disabled" ,  StatusColor = Dic_Mode_Color[SlotStatus.Disable], FontColor = Brushes.White},
            };

            Tgp_General.AddTag(Tag_ZoneLeft_PlaceinProgress, Tag_ZoneLeft_Row, Tag_ZoneRight_PlaceinProgress, Tag_ZoneRight_Row);
            for (int n = 1; n <= 2; n++)
            {
                Tgp_Rack.AddTag(new Tag { Name = string.Format(TagName_SlotAssetTag, n.ToString()), DataType = Logix.Tag.ATOMIC.STRING, Length = 16 });
                Tgp_Rack.AddTag(new Tag { Name = string.Format(TagName_SlotStatus, n.ToString()), DataType = Logix.Tag.ATOMIC.DINT, Length = 16 });
            }

            for (int r = Row; r > 0; r--)
            {
                Brush Color = Dic_Mode_Color[SlotStatus.Empty];
                string Text = "ASSET TAG";
                lst_RSM.Add(new RackSlotModel
                {
                    SlotIndex = "L" + r.ToString(),
                    SlotIdexBackground = Color,
                    SlotStatusText = Text,
                    SlotStatusBackground = Color
                });

                lst_RSM.Add(new RackSlotModel
                {
                    SlotIndex = "R" + r.ToString(),
                    SlotIdexBackground = Color,
                    SlotStatusText = Text,
                    SlotStatusBackground = Color
                });
            }
            itmctrl_RackLayout.ItemsSource = lst_RSM;
        }

        private void SetupUPHGraph()
        {
            SeriesCollection = new SeriesCollection();

            string[] ary_str = new string[24];
            for (int n = 1; n < 24; n++)
                ary_str[n] = $"-{n.ToString()}hrs";
            ary_str[0] = "Now";
            Labels = ary_str;

            YFormatter = value => value.ToString();

            ChartValues<int> cv = new ChartValues<int>();
            for (int n = 1; n <= 24; n++) cv.Add(0);
            SeriesCollection.Add(new LineSeries
            {
                Values = cv,
                LineSmoothness = 0,
            });
        }


        private void Update()
        {
            try
            {
                Dispatcher.Invoke(() => UpdateUI());
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            };
        }

        private void UpdateUI()
        {
            _Main.MyPLC.GroupRead(Tgp_General, Tgp_Rack, Tgp_Conveyor);

            UpdateConveyor();
            UpdateRack();
            UpdateUPH();
        }

        private void UpdateUPH()
        {
            int HrsNow = DateTime.Now.Hour;
            var lst_TodayUPH = _Main.Lst_UPH.Take(HrsNow);
            var lst_YtdUPH = _Main.Lst_UPH.Skip(HrsNow);

            int count = 0;
            foreach (string uph in lst_TodayUPH.Reverse())
                SeriesCollection[0].Values[count++] = Convert.ToInt32(uph);

            foreach (string uph in lst_YtdUPH.Reverse())
                SeriesCollection[0].Values[count++] = Convert.ToInt32(uph);
        }

        private void UpdateRack()
        {
            bool isLeftOn = Tag_ZoneLeft_PlaceinProgress.GetValue<bool>();
            bool isRightOn = Tag_ZoneRight_PlaceinProgress.GetValue<bool>();
            int Row = Tag_ZoneLeft_Row.GetValue<int>();

            Int16[] Ary_LSValue = null, Ary_RSValue = null;
            string[] Ary_LAValue = null, Ary_RAValue = null;
            foreach (Tag tag in Tgp_Rack.Tags)
            {
                if (tag.Name == string.Format(TagName_SlotStatus, "1"))
                    Ary_LSValue = (Int16[])tag.Value;
                else if (tag.Name == string.Format(TagName_SlotStatus, "2"))
                    Ary_RSValue = (Int16[])tag.Value;
                else if (tag.Name == string.Format(TagName_SlotAssetTag, "1"))
                    Ary_LAValue = (string[])tag.Value;
                else if (tag.Name == string.Format(TagName_SlotAssetTag, "2"))
                    Ary_RAValue = (string[])tag.Value;
            }

            foreach (var item in lst_RSM)
            {
                int itemRow = Convert.ToInt32(item.SlotIndex.Remove(0, 1));
                int itemCol = item.SlotIndex.Remove(1).ToString() == "L" ? 1 : 2;

                Dispatcher.Invoke(() => item.SlotIdexBackground = itemRow == Row && ((isLeftOn && itemCol == 1) || (isRightOn && itemCol == 2)) ?
                Dic_Mode_Color[SlotStatus.Working] : Dic_Mode_Color[SlotStatus.Default]);
                Dispatcher.Invoke(() => item.SlotStatusBackground = Dic_Mode_Color[Dic_Code_SlotStatus[(itemCol == 1 ?
                Ary_LSValue[itemRow] : Ary_RSValue[itemRow]).ToString()]]);
                Dispatcher.Invoke(() => item.SlotStatusText = itemCol == 1 ?
                Ary_LAValue[itemRow] : Ary_RAValue[itemRow]);
            }
        }

        private void UpdateConveyor()
        {
            foreach (Tag tag in Tgp_Conveyor.Tags)
                Dispatcher.Invoke(() => HiddenTextblockUIUpdate(Dic_Conveyor_Context_Tuple_Tag_Tbk[(Context)tag.MyObject].Item2, tag.GetValue<string>()));
        }

        private void HiddenTextblockUIUpdate(TextBlock tbk, string Value)
        {
            if (tbk.Text != Value)
            {
                if (tbk == tbk_TrayLifterStatus)
                {
                    tbk.Text = Dic_Code_Status[Value];
                }
                else
                {
                    tbk.Text = Value;
                    tbk.Visibility = string.IsNullOrWhiteSpace(Value) ? Visibility.Hidden : Visibility.Visible;
                }
            }
        }

        public void Dispose()
        {

        }

        private void lbl_Disable_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (_Main.UserAccessLevel.ToUpper() == "OPERATOR") return;

            Grid grd = sender as Grid;
            string Code = grd.Tag.ToString();
            bool IsLeft = Code.Substring(0, 1) == "L" ? true : false;
            int Row = Convert.ToInt32(Code.Substring(1));

            int Statusindex = _Main.OPC.Read<int>($"VTC_Rack_Matrix[{(IsLeft ? 1 : 2)},{Row}]");
            if (Statusindex == 1) return;
            bool isEnable = Statusindex == 2;

            if (Frm.DialogResult.Yes == Frm.MessageBox.Show((isEnable ? "Enable Slot: " : "Disable Slot: ") + (IsLeft ? "L" : "R") + Row, "Info", Frm.MessageBoxButtons.YesNo))
            {
                _Main.OPC.Write(IsLeft ? "HMI_VTC_Disable_RackZone_Left" : "HMI_VTC_Disable_RackZone_Right", true);
                _Main.OPC.Read<int>("HMI_VTC_Disable_RackSlot");
                _Main.OPC.Write("HMI_VTC_Disable_RackSlot", Row, typeof(int));
                _Main.OPC.Write(isEnable ? "HMI_VTC_RackSlot_Enable" : "HMI_VTC_RackSlot_Disable", true);
            }
        }

        private void btn_AbortRack_Click(object sender, RoutedEventArgs e)
        {
            if (_Main.UserAccessLevel.ToUpper() == "OPERATOR") return;

            if (Frm.DialogResult.Yes == Frm.MessageBox.Show("Proceed to Abort Rack", "Confirmation", Frm.MessageBoxButtons.YesNo))
                _Main.OPC.Write("HMI_VTC_Abort_Rack", true);
        }
    }

    public static class Extension
    {
        public static string ToTagString(this Tag _tag)
        {
            return _tag?.Value?.ToString() ?? string.Empty;
        }
        public static bool ToTagBool(this Tag _tag)
        {
            return Convert.ToBoolean(_tag?.Value ?? "False");
        }
    }

    public class Legend
    {
        public string Mode { get; set; }
        public Brush FontColor { get; set; }
        public Brush StatusColor { get; set; }
    }

    public class RackSlotModel : ViewModelBase
    {
        private string _SlotIndex = string.Empty;
        public string SlotIndex
        {
            get { return _SlotIndex; }
            set
            {
                if (_SlotIndex != value)
                {
                    _SlotIndex = value;
                    RaisePropertyChanged(nameof(SlotIndex));
                }
            }
        }

        private Brush _SlotIdexBackground = Brushes.White;
        public Brush SlotIdexBackground
        {
            get { return _SlotIdexBackground; }
            set
            {
                if (_SlotIdexBackground != value)
                {
                    _SlotIdexBackground = value;
                    RaisePropertyChanged(nameof(SlotIdexBackground));
                }
            }
        }

        private string _SlotStatusText = string.Empty;
        public string SlotStatusText
        {
            get { return _SlotStatusText; }
            set
            {
                if (_SlotStatusText != value)
                {
                    _SlotStatusText = value;
                    RaisePropertyChanged(nameof(SlotStatusText));
                }
            }
        }

        private Brush _SlotStatusBackground = Brushes.White;
        public Brush SlotStatusBackground
        {
            get { return _SlotStatusBackground; }
            set
            {
                if (_SlotStatusBackground != value)
                {
                    _SlotStatusBackground = value;
                    RaisePropertyChanged(nameof(SlotStatusBackground));
                }
            }
        }
    }

    static class Extensions
    {
        public static T GetValue<T>(this Tag item)
        {
            return (T)Convert.ChangeType(item.Value?.ToString() ?? default, typeof(T));
        }
    }

}
