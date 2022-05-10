using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SimpleDatabase
{
    public class SQLCarrier
    {
        private string Str_ConnectionString;
        private enum SQLMode
        {
            NonQuery,
            Scalar,
            DT_Select
        }

        public SQLCarrier(string ServerName, string InitialCatalog = "HMI", bool PersistSecurityInfo = false,
            string Password = "NA", string UserID = "NA")
        {
            //Str_ConnectionString =
            //    $"Persist Security Info = {(PersistSecurityInfo ? $"True;User ID = {UserID};Password = {Password};" : "False;")}" +
            //    $"Data Source = {ServerName};" +
            //    $"Integrated Security = true; " +
            //    $"Initial Catalog = {InitialCatalog};";
            Str_ConnectionString = "Persist Security Info = False;Data Source = 191.168.0.171;Integrated Security = False; Initial Catalog = gdb_DexcomSystem1;User ID=sa;Password=Pss123321!;";
        }

        public bool isConnected()
        {
            try
            {
                using (SqlConnection Conn = new SqlConnection(Str_ConnectionString))
                {
                    Conn.Open();
                    if (Conn.State == ConnectionState.Open) return true;
                }
            }
            catch { }
            return false;
        }

        public int Exec_NonQuery(string str_Statement)
        {
            return SQLAccess<int>(str_Statement, SQLMode.NonQuery);
        }

        public T Exec_Scalar<T>(string strCmd)
        {
            return SQLAccess<T>(strCmd, SQLMode.Scalar);
        }

        public DataTable Exec_DTSelect(string str_Statement)
        {
            return SQLAccess<DataTable>(str_Statement, SQLMode.DT_Select);
        }

        public bool Exec_SProc(string StoreProcName, Dictionary<string, object> Dic_Parameters_Value)
        {
            int RowAffected = 0;
            try
            {
                using (SqlConnection Conn = new SqlConnection(Str_ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(StoreProcName)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = Conn
                    };
                    foreach (var item in Dic_Parameters_Value)
                        cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));
                    cmd.Connection.Open();
                    RowAffected = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return RowAffected > 0 ? true : false;
        }

        public string Exec_SProc_Msg(string StoreProcName, Dictionary<string, object> Dic_Parameters_Value)
        {
            string OutputName = "@Message";
            try
            {
                using (SqlConnection Conn = new SqlConnection(Str_ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(StoreProcName)
                    {
                        CommandType = CommandType.StoredProcedure,
                        Connection = Conn
                    };
                    foreach (var item in Dic_Parameters_Value)
                        cmd.Parameters.Add(new SqlParameter(item.Key, item.Value));

                    cmd.Parameters.Add(OutputName, SqlDbType.VarChar, 255);
                    cmd.Parameters[OutputName].Direction = ParameterDirection.Output;
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                    return cmd.Parameters[OutputName].Value.ToString();
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            return "Stored Procedure Execution Failed";
        }

        private T SQLAccess<T>(string strCmd, SQLMode mode)
        {
            SqlTransaction Tran = null; object obj_Result = default;
            try
            {
                using (SqlConnection Conn = new SqlConnection(Str_ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand(strCmd, Conn);
                    cmd.Connection.Open();

                    if (SQLMode.Scalar == mode)
                    {
                        obj_Result = cmd.ExecuteScalar();
                    }
                    else if (SQLMode.DT_Select == mode)
                    {
                        DataTable DT = new DataTable();
                        DT.Load(cmd.ExecuteReader());
                        obj_Result = DT;
                    }
                    else if (SQLMode.NonQuery == mode)
                    {
                        Tran = Conn.BeginTransaction(IsolationLevel.ReadCommitted);
                        cmd.Transaction = Tran;
                        obj_Result = cmd.ExecuteNonQuery();
                        Tran.Commit();
                    }
                    cmd.Connection.Close();
                }
            }
            catch (Exception ex) { /*if (Tran != null) Tran.Rollback(); */ MessageBox.Show(ex.Message); }
            return (T)Convert.ChangeType(obj_Result, typeof(T));
        }
    }
}
