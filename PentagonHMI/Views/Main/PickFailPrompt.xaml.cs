using PentagonHMI.ChildControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    public partial class PickFailPrompt : Window
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
                    triggerTag = Tags.MainPage.PickFailPromptWindowPCBA.Name;
                    break;
                case SelectedTray.S1BATTERY:
                    title = "Battery";
                    TrayUniformGrid = trayMap.bat_SlotTray;
                    triggerTag = Tags.MainPage.PickFailPromptWindowBattery.Name;
                    break;
                case SelectedTray.S2LEFT:
                    title = "Left Shuttle";
                    TrayUniformGrid = trayMap.input_LeftSlot;
                    triggerTag = Tags.MainPage.PickFailPromptWindowLShuttle.Name;
                    break;
                case SelectedTray.S2RIGHT:
                    title = "Right Shuttle";
                    TrayUniformGrid = trayMap.input_RightSlot;
                    triggerTag = Tags.MainPage.PickFailPromptWindowRShuttle.Name;
                    break;
            }
            TitleLabel.Content = title + " Tray Pick Fail";
            cts = new CancellationTokenSource();
            ct = cts.Token;
            Task.Run(() =>
            {
                while(ct.IsCancellationRequested)
                {
                    Task.Delay(250);
                    SkipPickButton.Background = main.OPC.Read<bool>(Tags.MainPage.PickFailSkipPick.Name, typeof(bool)) ? Brushes.LimeGreen : Brushes.LightGray;
                    RetryPickButton.Background = main.OPC.Read<bool>(Tags.MainPage.PickFailRetryPick.Name, typeof(bool)) ? Brushes.LimeGreen : Brushes.LightGray;
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
        private string triggerTag;

        bool IsClicked { get; set; } = false;
        private void SkipPickClick(object sender, RoutedEventArgs e)
        {
            if(IsClicked)
            {
                return;
            }
            if(sender is Button b)
            {
                main.OPC.Write(Tags.MainPage.PickFailSkipPick.Name, true, typeof(bool));
            }
        }

        private void RetryPickClick(object sender, RoutedEventArgs e)
        {
            if(IsClicked)
            {
                return;
            }
            if(sender is Button b)
            {
                main.OPC.Write(Tags.MainPage.PickFailRetryPick.Name, true, typeof(bool));
            }
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
