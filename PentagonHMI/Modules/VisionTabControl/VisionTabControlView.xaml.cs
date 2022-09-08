using PentagonHMI.Classes;
using System;
using System.Windows;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for VisionTabControlViw.xaml.
    /// </summary>
    public partial class VisionTabControlView : UserControl
    {
        #region PrivateFields

        private VisionView visionView = null;
        private ucKeyenceVision ucKeyenceVision = null;
        private LogicClasses.Main main = null;

        #endregion PrivateFields

        #region Constructor

        public VisionTabControlView(LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                initializeOpc(_main);
                initializeView();
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
                main = _main;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeView()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    visionView = new VisionView(main);
                    addTabControlTab(visionView, "Vision", 0);

                    if(GlobalFunctions.ProjectType == ProjectType.HDD)
                    {
                        ucKeyenceVision = new ucKeyenceVision(main);
                        addTabControlTab(ucKeyenceVision, "Keyence Vision", 1);
                    }

                    if(VisionTabControl.Items.Count == 1)
                    {
                        VisionContentControl.Content = visionView;
                    }
                }
                catch(Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }

        #endregion PrivateInitializeMethods

        #region PrivateMethods

        private void addTabControlTab(UIElement uIElement, string tabHeader, int index = 0)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    TabItem tab = new TabItem();
                    tab.Header = tabHeader;
                    tab.Content = uIElement;

                    if(index == 0)
                        index = VisionTabControl.Items.Count > 0 ? VisionTabControl.Items.Count : 0;

                    VisionTabControl.Items.Insert(index, tab);
                }
                catch(Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }

        #endregion PrivateMethods
    }
}