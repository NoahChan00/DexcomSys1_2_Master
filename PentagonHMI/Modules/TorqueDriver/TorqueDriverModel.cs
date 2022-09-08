using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class TorqueDriverModel : ViewModelBase
    {
        #region PublicFields

        public Tag TriggerTorqueDriverResultTagName = new Tag();
        public Tag AssetTagTagName = new Tag();
        public Tag ZoneTagName = new Tag();
        public Tag TorqueDriverTcpDisconnectedTagName = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private bool _triggerTorqueDriver = false;

        public bool TriggerTorqueDriver
        {
            get { return _triggerTorqueDriver; }
            set
            {
                if(_triggerTorqueDriver != value)
                {
                    _triggerTorqueDriver = value;
                    RaisePropertyChanged(nameof(TriggerTorqueDriver));
                }
            }
        }

        #endregion PublicProperties
    }
}