using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class ZoneActionModel : ViewModelBase
    {
        #region PublicFields

        public Tag ZoneActionTag = new Tag();

        public Tag ZoneActionEnable1Tag = new Tag();

        public Tag ZoneActionEnable2Tag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string zoneActionName = string.Empty;

        public string ZoneActionName
        {
            get { return zoneActionName; }
            set
            {
                if(zoneActionName != value)
                {
                    zoneActionName = value;
                    RaisePropertyChanged(nameof(ZoneActionName));
                }
            }
        }

        private bool zoneActionEnable = true;

        public bool ZoneActionEnable
        {
            get { return zoneActionEnable; }
            set
            {
                if(zoneActionEnable != value)
                {
                    zoneActionEnable = value;
                    RaisePropertyChanged(nameof(ZoneActionEnable));
                }
            }
        }

        #endregion PublicProperties
    }
}