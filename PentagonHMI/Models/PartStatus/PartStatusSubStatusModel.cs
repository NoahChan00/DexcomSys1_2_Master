using GalaSoft.MvvmLight;

namespace PentagonHMI.Models
{
    public class PartStatusSubStatusModel : ViewModelBase
    {
        #region PublicProperties
        private string subStatusName = string.Empty;
        public string SubStatusName
        {
            get { return subStatusName; }
            set
            {
                if (subStatusName != value)
                {
                    subStatusName = value;
                    RaisePropertyChanged(nameof(SubStatusName));
                }
            }
        }

        private string subStatus = string.Empty;
        public string SubStatus
        {
            get { return subStatus; }
            set
            {
                if (subStatus != value)
                {
                    subStatus = value;
                    RaisePropertyChanged(nameof(SubStatus));
                }
            }
        }
        #endregion
    }
}