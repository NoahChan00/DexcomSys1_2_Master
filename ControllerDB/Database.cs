using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.OleDb;
using System.Data;
using System.Collections;

namespace Database
{
    public class Database
    {
        private static string Connstr = "Provider=sqloledb;Data Source=172.16.31.125;Initial Catalog=gdb;User Id=sa;Password=123321;";

        #region ConnectionVariable
        //public static OleDbConnection conn =
        //    new OleDbConnection("Provider=sqloledb;Data Source=localhost;Initial Catalog=gdb;Integrated Security = SSPI;");
        public static OleDbConnection conn =
        new OleDbConnection("Provider=sqloledb;Data Source=172.16.31.125;Initial Catalog=gdb;User Id=sa;Password=123321;");

        public static OleDbCommand cmd = new OleDbCommand();
        public static OleDbCommand cmd2 = new OleDbCommand();
        public static OleDbDataReader dr;
        public static OleDbDataReader dr2;

        public static Dictionary<string, string> ErrDict;
        #endregion

        #region Initalize

        public static Dictionary<string, string> getErrorList()
        {
            Dictionary<string, string> tempDict = new Dictionary<string, string>();

            dr = Database.ExecuteQuery("SELECT * FROM Error_List");

            while (dr.Read())
            {
                tempDict.Add(dr["ErrCode"].ToString(), dr["ErrDesc"].ToString());
            }
            dr.Close();

            return tempDict;
        }

        #endregion

