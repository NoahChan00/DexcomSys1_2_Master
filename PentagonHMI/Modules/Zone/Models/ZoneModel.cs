using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class ZoneModel : ViewModelBase
    {
        #region PublicProperties

        private string zoneName = string.Empty;

        public string ZoneName
        {
            get { return zoneName; }
            set
            {
                if(zoneName != value)
                {
                    zoneName = value;
                    RaisePropertyChanged(nameof(ZoneName));
                }
            }
        }

        private List<ZoneStatusModel> zoneStatusList = new List<ZoneStatusModel>();

        public List<ZoneStatusModel> ZoneStatusList
        {
            get { return zoneStatusList; }
            set
            {
                if(zoneStatusList != value)
                {
                    zoneStatusList = value;
                    RaisePropertyChanged(nameof(ZoneStatusList));
                    RaisePropertyChanged(nameof(ZoneStatusListView));
                }
            }
        }

        private ICollectionView zoneStatusListView = null;

        public ICollectionView ZoneStatusListView
        {
            get
            {
                zoneStatusListView = null;
                zoneStatusListView = CollectionViewSource.GetDefaultView(ZoneStatusList);
                return zoneStatusListView;
            }
        }

        private List<ZoneActionModel> zoneActionList = new List<ZoneActionModel>();

        public List<ZoneActionModel> ZoneActionList
        {
            get { return zoneActionList; }
            set
            {
                if(zoneActionList != value)
                {
                    zoneActionList = value;
                    RaisePropertyChanged(nameof(ZoneActionList));
                    RaisePropertyChanged(nameof(ZoneActionListView));
                }
            }
        }

        private ICollectionView zoneActionListView = null;

        public ICollectionView ZoneActionListView
        {
            get
            {
                zoneActionListView = null;
                zoneActionListView = CollectionViewSource.GetDefaultView(ZoneActionList);
                return zoneActionListView;
            }
        }

        #endregion PublicProperties
    }
}