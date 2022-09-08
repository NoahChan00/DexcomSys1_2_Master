using System;
using System.Collections;
using System.Data;
using System.Reflection;

namespace VanillaDB
{
    public class DataDBCall
    {
        private Database VDB;

        public DataDBCall(string strConn)
        {
            VDB = new Database(strConn);
        }

        public DataTable Select_UPH(string StationID, string Day, string Month, string Year, ref string ErrMsg)
        {
            DataTable dt = new DataTable();
            try
            {
                dt = VDB.ExecuteQueryDT($"EXEC Proc_UPH_Select '{StationID}','{Day}','{Month}','{Year}'", ref ErrMsg);
                return dt;
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
            finally
            {
                if(dt != null)
                    dt.Dispose();
            }
        }

        public DataTable Shift_Select(string ShiftID, string StationID, ref string ErrMsg)
        {
            long epochTime = Function.getEpochTime(DateTime.UtcNow);
            DataTable dt = new DataTable();
            string blnErrResult = "";
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                ErrMsg = "";
                dt = VDB.ExecuteQueryDT("EXEC Proc_Line_Shift_Select '" + ShiftID + "','" + StationID + "'", ref ErrMsg);

                return dt;
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                ErrMsg = ErrMsg + " (" + m.Name + ")";
                blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',''", ref ErrMsg);

                return null;
            }
            finally
            {
                if(dt != null)
                    dt.Dispose();
            }
        }

        public bool Log_UPH(int HrNow, string StationID, string UPH, ref string ErrMsg)
        {
            try
            {
                ErrMsg = "";
                string Query = $"EXEC Proc_UPH_Insert {UPH},{StationID},{HrNow},{DateTime.Now.Day},{DateTime.Now.Month},{DateTime.Now.Year}";

                VDB.ExecuteNonQuery(Query, ref ErrMsg);
                return ErrMsg.Length == 0 ? true : false;
            }
            catch(Exception ex)
            {
                ErrMsg = ex.Message;
                return false;
            }
        }

        public bool Shift_Reset(String ShiftID, string StationID, string NextShiftDT, string UserName, ref string ErrMsg)
        {
            long epochTime = Function.getEpochTime(DateTime.UtcNow);
            string blnErrResult = "";
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                ErrMsg = "";
                int _ReturnCount = VDB.ExecuteNonQuery("EXEC Proc_Line_Shift_Reset '" + ShiftID + "'" +
                    ",'" + StationID + "','" + NextShiftDT + "','" + UserName + "'"
                    , ref ErrMsg);

                if(ErrMsg != "")
                {
                    ErrMsg = ErrMsg + " (" + m.Name + ")";
                    blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',''", ref ErrMsg);
                }

                if(_ReturnCount == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                ErrMsg = ErrMsg + " (" + m.Name + ")";
                blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',''", ref ErrMsg);

                return false;
            }
        }

        public bool Shift_Update(String ShiftID, string StationID, string NextShiftDT, string ShiftTime, string Active, string UserName, ref string ErrMsg)
        {
            long epochTime = Function.getEpochTime(DateTime.UtcNow);
            string blnErrResult = "";
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                ErrMsg = "";
                int _ReturnCount = VDB.ExecuteNonQuery("EXEC Proc_Line_Shift_Update '" + ShiftID + "'" +
                    ",'" + StationID + "','" + NextShiftDT + "','" + ShiftTime + "','" + Active + "','" + UserName + "'"
                    , ref ErrMsg);

                if(ErrMsg != "")
                {
                    ErrMsg = ErrMsg + " (" + m.Name + ")";
                    blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',''", ref ErrMsg);
                }

                if(_ReturnCount == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                ErrMsg = ErrMsg + " (" + m.Name + ")";
                blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',''", ref ErrMsg);

                return false;
            }
        }

        public bool Alarm_Update(String AlmCode, String StationID, String ModuleCode, String AlmDesc, string AlmAction, string UserName, ref string ErrMsg)
        {
            long epochTime = Function.getEpochTime(DateTime.UtcNow);
            ErrMsg = "";
            bool blnresult = false;
            string strSQL = "";
            string blnErrResult = "";
            MethodBase m = MethodBase.GetCurrentMethod();
            ArrayList arrlist = new ArrayList();
            try
            {
                arrlist.Clear();

                strSQL = "EXEC Proc_Line_Alarms_Update  '" + AlmCode + "','" + StationID + "','" + ModuleCode + "','" + AlmDesc + "','" + AlmAction + "','" + epochTime.ToString() + "','" + UserName + "'";

                arrlist.Add(strSQL);

                blnresult = VDB.Execute(arrlist, ref ErrMsg);

                if(ErrMsg != "")
                {
                    ErrMsg = ErrMsg + " (" + m.Name + ")";
                    blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',''", ref ErrMsg);
                }

                return blnresult;
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                ErrMsg = ErrMsg + " (" + m.Name + ")";
                blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',''", ref ErrMsg);

                return false;
            }
            finally
            {
                if(arrlist != null)
                    arrlist = null;
            }
        }

