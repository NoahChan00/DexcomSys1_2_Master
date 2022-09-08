using GalaSoft.MvvmLight;

namespace PentagonHMI
{
    public class RejectReason : ViewModelBase
    {
        #region PublicProperties

        private string _rejectCode = string.Empty;

        public string RejectCode
        {
            get { return _rejectCode; }
            set
            {
                if(_rejectCode != value)
                {
                    _rejectCode = value;
                    RaisePropertyChanged(nameof(RejectCode));
                }
            }
        }

        private string _description = string.Empty;

        public string Description
        {
            get { return _description; }
            set
            {
                if(_description != value)
                {
                    _description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }

        #endregion PublicProperties
    }
}