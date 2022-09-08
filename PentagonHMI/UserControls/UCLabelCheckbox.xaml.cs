using System;
using System.Windows;
using System.Windows.Controls;

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCLabelCheckbox.xaml
    /// </summary>
    public partial class UCLabelCheckbox : UserControl
    {
        public UCLabelCheckbox()
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
            typeof(UCLabelCheckbox),
            new PropertyMetadata("lblTitle"));

        public string ucLabelTitleContent
        {
            get { return (string)GetValue(ucLabelTitleContentProperty); }
            set { SetValue(ucLabelTitleContentProperty, value); }
        }

        #endregion ucLabelTitle Properties

        #region ucIsCheck Properties

        public bool ucIsCheck
        {
            get { return Convert.ToBoolean(this.ucCheckBox.IsChecked); }
            set { this.ucCheckBox.IsChecked = value; }
        }

        #endregion ucIsCheck Properties

        #endregion Properties
    }
}