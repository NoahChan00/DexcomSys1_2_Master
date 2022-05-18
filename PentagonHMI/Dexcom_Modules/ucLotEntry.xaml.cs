using System;
using System.Windows.Media;
using System.Windows.Controls;
using Utilities;
using System.Windows;
using System.Collections.Generic;
using SimpleDatabase;
using System.Reflection;
using LiveCharts;
using LiveCharts.Wpf;
using System.Text.RegularExpressions;
using System.Data;
using PentagonHMI.Modules.NumUpDown;

namespace PentagonHMI.ChildControls
{
    public partial class ucLotEntry : UserControl, IDisposable
    {
        SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc();
        SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName);
        Dictionary<string, string> dic_BatteryType = new Dictionary<string, string>();
        List<string> lst_DUTID = new List<string>();
        List<string> lst_FirmwareVersion = new List<string>();

        private Dictionary<object, Control> dic_EngMajor;

        private LogicClasses.Main _Main;

        const string Tag_LotID_str = "Lot_Info.HMI_LotID";
        const string Tag_LotQty_dint = "Lot_Info.HMI_Lot_Quantity";
        const string Tag_OprID_str20 = "Lot_Info.HMI_OperatorID";
        const string Tag_DUTID_str20 = "RecipeParams.DUTid";
        const string Tag_Btry_dint = "Lot_Info.HMI_BatteryType";
        //const string Tag_FirmwareVersion_str20 = "RecipeParams.FirmwareVersion";
        //const string Tag_DayToExp_int = "Lot_Info.HMI_DaysToExpired";
        //const string Tag_ManufactureDate_str20 = "Lot_Info.HMI_ManufactureDate";
        //const string Tag_Exp_str20 = "Lot_Info.HMI_Expiration_Date";
        //const string Tag_NewLot_bool = "Lot_Info.HMI_New_Lot_Bit";
        //const string Tag_LotCreated_bool = "Lot_Info.New_Lot_Created";
        //const string Tag_Mode_Commercial_bool = "Lot_Info.HMI_Commercial_Mode";
        //const string Tag_Mode_NonCommercial_bool = "Lot_Info.HMI_NonCommercial_Mode";

        public ucLotEntry(LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;
#if !DEBUG
            if (!OPCore.Connect(Info.OPC.IP)) return;
            //btn_ServerTest.Visibility = Visibility.Collapsed;
#endif
            Initialize();
            main.OnLotUpdate += Update;
        }

        private void Initialize()
        {
            DataTable dt = SQLer.Exec_DTSelect("Select * From LotInfo");

            foreach (DataRow dr in dt.Rows)
            {
                var values = dr["Value"].ToString().Split(';');
                var keys = dr["Keys"].ToString().Split(';');


                if (dr["Name"].ToString() == "BatteryType")
                {
                    for (int n = 0; n < keys.Length; n++)
                        dic_BatteryType.Add(keys[n], values[n]);
                }
                else if (dr["Name"].ToString() == "DutID")
                {
                    for (int n = 0; n < values.Length; n++)
                        lst_DUTID.Add(values[n]);
                }
                //else if (dr["Name"].ToString() == "FirmwareVersion")
                //{
                //    for (int n = 0; n < values.Length; n++)
                //        lst_FirmwareVersion.Add(values[n]);
                //}
            }

            cbx_offlineBtrytype.DisplayMemberPath = "Key";
            cbx_offlineBtrytype.SelectedValuePath = "Value";
            cbx_offlineBtrytype.ItemsSource = dic_BatteryType;

            cbx_offlineDUT.ItemsSource = lst_DUTID;
            //cbx_offlineFirmwareVersion.ItemsSource = lst_FirmwareVersion;
        }

        private void Update()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    //grd_Main.IsEnabled = !OPCore.Read<bool>(Tag_LotCreated_bool);

                    if (GetServerInfo())
                    {
                        cbx_offlineDUT.Visibility = Visibility.Hidden;
                        //cbx_offlineFirmwareVersion.Visibility = Visibility.Hidden;
                        cbx_offlineBtrytype.Visibility = Visibility.Hidden;
                        //tbx_day2Exp.IsEnabled = false;
                    }
                    else
                    {
                        cbx_offlineDUT.Visibility = Visibility.Visible;
                        //cbx_offlineFirmwareVersion.Visibility = Visibility.Visible;
                        cbx_offlineBtrytype.Visibility = Visibility.Visible;
                        //tbx_day2Exp.IsEnabled = true;
                    }

