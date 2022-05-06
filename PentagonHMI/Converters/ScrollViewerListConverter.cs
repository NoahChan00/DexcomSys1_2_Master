using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;

namespace PentagonHMI.Converters
{
    public class ScrollViewerListConverter : IMultiValueConverter
    {
        #region PublicInterfaceMethods
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            List<ScrollViewer> scrollViewerList = new List<ScrollViewer>();

            values.ToList().ForEach(x =>
            {
                scrollViewerList.Add(x as ScrollViewer);
            });

            return scrollViewerList;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
        #endregion
    }
}