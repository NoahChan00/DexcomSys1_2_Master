using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCSmallLabelTextboxErrorMessage.xaml
    /// </summary>
    public partial class UCSmallLabelTextboxErrorMessage : UserControl
    {
        #region Constructor

        public UCSmallLabelTextboxErrorMessage()
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
            typeof(UCSmallLabelTextboxErrorMessage),
            new PropertyMetadata("lblTitle"));

        public string ucLabelTitleContent
        {
            get { return (string)GetValue(ucLabelTitleContentProperty); }
            set { SetValue(ucLabelTitleContentProperty, value); }
        }

        #endregion ucLabelTitle Properties

        private bool _isPassword;

        public bool isPassword
        {
            get { return this._isPassword; }
            set
            {
                _isPassword = value;

                if(_isPassword)
                {
                    this.ucPasswordBox.Visibility = System.Windows.Visibility.Visible;
                    this.ucTextBox.Visibility = System.Windows.Visibility.Collapsed;
                }
                else
                {
                    this.ucPasswordBox.Visibility = System.Windows.Visibility.Collapsed;
                    this.ucTextBox.Visibility = System.Windows.Visibility.Visible;
                }
            }
        }

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
                if(_isPassword)
                {
                    return this.ucPasswordBox.Password;
                }
                else
                {
                    return this.ucTextBox.Text;
                }
            }
            set
            {
                if(_isPassword)
                {
                    this.ucPasswordBox.Password = value;
                }
                else
                {
                    this.ucTextBox.Text = value;
                }
            }
        }

        //Retrieve the TextWrapping from the control
        public TextWrapping textboxWarp
        {
            get
            {
                return this.ucTextBox.TextWrapping;
            }
            set
            {
                this.ucTextBox.TextWrapping = value;
            }
        }

        //Retrieve the AcceptsReturn from the control
        public bool textboxAcceptsReturn
        {
            get
            {
                return this.ucTextBox.AcceptsReturn;
            }
            set
            {
                this.ucTextBox.AcceptsReturn = value;
            }
        }

        //Retrieve the ScrollBarVisibility from the control
        public ScrollBarVisibility textboxScrollBarVisibility
        {
            get
            {
                return this.ucTextBox.VerticalScrollBarVisibility;
            }
            set
            {
                this.ucTextBox.VerticalScrollBarVisibility = value;
            }
        }

        //Retrieve the IsReadOnly from the control
        public bool textboxIsReadOnly
        {
            get
            {
                return this.ucTextBox.IsReadOnly;
            }
            set
            {
                this.ucTextBox.IsReadOnly = value;
            }
        }

        //Retrieve the LabelTitleWidth from the control
        public double minLabelTitleWidth
        {
            get
            {
                return this.ucLabelTitle.MinWidth;
            }
            set
            {
                this.ucTextBox.MinWidth = value;
            }
        }

        //Retrieve the MaxLength from the control
        public int maxLength
        {
            get
            {
                if(_isPassword)
                {
                    return this.ucPasswordBox.MaxLength;
                }
                else
                {
                    return this.ucTextBox.MaxLength;
                }
            }
            set
            {
                if(_isPassword)
                {
                    this.ucPasswordBox.MaxLength = value;
                }
                else
                {
                    this.ucTextBox.MaxLength = value;
                }
            }
        }

        //Enable and disable the control
        public bool isEnabled
        {
            get
            {
                if(_isPassword)
                {
                    return this.ucPasswordBox.IsEnabled;
                }
                else
                {
                    return this.ucTextBox.IsEnabled;
                }
            }
            set
            {
                if(_isPassword)
                {
                    this.ucPasswordBox.IsEnabled = value;
                }
                else
                {
                    this.ucTextBox.IsEnabled = value;
                }
            }
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
                    //this.ucTextBox.Focus();
                    if(_isPassword)
                    {
                        this.ucPasswordBox.Focus();
                    }
                    else
                    {
                        this.ucTextBox.Focus();
                    }
                }
            }
        }

        //Show the error label
        public System.Windows.Visibility ucLabelErrorVisibility
        {
            get { return this.ucLabelError.Visibility; }
            set { this.ucLabelError.Visibility = value; }
        }

        #endregion Properties

        #region Methods

        // Use the PreviewTextInputHandler to respond to key presses
        private void PreviewTextInputHandler(Object sender, TextCompositionEventArgs e)
        {
            if(_isNumeric)
            {
                e.Handled = !IsTextAllowed(e.Text);
            }
        }

        // Use the DataObject.Pasting Handler
        private void PastingHandler(object sender, DataObjectPastingEventArgs e)
        {
            if(e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if(!IsTextAllowed(text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        //Validate the text
        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        #endregion Methods
    }
}