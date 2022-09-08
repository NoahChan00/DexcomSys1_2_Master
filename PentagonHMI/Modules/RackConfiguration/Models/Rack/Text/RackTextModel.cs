using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class RackTextModel : ViewModelBase
    {
        #region PublicFields

        public Tag RackTextTag = new Tag();

        public Tag RackTextEnableTag = new Tag();

        #endregion PublicFields

        #region PublicProperties

        private string rackTextName = string.Empty;

        public string RackTextName
        {
            get { return rackTextName; }
            set
            {
                if(rackTextName != value)
                {
                    rackTextName = value;
                    RaisePropertyChanged(nameof(RackTextName));
                }
            }
        }

        private bool rackTextEnable = false;

        public bool RackTextEnable
        {
            get { return rackTextEnable; }
            set
            {
                if(rackTextEnable != value)
                {
                    rackTextEnable = value;
                    RaisePropertyChanged(nameof(RackTextEnable));
                }
            }
        }

        private string rackText = string.Empty;

        public string RackText
        {
            get { return rackText; }
            set
            {
                if(rackText != value)
                {
                    rackText = value;
                    RaisePropertyChanged(nameof(RackText));
                }
            }
        }

        #endregion PublicProperties
    }
}