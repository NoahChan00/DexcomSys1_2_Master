using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Utilities;

namespace PentagonHMI.ChildControls
{
    public partial class ucDryrun : UserControl, IDisposable
    {
        private SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc(Info.OPC.IP);
        private DateTime DT = DateTime.Now.AddMinutes(-5);

        //20 Set
        private const string Tag_Dryrun_Enable = "Dry_Run_Data_{0}.Enable_DryRun";

        private const string Tag_Name = "Dry_Run_Data_{0}.Name";
        private const string Tag_Start = "Dry_Run_Data_{0}.Start";
        private const string Tag_Reset = "Dry_Run_Data_{0}.Reset";
        private const string Tag_Soft_Jam_Count = "Dry_Run_Data_{0}.Soft_Jam_Count";
        private const string Tag_Hard_Jam_Count = "Dry_Run_Data_{0}.Hard_Jam_Count";
        private const string Tag_Operation_Time_Sec = "Dry_Run_Data_{0}.Operation_Time_sec";
        private const string Tag_Idling_Time_Sec = "Dry_Run_Data_{0}.Idling_Time_sec";
        private const string Tag_Down_Time_Sec = "Dry_Run_Data_{0}.Down_Time_sec";
        private const string Tag_MTBA_Sec = "Dry_Run_Data_{0}.MTBA_sec";
        private const string Tag_MTBF_Sec = "Dry_Run_Data_{0}.MTBF_sec";
        private Dictionary<string, int> Dic_Name_Indexfrom1 = new Dictionary<string, int>();
        private Dictionary<string, int> tempDryRunList = new Dictionary<string, int>();

        private enum ChartSeq
        { Run, Idle, Down };

        private const int MaxDryRunMode = 20;

        private LogicClasses.Main _Main;

        private bool ResetUpdate = false;

        public ucDryrun(ref LogicClasses.Main main)
        {
            InitializeComponent();
            try
            {
                _Main = main;

                if(Classes.GlobalFunctions.ProjectType != ProjectType.HDD)
                    lbl_NoMaterialNote.Visibility = Visibility.Collapsed;

                SeriesCollections = new SeriesCollection
                    {
                        new StackedAreaSeries
                        {
                            Title = ChartSeq.Run.ToString(),
                            Values = new ChartValues<int>{},//0,0,0,0,0,/*5*/0,0,0,0,0,/*5*/0,0,0,0,0,/*5*/0,0,0,0,0,/*5*/ },
                            LineSmoothness = 1,
                            Fill = Brushes.Teal
                        },

                        new StackedAreaSeries
                        {
                            Title = ChartSeq.Idle.ToString(),
                            Values = new ChartValues<int>{},//0,0,0,0,0,/*5*/0,0,0,0,0,/*5*/0,0,0,0,0,/*5*/0,0,0,0,0,/*5*/ },
                            LineSmoothness = 1,
                            Fill = Brushes.LightBlue
                        },

                        new StackedAreaSeries
                        {
                            Title = ChartSeq.Down.ToString(),
                            Values = new ChartValues<int>{},//0,0,0,0,0,/*5*/0,0,0,0,0,/*5*/0,0,0,0,0,/*5*/0,0,0,0,0,/*5*/},
                            LineSmoothness = 1,
                            Fill = Brushes.OrangeRed
                        },
                    };

                OPCore.Connect(Info.OPC.IP);
                YFormatter = val => val.ToString() + "Min";
                DataContext = this;
                _Main.OnDryrunUpdate += new LogicClasses.Main.onDryrunHandler(DryRunSequence);
            }
            catch(Exception ex) { MessageBox.Show($"{MethodBase.GetCurrentMethod().Name}:{ex.Message}"); }
        }

