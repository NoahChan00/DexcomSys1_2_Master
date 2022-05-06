using GalaSoft.MvvmLight;
using Logix;
using System.Windows.Media;

namespace PentagonHMI
{
    public class ControlPanelActionModel : ViewModelBase
    {
        #region PublicFields
        public Tag ControlPanelActionTag = new Tag();

        public Tag ControlPanelActionEnableTag = new Tag();
        
        public Tag ControlPanelActionStatusTag = new Tag();
        #endregion

        #region PublicProperties
        private Brush controlPanelActionStatusOffColor = Brushes.Silver;
        public Brush ControlPanelActionStatusOffColor
        {
            get { return controlPanelActionStatusOffColor; }
            set
            {
                if (controlPanelActionStatusOffColor != value)
                {
                    controlPanelActionStatusOffColor = value;
                    RaisePropertyChanged(nameof(ControlPanelActionStatusOffColor));
                }
            }
        }
        
        private Brush controlPanelActionStatusOnColor = Brushes.Silver;
        public Brush ControlPanelActionStatusOnColor
        {
            get { return controlPanelActionStatusOnColor; }
            set
            {
                if (controlPanelActionStatusOnColor != value)
                {
                    controlPanelActionStatusOnColor = value;
                    RaisePropertyChanged(nameof(ControlPanelActionStatusOnColor));
                }
            }
        }
        
        private string controlPanelActionName = string.Empty;
        public string ControlPanelActionName
        {
            get { return controlPanelActionName; }
            set
            {
                if (controlPanelActionName != value)
                {
                    controlPanelActionName = value;
                    RaisePropertyChanged(nameof(ControlPanelActionName));
                }
            }
        }

        private bool controlPanelActionEnable = true;
        public bool ControlPanelActionEnable
        {
            get { return controlPanelActionEnable; }
            set
            {
                if (controlPanelActionEnable != value)
                {
                    controlPanelActionEnable = value;
                    RaisePropertyChanged(nameof(ControlPanelActionEnable));
                }
            }
        }
        
        private bool controlPanelActionStatus = false;
        public bool ControlPanelActionStatus
        {
            get { return controlPanelActionStatus; }
            set
            {
                if (controlPanelActionStatus != value)
                {
                    controlPanelActionStatus = value;
                    RaisePropertyChanged(nameof(ControlPanelActionStatus));
                }
            }
        }
        #endregion
    }
}