using SimpleOPC;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace PentagonHMI.DIMM_Modules.DummyMes
{
    public partial class ucDummyMES : UserControl, IDisposable
    {
        private INGEAR_Opc OPC;
        private Dictionary<string, KeyValuePair<TextBox, string>> Dic_Tag_TextboxTagName;
        private const string MPNRack1 = "Dummy_MPN1_string";
        private const string MPNRack2 = "Dummy_MPN2_string";
        private const string MPNRack3 = "Dummy_MPN1_Response_string";
        private const string MPNRack4 = "Dummy_MPN2_Response_string";

        public ucDummyMES(INGEAR_Opc Main_OPC)
        {
            InitializeComponent();
            OPC = Main_OPC;
            Initialize();
        }

        private void Initialize()
        {
            SetupInfo();
            ReadCurrentMPN();
        }

        private void SetupInfo()
        {
            Dic_Tag_TextboxTagName = new Dictionary<string, KeyValuePair<TextBox, string>>
            {
                ["1"] = new KeyValuePair<TextBox, string>(Tbx_Rack1MPN, MPNRack1),
                ["2"] = new KeyValuePair<TextBox, string>(Tbx_Rack2MPN, MPNRack2),
                ["3"] = new KeyValuePair<TextBox, string>(Tbx_Rack3MPN, MPNRack3),
                ["4"] = new KeyValuePair<TextBox, string>(Tbx_Rack4MPN, MPNRack4),
            };
        }

        private void ReadCurrentMPN()
        {
            Tbx_Rack1MPN.Text = OPC.Read<string>(MPNRack1);
            Tbx_Rack2MPN.Text = OPC.Read<string>(MPNRack2);
            Tbx_Rack3MPN.Text = OPC.Read<string>(MPNRack3);
            Tbx_Rack4MPN.Text = OPC.Read<string>(MPNRack4);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string Tags = btn.Tag.ToString();
            var TextboxTagName = Dic_Tag_TextboxTagName[Tags];
            if(Tags == "3" || Tags == "4")
            {
                TextBox tbx = Tags == "3" ? Tbx_Rack3MPN : Tbx_Rack4MPN;
                if(tbx.Text.Length < 2 || !tbx.Text.Contains(","))
                {
                    MessageBox.Show("Invalid MPN Slot Data Format");
                    return;
                }
                var Temp = tbx.Text.Remove(Tbx_Rack3MPN.Text.Length - 1).Split(',');
                foreach(string n in Temp)
                    if(n != "0" && n != "1")
                    {
                        MessageBox.Show("Invalid MPN Slot Data Format");
                        return;
                    }
            }
            MessageBox.Show(OPC.Write(TextboxTagName.Value, TextboxTagName.Key.Text, typeof(string)) ?
            "Updated" : "Update Failed");
        }

        public void Dispose()
        {
            //Dispose Here
        }
    }
}