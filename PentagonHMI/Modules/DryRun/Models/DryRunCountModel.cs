using GalaSoft.MvvmLight;

namespace PentagonHMI
{
    public class DryRunCountModel : ViewModelBase
    {
        #region PublicProperties
        private string _dryRunCountName = string.Empty;
        public string DryRunCountName
        {
            get { return _dryRunCountName; }
            set
            {
                if (_dryRunCountName != value)
                {
                    _dryRunCountName = value;
                    RaisePropertyChanged(nameof(DryRunCountName));
                }
            }
        }

        private int _dryRunCount = 0;
        public int DryRunCount
        {
            get { return _dryRunCount; }
            set
            {
                if (_dryRunCount != value)
                {
                    _dryRunCount = value;
                    RaisePropertyChanged(nameof(DryRunCount));
                }
            }
        }

        private string _dryRunCountTagKey = string.Empty;
        public string DryRunCountTagKey
        {
            get { return _dryRunCountTagKey; }
            set
            {
                if (_dryRunCountTagKey != value)
                {
                    _dryRunCountTagKey = value;
                    RaisePropertyChanged(nameof(DryRunCountTagKey));
                }
            }
        }
        #endregion
    }
}