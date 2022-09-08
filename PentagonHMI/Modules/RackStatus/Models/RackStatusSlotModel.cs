using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class RackStatusSlotModel : ViewModelBase
    {
        #region PublicFields

        public Tag RackStatusSlotStatusTag = new Tag();
        public Tag RackStatusSlotSelectedTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string _rackStatusSlotName = string.Empty;

        public string RackStatusSlotName
        {
            get { return _rackStatusSlotName; }
            set
            {
                if(_rackStatusSlotName != value)
                {
                    _rackStatusSlotName = value;
                    RaisePropertyChanged(nameof(RackStatusSlotName));
                }
            }
        }

        private RackStatusSlotStatusModel _rackStatusSlotStatusModel = new RackStatusSlotStatusModel();

        public RackStatusSlotStatusModel RackStatusSlotStatusModel
        {
            get { return _rackStatusSlotStatusModel; }
            set
            {
                if(_rackStatusSlotStatusModel != value)
                {
                    _rackStatusSlotStatusModel = value;
                    RaisePropertyChanged(nameof(RackStatusSlotStatusModel));
                }
            }
        }

        #endregion PublicProperties
    }
}