                    //tbx_mdate.Text = DateTime.Now.ToShortDateString();
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            });
        }

        ~ucLotEntry()
        {
            Dispose();
        }

        public void Dispose()
        {
            _Main.LotPageOn = false;
        }

        //Get From Server
        private bool GetServerInfo()
        {
            if (test_ServerOn)
            {
                tbx_onlineDUT.Text = "DUT1214";
                tbx_onlineBtrytype.Text = "Murata";
                //tbx_onlineFirmwareVersion.Text = "V1.0.12";
                //tbx_day2Exp.Text = "31";
            }
            return test_ServerOn;
        }

        //private void LotMode_Selected(object sender, RoutedEventArgs e)
        //{
        //    RadioButton rb = sender as RadioButton;
        //    if (rb.Content == null)
        //        return;

        //    if (rb.Content.ToString().ToUpper() == "COMMERCIAL")
        //    {
        //        //OPCore.Write(Tag_Mode_Commercial_bool, true);
        //        tbx_LotID.Text = "";
        //        tbx_LotID.IsEnabled = true;
        //    }
        //    else if (rb.Content.ToString().ToUpper() == "NON-COMMERCIAL")
        //    {
        //        //OPCore.Write(Tag_Mode_NonCommercial_bool, true);
        //        tbx_LotID.Text = rb.Content.ToString();
        //        tbx_LotID.IsEnabled = false;

        //    }
        //}

        private static readonly Regex _regex = new Regex("[^0-9]+");
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void OnlyInt(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void NewLot_Click(object sender, RoutedEventArgs e)
        {
            bool ServerOn = GetServerInfo();

            if (string.IsNullOrWhiteSpace(tbx_LotID.Text))
            {
                MessageBox.Show("Lot ID Not Defined");
                return;
            }
            else if /*string.IsNullOrWhiteSpace*/(LR_num_LotSize.Value == null)
            {
                MessageBox.Show("Lot Size Not Defined");
                return;
            }
            else if (string.IsNullOrWhiteSpace(tbx_OprID.Text))
            {
                MessageBox.Show("Operator ID Not Defined");
                return;
            }
            //else if (string.IsNullOrWhiteSpace(tbx_day2Exp.Text))
            //{
            //    MessageBox.Show("Days To Expire Not Defined");
            //    return;
            //}
            else if (ServerOn)
            {
                if (string.IsNullOrWhiteSpace(tbx_onlineDUT.Text))
                {
                    MessageBox.Show("DUT ID Not Defined");
                    return;
                }
                else if (!dic_BatteryType.TryGetValue(tbx_onlineBtrytype.Text, out string btyEnum))
                {
                    MessageBox.Show("Invalid Battery Type");
                    return;
                }
                //else if (string.IsNullOrWhiteSpace(tbx_onlineFirmwareVersion.Text))
                //{
                //    MessageBox.Show("Firmware Version Not Defined");
                //    return;
                //}
            }
            else if (!ServerOn)
            {
                if (string.IsNullOrWhiteSpace(cbx_offlineDUT.SelectedValue.ToString()))
                {
                    MessageBox.Show("DUT ID Not Defined");
                    return;
                }
                else if (string.IsNullOrWhiteSpace(cbx_offlineBtrytype.SelectedValue.ToString()))
                {
                    MessageBox.Show("Invalid Battery Type");
                    return;
                }
                //else if (string.IsNullOrWhiteSpace(cbx_offlineFirmwareVersion.SelectedValue.ToString()))
                //{
                //    MessageBox.Show("Firmware Version Not Defined");
                //    return;
                //}
            }

            OPCore.Write(Tag_LotID_str, tbx_LotID.Text, typeof(string));
            OPCore.Write(Tag_LotQty_dint, LR_num_LotSize.Value, typeof(Int32));
            OPCore.Write(Tag_OprID_str20, tbx_OprID.Text, typeof(string));
            //OPCore.Write(Tag_DayToExp_int, tbx_day2Exp.Text, typeof(int));
            if (ServerOn)
            {
                OPCore.Write(Tag_DUTID_str20, tbx_onlineDUT.Text, typeof(string));
                OPCore.Write(Tag_Btry_dint, dic_BatteryType[tbx_onlineBtrytype.Text], typeof(Int32));
                //OPCore.Write(Tag_FirmwareVersion_str20, tbx_onlineFirmwareVersion.Text, typeof(string));
            }
            else
            {
                OPCore.Write(Tag_DUTID_str20, cbx_offlineDUT.SelectedValue.ToString(), typeof(string));
                OPCore.Write(Tag_Btry_dint, cbx_offlineBtrytype.SelectedValue.ToString(), typeof(Int32));
                //OPCore.Write(Tag_FirmwareVersion_str20, cbx_offlineFirmwareVersion.SelectedValue.ToString(), typeof(string));
            }
            //OPCore.Write(Tag_Exp_str20, DateTime.Now.AddDays(Convert.ToInt16(tbx_day2Exp.Text)).ToString("yyMMdd"), typeof(string));
            //OPCore.Write(Tag_NewLot_bool, true);
        }

        private void NControl_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
                OPCore.Write(Tag_LotQty_dint, (sender as NumUpDown).Value, typeof(int));
        }

        bool test_ServerOn = false;
        private void Server_Toggle(object sender, RoutedEventArgs e)
        {
            test_ServerOn = !test_ServerOn;
        }
    }
}
