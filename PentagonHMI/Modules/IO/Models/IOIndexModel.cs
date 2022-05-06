using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace PentagonHMI
{
    public class IOIndexModel : ViewModelBase
    {
        #region PublicProperties
        private string _iOIndexName = string.Empty;
        public string IOIndexName
        {
            get { return _iOIndexName; }
            set
            {
                if (_iOIndexName != value)
                {
                    _iOIndexName = value;
                    RaisePropertyChanged(nameof(IOIndexName));
                }
            }
        }

        private ObservableCollection<IOModel> _iOList = new ObservableCollection<IOModel>();
        public ObservableCollection<IOModel> IOList
        {
            get { return _iOList; }
            set
            {
                if (_iOList != value)
                {
                    _iOList = value;
                    RaisePropertyChanged(nameof(IOList));
                }
            }
        }
        #endregion
    }
}