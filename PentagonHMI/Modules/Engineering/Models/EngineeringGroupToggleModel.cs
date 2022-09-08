using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class EngineeringGroupToggleModel : ViewModelBase
    {
        #region PublicFields

        public Tag EngineeringGroupToggleTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string engineeringGroupToggleName = string.Empty;

        public string EngineeringGroupToggleName
        {
            get { return engineeringGroupToggleName; }
            set
            {
                if(engineeringGroupToggleName != value)
                {
                    engineeringGroupToggleName = value;
                    RaisePropertyChanged(nameof(EngineeringGroupToggleName));
                }
            }
        }

        private bool engineeringGroupToggle = false;

        public bool EngineeringGroupToggle
        {
            get { return engineeringGroupToggle; }
            set
            {
                if(engineeringGroupToggle != value)
                {
                    engineeringGroupToggle = value;
                    RaisePropertyChanged(nameof(EngineeringGroupToggle));
                }
            }
        }

        #endregion PublicProperties
    }
}