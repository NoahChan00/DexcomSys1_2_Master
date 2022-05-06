using GalaSoft.MvvmLight;
using System.Windows.Media;

namespace PentagonHMI
{
    public class RackStatusSlotStatusModel : ViewModelBase
    {
        #region PublicProperties
        private string _rackStatusSlotStatusValue = string.Empty;
        public string RackStatusSlotStatusValue
        {
            get { return _rackStatusSlotStatusValue; }
            set
            {
                if (_rackStatusSlotStatusValue != value)
                {
                    _rackStatusSlotStatusValue = value;
                    RaisePropertyChanged(nameof(RackStatusSlotStatusValue));
                }
            }
        }
        
        private string _rackStatusSlotStatusName = string.Empty;
        public string RackStatusSlotStatusName
        {
            get { return _rackStatusSlotStatusName; }
            set
            {
                if (_rackStatusSlotStatusName != value)
                {
                    _rackStatusSlotStatusName = value;
                    RaisePropertyChanged(nameof(RackStatusSlotStatusName));
                }
            }
        }
        
        private Brush _rackStatusSlotStatusColor = Brushes.Transparent;
        public Brush RackStatusSlotStatusColor
        {
            get { return _rackStatusSlotStatusColor; }
            set
            {
                if (_rackStatusSlotStatusColor != value)
                {
                    _rackStatusSlotStatusColor = value;
                    RaisePropertyChanged(nameof(RackStatusSlotStatusColor));
                }
            }
        }
        #endregion
    }
}