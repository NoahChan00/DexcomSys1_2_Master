using PentagonHMI.ChildControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PentagonHMI.Views.Main
{
    /// <summary>
    /// Interaction logic for pickFailPrompt.xaml
    /// </summary>
    public partial class PickFailPrompt : UserControl
    {
        public PickFailPrompt(LogicClasses.Main main, SelectedTray selectedTray)
        {
            InitializeComponent();
            this.main = main;
            trayMap = new ucTrayMap(main);
            string title = "";
            switch(selectedTray)
            {
                case SelectedTray.S1PCBA:
                    title = "PCBA";
                    TrayUniformGrid = trayMap.pcba_SlotTray;
                    break;
                case SelectedTray.S1BATTERY:
                    title = "Battery";
                    TrayUniformGrid = trayMap.bat_SlotTray;
                    break;
                case SelectedTray.S2LEFT:
                    title = "Left Shuttle";
                    TrayUniformGrid = trayMap.input_LeftSlot;
                    break;
                case SelectedTray.S2RIGHT:
                    title = "Right Shuttle";
                    TrayUniformGrid = trayMap.input_RightSlot;
                    break;
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
                        SkipPickButton.IsChecked = main.OPC.Read<bool>(Tags.MainPage.PickFailSkipPick.Name, typeof(bool));
                        SkipPickButton.Background = (SkipPickButton.IsChecked ?? false) ? Brushes.LimeGreen : Brushes.LightGray;
                        RetryPickButton.IsChecked = main.OPC.Read<bool>(Tags.MainPage.PickFailRetryPick.Name, typeof(bool));
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

        bool IsClicked { get; set; } = false;

        private void RetryPickClick(object sender, RoutedEventArgs e)
        {
            if(IsClicked)
            {
                return;
            }
            if(sender is ToggleButton tb)
            {
                main.OPC.Write(Tags.MainPage.PickFailRetryPick.Name, tb.IsChecked, typeof(bool));
            }
        }

        private void SkipChecked(object sender, RoutedEventArgs e)
        {
            main.OPC.Write(Tags.MainPage.PickFailSkipPick.Name, true, typeof(bool));
        }

        private void SkipUnchecked(object sender, RoutedEventArgs e)
        {
            main.OPC.Write(Tags.MainPage.PickFailSkipPick.Name, false, typeof(bool));
        }

        private void RetryPickChecked(object sender, RoutedEventArgs e)
        {
            main.OPC.Write(Tags.MainPage.PickFailRetryPick.Name, true, typeof(bool));
        }

        private void RetryPickUnchecked(object sender, RoutedEventArgs e)
        {
            main.OPC.Write(Tags.MainPage.PickFailRetryPick.Name, false, typeof(bool));
        }
    }
    public enum SelectedTray
    {
        S1PCBA,
        S1BATTERY,
        S2LEFT,
        S2RIGHT,
    }
}
