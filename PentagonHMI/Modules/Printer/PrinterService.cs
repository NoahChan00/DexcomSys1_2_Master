using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using Utilities;

namespace PentagonHMI
{
    public class PrinterService
    {
        #region PrivateFields
        private List<PrinterModel> printerList = new List<PrinterModel>();
        private LogicClasses.Main _Main = null;
        //private TagGroup tgrp_Trigger = new TagGroup();
        #endregion

        #region Constructor
        public PrinterService(ref LogicClasses.Main _main)
        {
            try
            {
                initializePrinter();
                initializeOpc(_main);
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private void initializePrinter()
        {
            try
            {
                printerList = new List<PrinterModel>
                {
                    new PrinterModel
                    {
                        PortName = "COM1",
                        TriggerPrintTag = new Logix.Tag
                        {
                            Name = "bool_TriggerPrinterStart1",
                            DataType = Logix.Tag.ATOMIC.BOOL
                        },
                        TriggerPrintDoneTag = new Logix.Tag
                        {
                            Name = "bool_TriggerPrinterDone1",
                            DataType = Logix.Tag.ATOMIC.BOOL
                        },
                        TriggerPrintFailTag = new Logix.Tag
                        {
                            Name = "bool_TriggerPrinterFail1",
                            DataType = Logix.Tag.ATOMIC.BOOL
                        },
                        LabelCodeTag = new Logix.Tag
                        {
                            Name = "LM_Data.strAssetTag1",
                            DataType = Logix.Tag.ATOMIC.STRING
                        },
                        LabelFileDirectory = @"C:\Users\User\Desktop\Zebra Printer\AssetTagssss.prn"
                    },
                    new PrinterModel
                    {
                        PortName = "COM2",
                        TriggerPrintTag = new Logix.Tag
                        {
                            Name = "bool_TriggerPrinterStart2",
                            DataType = Logix.Tag.ATOMIC.BOOL
                        },
                        TriggerPrintDoneTag = new Logix.Tag
                        {
                            Name = "bool_TriggerPrinterDone2",
                            DataType = Logix.Tag.ATOMIC.BOOL
                        },
                        TriggerPrintFailTag = new Logix.Tag
                        {
                            Name = "bool_TriggerPrinterFail2",
                            DataType = Logix.Tag.ATOMIC.BOOL
                        },
                        LabelCodeTag = new Logix.Tag
                        {
                            Name = "LM_Data.strAssetTag2",
                            DataType = Logix.Tag.ATOMIC.STRING
                        },
                        LabelFileDirectory = @"C:\Users\User\Desktop\Zebra Printer\AssetTagssss.prn"
                    },
                    new PrinterModel
                    {
                        PortName = "COM3",
                        TriggerPrintTag = new Logix.Tag
                        {
                            Name = "bool_TriggerPrinterStart3",
                            DataType = Logix.Tag.ATOMIC.BOOL
                        },
                        TriggerPrintDoneTag = new Logix.Tag
                        {
                            Name = "bool_TriggerPrinterDone3",
                            DataType = Logix.Tag.ATOMIC.BOOL
                        },
                        TriggerPrintFailTag = new Logix.Tag
                        {
                            Name = "bool_TriggerPrinterFail3",
                            DataType = Logix.Tag.ATOMIC.BOOL
                        },
                        LabelCodeTag = new Logix.Tag
                        {
                            Name = "LM_Data.strAssetTag3",
                            DataType = Logix.Tag.ATOMIC.STRING
                        },
                        LabelFileDirectory = @"C:\Users\User\Desktop\Zebra Printer\AssetTagssss.prn"
                    }
                };

                printerList.ForEach(x =>
                {
                    try
                    {
                        x.SerialPort = new SerialPort(x.PortName, 9600, Parity.None, 8);
                        x.SerialPort.Open();

                        //x.Prn = File.ReadAllText(x.LabelFileDirectory).Replace("UFO>51234>6->51234>65", "{0}").Replace("UFO1234-12345", "{1}");

                        //tgrp_Trigger.AddTag(x.TriggerPrintTag);
                        //tgrp_Trigger.AddTag(x.LabelCodeTag);
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

        private void initializeOpc(LogicClasses.Main _main)
        {
            try
            {
                _Main = _main;
                _Main.OnFastUpdate += main_Home_OnUpdate;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateEventMethods
        //private void Update()
        //{
        //    _Main.MyPLC.GroupRead(tgrp_Trigger);
        //    printerList.ForEach(x =>
        //    {
        //        try
        //        {
        //            if (Convert.ToBoolean(x.TriggerPrintTag?.Value ?? false))
        //            {
        //                _Main.OPC.Write(x.TriggerPrintTag.Name, false);
        //                x.SerialPort.WriteLine(string.Format(x.Prn, x.LabelCodeTag.Value.ToString(), x.LabelCodeTag.Value.ToString()));
        //                _Main.OPC.Write(x.TriggerPrintDoneTag.Name, true);
        //            }
        //        }
        //        catch (Exception exception)
        //        {
        //            FileLogger.logError(exception.Message, exception.ToString());
        //            _Main.OPC.Write(x.TriggerPrintFailTag.Name, true);
        //        }
        //    });
        //}


        private void main_Home_OnUpdate()
        {
            try
            {
                printerList.ForEach(x =>
                {
                    try
                    {
                        _Main.MyPLC.ReadTag(x.TriggerPrintTag);

                        if (x.TriggerPrintTag.Value != null)
                        {
                            if (Convert.ToBoolean(x.TriggerPrintTag.Value))
                            {
                                x.TriggerPrintTag.Value = false;
                                _Main.MyPLC.WriteTag(x.TriggerPrintTag);

                                _Main.MyPLC.ReadTag(x.LabelCodeTag);
                                if (x.LabelCodeTag.Value == null)
                                {
                                    x.TriggerPrintDoneTag.Value = false;
                                    x.TriggerPrintFailTag.Value = true;
                                }
                                else
                                {
                                    string text = File.ReadAllText(x.LabelFileDirectory);

                                    if (text.Contains("UFO>51234>6->51234>65") && text.Contains("UFO1234-12345"))
                                    {
                                        text = text.Replace("UFO>51234>6->51234>65", x.LabelCodeTag.Value.ToString());
                                        text = text.Replace("UFO1234-12345", x.LabelCodeTag.Value.ToString());
                                        x.SerialPort.WriteLine(text);

                                        x.TriggerPrintDoneTag.Value = true;
                                        x.TriggerPrintFailTag.Value = false;
                                    }
                                    else
                                    {
                                        x.TriggerPrintDoneTag.Value = false;
                                        x.TriggerPrintFailTag.Value = true;
                                    }
                                }

                                _Main.MyPLC.WriteTag(x.TriggerPrintDoneTag);
                                _Main.MyPLC.WriteTag(x.TriggerPrintFailTag);
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