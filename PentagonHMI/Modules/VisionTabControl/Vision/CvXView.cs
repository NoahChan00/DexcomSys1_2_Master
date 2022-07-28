using Logix;
using NLog;
using System;
using System.Windows.Forms;
using Utilities;

namespace PentagonHMI
{
    public partial class CvXView : UserControl
    {
        #region PrivateFields
        private static Logger logger = LogManager.GetCurrentClassLogger();
        #endregion

        #region Constructor
        public CvXView()
        {
            InitializeComponent();
            try
            {
                intializeaxCvx();
            }
            catch (Exception ex) { logger.Error(ex); }
        }
        #endregion

        #region PrivateInitialize
        private void intializeaxCvx()
        {
            try
            {
                int initializeResult = axCVX1.Initialize();

                //MyLogger.VisionDebugLog($"initializeResult : {initializeResult}");

                if (initializeResult == 0)
                {
                    //MyLogger.VisionDebugLog("Processing succeeded");

                    axCVX1.Address = "193.168.3.21";
                    axCVX1.Port = 8502;

                    int Connect = axCVX1.Connect();

                    if (Connect == 0)
                    {
                        //MyLogger.VisionDebugLog($"Connected. IP Address : {axCVX1.Address} ");

                        axCVX1.StartRemoteDesktop(0);
                    }
                    else
                    {
                        //MyLogger.VisionDebugLog($"Fail To Connect (Value) : {Connect}");
                    }
                }
            }
            catch (Exception ex) { logger.Error(ex); }
        }
        #endregion
    }

}