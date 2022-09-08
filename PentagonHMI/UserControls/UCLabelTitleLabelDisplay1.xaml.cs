using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PentagonHMI.UserControls
{
    /// <summary>
    /// Interaction logic for UCLabelTitleLabelDisplay.xaml
    /// </summary>
    public partial class UCLabelTitleLabelDisplay1 : UserControl
    {
        #region Constructor

        public UCLabelTitleLabelDisplay1()
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
            typeof(UCLabelTitleLabelDisplay1),
            new PropertyMetadata("lblTitle"));

        public string ucLabelTitleContent
        {
            get { return (string)GetValue(ucLabelTitleContentProperty); }
            set { SetValue(ucLabelTitleContentProperty, value); }
        }

        #endregion ucLabelTitle Properties

        public double ucLabelTitleWidth
        {
            get
            {
                return ucLabelTitle.Width;
            }
            set
            {
                ucLabelTitle.Width = value;
                ucLabelDisplay.Margin = new Thickness(value, 0, 0, 0);
                ucrectangle.Margin = new Thickness(value, 0, 0, 0);
            }
        }

        public Brush ucRec
        {
            get
            {
                return Brushes.Transparent;
            }
            set
            {
                ucrectangle.Fill = value;
            }
        }

        public double ucLabelDisplayMinWidth
        {
            get
            {
                return ucLabelDisplay.MinWidth;
            }
            set
            {
                ucLabelDisplay.MinWidth = value;
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
                this.ucLabelDisplay.FontSize = value;
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
                this.ucLabelDisplay.FontWeight = value;
            }
        }

        public string ucText
        {
            get
            {
                return ucLabelDisplay.Content.ToString();
            }
            set
            {
                this.ucLabelDisplay.Dispatcher.Invoke(new Action(() => this.ucLabelDisplay.Content = value));
                //this.ucLabelDisplay.Content = value;
            }
        }

        public Brush ucForeground
        {
            get
            {
                return ucLabelDisplay.Foreground;
            }
            set
            {
                this.ucLabelDisplay.Foreground = value;
                // this.ucLabelTitle.Foreground = value;
            }
        }

        public Style ucStyle
        {
            get
            {
                return ucLabelDisplay.Style;
            }
            set
            {
                this.ucLabelDisplay.Style = value;
                this.ucLabelTitle.Style = value;
            }
        }

        #endregion Properties
    }
}