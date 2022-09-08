using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI.Models
{
    public class RejectBinModel : ViewModelBase
    {
        #region PublicProperties

        private string rejectBinName = string.Empty;

        public string RejectBinName
        {
            get { return rejectBinName; }
            set
            {
                if(rejectBinName != value)
                {
                    rejectBinName = value;
                    RaisePropertyChanged(nameof(RejectBinName));
                }
            }
        }

        private List<RejectBinPartModel> rejectBinPartList = new List<RejectBinPartModel>();

        public List<RejectBinPartModel> RejectBinPartList
        {
            get { return rejectBinPartList; }
            set
            {
                if(rejectBinPartList != value)
                {
                    rejectBinPartList = value;
                    RaisePropertyChanged(nameof(RejectBinPartList));
                    RaisePropertyChanged(nameof(RejectBinPartListView));
                }
            }
        }

        private ICollectionView rejectBinPartListView = null;

        public ICollectionView RejectBinPartListView
        {
            get
            {
                rejectBinPartListView = null;
                rejectBinPartListView = CollectionViewSource.GetDefaultView(RejectBinPartList);
                return rejectBinPartListView;
            }
        }

        #endregion PublicProperties
    }
}