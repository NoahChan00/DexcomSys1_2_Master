using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class EngineeringGroupTextModel : ViewModelBase
    {
        #region PublicFields

        public Tag EngineeringGroupTextTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string engineeringTextName = string.Empty;

        public string EngineeringTextName
        {
            get { return engineeringTextName; }
            set
            {
                if(engineeringTextName != value)
                {
                    engineeringTextName = value;
                    RaisePropertyChanged(nameof(EngineeringTextName));
                }
            }
        }

        private string engineeringTextValue = string.Empty;

        public string EngineeringTextValue
        {
            get { return engineeringTextValue; }
            set
            {
                if(engineeringTextValue != value)
                {
                    engineeringTextValue = value;
                    RaisePropertyChanged(nameof(EngineeringTextValue));
                }
            }
        }

        #endregion PublicProperties
    }
}