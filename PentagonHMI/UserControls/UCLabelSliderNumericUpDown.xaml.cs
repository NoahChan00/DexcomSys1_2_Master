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
    /// Interaction logic for ucLabelSliderNumericUpDown.xaml
    /// </summary>
    public partial class UCLabelSliderNumericUpDown : UserControl
    {
        #region Constructor
        public UCLabelSliderNumericUpDown()
        {
            InitializeComponent();
            ucUpDown.Value = 0;
        }
        #endregion

        #region Properties
        #region ucLabelTitle Properties
        //This DependencyProperty is needed in order for the labels in the program to be changed to the target language that the
        //user has selected in the Main Template window (MainWindow.xaml)
        public static readonly DependencyProperty ucLabelTitleContentProperty =
        DependencyProperty.Register(
            "ucLabelTitleContent", //This must follow the exact property name that you want the DependencyProperty to refer to
            typeof(string),
            typeof(UCLabelSliderNumericUpDown),
            new PropertyMetadata("lblTitle"));

        public string ucLabelTitleContent
        {
            get { return (string)GetValue(ucLabelTitleContentProperty); }
            set { SetValue(ucLabelTitleContentProperty, value); }
        }
        #endregion

        /// <summary>
        /// Set the maximum value for the control
        /// </summary>
        double _intMaximun = 100;
        public double Maximun
        {
            get { return _intMaximun; }
            set 
            { 
                _intMaximun = value;
                ucSlider.Maximum = value;
                ucUpDown.Maximum = value;
            }
        }

        /// <summary>
        /// Set the minumum value for the control
        /// </summary>
        double _intMinimum = 0;
        public double Minimum
        {
            get { return _intMinimum; }
            set 
            { 
                _intMinimum = value;
                ucSlider.Minimum = value;
                ucUpDown.Minimum = value;
            }
        }

        /// <summary>
        /// Set the value
        /// </summary>
        public double Value
        {
            get { return Convert.ToDouble(ucUpDown.Value); }
            set { ucUpDown.Value = value; }
        }

        /// <summary>
        /// Format the string shown on the updown control
        /// </summary>
        /// <remarks>
        /// C	 Currency
        /// F	 Fixed Point
        /// G	 General
        /// N	 Number
        /// P	 Percent
        /// Default F2
        /// </remarks>
        public string FormatString
        {
            get { return ucUpDown.FormatString; }
            set { ucUpDown.FormatString = value; }
        }

        /// <summary>
        /// Set the Increment for the updown control 
        /// </summary>
        /// <remarks>
        /// Default 1
        /// </remarks>
        public double Increment
        {
            get { return Convert.ToDouble(ucUpDown.Increment); }
            set { ucUpDown.Increment = value; }
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
                ucSlider.Margin = new Thickness(value, 3, 69, 2);
            }
        }
        #endregion

        #region FormEvents
        private void ucSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ucUpDown.Value = ucSlider.Value;
        }

        private void ucUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ucSlider.Value = Convert.ToDouble(ucUpDown.Value);
        }
        #endregion
    }
}
