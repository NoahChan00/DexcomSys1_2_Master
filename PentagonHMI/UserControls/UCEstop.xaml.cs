using System.Windows;
using System.Windows.Controls;

namespace PentagonHMI.UserControls
{
    public partial class UCEstop : UserControl
    {
        public UCEstop()
        {
            InitializeComponent();
        }

        public bool IsDown
        {
            get { return false; }
            set
            {
                if(value == true)
                { StopUp.Visibility = Visibility.Collapsed; StopDown.Visibility = Visibility.Visible; }
                else
                { StopDown.Visibility = Visibility.Collapsed; StopUp.Visibility = Visibility.Visible; }
            }
        }

        public string EName
        {
            get { return string.Empty; }
            set { Eup.Content = value; EDown.Content = value; }
        }
    }
}