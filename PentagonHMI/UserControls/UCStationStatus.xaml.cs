using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace PentagonHMI.UserControls
{
    public partial class UCStationStatus : UserControl
    {
        public UCStationStatus()
        {
            InitializeComponent();
        }

        bool PreZ1 = false;
        bool PreZ2 = false;
        public void SetDis(bool Zone1, bool Zone2)
        {
            if (Zone1 != PreZ1)
            {
                this.Dispatcher.Invoke(new Action(() => Lbl_Zone1.Visibility = Zone1 ? 
                Visibility.Visible : Visibility.Collapsed));
                PreZ1 = Zone1;
            }

            if (Zone2 != PreZ2)
            {
                this.Dispatcher.Invoke(new Action(() => Lbl_Zone2.Visibility = Zone2 ? 
                Visibility.Visible : Visibility.Collapsed));
                PreZ2 = Zone2;
            }
        }

        string PreStatus;
        public void SetStatus(string Sts)
        {
            if (string.IsNullOrWhiteSpace(Sts))
                return;

            if (Sts == PreStatus)
                return;
            else
                PreStatus = Sts;

            switch (Sts.ToUpper())
            {
                case "RUNNING":
                case "RUN":
                    this.Dispatcher.Invoke(new Action(()=> Rec_Status.Fill = Brushes.LawnGreen));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Background = Brushes.Green));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Foreground = Brushes.White));
                    break;

                case "STOPPED":
                case "STOP":
                    this.Dispatcher.Invoke(new Action(() => Rec_Status.Fill = Brushes.Yellow));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Background = Brushes.Yellow));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Foreground = Brushes.Black));
                    Sts = "Waiting";
                    break;

                case "DRY-RUN":
                case "DRY RUN":
                case "IDLE":
                case "IDLING":
                case "INITIALIZE":
                case "INITIALIZING":
                    this.Dispatcher.Invoke(new Action(() => Rec_Status.Fill = Brushes.Blue));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Background = Brushes.Blue));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Foreground = Brushes.White));
                    break;

                case "ALARM":
                case "ERROR":
                    this.Dispatcher.Invoke(new Action(() => Rec_Status.Fill = Brushes.Red));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Background = Brushes.Red));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Foreground = Brushes.White));
                    break;

                case "WARNING":
                    this.Dispatcher.Invoke(new Action(() => Rec_Status.Fill = Brushes.Yellow));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Background = Brushes.Yellow));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Foreground = Brushes.Black));
                    break;

                case "DISABLE":
                case "DISABLED":
                    this.Dispatcher.Invoke(new Action(() => Rec_Status.Fill = Brushes.Gray));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Background = Brushes.Gray));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Foreground = Brushes.White));
                    break;

                default:
                    this.Dispatcher.Invoke(new Action(() => Rec_Status.Fill = Brushes.Yellow));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Background = Brushes.Yellow));
                    this.Dispatcher.Invoke(new Action(() => Lbl_Status.Foreground = Brushes.Black));
                    break;
            }

            this.Dispatcher.Invoke(new Action(() => Lbl_Status.Content = Sts));
        }
    }
}
