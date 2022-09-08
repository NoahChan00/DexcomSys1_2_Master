using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace PentagonHMI.ChildControls
{
    /// <summary>
    /// Interaction logic for ucEventLog.xaml
    /// </summary>
    ///
    public partial class ucAlertList : UserControl, IDisposable
    {
        private VanillaDB.DataDBCall DBCall;
        private string strAlert => "AlarmPage";
        private DataSet ds = new DataSet();
        private string alarm_db_id = "";
        private int alarm_db_errcode = 0;
        private List<string> listErrorCode = new List<string>();
        private DataTable dt = new DataTable();
        private bool ON = false;
        private string ErrMsg = "";

        //string count = "";
        private Thread GetAll;

        private DataTable[] Slist = new DataTable[11];
        private string StationID;
        private bool fil = false;

        public ucAlertList(ref LogicClasses.Main MainConnection)
        {
            try
            {
                InitializeComponent();
                StationID = MainConnection.StationID;
                if(StationID != "0")
                    SP1.Visibility = Visibility.Collapsed;
                //if (Classes.GlobalFunctions.StationName == "0")
                //{ count = "1000"; }
                //else
                //{ count = "500"; }
                string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
                DBCall = new VanillaDB.DataDBCall(Connstr);

                DataTable dtalarm = new DataTable();

                if(StationID == "0")
                {
                    Slist[0] = DBCall.Alarm_Select("2", StationID, ref ErrMsg);
                    string[] selectedColumns = new[] { "AlmCode", "StationID", "ModuleCode", "AlmDesc", "AlmAction" };
                    if(Slist[0] != null)
                        Slist[0] = new DataView(Slist[0]).ToTable(false, selectedColumns);
                    this.dgLog.ItemsSource = Slist[0].DefaultView;
                    GetAll = new Thread(GetAllElist);
                    GetAll.Start();
                }
                else
                {
                    dtalarm = DBCall.Alarm_Select("2", StationID, ref ErrMsg);
                    string[] selectedColumns = new[] { "AlmCode", "StationID", "ModuleCode", "AlmDesc", "AlmAction" };
                    if(dtalarm != null)
                        dtalarm = new DataView(dtalarm).ToTable(false, selectedColumns);

                    if(dtalarm != null)
                    {
                        this.dgLog.ItemsSource = dtalarm.DefaultView;
                    }
                }
                dgLog.SelectionChanged += new SelectionChangedEventHandler(dgLog_SelectionChanged);
            }
            catch
            {
            }
        }

        private void GetAllElist()
        {
            for(int n = 8; n < 11; n++)
            {
                Slist[n] = DBCall.Alarm_Select("2", n.ToString(), ref ErrMsg);
            }
            GetAll.Abort();
            GetAll = null;
        }

        private string GetStnID(int Index)
        {
            if(Index == 1)
                return "8";
            else if(Index == 2)
                return "9";
            else if(Index == 3)
                return "10";
            else
                return "0";
        }

        private void Refresh()
        {
            ON = true;

            DataTable dtalarm = new DataTable();
            string[] selectedColumns = new[] { "AlmCode", "StationID", "ModuleCode", "AlmDesc", "AlmAction" };
            if(fil == true)
            { dtalarm = DBCall.Alarm_Select("1", StationID == "0" ? GetStnID(Cb_Station.SelectedIndex) : StationID /*StationID*/, ref ErrMsg); }
            else
            { dtalarm = DBCall.Alarm_Select("2", StationID == "0" ? GetStnID(Cb_Station.SelectedIndex) : StationID/*StationID*/, ref ErrMsg); }
            dtalarm = new DataView(dtalarm).ToTable(false, selectedColumns);

            if(!string.IsNullOrWhiteSpace(txtStd.Text))
                Slist[Convert.ToInt32(txtStd.Text)] = dtalarm;

            this.dgLog.ItemsSource = dtalarm.DefaultView;

            ON = false;
        }

        public void Dispose()
        {
            Slist = null;
        }

        ~ucAlertList()
        {
            Dispose();
        }

        private void dgLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ON == false)
            {
                object item = dgLog.SelectedItem;

                if(item == null)
                    return;

                if(!string.IsNullOrWhiteSpace((dgLog.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text))
                {
                    alarm_db_id = Convert.ToString((dgLog.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    alarm_db_errcode = Convert.ToInt32((dgLog.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);

                    string AlarmID = (dgLog.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                    this.txtAlarmCode.Dispatcher.Invoke(new Action(() => this.txtAlarmCode.Text = AlarmID));
                }
                else
                {
                    this.txtAlarmCode.Dispatcher.Invoke(new Action(() => this.txtAlarmCode.Text = ""));
                }

                if(!string.IsNullOrWhiteSpace((dgLog.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text))
                {
                    string std = (dgLog.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text;
                    this.txtStd.Dispatcher.Invoke(new Action(() => this.txtStd.Text = std));
                }
                else
                {
                    this.txtStd.Dispatcher.Invoke(new Action(() => this.txtStd.Text = ""));
                }

                if(!string.IsNullOrWhiteSpace((dgLog.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text))
                {
                    string ErrorMsg = (dgLog.SelectedCells[2].Column.GetCellContent(item) as TextBlock).Text;
                    this.txtModule.Dispatcher.Invoke(new Action(() => this.txtModule.Text = ErrorMsg));
                }
                else
                {
                    this.txtModule.Dispatcher.Invoke(new Action(() => this.txtModule.Text = ""));
                }

                if(!string.IsNullOrWhiteSpace((dgLog.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text))
                {
                    string ErrorMsg = (dgLog.SelectedCells[3].Column.GetCellContent(item) as TextBlock).Text;
                    this.txtErrorMessage.Dispatcher.Invoke(new Action(() => this.txtErrorMessage.Text = ErrorMsg));
                }
                else
                {
                    this.txtErrorMessage.Dispatcher.Invoke(new Action(() => this.txtErrorMessage.Text = ""));
                }

                if(!string.IsNullOrWhiteSpace((dgLog.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text))
                {
                    string Action = (dgLog.SelectedCells[4].Column.GetCellContent(item) as TextBlock).Text;
                    this.txtAction.Dispatcher.Invoke(new Action(() => this.txtAction.Text = Action));
                }
                else
                {
                    this.txtAction.Dispatcher.Invoke(new Action(() => this.txtAction.Text = ""));
                }
            }
        }

        private void txtAlarmCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox a = sender as TextBox;

            foreach(Char c in a.Text.ToCharArray())
            {
                if(char.IsControl(c) || char.IsDigit(c) || (c == '.'))
                {
                    e.Handled = true;
                }
            }
        }

        private void txtModule_LostFocus(object sender, TextChangedEventArgs e)
        {
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utilities.FileLogger.logButton(strAlert, "Save Button - Update Alert", MethodBase.GetCurrentMethod().ToString());

                bool start = DBCall.Alarm_Update(txtAlarmCode.Text, txtStd.Text /*StationID*/, txtModule.Text, txtErrorMessage.Text, txtAction.Text, Classes.GlobalFunctions.userID, ref ErrMsg);

                if(ErrMsg == "")
                {
                    MessageBox.Show("Successfully updated the error List");
                }
                else
                {
                    MessageBox.Show("Fail to update the error List\n" + ErrMsg);
                }

                Refresh();
            }
            catch(Exception error)
            {
                MessageBox.Show(error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtErrorMessage_LostFocus(object sender, RoutedEventArgs e)
        {
            var regexItem = new Regex("[^a-zA-Z0-9# ] -");

            if(regexItem.IsMatch(txtErrorMessage.Text))
            {
                MessageBox.Show("Special character cannot be use in error message", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtErrorMessage.Text = "";
                return;
            }
        }

        private void txtAction_LostFocus(object sender, RoutedEventArgs e)
        {
            //var regexItem = new Regex("[^a-zA-Z0-9 ]");

            //if (regexItem.IsMatch(txtAction.Text))
            //{
            //    MessageBox.Show("Special character cannot be use in error message", "Invalid Input", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    txtAction.Text = "";
            //    return;

            //}
        }

        private void Cb_Station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int n = Cb_Station.SelectedIndex;
            if(n == 1)
                n = 8;
            else if(n == 2)
                n = 9;
            else if(n == 3)
                n = 10;

            if(Slist[n] != null)
            {
                string[] selectedColumns = new[] { "AlmCode", "StationID", "ModuleCode", "AlmDesc", "AlmAction" };
                if(Slist[n] != null)
                    Slist[n] = new DataView(Slist[n]).ToTable(false, selectedColumns);
                this.dgLog.ItemsSource = Slist[n].DefaultView;
            }
        }
    }
}