        public DataTable Alarm_Select(String viewOption, String StationID, ref string ErrMsg)
        {
            long epochTime = Function.getEpochTime(DateTime.UtcNow);
            DataTable dt = new DataTable();
            MethodBase m = MethodBase.GetCurrentMethod();
            ErrMsg = "";
            string blnErrResult = "";
            try
            {
                dt = VDB.ExecuteQueryDT("EXEC Proc_Line_Alarms_Select '" + viewOption + "','" + StationID + "'", ref ErrMsg);

                if(ErrMsg != "")
                {
                    ErrMsg = ErrMsg + " (" + m.Name + ")";
                    blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',0", ref ErrMsg);
                }

                return dt;
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                ErrMsg = ErrMsg + " (" + m.Name + ")";
                blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',0", ref ErrMsg);

                return null;
            }
            finally
            {
                if(dt != null)
                    dt.Dispose();
            }
        }

        public DataTable Login_DisplayPass(string Password, string UserName, ref string ErrMsg)
        {
            long epochTime = Function.getEpochTime(DateTime.UtcNow);
            DataTable dt = new DataTable();
            string blnErrResult = "";
            MethodBase m = MethodBase.GetCurrentMethod();
            string strSQL;
            ErrMsg = "";
            try
            {
                strSQL = "EXEC Proc_Line_User_Validate '" + UserName + "','" + Password + "'";
                dt = VDB.ExecuteQueryDT(strSQL, ref ErrMsg);

                if(ErrMsg != "")
                {
                    ErrMsg = ErrMsg + " (" + m.Name + ")";
                    blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "','-1'", ref ErrMsg);
                }

                return dt;
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                ErrMsg = ErrMsg + " (" + m.Name + ")";
                blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "','-1'", ref ErrMsg);

                return null;
            }
            finally
            {
                if(dt != null)
                    dt.Dispose();
            }
        }

        public bool Login_ChangePass(string UserName, string OldPassword, string NewPassword, ref String ErrMsg)
        {
            ErrMsg = "";
            long epochTime = Function.getEpochTime(DateTime.UtcNow);
            string blnErrResult = "";
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                int _ResultCount = VDB.ExecuteNonQuery("EXEC Proc_Line_User_ChangePassword '" + UserName + "','" + OldPassword + "','" + NewPassword + "'", ref ErrMsg);

                if(ErrMsg != "")
                {
                    ErrMsg = ErrMsg + " (" + m.Name + ")";
                    blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',10", ref ErrMsg);
                }

                return (_ResultCount == 1);
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                ErrMsg = ErrMsg + " (" + m.Name + ")";
                blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',10", ref ErrMsg);

                return false;
            }
        }

        public bool Login_Delete(string UserName)
        {
            string ErrMsg = "";
            long epochTime = Function.getEpochTime(DateTime.UtcNow);
            string blnErrResult = "";
            MethodBase m = MethodBase.GetCurrentMethod();
            try
            {
                int _ResultCount = VDB.ExecuteNonQuery("EXEC Proc_Line_User_Delete '" + UserName + "'", ref ErrMsg);

                if(ErrMsg != "")
                {
                    ErrMsg = ErrMsg + " (" + m.Name + ")";
                    blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',10", ref ErrMsg);
                }

                return true;
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                ErrMsg = ErrMsg + " (" + m.Name + ")";
                blnErrResult = VDB.ExecuteScalar("EXEC Proc_Error_Insert '" + epochTime.ToString() + "','1207','" + ErrMsg + "',10", ref ErrMsg);

                return false;
            }
        }

        public DataTable Login_Select(string UserName, ref string ErrMsg)
        {
            long epochTime = Function.getEpochTime(DateTime.UtcNow);
            DataTable dt = new DataTable();
            ErrMsg = "";
            try
            {
                dt = VDB.ExecuteQueryDT("EXEC Proc_Line_User_Select '" + UserName + "'", ref ErrMsg);
                return dt;
            }
            catch(Exception e)
            {
                ErrMsg = e.Message;
                return null;
            }
            finally
            {
                if(dt != null)
                    dt.Dispose();
            }
        }

        public class Function
        {
            public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            public static long getEpochTime(DateTime datetime)
            {
                DateTime dateTimeUtc = datetime;
                if(datetime.Kind != DateTimeKind.Utc)
                {
                    dateTimeUtc = datetime.ToUniversalTime();
                }

                if(dateTimeUtc.ToUniversalTime() <= UnixEpoch)
                {
                    return 0;
                }

                return (long)(dateTimeUtc - UnixEpoch).TotalMilliseconds;
            }
        }
    }
}