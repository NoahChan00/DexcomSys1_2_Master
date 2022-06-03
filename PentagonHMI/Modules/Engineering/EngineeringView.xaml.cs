using Logix;
using PentagonHMI.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI
{
    public partial class EngineeringView : UserControl, IDisposable
    {
        #region PrivateFields
        private List<EngineeringGroupModel> engineeringGroupList = new List<EngineeringGroupModel>();
        private LogicClasses.Main main = null;
        //private readonly Tag engineeringModeTag = new Tag { Name = "MC_System_Tags.EngineeringMode", DataType = Logix.Tag.ATOMIC.BOOL };
        private readonly Tag engineeringModeTag = new Tag { Name = GlobalFunctions.IsSystem1 ? "System1_MC_Tag.EngineeringMode" : "System2_MC_Tag.EngineeringMode", DataType = Logix.Tag.ATOMIC.BOOL };

        #endregion

        #region Constructor
        public EngineeringView(ref LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                initializeEngineering();
                initializeOpc(_main);
                readEngineeringUpdate();
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private void initializeEngineering()
        {
            try
            {
                switch (GlobalFunctions.ProjectType)
                {
                    case ProjectType.TLA:
                        engineeringGroupList = new List<EngineeringGroupModel>
                        {
                            new EngineeringGroupModel
                            {
                                EngineeringGroupName = "General",
                                EngineeringGroupToggleList = new List<EngineeringGroupToggleModel>
                                {
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Trigger Calibration",
                                        EngineeringGroupToggleTag = new Logix.Tag
                                        {
                                            Name = "HMI_EngSeq_Trigger_rbtCalibration",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Start Master Sequence",
                                        EngineeringGroupToggleTag = new Logix.Tag
                                        {
                                            Name = "HMI_EngSeq_Start_Master_Seq",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Start Robot 1 Place To Tray",
                                        EngineeringGroupToggleTag = new Logix.Tag
                                        {
                                            Name = "HMI_EngSeq_StartRbt1_PlaceToTray",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    }
                                },
                                EngineeringGroupSelectionList = new List<EngineeringGroupSelectionModel>
                                {
                                    new EngineeringGroupSelectionModel
                                    {
                                        EngineeringGroupSelectionName = "Robot 1 Zone",
                                        EngineeringGroupSelectionItemList = new List<string>
                                        {
                                            "Zone 1",
                                            "Zone 2"
                                        },
                                        EngineeringGroupSelectionTag = new Tag
                                        {
                                            Name = "HMI_EngSeq_Rbt1_Zone",
                                            DataType = Logix.Tag.ATOMIC.DINT
                                        }
                                    },
                                    new EngineeringGroupSelectionModel
                                    {
                                        EngineeringGroupSelectionName = "Robot 1 Group",
                                        EngineeringGroupSelectionItemList =new List<string>
                                        {
                                            "Group 1",
                                            "Group 2",
                                            "Group 3",
                                            "Group 4"
                                        },
                                        EngineeringGroupSelectionTag = new Tag
                                        {
                                            Name = "HMI_EngSeq_Rbt1_Group",
                                            DataType = Logix.Tag.ATOMIC.DINT
                                        }
                                    }
                                }
                            }
                        };
                        break;
                    case ProjectType.ARCADIA:
                    case ProjectType.DIMM:
                        engineeringGroupList = new List<EngineeringGroupModel>
                        {
                            new EngineeringGroupModel
                            {
                                EngineeringGroupName = "General",
                                EngineeringGroupSelectionList = new List<EngineeringGroupSelectionModel>
                                {
                                    new EngineeringGroupSelectionModel
                                    {
                                        EngineeringGroupSelectionName = "Zone Number",
                                        EngineeringGroupSelectionItemList = new List<string>
                                        {
                                            "Zone #1",
                                            "Zone #2"
                                        },
                                        EngineeringGroupSelectionTag = new Tag
                                        {
                                            Name = "HMI_EngSeq_Rbt1_Zone",
                                            DataType = Logix.Tag.ATOMIC.DINT
                                        }
                                    },
                                    new EngineeringGroupSelectionModel
                                    {
                                        EngineeringGroupSelectionName = "Group Number",
                                        EngineeringGroupSelectionItemList = new List<string>
                                        {
                                            "Group #1",
                                            "Group #2",
                                            "Group #3",
                                            "Group #4"
                                        },
                                        EngineeringGroupSelectionTag = new Tag
                                        {
                                            Name = "HMI_EngSeq_Rbt1_Group",
                                            DataType = Logix.Tag.ATOMIC.DINT
                                        }
                                    }
                                },
                                EngineeringGroupToggleList = new List<EngineeringGroupToggleModel>
                                {
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Trigger Robot Calibration",
                                        EngineeringGroupToggleTag = new Logix.Tag
                                        {
                                            Name = "HMI_EngSeq_Trigger_rbtCalibration",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Start Master Sequence",
                                        EngineeringGroupToggleTag = new Logix.Tag
                                        {
                                            Name = "HMI_EngSeq_Start_Master_Seq",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Start Robot #1 Place To Tray",
                                        EngineeringGroupToggleTag = new Logix.Tag
                                        {
                                            Name = "HMI_EngSeq_StartRbt1_PlaceToTray",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    }
                                }
                            }
                        };
                        break;
                    case ProjectType.HDD:
                        engineeringGroupList = new List<EngineeringGroupModel>
                        {
                            new EngineeringGroupModel
                            {
                                EngineeringGroupName = "Gantry #1",
                                EngineeringGroupTextList = new List<EngineeringGroupTextModel>
                                {
                                    new EngineeringGroupTextModel
                                    {
                                        EngineeringTextName = "Gantry #1 Dismantle Which Screw",
                                        EngineeringGroupTextTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G1_DismantleWhichScrew",
                                            DataType = Logix.Tag.ATOMIC.INT
                                        }
                                    },
                                    new EngineeringGroupTextModel
                                    {
                                        EngineeringTextName = "Gantry #1 Torque Drive Size Selection (T6/T8)",
                                        EngineeringGroupTextTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G1_SelectT6T8_TorqDriver",
                                            DataType = Logix.Tag.ATOMIC.INT
                                        }
                                    }
                                },
                                EngineeringGroupToggleList = new List<EngineeringGroupToggleModel>
                                {
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #1 Dismantle Screw At Shuttle #1",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G1_DismatleShuttle1Screw",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #1 Dismantel Screw At Shuttle #2",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G1_DismatleShuttle2Screw",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #1 Machine Running Flag",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G1_RunFlag",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    }
                                }
                            },
                            new EngineeringGroupModel
                            {
                                EngineeringGroupName = "Gantry #2",
                                EngineeringGroupToggleList = new List<EngineeringGroupToggleModel>
                                {
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #2 Dispose Item Flag",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G2_Dispose",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #2 Load HDD To Shuttle #2",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G2_LoadHDDToS2",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #2 Remove Top Lid",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G2_RemoveTopLid",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #2 Remove ECM & Silica Bag",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G2_RmvECMSilica",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #2 Machine Running Flag",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G2_RunFlag",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #2 Unload HDD Drom Shuttle #1",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G2_UnloadHDDfrmS1",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    }
                                }
                            },
                            new EngineeringGroupModel
                            {
                                EngineeringGroupName = "Gantry #3",
                                EngineeringGroupToggleList = new List<EngineeringGroupToggleModel>
                                {
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #3 Dispose Item Flag",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G3_Dispose",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #3 Remove Bottom Magnet",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G3_RemoveBtmMagnet",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #3 Remove HAS",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G3_RemoveHSA",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #3 Remove Top Magnet",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G3_RemoveTopMagnet",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Gantry #3 Machine Running Flag",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_G3_RunFlag",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    }
                                }
                            },
                            new EngineeringGroupModel
                            {
                                EngineeringGroupName = "Vision #1",
                                EngineeringGroupToggleList = new List<EngineeringGroupToggleModel>
                                {
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Vision #1 Inspect Top Lid Screw Preesent",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_Vis1_InspectTopLidScrwPres",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    }
                                }
                            },
                            new EngineeringGroupModel
                            {
                                EngineeringGroupName = "Vision #2",
                                EngineeringGroupToggleList = new List<EngineeringGroupToggleModel>
                                {
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Vision #2 Inspect Bottom Magnet Present",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_Vis2_InspectBtmMag",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    },
                                    new EngineeringGroupToggleModel
                                    {
                                        EngineeringGroupToggleName = "Vision #2 Inspect Top Magnet, HAS & Disk",
                                        EngineeringGroupToggleTag = new Tag
                                        {
                                            Name = "HMI_EngMode_Vis2_InspectTopMagHSADisk",
                                            DataType = Logix.Tag.ATOMIC.BOOL
                                        }
                                    }
                                }
                            }
                        };
                        break;
                    default:
                        break;
                }

                EngineeringItemsControl.ItemsSource = engineeringGroupList;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeOpc(LogicClasses.Main _main)
        {
            try
            {
                main = _main;
                main.OnEngineeringUpdate += main_OnEngineeringUpdate;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateEventMethods
        private void setEngineeringGroupTextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                EngineeringGroupTextModel engineeringGroupTextModel =
                    button.Tag as EngineeringGroupTextModel;

                if (MessageBox.Show($"Are you sure you want to set" +
                    $" {engineeringGroupTextModel.EngineeringTextName}?",
                    nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                {
                    engineeringGroupTextModel.EngineeringGroupTextTag.Value =
                        engineeringGroupTextModel.EngineeringTextValue;

                    main.MyPLC.WriteTag(engineeringGroupTextModel.EngineeringGroupTextTag);
                    
                    readEngineeringUpdate();
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void setEngineeringGroupSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                EngineeringGroupSelectionModel engineeringGroupSelectionModel =
                    button.Tag as EngineeringGroupSelectionModel;
                
                if (MessageBox.Show($"Are you sure you want to set" +
                    $" {engineeringGroupSelectionModel.EngineeringGroupSelectionName}?",
                    nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                {
                    engineeringGroupSelectionModel.EngineeringGroupSelectionTag.Value =
                        engineeringGroupSelectionModel.EngineeringGroupSelectionSelectedIndex + 1;
                    
                    main.MyPLC.WriteTag(engineeringGroupSelectionModel.EngineeringGroupSelectionTag);
                    
                    readEngineeringUpdate();
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void engineeringGroupToggleOffButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                EngineeringGroupToggleModel engineeringGroupToggleModel =
                    button.Tag as EngineeringGroupToggleModel;

                if (MessageBox.Show($"Are you sure you want to off" +
                    $" {engineeringGroupToggleModel.EngineeringGroupToggleName}?",
                    nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                {
                    engineeringGroupToggleModel.EngineeringGroupToggleTag.Value = false;

                    main.MyPLC.WriteTag(engineeringGroupToggleModel.EngineeringGroupToggleTag);
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void engineeringGroupToggleOnButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                EngineeringGroupToggleModel engineeringGroupToggleModel =
                    button.Tag as EngineeringGroupToggleModel;

                if (MessageBox.Show($"Are you sure you want to on" +
                    $" {engineeringGroupToggleModel.EngineeringGroupToggleName}?",
                    nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                {
                    engineeringGroupToggleModel.EngineeringGroupToggleTag.Value = true;

                    main.MyPLC.WriteTag(engineeringGroupToggleModel.EngineeringGroupToggleTag);
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void engineeringGroupActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                EngineeringGroupActionModel engineeringGroupActionModel =
                    button.Tag as EngineeringGroupActionModel;

                if (MessageBox.Show($"Are you sure you want to" +
                    $" {engineeringGroupActionModel.EngineeringGroupActionName}?",
                    nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                {
                    engineeringGroupActionModel.EngineeringGroupActionTag.Value = true;

                    main.MyPLC.WriteTag(engineeringGroupActionModel.EngineeringGroupActionTag);
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        
        private void main_OnEngineeringUpdate()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    main.MyPLC.ReadTag(engineeringModeTag);
                    if (engineeringModeTag.Value != null)
                    {
                        bool value = Convert.ToBoolean(engineeringModeTag.Value);

                        if (EngineeringItemsControl.IsEnabled != value)
                        {
                            EngineeringItemsControl.IsEnabled = value;
                        }
                    }

                    engineeringGroupList.ForEach(x =>
                    {
                        try
                        {
                            x.EngineeringGroupToggleList.ForEach(y =>
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(y.EngineeringGroupToggleTag.Name))
                                    {
                                        main.MyPLC.ReadTag(y.EngineeringGroupToggleTag);

                                        if (y.EngineeringGroupToggleTag.Value != null)
                                        {
                                            bool value = Convert.ToBoolean(
                                                y.EngineeringGroupToggleTag.Value);

                                            if (y.EngineeringGroupToggle != value)
                                            {
                                                y.EngineeringGroupToggle = value;
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
                        catch (Exception exception)
                        {
                            FileLogger.logError(exception.Message, exception.ToString());
                        }
                    });
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }
        #endregion

        #region PrivateMethods
        private void readEngineeringUpdate()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    engineeringGroupList.ForEach(x =>
                    {
                        try
                        {
                            x.EngineeringGroupTextList.ForEach(y =>
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(y.EngineeringGroupTextTag.Name))
                                    {
                                        main.MyPLC.ReadTag(y.EngineeringGroupTextTag);

                                        if (y.EngineeringGroupTextTag.Value != null)
                                        {
                                            string value = y.EngineeringGroupTextTag.Value.ToString();

                                            if (y.EngineeringTextValue != value)
                                            {
                                                y.EngineeringTextValue = value;
                                            }
                                        }
                                    }
                                }
                                catch (Exception exception)
                                {
                                    FileLogger.logError(exception.Message, exception.ToString());
                                }
                            });

                            x.EngineeringGroupSelectionList.ForEach(y =>
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(y.EngineeringGroupSelectionTag.Name))
                                    {
                                        main.MyPLC.ReadTag(y.EngineeringGroupSelectionTag);

                                        if (y.EngineeringGroupSelectionTag.Value != null)
                                        {
                                            int value = Convert.ToInt32(
                                                y.EngineeringGroupSelectionTag.Value) - 1;

                                            if (y.EngineeringGroupSelectionSelectedIndex != value)
                                            {
                                                y.EngineeringGroupSelectionSelectedIndex = value;
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
                        catch (Exception exception)
                        {
                            FileLogger.logError(exception.Message, exception.ToString());
                        }
                    });
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }
        #endregion

        #region PublicInterfaceMethods
        public void Dispose()
        {
            try
            {
                main.EngineeringPageOn = false;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion
    }
}