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
using System.Windows.Navigation;
using System.Windows.Shapes;
using log4net;
using System.Data;
using System.Collections;
using PentagonHMI.Classes;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Controls.DataVisualization.Charting;

namespace PentagonHMI.ChildControls
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class ucHome : UserControl, IDisposable
    {
        #region Variable

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
           (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //List<KeyValuePair<string, long>> _pcValueList;
       // List<KeyValuePair<string, long>> PrevValueList;
        long PrevPass = -1;
        long PrevFail = -1;
        Thread TH_UPH;
        Boolean BolUPH;
        DataGrid dg;
        #endregion

        #region Constructor
        public ucHome(ref LogicClasses.Main MainConnection, ref LogicClasses.Home _pHome)
        {
            try
            {
                InitializeComponent(); 

                if (_pHome == null)
                {
                    _pHome = new LogicClasses.Home(ref MainConnection);
                }
                this._Home = _pHome;
                Home.OnUpdate += new LogicClasses.Home.onUpdateHandler(Home_OnUpdate);
                Initialize();
               
                Home_OnUpdate();
             //   this.dg = dg;

                //TH_UPH = new Thread(DisplayChart);
                BolUPH = true;
                //TH_UPH.Start();

                //showColumnChart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

             
        #endregion

        #region Properties
        

        LogicClasses.Home _Home;
        LogicClasses.Home Home
        {
            get { return _Home; }
            set { _Home = value; }
        }
        #endregion

        #region FormEvents
        private void btnClearLoaderMagazine_Click(object sender, RoutedEventArgs e)
        {
            Home.OPCWrite(Utilities.TagName.GlobalVariable.LoaderStopFeeding, "True");
        }

        private void btnClearUnLoaderMagazine_Click(object sender, RoutedEventArgs e)
        {
            Home.OPCWrite(Utilities.TagName.GlobalVariable.UnloaderStopFeeding, "True");
        }

        //private void btnStartRecipe_Click(object sender, RoutedEventArgs e)
        //{
        //    this.btnStartRecipe.Dispatcher.Invoke(new Action(() => this.btnStartRecipe.IsEnabled = false));

        //    if (recipeList.Text.Equals("Laser Cut"))
        //    {
        //        Home.OPCWrite(Utilities.TagName.GlobalVariable.Machine_Recipe, "1");
        //    }
        //    else if (recipeList.Text.Equals("Sweeping"))
        //    {
        //        Home.OPCWrite(Utilities.TagName.GlobalVariable.Machine_Recipe, "2");
        //    }

        //    Home.OPCWrite(Utilities.TagName.GlobalVariable.Set_Recipe, "True");
        //}

        private void btnStopRecipe_Click(object sender, RoutedEventArgs e)
        {
            Home.OPCWrite(Utilities.TagName.GlobalVariable.Stop_Recipe, "True");
        }

        //private void btnSetMode_Click(object sender, RoutedEventArgs e)
        //{
        //    if (btnSetMode.Content.Equals("One Cycle Mode ON"))
        //    {
        //        Home.OPCWrite(Utilities.TagName.GlobalVariable.One_Cycle_Mode, "True");
        //    }
        //    else
        //    {
        //        Home.OPCWrite(Utilities.TagName.GlobalVariable.One_Cycle_Mode, "False");
        //    }

        //}
        #endregion

        #region Events
      

        void Home_OnUpdate()
        {
            //this.StationCode11.Dispatcher.Invoke(new Action(() => this.StationCode11.Text = Home.StationCode11.ToString()));
            this.ucUPH.Dispatcher.Invoke(new Action(() => this.ucUPH.ucText = Home.UPH.ToString()));

            //this.txtTotalPass.Dispatcher.Invoke(new Action(() => this.txtTotalPass.Text = Home.TotalPass.ToString()));
            //this.txtTotalFail.Dispatcher.Invoke(new Action(() => this.txtTotalFail.Text = Home.TotalFailed.ToString()));
            //this.txtTotalCount.Dispatcher.Invoke(new Action(() => this.txtTotalCount.Text = Home.TotalCount.ToString()));

            this.ucYield.Dispatcher.Invoke(new Action(() => this.ucYield.ucText = Home.Yield.ToString("00.00")));

            //if (Home.Machine_Recipe == 0)
            //{
            //    this.recipeList.Dispatcher.Invoke(new Action(() => this.recipeList.Text = ""));
            //}
            //else if (Home.Machine_Recipe == 1)
            //{
            //    this.recipeList.Dispatcher.Invoke(new Action(() => this.recipeList.Text = "Laser Cut"));
            //}

            //else if (Home.Machine_Recipe == 2)
            //{
            //    this.recipeList.Dispatcher.Invoke(new Action(() => this.recipeList.Text = "Sweeping"));
            //}


            //if (Home.setRecipe == true)
            //{
            //    this.btnStartRecipe.Dispatcher.Invoke(new Action(() => this.btnStartRecipe.IsEnabled = false));
            //}
            //else
            //{
            //    this.btnStartRecipe.Dispatcher.Invoke(new Action(() => this.btnStartRecipe.IsEnabled = true));
            //}

            //if (Home.OneCycleMode == true)
            //{
            //    this.btnSetMode.Dispatcher.Invoke(new Action(() => this.btnSetMode.Content = "One Cycle Mode OFF"));
            //}
            //else
            //{
            //    this.btnSetMode.Dispatcher.Invoke(new Action(() => this.btnSetMode.Content = "One Cycle Mode ON"));
            //}


        }
        #endregion

        #region Methods
        private void Initialize()
        {
        }
                
        private void showColumnChart()
        {
            try
            {
                List<KeyValuePair<string, long>> valueList = new List<KeyValuePair<string, long>>();
                valueList.Add(new KeyValuePair<string, long>("8", 1000));
                valueList.Add(new KeyValuePair<string, long>("9", 2000));
                valueList.Add(new KeyValuePair<string, long>("10", 500));
                valueList.Add(new KeyValuePair<string, long>("11", 1500));
                valueList.Add(new KeyValuePair<string, long>("12", 2200));
                valueList.Add(new KeyValuePair<string, long>("10", 0));
                valueList.Add(new KeyValuePair<string, long>("11", 1100));
                valueList.Add(new KeyValuePair<string, long>("12", 2200));
                ////Setting data for column chart
                //columnChart.DataContext = valueList;
                //((AreaSeries)areaChart.Series[0]).ItemsSource = valueList;

                List<KeyValuePair<string, int>> valueList1 = new List<KeyValuePair<string, int>>();
                valueList1.Add(new KeyValuePair<string, int>("PASS", 10));
                valueList1.Add(new KeyValuePair<string, int>("FAIL", 90));

                //Setting data for line chart
                pieChart.DataContext = valueList1;
                //areaChart.DataContext = valueList;
            }
            catch
            {
                // WriteError(ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name, "ERROR","Main");
            }
        }

        private void DisplayChart()
        {
            while (BolUPH == true)
            {
                this.Dispatcher.Invoke(new Action(() => this.ShowAreaChart(Home.UPHItems)));
                this.Dispatcher.Invoke(new Action(() => this.ShowPieChart(Home.TotalPass, Home.TotalFailed)));
                Thread.Sleep(200);
            }
        }

        private void ShowAreaChart(List<KeyValuePair<string, long>> acValueList)
        {
            try
            {
                if (acValueList != null)
                {

                    lineChart.ItemsSource = null;
                    GC.Collect();
                    List<KeyValuePair<string, long>> _acValueList = new List<KeyValuePair<string, long>>();

                    foreach (KeyValuePair<string, long> item in acValueList)
                    {
                        //_acValueList.Add(new KeyValuePair<string, long>(Convert.ToInt16(item.Key), Convert.ToInt64(item.Value)));
                        _acValueList.Add(item);
                    }
                    lineChart.ItemsSource = _acValueList;

                    ////if (this._acValueList != null)
                    ////{
                    ////    ((AreaSeries)areaChart.Series[0]).ItemsSource = null;
                    ////    this.areaChart.DataContext = null;    
                    ////    this._acValueList = null;
                    ////}
                    ////if (PrevValueList == acValueList)
                    ////{ return; }

                    ////this._acValueList = new List<KeyValuePair<string, long>>();
                    ////_acValueList.Clear();
                    //List<KeyValuePair<string, long>> _acValueList = new List<KeyValuePair<string, long>>();
                    //foreach (KeyValuePair<string, long> item in acValueList)
                    //{
                    //    //this._acValueList.Add(item);
                    //    _acValueList.Add(item);
                    //}
                    ////GC.Collect();
                    ////acValueList.CopyTo(_acValueList);
                    ////_acValueList = acValueList;
                    ////((AreaSeries)areaChart.Series[0]).ItemsSource = _acValueList;
                    ////((AreaSeries)areaChart.Series[0]).ItemsSource = Home.UPHItems;
                    ////this.areaChart.DataContext = _acValueList;

                    ////((AreaSeries)areaChart.Series[0]).ItemsSource = this._acValueList;
                    //////((AreaSeries)areaChart.Series[0]).ItemsSource = Home.UPHItems;
                    ////this.areaChart.DataContext = this._acValueList;
                    ////areaChart.UpdateLayout();
                    ////GC.Collect();
                    ////PrevValueList = acValueList;
                }
            }
            catch (Exception ex)
            {
                log.Error("ShowAreaChart()", ex);
            }
        }

        private void ShowPieChart(long Pass, long Fail)
        {
            try
            {
               
                if (Pass != PrevPass || Fail != PrevFail)
                {
                    List<KeyValuePair<string, long>> _pcValueList = new List<KeyValuePair<string, long>>();
                    //_pcValueList.Clear();
                    //GC.Collect();
                    _pcValueList.Add(new KeyValuePair<string, long>("PASS", Pass));
                    _pcValueList.Add(new KeyValuePair<string, long>("FAIL", Fail));

                    ((PieSeries)this.pieChart.Series[0]).ItemsSource = _pcValueList;
                    this.pieChart.DataContext = _pcValueList;
                    
                    
                    this.pieChart.UpdateLayout();
                    PrevPass = Pass;
                    PrevFail = Fail;
                    GC.Collect();
                }
                // _pcValueList.Clear();
            }
            catch (Exception ex)
            {
                log.Error("ShowPieChart()", ex);
            }
        }

        public void Dispose()
        {
           

            if (this.Home != null)
            {
         

                this.Home.OnUpdate -= new  LogicClasses.Home.onUpdateHandler (Home_OnUpdate);
                //Home.Dispose();
                //Home = null;
            }
            BolUPH = false;
        }       
        #endregion

        #region Destructor
        ~ucHome()
        {
            Dispose();
        }
        #endregion        

        

       

    }
}
