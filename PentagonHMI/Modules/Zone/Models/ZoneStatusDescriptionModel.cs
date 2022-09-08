using GalaSoft.MvvmLight;

namespace PentagonHMI
{
    public class ZoneStatusDescriptionModel : ViewModelBase
    {
        #region PublicProperties

        private string zoneStatusIndex = string.Empty;

        public string ZoneStatusIndex
        {
            get { return zoneStatusIndex; }
            set
            {
                if(zoneStatusIndex != value)
                {
                    zoneStatusIndex = value;
                    RaisePropertyChanged(nameof(ZoneStatusIndex));
                }
            }
        }

        private string zoneStatusDescription = string.Empty;

        public string ZoneStatusDescription
        {
            get { return zoneStatusDescription; }
            set
            {
                if(zoneStatusDescription != value)
                {
                    zoneStatusDescription = value;
                    RaisePropertyChanged(nameof(ZoneStatusDescription));
                }
            }
        }

        #endregion PublicProperties
    }
}