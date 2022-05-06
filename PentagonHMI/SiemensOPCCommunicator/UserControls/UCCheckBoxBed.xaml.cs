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

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCCheckBoxBed.xaml
    /// </summary>
    public partial class UCCheckBoxBed : UserControl
    {
        public UCCheckBoxBed()
        {
            InitializeComponent();
        }

        public string IOvalue
        {
            get { return ""; }
            set { Set(value); }
        }

        private void Set(string s)
        {
            Zin.IsChecked = s.Split(' ')[0] == "T"? true : false;
            Zout.IsChecked = s.Split(' ')[1] == "T" ? true : false;
            Zpresent.IsChecked = s.Split(' ')[2] == "T" ? true : false;
        }

        private void Zout_Click(object sender, RoutedEventArgs e)
        {
            ((CheckBox)sender).IsChecked = ((CheckBox)sender).IsChecked == true ? false : true;
        }
    }
}
