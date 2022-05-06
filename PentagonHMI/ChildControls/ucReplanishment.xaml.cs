using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Data;

namespace PentagonHMI.ChildControls
{
    /// <summary>
    /// Interaction logic for ucReplanishment.xaml
    /// </summary>
    public partial class ucReplanishment : UserControl, IDisposable
    {
        private VanillaDB.DataDBCall DBCall;

        // Allen Bradley PLC TCP Connection
        // ////////////////////////////////
        Utilities.TcpAllenB rpmPlc = new Utilities.TcpAllenB();

        List<LogicClasses.StationPart> StationPartList;
        List<LogicClasses.StationPart> StationPartList_2;

        public ucReplanishment()
        {
            InitializeComponent();
            string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
            DBCall = new VanillaDB.DataDBCall(Connstr);

            string sZone = Classes.GlobalFunctions.Lifter1Station.ToString();
            if (!(sZone.Substring(sZone.Length - 1, 1) == "6" || sZone.Substring(sZone.Length - 1, 1) == "7"))
            {
                if (!rpmPlc.PLC_Connect(Classes.GlobalFunctions.PLC_IPAddress,
                                Classes.GlobalFunctions.PLC_Path,
                                Convert.ToInt16(Classes.GlobalFunctions.PLC_Timeout.ToString())))
                {
                    MessageBox.Show("Fail Connect PLC :\n" + rpmPlc.ERROR_MSG);
                }
            }


            //lbl_HeaderTitle_1.Content = $"Replenishment Zone ({Classes.GlobalFunctions.Lifter1Station})";
            //lbl_HeaderTitle_2.Content = $"Replenishment Zone ({Classes.GlobalFunctions.Lifter2Station})";

            lbl_HeaderTitle_1.Content = $"Replenishment";
            //lbl_HeaderTitle_2.Content = $"Replenishment Zone #2";

            if (Classes.GlobalFunctions.TotalLifter.ToString() == "1")
            {
                Title_2.Visibility = Visibility.Collapsed;
                ItemList_2.Visibility = Visibility.Collapsed;
            }
            SetAllAsmPartListDisp(Visibility.Collapsed);

            InitStationPart();
        }

        private void InitStationPart()
        {
            StationPartList = getReplanishmentPartsList(Classes.GlobalFunctions.Lifter1Station.ToString());
            int i = 0;
            foreach (LogicClasses.StationPart sPart in StationPartList)
            {
                SetItemDisplay(i + 1, sPart.PartNumber, sPart.Qty, Visibility.Visible);
                i++;
            }

            // Only need to show one Replenishment screen
            // Change on 11-Jul-2018. By SA
            // ===========================================
            //StationPartList_2 = getReplanishmentPartsList(Classes.GlobalFunctions.Lifter2Station.ToString());
            //int j = 0;
            //foreach(LogicClasses.StationPart sPart2 in StationPartList_2)
            //{
            //    SetItemDisplay_2(j + 1, sPart2.PartNumber, sPart2.Qty, Visibility.Visible);
            //    j++;
            //} 

        }

        private List<LogicClasses.StationPart> getReplanishmentPartsList(string zone)
        {
            string errMsg = "";
            List<LogicClasses.StationPart> stPlist;
            stPlist = new List<LogicClasses.StationPart>();

            // Do we still support Zone? By SA:11Jul-2018
            //DataTable dt = DBCall.Part_Select_Info_Zone(zone, "", ref errMsg);
            //DataTable dt = DBCall.Part_Select_Info(Classes.GlobalFunctions.StationName, "", ref errMsg);

            // Use this for PLC version. By SA:12-Jul-2018
            DataTable dt;

            if (Classes.GlobalFunctions.StationType.ToUpper() == "SUBSTATION" || Classes.GlobalFunctions.StationType.ToUpper() == "WORKSTATION")
            {
                dt = DBCall.Part_Select_Info_Zone(Classes.GlobalFunctions.Lifter1Station, string.Empty, ref errMsg, Classes.GlobalFunctions.StationName);
            }
            else
            {
                dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, "", ref errMsg);
            }

