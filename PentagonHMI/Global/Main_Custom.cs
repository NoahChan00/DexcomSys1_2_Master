using CsvHelper;
using PentagonHMI.Classes;
using SimpleDatabase;
using SimpleOPC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using Utilities;

namespace PentagonHMI
{
    public class Main_Custom
    {
        private LogicClasses.Main _Main;
        private VanillaDB.DataDBCall DBCall;
        private SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);
        private string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
        private Dictionary<string, string> shuttleBarcodeDictionary = new Dictionary<string, string>();
        private Dictionary<string, string> robotGripperDictionary = new Dictionary<string, string>();
        private string[] OEEShift = new string[3];
        private string OEELineShift = "";
        public INGEAR_Opc OPC;

        #region OEETags

        private string StartDateTimeTag = "OEE_Tags.str_HMI_OEEStartDateTime";
        private string TotalPassTag = "OEE_Tags.dint_Total_Pass";
        private string TotalFailTag = "OEE_Tags.dint_Total_Fail";
        private string TotalTimeTag = "OEE_Tags.dint_MachineTotalTimeAccSec";
        private string ProductiveTimeTag = "OEE_Tags.dint_MachineProductiveTimeAccSec";
        private string StandbyTimeTag = "OEE_Tags.dint_MachineStandbyTimeAccSec";
        private string EngineeringTimeTag = "OEE_Tags.dint_MachineEngineeringTimeAccSec";
        private string NonScheduledTimeTag = "OEE_Tags.dint_MachineNonScheduledTimeAccSec";
        private string MachineScheduledDownTimeTag = "OEE_Tags.dint_MachineScheduledDownTimeAccSec";
        private string MachineUncheduledDownTimeTag = "OEE_Tags.dint_MachineUnscheduledDownTimeAccSec";
        private string MeanDownTimeTag = "OEE_Tags.dint_MeanDownTimeSec";
        private string SoftJamTag = "OEE_Tags.dint_SoftJam";
        private string HardJamTag = "OEE_Tags.dint_HardJam";
        private string MTBATag = "OEE_Tags.dint_MTBASec";
        private string MTBFTag = "OEE_Tags.dint_MTBFSec";
        private string Z1_TotalIncomingPartFailTag = "OEE_Tags.dint_Z1_Total_PartFail";
        private string Z2_TotalIncomingPartFailTag = "OEE_Tags.dint_Z2_Total_PartFail";
        private string bool_HMIOEEReset = "OEE_Tags.bool_OEE_Reset";

        #endregion OEETags

        public Main_Custom(LogicClasses.Main main)
        {
            _Main = main;
            OPC = _Main.OPC;
            DBCall = new VanillaDB.DataDBCall(Connstr);
            if(GlobalFunctions.ProjectType == ProjectType.DIMM)
            {
                initializeShuttleBarcodeDictionary();
                initializeRobotGripperDictionary();
            }

            if((GlobalFunctions.ProjectType == ProjectType.ARCADIA && GlobalFunctions.StationType == StationType.VISION) || GlobalFunctions.ProjectType == ProjectType.TLA)
            {
                TagRename();
            }
        }

        private void TagRename()
        {
            if(GlobalFunctions.ProjectType == ProjectType.TLA && GlobalFunctions.StationType == StationType.ARCADIA_Main)
            {
                bool_HMIOEEReset = "Labelling_OEE_Tags.bool_OEE_Reset";

                StartDateTimeTag = "Labelling_OEE_Tags.str_HMI_OEEStartDateTime";
                TotalPassTag = "Labelling_OEE_Tags.dint_Total_Pass";
                TotalFailTag = "Labelling_OEE_Tags.dint_Total_Fail";
                TotalTimeTag = "Labelling_OEE_Tags.dint_MachineTotalTimeAccSec";
                ProductiveTimeTag = "Labelling_OEE_Tags.dint_MachineProductiveTimeAccSec";
                StandbyTimeTag = "Labelling_OEE_Tags.dint_MachineStandbyTimeAccSec";
                EngineeringTimeTag = "Labelling_OEE_Tags.dint_MachineEngineeringTimeAccSec";
                NonScheduledTimeTag = "Labelling_OEE_Tags.dint_MachineNonScheduledTimeAccSec";
                MachineScheduledDownTimeTag = "Labelling_OEE_Tags.dint_MachineScheduledDownTimeAccSec";
                MachineUncheduledDownTimeTag = "Labelling_OEE_Tags.dint_MachineUnscheduledDownTimeAccSec";
                MeanDownTimeTag = "Labelling_OEE_Tags.dint_MeanDownTimeSec";
                SoftJamTag = "Labelling_OEE_Tags.dint_SoftJam";
                HardJamTag = "Labelling_OEE_Tags.dint_HardJam";
                MTBATag = "Labelling_OEE_Tags.dint_MTBASec";
                MTBFTag = "Labelling_OEE_Tags.dint_MTBFSec";
            }
            else if(_Main.MachineName.ToUpper().Contains("FINAL"))
            {
                bool_HMIOEEReset = "OutputPnP_OEE_Tags.bool_OEE_Reset";

                StartDateTimeTag = "OutputPnP_OEE_Tags.str_HMI_OEEStartDateTime";
                TotalPassTag = "OutputPnP_OEE_Tags.dint_Total_Pass";
                TotalFailTag = "OutputPnP_OEE_Tags.dint_Total_Fail";
                TotalTimeTag = "OutputPnP_OEE_Tags.dint_MachineTotalTimeAccSec";
                ProductiveTimeTag = "OutputPnP_OEE_Tags.dint_MachineProductiveTimeAccSec";
                StandbyTimeTag = "OutputPnP_OEE_Tags.dint_MachineStandbyTimeAccSec";
                EngineeringTimeTag = "OutputPnP_OEE_Tags.dint_MachineEngineeringTimeAccSec";
                NonScheduledTimeTag = "OutputPnP_OEE_Tags.dint_MachineNonScheduledTimeAccSec";
                MachineScheduledDownTimeTag = "OutputPnP_OEE_Tags.dint_MachineScheduledDownTimeAccSec";
                MachineUncheduledDownTimeTag = "OutputPnP_OEE_Tags.dint_MachineUnscheduledDownTimeAccSec";
                MeanDownTimeTag = "OutputPnP_OEE_Tags.dint_MeanDownTimeSec";
                SoftJamTag = "OutputPnP_OEE_Tags.dint_SoftJam";
                HardJamTag = "OutputPnP_OEE_Tags.dint_HardJam";
                MTBATag = "OutputPnP_OEE_Tags.dint_MTBASec";
                MTBFTag = "OutputPnP_OEE_Tags.dint_MTBFSec";
            }
            else if(_Main.MachineName.ToUpper().Contains("CENTRAL"))
            {
                bool_HMIOEEReset = "CenVis_OEE_Tags.bool_OEE_Reset";

                StartDateTimeTag = "CenVis_OEE_Tags.str_HMI_OEEStartDateTime";
                TotalPassTag = "CenVis_OEE_Tags.dint_Total_Pass";
                TotalFailTag = "CenVis_OEE_Tags.dint_Total_Fail";
                TotalTimeTag = "CenVis_OEE_Tags.dint_MachineTotalTimeAccSec";
                ProductiveTimeTag = "CenVis_OEE_Tags.dint_MachineProductiveTimeAccSec";
                StandbyTimeTag = "CenVis_OEE_Tags.dint_MachineStandbyTimeAccSec";
                EngineeringTimeTag = "CenVis_OEE_Tags.dint_MachineEngineeringTimeAccSec";
                NonScheduledTimeTag = "CenVis_OEE_Tags.dint_MachineNonScheduledTimeAccSec";
                MachineScheduledDownTimeTag = "CenVis_OEE_Tags.dint_MachineScheduledDownTimeAccSec";
                MachineUncheduledDownTimeTag = "CenVis_OEE_Tags.dint_MachineUnscheduledDownTimeAccSec";
                MeanDownTimeTag = "CenVis_OEE_Tags.dint_MeanDownTimeSec";
                SoftJamTag = "CenVis_OEE_Tags.dint_SoftJam";
                HardJamTag = "CenVis_OEE_Tags.dint_HardJam";
                MTBATag = "CenVis_OEE_Tags.dint_MTBASec";
                MTBFTag = "CenVis_OEE_Tags.dint_MTBFSec";
                Z1_TotalIncomingPartFailTag = "CenVis_OEE_Tags.dint_Z1_Total_PartFail";
                Z2_TotalIncomingPartFailTag = "CenVis_OEE_Tags.dint_Z2_Total_PartFail";
            }
        }

