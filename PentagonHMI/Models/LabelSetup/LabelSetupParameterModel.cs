using GalaSoft.MvvmLight;

namespace PentagonHMI.Models
{
    public class LabelSetupParameterModel : ViewModelBase
    {
        #region PublicProperties
        private string labelSetupParameterName = string.Empty;
        public string LabelSetupParameterName
        {
            get { return labelSetupParameterName; }
            set
            {
                if (labelSetupParameterName != value)
                {
                    labelSetupParameterName = value;
                    RaisePropertyChanged(nameof(LabelSetupParameterName));
                }
            }
        }

        private string labelSetupParameter = string.Empty;
        public string LabelSetupParameter
        {
            get { return labelSetupParameter; }
            set
            {
                if (labelSetupParameter != value)
                {
                    labelSetupParameter = value;
                    RaisePropertyChanged(nameof(LabelSetupParameter));
                }
            }
        }

        private string labelSetupParameterDescription = string.Empty;
        public string LabelSetupParameterDescription
        {
            get { return labelSetupParameterDescription; }
            set
            {
                if (labelSetupParameterDescription != value)
                {
                    labelSetupParameterDescription = value;
                    RaisePropertyChanged(nameof(LabelSetupParameterDescription));
                }
            }
        }
        #endregion
    }
}