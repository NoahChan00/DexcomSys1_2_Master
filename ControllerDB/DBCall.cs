using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ControllerDB
{
    public class DataDBCall
    {
        #region Controller
        public string getNewOrder(string PalletID)
        {
            string _return = Database.Database.ExecuteScalar("SELECT AssetTag FROM Orders  WHERE Status != 'Completed' ORDER BY Priority, TakeOrderDT");

            return _return;
        }

        public List<string> getPartsID(string AssetTag, string StationID)
        {
            List<string> _listReturn = new List<string>();

            DataTable dt = Database.Database.ExecuteQueryDT("SELECT PartNumberID FROM Orders WHERE AssetTag='" + AssetTag + "' AND StationID='" + StationID + "'");

            for (int x = 0; x < dt.Rows.Count; x++)
            {
                _listReturn.Add(dt.Rows[x]["PartNumberID"].ToString());
            }

            return _listReturn;
        }

        public bool setPartInstalled(string OrderID, string StationID, string PartModelID, string SN)
        {
            string sqlStr = "UPDATE Orders_List SET SerialNumber='" + SN + "' WHERE OrderID='" + OrderID + "' AND PartModelID='" + PartModelID + "'";

            return true;
        }

        public bool setPartInstallFailed(string OrderID, string StationID, string PartModelID, string SN)
        {
            //string sqlStr = "INSERT INTO Orders_List_Reject(OrderID, PartModelID, SerialNumber, InstallationTimestamp) " +
            //                " VALUES('" + OrderID +
            //                "','" + PartModelID +
            //                "','" + SN +
            //                "'," + Convert.ToString(DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()) + "'";

            return true;
        }

        public bool setAlarm(string StationID, string AlarmID)
        {
            return true;
        }

        public bool setLineStatus(string StationID, string Status)
        {

            return true;
        }

        #endregion

        #region HMI

        public string getPartsDisplay(string StationID)
        {
            return "";
        }

        public bool setRejectPartStation(string StationID, string OrderID, string PartNumberID)
        {
            return true;
        }

        public string getAssetTag(string OrderID)
        {
            return "";
        }

        public bool setStationStatus(bool Status)
        {
            return true;
        }

        public bool setUpdatePartQty(string StationID, string PartNumberID, int Quantity)
        {
            return true;
        }

        public string getAlarms(string StationID)
        {
            return "11;12;13;14";
        }

        public string getLineStatus(string StationID)
        {
            return "";
        }

        public string getPartQty(string StationID, string PartNumberID)
        {
            return "";
        }

        #endregion

    }
}
