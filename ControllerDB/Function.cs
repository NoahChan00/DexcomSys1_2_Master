using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class Function
    {
        public static bool isNumeric(string str)
        {
            int n;

            bool isNumeric = int.TryParse(str, out n);
            
            return isNumeric;
        }

        public static string AddPrePostFix(string MyData, string PrePostFix = "'")
        {
            string[] arr_MyData = MyData.Split(',');

            for (int x = 0; x < arr_MyData.Length; x++)
            {
                arr_MyData[x] = PrePostFix + arr_MyData[x] + PrePostFix;
            }

            string MyReturn = string.Join(",", arr_MyData);


            return MyReturn;

        }




    }
}
