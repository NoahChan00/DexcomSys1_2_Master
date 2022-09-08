using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace PentagonHMI
{
    public class IOModel : ViewModelBase
    {
        #region PublicProperties

        private string _iOName = string.Empty;

        public string IOName
        {
            get { return _iOName; }
            set
            {
                if(_iOName != value)
                {
                    _iOName = value;
                    RaisePropertyChanged(nameof(IOName));
                }
            }
        }

        private bool _iOEnable = false;

        public bool IOEnable
        {
            get { return _iOEnable; }
            set
            {
                if(_iOEnable != value)
                {
                    _iOEnable = value;
                    RaisePropertyChanged(nameof(IOEnable));
                }
            }
        }

        private ObservableCollection<IOTagModel> _iOTagList = new ObservableCollection<IOTagModel>();

        public ObservableCollection<IOTagModel> IOTagList
        {
            get { return _iOTagList; }
            set
            {
                if(_iOTagList != value)
                {
                    _iOTagList = value;
                    RaisePropertyChanged(nameof(IOTagList));
                }
            }
        }

        #endregion PublicProperties
    }
}