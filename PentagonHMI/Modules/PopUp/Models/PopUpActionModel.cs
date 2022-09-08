using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class PopUpActionModel : ViewModelBase
    {
        #region PublicFields

        public Tag PopUpActionTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string popUpActionName = string.Empty;

        public string PopUpActionName
        {
            get { return popUpActionName; }
            set
            {
                if(popUpActionName != value)
                {
                    popUpActionName = value;
                    RaisePropertyChanged(nameof(PopUpActionName));
                }
            }
        }

        #endregion PublicProperties
    }
}