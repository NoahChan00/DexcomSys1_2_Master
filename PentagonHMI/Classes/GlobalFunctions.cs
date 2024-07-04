using SimpleDatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows;

namespace PentagonHMI.Classes
{
    internal class GlobalFunctions
    {
        #region Variables

        public const string EnglishFileSuffix = "_en";
        public const string ChineseFileSuffix = "_cn";

        public static Dictionary<int, string> ErrorListDict = new Dictionary<int, string>();
        public static string userID = "Operator";
        public static bool StartLot = false;
        public static string LotID = "";
        public static VanillaDB.DataDBCall DBCall = new VanillaDB.DataDBCall(Properties.Settings.Default.DatabaseConnectionString.ToString());

        //public static OleDbConnection gl_conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Directory.GetCurrentDirectory() + @"\Database.mdb");
        public static string StationName = Properties.Settings.Default.Station.ToString();

        public static string SubStationName = Properties.Settings.Default.SubStation.ToString();
        public static string Lifter1Station = Properties.Settings.Default.Lifter1Station.ToString();
        public static string Lifter2Station = Properties.Settings.Default.Lifter2Station.ToString();
        public static string TotalLifter = Properties.Settings.Default.TotalLifter.ToString();
        public static ProjectType ProjectType = (ProjectType)System.Enum.Parse(typeof(ProjectType), Properties.Settings.Default.ProjectType, true);
        public static StationType StationType = (StationType)System.Enum.Parse(typeof(StationType), Properties.Settings.Default.StationType, true);
        public static SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(Properties.Settings.Default.DatabaseConnectionString.ToString());
        public static string ServerName = sqlConnectionStringBuilder.DataSource;
        public static string DatabaseName = sqlConnectionStringBuilder.InitialCatalog;
        public static bool IntegratedSecurity = sqlConnectionStringBuilder.IntegratedSecurity;
        public static bool PersistSecurityInfo = sqlConnectionStringBuilder.PersistSecurityInfo;
        public static string UserID = sqlConnectionStringBuilder.UserID;
        public static string Password = sqlConnectionStringBuilder.Password;
        public static string FeatureFlag = Properties.Settings.Default.FeatureFlag.ToString();
        public static string ScannerIP = Properties.Settings.Default.ScannerIP.ToString();
        public static string ScannerPort = Properties.Settings.Default.ScannerPort.ToString();
        public static string LocalIP = Properties.Settings.Default.localIP.ToString();
        public static string LocalPort = Properties.Settings.Default.LocalPort.ToString();
        public static string login_Timeout = Properties.Settings.Default.LogoutTimeSec.ToString();
        public static string PLC_IPAddress = Properties.Settings.Default.PLC_IPAddress.ToString();
        public static string PLC_Path = Properties.Settings.Default.PLC_Path.ToString();
        public static string PLC_Timeout = Properties.Settings.Default.PLC_Timeout.ToString();
        public static string OEEIdealCycleTimeSec = Properties.Settings.Default.OEEIdealCycleTimeSec.ToString();
        public static string RepenishIconFlag = Properties.Settings.Default.RepenishIconFlag.ToString();
        public static string System1Insertion = Properties.Settings.Default.System1Insertion.ToString();
        public static string System1Battery = Properties.Settings.Default.System1Battery.ToString();
        public static string System2FailImage = Properties.Settings.Default.System2FailImage.ToString();
        //public static string CalibrationFlag = "ON";//PentagonHMI.Properties.Settings.Default.CalibrationFlag.ToString();
        //public static string PickRetryPrompt = "ON";// PentagonHMI.Properties.Settings.Default.PickRetryPrompt.ToString();
        //public static string PalletRejectInt = "ON";// PentagonHMI.Properties.Settings.Default.PalletRejectInt.ToString();

        public static SQLCarrier aSQL = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);
        public static MachineNameType MachineName = (aSQL.Exec_Scalar<string>("SELECT Info FROM dbo.Config WHERE Item = 'MachineName'") == MachineNameType.System_02.ToString()) ? MachineNameType.System_02 : MachineNameType.System_01;

        // Global variable to determine system 1 or system 2

        #endregion Variables

        // bool as only System 1 and 2
        public static bool IsSystem1 => MachineName == MachineNameType.System_01;

        public enum MachineNameType
        {
            System_01, System_02
        }

        //public static void LoadMachineName()
        //{
        //    var machineName = aSQL.Exec_Scalar<string>("SELECT Info FROM dbo.Config WHERE Item = 'MachineName'");
        //    // If fail to get legit value or connection fail, default to System_01;
        //    MachineName = (machineName == MachineNameType.System_02.ToString()) ? MachineNameType.System_02 : MachineNameType.System_01;
        //}

