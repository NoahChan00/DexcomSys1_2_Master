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

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCSmallLabelCheckbox.xaml
    /// </summary>
    public partial class UCSmallLabelCheckboxUserAccess : UserControl
    {
        public UCSmallLabelCheckboxUserAccess()
        {
            InitializeComponent();
        }

        #region Properties
        #region ucLabelTitle Properties
        //This DependencyProperty is needed in order for the labels in the program to be changed to the target language that the
        //user has selected in the Main Template window (MainWindow.xaml)
        public static readonly DependencyProperty ucLabelTitleContentProperty =
        DependencyProperty.Register(
            "ucLabelTitleContent", //This must follow the exact property name that you want the DependencyProperty to refer to
            typeof(string),
            typeof(UCSmallLabelCheckboxUserAccess),
            new PropertyMetadata("lblTitle"));

        public string ucLabelTitleContent
        {
            get { return (string)GetValue(ucLabelTitleContentProperty); }
            set { SetValue(ucLabelTitleContentProperty, value); }
        }
        #endregion

        #region ucIsCheck Properties
        public bool ucIsEdit
        {
            get { return Convert.ToBoolean(this.ucCheckBoxEdit.IsChecked); }
            set { this.ucCheckBoxEdit.IsChecked = value; }
        }
        public bool ucIsView
        {
            get { return Convert.ToBoolean(this.ucCheckBoxView.IsChecked); }
            set { this.ucCheckBoxView.IsChecked = value; }
        }
        public Visibility  ucIsEditInvsibility
        {
            get { return this.ucCheckBoxEdit.Visibility ; }
            set { this.ucCheckBoxEdit.Visibility = value; }
        }
        public Visibility ucIsViewInvsibility
        {
            get { return this.ucCheckBoxView.Visibility; }
            set { this.ucCheckBoxView.Visibility = value; }
        }
        public FontWeight ucFontStyle
        {
            get { return this.ucLabelTitle.FontWeight ; }
            set { this.ucLabelTitle.FontWeight = value; }
        }
        #endregion
        #endregion
    }
}
