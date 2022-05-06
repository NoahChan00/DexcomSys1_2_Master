using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for DryRunView.xaml
    /// </summary>
    public partial class DryRunView : UserControl, IDisposable
    {
        #region PrivateFields
        private LogicClasses.Main main = null;
        private ObservableCollection<DryRunModel> dryRunList = new ObservableCollection<DryRunModel>();
        private ICollectionView dryRunListView = null;
        private ObservableCollection<DryRunActionModel> dryRunActionList = new ObservableCollection<DryRunActionModel>();
        private ObservableCollection<DryRunTimeModel> dryRunTimeList = new ObservableCollection<DryRunTimeModel>();
        private ObservableCollection<DryRunCountModel> dryRunCountList = new ObservableCollection<DryRunCountModel>();
        #endregion

        #region Constructors
        public DryRunView(LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                initialize(_main);
                initializeDryRunList();
                initializeDryRunActionList();
                initializeDryRunTimeList();
                initializeDryRunCountList();
                initializeDryRunDurationChart();
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region Destructors
        ~DryRunView()
        {
            try
            {
                Dispose();
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private void initialize(LogicClasses.Main _main)
        {
            try
            {
                main = _main;
                main.OnDryrunUpdate += main_OnDryrunUpdate;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        
        private void initializeDryRunList()
        {
            try
            {
                dryRunList = new ObservableCollection<DryRunModel>();
                
                for (int i = 1; i <= 10; i++)
                {
                    dryRunList.Add(new DryRunModel
                    {
                        DryRunIndex = i,
                        DryRunEnableTag = new Logix.Tag
                        {
                            Name = $"Dry_Run_Data_{i}.Enable_DryRun",
                            DataType = Logix.Tag.ATOMIC.BOOL
                        },
                        DryRunNameTag = new Logix.Tag
                        {
                            Name = $"Dry_Run_Data_{i}.Name",
                            DataType = Logix.Tag.ATOMIC.STRING
                        }
                    });
                }
                
                dryRunListView = CollectionViewSource.GetDefaultView(dryRunList);
                DryRunListComboBox.ItemsSource = dryRunListView;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        
        private void initializeDryRunActionList()
        {
            try
            {
                dryRunActionList = new ObservableCollection<DryRunActionModel>
                {
                    new DryRunActionModel
                    {
                        DryRunActionName = "Start",
                        DryRunActionTagKey = "Dry_Run_Data_{0}.Start",
                        DryRunActionValue = true,
                        DryRunActionEnableTagKey = "Dry_Run_Data_{0}.Start",
                        DryRunActionEnableValue = false
                    },
                    new DryRunActionModel
                    {
                        DryRunActionName = "Stop",
                        DryRunActionTagKey = "Dry_Run_Data_{0}.Start",
                        DryRunActionValue = false,
                        DryRunActionEnableTagKey ="Dry_Run_Data_{0}.Start",
                        DryRunActionEnableValue = true
                    },
                    new DryRunActionModel
                    {
                        DryRunActionName = "Reset",
                        DryRunActionTagKey = "Dry_Run_Data_{0}.Reset",
                        DryRunActionValue = true
                    }
                };
                
                DryRunActionListItemsControl.ItemsSource = dryRunActionList;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeDryRunTimeList()
        {
            try
            {
                dryRunTimeList = new ObservableCollection<DryRunTimeModel>
                {
                    new DryRunTimeModel
                    {
                        DryRunTimeName = "Total Duration"
                    },
                    new DryRunTimeModel
                    {
                        DryRunTimeName = "Run Time",
                        DryRunTimeTagKey = "Dry_Run_Data_{0}.Operation_Time_sec"
                    },
                    new DryRunTimeModel
                    {
                        DryRunTimeName = "Idle Time",
                        DryRunTimeTagKey = "Dry_Run_Data_{0}.Idling_Time_sec"
                    },
                    new DryRunTimeModel
                    {
                        DryRunTimeName = "Down Time",
                        DryRunTimeTagKey = "Dry_Run_Data_{0}.Down_Time_sec"
                    },
                    new DryRunTimeModel
                    {
                        DryRunTimeName = "MTBA",
                        DryRunTimeTagKey = "Dry_Run_Data_{0}.MTBA_sec"
                    },
                    new DryRunTimeModel
                    {
                        DryRunTimeName = "MTBF",
                        DryRunTimeTagKey = "Dry_Run_Data_{0}.MTBF_sec"
                    }
                };
                
                DryRunTimeListItemsControl.ItemsSource = dryRunTimeList;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeDryRunCountList()
        {
            try
            {
                dryRunCountList = new ObservableCollection<DryRunCountModel>
                {
                    new DryRunCountModel
                    {
                        DryRunCountName = "Soft Jam",
                        DryRunCountTagKey = "Dry_Run_Data_{0}.Soft_Jam_Count"
                    },
                    new DryRunCountModel
                    {
                        DryRunCountName = "Hard Jam",
                        DryRunCountTagKey = "Dry_Run_Data_{0}.Hard_Jam_Count"
                    }
                };

                DryRunCountListItemsControl.ItemsSource = dryRunCountList;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeDryRunDurationChart()
        {
            try
            {
                DryRunDurationPieChart.Series = new SeriesCollection
                {
                    new PieSeries
                    {
                        Fill = Brushes.Teal,
                        Title = "Run Time",
                        Values = new ChartValues<int>{ 0 }
                    },
                    new PieSeries
                    {
                        Fill = Brushes.LightBlue,
                        Title = "Idle Time",
                        Values = new ChartValues<int>{ 0 }
                    },
                    new PieSeries
                    {
                        Fill = Brushes.OrangeRed,
                        Title = "Down Time",
                        Values = new ChartValues<int>{ 0 }
                    }
                };
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateEventMethods
        private void main_OnDryrunUpdate()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    dryRunList.ToList().ForEach(x =>
                    {
                        if (!string.IsNullOrEmpty(x.DryRunEnableTag.Name))
                        {
                            main.MyPLC.ReadTag(x.DryRunEnableTag);

                            if (x.DryRunEnableTag.Value != null)
                            {
                                bool value = Convert.ToBoolean(x.DryRunEnableTag.Value);

                                if (x.DryRunEnable != value)
                                {
                                    x.DryRunEnable = value;

                                    dryRunListView.Filter = y =>
                                    {
                                        DryRunModel dryRun = y as DryRunModel;
                                        return dryRun.DryRunEnable == true;
                                    };
                                }
                            }
                        }

                        if (!string.IsNullOrEmpty(x.DryRunNameTag.Name))
                        {
                            main.MyPLC.ReadTag(x.DryRunNameTag);

                            if (x.DryRunNameTag.Value != null)
                            {
                                string value = x.DryRunNameTag.Value.ToString();

                                if (x.DryRunName != value)
                                {
                                    x.DryRunName = value;
                                }
                            }
                        }
                    });

                    if (getCurrentDryRunModel(out DryRunModel dryRunModel))
                    {
                        dryRunActionList.ToList().ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.DryRunActionEnableTagKey))
                            {
                                Logix.Tag dryRunActionEnableTag = new Logix.Tag
                                {
                                    Name = string.Format(x.DryRunActionEnableTagKey, dryRunModel.DryRunIndex),
                                    DataType = Logix.Tag.ATOMIC.BOOL
                                };

                                if (!string.IsNullOrEmpty(dryRunActionEnableTag.Name))
                                {
                                    main.MyPLC.ReadTag(dryRunActionEnableTag);

                                    if (dryRunActionEnableTag.Value != null)
                                    {
                                        bool value = Convert.ToBoolean(dryRunActionEnableTag.Value);

                                        x.DryRunActionEnable = x.DryRunActionEnableValue == value;
                                    }
                                }
                            }
                        });

                        dryRunTimeList.ToList().ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.DryRunTimeTagKey))
                            {
                                Logix.Tag dryRunTimeTag = new Logix.Tag
                                {
                                    Name = string.Format(x.DryRunTimeTagKey, dryRunModel.DryRunIndex),
                                    DataType = Logix.Tag.ATOMIC.DINT
                                };

                                if (!string.IsNullOrEmpty(dryRunTimeTag.Name))
                                {
                                    main.MyPLC.ReadTag(dryRunTimeTag);

                                    if (dryRunTimeTag.Value != null)
                                    {
                                        int value = Convert.ToInt32(dryRunTimeTag.Value);

                                        if (x.DryRunTime != value)
                                        {
                                            x.DryRunTime = value;
                                        }
                                    }
                                }
                            }
                        });

                        dryRunTimeList.First().DryRunTime = dryRunTimeList.ToList().GetRange(1, 3).Select(x => x.DryRunTime).Sum();

                        dryRunCountList.ToList().ForEach(x =>
                        {
                            if (!string.IsNullOrEmpty(x.DryRunCountTagKey))
                            {
                                Logix.Tag dryRunCountTag = new Logix.Tag
                                {
                                    Name = string.Format(x.DryRunCountTagKey, dryRunModel.DryRunIndex),
                                    DataType = Logix.Tag.ATOMIC.INT
                                };

                                if (!string.IsNullOrEmpty(dryRunCountTag.Name))
                                {
                                    main.MyPLC.ReadTag(dryRunCountTag);

                                    if (dryRunCountTag.Value != null)
                                    {
                                        int value = Convert.ToInt32(dryRunCountTag.Value);

                                        if (x.DryRunCount != value)
                                        {
                                            x.DryRunCount = value;
                                        }
                                    }
                                }
                            }
                        });
                        
                        for (int i = 0; i < DryRunDurationPieChart.Series.Count; i++)
                        {
                            int currentValue = Convert.ToInt32(DryRunDurationPieChart.Series[i].Values[0]);
                            int value = Convert.ToInt32(TimeSpan.FromSeconds(dryRunTimeList[i + 1].DryRunTime).TotalMinutes);

                            if (currentValue != value)
                            {
                                DryRunDurationPieChart.Series[i].Values[0] = value;
                            }
                        }
                    }
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            });
        }
        
        private void dryRunActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (getCurrentDryRunModel(out DryRunModel dryRunModel))
                {
                    Button button = sender as Button;
                    DryRunActionModel dryRunActionModel = button.Tag as DryRunActionModel;
                    
                    Logix.Tag dryRunActionTag = new Logix.Tag
                    {
                        Name = string.Format(dryRunActionModel.DryRunActionTagKey, dryRunModel.DryRunIndex),
                        DataType = Logix.Tag.ATOMIC.BOOL,
                        Value = dryRunActionModel.DryRunActionValue
                    };

                    main.MyPLC.WriteTag(dryRunActionTag);
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateMethods
        private bool getCurrentDryRunModel(out DryRunModel dryRunModel)
        {
            dryRunModel = null;
            
            try
            {
                if (DryRunListComboBox.SelectedItem != null)
                {
                    dryRunModel = DryRunListComboBox.SelectedItem as DryRunModel;
                    return true;
                }
                
                return false;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
                return false;
            }
        }
        #endregion

        #region PublicInterfaceMethods
        public void Dispose()
        {
            try
            {
                main.DryRunPageON = false;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion
    }
}