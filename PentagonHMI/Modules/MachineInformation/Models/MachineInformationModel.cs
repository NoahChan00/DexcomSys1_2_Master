using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class MachineInformationModel : ViewModelBase
    {
        #region PublicProperties
        private List<MachineInformationToggleStatusModel> machineInformationToggleStatusList = new List<MachineInformationToggleStatusModel>();
        public List<MachineInformationToggleStatusModel> MachineInformationToggleStatusList
        {
            get { return machineInformationToggleStatusList; }
            set
            {
                if (machineInformationToggleStatusList != value)
                {
                    machineInformationToggleStatusList = value;
                    RaisePropertyChanged(nameof(MachineInformationToggleStatusList));
                    RaisePropertyChanged(nameof(MachineInformationToggleStatusListView));
                }
            }
        }

        private ICollectionView machineInformationToggleStatusListView = null;
        public ICollectionView MachineInformationToggleStatusListView
        {
            get
            {
                machineInformationToggleStatusListView = null;
                machineInformationToggleStatusListView = CollectionViewSource.GetDefaultView(MachineInformationToggleStatusList);
                return machineInformationToggleStatusListView;
            }
        }

        private List<MachineInformationTextModel> machineInformationTextList = new List<MachineInformationTextModel>();
        public List<MachineInformationTextModel> MachineInformationTextList
        {
            get { return machineInformationTextList; }
            set
            {
                if (machineInformationTextList != value)
                {
                    machineInformationTextList = value;
                    RaisePropertyChanged(nameof(MachineInformationTextList));
                    RaisePropertyChanged(nameof(MachineInformationTextListView));
                }
            }
        }

        private ICollectionView machineInformationTextListView = null;
        public ICollectionView MachineInformationTextListView
        {
            get
            {
                machineInformationTextListView = null;
                machineInformationTextListView = CollectionViewSource.GetDefaultView(MachineInformationTextList);
                return machineInformationTextListView;
            }
        }
        #endregion
    }
}