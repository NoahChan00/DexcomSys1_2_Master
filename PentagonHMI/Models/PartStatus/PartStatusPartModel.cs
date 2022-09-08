using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Media;

namespace PentagonHMI.Models
{
    public class PartStatusPartModel : ViewModelBase
    {
        #region PublicProperties

        private int partIndex = 0;

        public int PartIndex
        {
            get { return partIndex; }
            set
            {
                if(partIndex != value)
                {
                    partIndex = value;
                    RaisePropertyChanged(nameof(PartIndex));
                }
            }
        }

        private string partCount = string.Empty;

        public string PartCount
        {
            get { return partCount; }
            set
            {
                if(partCount != value)
                {
                    partCount = value;
                    RaisePropertyChanged(nameof(PartCount));
                }
            }
        }

        private Brush partStatus = Brushes.Transparent;

        public Brush PartStatus
        {
            get { return partStatus; }
            set
            {
                if(partStatus != value)
                {
                    partStatus = value;
                    RaisePropertyChanged(nameof(PartStatus));
                }
            }
        }

        private List<PartStatusSubStatusModel> partSubStatusList = new List<PartStatusSubStatusModel>();

        public List<PartStatusSubStatusModel> PartSubStatusList
        {
            get { return partSubStatusList; }
            set
            {
                if(partSubStatusList != value)
                {
                    partSubStatusList = value;
                    RaisePropertyChanged(nameof(PartSubStatusList));
                    RaisePropertyChanged(nameof(PartSubStatusListView));
                }
            }
        }

        private ICollectionView partSubStatusListView = null;

        public ICollectionView PartSubStatusListView
        {
            get
            {
                partSubStatusListView = null;
                partSubStatusListView = CollectionViewSource.GetDefaultView(PartSubStatusList);
                return partSubStatusListView;
            }
        }

        #endregion PublicProperties
    }
}