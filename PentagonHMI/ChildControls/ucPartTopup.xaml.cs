using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

using Library;
using PentamasterHMI;
using System.Collections.ObjectModel;


namespace PentagonHMI.ChildControls
{
    /// <summary>
    /// Interaction logic for ucPartTopup.xaml
    /// </summary>
    public partial class ucPartTopup : UserControl, IDisposable
    {
        private VanillaDB.DataDBCall DBCall;

        LogicClasses.Main _MainConnection;
        
        public ucPartTopup(ref LogicClasses.Main MainConnection)
        {
            InitializeComponent();

            string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
            DBCall = new VanillaDB.DataDBCall(Connstr);

            _MainConnection = MainConnection;
            RobotCell = new LogicClasses.RobotCell(ref MainConnection);
            RobotCell.OnUpdate += new LogicClasses.RobotCell.onUpdateHandle(RobotCell_OnUpdate);


            this.PartItem_1.Visibility = Visibility.Collapsed;
            this.PartItem_2.Visibility = Visibility.Collapsed;
            this.PartItem_3.Visibility = Visibility.Collapsed;
            this.PartItem_4.Visibility = Visibility.Collapsed;
            this.PartItem_5.Visibility = Visibility.Collapsed;
            this.PartQty_1.Visibility = Visibility.Collapsed;
            this.PartQty_2.Visibility = Visibility.Collapsed;
            this.PartQty_3.Visibility = Visibility.Collapsed;
            this.PartQty_4.Visibility = Visibility.Collapsed;
            this.PartQty_5.Visibility = Visibility.Collapsed;
            this.btnTopup_Item1.Visibility = Visibility.Collapsed;
            this.btnTopup_Item2.Visibility = Visibility.Collapsed;
            this.btnTopup_Item3.Visibility = Visibility.Collapsed;
            this.btnTopup_Item4.Visibility = Visibility.Collapsed;
            this.btnTopup_Item5.Visibility = Visibility.Collapsed;
            _MainConnection.PageTopupPageOn = true;
        }
        #region Properties
        private LogicClasses.RobotCell RobotCell { get; set; }
        #endregion
        #region Event
        public void RobotCell_OnUpdate()
        {
            try
            {
                if (this.RobotCell != null)
                {
                    if (_MainConnection.PageTopupPageOn == false)
                    { return; }
                    int i = 1;

                    foreach (LogicClasses.StationPartInfo StationPart in _MainConnection.StationPartInfo)
                    {
                        if (i == 1)
                        {
                            this.PartItem_1.Dispatcher.Invoke(new Action(() => this.PartItem_1.Visibility = Visibility.Visible));
                            this.PartItem_1.Dispatcher.Invoke(new Action(() => this.PartItem_1.ucText = StationPart.PartNumber));
                            this.PartQty_1.Dispatcher.Invoke(new Action(() => this.PartQty_1.ucText = StationPart.StockQty.ToString()));
                            this.PartItem_1.Dispatcher.Invoke(new Action(() => this.PartItem_1.Visibility = Visibility.Visible));
                            this.PartQty_1.Dispatcher.Invoke(new Action(() => this.PartQty_1.Visibility = Visibility.Visible));
                            this.btnTopup_Item1.Dispatcher.Invoke(new Action(() => this.btnTopup_Item1.Visibility = Visibility.Visible));
                        }
                        else if (i == 2)
                        {
                            this.PartItem_2.Dispatcher.Invoke(new Action(() => this.PartItem_2.Visibility = Visibility.Visible));
                            this.PartItem_2.Dispatcher.Invoke(new Action(() => this.PartItem_2.ucText = StationPart.PartNumber));
                            this.PartQty_2.Dispatcher.Invoke(new Action(() => this.PartQty_2.ucText = StationPart.StockQty.ToString()));
                            this.PartItem_2.Dispatcher.Invoke(new Action(() => this.PartItem_2.Visibility = Visibility.Visible));
                            this.btnTopup_Item2.Dispatcher.Invoke(new Action(() => this.btnTopup_Item2.Visibility = Visibility.Visible));
                            this.PartQty_2.Dispatcher.Invoke(new Action(() => this.PartQty_2.Visibility = Visibility.Visible));
                        }
                        else if (i == 3)
                        {
                            this.PartItem_3.Dispatcher.Invoke(new Action(() => this.PartItem_3.Visibility = Visibility.Visible));
                            this.PartItem_3.Dispatcher.Invoke(new Action(() => this.PartItem_3.ucText = StationPart.PartNumber));
                            this.PartQty_3.Dispatcher.Invoke(new Action(() => this.PartQty_3.ucText = StationPart.StockQty.ToString()));
                            this.PartItem_3.Dispatcher.Invoke(new Action(() => this.PartItem_3.Visibility = Visibility.Visible));
                            this.btnTopup_Item3.Dispatcher.Invoke(new Action(() => this.btnTopup_Item3.Visibility = Visibility.Visible));
                            this.PartQty_3.Dispatcher.Invoke(new Action(() => this.PartQty_3.Visibility = Visibility.Visible));
                        }
                        else if (i == 4)
                        {
                            this.PartItem_4.Dispatcher.Invoke(new Action(() => this.PartItem_4.Visibility = Visibility.Visible));
                            this.PartItem_4.Dispatcher.Invoke(new Action(() => this.PartItem_4.ucText = StationPart.PartNumber));
                            this.PartQty_4.Dispatcher.Invoke(new Action(() => this.PartQty_4.ucText = StationPart.StockQty.ToString()));
                            this.PartItem_4.Dispatcher.Invoke(new Action(() => this.PartItem_4.Visibility = Visibility.Visible));
                            this.btnTopup_Item4.Dispatcher.Invoke(new Action(() => this.btnTopup_Item4.Visibility = Visibility.Visible));
                            this.PartQty_4.Dispatcher.Invoke(new Action(() => this.PartQty_4.Visibility = Visibility.Visible));
                        }
                        else if (i == 5)
                        {
                            this.PartItem_5.Dispatcher.Invoke(new Action(() => this.PartItem_5.Visibility = Visibility.Visible));
                            this.PartItem_5.Dispatcher.Invoke(new Action(() => this.PartItem_5.ucText = StationPart.PartNumber));
                            this.PartQty_5.Dispatcher.Invoke(new Action(() => this.PartQty_5.ucText = StationPart.StockQty.ToString()));
                            this.PartItem_5.Dispatcher.Invoke(new Action(() => this.PartItem_5.Visibility = Visibility.Visible));
                            this.btnTopup_Item5.Dispatcher.Invoke(new Action(() => this.btnTopup_Item5.Visibility = Visibility.Visible));
                            this.PartQty_5.Dispatcher.Invoke(new Action(() => this.PartQty_5.Visibility = Visibility.Visible));
                        }
                        i = i + 1;
                    }



                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "RobotCell On Update");
            }

        }

