using System;
using System.Windows;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for MainView.xaml.
    /// </summary>
    public partial class MainView : UserControl, IDisposable
    {
        #region PrivateFields

        private ZoneView zoneView = null;
        private YieldView yieldView = null;
        private UphView uphView = null;
        private VisionTabControlView visionTabControlView = null;
        private LogicClasses.Main main = null;

        #endregion PrivateFields

        #region Constructor

        public MainView(ref LogicClasses.Main _main)
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
                    zoneView = new ZoneView(main);
                    addMainGridChildren(zoneView, 0, 0, 1, 2);

                    yieldView = new YieldView(main);
                    addMainGridChildren(yieldView, 1, 0);

                    uphView = new UphView(main);
                    addMainGridChildren(uphView, 1, 1);

                    MainTabControl.Items.Add(new TabItem
                    {
                        Header = "Vision",
                        Content = visionTabControlView = new VisionTabControlView(main)
                    });

                    if(main.HasRackStatus)
                    {
                        MainTabControl.Items.Add(new TabItem
                        {
                            Header = "Rack Status",
                            Content = new RackStatusView(main)
                        });
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

        private void addMainGridChildren(UIElement uIElement, int gridRow,
            int gridColumn, int gridRowSpan = 1, int gridColumnSpan = 1)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    MainGrid.Children.Add(uIElement);
                    Grid.SetRow(uIElement, gridRow);
                    Grid.SetColumn(uIElement, gridColumn);
                    Grid.SetRowSpan(uIElement, gridRowSpan);
                    Grid.SetColumnSpan(uIElement, gridColumnSpan);
                }
                catch(Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }

        #endregion PrivateMethods

        #region PublicInterfaceMethods

        public void Dispose()
        {
            try
            {
                main.HomePageON = false;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PublicInterfaceMethods
    }
}