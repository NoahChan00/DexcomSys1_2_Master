using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class EngineeringGroupActionModel : ViewModelBase
    {
        #region PublicFields

        public Tag EngineeringGroupActionTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string engineeringGroupActionName = string.Empty;

        public string EngineeringGroupActionName
        {
            get { return engineeringGroupActionName; }
            set
            {
                if(engineeringGroupActionName != value)
                {
                    engineeringGroupActionName = value;
                    RaisePropertyChanged(nameof(EngineeringGroupActionName));
                }
            }
        }

        #endregion PublicProperties
    }
}