using GalaSoft.MvvmLight;

namespace PentagonHMI
{
    public class DryRunActionModel : ViewModelBase
    {
        #region PublicProperties

        private string _dryRunActionName = string.Empty;

        public string DryRunActionName
        {
            get { return _dryRunActionName; }
            set
            {
                if(_dryRunActionName != value)
                {
                    _dryRunActionName = value;
                    RaisePropertyChanged(nameof(DryRunActionName));
                }
            }
        }

        private string _dryRunActionTagKey = string.Empty;

        public string DryRunActionTagKey
        {
            get { return _dryRunActionTagKey; }
            set
            {
                if(_dryRunActionTagKey != value)
                {
                    _dryRunActionTagKey = value;
                    RaisePropertyChanged(nameof(DryRunActionTagKey));
                }
            }
        }

        private bool _dryRunActionValue = true;

        public bool DryRunActionValue
        {
            get { return _dryRunActionValue; }
            set
            {
                if(_dryRunActionValue != value)
                {
                    _dryRunActionValue = value;
                    RaisePropertyChanged(nameof(DryRunActionValue));
                }
            }
        }

        private bool _dryRunActionEnable = true;

        public bool DryRunActionEnable
        {
            get { return _dryRunActionEnable; }
            set
            {
                if(_dryRunActionEnable != value)
                {
                    _dryRunActionEnable = value;
                    RaisePropertyChanged(nameof(DryRunActionEnable));
                }
            }
        }

        private string _dryRunActionEnableTagKey = string.Empty;

        public string DryRunActionEnableTagKey
        {
            get { return _dryRunActionEnableTagKey; }
            set
            {
                if(_dryRunActionEnableTagKey != value)
                {
                    _dryRunActionEnableTagKey = value;
                    RaisePropertyChanged(nameof(DryRunActionEnableTagKey));
                }
            }
        }

        private bool _dryRunActionEnableValue = true;

        public bool DryRunActionEnableValue
        {
            get { return _dryRunActionEnableValue; }
            set
            {
                if(_dryRunActionEnableValue != value)
                {
                    _dryRunActionEnableValue = value;
                    RaisePropertyChanged(nameof(DryRunActionEnableValue));
                }
            }
        }

        #endregion PublicProperties
    }
}