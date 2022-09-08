using System;

using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCCheckboxLabelType2.xaml
    /// </summary>
    public partial class UCCheckboxLabelType2 : UserControl
    {
        #region Constructor

        public UCCheckboxLabelType2()
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
            typeof(UCCheckboxLabelType2),
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
            set
            {
                //this to avoid cross thread exception
                //this.ucCheckBox.Dispatcher.Invoke(new Action(() => this.ucCheckBox.IsChecked =value));
                //this.ucCheckBox.Dispatcher.Invoke(new Action(() => this.ucCheckBox.IsChecked =value));
                //isAuto = true;
                this.ucCheckBox.IsChecked = value;
            }
        }

        public bool ucIsHitTestVisible
        {
            get { return this.ucCheckBox.IsHitTestVisible; }
            set
            {
                //this to avoid cross thread exception
                //this.ucCheckBox.Dispatcher.Invoke(new Action(() => this.ucCheckBox.IsHitTestVisible = value));
                this.ucCheckBox.IsHitTestVisible = value;
                //ucCheckBox.IsChecked= value;
            }
        }

        #endregion ucIsCheck Properties

        #endregion Properties

        #region Variable

        public event EventHandler ucChecked;

        public event EventHandler ucUnChecked;

        public event EventHandler ucMouseLeftButtonUp;

        public event EventHandler ucMouseClick;

        //private bool isAuto = false;
        //private bool isTrigger = false;

        #endregion Variable

        #region FormEvents

        private void ucCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            if(ucUnChecked != null)
            {
                ucUnChecked(this, EventArgs.Empty);
                //if (!isAuto)
                //{
                //    ucUnChecked(this, EventArgs.Empty);
                //}
                //isAuto = false;
            }
        }

        private void ucCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(ucChecked != null)
            {
                ucChecked(this, EventArgs.Empty);
                //if (!isAuto)
                //{
                //    ucChecked(this, EventArgs.Empty);
                //}
                //isAuto = false;
            }
        }

        private void ucCheckBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(ucMouseLeftButtonUp != null)
            {
                ucMouseLeftButtonUp(this, EventArgs.Empty);
            }
        }

        #endregion FormEvents

        private void ucCheckBox_Click(object sender, RoutedEventArgs e)
        {
            if(ucMouseClick != null)
            {
                ucMouseClick(this, EventArgs.Empty);
            }
        }
    }
}