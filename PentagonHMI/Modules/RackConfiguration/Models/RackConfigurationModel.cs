using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class RackConfigurationModel : ViewModelBase
    {
        #region PublicProperties

        private List<RackModel> rackList = new List<RackModel>();

        public List<RackModel> RackList
        {
            get { return rackList; }
            set
            {
                if(rackList != value)
                {
                    rackList = value;
                    RaisePropertyChanged(nameof(RackList));
                    RaisePropertyChanged(nameof(RackListView));
                }
            }
        }

        private ICollectionView rackListView = null;

        public ICollectionView RackListView
        {
            get
            {
                rackListView = null;
                rackListView = CollectionViewSource.GetDefaultView(RackList);
                return rackListView;
            }
        }

        #endregion PublicProperties
    }
}