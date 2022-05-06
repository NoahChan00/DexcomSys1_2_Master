using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PentagonHMI.Classes;
using System.Threading;
using System.Windows.Controls.DataVisualization.Charting;

namespace PentagonHMI.ChildControls
{
    /// <summary>
    /// Interaction logic for ucOEE.xaml
    /// </summary>
    public partial class ucOEE : UserControl, IDisposable
    {
        double PreAvailability;
        double PreEfficiencyRate;
        double PrePerformanceRate;
        double PreCalcOee;

        double PrevPass_1, PrevFail_1;
        double PrevPass_2, PrevFail_2;
        double PrevPass_3, PrevFail_3;
        double PrevPass_4, PrevFail_4;
        private VanillaDB.DataDBCall DBCall;
        string stationID;
        string errmsg;
        //List<KeyValuePair<string, double>> _pcValueList1 = new List<KeyValuePair<string, double>>();
        //List<KeyValuePair<string, double>> _pcValueList2 = new List<KeyValuePair<string, double>>();
        //List<KeyValuePair<string, double>> _pcValueList3 = new List<KeyValuePair<string, double>>();
        //List<KeyValuePair<string, double>> _pcValueList4 = new List<KeyValuePair<string, double>>();
        KeyValuePair<string, double> item2_1;
        KeyValuePair<string, double> item2_2;
        KeyValuePair<string, double> item3_1;
        KeyValuePair<string, double> item3_2;
        KeyValuePair<string, double> item4_1;
        KeyValuePair<string, double> item4_2;
        KeyValuePair<string, double> item1_1;
        KeyValuePair<string, double> item1_2;
        //DataGrid dg;
        //List<KeyValuePair<string, double>> _pcValueList = new List<KeyValuePair<string, double>>();
        //Double testing = 98.33;
        //Double testing2 = 117.00;
        //Double testing3 = 73.21;
        //Double testing4 = 81.00;

        Thread TH_OEE;
        Boolean BolOEE;


        private delegate void ChartUpdater(LogicClasses.OEE oee);

        #region Constructor
        public ucOEE(ref LogicClasses.Main MainConnection)
        {
            InitializeComponent();
            string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
            DBCall = new VanillaDB.DataDBCall(Connstr);
            stationID = MainConnection.StationID;
            OEE = new LogicClasses.OEE(ref MainConnection);
            OEE.OnUpdate += new LogicClasses.OEE.onUpdateHandler(OEE_OnUpdate);
            OEE.Initialization();
            //ShowPieChart1(testing, ((Double)100 - testing));
            //ShowPieChart2(testing2, ((Double)100 - testing2));
            //ShowPieChart3(testing3, ((Double)100 - testing3));
            //ShowPieChart4(testing4, ((Double)100 - testing4));
            TH_OEE = new Thread(DisplayChart);
            BolOEE = true;
            TH_OEE.Start();
            //this.dg = dg;
        }

        #endregion

        #region Properties
        //private LogicClasses.OEE _OEE;
        //private LogicClasses.OEE OEE
        //{
        //    get { return _OEE; }
        //    set { _OEE = value; }
        //}
        private LogicClasses.OEE OEE { get; set; }
        #endregion

