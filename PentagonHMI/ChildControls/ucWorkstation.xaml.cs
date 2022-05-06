using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Threading;

using PentamasterLibraries;

using System.Data;
using Logix;

namespace PentagonHMI.ChildControls
{
    /// <summary>
    /// Interaction logic for ucWorkstation.xaml
    /// </summary>
    /// 

    public partial class ucWorkstation : UserControl, IDisposable
    {
        private VanillaDB.DataDBCall DBCall;
        TCPIPClient tcpClient;
        bool chkSts = false;
        bool chkOrderSts = true;

        // Allen Bradley PLC TCP Connection
        // ////////////////////////////////
        Utilities.TcpAllenB plc = new Utilities.TcpAllenB();


        private string mAssetTag { get; set; }
        private string mAssetTag2 { get; set; }

        #region Variable
        List<LogicClasses.StationInstallPartList> StationPartList;
        List<LogicClasses.PartInstalled> GoodPartUsedList;
        List<LogicClasses.PartInstalled> BadPartUsedList;

        int iQtyBad = 0;
        int totalItem = 0;
        int currentItem = 0;

        bool OnWorkstationTread;
        Thread threadWorkstation;

        Tag Tag_strZone1 = new Tag("strZone1");
        Tag Tag_strZone2 = new Tag("strZone2");

        Tag Tag_PowerCable = new Tag("strPowerCableZone118");
        Tag Tag_SataCable = new Tag("strSataZone128");

        #endregion

