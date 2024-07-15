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
            if(item != null)
            {
                UserName = (dgLog.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text;
                txtUserid1.Text = UserName;
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
                MessageBox.Show("Wrong Old password has been filled in!");
                return;
            }
            if(txtNewPass1.Password == txtCNewPass1.Password)
            {
                change = DBCall.Login_ChangePass(txtUserid1.Text, txtPass1.Password, txtNewPass1.Password, ref errMsg);
                MessageBox.Show("Password Changed successfully");
                Updategrid();
            }
            else
            {
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
                MessageBox.Show("No User Selected");
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