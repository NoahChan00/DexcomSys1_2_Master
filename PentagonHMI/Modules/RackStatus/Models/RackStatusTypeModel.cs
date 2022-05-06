using GalaSoft.MvvmLight;
using Logix;
using System.Collections.ObjectModel;

namespace PentagonHMI
{
    public class RackStatusTypeModel : ViewModelBase
    {
        #region PublicFields
        public Tag RackStatusTypeTotalSlotTag = new Tag();
        #endregion

        #region PublicProperties
        private string _rackStatusTypeName = string.Empty;
        public string RackStatusTypeName
        {
            get { return _rackStatusTypeName; }
            set
            {
                if (_rackStatusTypeName != value)
                {
                    _rackStatusTypeName = value;
                    RaisePropertyChanged(nameof(RackStatusTypeName));
                }
            }
        }
        
        private string _rackStatusSlotStatusTagKey = string.Empty;
        public string RackStatusSlotStatusTagKey
        {
            get { return _rackStatusSlotStatusTagKey; }
            set
            {
                if (_rackStatusSlotStatusTagKey != value)
                {
                    _rackStatusSlotStatusTagKey = value;
                    RaisePropertyChanged(nameof(RackStatusSlotStatusTagKey));
                }
            }
        }

        private Tag.ATOMIC _rackStatusSlotStatusTagDataType = Tag.ATOMIC.INT;
        public Tag.ATOMIC RackStatusSlotStatusTagDataType
        {
            get { return _rackStatusSlotStatusTagDataType; }
            set
            {
                if (_rackStatusSlotStatusTagDataType != value)
                {
                    _rackStatusSlotStatusTagDataType = value;
                    RaisePropertyChanged(nameof(RackStatusSlotStatusTagDataType));
                }
            }
        }
        
        private string _rackStatusSlotSelectedTagKey = string.Empty;
        public string RackStatusSlotSelectedTagKey
        {
            get { return _rackStatusSlotSelectedTagKey; }
            set
            {
                if (_rackStatusSlotSelectedTagKey != value)
                {
                    _rackStatusSlotSelectedTagKey = value;
                    RaisePropertyChanged(nameof(RackStatusSlotSelectedTagKey));
                }
            }
        }
        
        private ObservableCollection<RackStatusSlotModel> _rackStatusSlotList = new ObservableCollection<RackStatusSlotModel>();
        public ObservableCollection<RackStatusSlotModel> RackStatusSlotList
        {
            get { return _rackStatusSlotList; }
            set
            {
                if (_rackStatusSlotList != value)
                {
                    _rackStatusSlotList = value;
                    RaisePropertyChanged(nameof(RackStatusSlotList));
                }
            }
        }
        #endregion
    }
}