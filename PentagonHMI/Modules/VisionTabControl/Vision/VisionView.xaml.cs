using System;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for VisionView.xaml.
    /// </summary>
    public partial class VisionView : UserControl
    {
        #region Constructor
        public VisionView(LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                initializeVision(_main);
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private void initializeVision(LogicClasses.Main _main)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    WindowsFormsHost windowsFormsHost = new WindowsFormsHost
                    {
                        Child = new AxXGXView(_main)
                    };

                    VisionGroupBox.Children.Add(windowsFormsHost);
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }
        #endregion
    }
}