        public static int Connect()
        {
            try
            {
                if (!conn.State.Equals(ConnectionState.Open))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = conn;

                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = conn;

                    //cmdSP.CommandType = CommandType.StoredProcedure;
                    //cmdSP.Connection = conn;

                    conn.Open();

                }

                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static int ExecuteNonQuery(string SQL_cmd = "")
        {
            Connect();

            try
            {
                if (SQL_cmd != "")
                {
                    cmd.CommandText = SQL_cmd;
                    return cmd.ExecuteNonQuery();
                }
                return 0;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static DataTable ExecuteQuerySP(string ProcName, string[] ParamIn, string[] ParamInOut, ref int[] ValueOut)
        {
            OleDbCommand cmdSP = new OleDbCommand(ProcName);
            cmdSP.CommandType = CommandType.StoredProcedure;
            cmdSP.Connection = conn;

            OleDbParameter outParam = new OleDbParameter();
            outParam.OleDbType = System.Data.OleDb.OleDbType.Integer;
            outParam.Direction = System.Data.ParameterDirection.InputOutput;

            cmdSP.CommandType = CommandType.StoredProcedure;

            for (int p = 0; p < ParamIn.Length; p++)
            {
                cmdSP.Parameters.AddWithValue(ParamIn[p].Split('=')[0], ParamIn[p].Split('=')[1]);
            }

            for (int p = 0; p < ParamInOut.Length; p++)
            {
                outParam.ParameterName = ParamInOut[p].Split('=')[0];
                outParam.Value = ParamInOut[p].Split('=')[1];

                cmdSP.Parameters.Add(outParam);
            }

            DataTable dt = new DataTable();
            dt.Load(cmdSP.ExecuteReader());

            for (int o = 0; o < ParamInOut.Length; o++)
            {
                var x = cmdSP.Parameters[ParamInOut[o].Split('=')[0]].Value.ToString();

                ValueOut[o] = Convert.ToInt32(x);
            }

            return dt;
        }
        public static DataTable ExecuteQuerySP1(string ProcName, string[] ParamIn)
        {
            OleDbCommand cmdSP = new OleDbCommand(ProcName);
            cmdSP.CommandType = CommandType.StoredProcedure;
            cmdSP.Connection = conn;

            OleDbParameter outParam = new OleDbParameter();
            outParam.OleDbType = System.Data.OleDb.OleDbType.Integer;
            outParam.Direction = System.Data.ParameterDirection.InputOutput;

            cmdSP.CommandType = CommandType.StoredProcedure;

            for (int p = 0; p < ParamIn.Length; p++)
            {
                cmdSP.Parameters.AddWithValue(ParamIn[p].Split('=')[0], ParamIn[p].Split('=')[1]);
            }

            DataTable dt = new DataTable();
            dt.Load(cmdSP.ExecuteReader());


            return dt;
        }

        public static OleDbDataReader ExecuteQuery(string SQL_cmd = "")
        {
            Connect();

            if (SQL_cmd == "")
            {
                SQL_cmd = "SELECT * FROM Station ";
            }

            cmd.CommandText = SQL_cmd;
            dr = cmd.ExecuteReader();

            return dr;
        }

        public static bool Execute(ArrayList SQLStatementList)
        {
            int i;
            string ErrMsg;
            OleDbTransaction myTrans = null;
            OleDbCommand myCommand = null;
            OleDbConnection myConnection;


            myConnection = new OleDbConnection(Connstr);
            try
            {
                myConnection.Open();
                myTrans = myConnection.BeginTransaction(IsolationLevel.ReadCommitted);
                myCommand = myConnection.CreateCommand();
                myCommand.Connection = myConnection;
                myCommand.Transaction = myTrans;
                myCommand.CommandTimeout = 1200;
                for (i = 0; i <= (SQLStatementList.Count - 1); i++)
                {
                    myCommand.CommandText = SQLStatementList[i] as string;
                    myCommand.ExecuteNonQuery();
                }

                myTrans.Commit();
                return true;
            }
            catch (Exception ex)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (OleDbException exOleDB)
                {
                    if (myTrans.Connection != null)
                    {
                        ErrMsg = "* An exception of type " + exOleDB.GetType().ToString() + " was encountered while attempting to roll back the transaction.";
                    }

                    return false;
                }

                ErrMsg = "* An exception of type " + ex.GetType().ToString() + " was encountered while inserting the data. Neither record was written to database." + " Error=(" + ex.Message + ")";
                return false;
            }
            finally
            {
                if (myCommand != null)
                    myCommand = null;
                if (myTrans != null)
                    myTrans = null;
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
        }

        public static DataTable ExecuteQueryDT(string SQL_cmd = "")
        {
            Connect();

            if (SQL_cmd == "")
            {
                SQL_cmd = "SELECT * FROM Station";
            }

            cmd.CommandText = SQL_cmd;

            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());

            return dt;
        }

        public static OleDbDataReader ExecuteQuery2(string SQL_cmd = "")
        {
            Connect();

            if (SQL_cmd == "")
            {
                SQL_cmd = "SELECT * FROM Station";
            }

            cmd2.CommandText = SQL_cmd;
            dr2 = cmd2.ExecuteReader();

            return dr2;
        }

        public static string ExecuteScalar(string SQL_cmd = "")
        {
            Connect();

            cmd.CommandText = SQL_cmd;
            var result = cmd.ExecuteScalar();

            if (result != null)
            {
                return result.ToString();
            }
            else
            {
                return null;
            }

        }


        public string QuerySingle(string StoredProcedureName, ArrayList ParameterList, string MyConnStr, string DatabaseName = "", string ErrMsg = "")
        {
            int i;
            string strReturnVal = "";
            OleDbCommand myCommand = null;
            OleDbCommandBuilder myCommandBuilder = new OleDbCommandBuilder();
            OleDbConnection myConnection = null;
            try
            {
                myConnection = new OleDbConnection(MyConnStr);
                myConnection.Open();
                if (DatabaseName != "")
                    myConnection.ChangeDatabase(DatabaseName);
                myCommand = myConnection.CreateCommand();
                myCommand.Connection = myConnection;
                myCommand.CommandType = CommandType.StoredProcedure;
                myCommand.CommandText = StoredProcedureName;
                OleDbCommandBuilder.DeriveParameters(myCommand);
                if (ParameterList != null)
                {
                    if (myCommand.Parameters.Count - 1 != ParameterList.Count)
                    {
                        ErrMsg = "* Invalid Parameter List";
                        return "";
                    }

                    for (i = 0; i <= (ParameterList.Count - 1); i++)
                    {
                        myCommand.Parameters[i + 1].Value = ParameterList[i];
                    }
                }
                else
                {
                    if (myCommand.Parameters.Count - 1 != 0)
                    {
                        ErrMsg = "* Invalid Parameter List";
                        return "";
                    }
                }

                //strReturnVal = myCommand.ExecuteScalar.ToString();

                if (strReturnVal != null)
                {
                    return strReturnVal.ToString();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                ErrMsg = "* An exception of type " + ex.GetType().ToString() + " was encountered while running the query." + "ErrorMsg=(" + ex.Message + ")";
                return "";
            }
            finally
            {
                if (myCommandBuilder != null)
                    myCommandBuilder = null;
                if (myCommand != null)
                    myCommand = null;
                if (myConnection.State == ConnectionState.Open)
                    myConnection.Close();
            }
        }
        //=======================================================
        //Service provided by Telerik (www.telerik.com)
        //Conversion powered by Refactoring Essentials.
        //Twitter: @telerik
        //Facebook: facebook.com/telerik
        //=======================================================



        #region TableFunction

        public static bool isOrderIDExist(string orderID)
        {
            string Result = Database.ExecuteScalar("SELECT COUNT(*) OrderID FROM Orders WHERE OrderID='" + orderID + "'");

            if (Function.isNumeric(Result) && Convert.ToInt32(Result) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isOrderIDInUsed(string orderID)
        {
            string Result = Database.ExecuteScalar("SELECT COUNT(*) OrderID FROM Orders WHERE OrderID='" + orderID +
                                                                                                "'  AND Status <> 'Pending'");

            if (Function.isNumeric(Result) && Convert.ToInt32(Result) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool isOrder_AssetTagExist(string AssetTag)
        {
            string Result = Database.ExecuteScalar("SELECT COUNT(*) OrderID FROM Orders WHERE AssetTag='" + AssetTag + "'");

            if (Function.isNumeric(Result) && Convert.ToInt32(Result) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isRecipeExist(string Recipe, string PartModelID = "")
        {
            string Result;

            if (PartModelID == "")
            {
                Result = Database.ExecuteScalar("SELECT COUNT(*) OrderID FROM Orders WHERE Recipe='" + Recipe + "'");
            }
            else
            {
                Result = Database.ExecuteScalar("SELECT COUNT(*) OrderID FROM Orders WHERE Recipe='" + Recipe +
                                                                                           "' AND PartModelID='" + PartModelID + "'");
            }

            if (Function.isNumeric(Result) && Convert.ToInt32(Result) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool isPartNoExist(string[] Parts, string StationID = "")
        {
            string Result;
            string sql_Parts = "";

            //foreach (string p in Parts)
            //{
            //    if (sql_Parts == "")
            //    {
            //        sql_Parts = sql_Parts + Function.AddPrePostFix(p);
            //    }
            //    else
            //    {
            //        sql_Parts = sql_Parts + "," + Function.AddPrePostFix(p);
            //    }
            //}

            foreach (string p in Parts)
            {
                if (sql_Parts == "")
                {
                    // sql_Parts = sql_Parts + Function.AddPrePostFix(p);
                    sql_Parts = p;
                }
                else
                {
                    //   sql_Parts = sql_Parts + "," + Function.AddPrePostFix(p);
                    sql_Parts = sql_Parts + "," + p;
                }
            }

            if (StationID == "")
            {
                Result = Database.ExecuteScalar("select count(*) from(SELECT splitdata FROM fnSplitString('" + sql_Parts + "', ',') " +
                " where splitdata <> '' ) c left outer join parts p on c.splitdata = p.partmodelid where P.partmodelid is null");
            }
            else
            {
                Result = Database.ExecuteScalar("select count(*) from(SELECT splitdata FROM fnSplitString('" + sql_Parts + "', ',') " +
                " where splitdata <> '' ) c left outer join parts p on c.splitdata = p.partmodelid where P.partmodelid is null AND StationID='" + StationID + "'");
            }


            if (Result == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool isOrderTypeExist(int OrderType)
        {
            string Result = "";

            if (OrderType > 0 && OrderType <= 3)
            {
                Result = Database.ExecuteScalar("SELECT COUNT(*) Parts FROM OrdersType WHERE OrderTypeID = " + OrderType);
            }



            if (Function.isNumeric(Result) && Convert.ToInt32(Result) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static int getOrderStatus(string OrderID)
        {
            return Convert.ToInt32(Database.ExecuteScalar("SELECT Status From Orders WHERE OrderID='" + OrderID + "'"));
        }

        public static bool logMessage(string command, string QueryString, string IP = "")
        {
            if (command != "" && QueryString != "")
            {
                int Result = Database.ExecuteNonQuery("INSERT INTO Log(Command, QueryString, IP) " +
                                                "VALUES('" + command +
                                                     "','" + QueryString +
                                                     "','" + IP + "');");


                return (Result > 0) ? true : false;

            }

            return false;
        }

        public static void SetError(string ErrorCode, string ID, out bool success, out int oErrCode, out string ErrDesc)
        {
            #region Init
            if (Database.ErrDict == null)
            {
                Database.ErrDict = Database.getErrorList();
            }
            #endregion

            success = false;
            oErrCode = 0;
            ErrDesc = "";

            if (ErrorCode == "0")
            {
                success = true;
                oErrCode = Convert.ToInt16(ErrorCode);
                ErrDesc = ErrDict[ErrorCode];
            }
            else if (ErrorCode != "0" && ErrDict.ContainsKey(ErrorCode) && ID != "")
            {
                success = false;
                oErrCode = Convert.ToInt16(ErrorCode);
                ErrDesc = ErrDict[ErrorCode] + " (" + ID + ")";
            }
            else if (ErrorCode != "0" && ErrDict.ContainsKey(ErrorCode))
            {
                success = false;
                oErrCode = Convert.ToInt16(ErrorCode);
                ErrDesc = ErrDict[ErrorCode];
            }
            else
            {
                success = false;
                oErrCode = Convert.ToInt16(ErrorCode);
                ErrDesc = "Unknown Error";
            }

        }

        #endregion

    }
}
