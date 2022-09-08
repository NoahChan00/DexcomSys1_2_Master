using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace VanillaDB
{
    public class Database
    {
        private string Connstr;
        public Dictionary<string, string> ErrDict;

        public Database(string _Connstr)
        {
            Connstr = _Connstr;
        }

        public string ExecuteScalar(string SQL_cmd, ref String ErrMsg)
        {
            SqlTransaction myTrans = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Connstr);

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                conn.Open();
                myTrans = conn.BeginTransaction();
                cmd.Transaction = myTrans;
                cmd.CommandTimeout = 4;
                cmd.CommandText = SQL_cmd;
                var result = cmd.ExecuteScalar();

                myTrans.Commit();

                if(result != null)
                {
                    return result.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                try
                {
                    if(myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                }
                catch(SqlException exsqlDB)
                {
                    if(myTrans.Connection != null)
                    {
                        ErrMsg = "* An exception of type " + exsqlDB.GetType().ToString() + " was encountered while attempting to roll back the transaction.";
                    }
                }
                ErrMsg = "* An exception of type " + ex.GetType().ToString() + " was encountered while running the query." + "ErrorMsg=(" + ex.Message + ")";
                return "";
            }
            finally
            {
                if(myTrans != null)
                    myTrans = null;
                if(cmd != null)
                    cmd = null;
                if(conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool Execute(ArrayList SQLStatementList, ref string ErrMsg)
        {
            int i;
            ErrMsg = "";

            SqlTransaction myTrans = null;
            SqlCommand myCommand = new SqlCommand();
            SqlConnection conn = new SqlConnection(Connstr);

            try
            {
                conn.Open();
                myTrans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                myCommand = conn.CreateCommand();
                myCommand.Connection = conn;

                myCommand.Transaction = myTrans;
                myCommand.CommandTimeout = 4;
                for(i = 0; i <= (SQLStatementList.Count - 1); i++)
                {
                    myCommand.CommandText = SQLStatementList[i] as string;
                    myCommand.ExecuteNonQuery();
                }

                myTrans.Commit();
                return true;
            }
            catch(Exception ex)
            {
                try
                {
                    if(myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                }
                catch(SqlException exsqlDB)
                {
                    if(myTrans.Connection != null)
                    {
                        ErrMsg = "* An exception of type " + exsqlDB.GetType().ToString() + " was encountered while attempting to roll back the transaction.";
                    }

                    return false;
                }

                ErrMsg = "* An exception of type " + ex.GetType().ToString() + " was encountered while inserting the data. Neither record was written to database." + " Error=(" + ex.Message + ")";
                return false;
            }
            finally
            {
                if(myCommand != null)
                    myCommand = null;
                if(myTrans != null)
                    myTrans = null;
                if(conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public int ExecuteNonQuery(string SQL_cmd, ref String ErrMsg)
        {
            SqlTransaction myTrans = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Connstr);
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                conn.Open();
                myTrans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
                cmd.Transaction = myTrans;
                cmd.CommandTimeout = 4;

                if(SQL_cmd != "")
                {
                    cmd.CommandText = SQL_cmd;
                    cmd.ExecuteNonQuery();
                }

                myTrans.Commit();
                return 1;
            }
            catch(Exception ex)
            {
                try
                {
                    if(myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                }
                catch(SqlException exSql)
                {
                    if(myTrans.Connection != null)
                    {
                        ErrMsg = "* An exception of type " + exSql.GetType().ToString() + " was encountered while attempting to roll back the transaction.";
                    }
                }
                ErrMsg = "* An exception of type " + ex.GetType().ToString() + " was encountered while running the query." + "ErrorMsg=(" + ex.Message + ")";
                return -1;
            }
            finally
            {
                if(myTrans != null)
                    myTrans = null;
                if(cmd != null)
                    cmd = null;
                if(conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public DataTable ExecuteQueryDT_Select(string SQL_cmd, ref string ErrMsg)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Connstr);

            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                conn.Open();

                cmd.CommandTimeout = 4;
                cmd.CommandText = SQL_cmd;

                dt.Load(cmd.ExecuteReader());

                return dt;
            }
            catch(Exception ex)
            {
                ErrMsg = "* An exception of type " + ex.GetType().ToString() + " was encountered while running the query." + "ErrorMsg=(" + ex.Message + ")";
                return null;
            }
            finally
            {
                if(dt != null)
                    dt = null;
                if(cmd != null)
                    cmd = null;
                if(conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public DataTable ExecuteQueryDT(string SQL_cmd, ref string ErrMsg)
        {
            DataTable dt = new DataTable();
            SqlTransaction myTrans = null;
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(Connstr);
            try
            {
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;

                conn.Open();

                myTrans = conn.BeginTransaction();
                cmd.Transaction = myTrans;
                cmd.CommandText = SQL_cmd;
                cmd.CommandTimeout = 4;

                dt.Load(cmd.ExecuteReader());

                myTrans.Commit();

                return dt;
            }
            catch(Exception ex)
            {
                try
                {
                    if(myTrans != null)
                    {
                        myTrans.Rollback();
                    }
                }
                catch(SqlException exSql)
                {
                    if(myTrans.Connection != null)
                    {
                        ErrMsg = "* An exception of type " + exSql.GetType().ToString() + " was encountered while attempting to roll back the transaction.";
                    }
                }
                ErrMsg = "* An exception of type " + ex.GetType().ToString() + " was encountered while running the query." + "ErrorMsg=(" + ex.Message + ")";
                return null;
            }
            finally
            {
                if(myTrans != null)
                    myTrans = null;
                if(dt != null)
                    dt = null;
                if(cmd != null)
                    cmd = null;
                if(conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}