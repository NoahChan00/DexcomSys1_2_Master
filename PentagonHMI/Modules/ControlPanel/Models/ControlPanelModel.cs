using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class ControlPanelModel : ViewModelBase
    {
        #region PublicProperties

        private List<ControlPanelActionModel> controlPanelActionList = new List<ControlPanelActionModel>();

        public List<ControlPanelActionModel> ControlPanelActionList
        {
            get { return controlPanelActionList; }
            set
            {
                if(controlPanelActionList != value)
                {
                    controlPanelActionList = value;
                    RaisePropertyChanged(nameof(ControlPanelActionList));
                    RaisePropertyChanged(nameof(ControlPanelActionListView));
                }
            }
        }

        private ICollectionView controlPanelActionListView = null;

        public ICollectionView ControlPanelActionListView
        {
            get
            {
                controlPanelActionListView = null;
                controlPanelActionListView = CollectionViewSource.GetDefaultView(ControlPanelActionList);
                return controlPanelActionListView;
            }
        }

        private List<ControlPanelToggleModel> controlPanelToggleList = new List<ControlPanelToggleModel>();

        public List<ControlPanelToggleModel> ControlPanelToggleList
        {
            get { return controlPanelToggleList; }
            set
            {
                if(controlPanelToggleList != value)
                {
                    controlPanelToggleList = value;
                    RaisePropertyChanged(nameof(ControlPanelToggleList));
                    RaisePropertyChanged(nameof(ControlPanelToggleListView));
                }
            }
        }

        private ICollectionView controlPanelToggleListView = null;

        public ICollectionView ControlPanelToggleListView
        {
            get
            {
                controlPanelToggleListView = null;
                controlPanelToggleListView = CollectionViewSource.GetDefaultView(ControlPanelToggleList);
                return controlPanelToggleListView;
            }
        }

        private int Ufg_clm = Classes.GlobalFunctions.ProjectType == ProjectType.HDD ? 3 : 2;

        public int ufg_clm
        {
            get { return Ufg_clm; }
            set
            {
                if(Ufg_clm != value)
                {
                    Ufg_clm = value;
                    RaisePropertyChanged(nameof(ufg_clm));
                }
            }
        }

        #endregion PublicProperties
    }
}