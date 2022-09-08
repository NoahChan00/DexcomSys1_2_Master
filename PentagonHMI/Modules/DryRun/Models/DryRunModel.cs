using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class DryRunModel : ViewModelBase
    {
        #region PrivateFields

        public Tag DryRunEnableTag = new Tag();
        public Tag DryRunNameTag = new Tag();

        #endregion PrivateFields

        #region PublicProperties

        private int _dryRunIndex = 0;

        public int DryRunIndex
        {
            get { return _dryRunIndex; }
            set
            {
                if(_dryRunIndex != value)
                {
                    _dryRunIndex = value;
                    RaisePropertyChanged(nameof(DryRunIndex));
                }
            }
        }

        private bool _dryRunEnable = false;

        public bool DryRunEnable
        {
            get { return _dryRunEnable; }
            set
            {
                if(_dryRunEnable != value)
                {
                    _dryRunEnable = value;
                    RaisePropertyChanged(nameof(DryRunEnable));
                }
            }
        }

        private string _dryRunName = string.Empty;

        public string DryRunName
        {
            get { return _dryRunName; }
            set
            {
                if(_dryRunName != value)
                {
                    _dryRunName = value;
                    RaisePropertyChanged(nameof(DryRunName));
                }
            }
        }

        #endregion PublicProperties
    }
}