using GalaSoft.MvvmLight;

namespace PentagonHMI
{
    public class MachineInformationTextDescriptionModel : ViewModelBase
    {
        #region PublicProperties

        private string machineInformationTextIndex = string.Empty;

        public string MachineInformationTextIndex
        {
            get { return machineInformationTextIndex; }
            set
            {
                if(machineInformationTextIndex != value)
                {
                    machineInformationTextIndex = value;
                    RaisePropertyChanged(nameof(MachineInformationTextIndex));
                }
            }
        }

        private string machineInformationTextDescription = string.Empty;

        public string MachineInformationTextDescription
        {
            get { return machineInformationTextDescription; }
            set
            {
                if(machineInformationTextDescription != value)
                {
                    machineInformationTextDescription = value;
                    RaisePropertyChanged(nameof(MachineInformationTextDescription));
                }
            }
        }

        #endregion PublicProperties
    }
}