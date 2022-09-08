using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class RejectBinDisplayStatusModel : ViewModelBase
    {
        #region PublicFields

        public Tag RejectBinDisplayStatusTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string rejectBinDisplayStatusName = string.Empty;

        public string RejectBinDisplayStatusName
        {
            get { return rejectBinDisplayStatusName; }
            set
            {
                if(rejectBinDisplayStatusName != value)
                {
                    rejectBinDisplayStatusName = value;
                    RaisePropertyChanged(nameof(RejectBinDisplayStatusName));
                }
            }
        }

        private string rejectBinDisplayStatus = string.Empty;

        public string RejectBinDisplayStatus
        {
            get { return rejectBinDisplayStatus; }
            set
            {
                if(rejectBinDisplayStatus != value)
                {
                    rejectBinDisplayStatus = value;
                    RaisePropertyChanged(nameof(RejectBinDisplayStatus));
                }
            }
        }

        #endregion PublicProperties
    }
}