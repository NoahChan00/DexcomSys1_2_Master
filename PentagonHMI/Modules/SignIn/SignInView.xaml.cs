using GalaSoft.MvvmLight.Messaging;
using PentagonHMI.LogicClasses;
using System;
using System.ComponentModel;
using System.Globalization;
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

        private string lastLoginShiftDateTime;

        private string currShiftStartTime;

        private string currShiftEndTime;

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
                        bool shiftPass = false;

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
                                //main.UserAccessLevel = "Administrator";
                                loginAttempt = 0;
                                shiftPass = true;
                            }
                        }
                        else
                        {
                            lastLoginShiftDateTime = main.SQLer.Exec_Scalar<string>($"SELECT [LoginShiftDateTime] FROM USERS WHERE USERNAME = '{Username.Text}' AND PASSWORD = '{Password.Text}'");

                            if (string.IsNullOrEmpty(lastLoginShiftDateTime))
                            {
                                DateTime shiftDateTime = FirstTimeLoginShift(currDate);
                                main.SQLer.Exec_NonQuery($"UPDATE [Users] SET [LoginShiftDateTime] = '{shiftDateTime}', [ResetLoginShift] = '0' WHERE [UserName] = '{Username.Text}';");
                                shiftPass = true;
                            }
                            else
                            {
                                DateTime loginShiftDateTime = DateTime.Parse(lastLoginShiftDateTime);
                                shiftPass = CheckShift(currDate, loginShiftDateTime);
                            }
                        }

                        //if (loginAttempt < 11)
                        //{
                        if (shiftPass)
                        {
                            if (!string.IsNullOrWhiteSpace(main.UserAccessLevel))
                            {
                                main.UserName = Username.Text;
                                if (main.UserAccessLevel != "Administrator")
                                {
                                    loginAttempt++;
                                    //Update db
                                    main.SQLer.Exec_NonQuery($"UPDATE [Users] SET [LastLoginDateTime] = '{currDate}' WHERE [UserName] = '{Username.Text}';");
                                }
                                Username.Text = "";
                                Password.Text = "";
                                Hide();

                                Messenger.Default.Send<Tuple<SignInView, string>, Main>(
                                    new Tuple<SignInView, string>(this, main.UserAccessLevel));
                            }
                            else
                                this.Password.ucLabelErrorContent = this.TryFindResource("LOGIN_TEXTBLOCK_INVALID").ToString(); 
                        }
                        else
                        {
                            if (!string.IsNullOrWhiteSpace(main.UserAccessLevel))
                            {
                                MessageBox.Show($"Current Shift: {currShiftStartTime} - {currShiftEndTime}. This user account is belong to another shift. Please contact Administrator to reset shift in [User Account] page");
                            }
                            else
                            {
                                this.Password.ucLabelErrorContent = this.TryFindResource("LOGIN_TEXTBLOCK_INVALID").ToString();
                            }
                        }
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
            if (StrName == "penta" && strPassword == "#Penta382")
            {
                main.UserAccessLevel = "Penta";
                return true;
            }
            else if (StrName == "jabiladmin" && strPassword == "#penta5196")
            {
                main.UserAccessLevel = "Administrator";
                return true;
            }
            else if (StrName == "plexusadmin" && strPassword == "#penta5196")
            {
                main.UserAccessLevel = "Administrator";
                return true;
            }
            else if (StrName == "luxshareadmin" && strPassword == "#penta5196")
            {
                main.UserAccessLevel = "Administrator";
                return true;
            }
            main.UserAccessLevel = string.Empty;
            return false;
        }

        private DateTime FirstTimeLoginShift(DateTime now)
        {
            try
            {
                string shiftStart = main.SQLer.Exec_Scalar<string>($"SELECT [KeyValue] FROM WORKSHIFTSETTING WHERE KeyName = 'ShiftHour';");
                string hourFromCurrTime = now.ToString("HH");
                //string nowShiftStart = string.Empty;
                //bool shiftPrevDay = false;

                int numOfShift = main.SQLer.Exec_Scalar<int>($"SELECT [KeyValue] FROM WORKSHIFTSETTING WHERE KeyName = 'NoOfShiftPerDay';");
                int durationOfPerShift = 24 / numOfShift;

                if (shiftStart.Length < 2)
                {
                    shiftStart = "0" + shiftStart;
                }
                string nowShiftStart = DateTime.Now.Date.ToString("yyyy/MM/dd") + "_" + shiftStart;

                //if (Convert.ToInt32(hourFromCurrTime) < Convert.ToInt32(shiftStart))
                //{
                //    if (Convert.ToInt32(shiftStart) - durationOfPerShift < 0)
                //    {
                //        if (Convert.ToInt32(hourFromCurrTime) - durationOfPerShift < 0)
                //        {
                //            shiftPrevDay = true;
                //        }
                //    }
                //}

                //if (!shiftPrevDay)
                //{
                //    nowShiftStart = DateTime.Now.Date.ToString("yyyy/MM/dd") + "_" + shiftStart;
                //}
                //else
                //{
                //    DateTime PrevDate = DateTime.Now.AddDays(-1);
                //    nowShiftStart = PrevDate.ToString("yyyy/MM/dd") + "_" + shiftStart;
                //}

                DateTime shiftStartTime = DateTime.ParseExact(nowShiftStart, "yyyy/MM/dd_HH", CultureInfo.InvariantCulture);

                if (now < shiftStartTime)
                {
                    shiftStartTime = shiftStartTime.AddDays(-1);
                }

                if (numOfShift == 2)
                {
                    DateTime shift2 = shiftStartTime.AddHours(durationOfPerShift);
                    if (now >= shift2)
                    {
                        return shift2;
                    }
                    else if (now < shiftStartTime)
                    {
                        return shift2;
                    }
                    else if (now >= shiftStartTime && now < shift2)
                    {
                        return shiftStartTime;
                    }
                }
                else if (numOfShift == 3)
                {
                    DateTime shift2 = shiftStartTime.AddHours(durationOfPerShift);
                    DateTime shift3 = shift2.AddHours(durationOfPerShift);
                    if (now >= shift3)
                    {
                        return shift3;
                    }
                    else if (now < shiftStartTime)
                    {
                        return shift3;
                    }
                    else if (now >= shift2 && now < shift3)
                    {
                        return shift2;
                    }
                    else if (now >= shiftStartTime && now < shift2)
                    {
                        return shiftStartTime;
                    }
                }
                else
                {
                    DateTime shift2 = shiftStartTime.AddHours(durationOfPerShift);
                    DateTime shift3 = shift2.AddHours(durationOfPerShift);
                    DateTime shift4 = shift3.AddHours(durationOfPerShift);
                    if (now >= shift4)
                    {
                        return shift4;
                    }
                    else if (now < shiftStartTime)
                    {
                        return shift4;
                    }
                    else if (now >= shift3 && now < shift4)
                    {
                        return shift3;
                    }
                    else if (now >= shift2 && now < shift3)
                    {
                        return shift2;
                    }
                    else if (now >= shiftStartTime && now < shift2)
                    {
                        return shiftStartTime;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError("[FirstTimeLoginShift]", ex.Message);
                //throw;
            }
            return default(DateTime);
        }

        private bool CheckShift(DateTime now, DateTime startShiftTime)
        {
            bool pass = false;

            try
            {
                int numOfShift = main.SQLer.Exec_Scalar<int>($"SELECT [KeyValue] FROM WORKSHIFTSETTING WHERE KeyName = 'NoOfShiftPerDay';");
                int duration = 24 / numOfShift;
                DateTime shiftStartTimeCalcFromDb = FirstTimeLoginShift(now);
                DateTime endShiftTime = shiftStartTimeCalcFromDb.AddHours(duration);
                DateTime new_startShiftTime = startShiftTime;
                currShiftEndTime = shiftStartTimeCalcFromDb.AddHours(duration).AddMinutes(-1).ToString("HH:mm");
                currShiftStartTime = shiftStartTimeCalcFromDb.ToString("HH:mm");

                if (now.Date == startShiftTime.Date)
                {
                    //if (now >= startShiftTime && now < endShiftTime)
                    //{
                    //    pass = true;
                    //}
                    //else
                    //{
                    //    pass = false;
                    //} 

                    if (startShiftTime != shiftStartTimeCalcFromDb)
                    {
                        pass = false;
                    }
                    else
                    {
                        pass = true;
                    }
                }
                else if (now.Date > startShiftTime.Date)
                {
                    int reset = main.SQLer.Exec_Scalar<int>($"SELECT [ResetLoginShift] FROM USERS WHERE [UserName] = '{Username.Text}';");
                    if (reset == 0)
                    {
                        bool nextdayButYtdShift = false;
                        if (now < endShiftTime)
                        {
                            if (startShiftTime == shiftStartTimeCalcFromDb)
                            {
                                nextdayButYtdShift = true;
                                pass = true; 
                            }
                        }

                        if (!nextdayButYtdShift)
                        {
                            startShiftTime = startShiftTime.AddDays(1);
                            if (startShiftTime != shiftStartTimeCalcFromDb)
                            {
                                string startShiftTimeStr = startShiftTime.ToString("HH:mm");
                                string shiftStartTimeCalcDbStr = shiftStartTimeCalcFromDb.ToString("HH:mm");

                                double shiftDuration = (shiftStartTimeCalcFromDb - new_startShiftTime).TotalDays;

                                if (shiftDuration <= 1)
                                {
                                    if (startShiftTimeStr != shiftStartTimeCalcDbStr)
                                    {
                                        pass = false;
                                    }
                                    else
                                    {
                                        main.SQLer.Exec_NonQuery($"UPDATE [Users] SET [LoginShiftDateTime] = '{shiftStartTimeCalcFromDb}' WHERE [UserName] = '{Username.Text}';");
                                        pass = true;
                                    }
                                }
                                else
                                {
                                    main.SQLer.Exec_NonQuery($"UPDATE [Users] SET [LoginShiftDateTime] = '{shiftStartTimeCalcFromDb}' WHERE [UserName] = '{Username.Text}';");
                                    pass = true;
                                }

                            }
                            else
                            {
                                main.SQLer.Exec_NonQuery($"UPDATE [Users] SET [LoginShiftDateTime] = '{startShiftTime}' WHERE [UserName] = '{Username.Text}';");
                                pass = true;
                            }
                        } 
                    }
                    else if (reset == 1)
                    {
                        main.SQLer.Exec_NonQuery($"UPDATE [Users] SET [LoginShiftDateTime] = '{shiftStartTimeCalcFromDb}', [ResetLoginShift] = '0' WHERE [UserName] = '{Username.Text}';");
                        pass = true;
                    }
                }
            }
            catch (Exception ex)
            {
                pass = false;
                FileLogger.logError("[CheckShift]", ex.Message);
                MessageBox.Show($"Exception Catch - Error: {ex.Message}");
            }

            return pass;
        }

        #endregion PrivateMethods
    }
}