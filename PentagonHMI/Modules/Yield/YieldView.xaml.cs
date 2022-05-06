using Logix;
using System;
using System.Windows.Controls;
using Utilities;
using LiveCharts;
using LiveCharts.Wpf;
using System.Windows.Media;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for YieldView.xaml.
    /// </summary>
    public partial class YieldView : UserControl
    {
        #region PrivateFields
        private LogicClasses.Main main = null;
        private readonly Tag zone1TotalPassTag = new Tag { Name = "OEE_Tags.dint_Z1_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
        private readonly Tag zone1TotalFailTag = new Tag { Name = "OEE_Tags.dint_Z1_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
        private readonly Tag zone2TotalPassTag = new Tag { Name = "OEE_Tags.dint_Z2_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
        private readonly Tag zone2TotalFailTag = new Tag { Name = "OEE_Tags.dint_Z2_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
        SeriesCollection Series;
        public Func<ChartPoint, string> PointLabel { get; set; }
        #endregion

        #region Constructor
        public YieldView(LogicClasses.Main _main)
        {
            try
            {
                main = _main;
                InitializeComponent();
                if (ProjectType.HDD == Classes.GlobalFunctions.ProjectType)
                {
                    YieldUserControl.Visibility = System.Windows.Visibility.Collapsed;
                    PieYield.Visibility = System.Windows.Visibility.Visible;
                    PointLabel = chartPoint => string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);
                    Series = new SeriesCollection
                    {
                      new PieSeries { Title = "Quality", Fill = Brushes.MediumSeaGreen, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true, LabelPoint = PointLabel },
                      new PieSeries { Title = "", Fill = Brushes.Orange, Values = new ChartValues<double>(new double[] { 0 }), DataLabels = true, LabelPoint = PointLabel }
                    };
                    PieYield.Series = Series;
                }
                else if (Classes.GlobalFunctions.ProjectType == ProjectType.ARCADIA && Classes.GlobalFunctions.StationType == StationType.VISION)
                {
                    if (main.MachineName.ToUpper().Contains("FINAL"))
                    {
                        zone1TotalPassTag = new Tag { Name = "OutputPnP_OEE_Tags.dint_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
                        zone1TotalFailTag = new Tag { Name = "OutputPnP_OEE_Tags.dint_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
                    }
                    else if (main.MachineName.ToUpper().Contains("CENTRAL"))
                    {
                        zone1TotalPassTag = new Tag { Name = "CenVis_OEE_Tags.dint_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
                        zone1TotalFailTag = new Tag { Name = "CenVis_OEE_Tags.dint_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
                    }
                }
                else if (Classes.GlobalFunctions.ProjectType == ProjectType.TLA)
                {
                    if (Classes.GlobalFunctions.StationType == StationType.ARCADIA_Main)
                    {
                        zone1TotalPassTag = new Tag { Name = "Labelling_OEE_Tags.dint_Z1_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
                        zone1TotalFailTag = new Tag { Name = "Labelling_OEE_Tags.dint_Z1_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
                        zone2TotalPassTag = new Tag { Name = "Labelling_OEE_Tags.dint_Z2_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
                        zone2TotalFailTag = new Tag { Name = "Labelling_OEE_Tags.dint_Z2_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
                    }
                    else if (main.MachineName.ToUpper().Contains("FINAL"))
                    {
                        zone1TotalPassTag = new Tag { Name = "OutputPnP_OEE_Tags.dint_Z1_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
                        zone1TotalFailTag = new Tag { Name = "OutputPnP_OEE_Tags.dint_Z1_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
                        zone2TotalPassTag = new Tag { Name = "OutputPnP_OEE_Tags.dint_Z2_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
                        zone2TotalFailTag = new Tag { Name = "OutputPnP_OEE_Tags.dint_Z2_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
                    }
                    else if (main.MachineName.ToUpper().Contains("CENTRAL"))
                    {
                        zone1TotalPassTag = new Tag { Name = "CenVis_OEE_Tags.dint_Z1_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
                        zone1TotalFailTag = new Tag { Name = "CenVis_OEE_Tags.dint_Z1_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
                        zone2TotalPassTag = new Tag { Name = "CenVis_OEE_Tags.dint_Z2_Total_Pass", DataType = Logix.Tag.ATOMIC.DINT };
                        zone2TotalFailTag = new Tag { Name = "CenVis_OEE_Tags.dint_Z2_Total_Fail", DataType = Logix.Tag.ATOMIC.DINT };
                    }
                }

                initializeOpc(_main);
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private void initializeOpc(LogicClasses.Main _main)
        {
            try
            {
                main = _main;
                main.Home_OnUpdate += main_Home_OnUpdate;
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
                if (ProjectType.HDD == Classes.GlobalFunctions.ProjectType)
                {
                    double Pass = main.OPC.Read<double>("OEE_Tags.dint_Total_Pass");
                    double Fail = main.OPC.Read<double>("OEE_Tags.dint_Total_Fail");
                    double percent = Pass / (Pass + Fail) * 100;

                    Dispatcher.Invoke(() =>
                    {
                        Series[0].Values[0] = Math.Round(percent, 2);
                        Series[1].Values[0] = Math.Round(100 - percent, 2);
                    });
                }
                else
                {
                    if (main.zoneCount == 1)
                    {
                        main.MyPLC.ReadTag(zone1TotalPassTag);
                        main.MyPLC.ReadTag(zone1TotalFailTag);

                        if (zone1TotalPassTag.Value != null && zone1TotalFailTag.Value != null)
                        {
                            YieldUserControl.PassFail = $"{zone1TotalPassTag.Value},{zone1TotalFailTag.Value},-,-";
                        }
                    }
                    else
                    {
                        main.MyPLC.ReadTag(zone1TotalPassTag);
                        main.MyPLC.ReadTag(zone1TotalFailTag);
                        main.MyPLC.ReadTag(zone2TotalPassTag);
                        main.MyPLC.ReadTag(zone2TotalFailTag);

                        if (zone1TotalPassTag.Value != null && zone1TotalFailTag.Value != null &&
                        zone2TotalPassTag.Value != null && zone2TotalFailTag.Value != null)
                        {
                            YieldUserControl.PassFail = $"{zone1TotalPassTag.Value},{zone1TotalFailTag.Value}," +
                            $"{zone2TotalPassTag.Value},{zone2TotalFailTag.Value}";
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion
    }
}