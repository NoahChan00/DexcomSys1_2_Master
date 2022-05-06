using System;
using System.Windows.Controls;
using Utilities;
using System.Data;
using System.Windows;
using System.Linq;

namespace PentagonHMI.Views
{
    public partial class LabelSetupView : UserControl, IDisposable
    {
        LogicClasses.Main _Main;
        DataTable DT_TableTemplate = new DataTable();

        #region Constructor
        public LabelSetupView(ref LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                _Main = _main;
                UpdateDataGrid();
                DT_TableTemplate.Columns.Add("Number");
                DT_TableTemplate.Columns.Add("Part Number (P Field)");
                DT_TableTemplate.Columns.Add("Quantity (Q Field)");
                DT_TableTemplate.Columns.Add("ST Batch Number (S Field)");
                DT_TableTemplate.Columns.Add("Brady Batch Number (Z Field)");
                for (int n = 0; n < 10;)
                    DT_TableTemplate.Rows.Add(++n);
                UpdateDataGrid();
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion

        private void UpdateDataGrid()
        {
            string[] PCodes = _Main.OPC.Read<string[]>(string.Format(Info.OPC.Tags.MainView.Array_Output_PCode_str, 1), typeof(string), 10);
            string[] QCodes = _Main.OPC.Read<string[]>(string.Format(Info.OPC.Tags.MainView.Array_Output_QCode_str, 1), typeof(string), 10);
            string[] SCodes = _Main.OPC.Read<string[]>(string.Format(Info.OPC.Tags.MainView.Array_Output_SCode_str, 1), typeof(string), 10);
            string[] ZCodes = _Main.OPC.Read<string[]>(string.Format(Info.OPC.Tags.MainView.Array_Output_ZCode_str, 1), typeof(string), 10);
            int n = 0;
            foreach (DataRow dr in DT_TableTemplate.Rows)
            {
                dr[1] = PCodes[n];
                dr[2] = QCodes[n];
                dr[3] = SCodes[n];
                dr[4] = ZCodes[n];
                n++;
            }
            Dgd_LabelHistory.ItemsSource = DT_TableTemplate.AsDataView();

            tbx_Selected_PField.Text = string.Empty;
            tbx_Selected_QField.Text = string.Empty;
            tbx_Selected_SField.Text = string.Empty;
            tbx_Selected_ZField.Text = string.Empty;

            dtp_Date.Text = _Main.OPC.Read<string>(Info.OPC.Tags.Printer.Tag_Printer_Date_str);
            tbx_CountryOfOrigin.Text = _Main.OPC.Read<string>(Info.OPC.Tags.Printer.Tag_Printer_Origin_str);
            tbx_PersoLocation.Text = _Main.OPC.Read<string>(Info.OPC.Tags.Printer.Tag_Printer_Location_str);
            tbx_MachineNumber.Text = _Main.OPC.Read<string>(Info.OPC.Tags.Printer.Tag_Printer_MachineTool_str);
        }

        private void Dgd_LabelHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid Dg = sender as DataGrid;
            DataRowView items = Dg.SelectedItem as DataRowView;
            if (items != null)
            {
                tbx_Selected_PField.Text = items[1]?.ToString() ?? string.Empty;
                tbx_Selected_QField.Text = items[2]?.ToString() ?? string.Empty;
                tbx_Selected_SField.Text = items[3]?.ToString() ?? string.Empty;
                tbx_Selected_ZField.Text = items[4]?.ToString() ?? string.Empty;
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        int CurrentIndex = 0;
        private void btn_Reprint_Click(object sender, RoutedEventArgs e)
        {
            if (Dgd_LabelHistory.SelectedIndex < 0)
                return;

            int test;
            DateTime dt;
            int valid = 0;
            string Err = string.Empty;
            //Check Q Field
            if (int.TryParse(tbx_Selected_QField.Text, out test))
                ++valid;
            else
                Err += "Q Field Invalid Input; ";
            //Check DateFormat
            if (DateTime.TryParse(dtp_Date.Text, out dt))
                ++valid;
            else
                Err += "Date Invalid Input; ";

            //Check Country of Origin
            if (!string.IsNullOrWhiteSpace(tbx_CountryOfOrigin.Text))
                ++valid;
            else
                Err += "Country of Origin Shuold Not be Empty; ";

            ////Check Perso Location
            //if ((tbx_PersoLocation.Text?.Length ?? 0) == 1 && tbx_PersoLocation.Text.All(char.IsLetter))
            //    ++valid;
            //else
            //    Err += "Perso Location Invalid Input; ";

            ////Check Machine Number
            //if ((tbx_MachineNumber.Text?.Length ?? 0) == 2 &&
            //    int.TryParse(tbx_MachineNumber.Text, out test))
            //    ++valid;
            //else
            //    Err += "Machine Number Invalid Input; ";

            if (valid == 3)
            {
                loginScreen.Visibility = Visibility.Visible;
                CurrentIndex = Dgd_LabelHistory.SelectedIndex + 1;
            }
            else
                MessageBox.Show(Err);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            loginScreen.Visibility = Visibility.Collapsed;
            ucPassword.Text = string.Empty;
            ucUsername.Text = string.Empty;
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            ucPassword.ucLabelErrorContent = string.Empty;
            if (_Main.SQLer.Exec_Scalar<int>($"SELECT COUNT(USERNAME) FROM USERS WHERE USERNAME = '{ucUsername.Text}' AND PASSWORD = '{ucPassword.Text}'") > 0)
            {
                loginScreen.Visibility = Visibility.Collapsed;

                _Main.OPC.Write(string.Format(Info.OPC.Tags.MainView.Array_Output_QCode_str,CurrentIndex), tbx_Selected_QField.Text, typeof(string));                                              
                //_Main.PrinterTrigger.Test(
                //    PrinterFile: _Main.PrinterFile,
                //    Printer_Location: tbx_PersoLocation.Text,
                //    Printer_MachineTool: tbx_MachineNumber.Text,
                //    Date: dtp_Date.Text,
                //    CurrentInput_OutputPartNumber: _Main.OPC.Read<string>(Info.OPC.Tags.MainView.CurrentInput_OutputPartNumber_str),
                //    CurrentInput_QCode: _Main.OPC.Read<string>(Info.OPC.Tags.MainView.CurrentInput_QCode_str),
                //    CurrentInput_SCode: _Main.OPC.Read<string>(Info.OPC.Tags.MainView.CurrentInput_SCode_str),
                //    CurrentInput_ZCode: _Main.OPC.Read<string>(Info.OPC.Tags.MainView.CurrentInput_ZCode_str)
                //    );

                _Main.OPC.Write("HMI_Printer_Print", true);

                MessageBox.Show("Triggered Successfully");
            }
            else
            {
                ucPassword.ucLabelErrorContent = "invalid credentials";
            }

            ucPassword.Text = string.Empty;
            ucUsername.Text = string.Empty;
        }

        public void Dispose()
        {

        }
    }
}