using GalaSoft.MvvmLight;
using Logix;
using System.Windows.Media;

namespace PentagonHMI
{
    public class ControlPanelToggleModel : ViewModelBase
    {
        #region PublicFields
        public Tag ControlPanelToggleTag = new Tag();

        public Tag ControlPanelToggleEnableTag = new Tag();
        #endregion

        #region PublicProperties
        private Brush controlPanelToggleStatusOffColor = Brushes.Silver;
        public Brush ControlPanelToggleStatusOffColor
        {
            get { return controlPanelToggleStatusOffColor; }
            set
            {
                if (controlPanelToggleStatusOffColor != value)
                {
                    controlPanelToggleStatusOffColor = value;
                    RaisePropertyChanged(nameof(ControlPanelToggleStatusOffColor));
                }
            }
        }

        private Brush controlPanelToggleStatusOnColor = Brushes.Silver;
        public Brush ControlPanelToggleStatusOnColor
        {
            get { return controlPanelToggleStatusOnColor; }
            set
            {
                if (controlPanelToggleStatusOnColor != value)
                {
                    controlPanelToggleStatusOnColor = value;
                    RaisePropertyChanged(nameof(ControlPanelToggleStatusOnColor));
                }
            }
        }
        
        private string controlPanelToggleName = string.Empty;
        public string ControlPanelToggleName
        {
            get { return controlPanelToggleName; }
            set
            {
                if (controlPanelToggleName != value)
                {
                    controlPanelToggleName = value;
                    RaisePropertyChanged(nameof(ControlPanelToggleName));
                }
            }
        }

        private bool controlPanelToggleEnable = true;
        public bool ControlPanelToggleEnable
        {
            get { return controlPanelToggleEnable; }
            set
            {
                if (controlPanelToggleEnable != value)
                {
                    controlPanelToggleEnable = value;
                    RaisePropertyChanged(nameof(ControlPanelToggleEnable));
                }
            }
        }

        private bool controlPanelToggleStatus = false;
        public bool ControlPanelToggleStatus
        {
            get { return controlPanelToggleStatus; }
            set
            {
                if (controlPanelToggleStatus != value)
                {
                    controlPanelToggleStatus = value;
                    RaisePropertyChanged(nameof(ControlPanelToggleStatus));
                }
            }
        }
        #endregion
    }
}