using Logix;
using PentagonHMI.UserControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Utilities;

namespace PentagonHMI.ChildControls
{
    public partial class ucSetting : UserControl, IDisposable//, INotifyPropertyChanged
    {
        #region Variables

        private string StrSetting = "Setting Page";
        private List<string> checkValues = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };
        private LogicClasses.Main _Main;
        private bool valid = false;
        private string ErrMsg;
        private DateTime date;
        private int chk;
        private DateTime Shift1st;
        private DateTime Shift2nd;
        private DateTime Shift3rd;
        private VanillaDB.DataDBCall DBCall;
        private bool FirstRun = true;
        private DataTable dt_BreakTime;
        private DataTable dt_OffDay;

        #endregion Variables

        #region Constructor

        public ucSetting(ref LogicClasses.Main MainConnection)
        {
            InitializeComponent();

            try
            {
                _Main = MainConnection;
                DBCall = new VanillaDB.DataDBCall(Properties.Settings.Default.DatabaseConnectionString.ToString());

                //if(ProjectType.ARCADIA == Classes.GlobalFunctions.ProjectType)
                //{
                //    if(StationType.ARCADIA_Main == Classes.GlobalFunctions.StationType)
                //    {
                //        SP_OEEBlock.Visibility = Visibility.Visible;
                //    }
                //    //SP_MainOEEReset.Visibility = Visibility.Visible;
                //}
                //else
                //{
                //    SP_OEEBlock.Visibility = Visibility.Visible;
                //}

                UpdateBreakTimeTable();
                UpdateOffDayTable();
                ShiftSelect();
                SetupControl();
                _Main.OnSettingUpdate += new LogicClasses.Main.onSettingUpdateHandler(Setting_OnUpdate);
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Failed to Load Setting Page");
            }
        }

