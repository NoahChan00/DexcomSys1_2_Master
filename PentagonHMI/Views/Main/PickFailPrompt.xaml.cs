using PentagonHMI.ChildControls;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PentagonHMI.Views.Main
{
    /// <summary>
    /// Interaction logic for pickFailPrompt.xaml
    /// </summary>
    public partial class PickFailPrompt : UserControl
    {
        private string skipPickTag;
        private string retryPickTag;

        public PickFailPrompt(LogicClasses.Main main, SelectedTray selectedTray)
        {
            InitializeComponent();
            this.main = main;
            string title = "";
            trayMap = new ucTrayMap(main);
            trayMap.RemoveParent();
            skipPickTag = Tags.MainPage.PickFailSkipPick.Name;
            retryPickTag = Tags.MainPage.PickFailRetryPick.Name;
            switch(selectedTray)
            {
                case SelectedTray.S1PCBA:
                    title = "PCBA";
                    TrayContentControl.Content = trayMap.pcba_SlotTray;
                    break;
                //case SelectedTray.S1BATTERY:
                //    title = "Battery";
                //    TrayContentControl.Content = trayMap.bat_SlotTray;
                //    break;
                case SelectedTray.S2LEFT:
                    title = "Left Shuttle";
                    TrayContentControl.Content = trayMap.input_LeftSlot;
                    break;

                case SelectedTray.S2RIGHT:
                    title = "Right Shuttle";
                    TrayContentControl.Content = trayMap.input_RightSlot;
                    break;

                case SelectedTray.S2OUTPUT:
                    title = "Unload Shuttle";
                    TrayContentControl.Content = trayMap.output_Slot;

                    // Unload use different Tag
                    skipPickTag = Tags.MainPage.PickFailUnloadSkipPick.Name;
                    retryPickTag = Tags.MainPage.PickFailUnloadRetryPick.Name;
                    break;
            }
            if(TrayContentControl.Content is UniformGrid ug)
            {
                ug.Margin = new Thickness(0);
            }
            TitleLabel.Content = title + " Tray Pick Fail";
            cts = new CancellationTokenSource();
            ct = cts.Token;
            Task.Run(() =>
            {
                while(!ct.IsCancellationRequested)
                {
                    Thread.Sleep(250);
                    Dispatcher.Invoke(() =>
                    {
                        SkipPickButton.IsChecked = main.OPC.Read<bool>(skipPickTag, typeof(bool));
                        SkipPickButton.Background = (SkipPickButton.IsChecked ?? false) ? Brushes.LimeGreen : Brushes.LightGray;
                        RetryPickButton.IsChecked = main.OPC.Read<bool>(retryPickTag, typeof(bool));
                        RetryPickButton.Background = (RetryPickButton.IsChecked ?? false) ? Brushes.LimeGreen : Brushes.LightGray;
                    });
                }
            });
        }

        ~PickFailPrompt()
        {
            cts.Cancel();
        }

        private CancellationToken ct;
        private CancellationTokenSource cts;
        private LogicClasses.Main main;
        private ucTrayMap trayMap;

        private void SkipChecked(object sender, RoutedEventArgs e)
        {
            main.OPC.Write(skipPickTag, true, typeof(bool));
        }

        private void SkipUnchecked(object sender, RoutedEventArgs e)
        {
            main.OPC.Write(skipPickTag, false, typeof(bool));
        }

        private void RetryPickChecked(object sender, RoutedEventArgs e)
        {
            main.OPC.Write(retryPickTag, true, typeof(bool));
        }

        private void RetryPickUnchecked(object sender, RoutedEventArgs e)
        {
            main.OPC.Write(retryPickTag, false, typeof(bool));
        }
    }

    public enum SelectedTray
    {
        S1PCBA,
        S1BATTERY,
        S2LEFT,
        S2RIGHT,
        S2OUTPUT,
    }
}