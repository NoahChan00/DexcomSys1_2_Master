using GalaSoft.MvvmLight;
using System;

namespace PentagonHMI
{
    public class DryRunTimeModel : ViewModelBase
    {
        #region PublicProperties
        private string _dryRunTimeName = string.Empty;
        public string DryRunTimeName
        {
            get { return _dryRunTimeName; }
            set
            {
                if (_dryRunTimeName != value)
                {
                    _dryRunTimeName = value;
                    RaisePropertyChanged(nameof(DryRunTimeName));
                }
            }
        }

        private int _dryRunTime = 0;
        public int DryRunTime
        {
            get { return _dryRunTime; }
            set
            {
                if (_dryRunTime != value)
                {
                    _dryRunTime = value;
                    RaisePropertyChanged(nameof(DryRunTime));
                    RaisePropertyChanged(nameof(DryRunTimeString));
                }
            }
        }

        private string _dryRunTimeString = string.Empty;
        public string DryRunTimeString
        {
            get
            {
                TimeSpan timeSpan = TimeSpan.FromSeconds(DryRunTime);

                _dryRunTimeString = string.Format("{0:D2}h:{1:D2}m:{2:D2}s",
                    timeSpan.Hours + (timeSpan.Days * 24), timeSpan.Minutes, timeSpan.Seconds);
                
                return _dryRunTimeString;
            }
        }
        
        private string _dryRunTimeTagKey = string.Empty;
        public string DryRunTimeTagKey
        {
            get { return _dryRunTimeTagKey; }
            set
            {
                if (_dryRunTimeTagKey != value)
                {
                    _dryRunTimeTagKey = value;
                    RaisePropertyChanged(nameof(DryRunTimeTagKey));
                }
            }
        }
        #endregion
    }
}