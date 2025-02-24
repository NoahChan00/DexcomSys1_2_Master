using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using Logix;
using PentagonHMI.Classes;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using Utilities;

namespace PentagonHMI.ChildControls
{
    public partial class ucDexcom_OEE : UserControl, IDisposable
    {
        private Controller ctrl = new Controller(Info.OPC.IP);

        public Func<ChartPoint, string> PointLabel
        {
            get; set;
        }

        private LogicClasses.Main _Main;
        public OEEModel OEEList = new OEEModel();
        private Brush Color1 = Brushes.MediumSeaGreen;
        private Brush Color2 = Brushes.DeepSkyBlue;
        private Brush Color3 = Brushes.Orange;
        private Brush Color4 = Brushes.Red;
        private Brush Color5 = Brushes.Violet;
        // Even the tagname got dint in it, it was real
        private string Tag_IdealCycletime = "Shift_OEE_Tags.dint_IdealCycleTime";

        private Tag Tag_Shift_OEE_STR_HMI_OEEStartDateTime = new Tag { Name = "Shift_OEE_Tags.str_HMI_OEEStartDateTime", DataType = Logix.Tag.ATOMIC.STRING };
        private Tag Tag_Shift_OEE_DINT_MachineUpTimeAccSec = new Tag { Name = "Shift_OEE_Tags.dint_MachineUpTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_MachineStandbyTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineStandbyTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_NoMaterialTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_NoMaterialTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_MachineProductiveTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineProductiveTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_MachineEngineeringTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineEngineeringTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_MachineScheduledDownTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineScheduledDownTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_MachineUnscheduledDownTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineUnscheduledDownTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_MachineNonScheduledTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_MachineNonScheduledTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_SoftJam = new Tag { Name = "Shift_OEE_Tags.DINT_SoftJam", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_HardJam = new Tag { Name = "Shift_OEE_Tags.DINT_HardJam", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_MTBASec = new Tag { Name = "Shift_OEE_Tags.DINT_MTBASec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_MTBFSec = new Tag { Name = "Shift_OEE_Tags.DINT_MTBFSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_Throughput = new Tag { Name = "Shift_OEE_Tags.DINT_Throughput", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_SprintUPH = new Tag { Name = "Shift_OEE_Tags.DINT_SprintUPH", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_PlannedProductiveTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_PlannedProductiveTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_AvailabilityTimeAccSec = new Tag { Name = "Shift_OEE_Tags.DINT_AvailabilityTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_IdealCycleTime = new Tag { Name = "Shift_OEE_Tags.DINT_IdealCycleTime", DataType = Logix.Tag.ATOMIC.REAL };
        private Tag Tag_Shift_OEE_DINT_IdealRunTime = new Tag { Name = "Shift_OEE_Tags.DINT_IdealRunTime", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_Performance = new Tag { Name = "Shift_OEE_Tags.DINT_Performance", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_Quality = new Tag { Name = "Shift_OEE_Tags.DINT_Quality", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_OEE = new Tag { Name = "Shift_OEE_Tags.DINT_OEE", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_Total_Pass = new Tag { Name = "Shift_OEE_Tags.DINT_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_Total_Fail = new Tag { Name = "Shift_OEE_Tags.DINT_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_Total_TotalProductiveUnit = new Tag { Name = "Shift_OEE_Tags.dint_TotalProductiveUnit", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Shift_OEE_DINT_Total_PartFail = new Tag { Name = "Shift_OEE_Tags.dint_Total_PartFail", DataType = Logix.Tag.ATOMIC.DINT };

        private Tag Tag_Lot_OEE_str_HMI_OEEStartDateTime = new Tag { Name = "Lot_OEE_Tags.str_HMI_OEEStartDateTime", DataType = Logix.Tag.ATOMIC.STRING };
        private Tag Tag_Lot_OEE_dint_MachineUpTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineUpTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_MachineStandbyTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineStandbyTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_NoMaterialTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_NoMaterialTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_MachineProductiveTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineProductiveTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_MachineEngineeringTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineEngineeringTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_MachineScheduledDownTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineScheduledDownTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_MachineUnscheduledDownTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineUnscheduledDownTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_MachineNonScheduledTimeAccSec = new Tag { Name = "Lot_OEE_Tags.dint_MachineNonScheduledTimeAccSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_SoftJam = new Tag { Name = "Lot_OEE_Tags.dint_SoftJam", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_HardJam = new Tag { Name = "Lot_OEE_Tags.dint_HardJam", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_MTBASec = new Tag { Name = "Lot_OEE_Tags.dint_MTBASec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_MTBFSec = new Tag { Name = "Lot_OEE_Tags.dint_MTBFSec", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_Throughput = new Tag { Name = "Lot_OEE_Tags.dint_Throughput", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_SprintUPH = new Tag { Name = "Lot_OEE_Tags.dint_SprintUPH", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_Quality = new Tag { Name = "Lot_OEE_Tags.dint_Quality", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_Total_Pass = new Tag { Name = "Lot_OEE_Tags.dint_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_Total_Fail = new Tag { Name = "Lot_OEE_Tags.dint_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_TotalProductiveUnit = new Tag { Name = "Lot_OEE_Tags.dint_TotalProductiveUnit", DataType = Logix.Tag.ATOMIC.DINT };
        private Tag Tag_Lot_OEE_dint_TotalPartFail = new Tag { Name = "Lot_OEE_Tags.dint_Total_PartFail", DataType = Logix.Tag.ATOMIC.DINT };


        private Tag Tag_Lot_OEE_real_TurretNestA = new Tag { Name = "TurretNest_A.Socket_Yield", DataType = Logix.Tag.ATOMIC.REAL };
        private Tag Tag_Lot_OEE_real_TurretNestB = new Tag { Name = "TurretNest_B.Socket_Yield", DataType = Logix.Tag.ATOMIC.REAL };
        private Tag Tag_Lot_OEE_real_TurretNestC = new Tag { Name = "TurretNest_C.Socket_Yield", DataType = Logix.Tag.ATOMIC.REAL };
        private Tag Tag_Lot_OEE_real_TurretNestD = new Tag { Name = "TurretNest_D.Socket_Yield", DataType = Logix.Tag.ATOMIC.REAL };
        private Tag Tag_Lot_OEE_real_TurretNestE = new Tag { Name = "TurretNest_E.Socket_Yield", DataType = Logix.Tag.ATOMIC.REAL };
        private Tag Tag_Lot_OEE_real_TurretNestF = new Tag { Name = "TurretNest_F.Socket_Yield", DataType = Logix.Tag.ATOMIC.REAL };
        private Tag Tag_Lot_OEE_real_TurretNestG = new Tag { Name = "TurretNest_G.Socket_Yield", DataType = Logix.Tag.ATOMIC.REAL };
        private Tag Tag_Lot_OEE_real_TurretNestH = new Tag { Name = "TurretNest_H.Socket_Yield", DataType = Logix.Tag.ATOMIC.REAL };
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
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void Initialize()
        {
            AddTagstoGroup();

            PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
            _Main.IdealCycleTime = _Main.OPC.Read<double>(Tag_IdealCycletime);

            if(OEEGrp == OEEType.Shift)
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
                           Title = "Operation Time",
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
                        //new InfoBlockModel
                        //{
                        //   Title = "Scheduled Downtime",
                        //   Key = Tag_Shift_OEE_DINT_MachineScheduledDownTimeAccSec.Name,
                        //   Group =  Grouping.sec
                        //},
                        new InfoBlockModel
                        {
                           Title = "Unscheduled Downtime",
                           Key = Tag_Shift_OEE_DINT_MachineUnscheduledDownTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        //new InfoBlockModel
                        //{
                        //   Title = "Non Scheduled Time",
                        //   Key = Tag_Shift_OEE_DINT_MachineNonScheduledTimeAccSec.Name,
                        //   Group =  Grouping.sec
                        //},
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
                           Group =  Grouping.number
                        },
                        new InfoBlockModel
                        {
                           Title = "Sprint UPH",
                           Key = Tag_Shift_OEE_DINT_SprintUPH.Name,
                           Group =  Grouping.number
                        },
                         new InfoBlockModel
                        {
                           Title = "Planned Production Time",
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
                         new InfoBlockModel
                         {
                             Title= "Total Pass",
                             Key=  Tag_Shift_OEE_DINT_Total_Pass.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Total Fail",
                             Key=  Tag_Shift_OEE_DINT_Total_Fail.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Total Productive Unit",
                             Key=  Tag_Shift_OEE_DINT_Total_TotalProductiveUnit.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Total Part Fail",
                             Key=  Tag_Shift_OEE_DINT_Total_PartFail.Name,
                             Group = Grouping.number
                         },
                    },
                };
            }
            else if(OEEGrp == OEEType.Lot)
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
                           Title = "Operation Time",
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
                        //new InfoBlockModel
                        //{
                        //   Title = "Scheduled Downtime",
                        //   Key = Tag_Lot_OEE_dint_MachineScheduledDownTimeAccSec.Name,
                        //   Group =  Grouping.sec
                        //},
                        new InfoBlockModel
                        {
                           Title = "Unscheduled Downtime",
                           Key = Tag_Lot_OEE_dint_MachineUnscheduledDownTimeAccSec.Name,
                           Group =  Grouping.sec
                        },
                        //new InfoBlockModel
                        //{
                        //   Title = "Non Schedule",
                        //   Key = Tag_Lot_OEE_dint_MachineNonScheduledTimeAccSec.Name,
                        //   Group =  Grouping.sec
                        //},
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
                           Group =  Grouping.number
                        },
                        new InfoBlockModel
                        {
                           Title = "Sprint UPH",
                           Key = Tag_Lot_OEE_dint_SprintUPH.Name,
                           Group =  Grouping.number
                        },
                        new InfoBlockModel
                        {
                           Title = "Quality",
                           Key = Tag_Lot_OEE_dint_Quality.Name,
                           Group =  Grouping.percent
                        },
                         new InfoBlockModel
                        {
                           Title = "Total Pass",
                           Key =  Tag_Lot_OEE_dint_Total_Pass.Name,
                           Group =  Grouping.number
                        },
                         new InfoBlockModel
                         {
                           Title = "Total Fail",
                           Key = Tag_Lot_OEE_dint_Total_Fail.Name,
                           Group =  Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Total Productive Unit",
                             Key=  Tag_Lot_OEE_dint_TotalProductiveUnit.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Total Part Fail",
                             Key=  Tag_Lot_OEE_dint_TotalPartFail.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Turret Nest A",
                             Key=  Tag_Lot_OEE_real_TurretNestA.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Turret Nest B",
                             Key=  Tag_Lot_OEE_real_TurretNestB.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Turret Nest C",
                             Key=  Tag_Lot_OEE_real_TurretNestC.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Turret Nest D",
                             Key=  Tag_Lot_OEE_real_TurretNestD.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Turret Nest E",
                             Key=  Tag_Lot_OEE_real_TurretNestE.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Turret Nest F",
                             Key=  Tag_Lot_OEE_real_TurretNestF.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Turret Nest G",
                             Key=  Tag_Lot_OEE_real_TurretNestG.Name,
                             Group = Grouping.number
                         },
                         new InfoBlockModel
                         {
                             Title= "Turret Nest H",
                             Key=  Tag_Lot_OEE_real_TurretNestH.Name,
                             Group = Grouping.number
                         },
                    }
                };
            }

            OEEList.PieChartInfo = new ObservableCollection<PieChartBlockModel>
            {
                new PieChartBlockModel
                     {
                         Chart = OEEChart.EquipmentUptime,
                         Formula = "Productive Time + Standby Time + Engineering Time + No Material Time",
                         Title = "Equipment Uptime",
                         PieInfo = new SeriesCollection
                          {
                            new PieSeries { Title = "Production Time",Fill = Color1,  Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true, LabelPoint = PointLabel },
                            new PieSeries { Title = "Standby Time", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "Engineering Time", Fill = Color3, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries {Title = "No Material Time", Fill = Color4, Values = new ChartValues<double>(new double[] {0 }), DataLabels=true, LabelPoint=PointLabel}
                          }
                     },
                     new PieChartBlockModel
                     {
                         Chart = OEEChart.OperationTime,
                         Formula = "Productive Time + Standby Time + No Material Time + Engineering Time + Unscheduled Downtime",
                         Title = "Operation Time",
                          PieInfo = new SeriesCollection
                          {
                            new PieSeries { Title = "Productive Time", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "Standby Time", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "No Material Time", Fill = Color3, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "Engineering Time", Fill = Color4, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                            new PieSeries { Title = "Unscheduled Downtime", Fill = Color5, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
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
            };

            DataContext = OEEList;
        }

        private double Productive, Standby, Engineering, NoMaterialTime, Shift,Unschedule, NonSchedule, TotalPass, TotalFail,
        IdealCycleTime, TotalCount, EquipmentUpTime, OperationTime, Quality, Performance,
        Availability, OEE, Loading, Teep, DeltaTime;

        private void Update()
        {
            try
            {
                ctrl.GroupRead(Info.OPC.TagGroups.OEE);
                _Main.IdealCycleTime = _Main.OPC.Read<double>(Tag_IdealCycletime);
                UpdateUI();
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void UpdateUI()
        {
            foreach(Tag _tag in Info.OPC.TagGroups.OEE.Tags)
            {
                var item = OEEList.OEEInfo.Where(x => x.Key == _tag.Name).FirstOrDefault();
                if(item != null && _tag.QualityCode == ResultCode.QUAL_GOOD && _tag.Value != null)
                    item.Value = FormatString(item.Group, _tag.Value);
            };

            if(OEEGrp == OEEType.Shift)
            {
                Productive = Tag_Shift_OEE_DINT_MachineProductiveTimeAccSec.ToDouble();
                Standby = Tag_Shift_OEE_DINT_MachineStandbyTimeAccSec.ToDouble();
                Engineering = Tag_Shift_OEE_DINT_MachineEngineeringTimeAccSec.ToDouble();
                NoMaterialTime = Tag_Shift_OEE_DINT_NoMaterialTimeAccSec.ToDouble();

                Shift = Tag_Shift_OEE_DINT_MachineUpTimeAccSec.ToDouble();
                NonSchedule = Tag_Shift_OEE_DINT_MachineNonScheduledTimeAccSec.ToDouble();
                Unschedule = Tag_Shift_OEE_DINT_MachineUnscheduledDownTimeAccSec.ToDouble();

                TotalPass = Tag_Shift_OEE_DINT_Total_Pass.ToDouble();
                TotalFail = Tag_Shift_OEE_DINT_Total_Fail.ToDouble();
            }
            else if(OEEGrp == OEEType.Lot)
            {
                Productive = Tag_Lot_OEE_dint_MachineProductiveTimeAccSec.ToDouble();
                Standby = Tag_Lot_OEE_dint_MachineStandbyTimeAccSec.ToDouble();
                Engineering = Tag_Lot_OEE_dint_MachineEngineeringTimeAccSec.ToDouble();
                NoMaterialTime = Tag_Lot_OEE_dint_NoMaterialTimeAccSec.ToDouble();

                Shift = Tag_Lot_OEE_dint_MachineUpTimeAccSec.ToDouble();
                NonSchedule = Tag_Lot_OEE_dint_MachineNonScheduledTimeAccSec.ToDouble();
                Unschedule = Tag_Lot_OEE_dint_MachineUnscheduledDownTimeAccSec.ToDouble();

                TotalPass = Tag_Lot_OEE_dint_Total_Pass.ToDouble();
                TotalFail = Tag_Lot_OEE_dint_Total_Fail.ToDouble();
            }

            IdealCycleTime = _Main.IdealCycleTime;



            InfoBlockModel model = OEEList.OEEInfo.FirstOrDefault(x => x.Title.Contains("Ideal Cycle Time"));
            if(model != null)
                model.Value = FormatString(Grouping.sec, IdealCycleTime);


            TotalCount = TotalPass + TotalFail;


            EquipmentUpTime = Productive + Standby + Engineering + NoMaterialTime;


            OperationTime = Shift - NonSchedule;


            double Total = TotalPass + TotalFail;
            Quality = Total <= 0 ? 0 : TotalPass / (Total);


            Performance = OperationTime <= 0 ? 0 : (IdealCycleTime * TotalCount) / OperationTime;


            Availability = OperationTime <= 0 ? 0 : EquipmentUpTime / OperationTime;


            OEE = Quality * Performance * Availability;


            Loading = Shift <= 0 ? 0 : EquipmentUpTime / Shift;


            Teep = OEE * Loading;


            DeltaTime = Shift - (Productive + Standby);

            foreach(var item in OEEList.PieChartInfo)
            {
                switch(item.Chart)
                {
                    //new PieChartBlockModel
                    //     {
                    //         Chart = OEEChart.EquipmentUptime,
                    //         Formula = "Productive Time + Standby Time + Engineering Time + No Material Time",
                    //         Title = "Equipment Uptime",
                    //         PieInfo = new SeriesCollection
                    //          {
                    //            new PieSeries { Title = "Production Time",Fill = Color1,  Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true, LabelPoint = PointLabel },
                    //            new PieSeries { Title = "Standby Time", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                    //            new PieSeries { Title = "Engineering Time", Fill = Color3, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                    //            new PieSeries {Title = "No Material Time", Fill = Color4, Values = new ChartValues<double>(new double[] {0 }), DataLabels=true, LabelPoint=PointLabel}
                    //          }
                    //     },
                    //     new PieChartBlockModel
                    //     {
                    //         Chart = OEEChart.OperationTime,
                    //         Formula = "Productive Time + Standby Time + No Material Time + Engineering Time + Unscheduled Downtime",
                    //         Title = "Operation Time",
                    //          PieInfo = new SeriesCollection
                    //          {
                    //            new PieSeries { Title = "Productive Time", Fill = Color1, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                    //            new PieSeries { Title = "Standby Time", Fill = Color2, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                    //            new PieSeries { Title = "No Material Time", Fill = Color3, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                    //            new PieSeries { Title = "Engineering Time", Fill = Color4, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                    //            new PieSeries { Title = "Unscheduled Downtime", Fill = Color5, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true,  LabelPoint = PointLabel },
                    //          }
                    //     },
                    case OEEChart.EquipmentUptime:
                        item.Value = FormatString(Grouping.sec, EquipmentUpTime);
                        item.PieInfo[0].Values[0] = Productive.To2Dcml();
                        item.PieInfo[1].Values[0] = Standby.To2Dcml();
                        item.PieInfo[2].Values[0] = Engineering.To2Dcml();
                        item.PieInfo[3].Values[0] = NoMaterialTime.To2Dcml();
                        ;
                        break;

                    case OEEChart.OperationTime:
                        item.Value = FormatString(Grouping.sec, OperationTime);
                        item.PieInfo[0].Values[0] = Productive.To2Dcml();
                        item.PieInfo[1].Values[0] = Standby.To2Dcml();
                        item.PieInfo[2].Values[0] = NoMaterialTime.To2Dcml();
                        item.PieInfo[3].Values[0] = Engineering.To2Dcml();
                        item.PieInfo[4].Values[0] = Unschedule.To2Dcml();
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

                    default:
                        break;
                }
            }
        }

        private string FormatString(Grouping grouping, object Value)
        {
            try
            {
                string strValue = Value?.ToString() ?? string.Empty;
                if(string.IsNullOrWhiteSpace(strValue))
                    return strValue;

                if(grouping.Equals(Grouping.sec))
                {
                    TimeSpan Ts = TimeSpan.FromSeconds(Convert.ToDouble(strValue));
                    int DayHours = Ts.Days * 24;
                    return string.Format("{0:D2}h:{1:D2}m:{2:D2}s", Ts.Hours + DayHours, Ts.Minutes, Ts.Seconds);
                }
                else if(grouping.Equals(Grouping.number))
                {
                    return strValue;
                }
                else if(grouping.Equals(Grouping.stringtime))
                {
                    try
                    {
                        return string.IsNullOrWhiteSpace(strValue) ? "" : new DateTime(Convert.ToInt32(strValue.Substring(0, 4)),
                            Convert.ToInt32(strValue.Substring(4, 2)),
                            Convert.ToInt32(strValue.Substring(6, 2)),
                            Convert.ToInt32(strValue.Substring(8, 2)),
                            Convert.ToInt32(strValue.Substring(10, 2)), 0).ToString();
                    }
                    catch(Exception)
                    {
                        return "Error DateTime";
                    }
                }
                else if(grouping.Equals(Grouping.percent))
                {
                    return Math.Round(Convert.ToDouble(strValue), 2).ToString() + "%";
                }
            }
            catch(Exception e)
            {
                FileLogger.logError(e.Message, e.StackTrace);
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
            if(File.Exists(item))
                System.Diagnostics.Process.Start(item);
        }

        private void AddTagstoGroup()
        {
            ctrl.Connect();
            if(OEEGrp == OEEType.Shift)
            {
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_STR_HMI_OEEStartDateTime);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineUpTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineStandbyTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_NoMaterialTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineProductiveTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineEngineeringTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineScheduledDownTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineUnscheduledDownTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MachineNonScheduledTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_SoftJam);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_HardJam);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MTBASec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_MTBFSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Throughput);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_SprintUPH);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_PlannedProductiveTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_AvailabilityTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_IdealCycleTime);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_IdealRunTime);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Performance);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Quality);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_OEE);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Total_Pass);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Total_Fail);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Total_TotalProductiveUnit);
                //if(GlobalFunctions.IsSystem1)
                //{
                    Info.OPC.TagGroups.OEE.AddTag(Tag_Shift_OEE_DINT_Total_PartFail);
                //}
            }
            else if(OEEGrp == OEEType.Lot)
            {
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_str_HMI_OEEStartDateTime);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineUpTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineStandbyTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_NoMaterialTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineProductiveTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineEngineeringTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineScheduledDownTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineUnscheduledDownTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MachineNonScheduledTimeAccSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_SoftJam);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_HardJam);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MTBASec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_MTBFSec);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_Throughput);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_SprintUPH);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_Quality);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_Total_Pass);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_Total_Fail);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_TotalProductiveUnit);
                //if(GlobalFunctions.IsSystem1)
                //{
                    Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_dint_TotalPartFail);
                //}
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_real_TurretNestA);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_real_TurretNestB);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_real_TurretNestC);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_real_TurretNestD);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_real_TurretNestE);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_real_TurretNestF);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_real_TurretNestG);
                Info.OPC.TagGroups.OEE.AddTag(Tag_Lot_OEE_real_TurretNestH);
            }
        }

        public class OEEModel : ViewModelBase
        {
            private ObservableCollection<PieChartBlockModel> _PieChartInfo = new ObservableCollection<PieChartBlockModel>();

            public ObservableCollection<PieChartBlockModel> PieChartInfo
            {
                get
                {
                    return _PieChartInfo;
                }
                set
                {
                    if(_PieChartInfo != value)
                    {
                        _PieChartInfo = value;
                        RaisePropertyChanged(nameof(PieChartInfo));
                    }
                }
            }

            private ObservableCollection<InfoBlockModel> _oeeinfo = new ObservableCollection<InfoBlockModel>();

            public ObservableCollection<InfoBlockModel> OEEInfo
            {
                get
                {
                    return _oeeinfo;
                }
                set
                {
                    if(_oeeinfo != value)
                    {
                        _oeeinfo = value;
                        RaisePropertyChanged(nameof(OEEInfo));
                    }
                }
            }
        }

        public class PieChartBlockModel : ViewModelBase
        {
            public OEEChart Chart
            {
                get; set;
            }

            public string Formula
            {
                get; set;
            }

            private string _Title = "NA";

            public string Title
            {
                get
                {
                    return _Title;
                }
                set
                {
                    if(_Title != value)
                    {
                        _Title = value;
                        RaisePropertyChanged(nameof(Title));
                    }
                }
            }

            private string _Value = "NA";

            public string Value
            {
                get
                {
                    return _Value;
                }
                set
                {
                    if(_Value != value)
                    {
                        _Value = value;
                        RaisePropertyChanged(nameof(Value));
                    }
                }
            }

            private SeriesCollection _PieInfo = new SeriesCollection();

            public SeriesCollection PieInfo
            {
                get
                {
                    return _PieInfo;
                }
                set
                {
                    if(_PieInfo != value)
                    {
                        _PieInfo = value;
                        RaisePropertyChanged(nameof(PieInfo));
                    }
                }
            }
        }

        public class InfoBlockModel : ViewModelBase
        {
            public string Key
            {
                get; set;
            }

            public Grouping Group
            {
                get; set;
            }

            private string _Title = string.Empty;

            public string Title
            {
                get
                {
                    return _Title;
                }
                set
                {
                    if(_Title != value)
                    {
                        _Title = value;
                        RaisePropertyChanged(nameof(Title));
                    }
                }
            }

            private string _Value = "NA";

            public string Value
            {
                get
                {
                    return _Value;
                }
                set
                {
                    if(_Value != value)
                    {
                        _Value = value;
                        RaisePropertyChanged(nameof(Value));
                    }
                }
            }
        }
    }
}