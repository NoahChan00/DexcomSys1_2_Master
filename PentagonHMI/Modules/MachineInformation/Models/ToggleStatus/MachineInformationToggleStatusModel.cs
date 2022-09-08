using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class MachineInformationToggleStatusModel : ViewModelBase
    {
        #region PublicFields

        public Tag MachineInformationToggleStatusTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string machineInformationToggleStatusName = string.Empty;

        public string MachineInformationToggleStatusName
        {
            get { return machineInformationToggleStatusName; }
            set
            {
                if(machineInformationToggleStatusName != value)
                {
                    machineInformationToggleStatusName = value;
                    RaisePropertyChanged(nameof(MachineInformationToggleStatusName));
                }
            }
        }

        private bool machineInformationToggleStatus = false;

        public bool MachineInformationToggleStatus
        {
            get { return machineInformationToggleStatus; }
            set
            {
                if(machineInformationToggleStatus != value)
                {
                    machineInformationToggleStatus = value;
                    RaisePropertyChanged(nameof(MachineInformationToggleStatus));
                }
            }
        }

        #endregion PublicProperties
    }
}