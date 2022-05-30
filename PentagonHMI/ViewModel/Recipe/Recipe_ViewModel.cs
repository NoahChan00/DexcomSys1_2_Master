using System.Data;
using SimpleDatabase;

namespace PentagonHMI.ViewModel.Recipe
{
    class Recipe_ViewModel
    {
        // Previously using Database COCO, pending verify
        private static SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);

        public static DataTable GetFamily_OutputPartNumber()
        {
            return SQLer.Exec_DTSelect("Select * from Family_OutputPartNumber");
        }

        public static DataTable GetInputPartNumber_Family()
        {
            return SQLer.Exec_DTSelect("Select * from InputPartNumber_Family");
        }

        //Else
        public static DataTable GetLabelHistory()
        {
            return SQLer.Exec_DTSelect("Select * from Label_History");
        }
    }
}
