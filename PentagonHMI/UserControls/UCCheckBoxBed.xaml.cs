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
            //if (s.Split(' ')[3] == "T")
            //{ this.ZoneStatus.Dispatcher.Invoke(new Action(() => this.ZoneStatus.Background = Brushes.Green)); this.ZoneStatus.Dispatcher.Invoke(new Action(() => this.ZoneStatus.Content = "Normal"));
            //}
            //else
            //{ this.ZoneStatus.Dispatcher.Invoke(new Action(() => this.ZoneStatus.Background = Brushes.Red));
            //    this.ZoneStatus.Dispatcher.Invoke(new Action(() => this.ZoneStatus.Content = "Abnormal"));
            //}

        }

        public bool NoPresentSensor
        {
            get { return Zpresent.Visibility == Visibility.Hidden ? true:false; }
            set { Set2(value); }
        }

        private void Set2(bool NoPresentSensor)
        {
            if(NoPresentSensor)
            Zpresent.Visibility = Visibility.Hidden;
        }

        private void Zout_Click(object sender, RoutedEventArgs e)
        {
            ((CheckBox)sender).IsChecked = ((CheckBox)sender).IsChecked == true ? false : true;
        }
    }
}
