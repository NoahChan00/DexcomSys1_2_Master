using Logix;
using System;
using System.Windows.Forms;
using Utilities;

namespace PentagonHMI
{
    public partial class AxXGXView : UserControl
    {
        #region PrivateFields

        private LogicClasses.Main _Main = null;
        private Tag hmiVisionCaptureTag = new Tag { Name = "HMI_Vision_Capture", DataType = Logix.Tag.ATOMIC.INT };
        private Tag hmiVisionCaptureAssetTag = new Tag { Name = "HMI_Vision_Capture_AssetTag", DataType = Logix.Tag.ATOMIC.STRING };
        private Tag hmiVisionCaptureTag2 = new Tag { Name = "HMI_Vision_Capture", DataType = Logix.Tag.ATOMIC.INT };
        private Tag hmiVisionCaptureAssetTag2 = new Tag { Name = "HMI_Vision_Capture_AssetTag", DataType = Logix.Tag.ATOMIC.STRING };

        #endregion PrivateFields

        #region Constructor

        public AxXGXView(LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                initializeOpc(_main);
                initializeAxXGX();
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion Constructor

        #region PrivateInitializeMethods

        private void initializeOpc(LogicClasses.Main _main)
        {
            try
            {
                _Main = _main;
                if(ProjectType.HDD == _Main.StnType)
                {
                    hmiVisionCaptureAssetTag.Name = "HMI_Zone1_VisCapture_HDDTag";
                    hmiVisionCaptureAssetTag2.Name = "HMI_Zone2_VisCapture_HDDTag";
                }
                _Main.OnFastUpdate += main_Home_OnUpdate;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeAxXGX()
        {
            try
            {
                if(axXGX1.Initialize() == 0)
                {
                    axXGX1.Address = Properties.Settings.Default.VISIONIP;
                    axXGX1.Port = Properties.Settings.Default.VISIONPORT;

                    if(axXGX1.Connect() == 0)
                    {
                        axXGX1.StartRemoteDesktop(0);
                    }
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateInitializeMethods

        #region PrivateEventMethods

        private void main_Home_OnUpdate()
        {
            try
            {
                _Main.MyPLC.ReadTag(hmiVisionCaptureTag);
                if(hmiVisionCaptureTag.Value != null)
                {
                    switch(Convert.ToInt32(hmiVisionCaptureTag.Value))
                    {
                        case 1:
                            FileLogger.logAnything("Start V", "");
                            hmiVisionCaptureTag.Value = 2;
                            _Main.MyPLC.WriteTag(hmiVisionCaptureTag);

                            _Main.MyPLC.ReadTag(hmiVisionCaptureAssetTag);
                            if(hmiVisionCaptureAssetTag.Value == null)
                            {
                                hmiVisionCaptureTag.Value = 4;
                            }
                            else
                            {
                                if(string.IsNullOrEmpty(hmiVisionCaptureAssetTag.Value.ToString()))
                                {
                                    hmiVisionCaptureTag.Value = 4;
                                }
                                else
                                {
                                    string filePath = $@"D:\{_Main.MachineName}_Vision\" +
                                        $@"{DateTime.Now.ToString("yyyy-MM-dd")}\" +
                                        $"{hmiVisionCaptureAssetTag.Value.ToString()}.bmp";

                                    FileLogger.logAnything("Start Vsave", hmiVisionCaptureAssetTag.Value.ToString());
                                    hmiVisionCaptureTag.Value = axXGX1.CaptureRemoteDesktop(filePath) == 0 ? 3 : 4;
                                    FileLogger.logAnything("End Vsave", hmiVisionCaptureAssetTag.Value.ToString() + ";" + hmiVisionCaptureTag.Value.ToString() == "3" ? "Good" : "Bad");
                                }
                            }

                            _Main.MyPLC.WriteTag(hmiVisionCaptureTag);
                            FileLogger.logAnything("End V", "");
                            break;

                        default:
                            break;
                    }

                    if(_Main.StnType == ProjectType.HDD)
                    {
                        SecondVision();
                    }
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateEventMethods

        private void SecondVision()
        {
            _Main.MyPLC.ReadTag(hmiVisionCaptureTag2);
            if(hmiVisionCaptureTag2.Value != null)
            {
                switch(Convert.ToInt32(hmiVisionCaptureTag2.Value))
                {
                    case 1:
                        hmiVisionCaptureTag2.Value = 2;
                        _Main.MyPLC.WriteTag(hmiVisionCaptureTag2);

                        _Main.MyPLC.ReadTag(hmiVisionCaptureAssetTag2);
                        if(hmiVisionCaptureAssetTag2.Value == null)
                        {
                            hmiVisionCaptureTag2.Value = 4;
                        }
                        else
                        {
                            if(string.IsNullOrEmpty(hmiVisionCaptureAssetTag2.Value.ToString()))
                            {
                                hmiVisionCaptureTag2.Value = 4;
                            }
                            else
                            {
                                string filePath = $@"D:\{_Main.MachineName}_Vision_2\" +
                                    $@"{DateTime.Now.ToString("yyyy-MM-dd")}\" +
                                    $"{hmiVisionCaptureAssetTag2.Value.ToString()}.bmp";

                                hmiVisionCaptureTag2.Value = axXGX1.CaptureRemoteDesktop(filePath) == 0 ? 3 : 4;
                            }
                        }
                        _Main.MyPLC.WriteTag(hmiVisionCaptureTag2);
                        break;

                    default:
                        break;
                }
            }
        }
    }
}