using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using Utilities;

namespace PentagonHMI.Views
{
    /// <summary>
    /// Interaction logic for UPHView.xaml.
    /// </summary>
    public partial class UPHView : UserControl
    {
        ChartValues<ObservableValue> observableValues;
        #region Constructor
        public UPHView()
        {
            try
            {
                InitializeComponent();
                initialize();
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion

        public void Update(string[] UPH)
        {
            if (UPH == null) return;
            int n = 0;
            foreach (string str in UPH)
                observableValues[n++].Value = Convert.ToDouble(str);
        }

        #region PrivateInitializeMethods
        private void initialize()
        {
            try
            {
                UPHYAxis.LabelFormatter = x => x.ToString("N0");

                observableValues = new ChartValues<ObservableValue>();
                for (int i = 0; i < 24; i++)
                {
                    try
                    {
                        observableValues.Add(new ObservableValue());
                    }
                    catch (Exception ex)
                    {
                        FileLogger.logError(ex.Message, ex.ToString());
                    }
                }

                UPHColumnSeries.Values = observableValues;
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion

        #region PrivateEventMethods
        private void uPHCartesianChart_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                var screenPoint = e.GetPosition(UPHCartesianChart);
                var point = UPHCartesianChart.ConvertToChartValues(screenPoint);
                UPHYAxisSection.Value = point.Y;
                var series = UPHCartesianChart.Series[0];
                var closestPoint = series.ClosestPointTo(point.X, AxisOrientation.X);
                UPHXAxisSection.Value = closestPoint.X;
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion
    }
}