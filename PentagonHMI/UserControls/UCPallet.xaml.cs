using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media.Animation;
using System.Windows.Media;

namespace PentagonHMI.UserControls
{
    public partial class UCPallet : UserControl
    {
        Storyboard Sty_Pop = new Storyboard();
        Storyboard Sty_InLeft = new Storyboard();
        Storyboard Sty_OutLeft = new Storyboard();
        Storyboard Sty_InRight = new Storyboard();
        Storyboard Sty_OutRight = new Storyboard();

        DoubleAnimation AnOpc = new DoubleAnimation
        {
            From = 0,
            To = 1,
            Duration = TimeSpan.FromSeconds(0.2),
            BeginTime = TimeSpan.FromSeconds(0)
        };

        DoubleAnimation AnIn = new DoubleAnimation
        {
            From = 0,
            To = 38,
            Duration = TimeSpan.FromSeconds(0.2),
            BeginTime = TimeSpan.FromSeconds(0)
        };

        DoubleAnimation AnOut = new DoubleAnimation
        {
            From = 38,
            To = 0,
            Duration = TimeSpan.FromSeconds(0.2),
            BeginTime = TimeSpan.FromSeconds(0)
        };

        DoubleAnimation AnIn2 = new DoubleAnimation
        {
            From = 0,
            To = 38,
            Duration = TimeSpan.FromSeconds(0.2),
            BeginTime = TimeSpan.FromSeconds(0)
        };

        DoubleAnimation AnOut2 = new DoubleAnimation
        {
            From = 38,
            To = 0,
            Duration = TimeSpan.FromSeconds(0.2),
            BeginTime = TimeSpan.FromSeconds(0)
        };

        public UCPallet()
        {
            InitializeComponent();

            Storyboard.SetTargetProperty(AnOpc, new PropertyPath(Grid.OpacityProperty));
            Storyboard.SetTarget(AnOpc, TrayRec);
            Sty_Pop.Children.Add(AnOpc);

            Storyboard.SetTargetProperty(AnIn, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTarget(AnIn, LeftPallet);
            Sty_InLeft.Children.Add(AnIn);
            Storyboard.SetTargetProperty(AnIn2, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTarget(AnIn2, RightPallet);
            Sty_InRight.Children.Add(AnIn2);

            Storyboard.SetTargetProperty(AnOut, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTarget(AnOut, LeftPallet);
            Sty_OutLeft.Children.Add(AnOut);
            Storyboard.SetTargetProperty(AnOut2, new PropertyPath(Ellipse.WidthProperty));
            Storyboard.SetTarget(AnOut2, RightPallet);
            Sty_OutRight.Children.Add(AnOut2);
        }

        public bool ToTray
        {
            get { return false; }
            set { LeftPallet.Fill = Brushes.Silver; RightPallet.Fill = Brushes.Silver; }
        }

        public void UpdatePointIO(bool In, bool Out, bool Presence = false, string OrdID = "")
        {
            this.Dispatcher.Invoke(new Action(() => SetLeftPallet(In)));
            this.Dispatcher.Invoke(new Action(() => SetRightPallet(Out)));
            this.Dispatcher.Invoke(new Action(() => SetTray(Presence)));
            SetOrderID(OrdID);
        }

        string PreOrderID { get; set; } = string.Empty;
        private void SetOrderID(string NowOrderID)
        {
            if (NowOrderID == PreOrderID)
                return;

            this.Dispatcher.Invoke(new Action(() => OrderID.Text = NowOrderID));

            PreOrderID = NowOrderID;
        }

        bool BoolLeftPallet { get; set; } = false;
        private void SetLeftPallet(bool Exist)
        {
            if (BoolLeftPallet == Exist)
                return;

            if (Exist)
                AnimateOpen("L");
            else
                AnimateClose("L");

            BoolLeftPallet = Exist;
        }

        bool BoolRightPallet { get; set; } = false;
        private void SetRightPallet(bool Exist)
        {
            if (BoolRightPallet == Exist)
                return;

            if (Exist)
                AnimateOpen("R");
            else
                AnimateClose("R");

            BoolRightPallet = Exist;
        }

        bool BoolTray { get; set; } = false;
        private void SetTray(bool Exist)
        {
            if (BoolTray == Exist)
                return;

            if (Exist)
            {
                Tray.Visibility = Visibility.Visible;
                AnimatePop();
            }
            else
                Tray.Visibility = Visibility.Collapsed;

            BoolTray = Exist;
        }

        private void AnimatePop()
        {
            try
            {
                Sty_Pop.Begin();
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void AnimateOpen(string str_side)
        {
            try
            {
                if (str_side == "L")
                {
                    LeftPallet.HorizontalAlignment = HorizontalAlignment.Right;
                    Sty_InLeft.Begin();
                }
                else if (str_side == "R")
                {
                    RightPallet.HorizontalAlignment = HorizontalAlignment.Right;
                    Sty_InRight.Begin();
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void AnimateClose(string str_side)
        {
            try
            {
                if (str_side == "L")
                {
                    LeftPallet.HorizontalAlignment = HorizontalAlignment.Left;
                    Sty_OutLeft.Begin();
                }
                else if (str_side == "R")
                {
                    RightPallet.HorizontalAlignment = HorizontalAlignment.Left;
                    Sty_OutRight.Begin();
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

    }
}
