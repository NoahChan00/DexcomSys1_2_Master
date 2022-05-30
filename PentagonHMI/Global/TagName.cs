using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleDatabase;

namespace PentagonHMI
{
    public class TagName
    {
        private SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);
        public Dictionary<string, string> Dic_Key_Address = new Dictionary<string, string>();
        public TagName()
        {
            try
            {
                foreach (DataRow dr in SQLer.Exec_DTSelect($"SELECT [Name],[Address] FROM [TagName] WHERE [StationName] = {Classes.GlobalFunctions.StationName}").Rows)
                    Dic_Key_Address.Add(dr["Name"].ToString(), dr["Address"].ToString());
            }
            catch { }
        }

        public static class Key
        {
            public static string EngineeringMode => "EngineeringMode";


        }
    }
}
