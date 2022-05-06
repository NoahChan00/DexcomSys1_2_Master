using GalaSoft.MvvmLight;

namespace PentagonHMI
{
    public class RecipeConfigurationModel : ViewModelBase
    {
        #region PublicProperties
        private string recipeName = string.Empty;
        public string RecipeName
        {
            get { return recipeName; }
            set
            {
                if (recipeName != value)
                {
                    recipeName = value;
                    RaisePropertyChanged(nameof(RecipeName));
                }
            }
        }

        private int mPNMaxCount = 0;
        public int MPNMaxCount
        {
            get { return mPNMaxCount; }
            set
            {
                if (mPNMaxCount != value)
                {
                    mPNMaxCount = value;
                    RaisePropertyChanged(nameof(MPNMaxCount));
                }
            }
        }

        private string trayImage = string.Empty;
        public string TrayImage
        {
            get { return trayImage; }
            set
            {
                if (trayImage != value)
                {
                    trayImage = value;
                    RaisePropertyChanged(nameof(TrayImage));
                }
            }
        }
        #endregion
    }
}