using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class IOTagModel : ViewModelBase
    {
        #region PublicFields
        public Tag IOReadTag = new Tag();
        public Tag IOToggleTag = new Tag();
        #endregion
        
        #region PublicProperties
        private string _iOTagName = string.Empty;
        public string IOTagName
        {
            get { return _iOTagName; }
            set
            {
                if (_iOTagName != value)
                {
                    _iOTagName = value;
                    RaisePropertyChanged(nameof(IOTagName));
                }
            }
        }

        private bool _iOTagEnable = false;
        public bool IOTagEnable
        {
            get { return _iOTagEnable; }
            set
            {
                if (_iOTagEnable != value)
                {
                    _iOTagEnable = value;
                    RaisePropertyChanged(nameof(IOTagEnable));
                }
            }
        }

        private bool _iOTagStatus = false;
        public bool IOTagStatus
        {
            get { return _iOTagStatus; }
            set
            {
                if (_iOTagStatus != value)
                {
                    _iOTagStatus = value;
                    RaisePropertyChanged(nameof(IOTagStatus));
                }
            }
        }
        #endregion
    }
}