            if (dt != null)
            {
                foreach (DataRow r in dt.Rows)
                {
                    stPlist.Add(new LogicClasses.StationPart
                    {
                        PartNumber = r["PartModelID"].ToString(),
                        Qty = r["StockQty"].ToString(),
                        iQty = Convert.ToInt32(r["StockQty"].ToString()),
                        LowLimit = r["StockLimit"].ToString(),
                        iLowLimit = Convert.ToInt32(r["StockLimit"].ToString()),
                        UpperLimit = r["UpperLimit"].ToString(),
                        iUpperLimit = Convert.ToInt32(r["UpperLimit"].ToString())

                    });
                }
            }

            return stPlist;
        }

        private void SetAllAsmPartListDisp(Visibility visibility)
        {
            SetAsmPartListDisp(visibility);
            //SetAsmPartListDisp_2(visibility);

            //this.Item_1.Dispatcher.Invoke(new Action(() => this.Item_1.Visibility = visibility));
            //this.Item_2.Dispatcher.Invoke(new Action(() => this.Item_2.Visibility = visibility));
            //this.Item_3.Dispatcher.Invoke(new Action(() => this.Item_3.Visibility = visibility));
            //this.Item_4.Dispatcher.Invoke(new Action(() => this.Item_4.Visibility = visibility));
            //this.Item_5.Dispatcher.Invoke(new Action(() => this.Item_5.Visibility = visibility));

            //this.Item_1.Visibility = visibility;
            //this.Item_2.Visibility = visibility;
            //this.Item_3.Visibility = visibility;
            //this.Item_4.Visibility = visibility;
            //this.Item_5.Visibility = visibility;
            //this.Qty_1.Visibility = visibility;
            //this.Qty_2.Visibility = visibility;
            //this.Qty_3.Visibility = visibility;
            //this.Qty_4.Visibility = visibility;
            //this.Qty_5.Visibility = visibility;
            //this.btn_AddItem1.Visibility = visibility;
            //this.btn_AddItem2.Visibility = visibility;
            //this.btn_AddItem3.Visibility = visibility;
            //this.btn_AddItem4.Visibility = visibility;
            //this.btn_AddItem5.Visibility = visibility;
        }
        private void SetAsmPartListDisp(Visibility visibility)
        {
            this.Item_1.Visibility = visibility;
            this.Item_2.Visibility = visibility;
            this.Item_3.Visibility = visibility;
            this.Item_4.Visibility = visibility;
            this.Item_5.Visibility = visibility;
            this.Qty_1.Visibility = visibility;
            this.Qty_2.Visibility = visibility;
            this.Qty_3.Visibility = visibility;
            this.Qty_4.Visibility = visibility;
            this.Qty_5.Visibility = visibility;
            this.btn_AddItem1.Visibility = visibility;
            this.btn_AddItem2.Visibility = visibility;
            this.btn_AddItem3.Visibility = visibility;
            this.btn_AddItem4.Visibility = visibility;
            this.btn_AddItem5.Visibility = visibility;
        }
        private void SetAsmPartListDisp_2(Visibility visibility)
        {
            this.Item_1_2.Visibility = visibility;
            this.Item_2_2.Visibility = visibility;
            this.Item_3_2.Visibility = visibility;
            this.Item_4_2.Visibility = visibility;
            this.Item_5_2.Visibility = visibility;
            this.Qty_1_2.Visibility = visibility;
            this.Qty_2_2.Visibility = visibility;
            this.Qty_3_2.Visibility = visibility;
            this.Qty_4_2.Visibility = visibility;
            this.Qty_5_2.Visibility = visibility;
            this.btn_AddItem1_2.Visibility = visibility;
            this.btn_AddItem2_2.Visibility = visibility;
            this.btn_AddItem3_2.Visibility = visibility;
            this.btn_AddItem4_2.Visibility = visibility;
            this.btn_AddItem5_2.Visibility = visibility;
        }



