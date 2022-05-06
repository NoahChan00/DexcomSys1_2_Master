using GalaSoft.MvvmLight;
using Logix;

namespace PentagonHMI
{
    public class RackStatusActionModel : ViewModelBase
    {
        #region PublicFields
        public Tag RackStatusActionTag = new Tag();
        #endregion

        #region PublicProperties
        private string _rackStatusActionName = string.Empty;
        public string RackStatusActionName
        {
            get { return _rackStatusActionName; }
            set
            {
                if (_rackStatusActionName != value)
                {
                    _rackStatusActionName = value;
                    RaisePropertyChanged(nameof(RackStatusActionName));
                }
            }
        }
        #endregion
    }
}