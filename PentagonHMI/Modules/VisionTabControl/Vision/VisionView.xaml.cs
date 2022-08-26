using PentagonHMI.Classes;
using System;
using System.Data;
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
                Main = _main;
                InitializeComponent();
                var data = Main.SQLer.Exec_DTSelect("SELECT * FROM dbo.VisionFailInfo");
                foreach(DataRow row in data.Rows)
                {
                    VisionFailInfoStackPanel.Children.Add(new Label() { Content = row["DisplayName"] });
                    VisionFailInfoStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = row["Tagname"] } });
                }
#if !DEBUG
                initializeVision(_main);
                _main.Home_OnUpdate += Vision_OnUpdate;
#endif
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        LogicClasses.Main Main;
        private void Vision_OnUpdate()
        {
            Dispatcher?.Invoke(() =>
            {
                // Only system2 got this page
                if(!GlobalFunctions.IsSystem1)
                {
                    foreach(var o in VisionFailInfoStackPanel.Children)
                    {
                        if(o is Border b && b.Child is TextBlock tb)
                        {
                            tb.Text = Main.OPC.Read<int>((string)tb.Tag).ToString();
                        }
                    }
                }
            });
        }

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
                        //Child = new CvXView()
                    };

                    VisionGroupBox.Children.Add(windowsFormsHost);
                }
                catch(Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }
        #endregion
    }
}