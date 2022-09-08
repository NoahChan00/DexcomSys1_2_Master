using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace PentagonHMI.Models
{
    public class CassetteStatusCassetteModel : ViewModelBase
    {
        #region PublicCommandProperties

        public ICommand ScrollChangedCommand { get; set; }

        #endregion PublicCommandProperties

        #region PublicProperties

        private string cassetteName = string.Empty;

        public string CassetteName
        {
            get { return cassetteName; }
            set
            {
                if(cassetteName != value)
                {
                    cassetteName = value;
                    RaisePropertyChanged(nameof(CassetteName));
                }
            }
        }

        private List<CassetteStatusPositionModel> positionList = new List<CassetteStatusPositionModel>();

        public List<CassetteStatusPositionModel> PositionList
        {
            get { return positionList; }
            set
            {
                if(positionList != value)
                {
                    positionList = value;
                    RaisePropertyChanged(nameof(PositionList));
                    RaisePropertyChanged(nameof(PositionListView));
                }
            }
        }

        private ICollectionView positionListView = null;

        public ICollectionView PositionListView
        {
            get
            {
                positionListView = null;
                positionListView = CollectionViewSource.GetDefaultView(PositionList);
                return positionListView;
            }
        }

        #endregion PublicProperties
    }
}