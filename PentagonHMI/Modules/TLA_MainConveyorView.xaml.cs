using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for TLA_MainConveyorView.xaml
    /// </summary>
    public partial class TLA_MainConveyorView : UserControl
    {
        #region PrivateFields
        private Dictionary<string, Brush> machineStatusDictionary = new Dictionary<string, Brush>
        {
            { "Running", Brushes.MediumSeaGreen },
            { "Idle", Brushes.DeepSkyBlue },
            { "Stop", Brushes.Yellow },
            { "Initialize", Brushes.DeepSkyBlue },
            { "Dry Run", Brushes.Blue },
            { "Error", Brushes.Red },
            { "Warning", Brushes.Yellow },
            { "Disabled", Brushes.DimGray },
            { "Alarm", Brushes.Red }
        };
        
        private List<Tuple<Logix.Tag, Rectangle, TextBlock, Logix.Controller>> machineStatusList = new List<Tuple<Logix.Tag, Rectangle, TextBlock, Logix.Controller>>();
        private List<Tuple<Logix.Tag, StackPanel>> eStopList = new List<Tuple<Logix.Tag, StackPanel>>();
        private LogicClasses.Main main = null;
        #endregion

        #region Constructors
        public TLA_MainConveyorView(LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();

                main = _main;

                initializeMachineStatusList();
                initializeEStopList();
                
                main.Home_OnUpdate += main_Home_OnUpdate;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private void initializeMachineStatusList()
        {
            try
            {
                machineStatusList = new List<Tuple<Logix.Tag, Rectangle, TextBlock, Logix.Controller>>
                {
                    new Tuple<Logix.Tag, Rectangle, TextBlock, Logix.Controller>(new Logix.Tag
                    {
                        Name = "InPnP_Machine_Status.str_MachineStatus",
                        DataType = Logix.Tag.ATOMIC.STRING
                    }, InPnPRectangle, InPnPTextBlock, null),

                    new Tuple<Logix.Tag, Rectangle, TextBlock, Logix.Controller>(new Logix.Tag
                    {
                        Name = "Labelling_Machine_Status.str_MachineStatus",
                        DataType = Logix.Tag.ATOMIC.STRING
                    }, LabellingRectangle, LabellingTextBlock, null),

                    new Tuple<Logix.Tag, Rectangle, TextBlock, Logix.Controller>(new Logix.Tag
                    {
                        Name = "CenVis_Machine_Status.str_MachineStatus",
                        DataType = Logix.Tag.ATOMIC.STRING
                    }, CenVisRectangle, CenVisTextBlock, null),

                    new Tuple<Logix.Tag, Rectangle, TextBlock, Logix.Controller>(new Logix.Tag
                    {
                        Name = "Machine_Status.str_MachineStatus",
                        DataType = Logix.Tag.ATOMIC.STRING
                    }, CSSDRectangle, CSSDTextBlock, main.CSSDController),

                    new Tuple<Logix.Tag, Rectangle, TextBlock, Logix.Controller>(new Logix.Tag
                    {
                        Name = "Machine_Status.str_MachineStatus",
                        DataType = Logix.Tag.ATOMIC.STRING
                    }, DIMMRectangle, DIMMTextBlock, main.DIMMController),

                    new Tuple<Logix.Tag, Rectangle, TextBlock, Logix.Controller>(new Logix.Tag
                    {
                        Name = "Machine_Status.str_MachineStatus",
                        DataType = Logix.Tag.ATOMIC.STRING
                    }, PCIERectangle, PCIETextBlock, main.PCIEController),

                    new Tuple<Logix.Tag, Rectangle, TextBlock, Logix.Controller>(new Logix.Tag
                    {
                        Name = "OutPnP_Machine_Status.str_MachineStatus",
                        DataType = Logix.Tag.ATOMIC.STRING
                    }, OutPnPRectangle, OutPnPTextBlock, null)
                };

                //machineStatusList.ForEach(x =>
                //{
                //    try
                //    {
                //        if (x.Item4 != null)
                //        {
                //            x.Item4.Path = Classes.GlobalFunctions.PLC_Path;
                //            x.Item4.Timeout = Convert.ToInt32(Classes.GlobalFunctions.PLC_Timeout.ToString());

                //            for (int i = 0; i < 5; i++)
                //            {
                //                if (x.Item4.Connect() != Logix.ResultCode.E_SUCCESS)
                //                {
                //                    FileLogger.logError($"ERROR - PLC_Connect ({i})\nIP= {x.Item4.IPAddress}, Path={x.Item4.Path}, Timeout={x.Item4.Timeout}\n {x.Item4.ErrorCode} : {x.Item4.ErrorString}", "Tracking Bad TCP PLC_Connect");

                //                    if (x.Item4.IsConnected)
                //                    {
                //                        break;
                //                    }
                //                }
                //                else
                //                {
                //                    FileLogger.logError($"PLC_Connect(TcpAB) OK\nIP={x.Item4.IPAddress}, Path={x.Item4.Path}, timeout={x.Item4.Timeout}\n", "Tracking Good TCP PLC_Connect");
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //    catch (Exception exception)
                //    {
                //        FileLogger.logError(exception.Message, exception.ToString());
                //    }
                //});
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeEStopList()
        {
            try
            {
                eStopList = new List<Tuple<Logix.Tag, StackPanel>>
                {
                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO201_I2_2_Z1_EStopButton_1",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone1EStop1StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO201_I2_3_Z1_EStopButton_2",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone1EStop2StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO230_I1_5_Z30A_EStopButton_1",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone30AEStop1StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO230_I1_6_Z30A_EStopButton_2",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone30AEStop2StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO203_I4_2_Z3B_EStopButton_1",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone3BEStop1StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO203_I4_3_Z3B_EStopButton_2",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone3BEStop2StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO204_I6_2_Z4C_EStopButton_1",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone4CEStop1StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO204_I6_3_Z4C_EStopButton_2",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone4CEStop2StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO206_I4_2_Z6B_EStopButton_1",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone6BEStop1StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO206_I4_3_Z6B_EStopButton_2",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone6BEStop2StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO207_I6_2_Z7C_EStopButton_1",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone7CEStop1StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO207_I6_3_Z7C_EStopButton_2",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone7CEStop2StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO209_I2_2_Z9_EStopButton_1",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone9EStop1StackPanel),

                    new Tuple<Logix.Tag, StackPanel>(new Logix.Tag
                    {
                        Name = "In_PIO209_I2_3_Z9_EStopButton_2",
                        DataType = Logix.Tag.ATOMIC.BOOL
                    }, Zone9EStop2StackPanel)
                };
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateEventMethods
        private void main_Home_OnUpdate()
        {
            try
            {
                machineStatusList.ForEach(x =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(x.Item1.Name))
                        {
                            if (x.Item4 == null)
                            {
                                main.MyPLC.ReadTag(x.Item1);
                            }
                            else
                            {
                                x.Item4.ReadTag(x.Item1);
                            }

                            if (x.Item1.Value != null)
                            {
                                if (!string.IsNullOrEmpty(x.Item1.Value.ToString()))
                                {
                                    Brush brush = Brushes.Transparent;

                                    if (machineStatusDictionary.ContainsKey(x.Item1.Value.ToString()))
                                    {
                                        brush = machineStatusDictionary[x.Item1.Value.ToString()];
                                    }

                                    Dispatcher.Invoke(new Action(() =>
                                    {
                                        try
                                        {
                                            if (x.Item2.Fill != brush)
                                            {
                                                x.Item2.Fill = brush;
                                            }

                                            if (x.Item3.Foreground != brush)
                                            {
                                                x.Item3.Foreground = brush;
                                            }

                                            if (x.Item3.Text != x.Item1.Value.ToString())
                                            {
                                                x.Item3.Text = x.Item1.Value.ToString();
                                            }
                                        }
                                        catch (Exception exception)
                                        {
                                            FileLogger.logError(exception.Message, exception.ToString());
                                        }
                                    }));
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                });

                eStopList.ForEach(x =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(x.Item1.Name))
                        {
                            main.MyPLC.ReadTag(x.Item1);

                            if (x.Item1.Value != null)
                            {
                                Dispatcher.Invoke(new Action(() =>
                                {
                                    x.Item2.Visibility = Convert.ToBoolean(x.Item1.Value) ? Visibility.Collapsed : Visibility.Visible;
                                }));
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
        }
        #endregion
    }
}