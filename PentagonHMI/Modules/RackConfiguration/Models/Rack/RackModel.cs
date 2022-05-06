using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class RackModel : ViewModelBase
    {
        #region PublicProperties
        private string rackName = string.Empty;
        public string RackName
        {
            get { return rackName; }
            set
            {
                if (rackName != value)
                {
                    rackName = value;
                    RaisePropertyChanged(nameof(RackName));
                }
            }
        }

        private List<RackTextModel> rackTextList = new List<RackTextModel>();
        public List<RackTextModel> RackTextList
        {
            get { return rackTextList; }
            set
            {
                if (rackTextList != value)
                {
                    rackTextList = value;
                    RaisePropertyChanged(nameof(RackTextList));
                    RaisePropertyChanged(nameof(RackTextListView));
                }
            }
        }
        
        private ICollectionView rackTextListView = null;
        public ICollectionView RackTextListView
        {
            get
            {
                rackTextListView = null;
                rackTextListView = CollectionViewSource.GetDefaultView(RackTextList);
                return rackTextListView;
            }
        }

        private List<RackToggleModel> rackToggleList = new List<RackToggleModel>();
        public List<RackToggleModel> RackToggleList
        {
            get { return rackToggleList; }
            set
            {
                if (rackToggleList != value)
                {
                    rackToggleList = value;
                    RaisePropertyChanged(nameof(RackToggleList));
                    RaisePropertyChanged(nameof(RackToggleListView));
                }
            }
        }

        private ICollectionView rackToggleListView = null;
        public ICollectionView RackToggleListView
        {
            get
            {
                rackToggleListView = null;
                rackToggleListView = CollectionViewSource.GetDefaultView(RackToggleList);
                return rackToggleListView;
            }
        }
        #endregion
    }
}