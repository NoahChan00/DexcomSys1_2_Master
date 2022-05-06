using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class RackToggleModel : ViewModelBase
    {
        #region PublicFields
        public Tag RackToggleTag = new Tag();
        #endregion

        #region PublicProperties
        private string rackToggleName = string.Empty;
        public string RackToggleName
        {
            get { return rackToggleName; }
            set
            {
                if (rackToggleName != value)
                {
                    rackToggleName = value;
                    RaisePropertyChanged(nameof(RackToggleName));
                }
            }
        }

        private bool rackToggleStatus = false;
        public bool RackToggleStatus
        {
            get { return rackToggleStatus; }
            set
            {
                if (rackToggleStatus != value)
                {
                    rackToggleStatus = value;
                    RaisePropertyChanged(nameof(RackToggleStatus));
                }
            }
        }
        #endregion
    }
}