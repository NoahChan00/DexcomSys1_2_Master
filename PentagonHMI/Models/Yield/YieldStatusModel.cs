using GalaSoft.MvvmLight;

namespace PentagonHMI.Models
{
    public class YieldStatusModel : ViewModelBase
    {
        #region PublicProperties
        private string yieldStatusName = string.Empty;
        public string YieldStatusName
        {
            get { return yieldStatusName; }
            set
            {
                if (yieldStatusName != value)
                {
                    yieldStatusName = value;
                    RaisePropertyChanged(nameof(YieldStatusName));
                }
            }
        }

        private string yieldStatus = string.Empty;
        public string YieldStatus
        {
            get { return yieldStatus; }
            set
            {
                if (yieldStatus != value)
                {
                    yieldStatus = value;
                    RaisePropertyChanged(nameof(YieldStatus));
                }
            }
        }
        #endregion
    }
}