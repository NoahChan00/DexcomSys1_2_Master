using System;
using System.Windows;
using System.Windows.Controls;

namespace PentagonHMI.Views.ComponentLifeCycle
{
    /// <summary>
    /// Interaction logic for ucComponentLifeCycle.xaml
    /// </summary>
    public partial class ucComponentLifeCycle : UserControl, IDisposable
    {
        private LogicClasses.Main _Main;

        public ucComponentLifeCycle(ref LogicClasses.Main _main)
        {
            InitializeComponent();
            _Main = _main;
            //_Main.ComponentLifeCycle_OnUpdate += new LogicClasses.Main.onComponentLifeCycleUpdateHandler(Update);
        }

        private const string Tag_PogoPinMax = "TCA_PinCycleCountMax";
        private const string Tag_PogoPinCurrent = "TCA_PinCycleCurrentCount";

        private enum State
        {
            PogoPinAdjust,
            PogoPinReset,
            PogoBlockAdjust,
            PogoBlockReset,
            None
        }

        public void Update()
        {
            Dispatcher.Invoke(() => PogoMaxCount.Text = _Main.OPC.Read<string>(Tag_PogoPinMax));
            Dispatcher.Invoke(() => PogoCurrentCount.Text = _Main.OPC.Read<string>(Tag_PogoPinCurrent));
        }

        private State CurState = new State();

        private void PogoMaxCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            int Cur = Convert.ToInt32(PogoCurrentCount.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Edit1.Visibility = Edit1.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            NewPogoMaxCount.Visibility = NewPogoMaxCount.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
            NewPogoMaxCount.Text = PogoMaxCount.Text;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            loginScreen.Visibility = Visibility.Collapsed;
            ucPassword.Text = string.Empty;
            ucUsername.Text = string.Empty;
            CurState = State.None;
        }

        private void btnLogIn_Click(object sender, RoutedEventArgs e)
        {
            ucPassword.ucLabelErrorContent = string.Empty;
            if(_Main.SQLer.Exec_Scalar<int>($"SELECT COUNT(USERNAME) FROM USERS WHERE USERNAME = '{ucUsername.Text}' AND PASSWORD = '{ucPassword.Text}'") > 0)
            {
                loginScreen.Visibility = Visibility.Collapsed;
                if(CurState.Equals(State.PogoPinAdjust))
                {
                    int n;
                    if(int.TryParse(NewPogoMaxCount.Text, out n))
                    {
                        _Main.OPC.Write(Tag_PogoPinMax, n);
                    }
                }
                //else if (CurState.Equals(State.PogoBlockAdjust))
                //{
                //    PogoMaxCount2.Text = NewPogoMaxCount2.Text;
                //}
                else if(CurState.Equals(State.PogoPinReset))
                {
                    _Main.OPC.Write(Tag_PogoPinCurrent, 0);
                }
                //else if (CurState.Equals(State.PogoBlockReset))
                //{
                //    PogoCurrentCount2.Text = "0";
                //}
            }
            else
            {
                ucPassword.ucLabelErrorContent = "invalid credentials";
            }

            ucPassword.Text = string.Empty;
            ucUsername.Text = string.Empty;
        }

        private void Edit1_Click(object sender, RoutedEventArgs e)
        {
            CurState = State.PogoPinAdjust;
            loginScreen.Visibility = Visibility.Visible;
        }

        private void Edit2_Click(object sender, RoutedEventArgs e)
        {
            CurState = State.PogoBlockAdjust;
            loginScreen.Visibility = Visibility.Visible;
        }

        private void Btn_Reset_Click(object sender, RoutedEventArgs e)
        {
            CurState = State.PogoPinReset;
            loginScreen.Visibility = Visibility.Visible;
        }

        private void Btn_Reset2_Click(object sender, RoutedEventArgs e)
        {
            CurState = State.PogoBlockReset;
            loginScreen.Visibility = Visibility.Visible;
        }

        public void Dispose()
        {
            //_Main.CLCPageOn = false;
        }
    }
}