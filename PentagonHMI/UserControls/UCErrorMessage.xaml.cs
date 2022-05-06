using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for UCErrorMessage.xaml
    /// </summary>
    public partial class UCErrorMessage : UserControl
    {
        public UCErrorMessage()
        {
            InitializeComponent();
        }

        //This DependencyProperty is needed in order for the labels in the program to be changed to the target language that the
        //user has selected in the Main Template window (MainWindow.xaml)
        public static readonly DependencyProperty ErrorCodeProperty =
        DependencyProperty.Register(
            "ErrorCode", //This must follow the exact property name that you want the DependencyProperty to refer to
            typeof(string),
            typeof(UCErrorMessage),
            new PropertyMetadata("N/A"));

        public string ErrorCode
        {
            get { return (string)GetValue(ErrorCodeProperty); }
            set { SetValue(ErrorCodeProperty, value); }
        }

        public static readonly DependencyProperty ModuleProperty =
DependencyProperty.Register(
    "Module",
    typeof(string),
    typeof(UCErrorMessage),
    new PropertyMetadata("N/A"));

        public string Module
        {
            get { return (string)GetValue(ModuleProperty); }
            set { SetValue(ModuleProperty, value); }
        }

        public static readonly DependencyProperty MessageProperty =
DependencyProperty.Register(
"Message",
typeof(string),
typeof(UCErrorMessage),
new PropertyMetadata("N/A"));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        public static readonly DependencyProperty ActionProperty =
DependencyProperty.Register(
"Action",
typeof(string),
typeof(UCErrorMessage),
new PropertyMetadata("N/A"));

        public string Action
        {
            get { return (string)GetValue(ActionProperty); }
            set { SetValue(ActionProperty, value); }
        }

        public static readonly DependencyProperty DateTimeProperty =
DependencyProperty.Register(
"DateTime",
typeof(string),
typeof(UCErrorMessage),
new PropertyMetadata("N/A"));

        public string DateTime
        {
            get { return (string)GetValue(DateTimeProperty); }
            set { SetValue(DateTimeProperty, value); }
        }
    }
}
