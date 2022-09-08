using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PentagonHMI.UserControls
{
    public partial class UCPartNumber : UserControl
    {
        public UCPartNumber()
        {
            InitializeComponent();
        }

        public string Part1
        {
            get { return string.Empty; }
            set { Lbl_Part1.Content = value; }
        }

        public int Part1_font
        {
            get { return 0; }
            set { Lbl_Part1.FontSize = value; }
        }

        public string Part2
        {
            get { return string.Empty; }
            set { Lbl_Part2.Content = value; }
        }

        public int Part2_font
        {
            get { return 0; }
            set { Lbl_Part2.FontSize = value; }
        }

        public string Part3
        {
            get { return string.Empty; }
            set { Lbl_Part3.Content = value; }
        }

        public int Part3_font
        {
            get { return 0; }
            set { Lbl_Part3.FontSize = value; }
        }

        public string Part4
        {
            get { return string.Empty; }
            set { Lbl_Part4.Content = value; }
        }

        public int Part4_font
        {
            get { return 0; }
            set { Lbl_Part4.FontSize = value; }
        }

        public int PartNum
        {
            get { return PartNum; }
            set
            {
                switch(value)
                {
                    case 4:
                        SP4.Visibility = Visibility.Visible;
                        goto case 3;
                    case 3:
                        SP3.Visibility = Visibility.Visible;
                        goto case 2;
                    case 2:
                        SP2.Visibility = Visibility.Visible;
                        goto case 1;
                    case 1:
                        SP1.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private int? low1 = null, low2 = null, low3 = null, low4 = null;

        public void SetLowStockQty(int? Low1 = null, int? Low2 = null, int? Low3 = null, int? Low4 = null)
        {
            low1 = Low1;
            low2 = Low2;
            low3 = Low3;
            low4 = Low4;
        }

        private int pn1 = -1, pn2 = -1, pn3 = -1, pn4 = -1;

        public void SetPartNumber(int p1, int p2 = -1, int p3 = -1, int p4 = -1)
        {
            if(p1 != pn1)
            {
                Lbl_N1.Content = p1;
                if(low1 != null)
                {
                    if(p1 >= low1)
                        Lbl_N1.Background = Brushes.Blue;
                    else if(p1 <= 0)
                        Lbl_N1.Background = Brushes.Black;
                    else if(p1 < low1)
                        Lbl_N1.Background = Brushes.Orange;
                }
                pn1 = p1;
            }

            if(p2 != pn2)
            {
                Lbl_N2.Content = p2;
                if(low2 != null)
                {
                    if(p2 >= low2)
                        Lbl_N2.Background = Brushes.Blue;
                    else if(p2 <= 0)
                        Lbl_N2.Background = Brushes.Black;
                    else if(p2 < low2)
                        Lbl_N2.Background = Brushes.Orange;
                }
                pn2 = p2;
            }

            if(p3 != pn3)
            {
                Lbl_N3.Content = p3;
                if(low3 != null)
                {
                    if(p3 >= low3)
                        Lbl_N3.Background = Brushes.Blue;
                    else if(p3 <= 0)
                        Lbl_N3.Background = Brushes.Black;
                    else if(p3 < low3)
                        Lbl_N3.Background = Brushes.Orange;
                }
                pn3 = p3;
            }

            if(p4 != pn4)
            {
                Lbl_N4.Content = p4;
                if(low4 != null)
                {
                    if(p4 >= low4)
                        Lbl_N4.Background = Brushes.Blue;
                    else if(p4 <= 0)
                        Lbl_N4.Background = Brushes.Black;
                    else if(p4 < low4)
                        Lbl_N4.Background = Brushes.Orange;
                }
                pn4 = p4;
            }
        }
    }
}