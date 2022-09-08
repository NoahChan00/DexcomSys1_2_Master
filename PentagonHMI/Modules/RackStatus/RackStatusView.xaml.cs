using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for RackStatusView.xaml
    /// </summary>
    public partial class RackStatusView : UserControl
    {
        #region PrivateFields

        private LogicClasses.Main main = null;
        private ObservableCollection<RackStatusTypeModel> rackStatusTypeList = new ObservableCollection<RackStatusTypeModel>();
        private ObservableCollection<RackStatusSlotStatusModel> rackStatusSlotStatusList = new ObservableCollection<RackStatusSlotStatusModel>();
        private ObservableCollection<RackStatusActionModel> rackStatusActionList = new ObservableCollection<RackStatusActionModel>();

        #endregion PrivateFields

        #region Constructors

        /// <summary>
        /// INSERT INTO [gdb].[dbo].[Config] (Item,Info,ID,Remark)
        ///
        /// VALUES
        ///
        /// ('HasRackStatus','true',1,'NULL')
        /// </summary>
        /// <param name="_main"></param>
        public RackStatusView(LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                initialize(_main);
                initializeRackStatusTypeList();
                initializeRackStatusSlotStatusList();
                initializeRackStatusActionList();
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion Constructors

        #region PrivateInitializeMethods

        private void initialize(LogicClasses.Main _main)
        {
            try
            {
                main = _main;
                main.Home_OnUpdate += main_Home_OnUpdate;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        /// <summary>
        /// INSERT INTO [gdb].[dbo].[RackStatusTypeModel] (RackStatusTypeName,RackStatusTypeTotalSlotTag,RackStatusSlotStatusTagKey,RackStatusSlotSelectedTagKey,RackStatusSlotStatusTagDataType)
        ///
        /// VALUES
        ///
        /// ('Left Rack Lifter','HMI_Tags_RackLifter_Total_Slot','LRack_Lifter.RM_Rack_Status','LRack_Lifter.Slot_Selected','INT'),
        /// ('Right Rack Lifter','HMI_Tags_RackLifter_Total_Slot','RRack_Lifter.RM_Rack_Status','RRack_Lifter.Slot_Selected','INT')
        ///
        /// &&
        ///
        /// INSERT INTO [gdb].[dbo].[Setting] (MainTitle,Type,Title,Tagname,Max,Min,AccessLevel,LayoutIndex,StationID)
        ///
        /// VALUES
        ///
        /// ('General Control','INT','Rack Slot Number','HMI_Tags_RackLifter_Total_Slot',NULL,NULL,NULL,0,1)
        /// </summary>
        private void initializeRackStatusTypeList()
        {
            try
            {
                rackStatusTypeList = new ObservableCollection<RackStatusTypeModel>();

                DataTable dataTable = main.SQLer.Exec_DTSelect($"SELECT * FROM [{nameof(RackStatusTypeModel)}]");
                if(dataTable != null)
                {
                    foreach(DataRow dataRow in dataTable.Rows)
                    {
                        System.Enum.TryParse(dataRow[nameof(RackStatusTypeModel.RackStatusSlotStatusTagDataType)].ToString(),
                            out Logix.Tag.ATOMIC rackStatusSlotStatusTagDataType);

                        rackStatusTypeList.Add(new RackStatusTypeModel
                        {
                            RackStatusTypeName = dataRow[nameof(RackStatusTypeModel.RackStatusTypeName)].ToString(),
                            RackStatusTypeTotalSlotTag = new Logix.Tag
                            {
                                Name = dataRow[nameof(RackStatusTypeModel.RackStatusTypeTotalSlotTag)].ToString(),
                                DataType = Logix.Tag.ATOMIC.INT
                            },
                            RackStatusSlotStatusTagKey = dataRow[nameof(RackStatusTypeModel.RackStatusSlotStatusTagKey)].ToString(),
                            RackStatusSlotStatusTagDataType = rackStatusSlotStatusTagDataType,
                            RackStatusSlotSelectedTagKey = dataRow[nameof(RackStatusTypeModel.RackStatusSlotSelectedTagKey)].ToString()
                        });
                    }
                }

                RackStatusTypeListItemsControl.ItemsSource = rackStatusTypeList;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        /// <summary>
        /// INSERT INTO [gdb].[dbo].[RackStatusSlotStatusModel] (RackStatusSlotStatusValue,RackStatusSlotStatusName,RackStatusSlotStatusColor)
        ///
        /// VALUES
        ///
        /// ('0','Empty / Not In Use','Gray'),
        /// ('1','Loaded','Green'),
        /// ('2','Used','Orange')
        /// </summary>
        private void initializeRackStatusSlotStatusList()
        {
            try
            {
                rackStatusSlotStatusList = new ObservableCollection<RackStatusSlotStatusModel>();

                DataTable dataTable = main.SQLer.Exec_DTSelect($"SELECT * FROM [{nameof(RackStatusSlotStatusModel)}]");
                if(dataTable != null)
                {
                    foreach(DataRow dataRow in dataTable.Rows)
                    {
                        rackStatusSlotStatusList.Add(new RackStatusSlotStatusModel
                        {
                            RackStatusSlotStatusValue = dataRow[nameof(RackStatusSlotStatusModel.RackStatusSlotStatusValue)].ToString(),
                            RackStatusSlotStatusName = dataRow[nameof(RackStatusSlotStatusModel.RackStatusSlotStatusName)].ToString(),
                            RackStatusSlotStatusColor = (SolidColorBrush)new BrushConverter().ConvertFromString(
                                dataRow[nameof(RackStatusSlotStatusModel.RackStatusSlotStatusColor)].ToString())
                        });
                    }
                }

                RackStatusSlotStatusListItemsControl.ItemsSource = rackStatusSlotStatusList;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        /// <summary>
        ///
        /// INSERT INTO [gdb].[dbo].[RackStatusActionModel] (RackStatusActionName,RackStatusActionTag)
        ///
        /// VALUES
        ///
        /// ('Eject Left Tray','HMI_Tags_Eject_Left_Tray'),
        /// ('Eject Right Tray','HMI_Tags_Eject_Right_Tray'),
        /// ('Purge All','HMI_Tags_Purge_All_Tray')
        ///
        /// </summary>
        private void initializeRackStatusActionList()
        {
            try
            {
                rackStatusActionList = new ObservableCollection<RackStatusActionModel>();

                DataTable dataTable = main.SQLer.Exec_DTSelect($"SELECT * FROM [{nameof(RackStatusActionModel)}]");
                if(dataTable != null)
                {
                    foreach(DataRow dataRow in dataTable.Rows)
                    {
                        rackStatusActionList.Add(new RackStatusActionModel
                        {
                            RackStatusActionName = dataRow[nameof(RackStatusActionModel.RackStatusActionName)].ToString(),
                            RackStatusActionTag = new Logix.Tag
                            {
                                Name = dataRow[nameof(RackStatusActionModel.RackStatusActionTag)].ToString(),
                                DataType = Logix.Tag.ATOMIC.BOOL
                            }
                        });
                    }
                }

                RackStatusActionListItemsControl.ItemsSource = rackStatusActionList;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateInitializeMethods

        #region PrivateEventMethods

        private void rackStatusActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                RackStatusActionModel rackStatusActionModel = button.Tag as RackStatusActionModel;

                if(MessageBox.Show($"Are you sure you want to {rackStatusActionModel.RackStatusActionName}?",
                    nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                    MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                {
                    rackStatusActionModel.RackStatusActionTag.Value = true;

                    main.MyPLC.WriteTag(rackStatusActionModel.RackStatusActionTag);
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void main_Home_OnUpdate()
        {
            try
            {
                rackStatusTypeList.ToList().ForEach(async x =>
                {
                    try
                    {
                        if(!string.IsNullOrEmpty(x.RackStatusTypeTotalSlotTag.Name))
                        {
                            main.MyPLC.ReadTag(x.RackStatusTypeTotalSlotTag);

                            if(x.RackStatusTypeTotalSlotTag.Value != null)
                            {
                                int value = Convert.ToInt32(x.RackStatusTypeTotalSlotTag.Value);

                                if(x.RackStatusSlotList.Count != value)
                                {
                                    await Task.Delay(500);

                                    List<RackStatusSlotModel> rackStatusSlotList = new List<RackStatusSlotModel>();

                                    for(int i = value; i > 0; i--)
                                    {
                                        rackStatusSlotList.Add(new RackStatusSlotModel
                                        {
                                            RackStatusSlotName = $"S{i}",
                                            RackStatusSlotStatusTag = new Logix.Tag
                                            {
                                                Name = $"{x.RackStatusSlotStatusTagKey}[{i - 1}]",
                                                DataType = x.RackStatusSlotStatusTagDataType
                                            },
                                            RackStatusSlotSelectedTag = new Logix.Tag
                                            {
                                                Name = $"{x.RackStatusSlotSelectedTagKey}[{i - 1}]",
                                                DataType = Logix.Tag.ATOMIC.BOOL
                                            }
                                        });
                                    }

                                    x.RackStatusSlotList = new ObservableCollection<RackStatusSlotModel>(rackStatusSlotList);

                                    await Task.Delay(500);
                                }
                            }
                        }
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }

                    try
                    {
                        x.RackStatusSlotList.ToList().ForEach(y =>
                        {
                            try
                            {
                                if(!string.IsNullOrEmpty(y.RackStatusSlotSelectedTag.Name))
                                {
                                    main.MyPLC.ReadTag(y.RackStatusSlotSelectedTag);

                                    if(y.RackStatusSlotSelectedTag.Value != null)
                                    {
                                        RackStatusSlotStatusModel rackStatusSlotStatusModel = new RackStatusSlotStatusModel();

                                        bool value = Convert.ToBoolean(y.RackStatusSlotSelectedTag.Value);

                                        if(value)
                                        {
                                            if(!string.IsNullOrEmpty(y.RackStatusSlotStatusTag.Name))
                                            {
                                                main.MyPLC.ReadTag(y.RackStatusSlotStatusTag);

                                                if(y.RackStatusSlotStatusTag.Value != null)
                                                {
                                                    if(rackStatusSlotStatusList.ToList().Exists(
                                                        z => z.RackStatusSlotStatusValue == y.RackStatusSlotStatusTag.Value.ToString()))
                                                    {
                                                        rackStatusSlotStatusModel = rackStatusSlotStatusList.ToList().Find(
                                                            z => z.RackStatusSlotStatusValue == y.RackStatusSlotStatusTag.Value.ToString());
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if(rackStatusSlotStatusList.Count >= 1)
                                            {
                                                rackStatusSlotStatusModel = rackStatusSlotStatusList.First();
                                            }
                                        }

                                        if(y.RackStatusSlotStatusModel != rackStatusSlotStatusModel)
                                        {
                                            y.RackStatusSlotStatusModel = rackStatusSlotStatusModel;
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