using GalaSoft.MvvmLight;

namespace PentagonHMI
{
    public class RecipeConfigurationMPNModel : ViewModelBase
    {
        #region PublicProperties

        private string mPN = string.Empty;

        public string MPN
        {
            get { return mPN; }
            set
            {
                if(mPN != value)
                {
                    mPN = value;
                    RaisePropertyChanged(nameof(MPN));
                }
            }
        }

        #endregion PublicProperties
    }
}