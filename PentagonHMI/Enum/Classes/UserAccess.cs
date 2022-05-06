using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PentagonHMI.Classes;
using System.Windows;
using System.IO;
namespace PentagonHMI.Classes
{
    public static  class UserAccess
    {
        public static string gl_UserName = "Operator";
        //public static string DefaultUserName = "Anonymous";
        public static string DefaultUserName = "Operator";
        public static string UserGroup = "";
        public static int intlogoutTimeSec = 0;

        //private static DataTable dtUsers;

        public static Visibility SetVisible(bool Setting)
        {
            if (Setting == true)
            { return Visibility.Visible; }
            else
            { return Visibility.Collapsed; }
        }
        
        /*
         * Date: 2013-02-05
         * Author: BK 
         * Description: Verify password.
         */
        public static bool verifyPassword(string password, ref string ErrorMessage)
        {
            string value;
            using (StreamReader sr = new StreamReader("pass.log", Encoding.Default))
            {
                    value = sr.ReadToEnd().ToString().Trim();
            }
            if (value == password)
            {
                gl_UserName = "Engineer";
                return true;
            }
            else
            {
                return false;
            }
        }

        //public static bool GetUserRight(string username, string password,ref string ErrorMessage)
        //{
        //    dtUsers = Serialization.Get("Users") != null ? (DataTable)Serialization.Get("Users") : null;
        //    if (dtUsers == null)
        //    {
        //        dtUsers = new DataTable();
        //        dtUsers.Columns.Add("UserName");
        //        dtUsers.Columns.Add("Password");
        //        dtUsers.Columns.Add("UserGroup");
        //        //dtUsers.Columns.Add("CreatedTime");

        //        DataRow dr = dtUsers.NewRow();
        //        dr["UserName"] = "SuperAdmin";
        //        dr["Password"] = "superAdmin";
        //        dr["UserGroup"] = "Admin";

        //        dtUsers.Rows.Add(dr);

        //        dr = dtUsers.NewRow();
        //        dr["UserName"] = "Engineer ";
        //        dr["Password"] = "Engineer";
        //        dr["UserGroup"] = "Engineer";

        //        dtUsers.Rows.Add(dr);

        //        dr = dtUsers.NewRow();
        //        dr["UserName"] = "Operator";
        //        dr["Password"] = "Operator";
        //        dr["UserGroup"] = "Operator";

        //        dtUsers.Rows.Add(dr);
        //        Serialization.Update("Users", dtUsers);
        //    }
        //    //string SQLStatement = "SELECT UserName,Password,GroupName ";
        //    //SQLStatement += " from  Access_UserRights WHERE UserName = '" + username + "' ";
        //    DataRow[] DrUser = dtUsers.Select("UserName ='" + username + "'");
        //    bool AccessRight = false;

        //    if (DrUser.Length > 0)
        //    {
        //        if (DrUser[0]["Password"].ToString().Trim() == password)
        //        {
        //            AccessRight = true;
        //            UserGroup = DrUser[0]["UserGroup"].ToString().Trim();
        //            //SetUserRight(UserGroup, ref ErrorMessage);
        //            gl_UserName = username;
        //        }
        //        else
        //        { 
        //            ErrorMessage = "Invalid User Name or Password!"; 
        //        }
        //    }
        //    else
        //    { 
        //        ErrorMessage = "Invalid User Name or Password!"; 
        //    }

        //    //DataTable dtResult = new DataTable();
        //    //dtResult = GlobalFunctions.QueryMultiple(GlobalFunctions.gl_StrConn, SQLStatement, ref ErrorMessage);
        //    //if (dtResult != null)
        //    //{
        //    //    if (dtResult.Rows.Count > 0)
        //    //    {
        //    //        if (dtResult.Rows[0]["Password"].ToString().Trim() == password)
        //    //        {
        //    //            AccessRight = true;
        //    //            UserGroup = dtResult.Rows[0]["GroupName"].ToString().Trim();
        //    //            SetUserRight(UserGroup, ref ErrorMessage);
        //    //            gl_UserName = username;
        //    //        }
        //    //        else
        //    //        { ErrorMessage = "Invalid User Name or Password!"; }
        //    //    }
        //    //    else
        //    //    { ErrorMessage = "Invalid User Name or Password!"; }
        //    //}
        //    //else
        //    //{ ErrorMessage = "Invalid User Name or Password!"; }
        //    return AccessRight;
        //}

