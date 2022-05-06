using GalaSoft.MvvmLight;

namespace PentagonHMI.Models
{
    public class RejectBinPartModel : ViewModelBase
    {
        #region PublicProperties
        private int partQuantity = 0;
        public int PartQuantity
        {
            get { return partQuantity; }
            set
            {
                if (partQuantity != value)
                {
                    partQuantity = value;
                    RaisePropertyChanged(nameof(PartQuantity));
                }
            }
        }
        
        private int partFull = 2;
        public int PartFull
        {
            get { return partFull; }
            set
            {
                if (partFull != value)
                {
                    partFull = value;
                    RaisePropertyChanged(nameof(PartFull));
                }
            }
        }
        #endregion
    }
}