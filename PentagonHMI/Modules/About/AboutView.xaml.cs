using System;
using System.IO;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for AboutView.xaml
    /// </summary>
    public partial class AboutView : UserControl, IDisposable
    {
        #region Constructors
        public AboutView()
        {
            try
            {
                InitializeComponent();
                updateAboutTextBox();
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion
        
        #region PrivateMethods
        private void updateAboutTextBox()
        {
            try
            {
                string filePath = $"{AppDomain.CurrentDomain.BaseDirectory}About.txt";
                
                if (!File.Exists(filePath))
                {
                    File.WriteAllText(filePath, string.Empty);
                }

                AboutTextBox.Dispatcher.Invoke(new Action(() => AboutTextBox.Text = File.ReadAllText(filePath)));
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion
        
        #region PublicInterfaceMethods
        public void Dispose()
        {
        }
        #endregion
    }
}