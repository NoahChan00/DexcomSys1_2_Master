using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class EngineeringGroupModel : ViewModelBase
    {
        #region PublicProperties
        private string engineeringGroupName = string.Empty;
        public string EngineeringGroupName
        {
            get { return engineeringGroupName; }
            set
            {
                if (engineeringGroupName != value)
                {
                    engineeringGroupName = value;
                    RaisePropertyChanged(nameof(EngineeringGroupName));
                }
            }
        }

        private List<EngineeringGroupTextModel> engineeringGroupTextList = new List<EngineeringGroupTextModel>();
        public List<EngineeringGroupTextModel> EngineeringGroupTextList
        {
            get { return engineeringGroupTextList; }
            set
            {
                if (engineeringGroupTextList != value)
                {
                    engineeringGroupTextList = value;
                    RaisePropertyChanged(nameof(EngineeringGroupTextList));
                    RaisePropertyChanged(nameof(EngineeringGroupTextListView));
                }
            }
        }

        private ICollectionView engineeringGroupTextListView = null;
        public ICollectionView EngineeringGroupTextListView
        {
            get
            {
                engineeringGroupTextListView = null;
                engineeringGroupTextListView = CollectionViewSource.GetDefaultView(EngineeringGroupTextList);
                return engineeringGroupTextListView;
            }
        }
        
        private List<EngineeringGroupSelectionModel> engineeringGroupSelectionList = new List<EngineeringGroupSelectionModel>();
        public List<EngineeringGroupSelectionModel> EngineeringGroupSelectionList
        {
            get { return engineeringGroupSelectionList; }
            set
            {
                if (engineeringGroupSelectionList != value)
                {
                    engineeringGroupSelectionList = value;
                    RaisePropertyChanged(nameof(EngineeringGroupSelectionList));
                    RaisePropertyChanged(nameof(EngineeringGroupSelectionListView));
                }
            }
        }
        
        private ICollectionView engineeringGroupSelectionListView = null;
        public ICollectionView EngineeringGroupSelectionListView
        {
            get
            {
                engineeringGroupSelectionListView = null;
                engineeringGroupSelectionListView = CollectionViewSource.GetDefaultView(EngineeringGroupSelectionList);
                return engineeringGroupSelectionListView;
            }
        }
        
        private List<EngineeringGroupToggleModel> engineeringGroupToggleList = new List<EngineeringGroupToggleModel>();
        public List<EngineeringGroupToggleModel> EngineeringGroupToggleList
        {
            get { return engineeringGroupToggleList; }
            set
            {
                if (engineeringGroupToggleList != value)
                {
                    engineeringGroupToggleList = value;
                    RaisePropertyChanged(nameof(EngineeringGroupToggleList));
                    RaisePropertyChanged(nameof(EngineeringGroupToggleListView));
                }
            }
        }
        
        private ICollectionView engineeringGroupToggleListView = null;
        public ICollectionView EngineeringGroupToggleListView
        {
            get
            {
                engineeringGroupToggleListView = null;
                engineeringGroupToggleListView = CollectionViewSource.GetDefaultView(EngineeringGroupToggleList);
                return engineeringGroupToggleListView;
            }
        }

        private List<EngineeringGroupActionModel> engineeringGroupActionList = new List<EngineeringGroupActionModel>();
        public List<EngineeringGroupActionModel> EngineeringGroupActionList
        {
            get { return engineeringGroupActionList; }
            set
            {
                if (engineeringGroupActionList != value)
                {
                    engineeringGroupActionList = value;
                    RaisePropertyChanged(nameof(EngineeringGroupActionList));
                    RaisePropertyChanged(nameof(EngineeringGroupActionListView));
                }
            }
        }

        private ICollectionView engineeringGroupActionListView = null;
        public ICollectionView EngineeringGroupActionListView
        {
            get
            {
                engineeringGroupActionListView = null;
                engineeringGroupActionListView = CollectionViewSource.GetDefaultView(EngineeringGroupActionList);
                return engineeringGroupActionListView;
            }
        }
        #endregion
    }
}