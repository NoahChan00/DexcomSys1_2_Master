using System.Windows.Controls;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;
using Logix;
using System.Linq;
using System.Data;
using System.IO;

namespace PentagonHMI.ChildControls
{
    public partial class ucOEE_Gen2 : UserControl, IDisposable
    {
        Controller ctrl = new Controller(Info.OPC.IP);
        public Func<ChartPoint, string> PointLabel { get; set; }
        private LogicClasses.Main _Main;
        public OEEModel OEEList = new OEEModel();
        private Brush Color1 = Brushes.MediumSeaGreen;
        private Brush Color2 = Brushes.DeepSkyBlue;
        private Brush Color3 = Brushes.Orange;
        private Brush Color4 = Brushes.LightGray;
        private bool isArcadiaMainConveyor = false;

        Tag Tag_OEEStartDateTime = new Tag { Name = "OEE_Tags.str_HMI_OEEStartDateTime", DataType = Logix.Tag.ATOMIC.STRING };
        Tag Tag_TotalTime = new Tag { Name = "OEE_Tags.dint_MachineTotalTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_ProductiveTime = new Tag { Name = "OEE_Tags.dint_MachineProductiveTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_StandbyTime = new Tag { Name = "OEE_Tags.dint_MachineStandbyTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_EngineeringTime = new Tag { Name = "OEE_Tags.dint_MachineEngineeringTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_ScheduledDownTime = new Tag { Name = "OEE_Tags.dint_MachineScheduledDownTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_UnscheduledDownTime = new Tag { Name = "OEE_Tags.dint_MachineUnscheduledDownTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_NonscheduledTime = new Tag { Name = "OEE_Tags.dint_MachineNonScheduledTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_MeanDownTime = new Tag { Name = "OEE_Tags.dint_MeanDownTimeSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_SoftJam = new Tag { Name = "OEE_Tags.dint_SoftJam", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_HardJam = new Tag { Name = "OEE_Tags.dint_HardJam", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_MTBA = new Tag { Name = "OEE_Tags.dint_MTBASec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_MTBF = new Tag { Name = "OEE_Tags.dint_MTBFSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_TotalPass = new Tag { Name = "OEE_Tags.dint_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_TotalFail = new Tag { Name = "OEE_Tags.dint_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_IncomingPartFail = new Tag { Name = "OEE_Tags.dint_Total_PartFail", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_IncomingPartFail_Z1 = new Tag { Name = "OEE_Tags.dint_Z1_Total_PartFail", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_IncomingPartFail_Z2 = new Tag { Name = "OEE_Tags.dint_Z2_Total_PartFail", DataType = Logix.Tag.ATOMIC.DINT };

        public enum Grouping
        {
            stringtime,
            sec,
            number,
            percent
        }

        public enum OEEChart
        {
            EquipmentUptime,
            OperationTime,
            Quality,
            Performance,
            Availability,
            OEE,
            Loading,
            TEEP,
            DeltaTime,
            IncomingPartFail,
        }

        public ucOEE_Gen2(ref LogicClasses.Main main)
        {
            try
            {
                InitializeComponent();
                _Main = main;
                TagRename();
                Initialize();
                _Main.OnOEEUpdate += new LogicClasses.Main.onOEEUpdateHandler(Update);
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void TagRename()
        {
            if ((StationType.ARCADIA_Main == Classes.GlobalFunctions.StationType && ProjectType.ARCADIA == Classes.GlobalFunctions.ProjectType) || 
                (Classes.GlobalFunctions.StationType == StationType.VISION && _Main.MachineName.ToUpper().Contains("FINAL")))
            {
                if (Classes.GlobalFunctions.ProjectType == ProjectType.ARCADIA)
                {
                    ctrl = new Controller("193.168.3.102");
                    isArcadiaMainConveyor = true;
                }
                else if (Classes.GlobalFunctions.ProjectType == ProjectType.TLA)
                {
                    ctrl = new Controller("194.168.3.2");
                    isArcadiaMainConveyor = true;
                }
                
                Tag_OEEStartDateTime.Name = "OutputPnP_OEE_Tags.str_HMI_OEEStartDateTime";
                Tag_TotalTime.Name = "OutputPnP_OEE_Tags.dint_MachineTotalTimeAccSec";
                Tag_ProductiveTime.Name = "OutputPnP_OEE_Tags.dint_MachineProductiveTimeAccSec";
                Tag_StandbyTime.Name = "OutputPnP_OEE_Tags.dint_MachineStandbyTimeAccSec";
                Tag_EngineeringTime.Name = "OutputPnP_OEE_Tags.dint_MachineEngineeringTimeAccSec";
                Tag_ScheduledDownTime.Name = "OutputPnP_OEE_Tags.dint_MachineScheduledDownTimeAccSec";
                Tag_UnscheduledDownTime.Name = "OutputPnP_OEE_Tags.dint_MachineUnscheduledDownTimeAccSec";
                Tag_NonscheduledTime.Name = "OutputPnP_OEE_Tags.dint_MachineNonScheduledTimeAccSec";
                Tag_MeanDownTime.Name = "OutputPnP_OEE_Tags.dint_MeanDownTimeSec";
                Tag_SoftJam.Name = "OutputPnP_OEE_Tags.dint_SoftJam";
                Tag_HardJam.Name = "OutputPnP_OEE_Tags.dint_HardJam";
                Tag_MTBA.Name = "OutputPnP_OEE_Tags.dint_MTBASec";
                Tag_MTBF.Name = "OutputPnP_OEE_Tags.dint_MTBFSec";
                Tag_TotalPass.Name = "OutputPnP_OEE_Tags.dint_Total_Pass";
                Tag_TotalFail.Name = "OutputPnP_OEE_Tags.dint_Total_Fail";
            }
            else if ((Classes.GlobalFunctions.StationType == StationType.VISION && _Main.MachineName.ToUpper().Contains("CENTRAL")))
            {
                if (Classes.GlobalFunctions.ProjectType == ProjectType.TLA)
                {
                    ctrl = new Controller("194.168.3.2");
                    isArcadiaMainConveyor = true;
                }

                Tag_OEEStartDateTime.Name = "CenVis_OEE_Tags.str_HMI_OEEStartDateTime";
                Tag_TotalTime.Name = "CenVis_OEE_Tags.dint_MachineTotalTimeAccSec";
                Tag_ProductiveTime.Name = "CenVis_OEE_Tags.dint_MachineProductiveTimeAccSec";
                Tag_StandbyTime.Name = "CenVis_OEE_Tags.dint_MachineStandbyTimeAccSec";
                Tag_EngineeringTime.Name = "CenVis_OEE_Tags.dint_MachineEngineeringTimeAccSec";
                Tag_ScheduledDownTime.Name = "CenVis_OEE_Tags.dint_MachineScheduledDownTimeAccSec";
                Tag_UnscheduledDownTime.Name = "CenVis_OEE_Tags.dint_MachineUnscheduledDownTimeAccSec";
                Tag_NonscheduledTime.Name = "CenVis_OEE_Tags.dint_MachineNonScheduledTimeAccSec";
                Tag_MeanDownTime.Name = "CenVis_OEE_Tags.dint_MeanDownTimeSec";
                Tag_SoftJam.Name = "CenVis_OEE_Tags.dint_SoftJam";
                Tag_HardJam.Name = "CenVis_OEE_Tags.dint_HardJam";
                Tag_MTBA.Name = "CenVis_OEE_Tags.dint_MTBASec";
                Tag_MTBF.Name = "CenVis_OEE_Tags.dint_MTBFSec";
                Tag_TotalPass.Name = "CenVis_OEE_Tags.dint_Total_Pass";
                Tag_TotalFail.Name = "CenVis_OEE_Tags.dint_Total_Fail";
                Tag_IncomingPartFail.Name = "CenVis_OEE_Tags.dint_Total_PartFail";
                Tag_IncomingPartFail_Z1.Name = "CenVis_OEE_Tags.dint_Z1_Total_PartFail";
                Tag_IncomingPartFail_Z2.Name = "CenVis_OEE_Tags.dint_Z2_Total_PartFail";
            }
            else if (Classes.GlobalFunctions.ProjectType == ProjectType.TLA && Classes.GlobalFunctions.StationType == StationType.ARCADIA_Main)
            {
                ctrl = new Controller("194.168.3.2");
                isArcadiaMainConveyor = true;
                Tag_OEEStartDateTime.Name = "Labelling_OEE_Tags.str_HMI_OEEStartDateTime";
                Tag_TotalTime.Name = "Labelling_OEE_Tags.dint_MachineTotalTimeAccSec";
                Tag_ProductiveTime.Name = "Labelling_OEE_Tags.dint_MachineProductiveTimeAccSec";
                Tag_StandbyTime.Name = "Labelling_OEE_Tags.dint_MachineStandbyTimeAccSec";
                Tag_EngineeringTime.Name = "Labelling_OEE_Tags.dint_MachineEngineeringTimeAccSec";
                Tag_ScheduledDownTime.Name = "Labelling_OEE_Tags.dint_MachineScheduledDownTimeAccSec";
                Tag_UnscheduledDownTime.Name = "Labelling_OEE_Tags.dint_MachineUnscheduledDownTimeAccSec";
                Tag_NonscheduledTime.Name = "Labelling_OEE_Tags.dint_MachineNonScheduledTimeAccSec";
                Tag_MeanDownTime.Name = "Labelling_OEE_Tags.dint_MeanDownTimeSec";
                Tag_SoftJam.Name = "Labelling_OEE_Tags.dint_SoftJam";
                Tag_HardJam.Name = "Labelling_OEE_Tags.dint_HardJam";
                Tag_MTBA.Name = "Labelling_OEE_Tags.dint_MTBASec";
                Tag_MTBF.Name = "Labelling_OEE_Tags.dint_MTBFSec";
                Tag_TotalPass.Name = "Labelling_OEE_Tags.dint_Total_Pass";
                Tag_TotalFail.Name = "Labelling_OEE_Tags.dint_Total_Fail";
            }
        }

        private void Initialize()
        {
            AddTagstoGroup();

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            _Main.IdealCycleTime = _Main.OPC.Read<double>("HMI_IdealCycleTime");
            OEEList = new OEEModel
            {
                OEEInfo = new ObservableCollection<InfoBlockModel>
                {
                    new InfoBlockModel
                    {
                       Title = "Start Date Time",
                       Key = Tag_OEEStartDateTime.Name,
                       Group =  Grouping.stringtime
                    },
                    new InfoBlockModel
                    {
                        Title = "Total Time",
                        Key = Tag_TotalTime.Name,
                        Group = Grouping.sec
                    },
                    new InfoBlockModel
                    {
                        Title = "Total Pass",
                        Key = Tag_TotalPass.Name,
                        Group = Grouping.number
                    },
                    new InfoBlockModel
                    {
                        Title = "Total Fail",
                        Key = Tag_TotalFail.Name,
                        Group = Grouping.number
                    },
                    new InfoBlockModel
                    {
                        Title = "Incoming Part Fail",
                        Key = Tag_IncomingPartFail.Name,
                        Group = Grouping.number
                    },
                    new InfoBlockModel
                    {
                        Title = "Productive Time",
                        Key = Tag_ProductiveTime.Name,
                        Group = Grouping.sec
                    },
                    new InfoBlockModel
                    {
                        Title = "Standby Time",
                        Key = Tag_StandbyTime.Name,
                        Group = Grouping.sec
                    },
                    new InfoBlockModel
                    {
                        Title = "Engineering Time",
                        Key = Tag_EngineeringTime.Name,
                        Group = Grouping.sec
                    },
                    new InfoBlockModel
                    {
                        Title = "Scheduled Down Time",
                        Key = Tag_ScheduledDownTime.Name,
                        Group = Grouping.sec
                    },
                    new InfoBlockModel
                    {
                        Title = "Unscheduled Down Time",
                        Key = Tag_UnscheduledDownTime.Name,
                        Group = Grouping.sec
                    },
                    new InfoBlockModel
                    {
                        Title = "Non Scheduled Time",
                        Key = Tag_NonscheduledTime.Name,
                        Group = Grouping.sec
                    },
                    new InfoBlockModel
                    {
                        Title = "Mean Down Time",
                        Key = Tag_MeanDownTime.Name,
                        Group = Grouping.sec
                    },
                    new InfoBlockModel
                    {
                        Title = "SoftJam",
                        Key = Tag_SoftJam.Name,
                        Group = Grouping.number
                    },
                    new InfoBlockModel
                    {
                        Title = "HardJam",
                        Key = Tag_HardJam.Name,
                        Group = Grouping.number
                    },
                    new InfoBlockModel
                    {
                        Title = "MTBA",
                        Key = Tag_MTBA.Name,
                        Group = Grouping.sec
                    },
                    new InfoBlockModel
                    {
                        Title = "MTBF",
                        Key = Tag_MTBF.Name,
                        Group = Grouping.sec
                    },
                    new InfoBlockModel
                    {
                        Title = "Ideal Cycle Time",
                        Value = FormatString(Grouping.sec, _Main.IdealCycleTime)
                    }
                },
                PieChartInfo = new ObservableCollection<PieChartBlockModel>
                {
                     new PieChartBlockModel
                     {
                         Chart = OEEChart.EquipmentUptime,
                         Formula = "Productive Time + Standby Time + Engineering Time",
                         Title = "Equipment Uptime",
                         PieInfo = new SeriesCollection
                          {
                            new PieSeries { Title = "Production Time",Fill = Color1,  Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true, LabelPoint = PointLabel },
                            new PieSeries { Title = "Standby Time", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "Engineering Time", Fill = Color3, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel }
                          }
                     },
                     new PieChartBlockModel
                     {
                         Chart = OEEChart.OperationTime,
                         Formula = "Shift Time - Non Schedule Time",
                         Title = "Operation Time",
                          PieInfo = new SeriesCollection
                          {
                            new PieSeries { Title = "Operation Time", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "Non Scheduled Time", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel }
                          }
                     },
                     new PieChartBlockModel
                     {
                         Chart = OEEChart.Quality,
                          Formula = "Total Passed / (Total Passed + Total Failed)",
                          Title = "Quality",
                          PieInfo = new SeriesCollection
                          {
                            new PieSeries { Title = "Quality", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "-", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel }
                          }
                     },
                     new PieChartBlockModel
                     {
                         Chart = OEEChart.Performance,
                          Formula = "(Ideal Cycle Time * Total Count) / Operation Time",
                          Title = "Performance",
                          PieInfo = new SeriesCollection
                          {
                            new PieSeries { Title = "Performance", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "-", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel }
                          }
                     },
                     new PieChartBlockModel
                     {
                         Chart = OEEChart.Availability,
                          Formula = "Equipment Uptime / Operation Time",
                          Title = "Availability",
                          PieInfo = new SeriesCollection
                          {
                            new PieSeries { Title = "Availability", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "-", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel }
                          }
                     },
                     new PieChartBlockModel
                     {
                         Chart = OEEChart.OEE,
                          Formula = "Quality * Performance * Availability",
                          Title = "OEE",
                          PieInfo = new SeriesCollection
                          {
                            new PieSeries { Title = "OEE", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "-", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel }
                          }
                     },
                     new PieChartBlockModel
                     {
                         Chart = OEEChart.Loading,
                          Formula = "Equipment Uptime / Total Time",
                          Title = "Loading",
                          PieInfo = new SeriesCollection
                          {
                            new PieSeries { Title = "Loading", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "-", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel }
                          }
                     },
                     new PieChartBlockModel
                     {
                         Chart = OEEChart.TEEP,
                          Formula = "OEE * Loading",
                          Title = "TEEP",
                          PieInfo = new SeriesCollection
                          {
                            new PieSeries { Title = "TEEP", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "-", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel }
                          }
                     },
                     new PieChartBlockModel
                     {
                         // DeltaTime = Total - Prodution + Stanby
                         Chart = OEEChart.DeltaTime,
                          Formula = "Total Time - (Production Time + Standby Time)",
                          Title = "Delta Time",
                          PieInfo = new SeriesCollection
                          {
                              new PieSeries { Title = "Delta Time", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                              new PieSeries { Title = "Production Time", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                              new PieSeries { Title = "Standby Time", Fill = Color3, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel }
                          }
                     },
                     new PieChartBlockModel
                     {
                         Chart = OEEChart.IncomingPartFail,
                         Formula = "Zone 1 Count + Zone 2 Count",
                         Title = "Incoming Part Fail",
                         PieInfo = new SeriesCollection
                         {
                              new PieSeries { Title = "Zone 1", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                              new PieSeries { Title = "Zone 2", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },

                         }
                     }
                }
            };
            DataContext = OEEList;
        }

        double Productive, Standby, Engineering, Shift, NonSchedule, TotalPass, TotalFail,
        IdealCycleTime, TotalCount, EquipmentUpTime, OperationTime, Quality, Performance,
        Availability, OEE, Loading, Teep, DeltaTime, IncomingPartFail, IncomingPartFail_Z1, IncomingPartFail_Z2;
        private void Update()
        {
            try
            {
                if (isArcadiaMainConveyor)
                {
                    foreach (Tag tag in Info.OPC.TagGroups.OEE.Tags)
                        ctrl.ReadTag(tag);
                }
                else
                    ctrl.GroupRead(Info.OPC.TagGroups.OEE);
                _Main.IdealCycleTime = _Main.OPC.Read<double>("HMI_IdealCycleTime");
                UpdateUI();
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void UpdateUI()
        {
            foreach (Tag _tag in Info.OPC.TagGroups.OEE.Tags)
            {
                var item = OEEList.OEEInfo.Where(x => x.Key == _tag.Name).FirstOrDefault();
                if (item != null && _tag.QualityCode == ResultCode.QUAL_GOOD && _tag.Value != null)
                {
                    item.Value = FormatString(item.Group, _tag.Value);
                }
            };

            Productive = Tag_ProductiveTime.ToDouble();
            Standby = Tag_StandbyTime.ToDouble();
            Engineering = Tag_EngineeringTime.ToDouble();

            Shift = Tag_TotalTime.ToDouble();
            NonSchedule = Tag_NonscheduledTime.ToDouble();

            TotalPass = Tag_TotalPass.ToDouble();
            TotalFail = Tag_TotalFail.ToDouble();
            
            IdealCycleTime = _Main.IdealCycleTime;
            OEEList.OEEInfo.First(x => x.Title == "Ideal Cycle Time").Value = FormatString(Grouping.sec, _Main.IdealCycleTime);

            TotalCount = TotalPass + TotalFail;

            //Productive Time +Standby Time + Engineering Time
            EquipmentUpTime = Productive + Standby + Engineering;

            //Shift Time -Non Schedule Time
            OperationTime = Shift - NonSchedule;

            //Total Passed / (Total passed + Total Failed)
            double Total = TotalPass + TotalFail;
            Quality = Total <= 0 ? 0 : TotalPass / (Total);

            //(Ideal Cycle Time * Total Count) / Operation Time
            Performance = OperationTime <= 0 ? 0 : (IdealCycleTime * TotalCount) / OperationTime;

            //Equipment Uptime / Operation Time
            Availability = OperationTime <= 0 ? 0 : EquipmentUpTime / OperationTime;

            //Quality * Performance * Availability
            OEE = Quality * Performance * Availability;

            //Equipment Uptime / Total Time
            Loading = Shift <= 0 ? 0 : EquipmentUpTime / Shift;

            //OEE * Loading
            Teep = OEE * Loading;

            //DeltaTime = Total Time - (Production Time + Standby Time)
            DeltaTime = Shift - (Productive + Standby);

            IncomingPartFail_Z1 = Tag_IncomingPartFail_Z1.ToDouble();
            IncomingPartFail_Z2 = Tag_IncomingPartFail_Z2.ToDouble();
            IncomingPartFail = IncomingPartFail_Z1 + IncomingPartFail_Z2;

            foreach (var item in OEEList.PieChartInfo)
            {
                switch (item.Chart)
                {
                    case OEEChart.EquipmentUptime:
                        item.Value = FormatString(Grouping.sec, EquipmentUpTime);
                        item.PieInfo[0].Values[0] = Productive.To2Dcml();
                        item.PieInfo[1].Values[0] = Standby.To2Dcml();
                        item.PieInfo[2].Values[0] = Engineering.To2Dcml();
                        break;
                    case OEEChart.OperationTime:
                        item.Value = FormatString(Grouping.sec, OperationTime);
                        item.PieInfo[0].Values[0] = (Shift - NonSchedule).To2Dcml();
                        item.PieInfo[1].Values[0] = NonSchedule.To2Dcml();
                        break;
                    case OEEChart.Quality:
                        double QualityPercent = Quality * 100;
                        item.Value = FormatString(Grouping.percent, QualityPercent);
                        item.PieInfo[0].Values[0] = QualityPercent.To2Dcml();
                        item.PieInfo[1].Values[0] = 100 - QualityPercent.To2Dcml();
                        break;
                    case OEEChart.Performance:
                        double PerformancePercent = Performance * 100;
                        item.Value = FormatString(Grouping.percent, PerformancePercent);
                        item.PieInfo[0].Values[0] = PerformancePercent.To2Dcml();
                        item.PieInfo[1].Values[0] = 100 - PerformancePercent.To2Dcml();
                        break;
                    case OEEChart.Availability:
                        double AvailabilityPercent = Availability * 100;
                        item.Value = FormatString(Grouping.percent, AvailabilityPercent);
                        item.PieInfo[0].Values[0] = AvailabilityPercent.To2Dcml();
                        item.PieInfo[1].Values[0] = 100 - AvailabilityPercent.To2Dcml();
                        break;
                    case OEEChart.OEE:
                        double OEEPercent = OEE * 100;
                        item.Value = FormatString(Grouping.percent, OEEPercent);
                        item.PieInfo[0].Values[0] = OEEPercent.To2Dcml();
                        item.PieInfo[1].Values[0] = 100 - OEEPercent.To2Dcml();
                        break;
                    case OEEChart.Loading:
                        double LoadingPercent = Loading * 100;
                        item.Value = FormatString(Grouping.percent, LoadingPercent);
                        item.PieInfo[0].Values[0] = LoadingPercent.To2Dcml();
                        item.PieInfo[1].Values[0] = 100 - LoadingPercent.To2Dcml();
                        break;
                    case OEEChart.TEEP:
                        double TeepPercent = Teep * 100;
                        item.Value = FormatString(Grouping.percent, TeepPercent);
                        item.PieInfo[0].Values[0] = TeepPercent.To2Dcml();
                        item.PieInfo[1].Values[0] = 100 - TeepPercent.To2Dcml();
                        break;
                    case OEEChart.DeltaTime:
                        item.Value = FormatString(Grouping.sec, DeltaTime);
                        item.PieInfo[0].Values[0] = DeltaTime.To2Dcml();
                        item.PieInfo[1].Values[0] = Productive.To2Dcml();
                        item.PieInfo[2].Values[0] = Standby.To2Dcml();
                        break;
                    case OEEChart.IncomingPartFail:
                        item.Value = IncomingPartFail.ToString();
                        item.PieInfo[0].Values[0] = IncomingPartFail_Z1;
                        item.PieInfo[1].Values[0] = IncomingPartFail_Z2;
                        break;
                    default:
                        break;
                }
            }
        }

        private string FormatString(Grouping grouping, object Value)
        {
            string strValue = Value?.ToString() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(strValue)) return strValue;

            if (grouping.Equals(Grouping.sec))
            {
                TimeSpan Ts = TimeSpan.FromSeconds(Convert.ToDouble(strValue));
                int DayHours = Ts.Days * 24;
                return string.Format("{0:D2}h:{1:D2}m:{2:D2}s", Ts.Hours + DayHours, Ts.Minutes, Ts.Seconds);
            }
            else if (grouping.Equals(Grouping.number))
            {
                return strValue;
            }
            else if (grouping.Equals(Grouping.stringtime))
            {
                return string.IsNullOrWhiteSpace(strValue) ? "" : new DateTime(Convert.ToInt32(strValue.Substring(0, 4)),
                             Convert.ToInt32(strValue.Substring(4, 2)),
                             Convert.ToInt32(strValue.Substring(6, 2)),
                             Convert.ToInt32(strValue.Substring(8, 2)),
                             Convert.ToInt32(strValue.Substring(10, 2)), 0).ToString();
            }
            else if (grouping.Equals(Grouping.percent))
            {
                return Math.Round(Convert.ToDouble(strValue), 2).ToString() + "%";
            }
            return "NA";
        }

        public void Dispose()
        {
            Info.OPC.TagGroups.OEE.Active = false;
            _Main.OEEPageON = false;
        }

        private void btn_HELP_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "System Monitoring and Reliability Metrics.pdf");
            if (File.Exists(item))
                System.Diagnostics.Process.Start(item);
        }

        private void AddTagstoGroup()
        {
            ctrl.Connect();
            Info.OPC.TagGroups.OEE.AddTag(Tag_OEEStartDateTime);
            Info.OPC.TagGroups.OEE.AddTag(Tag_TotalTime);
            Info.OPC.TagGroups.OEE.AddTag(Tag_ProductiveTime);
            Info.OPC.TagGroups.OEE.AddTag(Tag_StandbyTime);
            Info.OPC.TagGroups.OEE.AddTag(Tag_EngineeringTime);
            Info.OPC.TagGroups.OEE.AddTag(Tag_ScheduledDownTime);
            Info.OPC.TagGroups.OEE.AddTag(Tag_UnscheduledDownTime);
            Info.OPC.TagGroups.OEE.AddTag(Tag_NonscheduledTime);
            Info.OPC.TagGroups.OEE.AddTag(Tag_MeanDownTime);
            Info.OPC.TagGroups.OEE.AddTag(Tag_SoftJam);
            Info.OPC.TagGroups.OEE.AddTag(Tag_HardJam);
            Info.OPC.TagGroups.OEE.AddTag(Tag_MTBA);
            Info.OPC.TagGroups.OEE.AddTag(Tag_MTBF);
            Info.OPC.TagGroups.OEE.AddTag(Tag_TotalPass);
            Info.OPC.TagGroups.OEE.AddTag(Tag_TotalFail);
            Info.OPC.TagGroups.OEE.AddTag(Tag_IncomingPartFail);
            Info.OPC.TagGroups.OEE.AddTag(Tag_IncomingPartFail_Z1);
            Info.OPC.TagGroups.OEE.AddTag(Tag_IncomingPartFail_Z2);
        }


        public class OEEModel : ViewModelBase
        {
            private ObservableCollection<PieChartBlockModel> _PieChartInfo = new ObservableCollection<PieChartBlockModel>();
            public ObservableCollection<PieChartBlockModel> PieChartInfo
            {
                get { return _PieChartInfo; }
                set
                {
                    if (_PieChartInfo != value)
                    {
                        _PieChartInfo = value;
                        RaisePropertyChanged(nameof(PieChartInfo));
                    }
                }
            }

            private ObservableCollection<InfoBlockModel> _oeeinfo = new ObservableCollection<InfoBlockModel>();
            public ObservableCollection<InfoBlockModel> OEEInfo
            {
                get { return _oeeinfo; }
                set
                {
                    if (_oeeinfo != value)
                    {
                        _oeeinfo = value;
                        RaisePropertyChanged(nameof(OEEInfo));
                    }
                }
            }
        }

        public class PieChartBlockModel : ViewModelBase
        {
            public OEEChart Chart { get; set; }
            public string Formula { get; set; }

            private string _Title = "NA";

            public string Title
            {
                get { return _Title; }
                set
                {
                    if (_Title != value)
                    {
                        _Title = value;
                        RaisePropertyChanged(nameof(Title));
                    }
                }
            }

            private string _Value = "NA";
            public string Value
            {
                get { return _Value; }
                set
                {
                    if (_Value != value)
                    {
                        _Value = value;
                        RaisePropertyChanged(nameof(Value));
                    }
                }
            }

            private SeriesCollection _PieInfo = new SeriesCollection();
            public SeriesCollection PieInfo
            {
                get { return _PieInfo; }
                set
                {
                    if (_PieInfo != value)
                    {
                        _PieInfo = value;
                        RaisePropertyChanged(nameof(PieInfo));
                    }
                }
            }
        }

        public class InfoBlockModel : ViewModelBase
        {
            public string Key { get; set; }
            public Grouping Group { get; set; }

            private string _Title = string.Empty;
            public string Title
            {
                get { return _Title; }
                set
                {
                    if (_Title != value)
                    {
                        _Title = value;
                        RaisePropertyChanged(nameof(Title));
                    }
                }
            }

            private string _Value = "NA";
            public string Value
            {
                get { return _Value; }
                set
                {
                    if (_Value != value)
                    {
                        _Value = value;
                        RaisePropertyChanged(nameof(Value));
                    }
                }
            }
        }

    }

    public static class Extension
    {
        public static double ToDouble(this Tag _tag)
        {
            if (_tag.Value == null) return 0;
            return Convert.ToDouble(_tag.Value);
        }

        public static int ToInt(this Tag _tag)
        {
            if (_tag.Value == null) return 0;
            return Convert.ToInt32(_tag.Value);
        }

        public static double To2Dcml(this double Value)
        {
            return Math.Round(Value, 2);
        }
    }
}
