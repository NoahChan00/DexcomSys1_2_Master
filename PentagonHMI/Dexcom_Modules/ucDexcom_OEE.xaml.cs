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
    public partial class ucDexcom_OEE : UserControl, IDisposable
    {
        Controller ctrl = new Controller(Info.OPC.IP);
        public Func<ChartPoint, string> PointLabel { get; set; }
        private LogicClasses.Main _Main;
        public OEEModel OEEList = new OEEModel();
        private Brush Color1 = Brushes.MediumSeaGreen;
        private Brush Color2 = Brushes.DeepSkyBlue;
        private Brush Color3 = Brushes.Orange;
        string Tag_IdealCycletime = "Shift_OEE_Tags.dint_IdealCycleTime";

        Tag Tag_Shift_OEE_STR_HMI_OEEStartDateTime = new Tag { Name = "Shift_OEE_Tags.str_HMI_OEEStartDateTime", DataType = Logix.Tag.ATOMIC.STRING };
        Tag Tag_Shift_OEE_DINT_MachineUpTimeAccSec = new Tag { Name = "Shift_OEE_Tags.dint_MachineUpTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_MachineStandbyTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineStandbyTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_NoMaterialTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_NoMaterialTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_MachineProductiveTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineProductiveTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_MachineEngineeringTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineEngineeringTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_MachineScheduledDownTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineScheduledDownTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_MachineUnscheduledDownTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineUnscheduledDownTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_MachineNonScheduledTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineNonScheduledTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_SoftJam = new Tag { Name = "Shift_OEE_Tags.DINT_SoftJam", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_HardJam = new Tag { Name = "Shift_OEE_Tags.DINT_HardJam", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_MTBASec = new Tag { Name = "Shift_OEE_Tags.DINT_MTBASec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_MTBFSec = new Tag { Name = "Shift_OEE_Tags.DINT_MTBFSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_Throughput = new Tag { Name = "Shift_OEE_Tags.DINT_Throughput", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_SprintUPH = new Tag { Name = "Shift_OEE_Tags.DINT_SprintUPH", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_PlannedProductiveTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_PlannedProductiveTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_AvailabilityTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_AvailabilityTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_IdealCycleTime = new Tag { Name = "Shift_OEE_Tags.DINT_IdealCycleTime", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_IdealRunTime = new Tag { Name = "Shift_OEE_Tags.DINT_IdealRunTime", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_Performance = new Tag { Name = "Shift_OEE_Tags.DINT_Performance", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_Quality = new Tag { Name = "Shift_OEE_Tags.DINT_Quality", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_OEE = new Tag { Name = "Shift_OEE_Tags.DINT_OEE", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_Total_Pass = new Tag { Name = "Shift_OEE_Tags.DINT_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Shift_OEE_DINT_Total_Fail = new Tag { Name = "Shift_OEE_Tags.DINT_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };

        Tag Tag_Lot_OEE_str_HMI_OEEStartDateTime = new Tag { Name = "Lot_OEE_Tags.str_HMI_OEEStartDateTime", DataType = Logix.Tag.ATOMIC.STRING };
        Tag Tag_Lot_OEE_dint_MachineUpTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineUpTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_MachineStandbyTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineStandbyTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_NoMaterialTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_NoMaterialTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_MachineProductiveTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineProductiveTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_MachineEngineeringTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineEngineeringTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_MachineScheduledDownTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineScheduledDownTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_MachineUnscheduledDownTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineUnscheduledDownTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_MachineNonScheduledTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineNonScheduledTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_SoftJam = new Tag { Name = "Lot_OEE_Tags.dint_SoftJam", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_HardJam = new Tag { Name = "Lot_OEE_Tags.dint_HardJam", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_MTBASec = new Tag { Name = "Lot_OEE_Tags.dint_MTBASec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_MTBFSec = new Tag { Name = "Lot_OEE_Tags.dint_MTBFSec", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_Throughput = new Tag { Name = "Lot_OEE_Tags.dint_Throughput", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_SprintUPH = new Tag { Name = "Lot_OEE_Tags.dint_SprintUPH", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_Quality = new Tag { Name = "Lot_OEE_Tags.dint_Quality", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_Total_Pass = new Tag { Name = "Lot_OEE_Tags.dint_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
        Tag Tag_Lot_OEE_dint_Total_Fail = new Tag { Name = "Lot_OEE_Tags.dint_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };

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

        private OEEType OEEGrp;
        public enum OEEType
        {
            Shift,
            Lot
        }

        public ucDexcom_OEE(ref LogicClasses.Main main, OEEType OEE)
        {
            try
            {
                InitializeComponent();
                OEEGrp = OEE;
                _Main = main;
                Initialize();
                _Main.OnOEEUpdate += new LogicClasses.Main.onOEEUpdateHandler(Update);
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void Initialize()
        {
            AddTagstoGroup();

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            _Main.IdealCycleTime = _Main.OPC.Read<Int32>(Tag_IdealCycletime);

            if (OEEGrp == OEEType.Shift)
            {
                lbl_Tittle.Content = "Shift OEE";
                OEEList = new OEEModel
                {
                    OEEInfo = new ObservableCollection<InfoBlockModel>
                    {
                        new InfoBlockModel
                        {
                           Title = "Start Date Time",
                           Key = Tag_Shift_OEE_STR_HMI_OEEStartDateTime.Name,
                           Group =  Grouping.stringtime
                        },
                        new InfoBlockModel
                        {
                           Title = "Up Time",
                           Key = Tag_Shift_OEE_DINT_MachineUpTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Standby Time",
                           Key = Tag_Shift_OEE_DINT_MachineStandbyTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "No Material Time",
                           Key = Tag_Shift_OEE_DINT_NoMaterialTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Productive Time",
                           Key = Tag_Shift_OEE_DINT_MachineProductiveTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Engineering Time",
                           Key = Tag_Shift_OEE_DINT_MachineEngineeringTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Scheduled Down Time",
                           Key = Tag_Shift_OEE_DINT_MachineScheduledDownTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Unscheduled Down Time",
                           Key = Tag_Shift_OEE_DINT_MachineUnscheduledDownTimeAccSec.Name,
                           Group =  Grouping.sec

                        },
                        new InfoBlockModel
                        {
                           Title = "Non Scheduled Time",
                           Key = Tag_Shift_OEE_DINT_MachineNonScheduledTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Soft Jam",
                           Key = Tag_Shift_OEE_DINT_SoftJam.Name,
                           Group =  Grouping.number
                        },
                        new InfoBlockModel
                        {
                           Title = "Hard Jam",
                           Key = Tag_Shift_OEE_DINT_HardJam.Name,
                           Group =  Grouping.number
                        },
                        new InfoBlockModel
                        {
                           Title = "MTBA",
                           Key = Tag_Shift_OEE_DINT_MTBASec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "MTBF",
                           Key = Tag_Shift_OEE_DINT_MTBFSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Throughput",
                           Key = Tag_Shift_OEE_DINT_Throughput.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Sprint UPH",
                           Key = Tag_Shift_OEE_DINT_SprintUPH.Name,
                           Group =  Grouping.sec
                        },
                         new InfoBlockModel
                        {
                           Title = "Planned Productive Time",
                           Key = Tag_Shift_OEE_DINT_PlannedProductiveTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                         new InfoBlockModel
                        {
                           Title = "Availability Time",
                           Key = Tag_Shift_OEE_DINT_AvailabilityTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                         new InfoBlockModel
                        {
                           Title = "Ideal Cycle Time",
                           Key = Tag_Shift_OEE_DINT_IdealCycleTime.Name,
                           Group =  Grouping.sec
                        },
                         new InfoBlockModel
                        {
                           Title = "Ideal Run Time",
                           Key = Tag_Shift_OEE_DINT_IdealRunTime.Name,
                           Group =  Grouping.sec
                        },
                        // new InfoBlockModel
                        //{
                        //   Title = "Total Pass",
                        //   Key =  Tag_Shift_OEE_DINT_Total_Pass.Name,
                        //   Group =  Grouping.number
                        //},
                        // new InfoBlockModel
                        //{
                        //   Title = "Total Fail",
                        //   Key = Tag_Shift_OEE_DINT_Total_Fail.Name,
                        //   Group =  Grouping.number
                        //},
                         new InfoBlockModel
                        {
                           Title = "Performance",
                           Key = Tag_Shift_OEE_DINT_Performance.Name,
                           Group =  Grouping.percent
                        },
                         new InfoBlockModel
                        {
                           Title = "Quality",
                           Key = Tag_Shift_OEE_DINT_Quality.Name,
                           Group =  Grouping.percent
                        },
                         new InfoBlockModel
                        {
                           Title = "OEE",
                           Key = Tag_Shift_OEE_DINT_OEE.Name,
                           Group = Grouping.number 
                        },
                    },
                };
            }
            else if (OEEGrp == OEEType.Lot)
            {
                lbl_Tittle.Content = "Lot OEE";
                OEEList = new OEEModel
                {
                    OEEInfo = new ObservableCollection<InfoBlockModel>
                    {
                        new InfoBlockModel
                        {
                           Title = "Start Date Time",
                           Key = Tag_Lot_OEE_str_HMI_OEEStartDateTime.Name,
                           Group =  Grouping.stringtime
                        },
                        new InfoBlockModel
                        {
                           Title = "Up Time",
                           Key = Tag_Lot_OEE_dint_MachineUpTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Standby Time",
                           Key = Tag_Lot_OEE_dint_MachineStandbyTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "No Material Time",
                           Key = Tag_Lot_OEE_dint_NoMaterialTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Productive Time",
                           Key = Tag_Lot_OEE_dint_MachineProductiveTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Engineering Time",
                           Key = Tag_Lot_OEE_dint_MachineEngineeringTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Schedule Downtime",
                           Key = Tag_Lot_OEE_dint_MachineScheduledDownTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Unschedule Downtime",
                           Key = Tag_Lot_OEE_dint_MachineUnscheduledDownTimeAccSec.Name,
                           Group =  Grouping.sec

                        },
                        new InfoBlockModel
                        {
                           Title = "Non Schedule",
                           Key = Tag_Lot_OEE_dint_MachineNonScheduledTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Soft Jam",
                           Key = Tag_Lot_OEE_dint_SoftJam.Name,
                           Group =  Grouping.number
                        },
                        new InfoBlockModel
                        {
                           Title = "Hard Jam",
                           Key = Tag_Lot_OEE_dint_HardJam.Name,
                           Group =  Grouping.number
                        },
                        new InfoBlockModel
                        {
                           Title = "MTBA",
                           Key = Tag_Lot_OEE_dint_MTBASec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "MTBF",
                           Key = Tag_Lot_OEE_dint_MTBFSec.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Throughput",
                           Key = Tag_Lot_OEE_dint_Throughput.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Sprint UPH",
                           Key = Tag_Lot_OEE_dint_SprintUPH.Name,
                           Group =  Grouping.sec
                        },
                        new InfoBlockModel
                        {
                           Title = "Quality",
                           Key = Tag_Lot_OEE_dint_Quality.Name,
                           Group =  Grouping.percent
                        },
                        // new InfoBlockModel
                        //{
                        //   Title = "Total Pass",
                        //   Key =  Tag_Lot_OEE_dint_Total_Pass.Name,
                        //   Group =  Grouping.number
                        //},
                        // new InfoBlockModel
                        //{
                        //   Title = "Total Fail",
                        //   Key = Tag_Lot_OEE_dint_Total_Fail.Name,
                        //   Group =  Grouping.number
                        //},
                    }
                };
            }

            OEEList.PieChartInfo = new ObservableCollection<PieChartBlockModel>
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
                ctrl.GroupRead(Info.OPC.TagGroups.OEE);
                _Main.IdealCycleTime = _Main.OPC.Read<Int32>(Tag_IdealCycletime);
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
                    item.Value = FormatString(item.Group, _tag.Value);
            };

            if(OEEGrp == OEEType.Shift)
            {
                Productive = Tag_Shift_OEE_DINT_MachineProductiveTimeAccSec.ToDouble();
                Standby = Tag_Shift_OEE_DINT_MachineStandbyTimeAccSec.ToDouble();
                Engineering = Tag_Shift_OEE_DINT_MachineEngineeringTimeAccSec.ToDouble();

                Shift =  Tag_Shift_OEE_DINT_MachineUpTimeAccSec.ToDouble();
                NonSchedule = Tag_Shift_OEE_DINT_MachineNonScheduledTimeAccSec.ToDouble();

                TotalPass = Tag_Shift_OEE_DINT_Total_Pass.ToDouble();
                TotalFail = Tag_Shift_OEE_DINT_Total_Fail.ToDouble();
            }
            else if(OEEGrp == OEEType.Lot)
            {
                Productive = Tag_Lot_OEE_dint_MachineProductiveTimeAccSec.ToDouble();
                Standby = Tag_Lot_OEE_dint_MachineStandbyTimeAccSec.ToDouble();
                Engineering = Tag_Lot_OEE_dint_MachineEngineeringTimeAccSec.ToDouble();

                Shift = Tag_Lot_OEE_dint_MachineUpTimeAccSec.ToDouble();
                NonSchedule = Tag_Lot_OEE_dint_MachineNonScheduledTimeAccSec.ToDouble();

                TotalPass = Tag_Lot_OEE_dint_Total_Pass.ToDouble();
                TotalFail = Tag_Lot_OEE_dint_Total_Fail.ToDouble();
            }

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
            if (OEEGrp == OEEType.Shift)
            {
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_STR_HMI_OEEStartDateTime);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.str_HMI_OEEStartDateTime", DataType);// Logix.Tag.ATOMIC.STRING };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineUpTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.dint_MachineUpTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineStandbyTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_MachineStandbyTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_NoMaterialTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_NoMaterialTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineProductiveTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_MachineProductiveTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineEngineeringTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_MachineEngineeringTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineScheduledDownTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_MachineScheduledDownTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineUnscheduledDownTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_MachineUnscheduledDownTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineNonScheduledTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_MachineNonScheduledTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_SoftJam);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_SoftJam", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_HardJam);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_HardJam", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MTBASec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_MTBASec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MTBFSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_MTBFSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Throughput);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_Throughput", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_SprintUPH);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_SprintUPH", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_PlannedProductiveTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_PlannedProductiveTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_AvailabilityTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_AvailabilityTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_IdealCycleTime);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_IdealCycleTime", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_IdealRunTime);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_IdealRunTime", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Performance);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_Performance", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Quality);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_Quality", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_OEE);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_OEE", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Total_Pass);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_Total_Pass", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Total_Fail);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Shift_OEE_Tags.DINT_Total_Fail", DataType);// Logix.Tag.ATOMIC.DINT };

            }
            else if (OEEGrp == OEEType.Lot)
            {
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_str_HMI_OEEStartDateTime);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.str_HMI_OEEStartDateTime", DataType);// Logix.Tag.ATOMIC.STRING };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineUpTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_MachineUpTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineStandbyTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_MachineStandbyTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_NoMaterialTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_NoMaterialTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineProductiveTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_MachineProductiveTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineEngineeringTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_MachineEngineeringTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineScheduledDownTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_MachineScheduledDownTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineUnscheduledDownTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_MachineUnscheduledDownTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineNonScheduledTimeAccSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_MachineNonScheduledTimeAccSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_SoftJam);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_SoftJam", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_HardJam);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_HardJam", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MTBASec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_MTBASec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MTBFSec);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_MTBFSec", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_Throughput);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_Throughput", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_SprintUPH);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_SprintUPH", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_Quality);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_Quality", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_Total_Pass);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_Total_Pass", DataType);// Logix.Tag.ATOMIC.DINT };
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_Total_Fail);// new Info.OPC.TagGroups.OEE.AddTag({ Name);// "Lot_OEE_Tags.dint_Total_Fail", DataType);// Logix.Tag.ATOMIC.DINT };
            }
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
}
