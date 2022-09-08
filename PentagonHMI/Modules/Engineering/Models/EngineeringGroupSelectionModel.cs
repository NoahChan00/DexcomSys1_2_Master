using GalaSoft.MvvmLight;
using Logix;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class EngineeringGroupSelectionModel : ViewModelBase
    {
        #region PublicFields

        public Tag EngineeringGroupSelectionTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string engineeringGroupSelectionName = string.Empty;

        public string EngineeringGroupSelectionName
        {
            get { return engineeringGroupSelectionName; }
            set
            {
                if(engineeringGroupSelectionName != value)
                {
                    engineeringGroupSelectionName = value;
                    RaisePropertyChanged(nameof(EngineeringGroupSelectionName));
                }
            }
        }

        private int engineeringGroupSelectionSelectedIndex = 0;

        public int EngineeringGroupSelectionSelectedIndex
        {
            get { return engineeringGroupSelectionSelectedIndex; }
            set
            {
                if(engineeringGroupSelectionSelectedIndex != value)
                {
                    engineeringGroupSelectionSelectedIndex = value;
                    RaisePropertyChanged(nameof(EngineeringGroupSelectionSelectedIndex));
                }
            }
        }

        private List<string> engineeringGroupSelectionItemList = new List<string>();

        public List<string> EngineeringGroupSelectionItemList
        {
            get { return engineeringGroupSelectionItemList; }
            set
            {
                if(engineeringGroupSelectionItemList != value)
                {
                    engineeringGroupSelectionItemList = value;
                    RaisePropertyChanged(nameof(EngineeringGroupSelectionItemList));
                    RaisePropertyChanged(nameof(EngineeringGroupSelectionItemListView));
                }
            }
        }

        private ICollectionView engineeringGroupSelectionItemListView = null;

        public ICollectionView EngineeringGroupSelectionItemListView
        {
            get
            {
                engineeringGroupSelectionItemListView = null;
                engineeringGroupSelectionItemListView = CollectionViewSource.GetDefaultView(EngineeringGroupSelectionItemList);
                return engineeringGroupSelectionItemListView;
            }
        }

        #endregion PublicProperties
    }
}