using PentagonHMI.Modules.NumUpDown;
using SimpleDatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI.ChildControls
{
    public partial class ucLotEntry : UserControl, IDisposable
    {
        private SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc(Info.OPC.IP);
        private SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);
        private Dictionary<string, string> dic_BatteryType = new Dictionary<string, string>();
        private List<string> lst_DUTID = new List<string>();
        private List<string> lst_FirmwareVersion = new List<string>();

        private LogicClasses.Main _Main;

        private const string Tag_LotID_str = "Lot_Info.HMI_LotID";
        private const string Tag_LotQty_dint = "Lot_Info.HMI_Lot_Quantity";
        private const string Tag_OprID_str20 = "Lot_Info.HMI_OperatorID";
        private const string Tag_Btry_dint = "Lot_Info.HMI_BatteryType";
        private const string Tag_NewLot_bool = "Lot_Info.HMI_New_Lot_Bit";

        public ucLotEntry(LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;
#if !DEBUG
            if(!OPCore.Connect(Info.OPC.IP))
                return;
#endif
            Initialize();
            main.OnLotUpdate += Update;
        }

        private void Initialize()
        {
            DataTable dt = SQLer.Exec_DTSelect("Select * From LotInfo");

            foreach(DataRow dr in dt.Rows)
            {
                var values = dr["Value"].ToString().Split(';');
                var keys = dr["Keys"].ToString().Split(';');

                if(dr["Name"].ToString() == "BatteryType")
                {
                    for(int n = 0; n < keys.Length; n++)
                        dic_BatteryType.Add(keys[n], values[n]);
                }
                else if(dr["Name"].ToString() == "DutID")
                {
                    for(int n = 0; n < values.Length; n++)
                        lst_DUTID.Add(values[n]);
                }
            }

            cbx_offlineBtrytype.DisplayMemberPath = "Key";
            cbx_offlineBtrytype.SelectedValuePath = "Value";
            cbx_offlineBtrytype.ItemsSource = dic_BatteryType;
        }

        private void Update()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    ucLotEntryUserControl.IsEnabled = !_Main.OPC.Read<bool>("Lot_Info.New_Lot_Created");

                    if(GetServerInfo())
                    {
                    }
                    else
                    {
                        cbx_offlineBtrytype.Visibility = Visibility.Visible;
                    }

                    ActualPcbaRadioButton.IsChecked = _Main.OPC.Read<bool>("HMI_PCBA_Actual");
                    DummyPcbaRadioButton.IsChecked = _Main.OPC.Read<bool>("HMI_PCBA_Dummy");
                }
                catch(Exception exception)
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

        private bool GetServerInfo()
        {
            if(test_ServerOn)
            {
                tbx_onlineBtrytype.Text = "Murata";
            }
            return test_ServerOn;
        }

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

            if(string.IsNullOrWhiteSpace(tbx_LotID.Text))
            {
                MessageBox.Show("Lot ID Not Defined");
                return;
            }
            else if(LR_num_LotSize.Value == null)
            {
                MessageBox.Show("Lot Size Not Defined");
                return;
            }
            else if(string.IsNullOrWhiteSpace(tbx_OprID.Text))
            {
                MessageBox.Show("Operator ID Not Defined");
                return;
            }
            else if(ServerOn)
            {
                if(!dic_BatteryType.TryGetValue(tbx_onlineBtrytype.Text, out string btyEnum))
                {
                    MessageBox.Show("Invalid Battery Type");
                    return;
                }
            }
            else if(!ServerOn)
            {
                if(string.IsNullOrWhiteSpace(cbx_offlineBtrytype.SelectedValue.ToString()))
                {
                    MessageBox.Show("Invalid Battery Type");
                    return;
                }
            }

            OPCore.Write(Tag_OprID_str20, tbx_OprID.Text, typeof(string));
            OPCore.Write(Tag_LotID_str, tbx_LotID.Text, typeof(string));
            OPCore.Write(Tag_LotQty_dint, LR_num_LotSize.Value, typeof(Int32));
            if(ServerOn)
            {
                OPCore.Write(Tag_Btry_dint, dic_BatteryType[tbx_onlineBtrytype.Text], typeof(Int32));
            }
            else
            {
                OPCore.Write(Tag_Btry_dint, cbx_offlineBtrytype.SelectedValue.ToString(), typeof(Int32));
            }
            OPCore.Write(Tag_NewLot_bool, true);
        }

        private void NControl_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OPCore.Write(Tag_LotQty_dint, (sender as NumUpDown).Value, typeof(int));
        }

        private bool test_ServerOn = false;

        private void Server_Toggle(object sender, RoutedEventArgs e)
        {
            test_ServerOn = !test_ServerOn;
        }

        private void ActualPcbaRadioButtonChecked(object sender, RoutedEventArgs e)
        {
            OPCore.Write("HMI_PCBA_Actual", true);
        }

        private void DummyPcbaRaDioButtonChecked(object sender, RoutedEventArgs e)
        {
            OPCore.Write("HMI_PCBA_Dummy", true);
        }
    }
}