        //private static void SetUserRight(string GroupName,ref string ErrorMessage)
        //{
        //    string SQLStatement = "SELECT Module,Vision,MachineSetting,RecipeSetting,Statistic ";
        //    SQLStatement += " ,MiddlewareSettingEdit,MiddlewareSettingView,SingulationEdit,SingulationView,UPCEdit,UPCView ";
        //    SQLStatement += " ,FNBEdit,FNBView,SortingView,SortingEdit,UserAccessEdit,UserAccessView ";
        //    SQLStatement += " ,UserManagementEdit,UserManagementView,AlarmEdit,AlarmView,AlertView ";
        //    SQLStatement += " ,AlertEdit,IOInputView,IOOutputView,OEEView,YieldView,LogView,AlarmLogView,LogEdit,AlarmLogEdit,LabelEdit,LabelView ";
        //    SQLStatement += " ,ByPassEdit,ByPassView ";
        //    SQLStatement += " FROM Access_GroupRights With (NoLock)";
        //    SQLStatement += " WHERE GroupName = '" + GroupName + "' ";
           
        //    DataTable dtResult = new DataTable();
        //    dtResult = GlobalFunctions.QueryMultiple(GlobalFunctions.gl_StrConn, SQLStatement, ref ErrorMessage);
        //    if (dtResult != null)
        //    {
        //        if (dtResult.Rows.Count > 0)
        //        {
        //            if (dtResult.Rows[0]["Module"].ToString().Trim() == "True")
        //            { Module = true; }
        //            else { Module = false; }

        //            if (dtResult.Rows[0]["Vision"].ToString().Trim() == "True")
        //            { Vision = true; }
        //            else { Vision = false; }

        //            if (dtResult.Rows[0]["MachineSetting"].ToString().Trim() == "True")
        //            { MachineSetting = true; }
        //            else { MachineSetting = false; }

        //            if (dtResult.Rows[0]["RecipeSetting"].ToString().Trim() == "True")
        //            { RecipeSetting = true; }
        //            else { RecipeSetting = false; }

        //            if (dtResult.Rows[0]["Statistic"].ToString().Trim() == "True")
        //            { Statistic = true; }
        //            else { Statistic = false; }

        //            if (dtResult.Rows[0]["MiddlewareSettingEdit"].ToString().Trim() == "True")
        //            { MiddlewareSettingEdit = true; }
        //            else { MiddlewareSettingEdit = false; }

        //            if (dtResult.Rows[0]["MiddlewareSettingView"].ToString().Trim() == "True")
        //            { MiddlewareSettingView = true; }
        //            else { MiddlewareSettingView = false; }


        //            if (dtResult.Rows[0]["SingulationEdit"].ToString().Trim() == "True")
        //            { SingulationEdit = true; }
        //            else { SingulationEdit = false; }

        //            if (dtResult.Rows[0]["SingulationView"].ToString().Trim() == "True")
        //            { SingulationView = true; }
        //            else { SingulationView = false; }

        //            if (dtResult.Rows[0]["UPCEdit"].ToString().Trim() == "True")
        //            { UPCEdit = true; }
        //            else { UPCEdit = false; }

        //            if (dtResult.Rows[0]["UPCView"].ToString().Trim() == "True")
        //            { UPCView = true; }
        //            else { UPCView = false; }

        //            if (dtResult.Rows[0]["FNBEdit"].ToString().Trim() == "True")
        //            { FNBEdit = true; }
        //            else { FNBEdit = false; }

        //            if (dtResult.Rows[0]["FNBView"].ToString().Trim() == "True")
        //            { FNBView = true; }
        //            else { FNBView = false; }

