using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.ChildContainer.Children.Clear();
            this.ChildContainer.Children.Add(new ChildControls.ucHome());
        }

        private void OnItemSelected(object sender, RoutedEventArgs e)
        {

            TVLeftPanel.Tag = e.OriginalSource;
            //TreeViewItem item = (TreeViewItem)sender;
            //if (item == e.OriginalSource)
            //{
            //    MessageBox.Show(item.Header.ToString());
            //    MessageBox.Show(item.Items.Count.ToString());

            //}
            //else
            //{
            //    MessageBox.Show("Parent of selected");
            //    MessageBox.Show(item.Header.ToString());
            //    MessageBox.Show(item.Items.Count.ToString());
            //}


        }

        private void TreeViewItem_Selected(object sender, RoutedEventArgs e)
        {
            //TreeViewItem item =  (TreeViewItem )sender;
            //if (item.Header.ToString() == "Machine Setting")
            //{
            //    //Initialize instance
            //    ChildControls.ucMachineSetting ucMachineSetting = new ChildControls.ucMachineSetting();
            //    ucMachineSetting.ClickAlarm += new RoutedEventHandler(ClickAlarm);
            //    ucMachineSetting.ClickAlert  += new RoutedEventHandler(ClickAlert);
            //    //Clear the childcontainer
            //    this.ChildContainer.Children.Clear();

            //    //Add the uc to the childcontainer
            //    this.ChildContainer.Children.Add(ucMachineSetting);
                
            //    //Instance clear/release
            //    if (ucMachineSetting != null)
            //    {
            //        ucMachineSetting = null;
            //    }
            //}
        }




        void ClickAlarm(object sender, RoutedEventArgs e)
            {
                ChildControls.ucAlarm ucAlarm = new ChildControls.ucAlarm();
                //Clear the childcontainer
                this.ChildContainer.Children.Clear();
                //Add the uc to the childcontainer
                this.ChildContainer.Children.Add(ucAlarm);

                //Instance clear/release
                if (ucAlarm != null)
                {
                    ucAlarm = null;
                }
            }



        void ClickAlert(object sender, RoutedEventArgs e)
        {
            ChildControls.ucAlertSetting ucAlertSetting = new ChildControls.ucAlertSetting();
            //Clear the childcontainer
            this.ChildContainer.Children.Clear();
            //Add the uc to the childcontainer
            this.ChildContainer.Children.Add(ucAlertSetting);

            //Instance clear/release
            if (ucAlertSetting != null)
            {
                ucAlertSetting = null;
            }
        }

        private void TreeViewItem_Selected_1(object sender, RoutedEventArgs e)
        {
            this.ChildContainer.Children.Clear();
            this.ChildContainer.Children.Add(new ChildControls.ucHome());
        }

        private void TreeViewItem_Selected_2(object sender, RoutedEventArgs e)
        {
            //this.ChildContainer.Children.Clear();
            //this.ChildContainer.Children.Add(new ChildControls.ucFNB());
        }

        private void tviUPC_Selected(object sender, RoutedEventArgs e)
        {
            //this.ChildContainer.Children.Clear();
            //this.ChildContainer.Children.Add(new ChildControls.ucUPC());
        }

        private void TreeViewItem_Selected_3(object sender, RoutedEventArgs e)
        {
            //this.ChildContainer.Children.Clear();
            //this.ChildContainer.Children.Add(new ChildControls.ucSingulator());
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            this.btnSignIn.Visibility = System.Windows.Visibility.Collapsed;
            this.Logo_BtnLogout.Visibility = System.Windows.Visibility.Visible;
        }

        private void Logo_BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.btnSignIn.Visibility = System.Windows.Visibility.Visible;
            this.Logo_BtnLogout.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void TreeViewItem_Selected_4(object sender, RoutedEventArgs e)
        {
            //this.ChildContainer.Children.Clear();
            //this.ChildContainer.Children.Add(new ChildControls.ucSorting());
        }

    }
}