        private void SetItemDisplay(int index, string item, string qty, Visibility visibility)
        {
            if (index == 1)
            {
                this.Item_1.ucText = item;
                this.Qty_1.ucText = qty;
                this.Item_1.Visibility = visibility;
                this.Qty_1.Visibility = visibility;
                this.btn_AddItem1.Visibility = visibility;
                return;
            }
            if (index == 2)
            {
                this.Item_2.ucText = item;
                this.Qty_2.ucText = qty;
                this.Item_2.Visibility = visibility;
                this.Qty_2.Visibility = visibility;
                this.btn_AddItem2.Visibility = visibility;
                return;
            }
            if (index == 3)
            {
                this.Item_3.ucText = item;
                this.Qty_3.ucText = qty;
                this.Item_3.Visibility = visibility;
                this.Qty_3.Visibility = visibility;
                this.btn_AddItem3.Visibility = visibility;
                return;
            }
            if (index == 4)
            {
                this.Item_4.ucText = item;
                this.Qty_4.ucText = qty;
                this.Item_4.Visibility = visibility;
                this.Qty_4.Visibility = visibility;
                this.btn_AddItem4.Visibility = visibility;
                return;
            }
            if (index == 5)
            {
                this.Item_5.ucText = item;
                this.Qty_5.ucText = qty;
                this.Item_5.Visibility = visibility;
                this.Qty_5.Visibility = visibility;
                this.btn_AddItem5.Visibility = visibility;
                return;
            }
        }
        private void SetItemDisplay_2(int index, string item, string qty, Visibility visibility)
        {
            if (index == 1)
            {
                this.Item_1_2.ucText = item;
                this.Qty_1_2.ucText = qty;
                this.Item_1_2.Visibility = visibility;
                this.Qty_1_2.Visibility = visibility;
                this.btn_AddItem1_2.Visibility = visibility;
                return;
            }
            if (index == 2)
            {
                this.Item_2_2.ucText = item;
                this.Qty_2_2.ucText = qty;
                this.Item_2_2.Visibility = visibility;
                this.Qty_2_2.Visibility = visibility;
                this.btn_AddItem2_2.Visibility = visibility;
                return;
            }
            if (index == 3)
            {
                this.Item_3_2.ucText = item;
                this.Qty_3_2.ucText = qty;
                this.Item_3_2.Visibility = visibility;
                this.Qty_3_2.Visibility = visibility;
                this.btn_AddItem3_2.Visibility = visibility;
                return;
            }
            if (index == 4)
            {
                this.Item_4_2.ucText = item;
                this.Qty_4_2.ucText = qty;
                this.Item_4_2.Visibility = visibility;
                this.Qty_4_2.Visibility = visibility;
                this.btn_AddItem4_2.Visibility = visibility;
                return;
            }
            if (index == 5)
            {
                this.Item_5_2.ucText = item;
                this.Qty_5_2.ucText = qty;
                this.Item_5_2.Visibility = visibility;
                this.Qty_5_2.Visibility = visibility;
                this.btn_AddItem5_2.Visibility = visibility;
                return;
            }
        }

