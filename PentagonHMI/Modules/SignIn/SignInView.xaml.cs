using GalaSoft.MvvmLight.Messaging;
using PentagonHMI.LogicClasses;
using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for SignInView.xaml.
    /// </summary>
    public partial class SignInView : Window
    {
        #region PrivateFields

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private LogicClasses.Main main = null;

        private bool isPopUp = false;

        private int loginAttempt = 0;

        private string loginAttemptStr;

        private string lastLoginShiftTime;

        private string lastLoginShiftDate;

        #endregion PrivateFields

        #region Constructor

        public SignInView(ref LogicClasses.Main _main, bool popUp)
        {
            try
            {
                InitializeComponent();
                initialize(_main);
                isPopUp = popUp;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion Constructor

        #region PrivateInitializeMethods

        private void initialize(LogicClasses.Main _main)
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

        #endregion PrivateInitializeMethods

        #region PrivateEventMethods

        private void signInWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void signInWindow_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(PreCheck() == true)
                {
                    main.UserAccessLevel = main.SQLer.Exec_Scalar<string>($"SELECT [LEVEL] FROM USERS WHERE USERNAME = '{Username.Text}' AND PASSWORD = '{Password.Text}'");

                    if (!isPopUp)
                    {
                        DateTime currDate = DateTime.Now;

                        //login attempt receive customer feedback say want to remove it on 23/7/2024 by plc people -> Max, temporary remove for both lpm and tmp - KWY 25/7/2024
                        //if (!string.IsNullOrWhiteSpace(main.UserAccessLevel))
                        //{
                        //    lastLoginDateTimeStr = main.SQLer.Exec_Scalar<string>($"SELECT [LASTLOGINDATETIME] FROM USERS WHERE USERNAME = '{Username.Text}' AND PASSWORD = '{Password.Text}'");
                        //    loginAttemptStr = main.SQLer.Exec_Scalar<string>($"SELECT [LOGINATTEMPT] FROM USERS WHERE USERNAME = '{Username.Text}' AND PASSWORD = '{Password.Text}'");

                        //    if (string.IsNullOrEmpty(loginAttemptStr) || string.IsNullOrEmpty(lastLoginDateTimeStr))
                        //    {
                        //        //main.SQLer.Exec_NonQuery($"UPDATE TABLE [Users] SET [LoginAttempt] = '0' AND [LastLoginDateTime] = '{currDate}' WHERE [UserName] = {Username.Text};");
                        //        loginAttempt = 0;
                        //    }
                        //    else
                        //    {
                        //        loginAttempt = Convert.ToInt32(loginAttemptStr);
                        //    }

                        //    if (!string.IsNullOrEmpty(lastLoginDateTimeStr))
                        //    {
                        //        DateTime lastLogindateTime = DateTime.Parse(lastLoginDateTimeStr);
                        //        if (currDate.Date > lastLogindateTime.Date)
                        //        {
                        //            loginAttempt = 0;
                        //            //Update db
                        //            //main.SQLer.Exec_NonQuery($"UPDATE [Users] SET [LoginAttempt] = '0' WHERE [UserName] = {Username.Text};");
                        //        }
                        //    }
                        //}

                        if (string.IsNullOrWhiteSpace(main.UserAccessLevel))
                        {
                            if (LocalAccountCheck(Username.Text, Password.Text))
                            {
                                main.UserAccessLevel = "Administrator";
                                loginAttempt = 0;
                            }
                        }

                        //if (loginAttempt < 11)
                        //{
                            if (!string.IsNullOrWhiteSpace(main.UserAccessLevel))
                            {
                                main.UserName = Username.Text;
                                //if (main.UserAccessLevel != "Administrator")
                                //{
                                //    loginAttempt++;
                                //    //Update db
                                //    main.SQLer.Exec_NonQuery($"UPDATE [Users] SET [LoginAttempt] = '{loginAttempt}', [LastLoginDateTime] = '{currDate}' WHERE [UserName] = '{Username.Text}';"); 
                                //}
                                Username.Text = "";
                                Password.Text = "";
                                Hide();

                                Messenger.Default.Send<Tuple<SignInView, string>, Main>(
                                    new Tuple<SignInView, string>(this, main.UserAccessLevel));
                            }
                            else
                                this.Password.ucLabelErrorContent = this.TryFindResource("LOGIN_TEXTBLOCK_INVALID").ToString();
                        //}
                        //else
                        //{
                        //    this.Password.ucLabelErrorContent = this.TryFindResource("LOGIN_ERROR_MSG_LOGIN_ATTEMPT_EXCEED").ToString();
                        //} 
                    }
                    else
                    {
                        if (string.IsNullOrWhiteSpace(main.UserAccessLevel))
                            if (LocalAccountCheck(Username.Text, Password.Text))
                                main.UserAccessLevel = "Administrator";

                        if (!string.IsNullOrWhiteSpace(main.UserAccessLevel))
                        {
                            if (main.UserAccessLevel == "Administrator")
                            {
                                main.UserName = Username.Text;
                                FileLogger.logUser("[HMI]", $"[HMI] {main.UserName} Login Success | Remark : Bypass");
                                Username.Text = "";
                                Password.Text = "";
                                Hide();

                                //Messenger.Default.Send<Tuple<SignInView, string>, Main>(
                                //    new Tuple<SignInView, string>(this, main.UserAccessLevel)); 
                                string tag = "b_HMI_ToRequestAdminLogin";
                                main.OPC.Write(tag, false);
                            }
                            else
                            {
                                this.Password.ucLabelErrorContent = this.TryFindResource("LOGIN_ERROR_MSG_ADMIN_LOGIN").ToString();
                            }
                        }
                        else
                            this.Password.ucLabelErrorContent = this.TryFindResource("LOGIN_TEXTBLOCK_INVALID").ToString();
                    }
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Username.Text = string.Empty;
                Password.Text = string.Empty;
                main.UserName = string.Empty;
                Hide();
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateEventMethods

        #region PrivateMethods

        private bool PreCheck()
        {
            bool Check = true;

            if(String.IsNullOrEmpty(Password.Text))
            {
                if(String.IsNullOrEmpty(Password.ucLabelErrorContent))
                {
                    this.Password.ucLabelErrorContent = this.TryFindResource("LOGIN_ERROR_MSG_PASSWORD_IS_EMPTY").ToString();
                    Check = false;
                }
            }
            else if(!String.IsNullOrEmpty(Password.Text))
            {
                if(!String.IsNullOrEmpty(Password.ucLabelErrorContent))
                {
                    this.Password.ucLabelErrorContent = String.Empty;
                }
            }
            return Check;
        }

        private bool LocalAccountCheck(string StrName, string strPassword)
        {
            //Na requested to change in meeting 10/7/2024 
            //if (StrName == "admin" && strPassword == "#admin123")
            //{
            //    return true;
            //}
            if (StrName == "penta" && strPassword == "#penta2121")
            {
                return true;
            }
            else if (StrName == "jabiladmin" && strPassword == "#penta5196")
            {
                return true;
            }
            else if (StrName == "plexusadmin" && strPassword == "#penta5196")
            {
                return true;
            }
            return false;
        }

        #endregion PrivateMethods
    }
}