        //            if (dtResult.Rows[0]["SortingEdit"].ToString().Trim() == "True")
        //            { SortingEdit = true; }
        //            else { SortingEdit = false; }

        //            if (dtResult.Rows[0]["SortingView"].ToString().Trim() == "True")
        //            { SortingView = true; }
        //            else { SortingView = false; }

        //            if (dtResult.Rows[0]["UserAccessEdit"].ToString().Trim() == "True")
        //            { UserAccessEdit = true; }
        //            else { UserAccessEdit = false; }

        //            if (dtResult.Rows[0]["UserAccessView"].ToString().Trim() == "True")
        //            { UserAccessView = true; }
        //            else { UserAccessView = false; }

        //            if (dtResult.Rows[0]["UserManagementEdit"].ToString().Trim() == "True")
        //            { UserManagementEdit = true; }
        //            else { UserManagementEdit = false; }

        //            if (dtResult.Rows[0]["UserManagementView"].ToString().Trim() == "True")
        //            { UserManagementView = true; }
        //            else { UserManagementView = false; }

        //            if (dtResult.Rows[0]["AlarmEdit"].ToString().Trim() == "True")
        //            { AlarmEdit = true; }
        //            else { AlarmEdit = false; }

        //            if (dtResult.Rows[0]["AlarmView"].ToString().Trim() == "True")
        //            { AlarmView = true; }
        //            else { AlarmView = false; }

        //            if (dtResult.Rows[0]["AlertEdit"].ToString().Trim() == "True")
        //            { AlarmView = true; }
        //            else { AlarmView = false; }

        //            if (dtResult.Rows[0]["AlertView"].ToString().Trim() == "True")
        //            { AlertView = true; }
        //            else { AlertView = false; }

        //            if (dtResult.Rows[0]["IOInputView"].ToString().Trim() == "True")
        //            { IOInputView = true; }
        //            else { IOInputView = false; }

        //            if (dtResult.Rows[0]["IOOutputView"].ToString().Trim() == "True")
        //            { IOOutputView = true; }
        //            else { IOOutputView = false; }

        //            if (dtResult.Rows[0]["OEEView"].ToString().Trim() == "True")
        //            { OEEView = true; }
        //            else { OEEView = false; }

        //            if (dtResult.Rows[0]["YieldView"].ToString().Trim() == "True")
        //            { YieldView = true; }
        //            else { YieldView = false; }

        //            if (dtResult.Rows[0]["LogView"].ToString().Trim() == "True")
        //            { LogView = true; }
        //            else { LogView = false; }

        //            if (dtResult.Rows[0]["LogEdit"].ToString().Trim() == "True")
        //            { LogEdit = true; }
        //            else { LogEdit = false; }

        //            if (dtResult.Rows[0]["AlarmLogView"].ToString().Trim() == "True")
        //            { AlarmLogView = true; }
        //            else { AlarmLogView = false; }

        //            if (dtResult.Rows[0]["AlarmLogEdit"].ToString().Trim() == "True")
        //            { AlarmLogEdit = true; }
        //            else { AlarmLogEdit = false; }

        //            if (dtResult.Rows[0]["LabelView"].ToString().Trim() == "True")
        //            { LabelView = true; }
        //            else { LabelView = false; }

        //            if (dtResult.Rows[0]["LabelEdit"].ToString().Trim() == "True")
        //            { LabelEdit = true; }
        //            else { LabelEdit = false; }

        //            if (dtResult.Rows[0]["ByPassView"].ToString().Trim() == "True")
        //            { ByPassView = true; }
        //            else { ByPassView = false; }

        //            if (dtResult.Rows[0]["ByPassEdit"].ToString().Trim() == "True")
        //            { ByPassEdit = true; }
        //            else { ByPassEdit = false; }
        //        }
        //        else 
        //        {SetDefaultUserRight();}
        //    }
        //    else
        //    {
        //        SetDefaultUserRight();
        //    }

        //}
    }
}