        public void UpdateOEEShift()
        {
            try
            {
                string ErrMsg = string.Empty;
                if(GlobalFunctions.ProjectType == ProjectType.ARCADIA && GlobalFunctions.StationType == StationType.ARCADIA_Main) // !!
                {
                    try
                    {
                        DataTable OEEShiftMachine = _Main.MainSQLer.Exec_DTSelect(@"Select * from Shift where ShiftID != '99' AND StationID = '" + _Main.StationID + @"' AND Active = 1");

                        if(OEEShiftMachine != null)
                        {
                            foreach(DataRow dr in OEEShiftMachine.Rows)
                            {
                                string ShiftID = dr["ShiftID"].ToString();

                                if(DateTime.Now > Convert.ToDateTime(dr["NextShiftDT"]))
                                {
                                    DateTime ShiftDateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(dr["ShiftTime"]).ToShortTimeString());

                                    if(DateTime.Now > ShiftDateTime)
                                        ShiftDateTime = ShiftDateTime.AddDays(1);

                                    DBCall.Shift_Reset(ShiftID, Classes.GlobalFunctions.StationName.ToString(), ShiftDateTime.ToString("yyyy/MM/dd HH:mm:ss"), "User", ref ErrMsg);
                                }
                            }
                        }
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }
                else if(GlobalFunctions.ProjectType == ProjectType.ARCADIA)
                {
                    try
                    {
                        bool skip = false;
                        DataTable OEEShiftLine = _Main.MainSQLer.Exec_DTSelect(@"Select * from Shift where ShiftID = '99' AND StationID = '" + _Main.StationID + @"' AND Active = 1");
                        if(OEEShiftLine != null)
                        {
                            if(OEEShiftLine.Rows.Count == 1)
                            {
                                DataRow dr = OEEShiftLine.Rows[0];
                                string ShiftID = dr["ShiftID"].ToString();

                                if(string.IsNullOrEmpty(OEELineShift))
                                {
                                    OEELineShift = dr["NextShiftDT"].ToString();
                                }
                                else if(OEELineShift != dr["NextShiftDT"].ToString())
                                {
                                    LogOEE(ShiftID);

                                    if(!OPC.Write(bool_HMIOEEReset, true))
                                        Utilities.FileLogger.logError("OEE reset failed", "Write to PLC reset failed");

                                    OEELineShift = dr["NextShiftDT"].ToString();

                                    DataTable OEEShiftUpdate = _Main.MainSQLer.Exec_DTSelect(@"Select * from Shift where ShiftID != '99' AND StationID = '" + _Main.StationID + @"' AND Active = 1 ORDER BY ShiftID");
                                    foreach(DataRow dr1 in OEEShiftUpdate.Rows)
                                    {
                                        OEEShift[Convert.ToInt32(dr1["ShiftID"]) - 1] = dr1["NextShiftDT"].ToString();
                                    }
                                    skip = true;
                                }
                            }
                        }

                        if(!skip)
                        {
                            DataTable OEEShiftMachine = _Main.MainSQLer.Exec_DTSelect(@"Select * from Shift where ShiftID != '99' AND StationID = '" + _Main.StationID + @"' AND Active = 1 ORDER BY ShiftID");

                            if(OEEShiftMachine != null)
                            {
                                foreach(DataRow dr in OEEShiftMachine.Rows)
                                {
                                    string ShiftID = dr["ShiftID"].ToString();
                                    if(string.IsNullOrEmpty(OEEShift[Convert.ToInt32(ShiftID) - 1]))
                                    {
                                        OEEShift[Convert.ToInt32(ShiftID) - 1] = dr["NextShiftDT"].ToString();
                                    }
                                    else
                                    {
                                        if(OEEShift[Convert.ToInt32(ShiftID) - 1] != dr["NextShiftDT"].ToString())
                                        {
                                            if(Convert.ToDateTime(dr["NextShiftDT"]) > Convert.ToDateTime(OEEShift[Convert.ToInt32(ShiftID) - 1]))
                                            {
                                                LogOEE(ShiftID);
                                                if(!OPC.Write(bool_HMIOEEReset, true))
                                                    Utilities.FileLogger.logError("OEE reset failed", "Write to PLC reset failed");
                                                OEEShift[Convert.ToInt32(ShiftID) - 1] = dr["NextShiftDT"].ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }
                else
                {
                    try
                    {
                        DataTable OEEShift = _Main.SQLer.Exec_DTSelect(@"Select * from Shift where ShiftID != '99' AND StationID = '" + _Main.StationID + @"' AND Active = 1 ORDER BY ShiftID");
                        string ShiftID = "";
                        foreach(DataRow dr in OEEShift.Rows)
                        {
                            if(DateTime.Now > Convert.ToDateTime(dr["NextShiftDT"]))
                            {
                                ShiftID = dr["ShiftID"].ToString();
                                break;
                            }
                        }

                        if(!string.IsNullOrEmpty(ShiftID))
                        {
                            LogOEE(ShiftID);

                            if(GlobalFunctions.ProjectType == ProjectType.DIMM)
                            {
                                logShuttleBarcode();
                                logRobotGripper();
                            }

                            if(GlobalFunctions.ProjectType == ProjectType.TLA && GlobalFunctions.StationType == StationType.ARCADIA_Main)
                            {
                                _Main.CSSDController.WriteTag(new Logix.Tag
                                {
                                    Name = "OEE_Tags.bool_OEE_Reset",
                                    DataType = Logix.Tag.ATOMIC.BOOL,
                                    Value = true
                                });

                                _Main.DIMMController.WriteTag(new Logix.Tag
                                {
                                    Name = "OEE_Tags.bool_OEE_Reset",
                                    DataType = Logix.Tag.ATOMIC.BOOL,
                                    Value = true
                                });

                                _Main.PCIEController.WriteTag(new Logix.Tag
                                {
                                    Name = "OEE_Tags.bool_OEE_Reset",
                                    DataType = Logix.Tag.ATOMIC.BOOL,
                                    Value = true
                                });

                                _Main.OPC.Write("OutputPnP_OEE_Tags.bool_OEE_Reset", true);
                                _Main.OPC.Write("CenVis_OEE_Tags.bool_OEE_Reset", true);
                            }

                            if(!_Main.OPC.Write(bool_HMIOEEReset, true))
                            {
                                Utilities.FileLogger.logError("OEE reset failed", "Write to PLC reset failed");
                            }
                            else
                            {
                                OEEShift = DBCall.Shift_Select(ShiftID, Classes.GlobalFunctions.StationName.ToString(), ref ErrMsg);

                                if(ErrMsg == "")
                                {
                                    DateTime ShiftDateTime = DateTime.Now;
                                    foreach(DataRow DR in OEEShift.Rows)
                                    {
                                        ShiftDateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(DR["ShiftTime"]).ToShortTimeString());

                                        if(DateTime.Now > ShiftDateTime)
                                            ShiftDateTime = ShiftDateTime.AddDays(1);
                                    }

                                    if(ShiftDateTime != null)
                                        DBCall.Shift_Reset(ShiftID, Classes.GlobalFunctions.StationName.ToString(), ShiftDateTime.ToString("yyyy/MM/dd HH:mm:ss"), "User", ref ErrMsg);
                                }
                            }
                        }
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void LogOEE(string ShiftID)
        {
            try
            {
                string LogsPath = Utilities.FileLogger.DefaultLocation_Time + Path.DirectorySeparatorChar + @"OEE";
                if(!Directory.Exists(LogsPath))
                { Directory.CreateDirectory(LogsPath); }
                string _Path = Path.Combine(LogsPath, $"OEE_{DateTime.Now.ToString("yyyy-MMM-dd")}.csv");
                bool HasFile = File.Exists(_Path);
                using(FileStream stream = new FileStream(_Path,
                   HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                {
                    using(var writer = new StreamWriter(stream))
                    {
                        using(var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                        {
                            if(!HasFile)
                            {
                                csv.WriteField("DateTime");
                                csv.WriteField("ShiftID");
                                csv.WriteField("ShiftStartDateTime");
                                csv.WriteField("TotalPass (count)");
                                csv.WriteField("TotalFail (count)");
                                csv.WriteField("Zone 1 IncomingPartFail (count)");
                                csv.WriteField("Zone 2 IncomingPartFail (count)");
                                csv.WriteField("TotalTime (hh:mm:ss)");
                                csv.WriteField("ProductiveTime (hh:mm:ss)");
                                csv.WriteField("StandbyTime (hh:mm:ss)");
                                csv.WriteField("EngineeringTime (hh:mm:ss)");
                                csv.WriteField("ScheduledDownTime (hh:mm:ss)");
                                csv.WriteField("UnscheduledDownTime (hh:mm:ss)");
                                csv.WriteField("NonScheduledTime (hh:mm:ss)");
                                csv.WriteField("MeanDownTime (hh:mm:ss)");
                                csv.WriteField("SoftJam (times)");
                                csv.WriteField("HardJam (times)");
                                csv.WriteField("MTBA (hh:mm:ss)");
                                csv.WriteField("MTBF (hh:mm:ss)");
                                csv.WriteField("IdealCycleTime (hh:mm:ss)");

                                csv.WriteField("EquipmentUptime (hh:mm:ss)");
                                csv.WriteField("OperationTime (hh:mm:ss)");
                                csv.WriteField("Quality (%)");
                                csv.WriteField("Performance (%)");
                                csv.WriteField("Availability (%)");
                                csv.WriteField("OEE (%)");
                                csv.WriteField("Loading (%)");
                                csv.WriteField("TEEP (%)");
                                csv.WriteField("DeltaTime (hh:mm:ss)");

                                csv.NextRecord();
                            }

                            double Productive, Standby, Engineering, _Shift, NonSchedule, TotalPass, TotalFail,
                            IdealCycleTime, TotalCount, EquipmentUpTime, OperationTime, Quality, Performance,
                            Availability, OEE, Loading, Teep, DeltaTime;

                            csv.WriteField(DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff"));
                            string ShiftStartTime = FormatString(Grouping.stringtime, OPC.Read<string>(StartDateTimeTag));
                            csv.WriteField(ShiftStartTime);

                            TotalPass = OPC.Read<double>(TotalPassTag);
                            TotalFail = OPC.Read<double>(TotalFailTag);
                            _Shift = OPC.Read<double>(TotalTimeTag);
                            Productive = OPC.Read<double>(ProductiveTimeTag);
                            Standby = OPC.Read<double>(StandbyTimeTag);
                            Engineering = OPC.Read<double>(EngineeringTimeTag);
                            NonSchedule = OPC.Read<double>(NonScheduledTimeTag);

                            IdealCycleTime = _Main.IdealCycleTime;

                            TotalCount = TotalPass + TotalFail;
                            EquipmentUpTime = Productive + Standby + Engineering;
                            OperationTime = _Shift - NonSchedule;
                            double Total = TotalPass + TotalFail;
                            Quality = Total <= 0 ? 0 : TotalPass / (Total);
                            Performance = OperationTime <= 0 ? 0 : (IdealCycleTime * TotalCount) / OperationTime;
                            Availability = OperationTime <= 0 ? 0 : EquipmentUpTime / OperationTime;
                            OEE = Quality * Performance * Availability;
                            Loading = _Shift <= 0 ? 0 : EquipmentUpTime / _Shift;
                            Teep = OEE * Loading;
                            DeltaTime = _Shift - (Productive + Standby);

                            double QualityPercent = Quality * 100;
                            double PerformancePercent = Performance * 100;
                            double AvailabilityPercent = Availability * 100;
                            double OEEPercent = OEE * 100;
                            double LoadingPercent = Loading * 100;
                            double TeepPercent = Teep * 100;

                            csv.WriteField(TotalPass);
                            csv.WriteField(TotalFail);
                            csv.WriteField(OPC.Read<double>(Z1_TotalIncomingPartFailTag));
                            csv.WriteField(OPC.Read<double>(Z2_TotalIncomingPartFailTag));

                            csv.WriteField(FormatString(Grouping.sec, Productive));
                            csv.WriteField(FormatString(Grouping.sec, Standby));
                            csv.WriteField(FormatString(Grouping.sec, Engineering));
                            csv.WriteField(FormatString(Grouping.sec, OPC.Read<int>(MachineScheduledDownTimeTag)));
                            csv.WriteField(FormatString(Grouping.sec, OPC.Read<int>(MachineUncheduledDownTimeTag)));
                            csv.WriteField(FormatString(Grouping.sec, NonSchedule));
                            csv.WriteField(FormatString(Grouping.sec, OPC.Read<int>(MeanDownTimeTag)));
                            csv.WriteField(OPC.Read<int>(SoftJamTag));
                            csv.WriteField(OPC.Read<int>(HardJamTag));
                            csv.WriteField(FormatString(Grouping.sec, OPC.Read<int>(MTBATag)));
                            csv.WriteField(FormatString(Grouping.sec, OPC.Read<int>(MTBFTag)));
                            csv.WriteField(FormatString(Grouping.sec, Convert.ToInt32(_Main.IdealCycleTime)));

                            csv.WriteField(FormatString(Grouping.sec, EquipmentUpTime));
                            csv.WriteField(FormatString(Grouping.sec, OperationTime));
                            csv.WriteField(FormatString(Grouping.percent, QualityPercent));
                            csv.WriteField(FormatString(Grouping.percent, PerformancePercent));
                            csv.WriteField(FormatString(Grouping.percent, AvailabilityPercent));
                            csv.WriteField(FormatString(Grouping.percent, OEEPercent));
                            csv.WriteField(FormatString(Grouping.percent, LoadingPercent));
                            csv.WriteField(FormatString(Grouping.percent, TeepPercent));
                            csv.WriteField(FormatString(Grouping.sec, DeltaTime));

                            csv.NextRecord();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "OEELog");
            }
        }

        #region LotSummary

        private string LotID = "Lot_Info.HMI_LotID";
        private string DutInfo = "RecipeParams.DUTid";
        private string BatteryType = "Lot_Info.HMI_BatteryType";
        private string FirmwareVer = "RecipeParams.FirmwareVersion";
        private string ManufactureDate = "Lot_Info.HMI_ManufactureDate";
        private string ExpirationDate = "Lot_Info.HMI_Expiration_Date";
        private string OperatorID = "Lot_Info.HMI_OperatorID";
        private string StartDateTime = "Lot_OEE_Tags.str_HMI_OEEStartDateTime";
        private string SystemUpTime = "Lot_OEE_Tags.dint_MachineUpTimeAccSec";
        private string OperationTime = "Lot_OEE_Tags.dint_MachineProductiveTimeAccSec";
        private string DownTime = "Lot_OEE_Tags.dint_MachineUnscheduledDownTimeAccSec";
        private string IdleTime = "Lot_OEE_Tags.dint_MachineStandbyTimeAccSe";
        private string MaintenanceTime = "Lot_OEE_Tags.dint_MachineScheduledDownTimeAccSec";
        private string LotSize = "Lot_Info.HMI_Lot_Quantity";
        private string TotalQtyIn = "Lot_OEE_Tags.dint_TotalQuantityIn";
        private string TotalQtyOut = "Lot_OEE_Tags.dint_TotalProductiveUnit";
        private string OverallTotalPassed = "Lot_OEE_Tags.dint_Total_Pass";
        private string OverallTotalFailed = "Lot_OEE_Tags.dint_Total_Fail";
        private string TotalInputRobotPickFail = "Lot_Summary.dint_Total_Pick_Fail";
        private string TotalInputRobotPickDrop = "Lot_Summary.dint_Total_Pick_Drop";
        private string ProductionUPH = Tags.MainPage.dint_ProductionUPH.Name;
        private string SprintUPH = Tags.MainPage.dint_SprintUPH.Name;
        private string SoftJam = "Lot_OEE_Tags.dint_SoftJam";
        private string HardJam = "Lot_OEE_Tags.dint_HardJam";
        private string MTBA = "Lot_OEE_Tags.dint_MTBASec";
        private string MTBF = "Lot_OEE_Tags.dint_MTBFSec";
        private string OverallTotalYield = "Lot_OEE_Tags.dint_Quality";
        private string TotalPartFail = "Lot_OEE_Tags.dint_Total_PartFail";
        private string ShortShotFail = "ShortShot_Fail_Qty";
        private string OMFFFail = "OMFF_Fail_Qty";
        private string BrownStrainFail = "BrownStrainF_Fail_Qty";
        private string BatClipSurfaceFail = "BatClipSurface_Fail_Qty";
        private string FlashingFail = "Flashing_Fail_Qty";
        private string FodFail = "FOD_Fail_Qty";
        private string BugBubbleFail = "BugBubble_Fail_Qty";
        private string BrownStrainBFail = "BrownStrainB_Fail_Qty";
        private string MouseBiteFail = "MouseBite_Fail_Qty";
        private string TurretNestA = "TurretNest_A.Socket_Yield";
        private string TurretNestB = "TurretNest_B.Socket_Yield";
        private string TurretNestC = "TurretNest_C.Socket_Yield";
        private string TurretNestD = "TurretNest_D.Socket_Yield";
        private string TurretNestE = "TurretNest_E.Socket_Yield";
        private string TurretNestF = "TurretNest_F.Socket_Yield";
        private string TurretNestG = "TurretNest_G.Socket_Yield";
        private string TurretNestH = "TurretNest_H.Socket_Yield";
        #endregion LotSummary

        public void LotSummary()
        {
            try
            {
                var lotID = OPC.Read<string>(LotID); //

                string LogsPath = Utilities.FileLogger.DefaultLocation_Time + Path.DirectorySeparatorChar + @"LotSummary";
                if(!Directory.Exists(LogsPath))
                { Directory.CreateDirectory(LogsPath); }
                string _Path = Path.Combine(LogsPath, $"{lotID}_{DateTime.Now.ToString("yyyy-MMM-dd")}.csv");
                bool HasFile = File.Exists(_Path);
                using(FileStream stream = new FileStream(_Path,
                   HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                {
                    using(var writer = new StreamWriter(stream))
                    {
                        using(var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                        {
                            if(!HasFile)
                            {
                                csv.WriteField("Lot ID");
                                csv.WriteField("DUT Info");
                                csv.WriteField("BatteryType");
                                csv.WriteField("Firmware Version");
                                csv.WriteField("Manufacture Date");
                                csv.WriteField("Expiration Date");
                                csv.WriteField("Operator ID");
                                csv.WriteField("Start Date Time");
                                csv.WriteField("System Up Time");
                                csv.WriteField("Operation Time");
                                csv.WriteField("Down Time");
                                csv.WriteField("Idle Time ");
                                csv.WriteField("Maintenance Time");
                                csv.WriteField("Lot Size");
                                csv.WriteField("Total Quantity In");
                                csv.WriteField("Total Quantity Out");
                                csv.WriteField("Overall Total Passed");
                                csv.WriteField("Overall Total Failed");
                                csv.WriteField("Total Input Robot Pick Fail");
                                csv.WriteField("Total Input Robot Pick Drop ");
                                csv.WriteField("Production PUH");
                                csv.WriteField("Sprint UPH");
                                csv.WriteField("Soft Jam");
                                csv.WriteField("Hard Jam");
                                csv.WriteField("MTBA");
                                csv.WriteField("MTBF");
                                csv.WriteField("Overall Total Yield (%)");
                                if(GlobalFunctions.IsSystem1)
                                {
                                    csv.WriteField("Total Part Fail");
                                }
                                else
                                {
                                    csv.WriteField("Short Shot Fail");
                                    csv.WriteField("OMFF Fail");
                                    csv.WriteField("Brown Strain Front Fail");
                                    csv.WriteField("Battery Clip Surface Fail");
                                    csv.WriteField("Flashing Fail");
                                    csv.WriteField("FOD Fail");
                                    csv.WriteField("Live Bug Bubble Fail");
                                    csv.WriteField("Brown Strain Back Fail");
                                    csv.WriteField("Mouse Bite Fail");
                                }
                                csv.WriteField("Socket Yield A (%)");
                                csv.WriteField("Socket Yield B (%)");
                                csv.WriteField("Socket Yield C (%)");
                                csv.WriteField("Socket Yield D (%)");
                                csv.WriteField("Socket Yield E (%)");
                                csv.WriteField("Socket Yield F (%)");
                                csv.WriteField("Socket Yield G (%)");
                                csv.WriteField("Socket Yield H (%)");

                                csv.NextRecord();
                            }

                            csv.WriteField(OPC.Read<string>(LotID));
                            csv.WriteField(OPC.Read<string>(DutInfo));
                            csv.WriteField(OPC.Read<string>(BatteryType));
                            csv.WriteField(OPC.Read<string>(FirmwareVer));
                            csv.WriteField(OPC.Read<string>(ManufactureDate));
                            csv.WriteField(OPC.Read<string>(ExpirationDate));
                            csv.WriteField(OPC.Read<string>(OperatorID));
                            csv.WriteField(OPC.Read<string>(StartDateTime));
                            csv.WriteField(OPC.Read<int>(SystemUpTime));
                            csv.WriteField(OPC.Read<int>(OperationTime));
                            csv.WriteField(OPC.Read<int>(DownTime));
                            csv.WriteField(OPC.Read<int>(IdleTime));
                            csv.WriteField(OPC.Read<int>(MaintenanceTime));
                            csv.WriteField(OPC.Read<int>(LotSize));
                            csv.WriteField(OPC.Read<int>(TotalQtyIn));
                            csv.WriteField(OPC.Read<int>(TotalQtyOut));
                            csv.WriteField(OPC.Read<int>(OverallTotalPassed));
                            csv.WriteField(OPC.Read<int>(OverallTotalFailed));
                            csv.WriteField(OPC.Read<int>(TotalInputRobotPickFail));
                            csv.WriteField(OPC.Read<int>(TotalInputRobotPickDrop));
                            csv.WriteField(OPC.Read<int>(ProductionUPH));
                            csv.WriteField(OPC.Read<int>(SprintUPH));
                            csv.WriteField(OPC.Read<int>(SoftJam));
                            csv.WriteField(OPC.Read<int>(HardJam));
                            csv.WriteField(OPC.Read<int>(MTBA));
                            csv.WriteField(OPC.Read<int>(MTBF));
                            csv.WriteField(FormatString(Grouping.percent, OPC.Read<double>(OverallTotalYield)));
                            if(GlobalFunctions.IsSystem1)
                            {
                                csv.WriteField(OPC.Read<int>(TotalPartFail));
                            }
                            else
                            {
                                csv.WriteField(OPC.Read<int>(ShortShotFail));
                                csv.WriteField(OPC.Read<int>(OMFFFail));
                                csv.WriteField(OPC.Read<int>(BrownStrainFail));
                                csv.WriteField(OPC.Read<int>(BatClipSurfaceFail));
                                csv.WriteField(OPC.Read<int>(FlashingFail));
                                csv.WriteField(OPC.Read<int>(FodFail));
                                csv.WriteField(OPC.Read<int>(BugBubbleFail));
                                csv.WriteField(OPC.Read<int>(BrownStrainBFail));
                                csv.WriteField(OPC.Read<int>(MouseBiteFail));
                            }
                            csv.WriteField(OPC.Read<double>(TurretNestA));
                            csv.WriteField(OPC.Read<double>(TurretNestB));
                            csv.WriteField(OPC.Read<double>(TurretNestC));
                            csv.WriteField(OPC.Read<double>(TurretNestD));
                            csv.WriteField(OPC.Read<double>(TurretNestE));
                            csv.WriteField(OPC.Read<double>(TurretNestF));
                            csv.WriteField(OPC.Read<double>(TurretNestG));
                            csv.WriteField(OPC.Read<double>(TurretNestH));
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "LotSummary");
            }
        }

        public void LotSummaryCheck()
        {
            try
            {
                if(OPC.Read<bool>("HMI_Log_LotSummary") == true)
                {
                    //OPC.Write("HMI_Log_LotSummary", false);
                    LotSummary();
                    OPC.Write("HMI_Log_LotSummary", false);
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError("LotSummaryCheck", ex.Message);
            }
        }

        public void TestCSVCheck()
        {
            try
            {
                if(OPC.Read<bool>("HMI_Log_TestCSV") == true)
                {
                    OPC.Write("HMI_Log_TestCSV", false);
                    TestCSV();
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError("TestCSVCheck", ex.Message);
            }

            try
            {
                if(OPC.Read<bool>("HMI_Log_TestCSV_NewLog") == true)
                {
                    OPC.Write("HMI_Log_TestCSV_NewLog", false);
                    //
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError("TestCSVCheck_NewLog", ex.Message);
            }
        }

        public void TesterStnUnitTrackerCheck()
        {
            try
            {
                if(OPC.Read<bool>("HMI_Log_TestStationUnitTracker") == true)
                {
                    OPC.Write("HMI_Log_TestStationUnitTracker", false);
                    TesterStnUnitTracker();
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError("TesterStnUnitTrackerCheck", ex.Message);
            }
        }

        public void LaserStnUnitTrackerCheck()
        {
            try
            {
                if(OPC.Read<bool>("HMI_Log_LaserStationUnitTracker") == true)
                {
                    OPC.Write("HMI_Log_LaserStationUnitTracker", false);
                    LaserStnUnitTracker();
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError("LaserStnUnitTrackerCheck", ex.Message);
            }
        }

        public void UnldStnUnitTrackerCheck()
        {
            try
            {
                if(OPC.Read<bool>("HMI_Log_UldStationUnitTracker") == true)
                {
                    OPC.Write("HMI_Log_UldStationUnitTracker", false);
                    UnldStnUnitTracker();
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError("UnldStnUnitTrackerCheck", ex.Message);
            }
        }

        public void TnRStnUnitTrackerCheck()
        {
            try
            {
                if(OPC.Read<bool>("HMI_Log_TnRStationUnitTracker") == true)
                {
                    OPC.Write("HMI_Log_TnRStationUnitTracker", false);
                    TnRStnUnitTracker();
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError("HMI_Log_TnRStationUnitTracker", ex.Message);
            }
        }

        #region Test CSV

        private string PartNumber = "HMI_LogTestCSV[1].PartNumber";
        private string Lot = "Lot_Info.HMI_LotID";
        private string TXSN = "HMI_LogTestCSV[1].TxID";
        private string Result = "HMI_LogTestCSV[1].TFT_TEST.sPass";
        private string RejectCode = "HMI_LogTestCSV[1].RejectCode";
        private string RejectDesc = "HMI_LogTestCSV[1].RejectDesc";
        private string TimestampsTFT = "HMI_LogTestCSV[1].EnterStationTime";
        private string TFTPos = "HMI_LogTestCSV[1].TFT_POS";
        private string TFTBatteryType = "HMI_LogTestCSV[1].TFT_TEST.BatteryV.oBatteryTypeLim ";
        private string TestBatteryViDynamic = "HMI_LogTestCSV[1].TFT_TEST.BatteryV.iDynamic";
        private string TestBatteryViDynamicHL = "HMI_LogTestCSV[1].TFT_TEST.BatteryV.iDynamicHL";
        private string TestBatteryViDynamicLL = "HMI_LogTestCSV[1].TFT_TEST.BatteryV.iDynamicLL";
        private string TestBatteryViPass = "HMI_LogTestCSV[1].TFT_TEST.iPassArray.2";
        private string TestBatteryViStatic = "HMI_LogTestCSV[1].TFT_TEST.BatteryV.iStatic";
        private string TestBatteryViStaticHL = "HMI_LogTestCSV[1].TFT_TEST.BatteryV.iStaticHL";
        private string TestBatteryViStaticLL = "HMI_LogTestCSV[1].TFT_TEST.BatteryV.iStaticLL";
        private string TestBatteryViTime = "HMI_LogTestCSV[1].TFT_TEST.iTimeArray[2]";
        private string TestBatteryVoBatteryTypeLim = "HMI_LogTestCSV[1].TFT_TEST.BatteryV.oBatteryTypeLim ";
        private string TestiCmdCount = "HMI_LogTestCSV[1].TFT_TEST.iCmdCount";
        private string TestiTTime = "HMI_LogTestCSV[1].TFT_TEST.iTTime";
        private string TestNFCiPass = "HMI_LogTestCSV[1].TFT_TEST.iPassArray.0";
        private string TestNFCiTime = "HMI_LogTestCSV[1].TFT_TEST.iTimeArray[0]";
        private string TestNVMiBatteryType = "HMI_LogTestCSV[1].TFT_TEST.NVM.iBatteryType";
        private string TestNVMiPass = "HMI_LogTestCSV[1].TFT_TEST.iPassArray.5";
        private string TestNVMiTime = "HMI_LogTestCSV[1].TFT_TEST.iTimeArray[5]";
        private string TestNVMiTOM1 = "HMI_LogTestCSV[1].TFT_TEST.NVM.iTOM1";
        private string TestRadioiPass = "HMI_LogTestCSV[1].TFT_TEST.iPassArray.4";
        private string TestRadioiRSSIHL = "HMI_LogTestCSV[1].TFT_TEST.Radio.iRSSIHL";
        private string TestRadioiRSSILL = "HMI_LogTestCSV[1].TFT_TEST.Radio.iRSSILL";
        private string TestRadioiRXRSSI = "HMI_LogTestCSV[1].TFT_TEST.Radio.iRXRSSI";
        private string RxRSSI = "HMI_LogTestCSV[1].RxRSSI_32767";
        private string TestRadioiTime = "HMI_LogTestCSV[1].TFT_TEST.iTimeArray[4]";
        private string TestRadioiTXRSSI = "HMI_LogTestCSV[1].TFT_TEST.Radio.iTXRSSI";
        private string TestsPass = "HMI_LogTestCSV[1].TFT_TEST.sPass";
        private string TFTTestsStatus = "HMI_LogTestCSV[1].TFT_TEST.sStatus";
        private string TestsStorageiPass = "HMI_LogTestCSV[1].TFT_TEST.iPassArray.7";
        private string TestsStorageiTime = "HMI_LogTestCSV[1].TFT_TEST.iTimeArray[7]";
        private string TestTempiPass = "HMI_LogTestCSV[1].TFT_TEST.iPassArray.1";
        private string TestTempiTemp = "HMI_LogTestCSV[1].TFT_TEST.Temp.iTemp";
        private string TestTempiTempHL = "HMI_LogTestCSV[1].TFT_TEST.Temp.iTempHL";
        private string TestTempiTempLL = "HMI_LogTestCSV[1].TFT_TEST.Temp.iTempLL";
        private string TestTempiTime = "HMI_LogTestCSV[1].TFT_TEST.iTimeArray[1]";
        private string TestTOMiPass = "HMI_LogTestCSV[1].TFT_TEST.iPassArray.3";
        private string TestTOMiTime = "HMI_LogTestCSV[1].TFT_TEST.iTimeArray[3]";
        private string TestTUARTiPass = "HMI_LogTestCSV[1].TFT_TEST.iPassArray.6";
        private string TestTUARTiTime = "HMI_LogTestCSV[1].TFT_TEST.iTimeArray[6]";
        private string TestiSbRioSN = "HMI_LogTestCSV[1].TFT_TEST.iSbRioSN";
        private string TestiStationSoftware = "HMI_LogTestCSV[1].TFT_TEST.iStationSoftware";
        private string TestiNFCiFWVersion = "HMI_LogTestCSV[1].TFT_TEST.NFC.iFWVersion";
        private string TestiNFCiTXSN = "HMI_LogTestCSV[1].TFT_TEST.NFC.iTXSN";
        private string TestiNFCoFWVersionLim = "HMI_LogTestCSV[1].TFT_TEST.NFC.iFWVersionLim";
        private string TestoDUTId = "HMI_LogTestCSV[1].TFT_TEST.oDUTId";
        private string TestsStatus = "HMI_LogTestCSV[1].TFT_TEST.sStatus";
        private string TestTempiStatus = "HMI_LogTestCSV[1].TFT_TEST.Temp.iStatus";
        private string TestTempiStatusLim = "HMI_LogTestCSV[1].TFT_TEST.Temp.iStatusLim";
        private string IRxRSSI = "HMI_LogTestStn[1].TFT_TEST.Radio.iRXRSSI";

        #endregion Test CSV

        public void TestCSV()
        {
            try
            {
                string LogsPath = Utilities.FileLogger.DefaultLocation_Time + Path.DirectorySeparatorChar + @"TestCSV";
                if(!Directory.Exists(LogsPath))
                { Directory.CreateDirectory(LogsPath); }
                string _Path = Path.Combine(LogsPath, $"TestCSV_{DateTime.Now.ToString("yyyy-MMM-dd")}.csv");
                bool HasFile = File.Exists(_Path);
                using(FileStream stream = new FileStream(_Path,
                   HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                {
                    using(var writer = new StreamWriter(stream))
                    {
                        using(var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                        {
                            if(!HasFile)
                            {
                                csv.WriteField("PartNumber");
                                csv.WriteField("Lot");
                                csv.WriteField("TXSN");
                                csv.WriteField("Result");
                                csv.WriteField("Mean(CompletedDate|TOPM)");
                                csv.WriteField("Date[Mean(CompletedDate|TOPM)]");
                                csv.WriteField("RejectCode");
                                csv.WriteField("RejectDesc");
                                csv.WriteField("TOPM EQ");
                                csv.WriteField("Timestamps|TOPM");
                                csv.WriteField("Timestamps|Traypick");
                                csv.WriteField("TOPM Pos");
                                csv.WriteField("TOPM BatteryType");
                                csv.WriteField("TOPM TOPM_TESTBatteryViDynamic");
                                csv.WriteField("TOPM TOPM_TESTBatteryViDynamicHL");
                                csv.WriteField("TOPM TOPM_TESTBatteryViDynamicLL");
                                csv.WriteField("TOPM TOPM_TESTBatteryViPass");
                                csv.WriteField("TOPM TOPM_TESTBatteryViStatic");
                                csv.WriteField("TOPM TOPM_TESTBatteryViStaticHL");
                                csv.WriteField("TOPM TOPM_TESTBatteryViStaticLL");
                                csv.WriteField("TOPM TOPM_TESTBatteryViTime");
                                csv.WriteField("TOPM TOPM_TESTBatteryVoBatteryTypeLim");
                                csv.WriteField("TOPM TOPM_TESTiCmdCount");
                                csv.WriteField("TOPM TOPM_TESTiTTime");
                                csv.WriteField("TOPM TOPM_TESTNFCiPass");
                                csv.WriteField("TOPM TOPM_TESTNFCiTime");
                                csv.WriteField("TOPM TOPM_TESTNVMiBatteryType");
                                csv.WriteField("TOPM TOPM_TESTNVMiPass");
                                csv.WriteField("TOPM TOPM_TESTNVMiTime");
                                csv.WriteField("TOPM TOPM_TESTNVMiTOM1");
                                csv.WriteField("TOPM TOPM_TESTRadioiPass");
                                csv.WriteField("TOPM TOPM_TESTRadioiRSSIHL");
                                csv.WriteField("TOPM TOPM_TESTRadioiRSSILL");
                                csv.WriteField("TOPM TOPM_TESTRadioiRXRSSI");
                                csv.WriteField("RxRSSI=32767");
                                csv.WriteField("TOPM TOPM_TESTRadioiTime");
                                csv.WriteField("TOPM TOPM_TESTRadioiTXRSSI");
                                csv.WriteField("TOPM TOPM_TESTsPass");
                                csv.WriteField("TOPM TOPM_TESTsStatus");
                                csv.WriteField("TOPM TOPM_TESTsStorageiPass");
                                csv.WriteField("TOPM TOPM_TESTsStorageiTime");
                                csv.WriteField("TOPM TOPM_TESTTempiPass");
                                csv.WriteField("TOPM TOPM_TESTTempiTemp");
                                csv.WriteField("TOPM TOPM_TESTTempiTempHL");
                                csv.WriteField("TOPM TOPM_TESTTempiTempLL");
                                csv.WriteField("TOPM TOPM_TESTTempiTime");
                                csv.WriteField("TOPM TOPM_TESTTOMiPass");
                                csv.WriteField("TOPM TOPM_TESTTOMiTime");
                                csv.WriteField("TOPM TOPM_TESTUARTiPass");
                                csv.WriteField("TOPM TOPM_TESTUARTiTime");
                                csv.WriteField("TOPM_TESTiSbRioSN");
                                csv.WriteField("TOPM_TESTiStationSoftware");
                                csv.WriteField("TOPM_TESTNFCiFWVersion");
                                csv.WriteField("TOPM_TESTNFCiTXSN");
                                csv.WriteField("TOPM_TESTNFCoFWVersionLim");
                                csv.WriteField("TOPM_TESToDUTId");
                                csv.WriteField("TOPM_TESTsStatus");
                                csv.WriteField("TOPM_TESTTempiStatus");
                                csv.WriteField("TOPM_TESTTempiStatusLim");

                                csv.NextRecord();
                            }

                            var partNumber = OPC.Read<string[]>(PartNumber, typeof(string), 6);
                            var dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff");
                            var date = DateTime.Now.ToString("dd/MM/yyyy");
                            var txSN = OPC.Read<string[]>(TXSN, typeof(string), 6);
                            var result = OPC.Read<bool[]>(Result, typeof(bool), 6);
                            var rejectCode = OPC.Read<int[]>(RejectCode, typeof(int), 6);
                            var rejectDesc = OPC.Read<string[]>(RejectDesc, typeof(string), 6);
                            var timestampsTFT = OPC.Read<string[]>(TimestampsTFT, typeof(string), 6);
                            var tftPos = OPC.Read<int[]>(TFTPos, typeof(int), 6);
                            var tftBatteryType = OPC.Read<int[]>(TFTBatteryType, typeof(int), 6);
                            var testBatteryViDynamic = OPC.Read<int[]>(TestBatteryViDynamic, typeof(int), 6);
                            var testBatteryViDynamicHL = OPC.Read<int[]>(TestBatteryViDynamicHL, typeof(int), 6);
                            var testBatteryViDynamicLL = OPC.Read<int[]>(TestBatteryViDynamicLL, typeof(int), 6);
                            var testBatteryViPass = OPC.Read<bool[]>(TestBatteryViPass, typeof(bool), 6);
                            var testBatteryViStatic = OPC.Read<int[]>(TestBatteryViStatic, typeof(int), 6);
                            var testBatteryViStaticHL = OPC.Read<int[]>(TestBatteryViStaticHL, typeof(int), 6);
                            var testBatteryViStaticLL = OPC.Read<int[]>(TestBatteryViStaticLL, typeof(int), 6);
                            var testBatteryViTime = OPC.Read<int[]>(TestBatteryViTime, typeof(int), 6);
                            var testBatteryVoBatteryTypeLim = OPC.Read<int[]>(TestBatteryVoBatteryTypeLim, typeof(int), 6);
                            var testiCmdCount = OPC.Read<int[]>(TestiCmdCount, typeof(int), 6);
                            var testiTTime = OPC.Read<int[]>(TestiTTime, typeof(int), 6);
                            var testNFCiPass = OPC.Read<bool[]>(TestNFCiPass, typeof(bool), 6);
                            var testNFCiTime = OPC.Read<int[]>(TestNFCiTime, typeof(int), 6);
                            var testNVMiBatteryType = OPC.Read<int[]>(TestNVMiBatteryType, typeof(int), 6);
                            var testNVMiPass = OPC.Read<bool[]>(TestNVMiPass, typeof(bool), 6);
                            var testNVMiTime = OPC.Read<int[]>(TestNVMiTime, typeof(int), 6);
                            var testNVMiTOM1 = OPC.Read<int[]>(TestNVMiTOM1, typeof(int), 6);
                            var testRadioiPass = OPC.Read<bool[]>(TestRadioiPass, typeof(bool), 6);
                            var testRadioiRSSIHL = OPC.Read<int[]>(TestRadioiRSSIHL, typeof(int), 6);
                            var testRadioiRSSILL = OPC.Read<int[]>(TestRadioiRSSILL, typeof(int), 6);
                            var testRadioiRXRSSI = OPC.Read<int[]>(TestRadioiRXRSSI, typeof(int), 6);
                            var rxRSSI = OPC.Read<bool[]>(RxRSSI, typeof(bool), 6);
                            var testRadioiTime = OPC.Read<int[]>(TestRadioiTime, typeof(int), 6);
                            var testRadioiTXRSSI = OPC.Read<int[]>(TestRadioiTXRSSI, typeof(int), 6);
                            var testsPass = OPC.Read<bool[]>(TestsPass, typeof(bool), 6);
                            var tftTestsStatus = OPC.Read<int[]>(TFTTestsStatus, typeof(int), 6);
                            var testsStorageiPass = OPC.Read<bool[]>(TestsStorageiPass, typeof(bool), 6);
                            var testsStorageiTime = OPC.Read<int[]>(TestsStorageiTime, typeof(int), 6);
                            var testTempiPass = OPC.Read<bool[]>(TestTempiPass, typeof(bool), 6);
                            var testTempiTemp = OPC.Read<int[]>(TestTempiTemp, typeof(int), 6);
                            var testTempiTempHL = OPC.Read<int[]>(TestTempiTempHL, typeof(int), 6);
                            var testTempiTempLL = OPC.Read<int[]>(TestTempiTempLL, typeof(int), 6);
                            var testTempiTime = OPC.Read<int[]>(TestTempiTime, typeof(int), 6);
                            var testTOMiPass = OPC.Read<bool[]>(TestTOMiPass, typeof(bool), 6);
                            var testTOMiTime = OPC.Read<int[]>(TestTOMiTime, typeof(int), 6);
                            var testTUARTiPass = OPC.Read<bool[]>(TestTUARTiPass, typeof(bool), 6);
                            var testTUARTiTime = OPC.Read<int[]>(TestTUARTiTime, typeof(int), 6);
                            var testiSbRioSN = OPC.Read<string[]>(TestiSbRioSN, typeof(string), 6);
                            var testiStationSoftware = OPC.Read<string[]>(TestiStationSoftware, typeof(string), 6);
                            var testiNFCiFWVersion = OPC.Read<string[]>(TestiNFCiFWVersion, typeof(string), 6);
                            var testiNFCiTXSN = OPC.Read<int[]>(TestiNFCiTXSN, typeof(int), 6);
                            var testiNFCoFWVersionLim = OPC.Read<string[]>(TestiNFCoFWVersionLim, typeof(string), 6);
                            var testoDUTId = OPC.Read<string[]>(TestoDUTId, typeof(string), 6);
                            var testsStatus = OPC.Read<int[]>(TestsStatus, typeof(int), 6);
                            var testTempiStatus = OPC.Read<string[]>(TestTempiStatus, typeof(string), 6);
                            var testTempiStatusLim = OPC.Read<string[]>(TestTempiStatusLim, typeof(string), 6);

                            var dummyTXSNTester = OPC.Read<string[]>(DummyTXSNTester, typeof(string), 6);

                            var iRxRSSI = OPC.Read<int[]>(IRxRSSI, typeof(int), 6);

                            for(int i = 0; i < 6; i++)
                            {
                                string partNumberdb = _Main.SQLer.Exec_Scalar<string>("SELECT [Info] FROM dbo.Config WHERE Item = 'PartNumber'");
                                csv.WriteField(partNumberdb); //

                                csv.WriteField(partNumber[i]); //
                                csv.WriteField(OPC.Read<string>(Lot));

                                if(testsPass[i] == true) //
                                {
                                    csv.WriteField(txSN[i]);
                                }
                                else
                                {
                                    csv.WriteField(dummyTXSNTester[i]);
                                }
                                csv.WriteField(result[i] ? "1" : "0");
                                csv.WriteField(dateTime); //
                                csv.WriteField(rejectCode[i]); //
                                csv.WriteField(rejectDesc[i]);

                                string tOPMEQ = _Main.SQLer.Exec_Scalar<string>(" SELECT [TOPMEQ] FROM [dbo.Config]");
                                csv.WriteField(tOPMEQ); //

                                csv.WriteField(timestampsTFT[i]); // topm
                                csv.WriteField(dateTime); // traypick
                                csv.WriteField(tftPos[i]);
                                csv.WriteField(tftBatteryType[i]);
                                csv.WriteField(testBatteryViDynamic[i]);
                                csv.WriteField(testBatteryViDynamicHL[i]);
                                csv.WriteField(testBatteryViDynamicLL[i]);
                                csv.WriteField(testBatteryViPass[i] ? "1" : "0");
                                csv.WriteField(testBatteryViStatic[i]);
                                csv.WriteField(testBatteryViStaticHL[i]);
                                csv.WriteField(testBatteryViStaticLL[i]);
                                csv.WriteField(testBatteryViTime[i]);
                                csv.WriteField(testBatteryVoBatteryTypeLim[i]);
                                csv.WriteField(testiCmdCount[i]);
                                csv.WriteField(testiTTime[i]);
                                csv.WriteField(testNFCiPass[i] ? "1" : "0");
                                csv.WriteField(testNFCiTime[i]);
                                csv.WriteField(testNVMiBatteryType[i]);
                                csv.WriteField(testNVMiPass[i] ? "1" : "0");
                                csv.WriteField(testNVMiTime[i]);
                                csv.WriteField(testNVMiTOM1[i]);
                                csv.WriteField(testRadioiPass[i] ? "1" : "0");
                                csv.WriteField(testRadioiRSSIHL[i]);
                                csv.WriteField(testRadioiRSSILL[i]);
                                csv.WriteField(testRadioiRXRSSI[i]);
                                if(iRxRSSI[i] == 32767)
                                {
                                    csv.WriteField(rxRSSI[i] ? "1" : "0");
                                }

                                csv.WriteField(testRadioiTime[i]);
                                csv.WriteField(testRadioiTXRSSI[i]);
                                csv.WriteField(testsPass[i] ? "1" : "0");
                                csv.WriteField(tftTestsStatus[i]);
                                csv.WriteField(testsStorageiPass[i] ? "1" : "0");
                                csv.WriteField(testsStorageiTime[i]);
                                csv.WriteField(testTempiPass[i] ? "1" : "0");
                                csv.WriteField(testTempiTemp[i]);
                                csv.WriteField(testTempiTempHL[i]);
                                csv.WriteField(testTempiTempLL[i]);
                                csv.WriteField(testTempiTime[i]);
                                csv.WriteField(testTOMiPass[i] ? "1" : "0");
                                csv.WriteField(testTOMiTime[i]);
                                csv.WriteField(testTUARTiPass[i] ? "1" : "0");
                                csv.WriteField(testTUARTiTime[i]);
                                csv.WriteField(testiSbRioSN[i]);
                                csv.WriteField(testiStationSoftware[i]);
                                csv.WriteField(testiNFCiFWVersion[i]);
                                csv.WriteField(testiNFCiTXSN[i]);
                                csv.WriteField(testiNFCoFWVersionLim[i]);
                                csv.WriteField(testoDUTId[i]);
                                csv.WriteField(testsStatus[i]);
                                csv.WriteField(testTempiStatus[i]);
                                csv.WriteField(testTempiStatusLim[i]);

                                csv.NextRecord();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "TestCSV");
            }
        }

        #region TesterStn

        private string LotTester = "Lot_Info.HMI_LotID";
        private string PartNumberTester = "HM_LogTestUnitTracker[1].TFT_Test.oDUTId";
        private string RecipeNameTester = "HM_LogTestUnitTracker[1].RecipeName";
        private string TXSNTester = "HM_LogTestUnitTracker[1].TFT_Test.NFC.iTXSN";
        private string TestPositionTester = "HM_LogTestUnitTracker[1].TestPosition";
        private string OverallResultTester = "HM_LogTestUnitTracker[1].TFT_Test.sPass";
        private string PrRecValCreatedDateTester = "HM_LogTestUnitTracker[1].TimeRecorded";
        private string RejectCodeTester = "HM_LogTestUnitTracker.RejectCode";
        private string RejectDescTester = "HM_LogTestUnitTracker.RejectDesc";
        private string ProcessIDTester = "51";
        private string DummyTXSNTester = "HM_LogTestUnitTracker[1].Virtual_ID";

        #endregion TesterStn

        public void TesterStnUnitTracker()
        {
            try
            {
                string LogsPath = Utilities.FileLogger.DefaultLocation_Time + Path.DirectorySeparatorChar + @"TesterStnUnitTracker";
                if(!Directory.Exists(LogsPath))
                { Directory.CreateDirectory(LogsPath); }
                string _Path = Path.Combine(LogsPath, $"TesterStnUnitTracker_{DateTime.Now.ToString("yyyy-MMM-dd")}.csv");
                bool HasFile = File.Exists(_Path);
                using(FileStream stream = new FileStream(_Path,
                   HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                {
                    using(var writer = new StreamWriter(stream))
                    {
                        using(var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                        {
                            if(!HasFile)
                            {
                                csv.WriteField("Site");
                                csv.WriteField("EQNumber");
                                csv.WriteField("ProcessName");
                                csv.WriteField("Lot");
                                csv.WriteField("PartNumber");
                                csv.WriteField("RecipeName");
                                csv.WriteField("TxCreateDate");
                                csv.WriteField("TXSN");
                                csv.WriteField("PalletSerialNumber");
                                csv.WriteField("TestPosition");
                                csv.WriteField("OverallResult");
                                csv.WriteField("PrRecCreatedDate");
                                csv.WriteField("CompletedDate");
                                csv.WriteField("ValueName");
                                csv.WriteField("Value");
                                csv.WriteField("ValueString");
                                csv.WriteField("RejectCode");
                                csv.WriteField("RejectDesc");
                                csv.WriteField("ProcessID");
                                csv.WriteField("Dummy TXSN");

                                csv.NextRecord();
                            }

                            var dateTime = DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff");
                            var date = DateTime.Now.ToString("yyyy-MMM-dd");
                            var partNumTester = OPC.Read<string[]>(PartNumberTester, typeof(string), 6);
                            var recipeNameTester = OPC.Read<string[]>(RecipeNameTester, typeof(string), 6);
                            var txSNTester = OPC.Read<int[]>(TXSNTester, typeof(int), 6);
                            var testPositionTester = OPC.Read<string[]>(TestPositionTester, typeof(string), 6);
                            var overallResultTester = OPC.Read<int[]>(OverallResultTester, typeof(int), 6);
                            var prRecValCreatedDateTester = OPC.Read<string[]>(PrRecValCreatedDateTester, typeof(string), 6);
                            var dummyTXSNTester = OPC.Read<string[]>(DummyTXSNTester, typeof(string), 6);

                            for(int i = 0; i < 6; i++)
                            {
                                string SiteTestStn = _Main.SQLer.Exec_Scalar<string>("SELECT [Info] FROM dbo.Config WHERE Item = 'Site'");
                                csv.WriteField(SiteTestStn);

                                string EQNumTestStn = _Main.SQLer.Exec_Scalar<string>(" SELECT [] FROM [dbo.Config]");
                                csv.WriteField(EQNumTestStn);

                                string ProcessNameTestStn = _Main.SQLer.Exec_Scalar<string>(" SELECT [] FROM [dbo.Config]");
                                csv.WriteField(ProcessNameTestStn);

                                csv.WriteField(OPC.Read<string>(LotTester));
                                csv.WriteField(partNumTester[i]);

                                csv.WriteField(recipeNameTester[i]);
                                csv.WriteField(dateTime);
                                csv.WriteField(txSNTester[i]);
                                csv.WriteField(overallResultTester[i]);
                                csv.WriteField("NULL");
                                csv.WriteField(dateTime);
                                csv.WriteField(OPC.Read<string>(RejectCodeTester));
                                csv.WriteField(OPC.Read<string>(RejectDescTester));
                                csv.WriteField(OPC.Read<string>(ProcessIDTester));
                                csv.WriteField(dummyTXSNTester);

                                csv.NextRecord();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "TesterStnUnitTracker");
            }
        }

        #region UnldStn

        private string LotUld = "Lot_Info.HMI_LotID";
        private string PartNumberUld = "HMI_LogUldStnUnitTracker[1].oDUTId";
        private string RecipeNameUld = "HMI_LogUldStnUnitTracker[1].RecipeName";
        private string TxCreateDateUld = "HMI_LogUldStnUnitTracker[1].LaserDoneTime";
        private string TXSNUld = "HMI_LogUldStnUnitTracker[1].TxID";
        private string TestPositionUld = "HMI_LogUldStnUnitTracker[1].TestPosition";
        private string OverallResultUld = "HMI_LogUldStnUnitTracker[1].Result";
        private string PrRecValCreatedDateUld = "HMI_LogUldStnUnitTracker[1].RecordedTime";
        private string CompletedDateUld = "HMI_LogUldStnUnitTracker[1].LaserDoneTime";
        private string RejectCodeUld = "HMI_LogUldStnUnitTracker[1].RejectCode";
        private string RejectDescUld = "HMI_LogUldStnUnitTracker[1].RejectDesc";
        private string ProcessIDUld = "52";
        private string DummyTXSNUld = "HMI_LogUldStnUnitTracker[1].Virtual_ID";

        #endregion UnldStn

        public void UnldStnUnitTracker()
        {
            try
            {
                string LogsPath = Utilities.FileLogger.DefaultLocation_Time + Path.DirectorySeparatorChar + @"UnldStnUnitTracker";
                if(!Directory.Exists(LogsPath))
                { Directory.CreateDirectory(LogsPath); }
                string _Path = Path.Combine(LogsPath, $"UnldStnUnitTracker_{DateTime.Now.ToString("yyyy-MMM-dd")}.csv");
                bool HasFile = File.Exists(_Path);
                using(FileStream stream = new FileStream(_Path, HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                {
                    using(var writer = new StreamWriter(stream))
                    {
                        using(var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                        {
                            if(!HasFile)
                            {
                                csv.WriteField("Site");
                                csv.WriteField("EQNumber");
                                csv.WriteField("ProcessName");
                                csv.WriteField("Lot");
                                csv.WriteField("PartNumber");
                                csv.WriteField("RecipeName");
                                csv.WriteField("TxCreateDate");
                                csv.WriteField("TXSN");
                                csv.WriteField("PalletSerialNumber");
                                csv.WriteField("TestPosition");
                                csv.WriteField("OverallResult");
                                csv.WriteField("PrRecCreatedDate");
                                csv.WriteField("CompletedDate");
                                csv.WriteField("ValueName");
                                csv.WriteField("Value");
                                csv.WriteField("ValueString");
                                csv.WriteField("RejectCode");
                                csv.WriteField("RejectDesc");
                                csv.WriteField("ProcessID");
                                csv.WriteField("Dummy TXSN");

                                csv.NextRecord();
                            }

                            var dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
                            var date = DateTime.Now.ToString("yyyy/MMM/dd");
                            var partNumUld = OPC.Read<string[]>(PartNumberUld, typeof(string), 6);
                            var recipeNameUld = OPC.Read<string[]>(RecipeNameUld, typeof(string), 6);

                            var txCreateDateUld = OPC.Read<string[]>(TxCreateDateLaser, typeof(string), 6);

                            var txSNUld = OPC.Read<int[]>(TXSNUld, typeof(int), 6);
                            var testPositionUld = OPC.Read<string[]>(TestPositionUld, typeof(string), 6);
                            var overallResultUld = OPC.Read<int[]>(OverallResultUld, typeof(int), 6);

                            var completedDateUld = OPC.Read<string[]>(CompletedDateUld, typeof(string), 6);

                            var prRecValCreatedDateUld = OPC.Read<string[]>(PrRecValCreatedDateUld, typeof(string), 6);
                            var rejectCodeUld = OPC.Read<string[]>(RejectCodeUld, typeof(string), 6);
                            var rejectDescUld = OPC.Read<string[]>(RejectDescUld, typeof(string), 6);
                            var processIDUld = OPC.Read<string[]>(ProcessIDUld, typeof(string), 6);
                            var dummyTXSNUld = OPC.Read<string[]>(DummyTXSNUld, typeof(string), 6);

                            for(int i = 0; i < 6; i++)
                            {
                                string SiteTestStn = _Main.SQLer.Exec_Scalar<string>("SELECT [Info] FROM dbo.Config WHERE Item = 'Site'");
                                csv.WriteField(SiteTestStn);

                                string EQNumTestStn = _Main.SQLer.Exec_Scalar<string>(" SELECT [] FROM [dbo.Config]");
                                csv.WriteField(EQNumTestStn);

                                string ProcessNameTestStn = _Main.SQLer.Exec_Scalar<string>(" SELECT [] FROM [dbo.Config]");
                                csv.WriteField(ProcessNameTestStn);

                                csv.WriteField(OPC.Read<string>(LotUld));
                                csv.WriteField(partNumUld[i]);

                                csv.WriteField(recipeNameUld[i]);
                                csv.WriteField(dateTime);
                                csv.WriteField(txSNUld[i]);
                                csv.WriteField(overallResultUld[i]);
                                csv.WriteField(testPositionUld[i]);
                                csv.WriteField(overallResultUld[i]);
                                csv.WriteField(prRecValCreatedDateUld[i]);
                                csv.WriteField(dateTime);
                                csv.WriteField(rejectCodeUld[i]);
                                csv.WriteField(rejectDescUld[i]);
                                csv.WriteField(processIDUld[i]);
                                csv.WriteField(dummyTXSNUld[i]);

                                csv.NextRecord();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "UnldStnUnitTracker");
            }
        }

        #region TnRStn

        private string LotTnR = "Lot_Info.HMI_LotID";
        private string PartNumberTnR = "HMI_LogTnRStnUnitTracker[1].oDUTId";
        private string RecipeNameTnR = "HMI_LogTnRStnUnitTracker[1].RecipeName";
        private string TxCreateDateTnR = "HMI_LogTnRStnUnitTracker[1].LaserDoneTime";
        private string TXSNTnR = "HMI_LogTnRStnUnitTracker[1].TxID";
        private string TestPositionTnR = "HMI_LogTnRStnUnitTracker[1].TestPosition";
        private string OverallResultTnR = "HMI_LogTnRStnUnitTracker[1].Result";
        private string PrRecValCreatedDateTnR = "HMI_LogTnRStnUnitTracker[1].RecordedTime";
        private string CompletedDateTnR = "HMI_LogTnRStnUnitTracker[1].LaserDoneTime";
        private string RejectCodeTnR = "HMI_LogTnRStnUnitTracker[1].RejectCode";
        private string RejectDescTnR = "HMI_LogTnRStnUnitTracker[1].RejectDesc";
        private string ProcessIDTnR = "52";
        private string DummyTXSNTnR = "HMI_LogTnRStnUnitTracker[1].Virtual_ID";

        #endregion TnRStn

        public void TnRStnUnitTracker()
        {
            try
            {
                string LogsPath = Utilities.FileLogger.DefaultLocation_Time + Path.DirectorySeparatorChar + @"TnRStnUnitTracker";
                if(!Directory.Exists(LogsPath))
                { Directory.CreateDirectory(LogsPath); }
                string _Path = Path.Combine(LogsPath, $"TnRStnUnitTracker_{DateTime.Now.ToString("yyyy-MMM-dd")}.csv");
                bool HasFile = File.Exists(_Path);
                using(FileStream stream = new FileStream(_Path,
                   HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                {
                    using(var writer = new StreamWriter(stream))
                    {
                        using(var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                        {
                            if(!HasFile)
                            {
                                csv.WriteField("Site");
                                csv.WriteField("EQNumber");
                                csv.WriteField("ProcessName");
                                csv.WriteField("Lot");
                                csv.WriteField("PartNumber");
                                csv.WriteField("RecipeName");
                                csv.WriteField("TxCreateDate");
                                csv.WriteField("TXSN");
                                csv.WriteField("PalletSerialNumber");
                                csv.WriteField("TestPosition");
                                csv.WriteField("OverallResult");
                                csv.WriteField("PrRecCreatedDate");
                                csv.WriteField("CompletedDate");
                                csv.WriteField("ValueName");
                                csv.WriteField("Value");
                                csv.WriteField("ValueString");
                                csv.WriteField("RejectCode");
                                csv.WriteField("RejectDesc");
                                csv.WriteField("ProcessID");
                                csv.WriteField("Dummy TXSN");

                                csv.NextRecord();
                            }

                            var dateTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss.fff");
                            var date = DateTime.Now.ToString("yyyy/MMM/dd");
                            var partNumTnR = OPC.Read<string[]>(PartNumberTnR, typeof(string), 6);
                            var recipeNameTnR = OPC.Read<string[]>(RecipeNameTnR, typeof(string), 6);

                            var txCreateDateTnR = OPC.Read<string[]>(TxCreateDateTnR, typeof(string), 6);

                            var txSNTnR = OPC.Read<int[]>(TXSNTnR, typeof(int), 6);
                            var testPositionTnR = OPC.Read<string[]>(TestPositionTnR, typeof(string), 6);
                            var overallResultTnR = OPC.Read<int[]>(OverallResultTnR, typeof(int), 6);

                            var completedDateTnR = OPC.Read<string[]>(CompletedDateTnR, typeof(string), 6);

                            var prRecValCreatedDateTnR = OPC.Read<string[]>(PrRecValCreatedDateTnR, typeof(string), 6);
                            var rejectCodeTnR = OPC.Read<string[]>(RejectCodeTnR, typeof(string), 6);
                            var rejectDescTnR = OPC.Read<string[]>(RejectDescTnR, typeof(string), 6);
                            var processIDTnR = OPC.Read<string[]>(ProcessIDTnR, typeof(string), 6);
                            var dummyTXSNTnR = OPC.Read<string[]>(DummyTXSNTnR, typeof(string), 6);

                            for(int i = 0; i < 6; i++)
                            {
                                string SiteTestStn = _Main.SQLer.Exec_Scalar<string>("SELECT [Info] FROM dbo.Config WHERE Item = 'Site'");
                                csv.WriteField(SiteTestStn);

                                string EQNumTestStn = _Main.SQLer.Exec_Scalar<string>(" SELECT [] FROM [dbo.Config]");
                                csv.WriteField(EQNumTestStn);

                                string ProcessNameTestStn = _Main.SQLer.Exec_Scalar<string>(" SELECT [] FROM [dbo.Config]");
                                csv.WriteField(ProcessNameTestStn);

                                csv.WriteField(OPC.Read<string>(LotTnR));
                                csv.WriteField(partNumTnR[i]);

                                csv.WriteField(recipeNameTnR[i]);
                                csv.WriteField(dateTime);
                                csv.WriteField(txSNTnR[i]);
                                csv.WriteField(overallResultTnR[i]);
                                csv.WriteField(testPositionTnR[i]);
                                csv.WriteField(overallResultTnR[i]);
                                csv.WriteField(prRecValCreatedDateTnR[i]);
                                csv.WriteField(dateTime);
                                csv.WriteField(rejectCodeTnR[i]);
                                csv.WriteField(rejectDescTnR[i]);
                                csv.WriteField(processIDTnR[i]);
                                csv.WriteField(dummyTXSNTnR[i]);

                                csv.NextRecord();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "UnldStnUnitTracker");
            }
        }

        #region LaserStn

        private string LotLaser = "Lot_Info.HMI_LotID";
        private string PartNumberLaser = "HMI_LogLaserUnitTracker[1].oDUTId";
        private string RecipeNameLaser = "HMI_LogLaserUnitTracker[1].RecipeName";
        private string TxCreateDateLaser = "HMI_LogLaserUnitTracker[1].LaserDoneTime";
        private string TXSNLaser = "HMI_LogLaserUnitTracker[1].TxID";
        private string TestPositionLaser = "HMI_LogLaserUnitTracker[1].TestPosition";
        private string OverallResultLaser = "HMI_LogLaserUnitTracker[1].Result";
        private string PrRecValCreatedDateLaser = "HMI_LogLaserUnitTracker[1].RecordedTime";
        private string CompletedDateLaser = "HMI_LogLaserUnitTracker[1].LaserDoneTime";
        private string ValueNameLaser = "HMI_LogLaserUnitTracker[1].ValueName";
        private string ValueLaser = "HMI_LogLaserUnitTracker[1].Value";
        private string ValueStringLaser = "";
        private string RejectCodeLaser = "HMI_LogLaserUnitTracker[1].RejectCode";
        private string RejectDescLaser = "HMI_LogLaserUnitTracker[1].RejectDesc";
        private string ProcessIDLaser = "52";
        private string DummyTXSNLaser = "HMI_LogLaserUnitTracker[1].Virtual_ID";

        #endregion LaserStn

        public void LaserStnUnitTracker()
        {
            SQLer.Exec_DTSelect("INSERT INTO [dbo].[Table2] ([Site],[EQNumber],[ProcessName],[Lot],[PartNumber],[RecipeName],[TxCreateDate],[TXSN],[PalletSerialNumber]," +
                "[TestPosition],[OverallResult],[CompletedDate],[ValueName],[Value],[ValueString],[RejectCode],[RejectDesc],[ProcessID],[DummyTXSN]) VALUES " +
                    $"('','')");

            try
            {
                string LogsPath = Utilities.FileLogger.DefaultLocation_Time + Path.DirectorySeparatorChar + @"LaserStnUnitTracker";
                if(!Directory.Exists(LogsPath))
                { Directory.CreateDirectory(LogsPath); }
                string _Path = Path.Combine(LogsPath, $"LaserStnUnitTracker_{DateTime.Now.ToString("yyyy-MMM-dd")}.csv");
                bool HasFile = File.Exists(_Path);
                using(FileStream stream = new FileStream(_Path,
                   HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                {
                    using(var writer = new StreamWriter(stream))
                    {
                        using(var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                        {
                            if(!HasFile)
                            {
                                csv.WriteField("Site");
                                csv.WriteField("EQNumber");
                                csv.WriteField("ProcessName");
                                csv.WriteField("Lot");
                                csv.WriteField("PartNumber");
                                csv.WriteField("RecipeName");
                                csv.WriteField("TxCreateDate");
                                csv.WriteField("TXSN");
                                csv.WriteField("PalletSerialNumber");
                                csv.WriteField("TestPosition");
                                csv.WriteField("OverallResult");
                                csv.WriteField("PrRecCreatedDate");
                                csv.WriteField("CompletedDate");
                                csv.WriteField("ValueName");
                                csv.WriteField("Value");
                                csv.WriteField("ValueString");
                                csv.WriteField("RejectCode");
                                csv.WriteField("RejectDesc");
                                csv.WriteField("ProcessID");
                                csv.WriteField("Dummy TXSN");

                                csv.NextRecord();
                            }

                            var dateTime = DateTime.Now.ToString("yyyy-MMM-dd_HH:mm:ss.fff");
                            var date = DateTime.Now.ToString("yyyy-MMM-dd");
                            var partNumLaser = OPC.Read<string[]>(PartNumberLaser, typeof(string), 6);
                            var recipeNameLaser = OPC.Read<string[]>(RecipeNameLaser, typeof(string), 6);

                            var txCreateDateLaser = OPC.Read<string[]>(TxCreateDateLaser, typeof(string), 6);

                            var txSNLaser = OPC.Read<int[]>(TXSNLaser, typeof(int), 6);
                            var testPositionLaser = OPC.Read<string[]>(TestPositionLaser, typeof(string), 6);
                            var overallResultLaser = OPC.Read<int[]>(OverallResultLaser, typeof(int), 6);
                            var prRecValCreatedDateLaser = OPC.Read<string[]>(PrRecValCreatedDateLaser, typeof(string), 6);

                            var completedDateLaser = OPC.Read<string[]>(CompletedDateLaser, typeof(string), 6);
                            var valueNameLaser = OPC.Read<string[]>(ValueNameLaser, typeof(string), 6);
                            var valueLaser = OPC.Read<string[]>(ValueLaser, typeof(string), 6);
                            var valueStringLaser = OPC.Read<string[]>(ValueStringLaser, typeof(string), 6);

                            var rejectCodeLaser = OPC.Read<string[]>(RejectCodeLaser, typeof(string), 6);
                            var rejectDescLaser = OPC.Read<string[]>(RejectDescLaser, typeof(string), 6);
                            var dummyTXSNLaser = OPC.Read<string[]>(DummyTXSNLaser, typeof(string), 6);

                            for(int i = 0; i < 6; i++)
                            {
                                string SiteTestStn = _Main.SQLer.Exec_Scalar<string>("SELECT [Info] FROM dbo.Config WHERE Item = 'Site'");
                                csv.WriteField(SiteTestStn);

                                string EQNumTestStn = _Main.SQLer.Exec_Scalar<string>(" SELECT [] FROM [dbo.Config]");
                                csv.WriteField(EQNumTestStn);

                                string ProcessNameTestStn = _Main.SQLer.Exec_Scalar<string>(" SELECT [] FROM [dbo.Config]");
                                csv.WriteField(ProcessNameTestStn);

                                csv.WriteField(OPC.Read<string>(LotLaser));
                                csv.WriteField(partNumLaser[i]);

                                csv.WriteField(recipeNameLaser[i]);
                                csv.WriteField(txCreateDateLaser[i]);
                                csv.WriteField(dateTime);
                                csv.WriteField(txSNLaser[i]);
                                csv.WriteField(overallResultLaser[i]);
                                csv.WriteField("");
                                csv.WriteField(dateTime);

                                csv.WriteField(completedDateLaser[i]);
                                csv.WriteField(valueNameLaser[i]);
                                csv.WriteField(valueLaser[i]);
                                csv.WriteField(valueStringLaser[i]);

                                csv.WriteField(rejectCodeLaser[i]);
                                csv.WriteField(rejectDescLaser[i]);
                                csv.WriteField(OPC.Read<string>(ProcessIDLaser));

                                csv.WriteField(dummyTXSNLaser[i]);

                                csv.NextRecord();
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "LaserStnUnitTracker");
            }
        }

        private void initializeShuttleBarcodeDictionary()
        {
            try
            {
                shuttleBarcodeDictionary = new Dictionary<string, string>();

                for(int x = 1; x <= 2; x++)
                {
                    try
                    {
                        for(int y = 1; y <= 2; y++)
                        {
                            try
                            {
                                for(int z = 0; z < 8; z++)
                                {
                                    try
                                    {
                                        shuttleBarcodeDictionary.Add($"Shuttle {x} Barcode {y} Pass {z + 1}",
                                            $"HMI_Display_Shuttle{x}_Scanner{y}_PASS[{z}]");
                                    }
                                    catch(Exception exception)
                                    {
                                        FileLogger.logError(exception.Message, exception.ToString());
                                    }
                                }

                                for(int z = 0; z < 8; z++)
                                {
                                    try
                                    {
                                        shuttleBarcodeDictionary.Add($"Shuttle {x} Barcode {y} Fail {z + 1}",
                                            $"HMI_Display_Shuttle{x}_Scanner{y}_FAIL[{z}]");
                                    }
                                    catch(Exception exception)
                                    {
                                        FileLogger.logError(exception.Message, exception.ToString());
                                    }
                                }
                            }
                            catch(Exception exception)
                            {
                                FileLogger.logError(exception.Message, exception.ToString());
                            }
                        }
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        public void VTCRackBarcodeLog()
        {
            try
            {
                if(_Main.OPC.Read<bool>("bool_RackLogSave"))
                {
                    string LogsPath = Path.Combine(FileLogger.DefaultLocation_Time, "Barcode/RackBarcodeLog");
                    if(!Directory.Exists(LogsPath))
                    { Directory.CreateDirectory(LogsPath); }
                    string _Path = Path.Combine(LogsPath, $"RackBarcode_{DateTime.Today.ToString("yyyyMMdd")}.txt");
                    bool HasFile = File.Exists(_Path);
                    using(FileStream stream = new FileStream(_Path,
                       HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using(var writer = new StreamWriter(stream))
                        {
                            using(var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                            {
                                if(!HasFile)
                                {
                                    csv.WriteField("");
                                    csv.WriteField("Rack Left Zone");
                                    csv.WriteField("");
                                    csv.WriteField("Rack Right Zone");
                                    csv.NextRecord();

                                    csv.WriteField("Date Time");
                                    csv.WriteField("Left - Rack Slot");
                                    csv.WriteField("Left - Asset Tag");
                                    csv.WriteField("Right - Rack Slot");
                                    csv.WriteField("Right - Asset Tag");
                                    csv.NextRecord();
                                }
                                csv.WriteField(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
                                csv.WriteField(_Main.OPC.Read<string>("int_RackZoneA_SlotLog"));
                                csv.WriteField(_Main.OPC.Read<string>("str_RackZoneA_AssetTag"));
                                csv.WriteField(_Main.OPC.Read<string>("int_RackZoneB_SlotLog"));
                                csv.WriteField(_Main.OPC.Read<string>("str_RackZoneB_AssetTag"));
                                csv.NextRecord();
                            }
                        }
                    }
                    _Main.OPC.Write("bool_RackLogSaveDone", true);
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "InputZoneBarcodeLog");
            }
        }

        public void VTCInputBarcode()
        {
            try
            {
                if(_Main.OPC.Read<bool>("bool_LogSave"))
                {
                    string LogsPath = Path.Combine(FileLogger.DefaultLocation_Time, "Barcode/InputZoneBarcodeLog");
                    if(!Directory.Exists(LogsPath))
                    { Directory.CreateDirectory(LogsPath); }
                    string _Path = Path.Combine(LogsPath, $"InputZoneBarcode_{DateTime.Today.ToString("yyyyMMdd")}.txt");
                    bool HasFile = File.Exists(_Path);
                    using(FileStream stream = new FileStream(_Path,
                       HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using(var writer = new StreamWriter(stream))
                        {
                            using(var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                            {
                                if(!HasFile)
                                {
                                    csv.WriteField("Date Time");
                                    csv.WriteField("Asset Tag");
                                    csv.WriteField("Barcode");
                                    csv.WriteField("Status");
                                    csv.WriteField("Reason");
                                    csv.NextRecord();
                                }
                                csv.WriteField(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
                                csv.WriteField(_Main.OPC.Read<string>("str_AssetTagReceived"));
                                csv.WriteField(_Main.OPC.Read<string>("str_BarcodeReceived"));
                                csv.WriteField(_Main.OPC.Read<string>("str_PalletStatus"));
                                csv.WriteField(_Main.OPC.Read<string>("str_PalletRejectReason"));

                                csv.NextRecord();
                            }
                        }
                    }
                    _Main.OPC.Write("bool_LogSaveDone", true);
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "InputZoneBarcodeLog");
            }
        }

        private Dictionary<int, string> Dic_TrayRejectCode_Reason = new Dictionary<int, string>
        {
            [1] = "Tray Unable to Enter Rack Slot",
            [2] = "Tray Stuck Inside Rack Slot / Tray Collide With Rack Tray Splitter",
            [3] = "Tray MPN Not Match",
            [4] = "Fail to Read Barcode",
        };

        public void VTCRejectReason()
        {
            try
            {
                if(_Main.OPC.Read<bool>("HMI_PLCTrigger_Record_TrayReject"))
                {
                    string LogsPath = Path.Combine(FileLogger.DefaultLocation_Time, "TrayRejectReasonLog");
                    if(!Directory.Exists(LogsPath))
                    { Directory.CreateDirectory(LogsPath); }
                    string _Path = Path.Combine(LogsPath, $"TrayRejectReason_{DateTime.Today.ToString("yyyyMMdd")}.txt");
                    bool HasFile = File.Exists(_Path);
                    using(FileStream stream = new FileStream(_Path,
                       HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using(var writer = new StreamWriter(stream))
                        {
                            using(var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                            {
                                if(!HasFile)
                                {
                                    csv.WriteField("Date Time");
                                    csv.WriteField("Barcode");
                                    csv.WriteField("Reason");
                                    csv.NextRecord();
                                }

                                string Reason;
                                Dic_TrayRejectCode_Reason.TryGetValue(_Main.OPC.Read<int>("HMI_PLCTrigger_Record_TrayReject_Reason"), out Reason);
                                csv.WriteField(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
                                csv.WriteField(_Main.OPC.Read<string>("HMI_PLCTrigger_Record_TrayReject_Barcd"));
                                csv.WriteField(string.IsNullOrWhiteSpace(Reason) ? "NA" : Reason);

                                csv.NextRecord();
                            }
                        }
                    }
                    _Main.OPC.Write("HMI_PLCTrigger_Record_TrayReject", false);
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "InputZoneBarcodeLog");
            }
        }

        private void logShuttleBarcode()
        {
            try
            {
                string folderPath = $"{FileLogger.DefaultLocation_Time}{Path.DirectorySeparatorChar}ShuttleBarcode";
                if(!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, $"ShuttleBarcode_{DateTime.Now:yyyy-MM-dd}.txt");
                bool fileExists = File.Exists(filePath);

                using(FileStream fileStream = new FileStream(filePath,
                    FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter streamWriter = new StreamWriter(fileStream))
                using(CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    if(!fileExists)
                    {
                        csvWriter.WriteField("Date Time");

                        foreach(var item in shuttleBarcodeDictionary)
                        {
                            try
                            {
                                csvWriter.WriteField(item.Key);
                            }
                            catch(Exception exception)
                            {
                                csvWriter.WriteField(string.Empty);
                                FileLogger.logError(exception.Message, exception.ToString());
                            }
                        }

                        csvWriter.NextRecord();
                    }

                    csvWriter.WriteField(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

                    foreach(var item in shuttleBarcodeDictionary)
                    {
                        try
                        {
                            csvWriter.WriteField(_Main.OPC.Read<string>(item.Value));
                        }
                        catch(Exception exception)
                        {
                            csvWriter.WriteField(string.Empty);
                            FileLogger.logError(exception.Message, exception.ToString());
                        }
                    }

                    csvWriter.NextRecord();
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeRobotGripperDictionary()
        {
            try
            {
                robotGripperDictionary = new Dictionary<string, string>();

                for(int x = 3; x <= 4; x++)
                {
                    try
                    {
                        for(int y = 1; y <= 2; y++)
                        {
                            try
                            {
                                int pickTrayNo = x == 3 ? y : y + 2;

                                for(int z = 1; z <= 8; z++)
                                {
                                    try
                                    {
                                        robotGripperDictionary.Add(
                                            $"Robot {x} Gripper {z} Pick Tray {pickTrayNo} Pass Count",
                                            $"DIMM_Rbt{x}_PickTray{pickTrayNo}_PassCount[{z}]");
                                    }
                                    catch(Exception exception)
                                    {
                                        FileLogger.logError(exception.Message, exception.ToString());
                                    }
                                }

                                for(int z = 1; z <= 8; z++)
                                {
                                    try
                                    {
                                        robotGripperDictionary.Add(
                                            $"Robot {x} Gripper {z} Pick Tray {pickTrayNo} Fail Count",
                                            $"DIMM_Rbt{x}_PickTray{pickTrayNo}_FailCount[{z}]");
                                    }
                                    catch(Exception exception)
                                    {
                                        FileLogger.logError(exception.Message, exception.ToString());
                                    }
                                }
                            }
                            catch(Exception exception)
                            {
                                FileLogger.logError(exception.Message, exception.ToString());
                            }
                        }
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }

                for(int i = 3; i <= 4; i++)
                {
                    try
                    {
                        for(int j = 1; j <= 8; j++)
                        {
                            try
                            {
                                robotGripperDictionary.Add($"Robot {i} Unit Dropped at Gripper {j}",
                                    $"DIMM_Rbt{i}_Unit_Dropped[{j}]");
                            }
                            catch(Exception exception)
                            {
                                FileLogger.logError(exception.Message, exception.ToString());
                            }
                        }
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void logRobotGripper()
        {
            try
            {
                string folderPath = $"{FileLogger.DefaultLocation_Time}{Path.DirectorySeparatorChar}RobotGripper";
                if(!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string filePath = Path.Combine(folderPath, $"RobotGripper_{DateTime.Now:yyyy-MM-dd}.txt");
                bool fileExists = File.Exists(filePath);

                using(FileStream fileStream = new FileStream(filePath,
                    FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                using(StreamWriter streamWriter = new StreamWriter(fileStream))
                using(CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    if(!fileExists)
                    {
                        csvWriter.WriteField("Date Time");

                        foreach(var item in robotGripperDictionary)
                        {
                            try
                            {
                                csvWriter.WriteField(item.Key);
                            }
                            catch(Exception exception)
                            {
                                csvWriter.WriteField(string.Empty);
                                FileLogger.logError(exception.Message, exception.ToString());
                            }
                        }

                        csvWriter.NextRecord();
                    }

                    csvWriter.WriteField(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

                    foreach(var item in robotGripperDictionary)
                    {
                        try
                        {
                            csvWriter.WriteField(_Main.OPC.Read<string>(item.Value));
                        }
                        catch(Exception exception)
                        {
                            csvWriter.WriteField(string.Empty);
                            FileLogger.logError(exception.Message, exception.ToString());
                        }
                    }

                    csvWriter.NextRecord();
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        public enum Grouping
        {
            stringtime,
            sec,
            number,
            percent
        }

        private string FormatString(Grouping grouping, object Value)
        {
            string strValue = Value?.ToString() ?? string.Empty;
            if(string.IsNullOrWhiteSpace(strValue))
                return strValue;

            if(grouping.Equals(Grouping.sec))
            {
                TimeSpan Ts = TimeSpan.FromSeconds(Convert.ToDouble(strValue));
                int DayHours = Ts.Days * 24;
                return string.Format("{0:D2}:{1:D2}:{2:D2}", Ts.Hours + DayHours, Ts.Minutes, Ts.Seconds);
            }
            else if(grouping.Equals(Grouping.number))
            {
                return strValue;
            }
            else if(grouping.Equals(Grouping.stringtime))
            {
                return string.IsNullOrWhiteSpace(strValue) ? "" : new DateTime(Convert.ToInt32(strValue.Substring(0, 4)),
                             Convert.ToInt32(strValue.Substring(4, 2)),
                             Convert.ToInt32(strValue.Substring(6, 2)),
                             Convert.ToInt32(strValue.Substring(8, 2)),
                             Convert.ToInt32(strValue.Substring(10, 2)), 0).ToString();
            }
            else if(grouping.Equals(Grouping.percent))
            {
                return Math.Round(Convert.ToDouble(strValue), 2).ToString() + "%";
            }
            return "NA";
        }

        public void BarcodeResult(int Zone)
        {
            try
            {
                string Tag_BarcodeLog = $"HMI_PLCTrigger_Record_RAMBarcd_Z{Zone}";
                if(_Main.OPC.Read<bool>(Tag_BarcodeLog))
                {
                    string LogsPath = Path.Combine(FileLogger.DefaultLocation_Time, "RamBarcodeInstalled");
                    if(!Directory.Exists(LogsPath))
                    { Directory.CreateDirectory(LogsPath); }
                    string _Path = Path.Combine(LogsPath, $"RamBarcodeInstalled_{DateTime.Today.ToString("yyyyMMdd")}.txt");
                    bool HasFile = File.Exists(_Path);
                    using(FileStream stream = new FileStream(_Path,
                       HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                    {
                        using(var writer = new StreamWriter(stream))
                        {
                            using(var csv = new CsvHelper.CsvWriter(writer, System.Globalization.CultureInfo.CurrentCulture))
                            {
                                if(!HasFile)
                                {
                                    csv.WriteField(string.Empty);
                                    csv.WriteField(string.Empty);
                                    csv.WriteField(string.Empty);
                                    for(int n1 = 1; n1 <= 4; n1++)
                                    {
                                        csv.WriteField($"G{n1}");
                                        for(int n = 1; n < 8; n++)
                                            csv.WriteField(string.Empty);
                                    }
                                    csv.NextRecord();
                                    csv.WriteField("Start Date Time");
                                    csv.WriteField("End Date Time");
                                    csv.WriteField("Asset Tag");
                                    csv.WriteField("Zone Num");
                                    for(int n = 1; n <= 4; n++)
                                        for(int ns = 1; ns <= 8; ns++)
                                            csv.WriteField($"Slot{ns}");
                                    csv.NextRecord();
                                }

                                csv.WriteField(_Main.OPC.Read<string>($"OEE_Tags.str_StartTime_Z{Zone}_DDmmYYYYhhMMss"));
                                csv.WriteField(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));

                                csv.WriteField(_Main.OPC.Read<string>($"HMI_PLCTrigger_Record_RAMBarcd_AssTg_Z{Zone}"));
                                csv.WriteField(_Main.OPC.Read<int>($"HMI_PLCTrigger_Record_RAMBarcd_Z{Zone}_ZNum"));
                                for(int ng = 1; ng <= 4; ng++)
                                    for(int nb = 0; nb < 8; nb++)
                                    {
                                        csv.WriteField(_Main.OPC.Read<string>($"DIMM_Data_Tracking_Barcd_Z{Zone}_G{ng}[{nb}]"));
                                    }
                                csv.NextRecord();
                            }
                        }
                    }
                    _Main.OPC.Write(Tag_BarcodeLog, false);
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "VisionResult");
            }
        }


        public void HDD_CheckAndLogOperatorLoadTimeTaken()
        {
            try
            {
                string HMI_OprLoadTimeLog = "HMI_OprLoadTimeLog";
                if(!_Main.OPC.Read<bool>(HMI_OprLoadTimeLog))
                    return;

                _Main.OPC.Write(HMI_OprLoadTimeLog, false);

                string Tag_OprLoadTime = "CycleTime01.LastCycleTime";
                string OprLoadTime = _Main.OPC.Read<string>(Tag_OprLoadTime, typeof(double));
                string LogsPath = Path.Combine(FileLogger.DefaultLocation_Time, "OperatorLoadTimeTaken");
                if(!Directory.Exists(LogsPath))
                { Directory.CreateDirectory(LogsPath); }
                string _Path = Path.Combine(LogsPath, $"OprLoadTimeTaken_{DateTime.Today.ToString("yyyyMMdd")}.txt");
                bool HasFile = File.Exists(_Path);
                using(FileStream stream = new FileStream(_Path,
                   HasFile ? FileMode.Append : FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                {
                    using(var writer = new StreamWriter(stream))
                    {
                        using(var csv = new CsvWriter(writer, CultureInfo.CurrentCulture))
                        {
                            if(!HasFile)
                            {
                                csv.WriteField("DateTime");
                                csv.WriteField("Z1 Asset Tag");
                                csv.WriteField("Operator Load Time (s)");
                                csv.NextRecord();
                            }
                            csv.WriteField(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff"));
                            csv.WriteField(_Main.OPC.Read<string>("Zone_HDDTagZ1"));
                            csv.WriteField(OprLoadTime);
                            csv.NextRecord();
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Check Operator LoadTime");
            }
        }
    }
}