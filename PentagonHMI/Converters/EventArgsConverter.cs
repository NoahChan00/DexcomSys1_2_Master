using GalaSoft.MvvmLight.Command;
using PentagonHMI.Models;

namespace PentagonHMI.Converters
{
    public class EventArgsConverter : IEventArgsConverter
    {
        #region PublicInterfaceMethods
        public object Convert(object value, object parameter)
        {
            return new EventArgsModel
            {
                EventArgs = value,
                Parameter = parameter
            };
        }
        #endregion
    }
}