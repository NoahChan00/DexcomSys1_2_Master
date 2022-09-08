using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace PentagonHMI.UserControls
{
    public partial class UCIOLocMain : UserControl
    {
        public UCIOLocMain()
        {
            InitializeComponent();
        }

        private Canvas Cvs = null;
        private Polygon Poly = null;

        private void Paging_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string Tag = sender.GetType() == typeof(Polygon) ?
                ((Polygon)sender).Tag.ToString() : ((Image)sender).Tag.ToString();
                Canvas CvsNow = (Canvas)FindName(Tag.Contains("Sub") ? Tag.Substring(0, Tag.IndexOf("Sub")) : Tag);
                Polygon PolyNow = (Polygon)FindName("Poly" + Tag);
                if(Poly != null)
                    Poly.Visibility = Visibility.Collapsed;
                if(PolyNow != null)
                    PolyNow.Visibility = Visibility.Visible;
                Poly = PolyNow;
                if(CvsNow == null)
                    return;
                CvsNow.Visibility = Visibility.Visible;
                if(Cvs != null && !(Cvs.Name.Contains("0") && Cvs.Name.Length == 2))
                    Cvs.Visibility = Visibility.Collapsed;
                Cvs = CvsNow;
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        public void Update(List<bool> _IValue, List<bool> _OValue)
        {
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => TabLoc.SelectedIndex = Convert.ToInt32(((Image)sender).Tag)));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => TabLoc.SelectedIndex = 0));
        }
    }
}