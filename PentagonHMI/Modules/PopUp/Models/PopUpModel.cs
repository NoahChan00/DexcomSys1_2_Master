using GalaSoft.MvvmLight;
using Logix;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class PopUpModel : ViewModelBase
    {
        #region PublicFields

        public Tag PopUpTag = new Tag();

        public Tag PopUpMessageTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string popUpMessage = string.Empty;

        public string PopUpMessage
        {
            get { return popUpMessage; }
            set
            {
                if(popUpMessage != value)
                {
                    popUpMessage = value;
                    RaisePropertyChanged(nameof(PopUpMessage));
                }
            }
        }

        private List<PopUpActionModel> popUpActionList = new List<PopUpActionModel>();

        public List<PopUpActionModel> PopUpActionList
        {
            get { return popUpActionList; }
            set
            {
                if(popUpActionList != value)
                {
                    popUpActionList = value;
                    RaisePropertyChanged(nameof(PopUpActionList));
                    RaisePropertyChanged(nameof(PopUpActionListView));
                }
            }
        }

        private ICollectionView popUpActionListView = null;

        public ICollectionView PopUpActionListView
        {
            get
            {
                popUpActionListView = null;
                popUpActionListView = CollectionViewSource.GetDefaultView(PopUpActionList);
                return popUpActionListView;
            }
        }

        private List<PopUpMessageDescriptionModel> popUpMessageDescriptionList = new List<PopUpMessageDescriptionModel>();

        public List<PopUpMessageDescriptionModel> PopUpMessageDescriptionList
        {
            get { return popUpMessageDescriptionList; }
            set
            {
                if(popUpMessageDescriptionList != value)
                {
                    popUpMessageDescriptionList = value;
                    RaisePropertyChanged(nameof(PopUpMessageDescriptionList));
                    RaisePropertyChanged(nameof(PopUpMessageDescriptionListView));
                }
            }
        }

        private ICollectionView popUpMessageDescriptionListView = null;

        public ICollectionView PopUpMessageDescriptionListView
        {
            get
            {
                popUpMessageDescriptionListView = null;
                popUpMessageDescriptionListView = CollectionViewSource.GetDefaultView(PopUpMessageDescriptionList);
                return popUpMessageDescriptionListView;
            }
        }

        #endregion PublicProperties
    }
}