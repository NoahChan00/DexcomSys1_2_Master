using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI.Models
{
    public class PartStatusZoneModel : ViewModelBase
    {
        #region PublicProperties
        private string zoneName = string.Empty;
        public string ZoneName
        {
            get { return zoneName; }
            set
            {
                if (zoneName != value)
                {
                    zoneName = value;
                    RaisePropertyChanged(nameof(ZoneName));
                }
            }
        }

        private bool enableManualAudit = false;
        public bool EnableManualAudit
        {
            get { return enableManualAudit; }
            set
            {
                if (enableManualAudit != value)
                {
                    enableManualAudit = value;
                    RaisePropertyChanged(nameof(EnableManualAudit));
                }
            }
        }
        
        private List<PartStatusPartModel> partList = new List<PartStatusPartModel>();
        public List<PartStatusPartModel> PartList
        {
            get { return partList; }
            set
            {
                if (partList != value)
                {
                    partList = value;
                    RaisePropertyChanged(nameof(PartList));
                    RaisePropertyChanged(nameof(PartListView));
                }
            }
        }

        private ICollectionView partListView = null;
        public ICollectionView PartListView
        {
            get
            {
                partListView = null;
                partListView = CollectionViewSource.GetDefaultView(PartList);
                return partListView;
            }
        }
        #endregion
    }
}