        #region Event
        void OEE_OnUpdate()
        {
            try
            {

                if (OEE == null)
                    return;

                //Operation Time
                if (OEE.OperationTime.TotalHours > 24)
                {
                    this.ucOperationTime.Dispatcher.Invoke(new Action(() => this.ucOperationTime.ucText = string.Format("{0} Day(s) {1}:{2}:{3}", OEE.OperationTime.Days.ToString("00"), OEE.OperationTime.Hours.ToString("00"), OEE.OperationTime.Minutes.ToString("00"), OEE.OperationTime.Seconds.ToString("00"))));
                }
                else
                {
                    this.ucOperationTime.Dispatcher.Invoke(new Action(() => this.ucOperationTime.ucText = string.Format("{0}:{1}:{2}", OEE.OperationTime.Hours.ToString("00"), OEE.OperationTime.Minutes.ToString("00"), OEE.OperationTime.Seconds.ToString("00"))));
                }

                //Idling Time
                if (OEE.IdlingTime.TotalHours > 24)
                {
                    this.ucIdlingTime.Dispatcher.Invoke(new Action(() => this.ucIdlingTime.ucText = string.Format("{0} Day(s) {1}:{2}:{3}", OEE.IdlingTime.Days.ToString("00"), OEE.IdlingTime.Hours.ToString("00"), OEE.IdlingTime.Minutes.ToString("00"), OEE.IdlingTime.Seconds.ToString("00"))));
                }
                else
                {
                    this.ucIdlingTime.Dispatcher.Invoke(new Action(() => this.ucIdlingTime.ucText = string.Format("{0}:{1}:{2}", OEE.IdlingTime.Hours.ToString("00"), OEE.IdlingTime.Minutes.ToString("00"), OEE.IdlingTime.Seconds.ToString("00"))));
                }

                //SystemUpTime
                if (OEE.SystemUpTime.TotalHours > 24)
                {
                    this.ucSystemUpTime.Dispatcher.Invoke(new Action(() => this.ucSystemUpTime.ucText = string.Format("{0} Day(s) {1}:{2}:{3}", OEE.SystemUpTime.Days.ToString("00"), OEE.SystemUpTime.Hours.ToString("00"), OEE.SystemUpTime.Minutes.ToString("00"), OEE.SystemUpTime.Seconds.ToString("00"))));
                }
                else
                {
                    this.ucSystemUpTime.Dispatcher.Invoke(new Action(() => this.ucSystemUpTime.ucText = string.Format("{0}:{1}:{2}", OEE.SystemUpTime.Hours.ToString("00"), OEE.SystemUpTime.Minutes.ToString("00"), OEE.SystemUpTime.Seconds.ToString("00"))));
                }

                //DownTime
                if (OEE.DownTime.TotalHours > 24)
                {
                    this.ucDownTime.Dispatcher.Invoke(new Action(() => this.ucDownTime.ucText = string.Format("{0} Day(s) {1}:{2}:{3}", OEE.DownTime.Days.ToString("00"), OEE.DownTime.Hours.ToString("00"), OEE.DownTime.Minutes.ToString("00"), OEE.DownTime.Seconds.ToString("00"))));
                }
                else
                {
                    this.ucDownTime.Dispatcher.Invoke(new Action(() => this.ucDownTime.ucText = string.Format("{0}:{1}:{2}", OEE.DownTime.Hours.ToString("00"), OEE.DownTime.Minutes.ToString("00"), OEE.DownTime.Seconds.ToString("00"))));
                }

                //SoftJam
                this.ucSoftjam.Dispatcher.Invoke(new Action(() => this.ucSoftjam.ucText = OEE.SoftJam.ToString()));

                //HardJam
                this.ucHardjam.Dispatcher.Invoke(new Action(() => this.ucHardjam.ucText = OEE.HardJam.ToString()));

                ////MTBF
                if (OEE.MTBF.TotalHours > 24)
                {
                    this.ucMTBF.Dispatcher.Invoke(new Action(() => this.ucMTBF.ucText = string.Format("{0} Day(s) {1}:{2}:{3}", OEE.MTBF.Days.ToString("00"), OEE.MTBF.Hours.ToString("00"), OEE.MTBF.Minutes.ToString("00"), OEE.MTBF.Seconds.ToString("00"))));
                }
                else
                {
                    this.ucMTBF.Dispatcher.Invoke(new Action(() => this.ucMTBF.ucText = string.Format("{0}:{1}:{2}", OEE.MTBF.Hours.ToString("00"), OEE.MTBF.Minutes.ToString("00"), OEE.MTBF.Seconds.ToString("00"))));
                }

                //MTBA
                if (OEE.MTBA.TotalHours > 24)
                {
                    this.ucMTBA.Dispatcher.Invoke(new Action(() => this.ucMTBA.ucText = string.Format("{0} Day(s) {1}:{2}:{3}", OEE.MTBA.Days.ToString("00"), OEE.MTBA.Hours.ToString("00"), OEE.MTBA.Minutes.ToString("00"), OEE.MTBA.Seconds.ToString("00"))));
                }
                else
                {
                    this.ucMTBA.Dispatcher.Invoke(new Action(() => this.ucMTBA.ucText = string.Format("{0}:{1}:{2}", OEE.MTBA.Hours.ToString("00"), OEE.MTBA.Minutes.ToString("00"), OEE.MTBA.Seconds.ToString("00"))));
                }

                /*
                 * Date: 2013-02-02
                 * Author: Jimmy
                 * Description: Get Start date time.
                 */
                //StartDateTime
                this.ucStartDateTime.Dispatcher.Invoke(new Action(() => this.ucStartDateTime.ucText = OEE.StartDateTime.ToString())); //("yyyy-MM-dd HH:mm:ss")

                //PerformanceRate
                /*
                * BK 20130218 Add for Show PieChart
                */
                this.ucPerformance.Dispatcher.Invoke(new Action(() => this.ucPerformance.ucText = OEE.PerformanceRate.ToString("0.00")));

                this.ucAvailability2.Dispatcher.Invoke(new Action(() => this.ucAvailability2.ucText = OEE.Availability.ToString("0.00")));

                //UPH
                //this.ucUPH.Dispatcher.Invoke(new Action(() => this.ucUPH.ucText = OEE.UPH.ToString("0")));

                //Total Failed
                this.ucTotalFail.Dispatcher.Invoke(new Action(() => this.ucTotalFail.ucText = OEE.TotalFail.ToString("0")));

                //Total Passed
                this.ucTotalPass.Dispatcher.Invoke(new Action(() => this.ucTotalPass.ucText = OEE.TotalPass.ToString("0")));

                ////OEE
                //this.ucLblOEE.Dispatcher.Invoke(new Action(() => this.ucLblOEE.ucText = OEE.calcOEE.ToString("00.00")));
                /*
                 * BK 20130218 Add for Show PieChart
                 */
                this.ucOEEText.Dispatcher.Invoke(new Action(() => this.ucOEEText.ucText = OEE.calcOEE.ToString("0.00")));

                //Efficiency Rate
                //this.ucEfficiencyRate.Dispatcher.Invoke(new Action(() => this.ucEfficiencyRate.ucText = OEE.EfficiencyRate.ToString("0.00")));
                /*
                 * BK 20130218 Add for Show PieChart
                 */
                this.ucYield.Dispatcher.Invoke(new Action(() => this.ucYield.ucText = OEE.EfficiencyRate.ToString("0.00")));

                this.ucNoMaterial.Dispatcher.Invoke(new Action(() => this.ucNoMaterial.ucText = string.Format("{0}:{1}:{2}", OEE.NoMaterial.Hours.ToString("00"), OEE.NoMaterial.Minutes.ToString("00"), OEE.NoMaterial.Seconds.ToString("00"))));
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "OEE_OnUpdate");
                //MessageBox.Show(ex.Message);
            }
        }