        private void CheckStockLevel()
        {
            string errMsg = "";
            int lowCnt = 0;
            int highCnt = 0;
            int zeroCnt = 0;
            int totCnt = 0;

            DataTable dt;
            ////**dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, "", ref errMsg);

            //Need to make sure if DBCall able to support Zone? By SA:11Jun2018
            ////if (Classes.GlobalFunctions.StationType.ToString() == "RobotCell")
            ////    dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, "", ref errMsg);
            ////else
            ////    dt = DBCall.Part_Select_Info_Zone(Classes.GlobalFunctions.Lifter1Station, "", ref errMsg);

            //dt = DBCall.Part_Select_Info_Zone(Classes.GlobalFunctions.Lifter1Station, "", ref errMsg);

            // Use this for PLC version. By SA:12-Jul-2018
            //Foong Zone and Station has different update
            if (Classes.GlobalFunctions.StationType.ToUpper() == "SUBSTATION" || Classes.GlobalFunctions.StationType.ToUpper() == "WORKSTATION")
            {
                dt = DBCall.Part_Select_Info_Zone(Classes.GlobalFunctions.Lifter1Station, string.Empty, ref errMsg, Classes.GlobalFunctions.StationName);
            }
            else
            {
                dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, "", ref errMsg);
            }


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
                            //Foong
                        }
                        else
                        {
                            lowCnt++;
                            //Foong
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
                    rpmPlc.PLC_Write("bool_WarningBit[4]", "True", Logix.Tag.ATOMIC.BOOL);
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
                    rpmPlc.PLC_Write("bool_WarningBit[4]", "False", Logix.Tag.ATOMIC.BOOL);
                    ////DBCall.Alarm_TurnOff("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                }


                if (lowCnt > 0)
                {
                    rpmPlc.PLC_Write("bool_WarningBit[5]", "True", Logix.Tag.ATOMIC.BOOL);
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
                    rpmPlc.PLC_Write("bool_WarningBit[5]", "False", Logix.Tag.ATOMIC.BOOL);
                    ////DBCall.Alarm_TurnOff("5001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                }
            }
        }

        private bool GetAlamCodeStatus(string alamCode)
        {
            bool rtnFlag = false;
            string errMsg = "";

            //!!!
            //DataTable dt = DBCall.Alarm_Select(Classes.GlobalFunctions.StationName.ToString(),  ref errMsg);
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

        private void btn_AddItem1_Click(object sender, RoutedEventArgs e)
        {
            TopupScreen.Visibility = Visibility.Visible;
            PartItem_TopUp.ucTextBox.Text = Item_1.ucText;
            PartQty_Topup.ucTextBox.Text = Qty_1.ucText;
            ucQtyAdd.ucTextBox.Focus();
        }
        private void btn_AddItem2_Click(object sender, RoutedEventArgs e)
        {
            TopupScreen.Visibility = Visibility.Visible;
            PartItem_TopUp.ucTextBox.Text = Item_2.ucText;
            PartQty_Topup.ucTextBox.Text = Qty_2.ucText;
            ucQtyAdd.ucTextBox.Focus();
        }
        private void btn_AddItem3_Click(object sender, RoutedEventArgs e)
        {
            TopupScreen.Visibility = Visibility.Visible;
            PartItem_TopUp.ucTextBox.Text = Item_3.ucText;
            PartQty_Topup.ucTextBox.Text = Qty_3.ucText;
            ucQtyAdd.ucTextBox.Focus();
        }
        private void btn_AddItem4_Click(object sender, RoutedEventArgs e)
        {
            TopupScreen.Visibility = Visibility.Visible;
            PartItem_TopUp.ucTextBox.Text = Item_4.ucText;
            PartQty_Topup.ucTextBox.Text = Qty_4.ucText;
            ucQtyAdd.ucTextBox.Focus();
        }
        private void btn_AddItem5_Click(object sender, RoutedEventArgs e)
        {
            TopupScreen.Visibility = Visibility.Visible;
            PartItem_TopUp.ucTextBox.Text = Item_5.ucText;
            PartQty_Topup.ucTextBox.Text = Qty_5.ucText;
            ucQtyAdd.ucTextBox.Focus();
        }


        //Foong 1030
        private string CheckValidUpdate(int qtyadd, bool Plus = true)
        {
            string errMsg = "";

            string InvalidTopUp = string.Empty;

            DataTable dt;
            if (Classes.GlobalFunctions.StationType.ToUpper() == "SUBSTATION" || Classes.GlobalFunctions.StationType.ToUpper() == "WORKSTATION")
            {
                dt = DBCall.Part_Select_Info_Zone(Classes.GlobalFunctions.Lifter1Station, PartItem_TopUp.Text, ref errMsg, Classes.GlobalFunctions.StationName);
            }
            else
            {
                dt = DBCall.Part_Select_StockQty(Classes.GlobalFunctions.StationName, PartItem_TopUp.Text, ref errMsg);
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
                            InvalidTopUp = "Quantity cannot be negative";
                    }     
                }
            }

            return InvalidTopUp;
        }

        private void btnTopupQty_Click(object sender, RoutedEventArgs e)
        {
            int intTopqty;
            string errMsg = "";
            if (int.TryParse(this.ucQtyAdd.Text, out intTopqty))
            {
                if (intTopqty > 0)
                {
                    //Foong 1030
                    string ErrTopup = CheckValidUpdate(intTopqty);
                    if (ErrTopup.Length > 0)
                    {
                        this.ucQtyAdd.ucLabelErrorContent = ErrTopup;
                        return;
                    }

                    DBCall.Part_Update_Quantity("1", Classes.GlobalFunctions.Lifter1Station.ToString(), PartItem_TopUp.Text.ToString(), intTopqty, ref errMsg);
                    if (errMsg == "")
                    {
                        PartItem_TopUp.ucTextBox.Text = "";
                        PartQty_Topup.ucTextBox.Text = "";
                        ucQtyAdd.ucTextBox.Text = "";
                        this.ucQtyAdd.ucLabelErrorContent = "";
                        TopupScreen.Visibility = Visibility.Collapsed;

                        ////DBCall.Alarm_TurnOff("5001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                        ////DBCall.Alarm_TurnOff("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                        CheckStockLevel();

                        //rpmPlc.PLC_Write("intUpdateStockQty", "1", Logix.Tag.ATOMIC.DINT);
                        if (rpmPlc.PLC_Write("intUpdateStockQty", "1", Logix.Tag.ATOMIC.DINT) == "ERROR")
                        {
                            //MessageBox.Show("PLC Write Fail" + plc.ERROR_MSG);
                            Utilities.FileLogger.logError("PLC Write Fail @ btnTopupQty_Click; " + rpmPlc.ERROR_MSG, "intUpdateStockQty");
                        }


                        Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: Top Up Success! ! ");
                    }
                    else
                    {
                        this.ucQtyAdd.ucLabelErrorContent = "Top Up Fail!";
                        Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: Top Up Fail! ! ");
                    }

                }
                else
                {
                    this.ucQtyAdd.ucLabelErrorContent = "TOP UP Number Must Great Than 0 !";
                    Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: TOP UP Number Must Great Than 0 ! ");
                }
            }
            else
            {
                this.ucQtyAdd.ucLabelErrorContent = "Invalid Number!";
                Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: Invalid Number! ");
            }


            InitStationPart();
        }
        private void btnDeduct_Click(object sender, RoutedEventArgs e)
        {
            int intTopqty;
            string errMsg = "";
            if (int.TryParse(this.ucQtyAdd.Text, out intTopqty))
            {
                if (intTopqty > 0)
                {
                    string ErrTopup = CheckValidUpdate(intTopqty, false);
                    if (ErrTopup.Length > 0)
                    {
                        this.ucQtyAdd.ucLabelErrorContent = ErrTopup;
                        return;
                    }


                    DBCall.Part_Update_Quantity("1", Classes.GlobalFunctions.Lifter1Station.ToString(), PartItem_TopUp.Text.ToString(), intTopqty * -1, ref errMsg);
                    if (errMsg == "")
                    {
                        PartItem_TopUp.ucTextBox.Text = "";
                        PartQty_Topup.ucTextBox.Text = "";
                        ucQtyAdd.ucTextBox.Text = "";
                        this.ucQtyAdd.ucLabelErrorContent = "";
                        TopupScreen.Visibility = Visibility.Collapsed;

                        ////DBCall.Alarm_TurnOff("5001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                        ////DBCall.Alarm_TurnOff("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                        CheckStockLevel();

                        //rpmPlc.PLC_Write("intUpdateStockQty", "1", Logix.Tag.ATOMIC.DINT);
                        if (rpmPlc.PLC_Write("intUpdateStockQty", "1", Logix.Tag.ATOMIC.DINT) == "ERROR")
                        {
                            //MessageBox.Show("PLC Write Fail" + plc.ERROR_MSG);
                            Utilities.FileLogger.logError("PLC Write Fail @ btnDeduct_Click; " + rpmPlc.ERROR_MSG, "intUpdateStockQty");
                        }

                        Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: Deduct Success! ! ");
                    }
                    else
                    {
                        this.ucQtyAdd.ucLabelErrorContent = "Deduct Qty Fail!";
                        Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: Deduct Fail! ! ");
                    }

                }
                else
                {
                    this.ucQtyAdd.ucLabelErrorContent = "Deduct Number Must Great Than 0 !";
                    Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: Deduct Number Must Great Than 0 ! ");
                }
            }
            else
            {
                this.ucQtyAdd.ucLabelErrorContent = "Invalid Number!";
                Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: Invalid Number! ");
            }


            InitStationPart();

        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            PartItem_TopUp.ucTextBox.Text = "";
            PartQty_Topup.ucTextBox.Text = "";
            ucQtyAdd.ucTextBox.Text = "";
            this.ucQtyAdd.ucLabelErrorContent = "";
            TopupScreen.Visibility = Visibility.Collapsed;
        }

        private void btn_AddItem1_2_Click(object sender, RoutedEventArgs e)
        {
            TopupScreen_2.Visibility = Visibility.Visible;
            PartItem_TopUp_2.ucTextBox.Text = Item_1_2.ucText;
            PartQty_Topup_2.ucTextBox.Text = Qty_1_2.ucText;
            ucQtyAdd_2.ucTextBox.Focus();
        }
        private void btn_AddItem2_2_Click(object sender, RoutedEventArgs e)
        {
            TopupScreen_2.Visibility = Visibility.Visible;
            PartItem_TopUp_2.ucTextBox.Text = Item_2_2.ucText;
            PartQty_Topup_2.ucTextBox.Text = Qty_2_2.ucText;
            ucQtyAdd_2.ucTextBox.Focus();
        }
        private void btn_AddItem3_2_Click(object sender, RoutedEventArgs e)
        {
            TopupScreen_2.Visibility = Visibility.Visible;
            PartItem_TopUp_2.ucTextBox.Text = Item_3_2.ucText;
            PartQty_Topup_2.ucTextBox.Text = Qty_3_2.ucText;
            ucQtyAdd_2.ucTextBox.Focus();
        }
        private void btn_AddItem4_2_Click(object sender, RoutedEventArgs e)
        {
            TopupScreen_2.Visibility = Visibility.Visible;
            PartItem_TopUp_2.ucTextBox.Text = Item_4_2.ucText;
            PartQty_Topup_2.ucTextBox.Text = Qty_4_2.ucText;
            ucQtyAdd_2.ucTextBox.Focus();
        }
        private void btn_AddItem5_2_Click(object sender, RoutedEventArgs e)
        {
            TopupScreen_2.Visibility = Visibility.Visible;
            PartItem_TopUp_2.ucTextBox.Text = Item_5_2.ucText;
            PartQty_Topup_2.ucTextBox.Text = Qty_5_2.ucText;
            ucQtyAdd_2.ucTextBox.Focus();
        }
        private void btnTopupQty_2_Click(object sender, RoutedEventArgs e)
        {
            int intTopqty;
            string errMsg = "";
            if (int.TryParse(this.ucQtyAdd_2.Text, out intTopqty))
            {
                if (intTopqty > 0)
                {
                    DBCall.Part_Update_Quantity("1", Classes.GlobalFunctions.Lifter2Station.ToString(), PartItem_TopUp_2.Text.ToString(), intTopqty, ref errMsg);
                    if (errMsg == "")
                    {
                        PartItem_TopUp_2.ucTextBox.Text = "";
                        PartQty_Topup_2.ucTextBox.Text = "";
                        ucQtyAdd_2.ucTextBox.Text = "";
                        this.ucQtyAdd_2.ucLabelErrorContent = "";
                        TopupScreen_2.Visibility = Visibility.Collapsed;

                        ////DBCall.Alarm_TurnOff("5001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                        ////DBCall.Alarm_TurnOff("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                        CheckStockLevel();

                        Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp_2.Text + " : " + this.ucQtyAdd_2.Text + " :: Top Up Success! ! ");
                    }
                    else
                    {
                        this.ucQtyAdd_2.ucLabelErrorContent = "Top Up Fail!";
                        Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp_2.Text + " : " + this.ucQtyAdd_2.Text + " :Fail: Top Up Fail! ! ");
                    }

                }
                else
                {
                    this.ucQtyAdd.ucLabelErrorContent = "TOP UP Number Must Great Than 0 !";
                    Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: TOP UP Number Must Great Than 0 ! ");
                }
            }
            else
            {
                this.ucQtyAdd.ucLabelErrorContent = "Invalid Number!";
                Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: Invalid Number! ");
            }


            InitStationPart();
        }
        private void btnCancel_2_Click(object sender, RoutedEventArgs e)
        {
            PartItem_TopUp_2.ucTextBox.Text = "";
            PartQty_Topup_2.ucTextBox.Text = "";
            ucQtyAdd_2.ucTextBox.Text = "";
            this.ucQtyAdd_2.ucLabelErrorContent = "";
            TopupScreen_2.Visibility = Visibility.Collapsed;
        }
        private void btnDeduct_2_Click(object sender, RoutedEventArgs e)
        {
            int intTopqty;
            string errMsg = "";
            if (int.TryParse(this.ucQtyAdd_2.Text, out intTopqty))
            {
                if (intTopqty > 0)
                {
                    DBCall.Part_Update_Quantity("1", Classes.GlobalFunctions.Lifter2Station.ToString(), PartItem_TopUp_2.Text.ToString(), intTopqty * -1, ref errMsg);
                    if (errMsg == "")
                    {
                        PartItem_TopUp_2.ucTextBox.Text = "";
                        PartQty_Topup_2.ucTextBox.Text = "";
                        ucQtyAdd_2.ucTextBox.Text = "";
                        this.ucQtyAdd_2.ucLabelErrorContent = "";
                        TopupScreen_2.Visibility = Visibility.Collapsed;

                        ////DBCall.Alarm_TurnOff("5001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                        ////DBCall.Alarm_TurnOff("2001", Classes.GlobalFunctions.StationName.ToString(), "", ref errMsg);
                        CheckStockLevel();

                        Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp_2.Text + " : " + this.ucQtyAdd_2.Text + " :: Deduct Success! ! ");
                    }
                    else
                    {
                        this.ucQtyAdd_2.ucLabelErrorContent = "Deduct Fail!";
                        Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp_2.Text + " : " + this.ucQtyAdd_2.Text + " :Fail: Deduct Fail! ! ");
                    }

                }
                else
                {
                    this.ucQtyAdd.ucLabelErrorContent = "Deduct Number Must Great Than 0 !";
                    Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: Deduct Number Must Great Than 0 ! ");
                }
            }
            else
            {
                this.ucQtyAdd.ucLabelErrorContent = "Invalid Number!";
                Utilities.FileLogger.LogEvent(Main.EventDt, Classes.UserAccess.gl_UserName, "Info", this.PartItem_TopUp.Text + " : " + this.ucQtyAdd.Text + " :Fail: Invalid Number! ");
            }


            InitStationPart();
        }

        public void Dispose()
        {
            rpmPlc.PLC_Disconnect();
        }

        #region Destructor
        ~ucReplanishment()
        {
            Dispose();
        }


        #endregion


    }
}
