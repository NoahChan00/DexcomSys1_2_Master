using GalaSoft.MvvmLight;
using Logix;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI
{
    public class MachineInformationTextModel : ViewModelBase
    {
        #region PublicFields

        public Tag MachineInformationTextTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string machineInformationTextName = string.Empty;

        public string MachineInformationTextName
        {
            get { return machineInformationTextName; }
            set
            {
                if(machineInformationTextName != value)
                {
                    machineInformationTextName = value;
                    RaisePropertyChanged(nameof(MachineInformationTextName));
                }
            }
        }

        private string machineInformationTextValue = string.Empty;

        public string MachineInformationTextValue
        {
            get { return machineInformationTextValue; }
            set
            {
                if(machineInformationTextValue != value)
                {
                    machineInformationTextValue = value;
                    RaisePropertyChanged(nameof(MachineInformationTextValue));
                }
            }
        }

        private List<MachineInformationTextDescriptionModel> machineInformationTextDescriptionList = new List<MachineInformationTextDescriptionModel>();

        public List<MachineInformationTextDescriptionModel> MachineInformationTextDescriptionList
        {
            get { return machineInformationTextDescriptionList; }
            set
            {
                if(machineInformationTextDescriptionList != value)
                {
                    machineInformationTextDescriptionList = value;
                    RaisePropertyChanged(nameof(MachineInformationTextDescriptionList));
                    RaisePropertyChanged(nameof(MachineInformationTextDescriptionListView));
                }
            }
        }

        private ICollectionView machineInformationTextDescriptionListView = null;

        public ICollectionView MachineInformationTextDescriptionListView
        {
            get
            {
                machineInformationTextDescriptionListView = null;
                machineInformationTextDescriptionListView = CollectionViewSource.GetDefaultView(MachineInformationTextDescriptionList);
                return machineInformationTextDescriptionListView;
            }
        }

        #endregion PublicProperties
    }
}