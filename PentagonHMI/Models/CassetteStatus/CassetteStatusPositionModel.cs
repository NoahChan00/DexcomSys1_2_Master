using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI.Models
{
    public class CassetteStatusPositionModel : ViewModelBase
    {
        #region PublicProperties
        private int position = 0;
        public int Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    RaisePropertyChanged(nameof(Position));
                }
            }
        }
        
        private List<CassetteStatusSubStatusModel> cassetteSubStatusList = new List<CassetteStatusSubStatusModel>();
        public List<CassetteStatusSubStatusModel> CassetteSubStatusList
        {
            get { return cassetteSubStatusList; }
            set
            {
                if (cassetteSubStatusList != value)
                {
                    cassetteSubStatusList = value;
                    RaisePropertyChanged(nameof(CassetteSubStatusList));
                    RaisePropertyChanged(nameof(CassetteSubStatusListView));
                }
            }
        }
        
        private ICollectionView cassetteSubStatusListView = null;
        public ICollectionView CassetteSubStatusListView
        {
            get
            {
                cassetteSubStatusListView = null;
                cassetteSubStatusListView = CollectionViewSource.GetDefaultView(CassetteSubStatusList);
                return cassetteSubStatusListView;
            }
        }
        #endregion
    }
}