using System;
using System.Windows;
using System.Windows.Controls;

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCCheckboxSmallLabel.xaml
    /// </summary>
    public partial class UCCheckboxSmallLabel : UserControl
    {
        #region Constructor

        public UCCheckboxSmallLabel()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Properties

        #region ucLabelTitle Properties

        //This DependencyProperty is needed in order for the labels in the program to be changed to the target language that the
        //user has selected in the Main Template window (MainWindow.xaml)
        public static readonly DependencyProperty ucLabelTitleContentProperty =
        DependencyProperty.Register(
            "ucLabelTitleContent", //This must follow the exact property name that you want the DependencyProperty to refer to
            typeof(string),
            typeof(UCCheckboxSmallLabel),
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

        public bool ucCheckBoxIsHitTestVisible
        {
            get { return this.ucCheckBox.IsHitTestVisible; }
            set { this.ucCheckBox.IsHitTestVisible = value; }
        }

        //Set the width between label and textbox
        public double ucCheckBoxWidth
        {
            get
            {
                return this.ucCheckBox.Width;
            }
            set
            {
                ucCheckBox.Width = value;
                this.ucLabelTitle.Margin = new Thickness(value, 0, 0, 0);
            }
        }

        #endregion ucIsCheck Properties

        #endregion Properties
    }
}