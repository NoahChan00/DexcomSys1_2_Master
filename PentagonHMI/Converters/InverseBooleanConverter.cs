using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Utilities;

namespace PentagonHMI.Converters
{
    public class InverseBooleanConverter : IValueConverter
    {
        #region PublicInterfaceMethods

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool boolean = false;

            try
            {
                boolean = !System.Convert.ToBoolean(value);
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }

            return boolean;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }

        #endregion PublicInterfaceMethods
    }
}