        /*
         * Date: 2013-02-01
         * Author: Tammie
         * Description: Write Log for OEE when button is clicked.
         */
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info - OEE", "Button Save Click");


            Utilities.FileLogger.logOEE(OEE.StartDateTime + "," + OEE.SystemUpTime + "," + OEE.DownTime + "," + OEE.IdlingTime + "," + OEE.OperationTime + "," + OEE.NoMaterial + "," + OEE.SoftJam + "," + OEE.HardJam + ","
                   + OEE.MTBA + "," + OEE.MTBF + "," + OEE.TotalPass + "," + OEE.TotalFail + "," + OEE.Availability + "," + OEE.EfficiencyRate + "," + OEE.PerformanceRate + "," + OEE.calcOEE, "NotMain");

            OEE.OPCWrite(Utilities.TagName.HMI.ResetOEE, "True");

            ////Need to take note. By SA:11Jul2018
            ////DBCall.OEE_Reset_Update(stationID, "1", ref errmsg);
        }
        #endregion

        #region Methods
        /*
         * BK Added for Show 4 PieChart
         */
        private void DisplayChart()
        {
            while (BolOEE == true)
            {
                if (!this.Dispatcher.CheckAccess())
                {
                    //      this.Dispatcher.Invoke(new ChartUpdater(this.UpdateChart), new object[] { OEE });
                    //this.Dispatcher.Invoke(new Action(() => this.UpdateChart(OEE)));
                    this.Dispatcher.Invoke(new Action(() => this.UpdateChart()));
                }
                else
                {
                    //UpdateChart(OEE);
                    UpdateChart();
                }

                Thread.Sleep(1000);
            }
        }

        private void UpdateChart()//LogicClasses.OEE oee)
        {

            if (OEE.Availability != PreAvailability)
            {
                if (OEE.Availability > 100)
                {
                    this.ShowPieChart1(100, 0.00);
                }
                else
                {
                    this.ShowPieChart1(OEE.Availability, ((Double)100.00 - OEE.Availability));
                }
                PreAvailability = OEE.Availability;
                Thread.Sleep(200);

            }


            if (OEE.EfficiencyRate != PreEfficiencyRate)
            {
                if (OEE.EfficiencyRate > 100)
                {
                    this.ShowPieChart2(100, 0.00);
                }
                else
                {
                    this.ShowPieChart2(OEE.EfficiencyRate, ((Double)100.00 - OEE.EfficiencyRate));
                }
                PreEfficiencyRate = OEE.EfficiencyRate;
                Thread.Sleep(200);
            }


            if (OEE.PerformanceRate != PrePerformanceRate)
            {
                if (OEE.PerformanceRate > 100)
                {
                    this.ShowPieChart3(100, 0.00);
                }
                else
                {
                    this.ShowPieChart3(OEE.PerformanceRate, ((Double)100.00 - OEE.PerformanceRate));
                }
                PrePerformanceRate = OEE.PerformanceRate;
                Thread.Sleep(200);
            }

            if (OEE.calcOEE != PreCalcOee)
            {
                if (OEE.calcOEE > 100)
                {
                    this.ShowPieChart4(100, 0.00);
                }
                else
                {
                    this.ShowPieChart4(OEE.calcOEE, ((Double)100.00 - OEE.calcOEE));
                }
                PreCalcOee = OEE.calcOEE;
                Thread.Sleep(200);
            }

        }

        private void ShowPieChart1(double Pass, double Fail)
        {
            //if (this._pcValueList1 != null)
            //{
            //    this._pcValueList1 = null;
            //}

            //((PieSeries)this.pieChart.Series[0]).ItemsSource = null;
            //GC.Collect();

            //    //this.pieChart.UpdateLayout();
            //    GC.Collect();
            //}
            if (Pass != PrevPass_1 || Fail != PrevFail_1)
            {
                //**// ((PieSeries)this.pieChart.Series[0]).ItemsSource = null;
                //**// GC.Collect();
                //Pass = Convert.ToDouble(Pass.ToString("00.00"));
                //Fail = Convert.ToDouble(Fail.ToString("00.00"));

                //_pcValueList.Clear();
                List<KeyValuePair<string, double>> _pcValueList1 = new List<KeyValuePair<string, double>>();

                _pcValueList1.Add(new KeyValuePair<string, double>("Available", Convert.ToDouble(Pass.ToString("00.00"))));
                _pcValueList1.Add(new KeyValuePair<string, double>("Unavailable", Convert.ToDouble(Fail.ToString("00.00"))));
                ///**// item1_1 = new KeyValuePair<string, double>("Available", Convert.ToDouble(Pass.ToString("00.00")));

                //      KeyValuePair<string, double> item = new KeyValuePair<string, double>("Available", Convert.ToDouble(Pass.ToString("00.00")));
                ///**// var idx_Available = _pcValueList1.FindIndex(n => n.Key.Equals(item1_1.Key));
                ///**// if (idx_Available != -1) { _pcValueList1[idx_Available] = item1_1; }
                ///**// else { _pcValueList1.Add(item1_1); }

                ///**// item1_2 = new KeyValuePair<string, double>("Unavailable", Convert.ToDouble(Fail.ToString("00.00")));
                ///**// var idx_Unavailable = _pcValueList1.FindIndex(n => n.Key.Equals(item1_2.Key));
                ///**// if (idx_Unavailable != -1) { _pcValueList1[idx_Unavailable] = item1_2; }
                ///**// else { _pcValueList1.Add(item1_2); }

                ((PieSeries)this.pieChart.Series[0]).ItemsSource = _pcValueList1;
                this.pieChart.DataContext = _pcValueList1;
                this.pieChart.UpdateLayout();

                PrevPass_1 = Pass;
                PrevFail_1 = Fail;

                _pcValueList1 = null;

                //**// GC.Collect();
            }
            // _pcValueList.Clear();
        }

        private void ShowPieChart2(double Pass, double Fail)
        {
            //if (this._pcValueList != null)
            //{
            //    this._pcValueList = null;
            //    ((PieSeries)this.pieChart2.Series[0]).ItemsSource = null;
            //    //this.pieChart.UpdateLayout();
            //    GC.Collect();
            //}
            if (Pass != PrevPass_2 || Fail != PrevFail_2)
            {
                //**// ((PieSeries)this.pieChart2.Series[0]).ItemsSource = null;
                //**// GC.Collect();

                //_pcValueList.Clear();
                List<KeyValuePair<string, double>> _pcValueList2 = new List<KeyValuePair<string, double>>();
                _pcValueList2.Add(new KeyValuePair<string, double>("Efficient", Convert.ToDouble(Pass.ToString("00.00"))));
                _pcValueList2.Add(new KeyValuePair<string, double>("Inefficient", Convert.ToDouble(Fail.ToString("00.00"))));

                ///**// item2_1 = new KeyValuePair<string, double>("Efficient", Convert.ToDouble(Pass.ToString("00.00")));

                //      KeyValuePair<string, double> item = new KeyValuePair<string, double>("Available", Convert.ToDouble(Pass.ToString("00.00")));
                ///**// var idx_Available = _pcValueList2.FindIndex(n => n.Key.Equals(item2_1.Key));
                ///**// if (idx_Available != -1) { _pcValueList2[idx_Available] = item2_1; }
                ///**// else { _pcValueList2.Add(item2_1); }

                ///**// item2_2 = new KeyValuePair<string, double>("Inefficient", Convert.ToDouble(Fail.ToString("00.00")));
                ///**// var idx_Unavailable = _pcValueList2.FindIndex(n => n.Key.Equals(item2_2.Key));
                ///**// if (idx_Unavailable != -1) { _pcValueList2[idx_Unavailable] = item2_2; }
                ///**// else { _pcValueList2.Add(item2_2); }


                ((PieSeries)this.pieChart2.Series[0]).ItemsSource = _pcValueList2;
                this.pieChart2.DataContext = _pcValueList2;
                this.pieChart2.UpdateLayout();

                PrevPass_2 = Pass;
                PrevFail_2 = Fail;

                _pcValueList2 = null;
                //**// GC.Collect();
            }
            // _pcValueList.Clear();
        }
        private void ShowPieChart3(double Pass, double Fail)
        {
            //if (this._pcValueList != null)
            //{
            //    this._pcValueList = null;
            //    ((PieSeries)this.pieChart3.Series[0]).ItemsSource = null;
            //    //this.pieChart.UpdateLayout();
            //    GC.Collect();
            //}
            if (Pass != PrevPass_3 || Fail != PrevFail_3)
            {
                //**// ((PieSeries)this.pieChart3.Series[0]).ItemsSource = null;
                //**// GC.Collect();
                //Pass = Convert.ToDouble(Pass.ToString("00.00"));
                //Fail = Convert.ToDouble(Fail.ToString("00.00"));

                List<KeyValuePair<string, double>> _pcValueList3 = new List<KeyValuePair<string, double>>();
                ; _pcValueList3.Add(new KeyValuePair<string, double>("Performance Rate", Convert.ToDouble(Pass.ToString("00.00"))));
                _pcValueList3.Add(new KeyValuePair<string, double>("Not Perform Rate", Convert.ToDouble(Fail.ToString("00.00"))));
                ///**// item3_1 = new KeyValuePair<string, double>("Performance Rate", Convert.ToDouble(Pass.ToString("00.00")));

                //      KeyValuePair<string, double> item = new KeyValuePair<string, double>("Available", Convert.ToDouble(Pass.ToString("00.00")));
                ///**// var idx_Available = _pcValueList3.FindIndex(n => n.Key.Equals(item3_1.Key));
                ///**// if (idx_Available != -1) { _pcValueList3[idx_Available] = item3_1; }
                ///**// else { _pcValueList3.Add(item3_1); }

                ///**// item3_2 = new KeyValuePair<string, double>("Not Perform Rate", Convert.ToDouble(Fail.ToString("00.00")));
                ///**// var idx_Unavailable = _pcValueList3.FindIndex(n => n.Key.Equals(item3_2.Key));
                ///**// if (idx_Unavailable != -1) { _pcValueList3[idx_Unavailable] = item3_2; }
                ///**// else { _pcValueList3.Add(item3_2); }


                ((PieSeries)this.pieChart3.Series[0]).ItemsSource = _pcValueList3;
                this.pieChart3.DataContext = _pcValueList3;
                this.pieChart3.UpdateLayout();

                PrevPass_3 = Pass;
                PrevFail_3 = Fail;

                _pcValueList3 = null;

                //**// GC.Collect();
            }
            // _pcValueList.Clear();
        }

        private void ShowPieChart4(double Pass, double Fail)
        {
            //if (this._pcValueList != null)
            //{
            //    this._pcValueList = null;
            //    ((PieSeries)this.pieChart4.Series[0]).ItemsSource = null;
            //    //this.pieChart.UpdateLayout();
            //    GC.Collect();
            //}
            if (Pass != PrevPass_4 || Fail != PrevFail_4)
            {

                //**// ((PieSeries)this.pieChart4.Series[0]).ItemsSource = null;
                //**// GC.Collect();
                //Pass = Convert.ToDouble(Pass.ToString("00.00"));
                //Fail = Convert.ToDouble(Fail.ToString("00.00"));
                //List<KeyValuePair<string, double>> _pcValueList = new List<KeyValuePair<string, double>>();
                //_pcValueList.Clear();
                List<KeyValuePair<string, double>> _pcValueList4 = new List<KeyValuePair<string, double>>();
                _pcValueList4.Add(new KeyValuePair<string, double>("OEE Rate", Convert.ToDouble(Pass.ToString("00.00"))));
                _pcValueList4.Add(new KeyValuePair<string, double>("OEE Unrate", Convert.ToDouble(Fail.ToString("00.00"))));
                ///**// item4_1 = new KeyValuePair<string, double>("OEE Rate", Convert.ToDouble(Pass.ToString("00.00")));
                //      KeyValuePair<string, double> item = new KeyValuePair<string, double>("Available", Convert.ToDouble(Pass.ToString("00.00")));
                ///**// var idx_Available = _pcValueList4.FindIndex(n => n.Key.Equals(item4_1.Key));
                ///**// if (idx_Available != -1) { _pcValueList4[idx_Available] = item4_1; }
                ///**// else {v _pcValueList4.Add(item4_1); }

                ///**// item4_2 = new KeyValuePair<string, double>("OEE Unrate", Convert.ToDouble(Fail.ToString("00.00")));
                ///**// var idx_Unavailable = _pcValueList4.FindIndex(n => n.Key.Equals(item4_2.Key));
                ///**// if (idx_Unavailable != -1) { _pcValueList4[idx_Unavailable] = item4_2; }
                ///**// else { _pcValueList4.Add(item4_2); }


                ((PieSeries)this.pieChart4.Series[0]).ItemsSource = _pcValueList4;
                this.pieChart4.DataContext = _pcValueList4;
                this.pieChart4.UpdateLayout();
                PrevPass_4 = Pass;
                PrevFail_4 = Fail;

                _pcValueList4 = null;

                //**// GC.Collect();
            }
            // _pcValueList.Clear();
        }

        public void Dispose()
        {
            OEE.Dispose();
            //BolOEE = false;
            //if (OEE != null)
            //{
            //    OEE.OnUpdate -= new  LogicClasses.OEE.onUpdateHandler(OEE_OnUpdate);
            //    OEE.Dispose();
            //   // Classes.GlobalFunctions.Wait(200);
            //    OEE = null;
            //}
        }

        public void UpdateUI()
        {
        }
        #endregion

        #region Destructor
        ~ucOEE()
        {
            Dispose();
        }
        #endregion






    }
}
