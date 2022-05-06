using GalaSoft.MvvmLight;
using Logix;
using System.IO.Ports;

namespace PentagonHMI
{
    public class PrinterModel : ViewModelBase
    {
        #region PublicFields
        public SerialPort SerialPort = new SerialPort();
        public string Prn = string.Empty;

        public Tag TriggerPrintTag = new Tag();
        public Tag TriggerPrintDoneTag = new Tag();
        public Tag TriggerPrintFailTag = new Tag();
        public Tag LabelCodeTag = new Tag();
        #endregion

        #region PublicProperties
        private string portName = string.Empty;
        public string PortName
        {
            get { return portName; }
            set
            {
                if (portName != value)
                {
                    portName = value;
                    RaisePropertyChanged(nameof(PortName));
                }
            }
        }
        
        private string labelFileDirectory = string.Empty;
        public string LabelFileDirectory
        {
            get { return labelFileDirectory; }
            set
            {
                if (labelFileDirectory != value)
                {
                    labelFileDirectory = value;
                    RaisePropertyChanged(nameof(LabelFileDirectory));
                }
            }
        }
        #endregion
    }
}