        public static void LoadErrorList()
        {
            //string ErrMsg = "";
            lock(ErrorListDict)
            {
                DataSet ds = new DataSet();
                ds.Clear();
                ErrorListDict.Clear();
                DataTable dtalarm = new DataTable();

                dtalarm = aSQL.Exec_DTSelect($"SELECT [AlmCode],[AlmType],[ModuleCode],[AlmDesc],[AlmAction] FROM [Alarms_List] WHERE [StationID] = {StationName}");

                if(dtalarm != null)
                    foreach(DataRow row in dtalarm.Rows)
                    {
                        ErrorListDict.Add(Convert.ToInt32(row["AlmCode"]), row["ModuleCode"] + ";" + row["AlmType"] + ";" + row["AlmDesc"] + ";" + row["AlmAction"]);
                    }

                ds.Clear();
            }
        }

        public static string getValidLocalIP()
        {
            NetworkInterface[] ifaceList = NetworkInterface.GetAllNetworkInterfaces();
            foreach(NetworkInterface iface in ifaceList)
            {
                if(iface.OperationalStatus == OperationalStatus.Up && iface.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                {
                    UnicastIPAddressInformationCollection unicastIPC = iface.GetIPProperties().UnicastAddresses;
                    foreach(UnicastIPAddressInformation unicast in unicastIPC)
                    {
                        if(unicast.Address.ToString() == "191.168.3.3")
                        {
                            return "191.168.3.3";
                        }
                        else if(unicast.Address.ToString() == "191.168.3.2")
                        {
                            return "191.168.3.2";
                        }
                        else if(unicast.Address.ToString() == "192.168.3.1")
                        {
                            return "192.168.3.1";
                        }
                        else if(unicast.Address.ToString() == "191.168.3.44")
                        {
                            return "191.168.3.44";
                        }
                        else if(unicast.Address.ToString().Length > 10 && unicast.Address.ToString().Substring(0, 10) == "191.168.3.")
                        {
                            return unicast.Address.ToString();
                        }
                    }
                }
            }

            return "127.0.0.1";
        }

        public static string getUsePLCIP(string LocalIP)
        {
            //Load PLC IP address
            switch(LocalIP)
            {
                case "191.168.3.44":
                    return "191.168.3.114";

                case "191.168.3.45":
                    return "191.168.3.114";

                case "191.168.3.1":
                    return "191.168.3.141";

                case "191.168.3.2":
                    return "191.168.3.142";

                case "191.168.3.3":
                    return "191.168.3.143";

                case "192.168.3.1":
                    return "192.168.3.100";

                default:
                {
                    if(LocalIP.Length >= 10 && LocalIP.Substring(0, 10) == "191.168.3.")
                    {
                        return "191.168.3.144";
                    }
                    else
                    {
                        return "127.0.0.1";
                    }
                }
            }
        }

        #region "SetLanguageDictionary() - Set the current program's language"

        public static void SetLanguageDictionary()
        {
            ResourceDictionary dictResource = new ResourceDictionary();
            //   bool IsVer2BellyBand = IsVersion2BB();

            //App.Current.Resources.MergedDictionaries.Clear();
            switch(Thread.CurrentThread.CurrentCulture.ToString())
            {
                case "zh-CN":
                    dictResource.Source = new Uri("..\\Resources\\zh-CN.xaml", UriKind.Relative);
                    App.Current.Resources.MergedDictionaries.Add(dictResource);

                    // Load Changes on new BB 2 machine
                    //if (IsVer2BellyBand)
                    //{
                    //    ResourceDictionary dict = new ResourceDictionary();
                    //    dict.Source = new Uri("..\\Resources\\zh-CN_v2.xaml", UriKind.Relative);
                    //    App.Current.Resources.MergedDictionaries.Add(dict);
                    //}
                    break;

                default:
                    dictResource.Source = new Uri("..\\Resources\\en-US.xaml", UriKind.Relative);
                    App.Current.Resources.MergedDictionaries.Add(dictResource);

                    // Load Changes on new BB 2 machine
                    //if (IsVer2BellyBand)
                    //{
                    //    ResourceDictionary dict = new ResourceDictionary();
                    //    dict.Source = new Uri("..\\Resources\\en-US_v2.xaml", UriKind.Relative);
                    //    App.Current.Resources.MergedDictionaries.Add(dict);
                    //}
                    break;
            }

            if(dictResource != null)
            {
                dictResource = null;
            }
        }

        #endregion "SetLanguageDictionary() - Set the current program's language"
    }
}