        #region Constructor
        public ucWorkstation()
        {
            InitializeComponent();

            string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
            DBCall = new VanillaDB.DataDBCall(Connstr);

            string sZone = Classes.GlobalFunctions.Lifter1Station.ToString();
            if (sZone.Substring(sZone.Length - 1, 1) == "6" || sZone.Substring(sZone.Length - 1, 1) == "7")
            {
                mAssetTag = "N/A";
                this.Dispatcher.Invoke(new Action(() => btnReject.Visibility = Visibility.Hidden));
            }
            else
            {
                //plc.PLC_Connect(Classes.GlobalFunctions.PLC_IPAddress, Classes.GlobalFunctions.PLC_Path, Convert.ToInt16(Classes.GlobalFunctions.PLC_Timeout));
                if (!plc.PLC_Connect(Classes.GlobalFunctions.PLC_IPAddress, Classes.GlobalFunctions.PLC_Path, Convert.ToInt16(Classes.GlobalFunctions.PLC_Timeout)))
                    MessageBox.Show("Fail Connect PLC :\n" + plc.ERROR_MSG);
            }

            int reconnectInterval = 1000;
            bool enableHeartBeat = false;
            //bool connectTcpServerWhenConstruct = true;

            txtBarcode.IsEnabled = true;
            btnFixScan.Visibility = Visibility.Collapsed;

            if (Classes.GlobalFunctions.ScannerIP.ToString() != "NA")
            {
                btnFixScan.Visibility = Visibility.Visible;
                try
                {
                    TCPIPClient.LineTerminator lineTerminator = TCPIPClient.LineTerminator.CR;
                    tcpClient = new TCPIPClient(Classes.GlobalFunctions.LocalIP.ToString(), Classes.GlobalFunctions.LocalPort.ToString(),
                    Classes.GlobalFunctions.ScannerIP.ToString(), Classes.GlobalFunctions.ScannerPort.ToString(),
                    reconnectInterval, enableHeartBeat, false, lineTerminator); //connectTcpServerWhenConstruct
                    tcpClient.OnConnectionStatus += new TCPIPClient.ConnectionStatusEvent(tcpClient_OnConnectionStatus);
                    tcpClient.OnDataReceived += new TCPIPClient.DataReceivedEvent(tcpClient_OnDataReceived);
                    if (tcpClient != null)
                    {
                        tcpClient.Connect();
                        MessageBox.Show("Connected");
                    }
                    else
                    {
                        MessageBox.Show("tcp client is null!");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fail to Connect CPU Barcode Scanner : " + ex.Message);
                }

            }
            else
            {
                btnFixScan.Visibility = Visibility.Collapsed;
            }

            threadWorkstation = new Thread(getNewWorkOrder);

            RequestWorkOrder();
        }
        #endregion                         

        #region Method
        private void RequestWorkOrder()
        {
            OnWorkstationTread = true;
            ClearResetDisplay();

            if (threadWorkstation.IsAlive)
            {
                //MessageBox.Show("Thread Alive: A waiting new Workorder");
                return;
            }
            else
            {
                //MessageBox.Show("Thread Not Alive : Restart");
                threadWorkstation = new Thread(getNewWorkOrder);
                threadWorkstation.Start();
            }
        }
        private void getNewWorkOrder()
        {
            do
            {
                StationPartList = getStationPartList();
                if (StationPartList != null)
                {
                    if (StationPartList.Count != 0)
                    {
                        if (StationPartList[0].pStatus == "Aborted-Exited")
                        {
                            OrderNo.Dispatcher.Invoke(new Action(() => OrderNo.ucText = StationPartList[0].OrderID));
                            AssetTag.Dispatcher.Invoke(new Action(() => AssetTag.ucText = StationPartList[0].AssetTag));

                            txtOrderStatus.Dispatcher.Invoke(new Action(() => txtOrderStatus.Visibility = Visibility.Visible));
                            txtOrderStatus.Dispatcher.Invoke(new Action(() => txtOrderStatus.Text = "Order Status : " + StationPartList[0].pStatus));
                            continue;
                        }
                        else if (StationPartList[0].pStatus == "Aborted-Not-Exited")
                        {
                            OrderNo.Dispatcher.Invoke(new Action(() => OrderNo.ucText = StationPartList[0].OrderID));
                            AssetTag.Dispatcher.Invoke(new Action(() => AssetTag.ucText = StationPartList[0].AssetTag));

                            txtOrderStatus.Dispatcher.Invoke(new Action(() => txtOrderStatus.Visibility = Visibility.Visible));
                            txtOrderStatus.Dispatcher.Invoke(new Action(() => txtOrderStatus.Text = "Order Status : " + StationPartList[0].pStatus));
                            continue;
                        }
                        else
                        {
                            txtOrderStatus.Dispatcher.Invoke(new Action(() => txtOrderStatus.Visibility = Visibility.Collapsed));
                            break;
                        }
                    }
                }
                Thread.Sleep(1000);
            } while (true);
            //while (StationPartList.Count == 0);

            InitPickPackStage();

        }

        private List<LogicClasses.StationInstallPartList> getStationPartList()
        {
            string errMsg = "";
            List<LogicClasses.StationInstallPartList> stationPartList = new List<LogicClasses.StationInstallPartList>();

            // Get AssetTag from PLC
            // ======================
            mAssetTag = WS_GetAssetTag(Classes.GlobalFunctions.Lifter1Station.ToString());
            //mAssetTag = WS_GetAssetTag("ZONE_1");
            if (mAssetTag.Length >= 3)
            {
                if (mAssetTag.Substring(0, 3) == "ERR")
                {
                    /////**** MessageBox.Show(mAssetTag);
                    mAssetTag = "";
                }
            }
            else
            {
                mAssetTag = "";
            }
            //mAssetTag = "UFO1827-00001";

            stationPartList.Add(new LogicClasses.StationInstallPartList
            {
                AssetTag = mAssetTag
            });

            if (mAssetTag != "")
            {
                // Get OrderID
                if (mAssetTag != "N/A")
                {
                    DataTable dOrder = DBCall.Order_Select_Info(mAssetTag, ref errMsg);
                    if (errMsg == "" && dOrder != null && dOrder.Rows.Count > 0)
                    {
                        stationPartList[0].OrderID = dOrder.Rows[0]["OrderID"].ToString();
                        stationPartList[0].pStatus = dOrder.Rows[0]["Status"].ToString();
                    }
                }
                else
                {
                    stationPartList[0].OrderID = "N/A";
                }


                DataTable dPartInfo = DBCall.Part_Select_Info(Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                if (errMsg == "")
                {
                    int i = 0;
                    foreach (DataRow r in dPartInfo.Rows)
                    {
                        if (i > 0)
                        {
                            stationPartList.Add(new LogicClasses.StationInstallPartList
                            {
                                //Foong
                                AssetTag = stationPartList[0].AssetTag,
                                PartNumber = r["PartModelID"].ToString(),
                                HaveSN = r["HaveSN"].ToString(),
                                StockLimit = r["StockLimit"].ToString(),
                                iStockLimit = Convert.ToInt32(r["StockLimit"].ToString()),
                                UpperLimit = r["UpperLimit"].ToString(),
                                iUpperLimit = Convert.ToInt32(r["UpperLimit"].ToString()),
                                Matching = r["Matching"].ToString()
                            });
                        }
                        else
                        {
                            stationPartList[0].PartNumber = r["PartModelID"].ToString();
                            stationPartList[0].HaveSN = r["HaveSN"].ToString();
                            stationPartList[0].StockLimit = r["StockLimit"].ToString();
                            stationPartList[0].iStockLimit = Convert.ToInt32(r["StockLimit"].ToString());
                            stationPartList[0].UpperLimit = r["UpperLimit"].ToString();
                            stationPartList[0].iUpperLimit = Convert.ToInt32(r["UpperLimit"].ToString());
                            stationPartList[0].Matching = r["Matching"].ToString();
                        }
                        i++;
                    }
                }
            }
            else
            {
                stationPartList = null;
            }

            return stationPartList;
        }
        private List<LogicClasses.StationInstallPartList> getStationPartList_DB()
        {
            string errMsg = "";
            List<LogicClasses.StationInstallPartList> stationPartList = new List<LogicClasses.StationInstallPartList>();

            ////DataTable dt = DBCall.Zone_Select_Part(Classes.GlobalFunctions.Lifter1Station.ToString(), ref errMsg);

            ////if (errMsg == "")
            ////{
            ////    foreach (DataRow r in dt.Rows)
            ////    {
            ////        stationPartList.Add(new LogicClasses.StationInstallPartList
            ////        {
            ////            OrderID = r["OrderID"].ToString(),
            ////            AssetTag = r["AssetTagID"].ToString(),
            ////            PartNumber = r["PartModelID"].ToString(),
            ////            pStatus = r["Status"].ToString(),
            ////            HaveSN = r["HaveSN"].ToString(),
            ////            ErrorMsg = r["ErrorMessage"].ToString(),
            ////            Matching = r["Matching"].ToString()


            ////        });
            ////        if (r["PartModelID"].ToString().Contains("CPU_"))
            ////        {
            ////            this.txtBarcode.Dispatcher.Invoke(new Action(() => txtBarcode.IsEnabled = false));
            ////            this.btnFixScan.Dispatcher.Invoke(new Action(() => btnFixScan.Visibility = Visibility.Visible));
            ////            chkSts = true;
            ////        }
            ////        if (chkOrderSts)
            ////            if (r["OrderID"].ToString() == "N/A")
            ////            {
            ////                //this.btnReject.Dispatcher.Invoke(new Action(() => btnReject.Visibility = Visibility.Hidden));
            ////                chkOrderSts = false;
            ////            }
            ////    }
            ////}

            return stationPartList;
        }

        string WS_GetAssetTag(string zone)
        {
            string tagName = "";
            object rtnObj;

            if (Classes.GlobalFunctions.StationName == "11")
            {
                Tag T = new Tag("Z12A_T_MANUALDATA.PopUp_Pad");
                string Popped = plc.PLC_ReadNotNull(ref T, Logix.Tag.ATOMIC.BOOL).ToString();
                if (Popped != "True")
                    return string.Empty;
            }
            else if (Classes.GlobalFunctions.StationName == "12")
            {
                Tag T = new Tag("Z24A_T_MANUALDATA.PopUp_Pad");
                string Popped = plc.PLC_ReadNotNull(ref T, Logix.Tag.ATOMIC.BOOL).ToString();
                if (Popped != "True")
                    return string.Empty;
            }


            switch (zone)
            {
                case "ZONE_1":
                    //tagName = "strZone1";
                    rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    break;
                case "ZONE_2":
                    //tagName = "strZone2";
                    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                    break;

                case "28":
                    //tagName = "strZone1";
                    //tagName = "strLabelZone28";
                    rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    break;
                case "29":
                    //tagName = "strZone2";
                    //tagName = "strLabelZone29";
                    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                    break;

                case "48":
                    //tagName = "strZone1";
                    //tagName = "strMoboZone48";
                    rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    break;
                case "49":
                    //tagName = "strZone2";
                    //tagName = "strMoboZone49";
                    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                    break;

                case "58":
                    //tagName = "strZone1";
                    //tagName = "strKnGZone58";
                    rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    break;
                case "59":
                    //tagName = "strZone2";
                    //tagName = "strKnGZone59";
                    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                    break;

                case "68":
                    //tagName = "strZone1";
                    //tagName = "strAffixZone68";
                    rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    break;
                case "69":
                    //tagName = "strZone2";
                    //tagName = "strAffixZone69";
                    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                    break;

                case "78":
                    //tagName = "strZone1";
                    //tagName = "strBafflesZone78";
                    rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    break;
                case "79":
                    //tagName = "strZone2";
                    //tagName = "strBafflesZone79";
                    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                    break;

                case "88":
                    //tagName = "strZone1";
                    //tagName = "strBnCZone88";
                    rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    break;
                case "89":
                    //tagName = "strZone2";
                    //tagName = "strBnCZone89";
                    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                    break;

                case "98":
                    //tagName = "strZone1";
                    //tagName = "strLHZone98";
                    rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    break;
                case "99":
                    //tagName = "strZone2";
                    //tagName = "strLHZone99";
                    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                    break;

                case "108":
                    //tagName = "strZone1";
                    //tagName = "strSHZone108";
                    rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    break;
                case "109":
                    //tagName = "strZone2";
                    //tagName = "strSHZone109";
                    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                    break;

                case "118":
                    //tagName = "strZone1";
                    //tagName = "strSHZone118";

                    //rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    rtnObj = plc.PLC_Read(ref Tag_PowerCable, Logix.Tag.ATOMIC.STRING);
                    break;

                //case "119":
                //    //tagName = "strZone2";
                //    //tagName = "strSHZone119";
                //    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                //    break;

                case "128":
                    //tagName = "strZone1";
                    //tagName = "strSHZone118";

                    //rtnObj = plc.PLC_Read(ref Tag_strZone1, Logix.Tag.ATOMIC.STRING);
                    rtnObj = plc.PLC_Read(ref Tag_SataCable, Logix.Tag.ATOMIC.STRING);
                    break;
                //case "129":
                //    //tagName = "strZone2";
                //    //tagName = "strSHZone119";
                //    rtnObj = plc.PLC_Read(ref Tag_strZone2, Logix.Tag.ATOMIC.STRING);
                //    break;

                default:
                    rtnObj = new object();
                    tagName = "N/A";

                    break;
            }
            if (tagName == "N/A")
                return "N/A";

            if (rtnObj == null) { return "N/A"; }
            //object rtnObj = plc.PLC_Read(tagName, Logix.Tag.ATOMIC.STRING);
            if (plc.ERROR_MSG == "")
            {
                //Foong Check repeated Asset Tag
                string ErrMsg = string.Empty;
                DataTable dCheckRepeat = DBCall.Order_Select_Info_InstallVerify(rtnObj.ToString(), Classes.GlobalFunctions.StationName, ref ErrMsg);
                if (ErrMsg == string.Empty && dCheckRepeat.Rows.Count > 0)
                    if (dCheckRepeat.Rows[0]["Result"].ToString() == "1")
                        return string.Empty;

                return rtnObj.ToString();

            }
            else
            {
                return "ERROR - PLC_GetAssetTag\nError TagName : " + tagName + "\n" + plc.ERROR_MSG;
            }
        }

        private void UpdateAsmPartList(List<LogicClasses.StationInstallPartList> asmList)
        {
            SetAllAsmPartListDisp(Visibility.Collapsed);
            int i = 1;
            if (OnWorkstationTread)
            {
                OrderNo.ucText = asmList[0].OrderID;
                AssetTag.ucText = asmList[0].AssetTag;
            }
            else
            {
                OrderNo.Dispatcher.Invoke(new Action(() => OrderNo.ucText = asmList[0].OrderID));
                AssetTag.Dispatcher.Invoke(new Action(() => AssetTag.ucText = asmList[0].AssetTag));
            }


            foreach (LogicClasses.StationInstallPartList spList in asmList)
            {
                if (i == 1)
                {
                    if (OnWorkstationTread)
                    {
                        Item_1.Dispatcher.Invoke(new Action(() => Item_1.ucText = spList.PartNumber));
                        Item_1.Dispatcher.Invoke(new Action(() => Item_1.Visibility = Visibility.Visible));
                    }
                    else
                    {
                        Item_1.ucText = spList.PartNumber;
                        Item_1.Visibility = Visibility.Visible;
                    }

                }
                else if (i == 2)
                {
                    if (OnWorkstationTread)
                    {
                        Item_2.Dispatcher.Invoke(new Action(() => Item_2.ucText = spList.PartNumber));
                        Item_2.Dispatcher.Invoke(new Action(() => Item_2.Visibility = Visibility.Visible));
                    }
                    else
                    {
                        Item_2.ucText = spList.PartNumber;
                        Item_2.Visibility = Visibility.Visible;
                    }
                }
                else if (i == 3)
                {
                    if (OnWorkstationTread)
                    {
                        Item_3.Dispatcher.Invoke(new Action(() => Item_3.ucText = spList.PartNumber));
                        Item_3.Dispatcher.Invoke(new Action(() => Item_3.Visibility = Visibility.Visible));
                    }
                    else
                    {
                        Item_3.ucText = spList.PartNumber;
                        Item_3.Visibility = Visibility.Visible;
                    }
                }
                else if (i == 4)
                {
                    if (OnWorkstationTread)
                    {
                        Item_4.Dispatcher.Invoke(new Action(() => Item_4.ucText = spList.PartNumber));
                        Item_4.Dispatcher.Invoke(new Action(() => Item_4.Visibility = Visibility.Visible));
                    }
                    else
                    {
                        Item_4.ucText = spList.PartNumber;
                        Item_4.Visibility = Visibility.Visible;
                    }

                }
                else if (i == 5)
                {
                    if (OnWorkstationTread)
                    {
                        Item_5.Dispatcher.Invoke(new Action(() => Item_5.ucText = spList.PartNumber));
                        Item_5.Dispatcher.Invoke(new Action(() => Item_5.Visibility = Visibility.Visible));
                    }
                    else
                    {
                        Item_5.ucText = spList.PartNumber;
                        Item_5.Visibility = Visibility.Visible;
                    }
                }
                i++;
            }
        }
        private void UpdateAsmBadQty(int index, string value)
        {
            if (index == 1)
            {
                if (OnWorkstationTread)
                {
                    QtyBad_1.Dispatcher.Invoke(new Action(() => this.QtyBad_1.ucText = value));
                }
                else
                {
                    QtyBad_1.ucText = value;
                }
            }
            else if (index == 2)
            {
                if (OnWorkstationTread)
                {
                    QtyBad_2.Dispatcher.Invoke(new Action(() => this.QtyBad_2.ucText = value));
                }
                else
                {
                    QtyBad_2.ucText = value;
                }
            }
            else if (index == 3)
            {
                if (OnWorkstationTread)
                {
                    QtyBad_3.Dispatcher.Invoke(new Action(() => this.QtyBad_3.ucText = value));
                }
                else
                {
                    QtyBad_3.ucText = value;
                }
            }
            else if (index == 4)
            {
                if (OnWorkstationTread)
                {
                    QtyBad_4.Dispatcher.Invoke(new Action(() => this.QtyBad_4.ucText = value));
                }
                else
                {
                    QtyBad_4.ucText = value;
                }
            }
            else if (index == 5)
            {
                if (OnWorkstationTread)
                {
                    QtyBad_5.Dispatcher.Invoke(new Action(() => this.QtyBad_5.ucText = value));
                }
                else
                {
                    QtyBad_5.ucText = value;
                }
            }
        }



        private void HighlightAsmItem(int index, Brush hColor)
        {
            if (OnWorkstationTread)
            {
                Item_1.Dispatcher.Invoke(new Action(() => Item_1.Background = Brushes.White));
                Item_2.Dispatcher.Invoke(new Action(() => Item_2.Background = Brushes.White));
                Item_3.Dispatcher.Invoke(new Action(() => Item_3.Background = Brushes.White));
                Item_4.Dispatcher.Invoke(new Action(() => Item_4.Background = Brushes.White));
                Item_5.Dispatcher.Invoke(new Action(() => Item_5.Background = Brushes.White));
                if (index == 1)
                {
                    Item_1.Dispatcher.Invoke(new Action(() => Item_1.Background = hColor));
                }
                else if (index == 2)
                {
                    Item_2.Dispatcher.Invoke(new Action(() => Item_2.Background = hColor));
                }
                else if (index == 3)
                {
                    Item_3.Dispatcher.Invoke(new Action(() => Item_3.Background = hColor));
                }
                else if (index == 4)
                {
                    Item_4.Dispatcher.Invoke(new Action(() => Item_4.Background = hColor));
                }
                else if (index == 5)
                {
                    Item_5.Dispatcher.Invoke(new Action(() => Item_5.Background = hColor));
                }
            }
            else
            {
                Item_1.Background = Brushes.White;
                Item_2.Background = Brushes.White;
                Item_3.Background = Brushes.White;
                Item_4.Background = Brushes.White;
                Item_5.Background = Brushes.White;
                if (index == 1)
                {
                    Item_1.Background = hColor;
                }
                else if (index == 2)
                {
                    Item_2.Background = hColor;
                }
                else if (index == 3)
                {
                    Item_3.Background = hColor;
                }
                else if (index == 4)
                {
                    Item_4.Background = hColor;
                }
                else if (index == 5)
                {
                    Item_5.Background = hColor;
                }
            }



        }
        private void SetAllAsmPartListDisp(Visibility visibility)
        {
            if (OnWorkstationTread)
            {
                this.Item_1.Dispatcher.Invoke(new Action(() => this.Item_1.Visibility = visibility));
                this.Item_2.Dispatcher.Invoke(new Action(() => this.Item_2.Visibility = visibility));
                this.Item_3.Dispatcher.Invoke(new Action(() => this.Item_3.Visibility = visibility));
                this.Item_4.Dispatcher.Invoke(new Action(() => this.Item_4.Visibility = visibility));
                this.Item_5.Dispatcher.Invoke(new Action(() => this.Item_5.Visibility = visibility));
            }
            else
            {
                this.Item_1.Visibility = visibility;
                this.Item_2.Visibility = visibility;
                this.Item_3.Visibility = visibility;
                this.Item_4.Visibility = visibility;
                this.Item_5.Visibility = visibility;
            }



        }
        private void SetAllBtnDisp(Visibility visibility)
        {
            if (OnWorkstationTread)
            {
                btnOK.Dispatcher.Invoke(new Action(() => this.btnOK.Visibility = visibility));
                btnBad.Dispatcher.Invoke(new Action(() => this.btnBad.Visibility = visibility));
                btnPass.Dispatcher.Invoke(new Action(() => this.btnPass.Visibility = visibility));
                //!!!
                string sZone = Classes.GlobalFunctions.Lifter1Station.ToString();
                if (sZone.Substring(sZone.Length - 1, 1) == "6" || sZone.Substring(sZone.Length - 1, 1) == "7")
                {
                    mAssetTag = "N/A";
                    this.Dispatcher.Invoke(new Action(() => btnReject.Visibility = Visibility.Hidden));
                }
                else
                {
                    btnReject.Dispatcher.Invoke(new Action(() => this.btnReject.Visibility = visibility));
                }

                //btnReset.Dispatcher.Invoke(new Action(() => this.btnReset.Visibility = visibility));
            }
            else
            {
                this.btnOK.Visibility = visibility;
                this.btnBad.Visibility = visibility;
                this.btnPass.Visibility = visibility;


                //!!!
                string sZone = Classes.GlobalFunctions.Lifter1Station.ToString();
                if (sZone.Substring(sZone.Length - 1, 1) == "6" || sZone.Substring(sZone.Length - 1, 1) == "7")
                {
                    this.Dispatcher.Invoke(new Action(() => btnReject.Visibility = Visibility.Hidden));
                }
                else
                {
                    if (chkOrderSts)
                        this.btnReject.Visibility = visibility;
                }
                //this.btnReset.Visibility = visibility;
            }
        }
        private void PromptBarcodeScan(string bCodeCheck)
        {
            if (bCodeCheck.ToUpper() == "TRUE")
            {
                if (OnWorkstationTread)
                {
                    ScanBarcodeBlock.Dispatcher.Invoke(new Action(() => ScanBarcodeBlock.Visibility = Visibility.Visible));
                    txtBarcode.Dispatcher.Invoke(new Action(() => txtBarcode.Text = ""));
                    txtBarcode.Dispatcher.Invoke(new Action(() => txtBarcode.Focus()));
                }
                else
                {
                    ScanBarcodeBlock.Visibility = Visibility.Visible;
                    txtBarcode.Text = "";
                    txtBarcode.Focus();
                }

            }
            else
            {
                if (OnWorkstationTread)
                {
                    txtBarcode.Dispatcher.Invoke(new Action(() => txtBarcode.Text = ""));
                    ScanBarcodeBlock.Dispatcher.Invoke(new Action(() => ScanBarcodeBlock.Visibility = Visibility.Hidden));
                }
                else
                {
                    txtBarcode.Text = "";
                    ScanBarcodeBlock.Visibility = Visibility.Hidden;
                }
            }
        }
        private bool CheckBarcodeScanned(string bCodeCheck)
        {
            bool rtnFlag = false;
            if (bCodeCheck.ToUpper() == "TRUE")
            {
                if (txtBarcode.Text.Trim().Length == 0)
                {
                    rtnFlag = false;
                }
                else
                {
                    rtnFlag = true;
                }
            }
            else
            {
                rtnFlag = true;
            }

            return rtnFlag;
        }
        private void addUsedPart(int index, ref List<LogicClasses.PartInstalled> partList)
        {
            string bCode = txtBarcode.Text;
            if (StationPartList[index].HaveSN.ToLower() == "false")
                bCode = "N/A";
            partList.Add(new LogicClasses.PartInstalled
            {
                StationID = Classes.GlobalFunctions.StationName.ToString(),
                ZoneID = Classes.GlobalFunctions.Lifter1Station.ToString(),
                OrderID = StationPartList[index].OrderID,
                AssetTag = StationPartList[index].AssetTag,
                PartNumber = AsmPart.ucText,
                Barcode = bCode,
                Matching = StationPartList[index].Matching
            });
        }
        private void sendPartUsedList()
        {
            bool rtnFlag;
            string errMsg = "";

            string msg = "Bad Parts\r\n";
            foreach (LogicClasses.PartInstalled pUsed in BadPartUsedList)
            {
                pUsed.Reason = txt_Remarks.Text;
                if (pUsed.AssetTag == "N/A")
                {
                    pUsed.Buffer = "";
                    rtnFlag = DBCall.Order_Update_Part_Rejected(pUsed.AssetTag, pUsed.ZoneID, pUsed.PartNumber, pUsed.Barcode, 0, pUsed.Reason, pUsed.Buffer, ref errMsg);
                    msg += $"{pUsed.StationID} : {pUsed.OrderID} : {pUsed.PartNumber} : {pUsed.Barcode} : {rtnFlag.ToString()}\r\n";
                }
                else
                {
                    //rtnFlag = DBCall.setRejectPartStation(pUsed.StationID, pUsed.OrderID, pUsed.PartNumber, pUsed.Barcode);
                    if (pUsed.Reason == null) pUsed.Reason = "";
                    if (pUsed.Buffer == null) pUsed.Buffer = "";
                    rtnFlag = DBCall.Order_Update_Part_Rejected(pUsed.AssetTag, pUsed.ZoneID, pUsed.PartNumber, pUsed.Barcode, 1, pUsed.Reason, pUsed.Buffer, ref errMsg);
                    msg += $"{pUsed.StationID} : {pUsed.OrderID} : {pUsed.PartNumber} : {pUsed.Barcode} : {rtnFlag.ToString()}\r\n";
                }
            }
            msg += "Good Parts\r\n";
            foreach (LogicClasses.PartInstalled pUsed in GoodPartUsedList)
            {
                //rtnFlag = DBCall.setPartInstalled(pUsed.StationID, pUsed.OrderID, pUsed.PartNumber, pUsed.Barcode);
                if (pUsed.AssetTag.ToUpper() != "N/A")
                {
                    rtnFlag = DBCall.Order_Update_Part_Installed(pUsed.AssetTag, pUsed.ZoneID, pUsed.PartNumber, pUsed.Barcode, "", ref errMsg);
                    msg += $"{pUsed.StationID} : {pUsed.OrderID} : {pUsed.PartNumber} : {pUsed.Barcode} : {rtnFlag.ToString()} \r\n";
                }
            }
            //MessageBox.Show(msg);
        }
        private void sendMatching()
        {
            int i = 0;
            string[] partNo = new string[2];
            string[] bCode = new string[2];
            string errMsg = "";

            foreach (LogicClasses.PartInstalled pUsed in GoodPartUsedList)
            {
                if (pUsed.Matching.ToUpper() == "TRUE")
                {
                    partNo[i] = pUsed.PartNumber;
                    bCode[i] = pUsed.Barcode;
                    i++;
                }
            }
            if (i == 2)
            {
                //MessageBox.Show(partNo[0] + " - " + bCode[0] + "\n" + partNo[1] + " - " + bCode[1]);
                DBCall.Parts_Storage_Update(partNo[0], bCode[0], partNo[1], bCode[1], Classes.GlobalFunctions.StationName.ToString(), ref errMsg);
            }
        }
        private void sendReject()
        {
            bool rtnFlag;
            string errMsg = "";

            //rtnFlag = DBCall.setRejectPartStation(Classes.GlobalFunctions.StationName.ToString(), StationPartList[0].OrderID, null, null);
            rtnFlag = DBCall.Order_Update_Part_Rejected(StationPartList[0].AssetTag, Classes.GlobalFunctions.Lifter1Station.ToString(), "", "", 5, txt_Remarks.Text, "", ref errMsg);
            //MessageBox.Show("Return : " + rtnFlag.ToString());
        }

        private void sendComplete()
        {
            string errMsg = "";
            DBCall.Order_Update_Zone("3", Classes.GlobalFunctions.Lifter1Station.ToString(), StationPartList[0].AssetTag, ref errMsg);
        }
        private void ClearResetDisplay()
        {
            totalItem = 0;
            currentItem = 0;
            iQtyBad = 0;

            SetAllAsmPartListDisp(Visibility.Collapsed);
            SetAllBtnDisp(Visibility.Hidden);

            if (OnWorkstationTread)
            {
                ScanBarcodeBlock.Dispatcher.Invoke(new Action(() => ScanBarcodeBlock.Visibility = Visibility.Hidden));
                txtOrderStatus.Dispatcher.Invoke(new Action(() => txtOrderStatus.Visibility = Visibility.Collapsed));
                RejectOrderScreen.Dispatcher.Invoke(new Action(() => RejectOrderScreen.Visibility = Visibility.Collapsed));
            }
            else
            {
                ScanBarcodeBlock.Visibility = Visibility.Hidden;
                txtOrderStatus.Visibility = Visibility.Collapsed;
                RejectOrderScreen.Visibility = Visibility.Collapsed;
            }

            for (int i = 1; i <= 5; i++)
            {
                UpdateAsmBadQty(i, "");
            }

            if (OnWorkstationTread)
            {
                AsmPart.Dispatcher.Invoke(new Action(() => AsmPart.ucText = ""));
                QtyBad.Dispatcher.Invoke(new Action(() => QtyBad.ucText = ""));

                OrderNo.Dispatcher.Invoke(new Action(() => OrderNo.ucText = ""));
                AssetTag.Dispatcher.Invoke(new Action(() => AssetTag.ucText = ""));

                txtOrderStatus.Dispatcher.Invoke(new Action(() => txtOrderStatus.Text = ""));
            }
            else
            {
                AsmPart.ucText = "";
                QtyBad.ucText = "";

                OrderNo.ucText = "";
                AssetTag.ucText = "";

                txtOrderStatus.Text = "";
            }

        }
        private void InitPickPackStage()
        {
            UpdateAsmPartList(StationPartList);
            totalItem = StationPartList.Count;
            currentItem = 0;
            AsmPart.ucText = StationPartList[currentItem].PartNumber;
            QtyBad.ucText = "0";
            string errMsg = "";


            HighlightAsmItem(currentItem + 1, Brushes.LightBlue);
            GoodPartUsedList = new List<LogicClasses.PartInstalled>();
            BadPartUsedList = new List<LogicClasses.PartInstalled>();

            if (OnWorkstationTread)
            {
                btnBad.Dispatcher.Invoke(new Action(() => btnBad.Visibility = Visibility.Visible));
                string sZone = Classes.GlobalFunctions.Lifter1Station.ToString();
                if (sZone.Substring(sZone.Length - 1, 1) == "6" || sZone.Substring(sZone.Length - 1, 1) == "7")
                {
                    this.Dispatcher.Invoke(new Action(() => btnReject.Visibility = Visibility.Hidden));
                }
                else
                {
                    if (chkOrderSts)
                        btnReject.Dispatcher.Invoke(new Action(() => btnReject.Visibility = Visibility.Visible));
                }
                btnReset.Dispatcher.Invoke(new Action(() => btnReset.Visibility = Visibility.Visible));
            }
            else
            {
                btnBad.Visibility = Visibility.Visible;

                string sZone = Classes.GlobalFunctions.Lifter1Station.ToString();
                if (sZone.Substring(sZone.Length - 1, 1) == "6" || sZone.Substring(sZone.Length - 1, 1) == "7")
                {
                    this.Dispatcher.Invoke(new Action(() => btnReject.Visibility = Visibility.Hidden));
                }
                else
                {
                    if (chkOrderSts)
                        btnReject.Visibility = Visibility.Visible;
                }
                btnReset.Visibility = Visibility.Visible;
            }

            if (totalItem == 1)
            {
                if (OnWorkstationTread)
                {
                    btnOK.Dispatcher.Invoke(new Action(() => btnOK.Visibility = Visibility.Hidden));
                    btnPass.Dispatcher.Invoke(new Action(() => btnPass.Visibility = Visibility.Visible));
                }
                else
                {
                    btnOK.Visibility = Visibility.Hidden;
                    btnPass.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (OnWorkstationTread)
                {
                    btnOK.Dispatcher.Invoke(new Action(() => btnOK.Visibility = Visibility.Visible));
                    btnPass.Dispatcher.Invoke(new Action(() => btnPass.Visibility = Visibility.Hidden));
                }
                else
                {
                    btnOK.Visibility = Visibility.Visible;
                    btnPass.Visibility = Visibility.Hidden;
                }
            }

            PromptBarcodeScan(StationPartList[currentItem].HaveSN);
            if (OnWorkstationTread)
            {
                txtBarcode.Dispatcher.Invoke(new Action(() => txtBarcode.Focus()));
            }
            else
            {
                txtBarcode.Focus();
            }

            CheckStockLevel();

        }

        private void CheckStockLevel()
        {
            string errMsg = "";
            int lowCnt = 0;
            int highCnt = 0;
            int zeroCnt = 0;
            int totCnt = 0;

            DataTable dt;

            //Need to make sure if DBCall able to support Zone? By SA:11Jun2018
            ////if (Classes.GlobalFunctions.StationType.ToString() == "RobotCell")
            ////    dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, "", ref errMsg);
            ////else
            ////    dt = DBCall.Part_Select_Info_Zone(Classes.GlobalFunctions.Lifter1Station, "", ref errMsg);

            // Use this for PLC version. By SA:12-Jul-2018
            //Zone and Part has different update
            if (Classes.GlobalFunctions.StationType.ToUpper() == "SUBSTATION" || Classes.GlobalFunctions.StationType.ToUpper() == "WORKSTATION")
            {
                dt = DBCall.Part_Select_Info_Zone(Classes.GlobalFunctions.Lifter1Station, string.Empty, ref errMsg, Classes.GlobalFunctions.StationName);
            }
            else
            {
                dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, "", ref errMsg);
            }

            //dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, "", ref errMsg);

            if (errMsg == "")
            {
                foreach (DataRow r in dt.Rows)
                {
                    int qty = Convert.ToInt32(r["StockQty"].ToString());
                    int qtyLimit = Convert.ToInt32(r["StockLimit"].ToString());
                    int upperLimit = Convert.ToInt32(r["UpperLimit"].ToString());
                    if (qty < qtyLimit)
                    {
                        totCnt++;
                        if (qty == 0)
                        {
                            zeroCnt++;
                        }
                        else
                        {
                            lowCnt++;
                        }
                    }
                    else
                    {
                        if (qty > upperLimit)
                        {
                            totCnt++;
                            highCnt++;
                        }
                    }
                }

                if (zeroCnt > 0)
                {
                    plc.PLC_Write("bool_WarningBit[4]", "True", Logix.Tag.ATOMIC.BOOL);
                    ////if (!GetAlamCodeStatus("2001"))
                    ////{
                    ////    DBCall.Alarm_Insert("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                    ////    if (errMsg != "")
                    ////    {
                    ////        //MessageBox.Show("ERROR: " + errMsg);
                    ////    }
                    ////}
                }
                else
                {
                    plc.PLC_Write("bool_WarningBit[4]", "False", Logix.Tag.ATOMIC.BOOL);
                    ////DBCall.Alarm_TurnOff("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                }


                if (lowCnt > 0)
                {
                    plc.PLC_Write("bool_WarningBit[5]", "True", Logix.Tag.ATOMIC.BOOL);
                    //if (!GetAlamCodeStatus("5001"))
                    //{
                    //    ////DBCall.Alarm_Insert("5001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                    //    ////if (errMsg != "")
                    //    ////{
                    //    ////    //MessageBox.Show("ERROR: " + errMsg);
                    //    ////}
                    //}
                }
                else
                {
                    plc.PLC_Write("bool_WarningBit[5]", "False", Logix.Tag.ATOMIC.BOOL);
                    ////DBCall.Alarm_TurnOff("5001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                }
            }
        }
        private void CheckStockLevel_DB()
        {
            string errMsg = "";
            int lowCnt = 0;
            int zeroCnt = 0;
            int totCnt = 0;


            DataTable dt;
            ////if (Classes.GlobalFunctions.StationType.ToString() == "RobotCell")
            ////    dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, "", ref errMsg);
            ////else
            ////    dt = DBCall.Part_Select_Info_Zone(Classes.GlobalFunctions.Lifter1Station, "", ref errMsg);

            // Use this for PLC version. By SA:12-Jul-2018
            dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, "", ref errMsg);

            if (errMsg == "")
            {
                foreach (DataRow r in dt.Rows)
                {
                    int qty = Convert.ToInt32(r["StockQty"].ToString());
                    int qtyLimit = Convert.ToInt32(r["StockLimit"].ToString());
                    if (qty < qtyLimit)
                    {
                        totCnt++;
                        if (qty == 0)
                        {
                            zeroCnt++;
                            //if (!GetAlamCodeStatus("2001"))
                            //{
                            //    DBCall.Alarm_Insert("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                            //    if (errMsg != "")
                            //    {
                            //        MessageBox.Show("ERROR: " + errMsg);
                            //    }
                            //}
                        }
                        else
                        {
                            lowCnt++;
                            //if (!GetAlamCodeStatus("5001"))
                            //{
                            //    DBCall.Alarm_Insert("5001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                            //    if (errMsg != "")
                            //    {
                            //        MessageBox.Show("ERROR: " + errMsg);
                            //    }
                            //}
                        }

                    }
                }
                if (totCnt == 0)
                {

                    ////DBCall.Alarm_TurnOff("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                }
                if (zeroCnt > 0)
                {
                    if (!GetAlamCodeStatus("2001"))
                    {
                        ////DBCall.Alarm_Insert("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                        ////if (errMsg != "")
                        ////{
                        ////    //MessageBox.Show("ERROR: " + errMsg);
                        ////}
                    }
                }
                else
                {
                    ////DBCall.Alarm_TurnOff("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                }


                if (lowCnt > 0)
                {
                    if (!GetAlamCodeStatus("5001"))
                    {
                        ////DBCall.Alarm_Insert("5001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                        ////if (errMsg != "")
                        ////{
                        ////    //MessageBox.Show("ERROR: " + errMsg);
                        ////}
                    }
                }
                else
                {
                    ////DBCall.Alarm_TurnOff("5001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                }
            }
        }

        private bool GetAlamCodeStatus(string alamCode)
        {
            bool rtnFlag = false;
            string errMsg = "";
            //!!!
            //DataTable dt = DBCall.Alarm_Select(Classes.GlobalFunctions.StationName.ToString(), ref errMsg);
            DataTable dt = DBCall.Alarm_Select("3", Classes.GlobalFunctions.StationName.ToString(), ref errMsg);
            if (errMsg == "")
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (alamCode == r["AlmCode"].ToString())
                    {
                        rtnFlag = true;
                        break;
                    }
                }
            }

            return rtnFlag;
        }
        #endregion

        #region Event
        void tcpClient_OnDataReceived(string Message)
        {
            // Data received event.
            //MessageBox.Show(Message);
            txtBarcode.Dispatcher.Invoke(new Action(() => txtBarcode.Text = Message.TrimEnd()));
            if (tcpClient != null)
                sendMessageToTcpServer("LOFF");
        }

        void tcpClient_OnConnectionStatus(string connectionStatus, string IP)
        {
            // Connection status event
            if (connectionStatus.Equals("Disconnected")) { MessageBox.Show("CPU Barcode Scanner not connected"); }
        }

        void sendMessageToTcpServer(string msg)
        {
            //tcpClient.Send("LON");
            if (tcpClient != null)
                tcpClient.Send(msg);
        }



        private void btnBad_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Button Bad Click");

            //Foong 1030
            string ErrTopup = CheckValidUpdate(iQtyBad + 1, StationPartList[currentItem].PartNumber, false);
            if (ErrTopup.Length > 0)
            {
                MessageBox.Show(ErrTopup);
                return;
            }

            if (!CheckBarcodeScanned(StationPartList[currentItem].HaveSN))
            {
                MessageBox.Show("Barcode Scan Required");
                return;
            }

            iQtyBad += 1;
            QtyBad.ucText = iQtyBad.ToString();
            addUsedPart(currentItem, ref BadPartUsedList);
            UpdateAsmBadQty(currentItem + 1, iQtyBad.ToString() + " Bad");

            if (CheckBarcodeScanned(StationPartList[currentItem].HaveSN))
            {
                txtBarcode.Text = "";
                txtBarcode.Focus();
            }

            CheckStockLevel();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            string ErrMsg = "";
            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Button Next Click");

            // OK button is NEXT button
            //Before Next Item

            //Foong 1030
            string ErrTopup = CheckValidUpdate(iQtyBad + 1, StationPartList[currentItem].PartNumber, false);
            if (ErrTopup.Length > 0)
            {
                MessageBox.Show(ErrTopup);
                return;
            }

            if (!CheckBarcodeScanned(StationPartList[currentItem].HaveSN))
            {
                MessageBox.Show("Barcode Scan Required");
                return;
            }

            if (Classes.GlobalFunctions.ScannerIP.ToString() != "NA")
                if (!(DBCall.Parts_Storage_Select_Exists(StationPartList[currentItem].PartNumber, txtBarcode.Text, ref ErrMsg)))
                {
                    MessageBox.Show(StationPartList[currentItem].PartNumber + " - Part Serial Number (" + txtBarcode.Text + ") already existed");
                    return;
                }

            addUsedPart(currentItem, ref GoodPartUsedList);


            //Prepare to go Next Item
            currentItem++;
            UpdateAsmBadQty(currentItem, "1 Good, " + iQtyBad.ToString() + " Bad");
            HighlightAsmItem(currentItem + 1, Brushes.LightBlue);
            iQtyBad = 0;
            QtyBad.ucText = iQtyBad.ToString();

            AsmPart.ucText = StationPartList[currentItem].PartNumber;
            PromptBarcodeScan(StationPartList[currentItem].HaveSN);
            if (currentItem >= totalItem - 1)
            {
                btnOK.Visibility = Visibility.Hidden;
                //btnBad.Visibility = Visibility.Hidden;
                btnPass.Visibility = Visibility.Visible;

                if (chkSts)
                {
                    txtBarcode.IsEnabled = true;
                    txtBarcode.Focus();
                    btnFixScan.Visibility = Visibility.Collapsed;
                }
            }

            CheckStockLevel();

        }
        private void btnPass_Click(object sender, RoutedEventArgs e)
        {

            string ErrMsg = "";
            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Button Pass Click");

            string ErrTopup = CheckValidUpdate(iQtyBad + 1, StationPartList[currentItem].PartNumber, false);
            if (ErrTopup.Length > 0)
            {
                MessageBox.Show(ErrTopup);
                return;
            }

            if (!CheckBarcodeScanned(StationPartList[currentItem].HaveSN))
            {
                MessageBox.Show("Barcode Scan Required");
                return;
            }

            if (Classes.GlobalFunctions.ScannerIP.ToString() != "NA")
                if (!(DBCall.Parts_Storage_Select_Exists(StationPartList[currentItem].PartNumber, txtBarcode.Text, ref ErrMsg)))
                {
                    MessageBox.Show(StationPartList[currentItem].PartNumber + " - Part Serial Number (" + txtBarcode.Text + ") already existed");
                    return;
                }

            addUsedPart(currentItem, ref GoodPartUsedList);

            sendPartUsedList();


            sendMatching();

            Tag T = new Tag();
            if (Classes.GlobalFunctions.StationName == "11")
            {
                T.Name = "Z12A_T_MANUALDATA.Operator_Done";
            }
            else if (Classes.GlobalFunctions.StationName == "12")
            {
                T.Name = "Z24A_T_MANUALDATA.Operator_Done";
            }

            int n = 0;
            Retry:
            T.Value = true;
            plc.PLC_WriteByTag(ref T);
            if (T.QualityCode != ResultCode.QUAL_GOOD && n < 3)
            {
                n++;
                goto Retry;
            }


            string aa = mAssetTag;
            //if(Classes.GlobalFunctions.StationType.ToString()== "RobotCell")
            //if(mAssetTag!="N/A")
            //    sendComplete();

            CheckStockLevel();

            RequestWorkOrder();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Button Reset Click");

            RequestWorkOrder();

        }

        //Foong
        private string CheckValidUpdate(int qtyadd, string CurrentItem, bool Plus = true)
        {
            string errMsg = "";

            string InvalidTopUp = string.Empty;

            DataTable dt;
            if (Classes.GlobalFunctions.StationType.ToUpper() == "SUBSTATION" || Classes.GlobalFunctions.StationType.ToUpper() == "WORKSTATION")
            {
                dt = DBCall.Part_Select_Info_Zone(Classes.GlobalFunctions.Lifter1Station, CurrentItem, ref errMsg, Classes.GlobalFunctions.StationName);
            }
            else
            {
                dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, CurrentItem, ref errMsg);
            }

            if (errMsg == "")
            {
                foreach (DataRow r in dt.Rows)
                {
                    int qty = Convert.ToInt32(r["StockQty"].ToString()) + (Plus ? qtyadd : -qtyadd);
                    int qtyLimit = Convert.ToInt32(r["StockLimit"].ToString());
                    int upperLimit = Convert.ToInt32(r["UpperLimit"].ToString());

                    if (Plus)
                    {
                        if (qty > upperLimit)
                        {
                            InvalidTopUp = "Quantity Exceed Upper Limit!";
                        }
                    }
                    else
                    {
                        if (qty < qtyLimit && qty >= 0)
                            MessageBox.Show("Low Stock Quantity Warning!");

                        if (qty < 0)
                            InvalidTopUp = "Quantity Out of Stock";
                    }
                }
            }

            return InvalidTopUp;
        }

        private void btnReject_Click(object sender, RoutedEventArgs e)
        {

            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Button Reject Click");

            //Foong 1030
            //string ErrTopup = CheckValidUpdate(iQtyBad + 1, StationPartList[currentItem].PartNumber, false);
            //if (ErrTopup.Length > 0)
            //{
            //    MessageBox.Show(ErrTopup);
            //    return;
            //}

            UpdateAsmBadQty(currentItem + 1, "0 Good, " + iQtyBad.ToString() + " Bad");


            string msg = "Please check Assemble Part List on items used for this order\n";
            if (BadPartUsedList.Count > 0)
                msg += " Bad Parts\n";
            foreach (LogicClasses.PartInstalled pUsed in BadPartUsedList)
                msg += $"    { pUsed.StationID} : {pUsed.OrderID} : {pUsed.AssetTag} : {pUsed.PartNumber} : {pUsed.Barcode}\n";

            if (GoodPartUsedList.Count > 0)
                msg += " Good Parts\n";
            foreach (LogicClasses.PartInstalled pUsed in GoodPartUsedList)
            {
                if (pUsed.AssetTag.ToUpper() != "N/A")
                    msg += $"    {pUsed.StationID} : {pUsed.OrderID} : {pUsed.AssetTag} :  {pUsed.PartNumber} : {pUsed.Barcode}\n";
            }

            RejectOrderScreen.Visibility = Visibility.Visible;
            txt_Msg.Text = msg;

            Tag T = new Tag();
            if (Classes.GlobalFunctions.StationName == "11")
            {
                T.Name = "Z12A_T_MANUALDATA.Operator_Done";
            }
            else if (Classes.GlobalFunctions.StationName == "12")
            {
                T.Name = "Z24A_T_MANUALDATA.Operator_Done";
            }

            int n = 0;
            Retry:
            T.Value = true;
            plc.PLC_WriteByTag(ref T);
            if (T.QualityCode != ResultCode.QUAL_GOOD && n < 3)
            {
                n++;
                goto Retry;
            }

            CheckStockLevel();

            //if (MessageBox.Show(msg,"Confirm Reject", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            //{
            //    sendPartUsedList();
            //    sendReject();

            //    sendComplete();
            //    RequestWorkOrder();
            //}
            //else
            //{
            //    return;
            //}

        }
        #endregion

        #region Destructor
        ~ucWorkstation()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (Classes.GlobalFunctions.ScannerIP.ToString() != "NA")
            {
                if (tcpClient != null)
                {
                    tcpClient.OnConnectionStatus -= new TCPIPClient.ConnectionStatusEvent(tcpClient_OnConnectionStatus);
                    tcpClient.OnDataReceived -= new TCPIPClient.DataReceivedEvent(tcpClient_OnDataReceived);
                    tcpClient.Disconnect();
                    //MessageBox.Show("After Disconnect" + tcpClient);
                }
            }
        }




        #endregion

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            txt_Msg.Text = "";
            txt_Remarks.Text = "";
            RejectOrderScreen.Visibility = Visibility.Collapsed;

            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Button No Click");
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            sendPartUsedList();
            sendReject();
            string bb = mAssetTag;
            //if(Classes.GlobalFunctions.StationType.ToString()== "RobotCell")
            //if (mAssetTag != "N/A")
            //    sendComplete();
            CheckStockLevel();

            txt_Msg.Text = "";
            txt_Remarks.Text = "";
            RejectOrderScreen.Visibility = Visibility.Collapsed;

            Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", "Button Yes Click");

            RequestWorkOrder();
        }

        private void btnFixScan_Click(object sender, RoutedEventArgs e)
        {
            txtBarcode.Text = "";
            sendMessageToTcpServer("LON");
        }
    }
}
