using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using GalaSoft.MvvmLight;
using System.Linq;
using System;
using PentagonHMI.Classes;
using Logix;

namespace PentagonHMI.UserControls
{
    public partial class ucSettingBlock : UserControl
    {
        public LogicClasses.Main _Main;
        public List<SettingModel> SettingList = new List<SettingModel>();
        public bool PendingUpdate = false;

        public ucSettingBlock()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetControl()
        {
            Toggle_ItemsControl.ItemsSource = SettingList;
        }

        private void Toggle_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton Tgbtn = sender as ToggleButton;
            string Address = Tgbtn.Tag.ToString();
            bool Value = Tgbtn.IsChecked ?? false;
            _Main.OPC.Write(Address, Value);

            if (ProjectType.ARCADIA == GlobalFunctions.ProjectType)
            {
                string Tag_JogMode = "HMI_JogMode.Jog{0}";
                if (Address.Contains(string.Format(Tag_JogMode, string.Empty)) && Value)
                {
                    string N = Address.Replace(string.Format(Tag_JogMode, string.Empty), string.Empty);
                    for (int n = 1; n <= 5; n++)
                        if (N != n.ToString())
                            _Main.OPC.Write(string.Format(Tag_JogMode, n), false);
                }
            }
            PendingUpdate = true;
        }

        private void Num_Button_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = sender as Button;
            string Address = Btn.Tag.ToString();

            var item = SettingList.Where(x => x.Config.Where(y => y.Key == Address).Count() > 0).First().Config.Where(z => z.Key == Address).First();
            if (item != null)
            {
                _Main.OPC.Read(Address, item.DataType);
                _Main.OPC.Write(Address, item.Num_Value, item.DataType);
            }
            PendingUpdate = true;
        }

        private void actionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                SettingBlockModel settingBlockModel = button.Tag as SettingBlockModel;
                
                if (MessageBox.Show($"Are you sure you want to set {settingBlockModel.Title}?",
                    nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                {
                    _Main.OPC.Write(settingBlockModel.Key, true);
                }
            }
            catch (Exception exception)
            {
                Utilities.FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        public class SettingModel : ViewModelBase
        {
            private string _MainTitle = "NA";
            public string MainTitle
            {
                get { return _MainTitle; }
                set
                {
                    if (_MainTitle != value)
                    {
                        _MainTitle = value;
                        RaisePropertyChanged(nameof(MainTitle));
                    }
                }
            }

            private List<SettingBlockModel> _Config = new List<SettingBlockModel>();
            public List<SettingBlockModel> Config
            {
                get { return _Config; }
                set
                {
                    if (_Config != value)
                    {
                        _Config = value;
                        RaisePropertyChanged(nameof(Config));
                    }
                }
            }
        }

        public class SettingBlockModel : ViewModelBase
        {
            private string _Title = "NA";
            public string Title
            {
                get { return _Title; }
                set
                {
                    if (_Title != value)
                    {
                        _Title = value;
                        RaisePropertyChanged(nameof(Title));
                    }
                }
            }

            private Visibility _Vis_isNum = Visibility.Collapsed;
            public Visibility Vis_isNum
            {
                get { return _Vis_isNum; }
                set
                {
                    if (_Vis_isNum != value)
                    {
                        _Vis_isNum = value;
                        RaisePropertyChanged(nameof(Vis_isNum));
                    }
                }
            }

            private Visibility _Vis_isToggle = Visibility.Collapsed;
            public Visibility Vis_isToggle
            {
                get { return _Vis_isToggle; }
                set
                {
                    if (_Vis_isToggle != value)
                    {
                        _Vis_isToggle = value;
                        RaisePropertyChanged(nameof(Vis_isToggle));
                    }
                }
            }

            private Visibility _actionVisibility = Visibility.Collapsed;
            public Visibility ActionVisibility
            {
                get { return _actionVisibility; }
                set
                {
                    if (_actionVisibility != value)
                    {
                        _actionVisibility = value;
                        RaisePropertyChanged(nameof(ActionVisibility));
                    }
                }
            }

            private Visibility _Vis_Access = Visibility.Visible;
            public Visibility Vis_Access
            {
                get { return _Vis_Access; }
                set
                {
                    if (_Vis_Access != value)
                    {
                        _Vis_Access = value;
                        RaisePropertyChanged(nameof(Vis_Access));
                    }
                }
            }

            private int _Num_Value = 0;
            public int Num_Value
            {
                get { return _Num_Value; }
                set
                {
                    if (_Num_Value != value)
                    {
                        _Num_Value = value;
                        RaisePropertyChanged(nameof(Num_Value));
                    }
                }
            }

            private int _Num_Max = 0;
            public int Num_Max
            {
                get { return _Num_Max; }
                set
                {
                    if (_Num_Max != value)
                    {
                        _Num_Max = value;
                        RaisePropertyChanged(nameof(Num_Max));
                    }
                }
            }

            private int _Num_Min = 0;
            public int Num_Min
            {
                get { return _Num_Min; }
                set
                {
                    if (_Num_Min != value)
                    {
                        _Num_Min = value;
                        RaisePropertyChanged(nameof(Num_Min));
                    }
                }
            }

            private string _Key = string.Empty;
            public string Key
            {
                get { return _Key; }
                set
                {
                    if (_Key != value)
                    {
                        _Key = value;
                        RaisePropertyChanged(nameof(Key));
                    }
                }
            }


            //private string _Num_Key = string.Empty;
            //public string Num_Key
            //{
            //    get { return _Num_Key; }
            //    set
            //    {
            //        if (_Num_Key != value)
            //        {
            //            _Num_Key = value;
            //            RaisePropertyChanged(nameof(Num_Key));
            //        }
            //    }
            //}

            //private string _Tg_Key = string.Empty;
            //public string Tg_Key
            //{
            //    get { return _Tg_Key; }
            //    set
            //    {
            //        if (_Tg_Key != value)
            //        {
            //            _Tg_Key = value;
            //            RaisePropertyChanged(nameof(Tg_Key));
            //        }
            //    }
            //}

            private bool _Tg_Stat = false;
            public bool Tg_Stat
            {
                get { return _Tg_Stat; }
                set
                {
                    if (_Tg_Stat != value)
                    {
                        _Tg_Stat = value;
                        RaisePropertyChanged(nameof(Tg_Stat));
                    }
                }
            }

            public Type DataType { get; set; } = typeof(bool);

            public Tag Tag = new Tag();

        }
    }
}
