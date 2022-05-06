using System.Data;
using SimpleDatabase;

namespace PentagonHMI.ViewModel.Recipe
{
    class Recipe_ViewModel
    {
        private static SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, "COCO");

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
