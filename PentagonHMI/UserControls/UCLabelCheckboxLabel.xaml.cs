using System.Windows;
using System.Windows.Controls;

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCLabelCheckboxLabel.xaml
    /// </summary>
    public partial class UCLabelCheckboxLabel : UserControl
    {
        #region Constructor

        public UCLabelCheckboxLabel()
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
            typeof(UCLabelCheckboxLabel),
            new PropertyMetadata("lblTitle"));

        public string ucLabelTitleContent
        {
            get { return (string)GetValue(ucLabelTitleContentProperty); }
            set { SetValue(ucLabelTitleContentProperty, value); }
        }

        #endregion ucLabelTitle Properties

        public bool? ucIsCheck
        {
            get { return this.ucCheckBox.IsChecked; }
            set
            {
                //this to avoid cross thread exception
                //this.ucCheckBox.Dispatcher.Invoke(new Action(() => this.ucCheckBox.IsChecked = value));
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

        //Set the width between label and textbox
        public double ucCheckBoxPosition
        {
            get
            {
                return this.ucCheckBox.Margin.Left;
            }
            set
            {
                this.ucLabelTitle.Width = value - 5;
                this.ucCheckBox.Margin = new Thickness(value, 2, 0, 6);
                this.ucLabelContent.Margin = new Thickness(value + 20, 0, 0, 6);
            }
        }

        public double ucLabelContentWidth
        {
            get { return this.ucLabelContent.Width; }
            set
            {
                this.ucLabelContent.Width = value;
            }
        }

        public string ucLabelContentText
        {
            get { return this.ucLabelContent.Content.ToString(); }
            set
            {
                this.ucLabelContent.Content = value;
            }
        }

        public bool ucCheckBoxIsHitTestVisible
        {
            get { return this.ucCheckBox.IsHitTestVisible; }
            set { this.ucCheckBox.IsHitTestVisible = value; }
        }

        public bool ucIsThreeState
        {
            get { return this.ucCheckBox.IsThreeState; }
            set { this.ucCheckBox.IsThreeState = value; }
        }

        #endregion Properties
    }
}