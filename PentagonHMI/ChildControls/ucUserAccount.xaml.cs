using System;
using System.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace PentagonHMI.ChildControls
{
    public partial class ucUserAccount : UserControl, IDisposable
    {
        private VanillaDB.DataDBCall DBCall;
        private string ErrMsg = "";
        private string StrAccount = "UserAccount";
        private LogicClasses.Main _Main;

        #region Variables

        private DataSet ds = new DataSet();
        private string UserName = null;
        private bool change;
        private bool deluser;

        #endregion Variables

        #region Constructor

        public ucUserAccount(ref LogicClasses.Main _main)
        {
            InitializeComponent();
            _Main = _main;
            string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
            DBCall = new VanillaDB.DataDBCall(Connstr);

            DataTable dt = new DataTable();
            DataTable dtuser = new DataTable();
            dtuser = DBCall.Login_Select("", ref ErrMsg);

            if(dtuser != null)
            {
                dtuser.Columns.Remove("Password");
                this.dgLog.ItemsSource = dtuser.DefaultView;
            }
            dgLog.SelectionChanged += new SelectionChangedEventHandler(dgLog_SelectionChanged);
        }

        private void dgLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dgLog.SelectedItem;
            if (item != null)
            {
                UserName = (dgLog.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                txtUserid1.Text = UserName;
                txtUseridShift.Text = UserName;
            }
        }

        #endregion Constructor

        #region FormEvents

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(StrAccount, $"Add User [{txtUserid.Text}]", MethodBase.GetCurrentMethod().ToString());

            if(txtUserid.Text.Length == 0)
            {
                MessageBox.Show("User Name Should not be Empty");
                return;
            }

            if(userlevel.Text.Length == 0)
            {
                MessageBox.Show("Please Select a User Level");
                return;
            }

            if(txtpassword.Password.Length == 0)
            {
                MessageBox.Show("Password Cannot Be Empty");
                return;
            }

            if(txtCpassword.Password != txtpassword.Password)
            {
                MessageBox.Show("Password Does Not Match");
                return;
            }

            DataTable dt = new DataTable();
            dt = DBCall.Login_Select(txtUserid.Text, ref ErrMsg);
            if(dt.Rows.Count > 0)
            {
                MessageBox.Show("UserName Already Exists");
            }
            else
            {
                _Main.SQLer.Exec_NonQuery($"INSERT INTO [dbo].[Users]([UserName],[Password] ,[Level]) VALUES ('{txtUserid.Text}','{txtpassword.Password}','{userlevel.Text}')");
                MessageBox.Show("New User Added");
                Updategrid();

                txtUserid.Text = string.Empty;

                txtpassword.Password = string.Empty;

                txtCpassword.Password = string.Empty;

                userlevel.Text = string.Empty;
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            string errMsg = "";
            Utilities.FileLogger.logButton(StrAccount, $"Change Password [{txtUserid1.Text}]", MethodBase.GetCurrentMethod().ToString());
            if(txtUserid1.Text == null || txtUserid1.Text == "" || txtPass1.Password == null || txtPass1.Password == "" || txtPass1.Password == null || txtPass1.Password == "" || txtNewPass1.Password == null || txtNewPass1.Password == "")
            {
                Utilities.FileLogger.logButton(StrAccount, $"Change Password [{txtUserid1.Text}] Error: Please fill in all the information.", MethodBase.GetCurrentMethod().ToString());
                MessageBox.Show("Please fill in all the information!");
                return;
            }
            DataTable dt = new DataTable();
            dt = DBCall.Login_DisplayPass(txtPass1.Password, txtUserid1.Text, ref errMsg);
            if(dt.Rows.Count != 0)
            {
            }
            else
            {
                Utilities.FileLogger.logButton(StrAccount, $"Change Password [{txtUserid1.Text}] Error: Wrong Old password has been filled in.", MethodBase.GetCurrentMethod().ToString());
                MessageBox.Show("Wrong Old password has been filled in!");
                return;
            }
            if(txtNewPass1.Password == txtCNewPass1.Password)
            {
                change = DBCall.Login_ChangePass(txtUserid1.Text, txtPass1.Password, txtNewPass1.Password, ref errMsg);
                Utilities.FileLogger.logButton(StrAccount, $"Change Password [{txtUserid1.Text}] Success.", MethodBase.GetCurrentMethod().ToString());
                MessageBox.Show("Password Changed successfully");
                Updategrid();
            }
            else
            {
                Utilities.FileLogger.logButton(StrAccount, $"Change Password [{txtUserid1.Text}] Error: New Password Didnt Match.", MethodBase.GetCurrentMethod().ToString());
                MessageBox.Show("New Password Didnt Match");
            }
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(StrAccount, $"Remove User [{UserName}]", MethodBase.GetCurrentMethod().ToString());

            if(UserName != null)
            {
                deluser = DBCall.Login_Delete(UserName);
                if(deluser == true)
                {
                    MessageBox.Show("User Had Been Deleted");
                    UserName = null;
                    Updategrid();
                }
                else
                    MessageBox.Show("Fail to delete user");
            }
            else
            {
                Utilities.FileLogger.logButton(StrAccount, $"Change Password [{txtUserid1.Text}] Error: No User Selected.", MethodBase.GetCurrentMethod().ToString());
                MessageBox.Show("No User Selected");
            }
        }

        private void btnResetAttempt_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(StrAccount, $"Reset User [{txtUseridShift.Text}] Shift", MethodBase.GetCurrentMethod().ToString());
            Utilities.FileLogger.logUser("[HMI]", $"[HMI] UserAccessLevel: {_Main.UserAccessLevel} | {_Main.UserName} | Action: Reset User [{txtUseridShift.Text}] Shift");

            try
            {
                if (!string.IsNullOrEmpty(txtUseridShift.Text))
                {
                    DataTable dt = new DataTable();
                    dt = DBCall.Login_Select(txtUseridShift.Text, ref ErrMsg);
                    if (dt != null)
                    {
                        string dateTimeStr = _Main.SQLer.Exec_Scalar<string>($"SELECT [LoginShiftDateTime] FROM USERS WHERE USERNAME = '{txtUseridShift.Text}'");
                        if (!string.IsNullOrEmpty(dateTimeStr))
                        {
                            DateTime prevDateTime = DateTime.Parse(dateTimeStr).AddDays(-1);
                            _Main.SQLer.Exec_NonQuery($"UPDATE [Users] SET [LoginShiftDateTime] = '{prevDateTime}', [ResetLoginShift] = '1' WHERE [UserName] = '{txtUseridShift.Text}'");
                            MessageBox.Show("User Login Shift Reset Successful");
                        }
                        else
                        {
                            MessageBox.Show("User Login Shift Reset Successful");
                        }
                    }
                    else
                    {
                        MessageBox.Show("User Key In Not Matched with Any User From Existing User List");
                    }
                }
                else
                {
                    MessageBox.Show("User Id Empty");
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError("[UserAccountPage]", $"Reset Shift - {ex.Message}");
                MessageBox.Show("User Login Shift Reset Fail");
            }
        }

        #endregion FormEvents

        #region Events

        private void Updategrid()
        {
            DataTable dtg = DBCall.Login_Select("", ref ErrMsg);
            dtg.Columns.Remove("Password");
            this.dgLog.ItemsSource = dtg.DefaultView;
        }

        #endregion Events

        #region Methods

        public void Dispose()
        {
        }

        #endregion Methods

        #region Destructor

        ~ucUserAccount()
        {
            Dispose();
        }

        #endregion Destructor
    }
}