        private void SetupControl()
        {
            try
            {
                ucGeneral._Main = _Main;
                string Pretitle = string.Empty;
                DataTable DT = _Main.SQLer.Exec_DTSelect("SELECT * FROM [SETTING] ORDER BY [LAYOUTINDEX]");
                foreach(DataRow dr in DT.Rows)
                {
                    string Tagname = dr["TAGNAME"].ToString().TrimEnd();
                    Tag.ATOMIC MIC = Logix.Tag.ATOMIC.BOOL;
                    string ty = dr["TYPE"].ToString().ToUpper();
                    if(ty == "STRING")
                        MIC = Logix.Tag.ATOMIC.STRING;
                    else if(ty == "DINT")
                        MIC = Logix.Tag.ATOMIC.DINT;
                    else if(ty == "INT")
                        MIC = Logix.Tag.ATOMIC.INT;
                    else if(ty == "REAL")
                        MIC = Logix.Tag.ATOMIC.REAL;

                    //Info.OPC.TagGroups.Setting.AddTag(new Tag { Name = Tagname, DataType = MIC, MyObject = dr });

                    string Maintitle = dr["MAINTITLE"].ToString();
                    if(Maintitle != Pretitle)
                    {
                        Pretitle = Maintitle;
                        ucGeneral.SettingList.Add(new ucSettingBlock.SettingModel
                        {
                            MainTitle = Maintitle,
                            Config = new List<ucSettingBlock.SettingBlockModel>()
                        });
                    }
                    int curindex = ucGeneral.SettingList.Count - 1;
                    //bool isbool = dr["MAX"] == DBNull.Value;
                    int Max = dr["MAX"] == DBNull.Value ? int.MaxValue : Convert.ToInt32(dr["MAX"]);
                    int Min = dr["MIN"] == DBNull.Value ? 0 : Convert.ToInt32(dr["MIN"]);
                    Type type;
                    bool action = false;
                    switch(dr["TYPE"].ToString().ToUpper())
                    {
                        case "STRING":
                            type = typeof(string);
                            break;

                        case "BOOL":
                        default:
                            type = typeof(bool);
                            break;

                        case "INT":
                        case "DINT":
                            type = typeof(int);
                            break;

                        case "REAL":
                            type = typeof(double);
                            break;

                        case "BOOL;ACTION":
                            type = typeof(bool);
                            action = true;
                            break;
                    }
                    ucGeneral.SettingList[curindex].Config.Add(new ucSettingBlock.SettingBlockModel
                    {
                        Key = Tagname,
                        Num_Max = Max,
                        Num_Min = Min,
                        Title = dr["TITLE"].ToString(),
                        Vis_isNum = (type == typeof(int) || type == typeof(string) || type == typeof(double)) ? Visibility.Visible : Visibility.Collapsed,
                        Vis_isToggle = (type == typeof(bool) && !action) ? Visibility.Visible : Visibility.Collapsed,
                        ActionVisibility = (type == typeof(bool) && action) ? Visibility.Visible : Visibility.Collapsed,
                        DataType = type,
                        Tag = new Tag
                        {
                            Name = Tagname,
                            DataType = MIC
                        }
                    });
                }

                //Info.OPC.Add_TagGroup(ref Info.OPC.TagGroups.Setting);
                ucGeneral.SetControl();
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #region Events

        private string PreAccessLevel = "NA";
        private bool AccessLevelChanged = false;

        private void Setting_OnUpdate()
        {
            try
            {
                if(_Main.UserAccessLevel != PreAccessLevel)
                {
                    PreAccessLevel = _Main.UserAccessLevel;
                    AccessLevelChanged = true;
                }

                if(_Main.HasTorqueDriver)
                {
                    Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                    {
                        if(lblStatusDesc.Content.ToString() != _Main.Tcpip_ArcadiaTorqueDriver.ConnectionStatus)
                        {
                            lblStatusDesc.Content = _Main.Tcpip_ArcadiaTorqueDriver.ConnectionStatus;
                        }
                    }));
                }

                if(true/*ucGeneral.PendingUpdate || FirstRun || AccessLevelChanged*/)
                {
                    System.Threading.Thread.Sleep(2000);
                    FirstRun = false;
                    ucGeneral.PendingUpdate = false;

                    if(_Main.HasTorqueDriver)
                    {
                        if(PreAccessLevel.ToUpper() == "PENTA")
                        {
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                            {
                                SP_TorqueBlock.Visibility = Visibility.Visible;
                            }));
                        }
                        else
                        {
                            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                            {
                                SP_TorqueBlock.Visibility = Visibility.Collapsed;
                            }));
                        }
                    }

                    ucGeneral.SettingList.ForEach(x =>
                    {
                        try
                        {
                            x.Config.ForEach(y =>
                            {
                                try
                                {
                                    if(!string.IsNullOrEmpty(y.Tag.Name))
                                    {
                                        _Main.MyPLC.ReadTag(y.Tag);

                                        if(y.Tag.Value != null)
                                        {
                                            if(y.Vis_isNum == Visibility.Visible)
                                            {
                                                int value = Convert.ToInt32(y.Tag.Value);

                                                if(y.Num_Value != value)
                                                {
                                                    y.Num_Value = value;
                                                }
                                                //FileLogger.logSetting("[HMI - Settings]", $"[HMI - Settings] {y.Key} - Data Change | Tag : {y.Tag.Name} | Value : {value}");
                                            }
                                            else if(y.Vis_isToggle == Visibility.Visible)
                                            {
                                                bool value = Convert.ToBoolean(y.Tag.Value);

                                                if(y.Tg_Stat != value)
                                                {
                                                    y.Tg_Stat = value;
                                                }
                                                //FileLogger.logSetting("[HMI - Settings]", $"[HMI - Settings] {y.Key} - Data Change | Tag : {y.Tag.Name} | Value : {value}");
                                            }
                                        }
                                    }
                                }
                                catch(Exception exception)
                                {
                                    Utilities.FileLogger.logError(exception.Message, exception.ToString());
                                }
                            });
                        }
                        catch(Exception exception)
                        {
                            Utilities.FileLogger.logError(exception.Message, exception.ToString());
                        }
                    });

                    //foreach (Tag T in Info.OPC.TagGroups.Setting.Tags)
                    //{
                    //    DataRow dr = T.MyObject as DataRow;
                    //    var item = ucGeneral.SettingList[Convert.ToInt32(dr["LAYOUTINDEX"])].Config.Where(x => x.Key == T.Name).First();
                    //    if (item != null)
                    //    {
                    //        if (AccessLevelChanged)
                    //            item.Vis_Access = dr["ACCESSLEVEL"].ToString().ToUpper() == "PENTA" && _Main.UserAccessLevel.ToUpper() != "PENTA" ? Visibility.Collapsed : Visibility.Visible;

                    //        if (item.Vis_isToggle == Visibility.Visible)
                    //            item.Tg_Stat = Convert.ToBoolean(T.Value);
                    //        else if (item.Vis_isNum == Visibility.Visible)
                    //            item.Num_Value = Convert.ToInt32(T.Value);
                    //    }
                    //}
                    AccessLevelChanged = false;
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion Events

        private void ShiftSelect()
        {
            //try
            //{
            //    DataTable DT = DBCall.Shift_Select(" ", _Main.StationID, ref ErrMsg);

            //    foreach(DataRow DR in DT.Rows)
            //    {
            //        switch(DR["ShiftID"].ToString())
            //        {
            //            case "1":
            //                hr.Text = (Convert.ToDateTime(DR["ShiftTime"].ToString())).ToString("%h");
            //                min.Text = (Convert.ToDateTime(DR["ShiftTime"].ToString())).Minute.ToString();
            //                cb1.SelectedIndex = Convert.ToDateTime(DR["ShiftTime"].ToString()).ToString("tt", CultureInfo.InvariantCulture).ToUpper() == "AM" ? 0 : 1;
            //                if(min.Text.Length == 1)
            //                    min.Text = "0" + min.Text;
            //                Act.IsChecked = DR["Active"].ToString() == "True" ? true : false;
            //                hr.IsEnabled = DR["Active"].ToString() == "True" ? true : false;
            //                min.IsEnabled = DR["Active"].ToString() == "True" ? true : false;
            //                cb1.IsEnabled = DR["Active"].ToString() == "True" ? true : false;
            //                break;

            //            case "2":
            //                hr2.Text = (Convert.ToDateTime(DR["ShiftTime"].ToString())).ToString("%h");
            //                min2.Text = (Convert.ToDateTime(DR["ShiftTime"].ToString())).Minute.ToString();
            //                if(min2.Text.Length == 1)
            //                    min2.Text = "0" + min2.Text;
            //                cb2.SelectedIndex = Convert.ToDateTime(DR["ShiftTime"].ToString()).ToString("tt", CultureInfo.InvariantCulture).ToUpper() == "AM" ? 0 : 1;
            //                Act2.IsChecked = DR["Active"].ToString() == "True" ? true : false;
            //                hr2.IsEnabled = DR["Active"].ToString() == "True" ? true : false;
            //                min2.IsEnabled = DR["Active"].ToString() == "True" ? true : false;
            //                cb2.IsEnabled = DR["Active"].ToString() == "True" ? true : false;
            //                break;

            //            case "3":
            //                hr3.Text = (Convert.ToDateTime(DR["ShiftTime"].ToString())).ToString("%h");
            //                min3.Text = (Convert.ToDateTime(DR["ShiftTime"].ToString())).Minute.ToString();
            //                if(min3.Text.Length == 1)
            //                    min3.Text = "0" + min3.Text;
            //                cb3.SelectedIndex = Convert.ToDateTime(DR["ShiftTime"].ToString()).ToString("tt", CultureInfo.InvariantCulture).ToUpper() == "AM" ? 0 : 1;
            //                Act3.IsChecked = DR["Active"].ToString() == "True" ? true : false;
            //                hr3.IsEnabled = DR["Active"].ToString() == "True" ? true : false;
            //                min3.IsEnabled = DR["Active"].ToString() == "True" ? true : false;
            //                cb3.IsEnabled = DR["Active"].ToString() == "True" ? true : false;
            //                break;
            //        }
            //    }
            //}
            //catch(Exception ex)
            //{
            //    Utilities.FileLogger.logError(ex.Message, ex.ToString());
            //}
        }

        #endregion Constructor

        #region Methods

        public void Dispose()
        {
            try
            {
                _Main.SettingPageON = false;
                Info.OPC.TagGroups.Setting.Active = false;
                FirstRun = true;
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion Methods

        private void rb_check(object sender, RoutedEventArgs e)
        {
            try
            {
                RadioButton RB = (RadioButton)sender;
                if(RB.Tag == null)
                    RB.Tag = "True";

                if(RB.Tag.ToString() == "False")
                {
                    RB.Tag = "True";
                }

                //if(RB.Name == Act3.Name)
                //{
                //    if(Act2.IsChecked == false)
                //    {
                //        RB.Tag = "False";
                //    }
                //}
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void act_click(object sender, RoutedEventArgs e)
        {
            try
            {
                Tag tag = new Logix.Tag();
                RadioButton RB = (RadioButton)sender;
                Utilities.FileLogger.logButton(StrSetting, "Act Click - " + RB.Name, MethodBase.GetCurrentMethod().ToString());

                if(RB.Tag != null)
                {
                    if(RB.Tag.ToString() == "True" && RB.IsChecked == true)
                    {
                        RB.Tag = "False";
                    }
                    else
                    {
                        RB.IsChecked = false;

                        if (Act2.IsChecked == false)
                        {
                            Act3.IsChecked = false;
                            hr3.IsEnabled = false;
                            min3.IsEnabled = false;
                            cb3.IsEnabled = false;
                        }

                        if (RB.Name == "Act")
                        {
                            RB.IsChecked = true;
                        }
                    }
                }

                if(RB.Name.ToString().Contains("Act"))
                {
                    if(!(Boolean)RB.IsChecked)
                    {
                        if(RB.Name == "Act2")
                        {
                            hr2.IsEnabled = false;
                            min2.IsEnabled = false;
                            cb2.IsEnabled = false;
                        }

                        if(RB.Name == "Act3")
                        {
                            hr3.IsEnabled = false;
                            min3.IsEnabled = false;
                            cb3.IsEnabled = false;
                        }
                    }
                    else
                    {
                        if(RB.Name == "Act2")
                        {
                            hr2.IsEnabled = true;
                            min2.IsEnabled = true;
                            cb2.IsEnabled = true;
                        }

                        if(RB.Name == "Act3")
                        {
                            hr3.IsEnabled = true;
                            min3.IsEnabled = true;
                            cb3.IsEnabled = true;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Fail to update Machine Status (Setting)");
            }
        }

        #region Destructor

        ~ucSetting()
        {
            Dispose();
        }

        #endregion Destructor

        private void sl_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox TB = (TextBox)sender;

                TB.Background = Brushes.Transparent;

                if(!(int.TryParse(TB.Text.ToString(), out chk)))
                {
                    TB.Text = "";
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void hr_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                valid = true;
                TextBox TB = (TextBox)sender;

                if(TB.Text.Length > 2)
                {
                    TB.Text = TB.Text.Substring(TB.Text.Length - 2, 2);
                }

                if(int.TryParse(TB.Text.ToString(), out chk))
                {
                    if(TB.Tag.ToString() == "hr")
                    {
                        if(!(checkValues.Contains(TB.Text.ToString())))
                        {
                            valid = false;
                        }
                    }

                    if(TB.Tag.ToString() == "min")
                    {
                        if(Convert.ToInt32(TB.Text) > 59)
                        {
                            valid = false;
                        }
                    }
                }
                else
                {
                    valid = false;
                }

                if(!(valid))
                    TB.Text = "";
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void oeeset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utilities.FileLogger.logButton(StrSetting, "Set OEE Shift", MethodBase.GetCurrentMethod().ToString());

                if (min.Text.ToString().Length == 1)
                    min.Text = "0" + min.Text;

                if (min2.Text.ToString().Length == 1)
                    min2.Text = "0" + min2.Text;

                if (min3.Text.ToString().Length == 1)
                    min3.Text = "0" + min3.Text;

                valid = true;
                var DatenTime = DateTime.Now;
                string Shift1 = DatenTime.ToShortDateString() + " " + hr.Text.ToString() + ":" + min.Text.ToString() + " " + cb1.Text.ToString();
                string Shift2 = DatenTime.ToShortDateString() + " " + hr2.Text.ToString() + ":" + min2.Text.ToString() + " " + cb2.Text.ToString();
                string Shift3 = DatenTime.ToShortDateString() + " " + hr3.Text.ToString() + ":" + min3.Text.ToString() + " " + cb3.Text.ToString();

                if (DateTime.TryParse(Shift1, out date))
                    Shift1st = Convert.ToDateTime(Shift1);
                else
                    valid = false;

                if (DateTime.TryParse(Shift2, out date))
                    Shift2nd = Convert.ToDateTime(Shift2);
                else
                    valid = false;

                if (DateTime.TryParse(Shift3, out date))
                    Shift3rd = Convert.ToDateTime(Shift3);
                else
                    valid = false;

                if (valid == true)
                {
                    valid = false;
                    if (Shift3rd > Shift2nd || Act3.IsChecked == false)
                    {
                        if (Shift2nd > Shift1st || Act2.IsChecked == false)
                            valid = true;
                        else
                            MessageBox.Show("Schedule 1 timing should come before Schedule 2");
                    }
                    else
                        MessageBox.Show("Schedule 2 timing should come before Schedule 3");
                }

                if (valid == true)
                {
                    DataTable dt = DBCall.Shift_Select("1", _Main.StationID, ref ErrMsg);
                    if (string.IsNullOrEmpty(ErrMsg))
                        DBCall.Shift_Update("1", _Main.StationID, Convert.ToDateTime(dt.Rows[0]["NextShiftDT"]).ToString(), Shift1, Act.IsChecked == true ? "1" : "0", "User", ref ErrMsg);
                    else
                        DBCall.Shift_Update("1", _Main.StationID, Shift1, Shift1, Act.IsChecked == true ? "1" : "0", "User", ref ErrMsg);

                    dt = _Main.SQLer.Exec_DTSelect($"SELECT * FROM [Shift] WHERE StationID = {_Main.StationID} AND ShiftID = 2;");
                    if (dt.Rows.Count == 1)
                        DBCall.Shift_Update("2", _Main.StationID, Convert.ToDateTime(dt.Rows[0]["NextShiftDT"]).ToString(), Shift2, Act2.IsChecked == true ? "1" : "0", "User", ref ErrMsg);
                    else
                        DBCall.Shift_Update("2", _Main.StationID, Shift2, Shift2, Act2.IsChecked == true ? "1" : "0", "User", ref ErrMsg);

                    dt = _Main.SQLer.Exec_DTSelect($"SELECT * FROM [Shift] WHERE StationID = {_Main.StationID} AND ShiftID = 3;");
                    if (dt.Rows.Count == 1)
                        DBCall.Shift_Update("3", _Main.StationID, Convert.ToDateTime(dt.Rows[0]["NextShiftDT"]).ToString(), Shift3, Act3.IsChecked == true ? "1" : "0", "User", ref ErrMsg);
                    else
                        DBCall.Shift_Update("3", _Main.StationID, Shift3, Shift3, Act3.IsChecked == true ? "1" : "0", "User", ref ErrMsg);

                    //if (Act.IsChecked == true)
                    //{
                    //    DBCall.Shift_Update("1", _Main.StationID, Shift1, Shift1, "1", "User", ref ErrMsg);
                    //}
                    //else
                    //{
                    //    DBCall.Shift_Update("1", _Main.StationID, Shift1, Shift1, "0", "User", ref ErrMsg);
                    //}

                    //if (Act2.IsChecked == true)
                    //{
                    //    DBCall.Shift_Update("2", _Main.StationID, Shift2, Shift2, "1", "User", ref ErrMsg);
                    //}
                    //else
                    //{
                    //    DBCall.Shift_Update("2", _Main.StationID, Shift2, Shift2, "0", "User", ref ErrMsg);
                    //}

                    //if (Act3.IsChecked == true)
                    //{
                    //    DBCall.Shift_Update("3", _Main.StationID, Shift3, Shift3, "1", "User", ref ErrMsg);
                    //}
                    //else
                    //{
                    //    DBCall.Shift_Update("3", _Main.StationID, Shift3, Shift3, "0", "User", ref ErrMsg);
                    //}

                    if (ErrMsg == "")
                    {
                        ShiftSelect();
                        MessageBox.Show("Shift Updated");
                    }
                    else
                        Utilities.FileLogger.logError("OEE Shift Update Failed", ErrMsg);
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "OEE Shift Update Failed");
            }
        }

        private void hr_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            try
            {
                TextBox TB = (TextBox)sender;
                if(TB.Text.Length == 1)
                {
                    if(e.Key == System.Windows.Input.Key.Back)
                    {
                        e.Handled = true;
                    }

                    if(e.Key == System.Windows.Input.Key.Delete)
                    {
                        e.Handled = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        //private void OEEReset_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        Utilities.FileLogger.logButton(StrSetting, "Reset OEE", MethodBase.GetCurrentMethod().ToString());

        //        if (Classes.GlobalFunctions.StationType == StationType.ARCADIA_Main &&
        //            Classes.GlobalFunctions.ProjectType == ProjectType.ARCADIA)
        //        {
        //            if (System.Windows.Forms.MessageBox.Show("Are You Sure to Reset Line OEE?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
        //            {
        //                _Main.SQLer.Exec_NonQuery(@"Update [dbo].[Shift]
        //                                SET [NextShiftDT] = GETDATE() - 1,
        //                                [UpdatedBy] = 'ME',
        //                                [updatedDT] = getdate()
        //                                where [ShiftID] = 99");
        //                MessageBox.Show("Line OEE Reset Triggered");

        //                //string[] folders = Directory.GetDirectories(_Main.CheckPath);
        //                //foreach (string folder in folders)
        //                //    File.Create(Path.Combine(folder, _Main.ResetOEEMagicWord));
        //            };
        //        }
        //        else
        //        {
        //            if (System.Windows.Forms.MessageBox.Show("Are You Sure to Reset OEE?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
        //            {
        //                string Past = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd HH:mm:ss");
        //                DBCall.Shift_Reset("99", "1", Past, "User", ref ErrMsg);
        //                MessageBox.Show("OEE Reset Triggered");
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.FileLogger.logError(ex.Message, ex.ToString());
        //    }
        //}

        private void OEEReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utilities.FileLogger.logButton(StrSetting, "Reset OEE", MethodBase.GetCurrentMethod().ToString());

                if(Classes.GlobalFunctions.StationType == StationType.ARCADIA_Main &&
                    Classes.GlobalFunctions.ProjectType == ProjectType.ARCADIA)
                {
                    if(System.Windows.Forms.MessageBox.Show("Are You Sure to Reset Line OEE?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataTable OEEShift = _Main.MainSQLer.Exec_DTSelect(@"Select * from Shift where ShiftID != '99' AND StationID = '" + _Main.StationID + @"' AND Active = 1");

                        if(ErrMsg == "")
                        {
                            DateTime currentShift = DateTime.Now;

                            // All rows, get the current active shift
                            int rowIndex = 0;
                            foreach(DataRow DR in OEEShift.Rows)
                            {
                                if(rowIndex == 0)
                                    currentShift = Convert.ToDateTime(DR["NextShiftDT"]);
                                else if(Convert.ToDateTime(DR["NextShiftDT"]) < currentShift)
                                {
                                    currentShift = Convert.ToDateTime(DR["NextShiftDT"]);
                                    rowIndex = OEEShift.Rows.IndexOf(DR);
                                }
                            }

                            string ShiftID = OEEShift.Rows[rowIndex]["ShiftID"].ToString();
                            DateTime ShiftDateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(OEEShift.Rows[rowIndex]["ShiftTime"]).ToShortTimeString());

                            if(DateTime.Now > ShiftDateTime)
                                ShiftDateTime = ShiftDateTime.AddDays(1);

                            if(ShiftDateTime != null)
                            {
                                DBCall.Shift_Reset(ShiftID, Classes.GlobalFunctions.StationName.ToString(), ShiftDateTime.ToString("yyyy/MM/dd HH:mm:ss"), "User", ref ErrMsg);
                                DBCall.Shift_Reset("99", Classes.GlobalFunctions.StationName.ToString(), DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"), "User", ref ErrMsg);
                            }
                        }
                        MessageBox.Show("Line OEE Reset Triggered");
                    }
                }
                else
                {
                    if(System.Windows.Forms.MessageBox.Show("Are You Sure to Reset Machine OEE?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
                    {
                        DataTable OEEShift = _Main.SQLer.Exec_DTSelect(@"Select * from Shift where ShiftID != '99' AND StationID = '" + _Main.StationID + @"' AND Active = 1");

                        if(ErrMsg == "")
                        {
                            DateTime currentShift = DateTime.Now;

                            // All rows, get the current active shift
                            int rowIndex = 0;
                            foreach(DataRow DR in OEEShift.Rows)
                            {
                                if(rowIndex == 0)
                                    currentShift = Convert.ToDateTime(DR["NextShiftDT"]);
                                else if(Convert.ToDateTime(DR["NextShiftDT"]) < currentShift)
                                {
                                    currentShift = Convert.ToDateTime(DR["NextShiftDT"]);
                                    rowIndex = OEEShift.Rows.IndexOf(DR);
                                }
                            }

                            string ShiftID = OEEShift.Rows[rowIndex]["ShiftID"].ToString();
                            DateTime ShiftDateTime = Convert.ToDateTime(DateTime.Now.ToShortDateString() + " " + Convert.ToDateTime(OEEShift.Rows[rowIndex]["ShiftTime"]).ToShortTimeString());

                            if(ShiftDateTime > DateTime.Now)
                                ShiftDateTime = ShiftDateTime.AddDays(-1);

                            if(ShiftDateTime != null)
                            {
                                DBCall.Shift_Reset(ShiftID, Classes.GlobalFunctions.StationName.ToString(), ShiftDateTime.ToString("yyyy/MM/dd HH:mm:ss"), "User", ref ErrMsg);
                            }
                        }
                        MessageBox.Show("OEE Reset Triggered");
                    }
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void RemoveBreakTime_Click(object sender, RoutedEventArgs e)
        {
            DataRowView item = dgd_BreakTime.SelectedItem as DataRowView;
            DataTable dt = _Main.SQLer.Exec_DTSelect($"DELETE FROM [NONSCHEDULEDDOWNTIME] WHERE [ID]= {item.Row["ID"]} AND [TYPE] = 'TIME'; SELECT [ID] FROM [NONSCHEDULEDDOWNTIME] WHERE [TYPE] = 'TIME' ORDER BY [ID] ASC;");
            string SQLUpdateID = string.Empty;
            int nID = 1;
            foreach (DataRow data in dt.Rows)
                SQLUpdateID += $"UPDATE [NONSCHEDULEDDOWNTIME] SET [ID] = {nID++} WHERE [ID] = {data[0]} AND [TYPE] = 'TIME';";
            if (!string.IsNullOrWhiteSpace(SQLUpdateID))
                _Main.SQLer.Exec_NonQuery(SQLUpdateID);
            UpdateBreakTimeTable();
        }

        private void RemoveOffDay_Click(object sender, RoutedEventArgs e)
        {
            DataRowView item = dgd_OffDay.SelectedItem as DataRowView;
            DataTable dt = _Main.SQLer.Exec_DTSelect($"DELETE FROM [NONSCHEDULEDDOWNTIME] WHERE [ID]= {item.Row["ID"]} AND [TYPE] = 'DAY'; SELECT [ID] FROM [NONSCHEDULEDDOWNTIME] WHERE [TYPE] = 'DAY' ORDER BY [ID] ASC;");
            string SQLUpdateID = string.Empty;
            int nID = 1;
            foreach (DataRow data in dt.Rows)
                SQLUpdateID += $"UPDATE [NONSCHEDULEDDOWNTIME] SET [ID] = {nID++} WHERE [ID] = {data[0]} AND [TYPE] = 'DAY';";
            if (!string.IsNullOrWhiteSpace(SQLUpdateID))
                _Main.SQLer.Exec_NonQuery(SQLUpdateID);
            UpdateOffDayTable();
        }

        private void AddBreakTime_Click(object sender, RoutedEventArgs e)
        {
            TimeSpan End = new TimeSpan(GetTime(hr_breakend.Text, meridiem_breakend.Text), Convert.ToInt32(min_breakend.Text), 0);
            TimeSpan Start = new TimeSpan(GetTime(hr_breakstart.Text, meridiem_breakstart.Text), Convert.ToInt32(min_breakstart.Text), 0);
            if (Start >= End)
                MessageBox.Show("Start Time should happen before End Time");
            else
                _Main.SQLer.Exec_NonQuery("INSERT INTO [dbo].[NonScheduledDowntime] ([ID],[StartTime],[EndTime],[Type]) VALUES " +
                                          $"((ISNULL((SELECT MAX(ID) FROM [NonSCHEDULEDDOWNTIME]),0)) + 1, '{Start.ToString()}','{End.ToString()}','TIME')");
            UpdateBreakTimeTable();
        }

        private void AddOffDay_Click(object sender, RoutedEventArgs e)
        {
            if (cld_offDay.SelectedDates.Count > 0)
            {
                DateTime Start = cld_offDay.SelectedDates.Min();
                DateTime End = cld_offDay.SelectedDates.Max();

                _Main.SQLer.Exec_NonQuery($@"INSERT INTO [dbo].[NonScheduledDownTime]
                ([ID],[StartDay],[EndDay],[Type]) VALUES
                ((ISNULL((SELECT MAX(ID) FROM [NONSCHEDULEDDOWNTIME]),0)) + 1,'{Start.ToString()}', '{End.ToString()}', 'DAY')");

                UpdateOffDayTable();
            }
        }

        private int GetTime(string hr, string meridiem)
        {
            int hrs = Convert.ToInt32(hr);
            if(meridiem.Contains("A"))
                hrs = hrs == 12 ? 0 : hrs;
            else if(meridiem.Contains("P"))
                hrs = hrs == 12 ? hrs : hrs + 12;

            return hrs;
        }

        private void UpdateBreakTimeTable()
        {
            dt_BreakTime = _Main.SQLer.Exec_DTSelect(
                "SELECT [ID], SUBSTRING( CONVERT(VARCHAR, [STARTTIME],108),1,5) AS 'Start Time'," +
                $"SUBSTRING( CONVERT(VARCHAR,[ENDTIME],108),1,5) AS 'End Time' FROM [NONSCHEDULEDDOWNTIME] WHERE [TYPE] = 'TIME' ORDER BY [ID] ASC");
            if (dt_BreakTime != null)
                dgd_BreakTime.ItemsSource = dt_BreakTime.AsDataView();
        }

        private void UpdateOffDayTable()
        {
            dt_OffDay = _Main.SQLer.Exec_DTSelect($@"SELECT [ID], CONVERT(VARCHAR(10), [StartDay], 111) as 'Start',
            CONVERT(VARCHAR(10), [EndDay], 111) as 'End'
            FROM [{Info.SQL.DatabaseName}].[dbo].[NonScheduledDownTime] Where [Type] = 'Day' ORDER BY [ID]");
            if(dt_OffDay != null)
            {
                dgd_OffDay.ItemsSource = dt_OffDay.AsDataView();
                cld_offDay.BlackoutDates.Clear();
                cld_offDay.SelectedDates.Clear();
                foreach (DataRow dr in dt_OffDay.Rows)
                {
                    var dates = new CalendarDateRange(Convert.ToDateTime(dr["Start"]), Convert.ToDateTime(dr["End"]));
                    cld_offDay.BlackoutDates.Add(dates);
                }
            }
        }

        //private void LineOEEReset_Click(object sender, RoutedEventArgs e)
        //{
        //    if (System.Windows.Forms.MessageBox.Show("Are You Sure to Reset Line OEE?", "Warning", System.Windows.Forms.MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
        //    {
        //        string _LookUpPath = @"D:\UPDATE\Main";
        //        string LookUpPath_1 = Path.GetDirectoryName(_LookUpPath);
        //        string MainFolderName = Path.GetFileName(_LookUpPath);
        //        string SQL = "SQL";
        //        string Key = "New Text Document.txt";
        //        string SQLTargetFile = Path.Combine(_LookUpPath, "OEEReset.SQL");
        //        string[] folders = Directory.GetDirectories(LookUpPath_1);
        //        foreach (string folder in folders)
        //        {
        //            if (Path.GetFileName(folder) == MainFolderName) continue;

        //            if (!Directory.Exists(Path.Combine(folder, SQL)))
        //                Directory.CreateDirectory(Path.Combine(folder, SQL));

        //            File.Copy(SQLTargetFile, Path.Combine(folder, SQL, Path.GetFileName(SQLTargetFile)), true);
        //            File.Create(Path.Combine(folder, Key)).Close();
        //        }

        //        MessageBox.Show("Line OEE Reset Done");
        //    }
        //}

        private void Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_Main.Tcpip_ArcadiaTorqueDriver.ConnectionStatus != "Connected")
                {
                    if(_Main.Tcpip_ArcadiaTorqueDriver.Connect())
                        MessageBox.Show("Connection establish.");
                }
                else
                {
                    MessageBox.Show("Connection is already connected.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void Disconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_Main.Tcpip_ArcadiaTorqueDriver.ConnectionStatus == "Connected")
                {
                    if(_Main.Tcpip_ArcadiaTorqueDriver.Disconnect())
                        MessageBox.Show("Connection disconncted.");
                }
                else
                {
                    MessageBox.Show("Connection is not connected.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void Reconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_Main.Tcpip_ArcadiaTorqueDriver.Reconnect())
                {
                    MessageBox.Show("Reconnect Success.");
                }
                else
                {
                    MessageBox.Show("Fail to reconnect. Please check logs.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void SendHeader1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_Main.Tcpip_ArcadiaTorqueDriver.SendMessage("00200001006         $00"))
                {
                    MessageBox.Show("Send success.");
                }
                else
                {
                    MessageBox.Show("Fail to send message. Please check logs.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void SendHeader2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(_Main.Tcpip_ArcadiaTorqueDriver.SendMessage("006000080010        1201001310000000000000000000000000000001   "))
                {
                    MessageBox.Show("Send success.");
                }
                else
                {
                    MessageBox.Show("Fail to send message. Please check logs.");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }
    }
}