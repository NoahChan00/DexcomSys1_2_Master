using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class RejectBinDisplayModel : ViewModelBase
    {
        #region PublicProperties
        private List<RejectBinDisplayStatusModel> rejectBinDisplayStatusList = new List<RejectBinDisplayStatusModel>();
        public List<RejectBinDisplayStatusModel> RejectBinDisplayStatusList
        {
            get { return rejectBinDisplayStatusList; }
            set
            {
                if (rejectBinDisplayStatusList != value)
                {
                    rejectBinDisplayStatusList = value;
                    RaisePropertyChanged(nameof(RejectBinDisplayStatusList));
                    RaisePropertyChanged(nameof(RejectBinDisplayStatusListView));
                }
            }
        }

        private ICollectionView rejectBinDisplayStatusListView = null;
        public ICollectionView RejectBinDisplayStatusListView
        {
            get
            {
                rejectBinDisplayStatusListView = null;
                rejectBinDisplayStatusListView = CollectionViewSource.GetDefaultView(RejectBinDisplayStatusList);
                return rejectBinDisplayStatusListView;
            }
        }
        #endregion
    }
}