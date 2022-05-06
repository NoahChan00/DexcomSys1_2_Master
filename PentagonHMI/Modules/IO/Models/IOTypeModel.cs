using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;

namespace PentagonHMI
{
    public class IOTypeModel : ViewModelBase
    {
        #region PublicProperties
        private string _iOTypeName = string.Empty;
        public string IOTypeName
        {
            get { return _iOTypeName; }
            set
            {
                if (_iOTypeName != value)
                {
                    _iOTypeName = value;
                    RaisePropertyChanged(nameof(IOTypeName));
                }
            }
        }

        private ObservableCollection<IOIndexModel> _iOIndexList = new ObservableCollection<IOIndexModel>();
        public ObservableCollection<IOIndexModel> IOIndexList
        {
            get { return _iOIndexList; }
            set
            {
                if (_iOIndexList != value)
                {
                    _iOIndexList = value;
                    RaisePropertyChanged(nameof(IOIndexList));
                }
            }
        }
        #endregion
    }
}