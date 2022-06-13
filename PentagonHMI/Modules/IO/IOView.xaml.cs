using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Utilities;
using VanillaDB;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for IOView.xaml
    /// </summary>
    public partial class IOView : UserControl, IDisposable
    {
        #region PrivateFields
        private LogicClasses.Main main = null;
        private ObservableCollection<IOTypeModel> iOTypeList = new ObservableCollection<IOTypeModel>();
        private Logix.Tag machineRunningTag = new Logix.Tag
        {
            Name = Tags.MainPage.MachineStatus.Name,            //Name = "MC_System_Tags.MachineRunning",
            DataType = Logix.Tag.ATOMIC.BOOL
        };
        private Logix.Tag engineeringModeTag = new Logix.Tag(Tags.MainPage.EngineeringMode.Name, Logix.Tag.ATOMIC.BOOL);
        #endregion

        #region Constructors
        public IOView(ref LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();

                main = _main;

                if (initializeIO())
                {
                    main.OnIOUpdate += main_OnIOUpdate;
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private bool initializeIO()
        {
            try
            {
                iOTypeList = new ObservableCollection<IOTypeModel>();

                Database database = new Database(Properties.Settings.Default.DatabaseConnectionString);
                string errorMessage = string.Empty;

                DataTable dataTable = database.ExecuteQueryDT_Select($"SELECT * FROM IO WHERE " +
                    $"StationID = {main.StationID} ORDER BY CAST([TagIndex] AS INT)", ref errorMessage);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    object iOReadTag = dataRow["TagName"];
                    object iOTagName = dataRow["DisplayName"];
                    object iOIndexName = dataRow["IOIndex"];
                    object _iOName = dataRow["IO"];
                    object iOToggleTag = dataRow["OutputTagName"];
                    object iOTypeName = dataRow["IOType"];

                    if (iOReadTag != null && iOTagName != null && iOIndexName != null && _iOName != null && iOTypeName != null)
                    {
                        if (!string.IsNullOrEmpty(iOReadTag.ToString()) && !string.IsNullOrEmpty(iOTagName.ToString()) &&
                            !string.IsNullOrEmpty(iOIndexName.ToString()) && !string.IsNullOrEmpty(_iOName.ToString()) &&
                            !string.IsNullOrEmpty(iOTypeName.ToString()))
                        {
                            string iOName = string.Empty;
                            bool iOTagEnable = false;

                            switch (_iOName.ToString())
                            {
                                case "I":
                                    iOName = "Input";
                                    iOTagEnable = false;
                                    break;
                                case "O":
                                    iOName = "Output";
                                    iOTagEnable = true;
                                    break;
                                default:
                                    break;
                            }

                            if (!string.IsNullOrEmpty(iOName))
                            {
                                if (!iOTypeList.ToList().Exists(x => x.IOTypeName == iOTypeName.ToString()))
                                {
                                    iOTypeList.Add(new IOTypeModel
                                    {
                                        IOTypeName = iOTypeName.ToString(),
                                        IOIndexList = new ObservableCollection<IOIndexModel>()
                                    });
                                }

                                if (!iOTypeList.ToList().Find(x => x.IOTypeName == iOTypeName.ToString()).IOIndexList.ToList().Exists(
                                    x => x.IOIndexName == iOIndexName.ToString()))
                                {
                                    iOTypeList.ToList().Find(x => x.IOTypeName == iOTypeName.ToString()).IOIndexList.Add(new IOIndexModel
                                    {
                                        IOIndexName = iOIndexName.ToString(),
                                        IOList = new ObservableCollection<IOModel>()
                                    });
                                }

                                if (!iOTypeList.ToList().Find(x => x.IOTypeName == iOTypeName.ToString()).IOIndexList.ToList().Find(
                                    x => x.IOIndexName == iOIndexName.ToString()).IOList.ToList().Exists(x => x.IOName == iOName))
                                {
                                    IOModel iOModel = new IOModel
                                    {
                                        IOName = iOName,
                                        IOTagList = new ObservableCollection<IOTagModel>()
                                    };

                                    if (iOTagEnable)
                                    {
                                        iOTypeList.ToList().Find(x => x.IOTypeName == iOTypeName.ToString()).IOIndexList.ToList().Find(
                                            x => x.IOIndexName == iOIndexName.ToString()).IOList.Add(iOModel);
                                    }
                                    else
                                    {
                                        iOTypeList.ToList().Find(x => x.IOTypeName == iOTypeName.ToString()).IOIndexList.ToList().Find(
                                            x => x.IOIndexName == iOIndexName.ToString()).IOList.Insert(0, iOModel);
                                    }
                                }

                                IOTagModel iOTagModel = new IOTagModel
                                {
                                    IOReadTag = new Logix.Tag
                                    {
                                        Name = iOReadTag.ToString(),
                                        DataType = Logix.Tag.ATOMIC.BOOL
                                    },
                                    IOTagName = iOTagName.ToString(),
                                    IOTagEnable = iOTagEnable
                                };

                                if (iOTagModel.IOTagEnable)
                                {
                                    if (iOToggleTag != null)
                                    {
                                        if (!string.IsNullOrEmpty(iOToggleTag.ToString()))
                                        {
                                            iOTagModel.IOToggleTag = new Logix.Tag
                                            {
                                                Name = iOToggleTag.ToString(),
                                                DataType = Logix.Tag.ATOMIC.BOOL
                                            };
                                        }
                                    }
                                }

                                iOTypeList.ToList().Find(x => x.IOTypeName == iOTypeName.ToString()).IOIndexList.ToList().Find(
                                    x => x.IOIndexName == iOIndexName.ToString()).IOList.ToList().Find(
                                    x => x.IOName == iOName).IOTagList.Add(iOTagModel);
                            }
                        }
                    }
                }

                IOTabControl.ItemsSource = iOTypeList;

                return true;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
                return false;
            }
        }
        #endregion

        #region PrivateEventMethods
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TabControl tabControl = sender as TabControl;
                if (tabControl.SelectedIndex < 0)
                {
                    tabControl.SelectedIndex = 0;
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void iOCheckBox_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox checkBox = sender as CheckBox;
                IOTagModel iOTagModel = checkBox.Tag as IOTagModel;

                if (iOTagModel.IOTagEnable)
                {
                    if (!string.IsNullOrEmpty(iOTagModel.IOToggleTag.Name))
                    {
                        main.OPC.Read<bool>(iOTagModel.IOToggleTag.Name);
                        main.OPC.Write(iOTagModel.IOToggleTag.Name, Convert.ToBoolean(checkBox.IsChecked));
                    }
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void engineeringModeToggleButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton toggleButton = sender as ToggleButton;

                engineeringModeTag.Value = Convert.ToBoolean(toggleButton.IsChecked);

                FileLogger.logButton(nameof(IOView), $"{toggleButton.Content} - {engineeringModeTag.Value}",
                    nameof(engineeringModeToggleButton_Click));

                main.MyPLC.WriteTag(engineeringModeTag);
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void main_OnIOUpdate()
        {
            try
            {
                if (!string.IsNullOrEmpty(machineRunningTag.Name))
                {
                    main.MyPLC.ReadTag(machineRunningTag);

                    if (machineRunningTag.Value != null)
                    {
                        bool machineRunning = Convert.ToBoolean(machineRunningTag.Value);

                        EngineeringModeToggleButton.Dispatcher.Invoke(new Action(() =>
                        {
                            if (EngineeringModeToggleButton.IsEnabled != !machineRunning)
                            {
                                EngineeringModeToggleButton.IsEnabled = !machineRunning;
                            }
                        }));
                    }
                }

                if (!string.IsNullOrEmpty(engineeringModeTag.Name))
                {
                    main.MyPLC.ReadTag(engineeringModeTag);

                    if (engineeringModeTag.Value != null)
                    {
                        bool engineeringMode = Convert.ToBoolean(engineeringModeTag.Value);

                        EngineeringModeToggleButton.Dispatcher.Invoke(new Action(() =>
                        {
                            if (EngineeringModeToggleButton.IsChecked != engineeringMode)
                            {
                                EngineeringModeToggleButton.IsChecked = engineeringMode;
                            }
                        }));

                        iOTypeList.ToList().ForEach(x =>
                        {
                            x.IOIndexList.ToList().ForEach(y =>
                            {
                                y.IOList.ToList().ForEach(z =>
                                {
                                    if (z.IOEnable != engineeringMode)
                                    {
                                        z.IOEnable = engineeringMode;
                                    }
                                });
                            });
                        });
                    }
                }

                iOTypeList.ToList().ForEach(x =>
                {
                    x.IOIndexList.ToList().ForEach(y =>
                    {
                        y.IOList.ToList().ForEach(z =>
                        {
                            z.IOTagList.ToList().ForEach(i =>
                            {
                                if (!string.IsNullOrEmpty(i.IOReadTag.Name))
                                {
                                    main.MyPLC.ReadTag(i.IOReadTag);

                                    if (i.IOReadTag.Value != null)
                                    {
                                        bool iOTagStatus = Convert.ToBoolean(i.IOReadTag.Value);

                                        if (i.IOTagStatus != iOTagStatus)
                                        {
                                            i.IOTagStatus = iOTagStatus;
                                        }
                                    }
                                }
                            });
                        });
                    });
                });
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PublicInterfaceMethods
        public void Dispose()
        {
            try
            {
                main.IOPageON = false;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion
    }
}