        public void Dispose()
        {

            _MainConnection.PageTopupPageOn = false;
            if (this.RobotCell != null)
            {


                RobotCell.OnUpdate -= new LogicClasses.RobotCell.onUpdateHandle(RobotCell_OnUpdate);
                //Home.Dispose();
                //Home = null;
            }
        
        }
        #endregion

        #region Destructor
        ~ucPartTopup()
        {
            Dispose();
        }
        #endregion

        private void btnTopup_Item1_Click(object sender, RoutedEventArgs e)
        {
            this.TopupScreen.Visibility = Visibility.Visible;
            this.PartItem_TopUp.Text = this.PartItem_1.ucText;
            this.PartQty_Topup.Text = this.PartQty_1.ucText;
            this.ucTopupQty.ucLabelErrorContent = "";
            this.ucTopupQty.Text = "";

            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Part item 1 - Top Up Click");

        }
        private void btnTopup_Item2_Click(object sender, RoutedEventArgs e)
        {
            this.TopupScreen.Visibility = Visibility.Visible;
            this.PartItem_TopUp.Text = this.PartItem_2.ucText;
            this.PartQty_Topup.Text = this.PartQty_2.ucText;
            this.ucTopupQty.ucLabelErrorContent = "";
            this.ucTopupQty.Text = "";

            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Part item 2 - Top Up Click");

        }
        private void btnTopup_Item3_Click(object sender, RoutedEventArgs e)
        {
            this.TopupScreen.Visibility = Visibility.Visible;
            this.PartItem_TopUp.Text = this.PartItem_3.ucText;
            this.PartQty_Topup.Text = this.PartQty_3.ucText;
            this.ucTopupQty.ucLabelErrorContent = "";
            this.ucTopupQty.Text = "";

            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Part item 3 - Top Up Click");

        }
        private void btnTopup_Item4_Click(object sender, RoutedEventArgs e)
        {
            this.TopupScreen.Visibility = Visibility.Visible;
            this.PartItem_TopUp.Text = this.PartItem_4.ucText;
            this.PartQty_Topup.Text = this.PartQty_4.ucText;
            this.ucTopupQty.ucLabelErrorContent = "";
            this.ucTopupQty.Text = "";

            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Part item 4 - Top Up Click");

        }
        private void btnTopup_Item5_Click(object sender, RoutedEventArgs e)
        {
            this.TopupScreen.Visibility = Visibility.Visible;
            this.PartItem_TopUp.Text = this.PartItem_5.ucText;
            this.PartQty_Topup.Text = this.PartQty_5.ucText;
            this.ucTopupQty.ucLabelErrorContent = "";
            this.ucTopupQty.Text = "";

            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Part item 5 - Top Up Click");

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.TopupScreen.Visibility = Visibility.Collapsed ;
            this.PartItem_TopUp.Text = "";
            this.PartQty_Topup.Text = "";
            this.ucTopupQty.ucLabelErrorContent = "";
            this.ucTopupQty.Text = "";

            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Cancel Click");

        }
        private void btnTopupQty_Click(object sender, RoutedEventArgs e)
        {
            //////int intTopqty;
            //////if (int.TryParse(this.ucTopupQty.Text, out intTopqty))
            //////{
            //////    if (intTopqty > 0)
            //////    {
            //////        if (DBCall.setUpdatePartQty(Properties.Settings.Default.Station, this.PartItem_TopUp.Text, intTopqty) == true)
            //////        {
            //////            this.TopupScreen.Visibility = Visibility.Collapsed;
            //////            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucTopupQty.Text + " :Fail: Top Up Success! ! ");

            //////        }
            //////        else { this.ucTopupQty.ucLabelErrorContent = "Top Up Fail!";
            //////            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucTopupQty.Text + " :Fail: Top Up Fail! ! ");
            //////        }
            //////    }
            //////    else { this.ucTopupQty.ucLabelErrorContent = "TOP UP Number Must Great Than 0 !";
            //////        Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucTopupQty.Text + " :Fail: TOP UP Number Must Great Than 0 ! ");

            //////    }
            //////}
            //////else { this.ucTopupQty.ucLabelErrorContent = "Invalid Number!";
            //////    Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text +  " : " + this.ucTopupQty.Text + " :Fail: Invalid Number! "   );
            //////}
        }
        
    }
}
