using GalaSoft.MvvmLight;
using Logix;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class ZoneStatusModel : ViewModelBase
    {
        #region PublicFields

        public Tag ZoneStatusTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string zoneStatusName = string.Empty;

        public string ZoneStatusName
        {
            get { return zoneStatusName; }
            set
            {
                if(zoneStatusName != value)
                {
                    zoneStatusName = value;
                    RaisePropertyChanged(nameof(ZoneStatusName));
                }
            }
        }

        private string zoneStatus = string.Empty;

        public string ZoneStatus
        {
            get { return zoneStatus; }
            set
            {
                if(zoneStatus != value)
                {
                    zoneStatus = value;
                    RaisePropertyChanged(nameof(ZoneStatus));
                }
            }
        }

        private List<ZoneStatusDescriptionModel> zoneStatusDescriptionList = new List<ZoneStatusDescriptionModel>();

        public List<ZoneStatusDescriptionModel> ZoneStatusDescriptionList
        {
            get { return zoneStatusDescriptionList; }
            set
            {
                if(zoneStatusDescriptionList != value)
                {
                    zoneStatusDescriptionList = value;
                    RaisePropertyChanged(nameof(ZoneStatusDescriptionList));
                    RaisePropertyChanged(nameof(ZoneStatusDescriptionListView));
                }
            }
        }

        private ICollectionView zoneStatusDescriptionListView = null;

        public ICollectionView ZoneStatusDescriptionListView
        {
            get
            {
                zoneStatusDescriptionListView = null;
                zoneStatusDescriptionListView = CollectionViewSource.GetDefaultView(ZoneStatusDescriptionList);
                return zoneStatusDescriptionListView;
            }
        }

        #endregion PublicProperties
    }
}