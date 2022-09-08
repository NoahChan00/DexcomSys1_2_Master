using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCPointIO.xaml
    /// </summary>
    public partial class UCPointIO : UserControl
    {
        public UCPointIO()
        {
            InitializeComponent();
        }

        public string ZoneSpan
        {
            get { return ""; }
            set { Set1(value); }
        }

        private void Set1(string Ln)
        {
            int n = 0;
            int width = 53;
            if(Ln.ToUpper().Contains('L'))
            {
                n = Convert.ToInt32(Ln.Remove(0, 1));
                PointIO.Width += 2;
                width = 55;
            }
            else
            {
                n = Convert.ToInt32(Ln);
            }

            if(n > 1)
                PointIO.Width += (width * (n - 1));
        }

        public bool Status
        {
            get { return false; }
            set { Set2(value); }
        }

        private void Set2(bool status)
        {
            if(status)
                PointIO.Background = Brushes.Green;
            else
                PointIO.Background = Brushes.Red;
        }

        public string IP
        {
            get { return ""; }
            set { Set3(value); }
        }

        private void Set3(string ip)
        {
            PointIO.Content = ip;
        }

        public bool Enlarge
        {
            get { return false; }
            set { Set4(value); }
        }

        private void Set4(bool ip)
        {
            PointIO.FontSize = 14;
            PointIO.Width = PointIO.Width * 1.5;
            PointIO.Height = PointIO.Height * 1.5;
        }

        public int ucwidth
        {
            get { return 0; }
            set { Set5(value); }
        }

        private void Set5(int width)
        {
            PointIO.Width = width;
        }
    }
}