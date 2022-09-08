using System.Windows;
using System.Windows.Controls;

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCLabelTextboxErrorMessage.xaml
    /// </summary>
    public partial class UCLabelTextbox : UserControl
    {
        #region Constructor

        public UCLabelTextbox()
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
            typeof(UCLabelTextbox),
            new PropertyMetadata("lblTitle"));

        public string ucLabelTitleContent
        {
            get { return (string)GetValue(ucLabelTitleContentProperty); }
            set { SetValue(ucLabelTitleContentProperty, value); }
        }

        #endregion ucLabelTitle Properties

        private bool _isNumeric = false;

        public bool isNumeric
        {
            get { return this._isNumeric; }
            set { _isNumeric = value; }
        }

        //Retrieve the value from the control
        public string Text
        {
            get
            {
                return this.ucTextBox.Content.ToString();
            }
            set
            {
                this.ucTextBox.Content = value;
            }
        }

        public HorizontalAlignment LabelTitleHorizontalAlignment
        {
            get
            {
                return this.ucLabelTitle.HorizontalAlignment;
            }
            set
            {
                this.ucLabelTitle.HorizontalAlignment = value;
            }
        }

        #endregion Properties
    }
}