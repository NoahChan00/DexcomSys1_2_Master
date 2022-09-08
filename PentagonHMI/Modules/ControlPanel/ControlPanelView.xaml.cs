using PentagonHMI.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for ControlPanelView.xaml.
    /// </summary>
    public partial class ControlPanelView : UserControl
    {
        #region PrivateFields

        private ControlPanelModel controlPanelModel = new ControlPanelModel();
        private LogicClasses.Main _Main = null;

        #endregion PrivateFields

        #region Constructor

        public ControlPanelView(LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                _Main = _main;
                initializeControlPanel();
                initializeOpc(_main);
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion Constructor

        #region PrivateInitializeMethods

        private void initializeControlPanel()
        {
            try
            {
                if(Classes.GlobalFunctions.ProjectType == ProjectType.HDD)
                {
                    controlPanelModel = new ControlPanelModel
                    {
                        ControlPanelActionList = new List<ControlPanelActionModel>
                    {
                        new ControlPanelActionModel
                        {
                            ControlPanelActionStatusOffColor = Brushes.MediumSeaGreen,
                            ControlPanelActionStatusOnColor = Brushes.LawnGreen,
                            ControlPanelActionName = "Cycle Start",
                            ControlPanelActionTag = new Logix.Tag
                            {
                                Name = "HMI_Tags.StartButton",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            },
                            ControlPanelActionStatusTag = new Logix.Tag
                            {
                                Name = "Cell_PIO_111:22:O.0",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            }
                        },
                        new ControlPanelActionModel
                        {
                            ControlPanelActionStatusOffColor = Brushes.Crimson,
                            ControlPanelActionStatusOnColor = Brushes.LightPink,
                            ControlPanelActionName = "Cycle Stop",
                            ControlPanelActionTag = new Logix.Tag
                            {
                                Name = "HMI_Tags.StopButton",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            },
                            ControlPanelActionStatusTag = new Logix.Tag
                            {
                                Name = "Cell_PIO_111:22:O.1",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            }
                        },
                        new ControlPanelActionModel
                        {
                            ControlPanelActionStatusOffColor = Brushes.Snow,
                            ControlPanelActionStatusOnColor = Brushes.LightGray,
                            ControlPanelActionName = "Fault Reset",
                            ControlPanelActionTag = new Logix.Tag
                            {
                                Name = "HMI_Tags.ResetButton",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            },
                            ControlPanelActionStatusTag = new Logix.Tag
                            {
                                Name = "Cell_PIO_111:22:O.2",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            }
                        },
                        new ControlPanelActionModel
                        {
                            ControlPanelActionStatusOffColor = Brushes.Orange,
                            ControlPanelActionStatusOnColor = Brushes.Yellow,
                            ControlPanelActionName = "Entry Request",
                            ControlPanelActionTag = new Logix.Tag
                            {
                                Name = "HMI_Tags.EntryReqButton",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            },
                            ControlPanelActionStatusTag = new Logix.Tag
                            {
                                Name = "Cell_PIO_111:23:O.0",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            }
                        },
                        new ControlPanelActionModel
                        {
                            ControlPanelActionName = "Machine Initialize",
                            ControlPanelActionTag = new Logix.Tag
                            {
                                Name = GlobalFunctions.IsSystem1? "System1_MC_Tag.MachineInit": "System2_MC_Tag.MachineInit",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            },
                            ControlPanelActionEnableTag = new Logix.Tag
                            {
                                Name = "MC_System_Tags.MachineRunning",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            }
                        }
                    },
                        ControlPanelToggleList = new List<ControlPanelToggleModel>
                        {
                            new ControlPanelToggleModel
                            {
                                ControlPanelToggleStatusOffColor = Brushes.Silver,
                                ControlPanelToggleStatusOnColor = Brushes.LawnGreen,
                                ControlPanelToggleName = "Engineering Mode",
                                ControlPanelToggleTag = new Logix.Tag(Tags.MainPage.EngineeringMode.Name, Logix.Tag.ATOMIC.BOOL),
                                ControlPanelToggleEnableTag = new Logix.Tag("MC_System_Tags.MachineRunning",Logix.Tag.ATOMIC.BOOL)
                            }
                        }
                    };
                }
                else
                {
                    string StartRead = string.Empty;
                    string StartWrite = string.Empty;
                    string StopRead = string.Empty;
                    string StopWrite = string.Empty;
                    string ResetRead = string.Empty;
                    string ResetWrite = string.Empty;
                    string InitializeRead = string.Empty;
                    string InitializeWrite = string.Empty;

                    if(Classes.GlobalFunctions.ProjectType == ProjectType.TLA)
                    {
                        StartRead = "HMI_Tags.StartButton";
                        StartWrite = "HMI_Tags.StartButton";

                        StopRead = "HMI_Tags.StopButton";
                        StopWrite = "HMI_Tags.StopButton";

                        ResetRead = "HMI_Tags.ResetButton";
                        ResetWrite = "HMI_Tags.ResetButton";

                        InitializeRead = GlobalFunctions.IsSystem1 ? "System1_MC_Tag.MachineInit" : "System2_MC_Tag.MachineInit";
                        InitializeWrite = GlobalFunctions.IsSystem1 ? "System1_MC_Tag.MachineInit" : "System2_MC_Tag.MachineInit";

                        if(_Main.MachineName.ToUpper().Contains("FINAL"))
                        {
                            StartRead = "OutputPnP_HMI_Tags.StartButton";
                            StartWrite = "OutputPnP_HMI_Tags.StartButton";

                            StopRead = "OutputPnP_HMI_Tags.StopButton";
                            StopWrite = "OutputPnP_HMI_Tags.StopButton";

                            ResetRead = "OutputPnP_HMI_Tags.ResetButton";
                            ResetWrite = "OutputPnP_HMI_Tags.ResetButton";

                            InitializeRead = "OutPnP_MC_System_Tags.MachineInit";
                            InitializeWrite = "OutPnP_MC_System_Tags.MachineInit";
                        }
                        else if(_Main.MachineName.ToUpper().Contains("CENTRAL"))
                        {
                            StartRead = "CenVis_HMI_Tags.StartButton";
                            StartWrite = "CenVis_HMI_Tags.StartButton";

                            StopRead = "CenVis_HMI_Tags.StopButton";
                            StopWrite = "CenVis_HMI_Tags.StopButton";

                            ResetRead = "CenVis_HMI_Tags.ResetButton";
                            ResetWrite = "CenVis_HMI_Tags.ResetButton";

                            InitializeRead = "CenVis_MC_System_Tags.MachineInit";
                            InitializeWrite = "CenVis_MC_System_Tags.MachineInit";
                        }
                    }
                    else
                    {
                        StartRead = "Cell_PIO_131:24:O.0";
                        StartWrite = "HMI_Tags.StartButton";
                        StopRead = "Cell_PIO_131:24:O.1";
                        StopWrite = "HMI_Tags.StopButton";
                        ResetRead = "Cell_PIO_131:24:O.2";
                        ResetWrite = "HMI_Tags.ResetButton";
                        InitializeRead = "MC_System_Tags.MachineRunning";
                        InitializeWrite = GlobalFunctions.IsSystem1 ? "System1_MC_Tag.MachineInit" : "System2_MC_Tag.MachineInit";

                        if(_Main.MachineName.ToUpper() == "FINAL INSPECTION")
                        {
                            StartRead = "OutPnP_HMI_Tags.StartButton";
                            StartWrite = "OutPnP_HMI_Tags.StartButton";
                            StopRead = "OutPnP_HMI_Tags.StopButton";
                            StopWrite = "OutPnP_HMI_Tags.StopButton";
                            ResetRead = "OutPnP_HMI_Tags.ResetButton";
                            ResetWrite = "OutPnP_HMI_Tags.ResetButton";
                            InitializeRead = "OutputPnP_ModuleInit";
                            InitializeWrite = "OutputPnP_ModuleInit";
                        }
                        else if(_Main.MachineName.ToUpper() == "CENTRALIZED VISION")
                        {
                            StartRead = "CenVis_HMI_Tags.StartButton";
                            StartWrite = "CenVis_HMI_Tags.StartButton";
                            StopRead = "CenVis_HMI_Tags.StopButton";
                            StopWrite = "CenVis_HMI_Tags.StopButton";
                            ResetRead = "CenVis_HMI_Tags.ResetButton";
                            ResetWrite = "CenVis_HMI_Tags.ResetButton";
                            InitializeRead = "CenVis_ModuleInit";
                            InitializeWrite = "CenVis_ModuleInit";
                        }
                    }

                    controlPanelModel = new ControlPanelModel
                    {
                        ControlPanelActionList = new List<ControlPanelActionModel>
                        {
                            new ControlPanelActionModel
                            {
                                ControlPanelActionStatusOffColor = Brushes.Green,
                                ControlPanelActionStatusOnColor = Brushes.LawnGreen,
                                ControlPanelActionName = "Start",
                                ControlPanelActionTag = new Logix.Tag
                                {
                                    Name = StartWrite,
                                    DataType = Logix.Tag.ATOMIC.BOOL
                                },
                                ControlPanelActionStatusTag = new Logix.Tag
                                {
                                    Name = StartRead,
                                    DataType = Logix.Tag.ATOMIC.BOOL
                                }
                            },
                            new ControlPanelActionModel
                            {
                                ControlPanelActionStatusOffColor = Brushes.Crimson,
                                ControlPanelActionStatusOnColor = Brushes.LightPink,
                                ControlPanelActionName = "Stop",
                                ControlPanelActionTag = new Logix.Tag
                                {
                                    Name = StopWrite,
                                    DataType = Logix.Tag.ATOMIC.BOOL
                                },
                                ControlPanelActionStatusTag = new Logix.Tag
                                {
                                    Name = StopRead,
                                    DataType = Logix.Tag.ATOMIC.BOOL
                                }
                            },
                            new ControlPanelActionModel
                            {
                                ControlPanelActionStatusOffColor = Brushes.Orange,
                                ControlPanelActionStatusOnColor = Brushes.Yellow,
                                ControlPanelActionName = "Reset",
                                ControlPanelActionTag = new Logix.Tag
                                {
                                    Name = ResetWrite,
                                    DataType = Logix.Tag.ATOMIC.BOOL
                                },
                                ControlPanelActionStatusTag = new Logix.Tag
                                {
                                    Name = ResetRead,
                                    DataType = Logix.Tag.ATOMIC.BOOL
                                }
                            },
                            new ControlPanelActionModel
                            {
                                ControlPanelActionName = "Machine Initialize",
                                ControlPanelActionTag = new Logix.Tag
                                {
                                    Name = InitializeWrite,
                                    DataType = Logix.Tag.ATOMIC.BOOL
                                },
                                ControlPanelActionEnableTag = new Logix.Tag
                                {
                                    Name = InitializeRead,
                                    DataType = Logix.Tag.ATOMIC.BOOL
                                }
                            }
                        },
                        ControlPanelToggleList = new List<ControlPanelToggleModel>
                    {
                         new ControlPanelToggleModel
                        {
                            ControlPanelToggleStatusOffColor = Brushes.Silver,
                            ControlPanelToggleStatusOnColor = Brushes.LawnGreen,
                            ControlPanelToggleName = "Engineering Mode",
                            ControlPanelToggleTag = new Logix.Tag
                            {
                                Name = GlobalFunctions.IsSystem1 ? "System1_MC_Tag.EngineeringMode" : "System2_MC_Tag.EngineeringMode",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            },
                            ControlPanelToggleEnableTag = new Logix.Tag
                            {
                                Name = "MC_System_Tags.MachineRunning",
                                DataType = Logix.Tag.ATOMIC.BOOL
                            }
                        }
                    }
                    };
                    //controlPanelModel = new ControlPanelModel
                    //{
                    //    ControlPanelActionList = new List<ControlPanelActionModel>
                    //{
                    //    new ControlPanelActionModel
                    //    {
                    //        ControlPanelActionStatusOffColor = Brushes.Green,
                    //        ControlPanelActionStatusOnColor = Brushes.LawnGreen,
                    //        ControlPanelActionName = "Start",
                    //        ControlPanelActionTag = new Logix.Tag
                    //        {
                    //            Name = "HMI_Tags.StartButton",
                    //            DataType = Logix.Tag.ATOMIC.BOOL
                    //        },
                    //        ControlPanelActionStatusTag = new Logix.Tag
                    //        {
                    //            Name = "Cell_PIO_131:24:O.0",
                    //            DataType = Logix.Tag.ATOMIC.BOOL
                    //        }
                    //    },
                    //    new ControlPanelActionModel
                    //    {
                    //        ControlPanelActionStatusOffColor = Brushes.Crimson,
                    //        ControlPanelActionStatusOnColor = Brushes.LightPink,
                    //        ControlPanelActionName = "Stop",
                    //        ControlPanelActionTag = new Logix.Tag
                    //        {
                    //            Name = "HMI_Tags.StopButton",
                    //            DataType = Logix.Tag.ATOMIC.BOOL
                    //        },
                    //        ControlPanelActionStatusTag = new Logix.Tag
                    //        {
                    //            Name = "Cell_PIO_131:24:O.1",
                    //            DataType = Logix.Tag.ATOMIC.BOOL
                    //        }
                    //    },
                    //    new ControlPanelActionModel
                    //    {
                    //        ControlPanelActionStatusOffColor = Brushes.Orange,
                    //        ControlPanelActionStatusOnColor = Brushes.Yellow,
                    //        ControlPanelActionName = "Reset",
                    //        ControlPanelActionTag = new Logix.Tag
                    //        {
                    //            Name = "HMI_Tags.ResetButton",
                    //            DataType = Logix.Tag.ATOMIC.BOOL
                    //        },
                    //        ControlPanelActionStatusTag = new Logix.Tag
                    //        {
                    //            Name = "Cell_PIO_131:24:O.2",
                    //            DataType = Logix.Tag.ATOMIC.BOOL
                    //        }
                    //    },
                    //    new ControlPanelActionModel
                    //    {
                    //        ControlPanelActionName = "Machine Initialize",
                    //        ControlPanelActionTag = new Logix.Tag
                    //        {
                    //            Name = "MC_System_Tags.MachineInit",
                    //            DataType = Logix.Tag.ATOMIC.BOOL
                    //        },
                    //        ControlPanelActionEnableTag = new Logix.Tag
                    //        {
                    //            Name = "MC_System_Tags.MachineRunning",
                    //            DataType = Logix.Tag.ATOMIC.BOOL
                    //        }
                    //    }
                    //},
                    //    ControlPanelToggleList = new List<ControlPanelToggleModel>
                    //{
                    //    new ControlPanelToggleModel
                    //    {
                    //        ControlPanelToggleStatusOffColor = Brushes.Silver,
                    //        ControlPanelToggleStatusOnColor = Brushes.LawnGreen,
                    //        ControlPanelToggleName = "Engineering Mode",
                    //        ControlPanelToggleTag = new Logix.Tag
                    //        {
                    //            Name = "MC_System_Tags.EngineeringMode",
                    //            DataType = Logix.Tag.ATOMIC.BOOL
                    //        },
                    //        ControlPanelToggleEnableTag = new Logix.Tag
                    //        {
                    //            Name = "MC_System_Tags.MachineRunning",
                    //            DataType = Logix.Tag.ATOMIC.BOOL
                    //        }
                    //    }
                    //}
                    //};
                }

                ControlPanelVirtualizingStackPanel.DataContext = controlPanelModel;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeOpc(LogicClasses.Main _main)
        {
            try
            {
                _Main.OnAlwaysUpdate += main_OnAlwaysUpdate;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateInitializeMethods

        #region PrivateEventMethods

        private void controlPanelActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                ControlPanelActionModel controlPanelActionModel = button.Tag as ControlPanelActionModel;

                if(controlPanelModel.ControlPanelActionList.Exists(x => x == controlPanelActionModel))
                {
                    if(controlPanelModel.ControlPanelActionList.FindIndex(x => x == controlPanelActionModel) == 3)
                    {
                        if(MessageBox.Show($"Are you sure you want to" +
                            $" {controlPanelActionModel.ControlPanelActionName}?", nameof(MessageBoxImage.Question),
                            MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No,
                            MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                        {
                            controlPanelActionModel.ControlPanelActionTag.Value = true;
                            _Main.MyPLC.WriteTag(controlPanelActionModel.ControlPanelActionTag);
                        }
                    }
                    else
                    {
                        controlPanelActionModel.ControlPanelActionTag.Value = true;
                        _Main.MyPLC.WriteTag(controlPanelActionModel.ControlPanelActionTag);
                    }
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void controlPanelToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton toggleButton = sender as ToggleButton;
                ControlPanelToggleModel controlPanelToggleModel = toggleButton.Tag as ControlPanelToggleModel;

                if(controlPanelModel.ControlPanelToggleList.Exists(x => x == controlPanelToggleModel))
                {
                    string message = toggleButton.IsChecked == true ? "on" : "off";

                    if(MessageBox.Show($"Are you sure you want to {message}" +
                        $" {controlPanelToggleModel.ControlPanelToggleName}?", nameof(MessageBoxImage.Question),
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No,
                        MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        controlPanelToggleModel.ControlPanelToggleTag.Value = Convert.ToBoolean(toggleButton.IsChecked);
                        _Main.MyPLC.WriteTag(controlPanelToggleModel.ControlPanelToggleTag);
                    }
                    else
                    {
                        toggleButton.IsChecked = !toggleButton.IsChecked;
                    }
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void main_OnAlwaysUpdate()
        {
            try
            {
                controlPanelModel.ControlPanelActionList.ForEach(x =>
                {
                    try
                    {
                        if(!string.IsNullOrEmpty(x.ControlPanelActionEnableTag.Name))
                        {
                            _Main.MyPLC.ReadTag(x.ControlPanelActionEnableTag);

                            if(x.ControlPanelActionEnableTag.Value != null)
                            {
                                bool value = Convert.ToBoolean(
                                    x.ControlPanelActionEnableTag.Value) == false &&
                                    _Main.UserAccessLevel != "Operator";

                                if(x.ControlPanelActionEnable != value)
                                {
                                    x.ControlPanelActionEnable = value;
                                }
                            }
                        }

                        if(!string.IsNullOrEmpty(x.ControlPanelActionStatusTag.Name))
                        {
                            _Main.MyPLC.ReadTag(x.ControlPanelActionStatusTag);

                            if(x.ControlPanelActionStatusTag.Value != null)
                            {
                                bool value = Convert.ToBoolean(
                                    x.ControlPanelActionStatusTag.Value);

                                if(x.ControlPanelActionStatus != value)
                                {
                                    x.ControlPanelActionStatus = value;
                                }
                            }
                        }
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                });
                controlPanelModel.ControlPanelToggleList.ForEach(x =>
                {
                    try
                    {
                        if(!string.IsNullOrEmpty(x.ControlPanelToggleEnableTag.Name))
                        {
                            _Main.MyPLC.ReadTag(x.ControlPanelToggleEnableTag);

                            if(x.ControlPanelToggleEnableTag.Value != null)
                            {
                                bool value = Convert.ToBoolean(
                                    x.ControlPanelToggleEnableTag.Value) == false &&
                                    _Main.UserAccessLevel != "Operator";

                                if(x.ControlPanelToggleEnable != value)
                                {
                                    x.ControlPanelToggleEnable = value;
                                }
                            }
                        }

                        if(!string.IsNullOrEmpty(x.ControlPanelToggleTag.Name))
                        {
                            _Main.MyPLC.ReadTag(x.ControlPanelToggleTag);

                            if(x.ControlPanelToggleTag.Value != null)
                            {
                                bool value = Convert.ToBoolean(
                                    x.ControlPanelToggleTag.Value);

                                if(x.ControlPanelToggleStatus != value)
                                {
                                    x.ControlPanelToggleStatus = value;
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
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateEventMethods
    }
}