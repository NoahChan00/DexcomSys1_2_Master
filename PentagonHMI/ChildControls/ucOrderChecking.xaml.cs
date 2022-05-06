using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;

namespace PentagonHMI.ChildControls
{
    /// <summary>
    /// Interaction logic for ucOrderChecking.xaml
    /// </summary>
    public partial class ucOrderChecking : UserControl, IDisposable
    {
        private VanillaDB.DataDBCall DBCall;
        private string ErrMsg = "";

        public ucOrderChecking()
        {
            InitializeComponent();
            string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
            DBCall = new VanillaDB.DataDBCall(Connstr);

            //DataGrid_Loaded("UFO1821-00001");
        }

        private void DataGrid_Loaded(string assetTag)
        {
            DataTable dt = DBCall.Order_Select_ByAssetTag(assetTag, ref ErrMsg);
            if (dt != null)
            {
                dt.Columns.Remove("Current Location");
                gridOrder.DataContext = dt.DefaultView;
            }
            //dt.Rows.RemoveAt(dt.Rows.Count);

            DataTable dt2 = DBCall.Order_Select_Part_Installed(assetTag, ref ErrMsg);
            if(dt2 != null)
            gridInstall.DataContext = dt2.DefaultView;
           // dt2.Rows.RemoveAt(dt2.Rows.Count);

            DataTable dt3 = DBCall.Order_Select_Part_Rejected(assetTag, ref ErrMsg);
            if (dt3 != null)
                gridReject.DataContext = dt3.DefaultView;
           // dt3.Rows.RemoveAt(dt3.Rows.Count);

        }

        #region Destructor
        ~ucOrderChecking()
        {
            Dispose();
        }
        public void Dispose()
        {

        }
        #endregion

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            DataGrid_Loaded(txtBarcode.Text.ToString());
        }

        private void txtBarcode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != System.Windows.Input.Key.Enter) return;

            // your event handler here
            e.Handled = true;

            txtBarcodeScanned.Text = txtBarcode.Text.ToString();
            DataGrid_Loaded(txtBarcode.Text.ToString());
            //MessageBox.Show("Enter pressed");
            txtBarcode.Clear();
        }
    }

    class Dog
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public Dog(string name, int size)
        {
            this.Name = name;
            this.Size = size;
        }
    }
}
