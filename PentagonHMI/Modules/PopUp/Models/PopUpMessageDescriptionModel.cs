using GalaSoft.MvvmLight;

namespace PentagonHMI
{
    public class PopUpMessageDescriptionModel : ViewModelBase
    {
        #region PublicProperties

        private string popUpMessageIndex = string.Empty;

        public string PopUpMessageIndex
        {
            get { return popUpMessageIndex; }
            set
            {
                if(popUpMessageIndex != value)
                {
                    popUpMessageIndex = value;
                    RaisePropertyChanged(nameof(PopUpMessageIndex));
                }
            }
        }

        private string popUpMessageDescription = string.Empty;

        public string PopUpMessageDescription
        {
            get { return popUpMessageDescription; }
            set
            {
                if(popUpMessageDescription != value)
                {
                    popUpMessageDescription = value;
                    RaisePropertyChanged(nameof(PopUpMessageDescription));
                }
            }
        }

        #endregion PublicProperties
    }
}