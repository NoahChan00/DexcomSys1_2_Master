using Logix;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for RackConfigurationView.xaml.
    /// </summary>
    public partial class RackConfigurationView : UserControl, IDisposable
    {
        #region PrivateFields
        private RackConfigurationModel rackConfigurationModel = new RackConfigurationModel();
        private LogicClasses.Main main = null;
        private readonly Tag bypassLMTag = new Tag { Name = "HMI_BypassLM", DataType = Logix.Tag.ATOMIC.BOOL };
        #endregion

        #region Constructor
        public RackConfigurationView(ref LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                initializeRackConfiguration();
                initializeOpc(_main);
                readRackConfigurationUpdate();
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private void initializeRackConfiguration()
        {
            try
            {
                rackConfigurationModel = new RackConfigurationModel();

                for (int i = 1; i <= 4; i++)
                {
                    try
                    {
                        rackConfigurationModel.RackList.Add(new RackModel
                        {
                            RackName = $"Rack #{i}",
                            RackTextList = new List<RackTextModel>
                            {
                                new RackTextModel
                                {
                                    RackTextName = "MPN",
                                    RackTextTag = new Logix.Tag
                                    {
                                        Name = $"HMI_Rack{i}_Configured_MPN",
                                        DataType = Logix.Tag.ATOMIC.STRING
                                    },
                                    RackTextEnableTag = new Logix.Tag
                                    {
                                        Name = $"HMI_Rack{i}_OkToConfigure",
                                        DataType = Logix.Tag.ATOMIC.BOOL
                                    }
                                }
                            },
                            RackToggleList = new List<RackToggleModel>
                            {
                                new RackToggleModel
                                {
                                    RackToggleName = "Purge",
                                    RackToggleTag = new Logix.Tag
                                    {
                                        Name = $"HMI_Purge_Rack{i}",
                                        DataType = Logix.Tag.ATOMIC.BOOL
                                    }
                                }
                            }
                        });
                    }
                    catch (Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }
                
                RackConfigurationGroupBox.DataContext = rackConfigurationModel;
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
                main.OnRackConfigurationUpdate += main_OnRackConfigurationUpdate;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateEventMethods
        private void updateRackTextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                RackTextModel rackTextModel = button.Tag as RackTextModel;
                
                if (rackConfigurationModel.RackList.Exists(
                    x => x.RackTextList.Exists(y => y == rackTextModel)))
                {
                    RackModel rackModel = rackConfigurationModel.RackList.Find(
                        x => x.RackTextList.Exists(y => y == rackTextModel));
                    
                    if (MessageBox.Show($"Are you sure you want to update" +
                        $" {rackModel.RackName}'s {rackTextModel.RackTextName}?",
                        nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                        MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        rackTextModel.RackTextTag.Value = rackTextModel.RackText.ToUpper(); //rackTextModel.RackText;
                        main.MyPLC.WriteTag(rackTextModel.RackTextTag);
                        
                        readRackConfigurationUpdate();
                    }
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void clearRackTextButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                RackTextModel rackTextModel = button.Tag as RackTextModel;

                if (rackConfigurationModel.RackList.Exists(
                    x => x.RackTextList.Exists(y => y == rackTextModel)))
                {
                    RackModel rackModel = rackConfigurationModel.RackList.Find(
                        x => x.RackTextList.Exists(y => y == rackTextModel));

                    if (MessageBox.Show($"Are you sure you want to clear" +
                        $" {rackModel.RackName}'s {rackTextModel.RackTextName}?",
                        nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                        MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        rackTextModel.RackText = string.Empty;
                    }
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void rackToggleOffButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                RackToggleModel rackToggleModel = button.Tag as RackToggleModel;

                if (rackConfigurationModel.RackList.Exists(
                    x => x.RackToggleList.Exists(y => y == rackToggleModel)))
                {
                    RackModel rackModel = rackConfigurationModel.RackList.Find(
                        x => x.RackToggleList.Exists(y => y == rackToggleModel));
                    
                    if (MessageBox.Show($"Are you sure you want to off" +
                        $" {rackModel.RackName}'s {rackToggleModel.RackToggleName}?",
                        nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                        MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        rackToggleModel.RackToggleTag.Value = false;
                        main.MyPLC.WriteTag(rackToggleModel.RackToggleTag);
                    }
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void rackToggleOnButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                RackToggleModel rackToggleModel = button.Tag as RackToggleModel;

                if (rackConfigurationModel.RackList.Exists(
                    x => x.RackToggleList.Exists(y => y == rackToggleModel)))
                {
                    RackModel rackModel = rackConfigurationModel.RackList.Find(
                        x => x.RackToggleList.Exists(y => y == rackToggleModel));

                    if (MessageBox.Show($"Are you sure you want to on" +
                        $" {rackModel.RackName}'s {rackToggleModel.RackToggleName}?",
                        nameof(MessageBoxImage.Question), MessageBoxButton.YesNo, MessageBoxImage.Question,
                        MessageBoxResult.No, MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        rackToggleModel.RackToggleTag.Value = true;
                        main.MyPLC.WriteTag(rackToggleModel.RackToggleTag);
                    }
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        
        private void main_OnRackConfigurationUpdate()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    main.MyPLC.ReadTag(bypassLMTag);
                    if (bypassLMTag.Value != null)
                    {
                        bool value = Convert.ToBoolean(bypassLMTag.Value);

                        if (RackConfigurationScrollViewer.IsEnabled != value)
                        {
                            RackConfigurationScrollViewer.IsEnabled = value;
                        }
                    }

                    rackConfigurationModel.RackList.ForEach(x =>
                    {
                        try
                        {
                            x.RackTextList.ForEach(y =>
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(y.RackTextEnableTag.Name))
                                    {
                                        main.MyPLC.ReadTag(y.RackTextEnableTag);

                                        if (y.RackTextEnableTag.Value != null)
                                        {
                                            bool value = Convert.ToBoolean(
                                                y.RackTextEnableTag.Value);

                                            if (y.RackTextEnable != value)
                                            {
                                                y.RackTextEnable = value;
                                            }
                                        }
                                    }
                                }
                                catch (Exception exception)
                                {
                                    FileLogger.logError(exception.Message, exception.ToString());
                                }
                            });

                            x.RackToggleList.ForEach(y =>
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(y.RackToggleTag.Name))
                                    {
                                        main.MyPLC.ReadTag(y.RackToggleTag);

                                        if (y.RackToggleTag.Value != null)
                                        {
                                            bool value = Convert.ToBoolean(
                                                y.RackToggleTag.Value);

                                            if (y.RackToggleStatus != value)
                                            {
                                                y.RackToggleStatus = value;
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
        private void readRackConfigurationUpdate()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    rackConfigurationModel.RackList.ForEach(x =>
                    {
                        try
                        {
                            x.RackTextList.ForEach(y =>
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(y.RackTextTag.Name))
                                    {
                                        main.MyPLC.ReadTag(y.RackTextTag);

                                        if (y.RackTextTag.Value != null)
                                        {
                                            string value = y.RackTextTag.Value.ToString();

                                            if (y.RackText != value)
                                            {
                                                y.RackText = value;
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
                main.RackConfigurationPageOn = false;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion
    }
}