        private void DryRunSequence()
        {
            try
            {
                tempDryRunList.Clear();

                for(int i = 1; i <= MaxDryRunMode; i++)
                {
                    if(OPCore.Read<bool>(FORM(Tag_Dryrun_Enable, i)))
                    {
                        string name = OPCore.Read<string>(FORM(Tag_Name, i));
                        if(!string.IsNullOrEmpty(name))
                        {
                            if(!tempDryRunList.ContainsKey(name))
                            {
                                tempDryRunList.Add(name, i);
                            }
                        }
                    }
                }

                if(!tempDryRunList.Select(x => x.Key).SequenceEqual(Dic_Name_Indexfrom1.Select(x => x.Key)))
                {
                    Dic_Name_Indexfrom1.Clear();
                    foreach(var item in tempDryRunList)
                        Dic_Name_Indexfrom1.Add(item.Key, item.Value);

                    cbx_DryRunMode.Dispatcher.Invoke(() => cbx_DryRunMode.ItemsSource = null);
                    cbx_DryRunMode.Dispatcher.Invoke(() => cbx_DryRunMode.ItemsSource = Dic_Name_Indexfrom1.Keys);
                    cbx_DryRunMode.Dispatcher.Invoke(() => cbx_DryRunMode.SelectedIndex = 0);
                }

                if(cbx_DryRunMode.SelectedItem != null)
                {
                    int selectedIndex = -1;
                    Dispatcher.Invoke(() => Dic_Name_Indexfrom1.TryGetValue(cbx_DryRunMode.SelectedItem.ToString(), out selectedIndex));
                    if(selectedIndex > 0)
                    {
                        lbl_SoftJam.Dispatcher.Invoke(() => lbl_SoftJam.Content = OPCore.Read<Int16>(FORM(Tag_Soft_Jam_Count, selectedIndex)));
                        lbl_HardJam.Dispatcher.Invoke(() => lbl_HardJam.Content = OPCore.Read<Int16>(FORM(Tag_Hard_Jam_Count, selectedIndex)));

                        int run = OPCore.Read<Int32>(FORM(Tag_Operation_Time_Sec, selectedIndex));
                        int idle = OPCore.Read<Int32>(FORM(Tag_Idling_Time_Sec, selectedIndex));
                        int down = OPCore.Read<Int32>(FORM(Tag_Down_Time_Sec, selectedIndex));

                        lbl_Total.Dispatcher.Invoke(() => lbl_Total.Content = TimeFormat(run + idle + down));
                        lbl_Run.Dispatcher.Invoke(() => lbl_Run.Content = TimeFormat(run));
                        lbl_Idle.Dispatcher.Invoke(() => lbl_Idle.Content = TimeFormat(idle));
                        lbl_Down.Dispatcher.Invoke(() => lbl_Down.Content = TimeFormat(down));

                        lbl_MTBA.Dispatcher.Invoke(() => lbl_MTBA.Content = TimeFormat(OPCore.Read<Int32>(FORM(Tag_MTBA_Sec, selectedIndex))));
                        lbl_MTBF.Dispatcher.Invoke(() => lbl_MTBF.Content = TimeFormat(OPCore.Read<Int32>(FORM(Tag_MTBF_Sec, selectedIndex))));

                        if(DateTime.Now > DT)
                        {
                            DT = DateTime.Now.AddMinutes(5);

                            SeriesCollections[(int)ChartSeq.Run].Values.Add(GetMinutes(run));
                            SeriesCollections[(int)ChartSeq.Idle].Values.Add(GetMinutes(idle));
                            SeriesCollections[(int)ChartSeq.Down].Values.Add(GetMinutes(down));
                        }
                    }
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private string TimeFormat(int Sec)
        {
            TimeSpan Ts = TimeSpan.FromSeconds(Sec);
            int DayHours = Ts.Days * 24;
            return string.Format("{0:D2}h:{1:D2}m:{2:D2}s", Ts.Hours + DayHours, Ts.Minutes, Ts.Seconds);
        }

        private int GetMinutes(int Sec)
        {
            return Convert.ToInt32(TimeSpan.FromSeconds(Sec).TotalMinutes);
        }

        private string FORM(string OriginalStr, int Index)
        {
            return string.Format(OriginalStr, Index.ToString());
        }

        public SeriesCollection SeriesCollections { get; set; }
        private Func<double, string> _yFormatter;

        public Func<double, string> YFormatter
        {
            get { return _yFormatter; }
            set
            {
                _yFormatter = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if(PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Index = -1;
                this.Dispatcher.Invoke(() => Dic_Name_Indexfrom1.TryGetValue(cbx_DryRunMode.SelectedValue?.ToString() ?? string.Empty, out Index));
                if(Index > 0)
                {
                    if(OPCore.Read<bool>(FORM(Tag_Dryrun_Enable, Index)) && OPCore.Write(FORM(Tag_Start, Index), true))
                    {
                        cbx_DryRunMode.IsEnabled = false;
                        btn_Start.IsEnabled = false;
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show($"{MethodBase.GetCurrentMethod().Name}:{ex.Message}"); }
        }

        private void Btn_Stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int Index = -1;
                this.Dispatcher.Invoke(() => Dic_Name_Indexfrom1.TryGetValue(cbx_DryRunMode.SelectedValue?.ToString() ?? string.Empty, out Index));
                if(Index > 0)
                {
                    if(OPCore.Write(FORM(Tag_Start, Index), false))
                    {
                        cbx_DryRunMode.IsEnabled = true;
                        btn_Start.IsEnabled = true;
                    }
                }
            }
            catch(Exception ex) { MessageBox.Show($"{MethodBase.GetCurrentMethod().Name}:{ex.Message}"); }
        }

        private void Btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(MessageBox.Show("Are you sure you want to Reset Current Dryrun Record?",
                    nameof(MessageBoxImage.Question), MessageBoxButton.YesNo,
                    MessageBoxImage.Question, MessageBoxResult.No,
                    MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                {
                    int Index = -1;
                    this.Dispatcher.Invoke(() => Dic_Name_Indexfrom1.TryGetValue(cbx_DryRunMode.SelectedValue?.ToString() ?? string.Empty, out Index));

                    if(Index > 0)
                    {
                        //Log
                        FileLogger.DryRun(
                        RunTime: OPCore.Read<Int32>(FORM(Tag_Operation_Time_Sec, Index)).ToString(),
                        IdleTime: OPCore.Read<Int32>(FORM(Tag_Idling_Time_Sec, Index)).ToString(),
                        DownTime: OPCore.Read<Int32>(FORM(Tag_Down_Time_Sec, Index)).ToString(),
                        MTBA: OPCore.Read<Int32>(FORM(Tag_MTBA_Sec, Index)).ToString(),
                        MTBF: OPCore.Read<Int32>(FORM(Tag_MTBF_Sec, Index)).ToString(),
                        SoftJam: OPCore.Read<Int16>(FORM(Tag_Soft_Jam_Count, Index)).ToString(),
                        HardJam: OPCore.Read<Int16>(FORM(Tag_Hard_Jam_Count, Index)).ToString());

                        OPCore.Write(FORM(Tag_Reset, Index), true);
                    }
                    ResetUpdate = true;
                }
            }
            catch(Exception ex) { MessageBox.Show($"{MethodBase.GetCurrentMethod().Name}:{ex.Message}"); }
        }

        ~ucDryrun()
        {
            Dispose();
        }

        private void Demo_Click(object sender, RoutedEventArgs e)
        {
            Task.Run(new Action(() => Fake()));
        }

        private void Fake()
        {
            try
            {
                int num = 0;
                Dic_Name_Indexfrom1.Clear();
                Dic_Name_Indexfrom1.Add("Dry Run Mode 1", 1);
                this.Dispatcher.Invoke(() => cbx_DryRunMode.ItemsSource = Dic_Name_Indexfrom1.Keys);
                this.Dispatcher.Invoke(() => cbx_DryRunMode.SelectedIndex = 0);
                this.Dispatcher.Invoke(() => cbx_DryRunMode.IsEnabled = false);
                this.Dispatcher.Invoke(() => btn_Start.IsEnabled = false);
                int Run = 0, Idle = 0, Down = 0, softjam = 0, hardjam = 0;
                Random Ran = new Random();
                do
                {
                    this.Dispatcher.Invoke(() => lbl_SoftJam.Content = Ran.Next(1, 10) < 2 ? softjam++ : softjam);
                    this.Dispatcher.Invoke(() => lbl_HardJam.Content = Ran.Next(1, 10) < 2 ? hardjam++ : hardjam);

                    int n = Ran.Next(1, 10);
                    if(n == 1)
                        Down += 20;
                    else if(n == 2 || n == 3)
                        Idle += 30;
                    else
                        Run += Ran.Next(200, 320);
                    this.Dispatcher.Invoke(() => lbl_Total.Content = TimeFormat(Run++ + Idle + Down));
                    this.Dispatcher.Invoke(() => lbl_Run.Content = TimeFormat(Run));
                    this.Dispatcher.Invoke(() => lbl_Idle.Content = TimeFormat(Idle));
                    this.Dispatcher.Invoke(() => lbl_Down.Content = TimeFormat(Down));

                    this.Dispatcher.Invoke(() => lbl_MTBA.Content = TimeFormat(Run));
                    this.Dispatcher.Invoke(() => lbl_MTBF.Content = TimeFormat(Run));

                    this.Dispatcher.Invoke(() => SeriesCollections[(int)ChartSeq.Run].Values.RemoveAt(0));
                    this.Dispatcher.Invoke(() => SeriesCollections[(int)ChartSeq.Run].Values.Add(GetMinutes(Run)));

                    this.Dispatcher.Invoke(() => SeriesCollections[(int)ChartSeq.Idle].Values.RemoveAt(0));
                    this.Dispatcher.Invoke(() => SeriesCollections[(int)ChartSeq.Idle].Values.Add(GetMinutes(Idle)));

                    this.Dispatcher.Invoke(() => SeriesCollections[(int)ChartSeq.Down].Values.RemoveAt(0));
                    this.Dispatcher.Invoke(() => SeriesCollections[(int)ChartSeq.Down].Values.Add(GetMinutes(Down)));

                    Thread.Sleep(100);
                }
                while(num++ <= 50);
            }
            catch(Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void Dispose()
        {
            _Main.DryRunPageON = false;
        }
    }
}