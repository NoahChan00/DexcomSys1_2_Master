using System.Windows;
using System.Windows.Controls;

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCLabelComboboxErrorMessage.xaml
    /// </summary>
    public partial class UCLabelComboboxErrorMessage : UserControl
    {
        #region Constructor

        public UCLabelComboboxErrorMessage()
        {
            InitializeComponent();
        }

        #endregion Constructor

        #region Properties

        #region ucLabelTitle Properties

        //This DependencyProperty is needed in order for the labels in the program to be changed to the target language that the
        //user has selected in the Main Template window (MainWindow.xaml)
        public static readonly DependencyProperty ucLabelTitleComboboxContentProperty =
        DependencyProperty.Register(
            "ucLabelTitleComboboxContent", //This must follow the exact property name that you want the DependencyProperty to refer to
            typeof(string),
            typeof(UCLabelComboboxErrorMessage),
            new PropertyMetadata("lblTitle"));

        public string ucLabelTitleComboboxContent
        {
            get { return (string)GetValue(ucLabelTitleComboboxContentProperty); }
            set { SetValue(ucLabelTitleComboboxContentProperty, value); }
        }

        #endregion ucLabelTitle Properties

        //Retrieve the text from the control
        public string Text
        {
            get
            {
                return this.ucComboBox.Text;
            }
            set
            {
                this.ucComboBox.Text = value;
            }
        }

        //Retrieve the selectedValue from the control
        public object selectedValue
        {
            get
            {
                return this.ucComboBox.SelectedValue;
            }
            set
            {
                this.ucComboBox.SelectedValue = value;
            }
        }

        //Retrieve the selectedIndex from the control
        public int selectedIndex
        {
            get
            {
                return this.ucComboBox.SelectedIndex;
            }
            set
            {
                this.ucComboBox.SelectedIndex = value;
            }
        }

        //Enable and disable the control
        public bool isEnabled
        {
            get
            {
                return this.ucComboBox.IsEnabled;
            }
            set
            {
                this.ucComboBox.IsEnabled = value;
            }
        }

        //Enable and disable the control
        public ItemCollection items
        {
            get
            {
                return this.ucComboBox.Items;
            }
        }

        public void AddItem(object newItem)
        {
            this.ucComboBox.Items.Add(newItem);
        }

        //Set the error label content and focus the control
        public string ucLabelErrorContent
        {
            get { return this.ucLabelError.Content.ToString(); }
            set
            {
                this.ucLabelError.Content = value;
                if(!string.IsNullOrEmpty(value))
                {
                    this.ucComboBox.Focus();
                }
            }
        }

        public double ucLabelTitleWidth
        {
            get
            {
                return ucLabelTitle.Width;
            }
            set
            {
                ucLabelTitle.Width = value;
                ucComboBox.Margin = new Thickness(value, 0, 12, 0);
                ucLabelError.Margin = new Thickness(value, 20, 12, 0);
            }
        }

        public double ucLabelTitleMaxWidth
        {
            get
            {
                return ucLabelTitle.MaxWidth;
            }
            set
            {
                ucLabelTitle.MaxWidth = value;
                //ucComboBox.Margin = new Thickness(value, 0, 12, 0);
                //ucLabelError.Margin = new Thickness(value, 20, 12, 0);
            }
        }

        public double ucComboBoxMinWidth
        {
            get
            {
                return ucComboBox.MinWidth;
            }
            set
            {
                ucComboBox.MinWidth = value;
                ucLabelError.MinWidth = value;
            }
        }

        public double ucComboBoxWidth
        {
            get
            {
                return ucComboBox.Width;
            }
            set
            {
                ucComboBox.Width = value;
                ucLabelError.Width = value;
            }
        }

        public double ucFontSize
        {
            get
            {
                return ucLabelTitle.FontSize;
            }
            set
            {
                this.ucLabelTitle.FontSize = value;
                this.ucComboBox.FontSize = value;
                this.ucLabelError.FontSize = value;
            }
        }

        public FontWeight ucFontWeight
        {
            get
            {
                return ucLabelTitle.FontWeight;
            }
            set
            {
                this.ucLabelTitle.FontWeight = value;
                this.ucComboBox.FontWeight = value;
                this.ucLabelError.FontWeight = value;
            }
        }

        public void AddHandleSelectionChanged(SelectionChangedEventHandler handler)
        {
            this.ucComboBox.SelectionChanged += handler;
        }

        #endregion Properties
    }
}