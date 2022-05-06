using System;
using System.Windows;
using System.Windows.Controls;
using Logix;
using System.ComponentModel;
using System.Windows.Controls.Primitives;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using Frm = System.Windows.Forms;
using System.Collections.ObjectModel;
using Library;
using System.Reflection;

namespace PentagonHMI.UserControls
{
    public partial class UCMotorPage : UserControl, INotifyPropertyChanged
    {
        #region V/P
        private bool isDouble = false;
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ExportCurrentPoint;
        public event EventHandler OpenFileLocation;
        public event EventHandler IOUpdate;
        public string strMotorName { get; set; } = "Motor Page";
        private void NotifyChange(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        private double _CurrentPosition = 0;
        public double CurrentPosition
        {
            get { return _CurrentPosition; }
            set
            {
                if (value != _CurrentPosition)
                {
                    _CurrentPosition = value;
                    NotifyChange(new PropertyChangedEventArgs("CurrentPosition"));
                }
            }
        }

        private int _CurrentSpeed = 1;
        public int CurrentSpeed
        {
            get { return _CurrentSpeed; }
            set
            {
                if (value != _CurrentSpeed)
                {
                    _CurrentSpeed = value;
                    NotifyChange(new PropertyChangedEventArgs("CurrentSpeed"));
                }
            }
        }

        private string _ErrorID = string.Empty;
        public string ErrorID
        {
            get { return _ErrorID; }
            set
            {
                if (value != _ErrorID)
                {
                    _ErrorID = value;
                    NotifyChange(new PropertyChangedEventArgs("ErrorID"));
                }
            }
        }

        private ObservableCollection<IO> _ICollection = new ObservableCollection<IO>();
        public ObservableCollection<IO> ICollection
        {
            get { return _ICollection; }
            set { _ICollection = value; NotifyChange(new PropertyChangedEventArgs("ICollection")); }
        }

        private ObservableCollection<IO> _OCollection = new ObservableCollection<IO>();
        public ObservableCollection<IO> OCollection
        {
            get { return _OCollection; }
            set { _OCollection = value; NotifyChange(new PropertyChangedEventArgs("OCollection")); }
        }
        public class IO : INotifyPropertyChangedBase
        {
            public string Content { get; set; }

            private bool _Status = false;
            public bool Status
            {
                get { return _Status; }
                set { _Status = value; OnPropertyChanged("Status"); }
            }
            public int Index { get; set; }
            public string OutputTagName { get; set; }

        }

        public string AxisName { get; set; }
        private int MaxSavePositionNo { get; set; }
        private bool HomeDone { get; set; } = false;
        private bool HomingFromTag { get; set; } = false;
        private bool MoveBusy { get; set; } = false;
        private bool SavePosMoveBusy { get; set; } = false;
        private int intCurrentPos { get; set; } = 0;
        private Dictionary<int, Tag> DicTagSavePos { get; set; } = new Dictionary<int, Logix.Tag>();
        private Dictionary<int, Tag> DicTagMoveSavePos { get; set; } = new Dictionary<int, Logix.Tag>();
        private Dictionary<int, Tag> DicTagSaveCur { get; set; } = new Dictionary<int, Logix.Tag>();
        private Dictionary<int, Tag> DicTagSaveCus { get; set; } = new Dictionary<int, Logix.Tag>();
        private Tag TagSharedPosition { get; set; }
        private Tag TagAllPosition { get; set; }
        #endregion

        #region Setup
        public Controller PLCController { get; set; } = new Controller();
        public Tag Tag1CurrentPosition { get; set; } = new Tag();
        public Tag Tag2CurrentSpeed { get; set; } = new Tag();
        public Tag Tag3HomeDone { get; set; } = new Tag();
        public Tag Tag4StartHome { get; set; } = new Tag();
        public Tag Tag5JogSpeed { get; set; } = new Tag();
        public Tag Tag6JogMode { get; set; } = new Tag();
        public Tag Tag7MoveBusy { get; set; } = new Tag();
        public Tag Tag8AbsPosition { get; set; } = new Tag();
        public Tag Tag9MoveAbs { get; set; } = new Tag();
        public Tag Tag10RelPosition { get; set; } = new Tag();
        public Tag Tag11MoveRel { get; set; } = new Tag();
        public Tag Tag12Homing { get; set; } = new Tag();
        public Tag Tag13ErrorID { get; set; } = new Tag();
        public int PosMax { get; set; } = 999999;
        public int NegMax { get; set; } = -999999;
        public int SpeedMax { get; set; } = 100;
        public int SpeedMin { get; set; } = 1;
        public int Axis { get; set; } = 0;
        public double Proportion { get; set; } = 0;

        private DataTable DTSP = new DataTable();

        public void SetSavePosition(ref DataTable displayuse_SP, ref DataTable referenceuse_SP)
        {
            try
            {
                if (displayuse_SP == null || displayuse_SP.Rows.Count < 1)
                    return;
                if (referenceuse_SP == null || referenceuse_SP.Rows.Count < 1)
                    return;

                if (string.IsNullOrWhiteSpace(TagSharedPosition?.Name ?? string.Empty))
                    TagSharedPosition = new Tag { Name = referenceuse_SP.Rows[0]["HMIKeyInPos"].ToString(), DataType = Logix.Tag.ATOMIC.REAL };

                foreach (DataColumn col in displayuse_SP.Columns) col.ReadOnly = false;
                MaxSavePositionNo = (int)displayuse_SP.AsEnumerable().Max(row => row["No"]);
                int No = 0; int n = 0;
                foreach (DataRow dr in referenceuse_SP.Rows)
                {
                    No = (int)dr["No"];
                    Tag PosValue = new Tag { Name = dr["PositionTagValue"].ToString(), DataType = Logix.Tag.ATOMIC.REAL };

                    if (PLCController?.ReadTag(PosValue) == ResultCode.E_SUCCESS)
                        displayuse_SP.Rows[n]["Position"] = Math.Round(Convert.ToSingle(PosValue.Value) / Proportion, 1);
                    DicTagSavePos.Add(No, PosValue);
                    DicTagSaveCur.Add(No, new Tag { Name = dr["SaveCurrentPosition"].ToString(), DataType = Logix.Tag.ATOMIC.BOOL });
                    DicTagSaveCus.Add(No, new Tag { Name = dr["SaveCustomPosition"].ToString(), DataType = Logix.Tag.ATOMIC.BOOL });
                    n++;
                }

                DTSP = displayuse_SP;
                DgSavePosition.ItemsSource = DTSP.AsDataView();
            }
            catch (Exception ex)
            {
                DO(false, "SetSavePosition: " + ex.Message);
            }
        }

        //public void SetSavePosition(ref DataTable dt, string strMove, string strSharedPos, string strSavePos,
        //            string strSaveCurrent, string strSaveCustom)
        //{
        //    try
        //    {
        //        TagSharedPosition = new Tag { Name = strSharedPos, DataType = Logix.Tag.ATOMIC.REAL };
        //        if (dt == null || dt.Rows.Count < 1)
        //            return;

        //        foreach (DataColumn col in dt.Columns) col.ReadOnly = false;
        //        MaxSavePositionNo = (int)dt.AsEnumerable().Max(row => row["No"]);
        //        int[] theData = new int[MaxSavePositionNo + 1];
        //        TagAllPosition = new Tag { Name = strSavePos + "[0]", Length = MaxSavePositionNo + 1, NetType = typeof(Single) };
        //        if (PLCController.ReadTag(TagAllPosition) == ResultCode.E_SUCCESS)
        //        {
        //            int ns = 0;
        //            foreach (Single s in (Single[])TagAllPosition.Value)
        //            {
        //                theData[ns] = (int)s;
        //                ns++;
        //            }
        //        }

        //        int No = 0; int n = 0;
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            No = (int)dr["No"];

        //            dt.Rows[n]["Position"] = theData[No];
        //            DicTagSavePos.Add(No, new Tag { Name = strSavePos + $"[{No}]", DataType = Logix.Tag.ATOMIC.REAL });
        //            DicTagMoveSavePos.Add(No, new Tag { Name = strMove + $"[{No}]", DataType = Logix.Tag.ATOMIC.BOOL });
        //            DicTagSaveCur.Add(No, new Tag { Name = strSaveCurrent + $"[{No}]", DataType = Logix.Tag.ATOMIC.BOOL });
        //            DicTagSaveCus.Add(No, new Tag { Name = strSaveCustom + $"[{No}]", DataType = Logix.Tag.ATOMIC.BOOL });
        //            n++;
        //        }
        //        DTSP = dt;
        //        DgSavePosition.ItemsSource = DTSP.AsDataView();
        //    }
        //    catch (Exception ex)
        //    {
        //        DO(false, "SetSavePosition: " + ex.Message);
        //    }
        //}

        private void UpdateDatagrid()
        {
            foreach (DataRow dr in DTSP.Rows)
            {
                var item = DicTagSavePos[Convert.ToInt32(dr["No"])];
                if (PLCController.ReadTag(item) == ResultCode.E_SUCCESS)
                    dr["Position"] = Math.Round(Convert.ToSingle(item.Value) / Proportion, 1);
            }

            DgSavePosition.ItemsSource = null;
            DgSavePosition.ItemsSource = DTSP.AsDataView();
        }

        public DataTable GetSavePosition()
        {
            try
            {
                if (DgSavePosition.ItemsSource == null)
                    return null;

                DataTable DT = new DataTable();
                DT = ((DataView)DgSavePosition.ItemsSource).ToTable();
                return DT;
            }
            catch (Exception ex)
            {
                DO(false, "GetSavePosition : " + ex.Message);
                return null;
            }
        }

        public void SetMotorIOList(ref DataTable AllIO)
        {
            try
            {
                DgEditIO.ItemsSource = AllIO.AsDataView();
            }
            catch (Exception ex)
            {
                DO(false, "SetMotorIOList : " + ex.Message);
            }
        }

        #endregion
        private List<bool> Ivalue;
        private List<bool> Ovalue;
        public UCMotorPage(List<bool> _Ivalue, List<bool> _Ovalue)
        {
            try
            {
                Ivalue = _Ivalue;
                Ovalue = _Ovalue;
                InitializeComponent();
                this.DataContext = this;
                UpdateUnit();
            }
            catch (Exception ex)
            {
                DO(false, "UCMotorPage : " + ex.Message);
            }
        }

        private void UpdateUnit()
        {
            string Distance = string.Empty;
            string Speed = string.Empty;
            if (ProjectType.HDD == Classes.GlobalFunctions.ProjectType)
            {
                Distance = "mm";
                Speed = "mm/s";
                isDouble = true;
            }

            unit_CurrentPos.Text = Distance;
            unit_CurrentSpd.Text = Speed;
            unit_JogSpd.Text = "%";
            unit_AbsPos.Text = Distance;
            unit_RelPos.Text = Distance;
            unit_SavePos.Text = Distance;
            unit_Teach.Text = Distance;
            unit_SaveCurPos.Text = Distance;
            unit_SaveCusPos.Text = Distance;
        }

        bool Homing = false;
        bool Moving = false;
        bool SavePosMoving = false;
        public void Update()
        {
            try
            {
                if (PLCController.ReadTag(Tag1CurrentPosition) == ResultCode.E_SUCCESS && Tag1CurrentPosition?.Value != null)
                    CurrentPosition = isDouble? Math.Round(Convert.ToDouble(Tag1CurrentPosition.Value), 1) : Convert.ToInt32(Tag1CurrentPosition.Value);

                if (PLCController.ReadTag(Tag2CurrentSpeed) == ResultCode.E_SUCCESS && Tag2CurrentSpeed?.Value != null)
                    CurrentSpeed = Convert.ToInt32(Tag2CurrentSpeed.Value);

                if (PLCController.ReadTag(Tag13ErrorID) == ResultCode.E_SUCCESS && Tag2CurrentSpeed?.Value != null)
                    ErrorID = Tag13ErrorID?.Value.ToString() ?? string.Empty;

                //PLCController.ReadTag(Tag12Homing) == ResultCode.E_SUCCESS && Tag12Homing?.Value != null &&
                if (Homing && PLCController.ReadTag(Tag3HomeDone) == ResultCode.E_SUCCESS && Tag3HomeDone?.Value != null)
                {
                    HomeDone = (bool)Tag3HomeDone.Value;
                    //HomingFromTag = (bool)Tag12Homing.Value;
                    if (HomeDone)
                    {
                        this.Dispatcher.Invoke(new Action(() => TbtnHoming.IsChecked = false));
                        this.Dispatcher.Invoke(new Action(() => TbtnHoming.Content = "Home Done"));
                        Homing = false;
                    }
                    //else if (!HomingFromTag)
                    //{
                    //    this.Dispatcher.Invoke(new Action(() => TbtnHoming.IsChecked = false));
                    //    Homing = false;
                    //}
                }

                if (Moving)
                {
                    if (PLCController.ReadTag(Tag7MoveBusy) == ResultCode.E_SUCCESS && Tag7MoveBusy?.Value != null)
                    {
                        MoveBusy = (bool)Tag7MoveBusy.Value;
                        if (!MoveBusy)
                        {
                            this.Dispatcher.Invoke(new Action(() => TBtnAbsMove.IsChecked = false));
                            this.Dispatcher.Invoke(new Action(() => TBtnRelMove.IsChecked = false));
                            this.Dispatcher.Invoke(new Action(() => TBtnSavePositionMove.IsChecked = false));
                            this.Dispatcher.Invoke(new Action(() => DgSavePosition.IsEnabled = true));
                            this.Dispatcher.Invoke(new Action(() => DgvCustomPos.IsEnabled = true));
                            this.Dispatcher.Invoke(new Action(() => SaveReset.IsEnabled = true));

                            Moving = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DO(false, "Update: " + ex.Message);
            }
        }

        private bool GetStatus()
        {
            try
            {
                if (SavePosMoving || Moving || Homing)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                DO(false, "GetStatus: " + ex.Message);
                return false;
            }
        }

        private string StatusMessage()
        {
            try
            {
                if (SavePosMoving || Moving)
                    return "Moving";
                else if (Homing)
                    return "Homing";
                else
                    return "Motor Busy";
            }
            catch (Exception ex)
            {
                DO(false, "StatusMessage: " + ex.Message);
                return "Failed to get Status";
            }
        }

        private bool DO(bool Success, string ErrorMessage)
        {
            try
            {
                if (!Success)
                {
                    Utilities.FileLogger.logError(ErrorMessage, "Error");
                    MessageBox.Show(ErrorMessage, "Error");
                }
                return Success;
            }
            catch (Exception ex)
            {
                DO(false, "DO: " + ex.Message);
                return false;
            }
        }

        private bool Confirmation(string Message)
        {
            if (Frm.DialogResult.OK == Frm.MessageBox.Show(Message, "Info", Frm.MessageBoxButtons.OKCancel))
                return true;

            return false;
        }

        #region Event
        private void TBtnAbsMove_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton Tbtn = sender as ToggleButton;
                if (Confirmation("Trigger Move to Absolute Position: " + TBAbsPos.Text))
                {
                    Utilities.FileLogger.logButton(strMotorName, "Move Absolute - " + TBAbsPos.Text, MethodBase.GetCurrentMethod().ToString());
                    if (DO(GetStatus(), StatusMessage()))
                        if (isDouble) MoveAbsDouble(); else MoveAbsInt();
                }

                Tbtn.IsChecked = false;
            }
            catch (Exception ex)
            {
                DO(false, "TBtnAbsMove_Checked: " + ex.Message);
            }
        }

        private void MoveAbsInt()
        {
            int AbsPos;
            if (DO(int.TryParse(TBAbsPos.Text, out AbsPos),
                "TBtnAbsMove_Checked: Invalid Position: " + TBAbsPos.Text))
            {
                if (DO((AbsPos <= PosMax && AbsPos >= NegMax),
                    "TBtnAbsMove_Checked: Absolute Position should be within " + NegMax + " and " + PosMax))
                {
                    Tag8AbsPosition.Value = AbsPos;
                    if (DO(PLCController.WriteTag(Tag8AbsPosition) == ResultCode.E_SUCCESS,
                        "TBtnAbsMove_Checked: Write Fail Tag8AbsPosition"))
                    {
                        Tag9MoveAbs.Value = true;
                        if (DO(PLCController.WriteTag(Tag9MoveAbs) == ResultCode.E_SUCCESS,
                            "TBtnAbsMove_Checked: Write Fail Tag9MoveAbs"))
                        {
                            this.Dispatcher.Invoke(new Action(() => DgSavePosition.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => DgvCustomPos.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => SaveReset.IsEnabled = false));
                            Moving = true;
                            System.Threading.Thread.Sleep(400);
                            return;
                        }
                    }
                }
            }

        }

        private void MoveAbsDouble()
        {
            double AbsPos;
            if (DO(double.TryParse(TBAbsPos.Text, out AbsPos),
                "TBtnAbsMove_Checked: Invalid Position: " + TBAbsPos.Text))
            {
                if (DO((AbsPos <= PosMax && AbsPos >= NegMax),
                    "TBtnAbsMove_Checked: Absolute Position should be within " + NegMax + " and " + PosMax))
                {
                    Tag8AbsPosition.Value = AbsPos;
                    if (DO(PLCController.WriteTag(Tag8AbsPosition) == ResultCode.E_SUCCESS,
                        "TBtnAbsMove_Checked: Write Fail Tag8AbsPosition"))
                    {
                        Tag9MoveAbs.Value = true;
                        if (DO(PLCController.WriteTag(Tag9MoveAbs) == ResultCode.E_SUCCESS,
                            "TBtnAbsMove_Checked: Write Fail Tag9MoveAbs"))
                        {
                            this.Dispatcher.Invoke(new Action(() => DgSavePosition.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => DgvCustomPos.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => SaveReset.IsEnabled = false));
                            Moving = true;
                            System.Threading.Thread.Sleep(400);
                            return;
                        }
                    }
                }
            }
        }



        private void TBtnRelMove_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton Tbtn = sender as ToggleButton;
                if (Confirmation("Trigger Move to Relative Position: " + TBRelPos.Text))
                {
                    Utilities.FileLogger.logButton(strMotorName, "Move Relative - " + TBRelPos.Text, MethodBase.GetCurrentMethod().ToString());
                    if (DO(GetStatus(), StatusMessage()))
                        if (isDouble) MoveRelDouble(); else MoveRelInt();
                }
                Tbtn.IsChecked = false;
            }
            catch (Exception ex)
            {
                DO(false, "TBtnRelMove_Checked: " + ex.Message);
            }
        }

        private void MoveRelInt()
        {
            int RelPos;
            if (DO(int.TryParse(TBRelPos.Text, out RelPos),
                "TBtnRelMove_Checked: Invalid Position: " + TBRelPos.Text))
            {
                if (DO(CurrentPosition + RelPos <= PosMax && CurrentPosition + RelPos >= NegMax,
                    "TBtnRelMove_Checked: Relative Position should be within " + (NegMax - CurrentPosition) + "and" + (PosMax - CurrentPosition)))
                {
                    Tag10RelPosition.Value = RelPos;
                    if (DO(PLCController.WriteTag(Tag10RelPosition) == ResultCode.E_SUCCESS,
                        "TBtnRelMove_Checked: Write Fail Tag10RelPosition"))
                    {
                        Tag11MoveRel.Value = true;
                        if (DO(PLCController.WriteTag(Tag11MoveRel) == ResultCode.E_SUCCESS,
                            "TBtnRelMove_Checked: Write Fail Tag11MoveRel"))
                        {
                            this.Dispatcher.Invoke(new Action(() => DgSavePosition.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => DgvCustomPos.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => SaveReset.IsEnabled = false));
                            Moving = true;
                            System.Threading.Thread.Sleep(400);
                            return;
                        }
                    }
                }
            }
        }

        private void MoveRelDouble()
        {
            double RelPos;
            if (DO(double.TryParse(TBRelPos.Text, out RelPos),
                "TBtnRelMove_Checked: Invalid Position: " + TBRelPos.Text))
            {
                if (DO(CurrentPosition + RelPos <= PosMax && CurrentPosition + RelPos >= NegMax,
                    "TBtnRelMove_Checked: Relative Position should be within " + (NegMax - CurrentPosition) + "and" + (PosMax - CurrentPosition)))
                {
                    Tag10RelPosition.Value = RelPos;
                    if (DO(PLCController.WriteTag(Tag10RelPosition) == ResultCode.E_SUCCESS,
                        "TBtnRelMove_Checked: Write Fail Tag10RelPosition"))
                    {
                        Tag11MoveRel.Value = true;
                        if (DO(PLCController.WriteTag(Tag11MoveRel) == ResultCode.E_SUCCESS,
                            "TBtnRelMove_Checked: Write Fail Tag11MoveRel"))
                        {
                            this.Dispatcher.Invoke(new Action(() => DgSavePosition.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => DgvCustomPos.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => SaveReset.IsEnabled = false));
                            Moving = true;
                            System.Threading.Thread.Sleep(400);
                            return;
                        }
                    }
                }
            }
        }

        private void TbtnHoming_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton Tbtn = sender as ToggleButton;
                if (Confirmation("Trigger Homing"))
                {
                    Utilities.FileLogger.logButton(strMotorName, "Homing", MethodBase.GetCurrentMethod().ToString());
                    if (DO(GetStatus(), StatusMessage()))
                    {
                        Tag4StartHome.Value = true;
                        if (DO(PLCController.WriteTag(Tag4StartHome) == ResultCode.E_SUCCESS,
                            "TbtnHoming_Checked: Write Fail Tag4StartHome"))
                        {
                            Homing = true;
                            System.Threading.Thread.Sleep(400);
                            return;
                        }
                    }
                }
                Tbtn.IsChecked = false;
            }
            catch (Exception ex)
            {
                DO(false, "TbtnHoming_Checked: " + ex.Message);
            }
        }

        private void SavePositionMove_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ToggleButton Tbtn = sender as ToggleButton;
                if (Confirmation("Trigger Move to Saved Position: " + DgvCustomPos.Text))
                {
                    Utilities.FileLogger.logButton(strMotorName, "Move to Save Position - " + DgvCustomPos.Text, MethodBase.GetCurrentMethod().ToString());
                    if (DO(GetStatus(), StatusMessage()))
                        if (isDouble) SavePosMoveDouble(); else SavePosMoveInt();
                }
                Tbtn.IsChecked = false;
            }
            catch (Exception ex)
            {
                DO(false, "SavePositionMove_Checked: " + ex.Message);
            }
        }

        private void SavePosMoveInt()
        {
            int Pos;
            if (DO(int.TryParse(DgvCustomPos.Text, out Pos),
                "SavePositionMove_Checked: Invalid Position: " + DgvCustomPos.Text))
            {
                if (DO((Pos <= PosMax && Pos >= NegMax),
                    "SavePositionMove_Checked: Position should be within " + NegMax + " and " + PosMax))
                {
                    Tag8AbsPosition.Value = Pos;
                    if (DO(PLCController.WriteTag(Tag8AbsPosition) == ResultCode.E_SUCCESS,
                        "SavePositionMove_Checked: Write Fail Tag8AbsPosition"))
                    {
                        Tag9MoveAbs.Value = true;
                        if (DO(PLCController.WriteTag(Tag9MoveAbs) == ResultCode.E_SUCCESS,
                            "SavePositionMove_Checked: Write Fail Tag9MoveAbs"))
                        {
                            this.Dispatcher.Invoke(new Action(() => DgSavePosition.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => DgvCustomPos.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => SaveReset.IsEnabled = false));
                            Moving = true;
                            System.Threading.Thread.Sleep(400);
                            return;
                        }
                    }
                }
            }
        }
        private void SavePosMoveDouble()
        {
            double Pos;
            if (DO(double.TryParse(DgvCustomPos.Text, out Pos),
                "SavePositionMove_Checked: Invalid Position: " + DgvCustomPos.Text))
            {
                if (DO((Pos <= PosMax && Pos >= NegMax),
                    "SavePositionMove_Checked: Position should be within " + NegMax + " and " + PosMax))
                {
                    Tag8AbsPosition.Value = Pos;
                    if (DO(PLCController.WriteTag(Tag8AbsPosition) == ResultCode.E_SUCCESS,
                        "SavePositionMove_Checked: Write Fail Tag8AbsPosition"))
                    {
                        Tag9MoveAbs.Value = true;
                        if (DO(PLCController.WriteTag(Tag9MoveAbs) == ResultCode.E_SUCCESS,
                            "SavePositionMove_Checked: Write Fail Tag9MoveAbs"))
                        {
                            this.Dispatcher.Invoke(new Action(() => DgSavePosition.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => DgvCustomPos.IsEnabled = false));
                            this.Dispatcher.Invoke(new Action(() => SaveReset.IsEnabled = false));
                            Moving = true;
                            System.Threading.Thread.Sleep(400);
                            return;
                        }
                    }
                }
            }
        }

        bool Guide = true;
        private void TBtnJogMode_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Guide)
                {
                    if (Confirmation("Trigger Jog Mode to " + ((bool)TBtnJogMode.IsChecked ? "ON;\nSpeed Set to " + tbJogSpd.Text + "%" : "OFF")))
                    {
                        Utilities.FileLogger.logButton(strMotorName,
                            "Jog Mode Trigger" + TBtnJogMode.IsChecked.ToString() + tbJogSpd.Text, MethodBase.GetCurrentMethod().ToString());
                        int JogSpeed;
                        if (int.TryParse(tbJogSpd.Text, out JogSpeed))
                        {
                            if (DO(JogSpeed <= 100 && JogSpeed >= 1,
                                "TBtnJogMode_Checked: JogSpeed(" + JogSpeed + ") Should between 1% to 100%"))
                            {
                                double Speed = ((SpeedMax - SpeedMin) * JogSpeed / 100) + SpeedMin;
                                unit_desp_spd.Text = $"Actual Speed: {Speed} mm/s";
                                Tag5JogSpeed.Value = Speed;
                                if (!DO(PLCController.WriteTag(Tag5JogSpeed) == ResultCode.E_SUCCESS,
                                    "TBtnJogMode_Checked: Write Speed Failed Tag5JogSpeed"))
                                    goto EndThis;
                            }
                            else
                                goto EndThis;
                        }
                        else
                        {
                            if (!DO(!(bool)TBtnJogMode.IsChecked,
                                "TBtnJogMode_Checked: Invalid Speed Margin Inserted:" + tbJogSpd.Text))
                                goto EndThis;
                        }

                        //if (int.TryParse(tbJogSpd.Text, out JogSpeed))
                        //{
                        //    if (DO(JogSpeed <= SpeedMax && JogSpeed >= 1,
                        //        "TBtnJogMode_Checked: JogSpeed(" + JogSpeed + ") Should between 1 to " + SpeedMax))
                        //    {
                        //        Tag5JogSpeed.Value = JogSpeed;
                        //        if (!DO(PLCController.WriteTag(Tag5JogSpeed) == ResultCode.E_SUCCESS,
                        //            "TBtnJogMode_Checked: Write Speed Failed Tag5JogSpeed"))
                        //            goto EndThis;
                        //    }
                        //    else
                        //        goto EndThis;
                        //}
                        //else
                        //{
                        //    if (!DO(!(bool)TBtnJogMode.IsChecked,
                        //        "TBtnJogMode_Checked: Invalid Speed Inserted:" + tbJogSpd.Text))
                        //        goto EndThis;
                        //}

                        Tag6JogMode.Value = TBtnJogMode.IsChecked;
                        if (DO(PLCController.WriteTag(Tag6JogMode) == ResultCode.E_SUCCESS,
                            "TBtnJogMode_Checked: Write Fail Tag6JogMode"))
                        {
                            JogInfo.Visibility = (bool)TBtnJogMode.IsChecked ? Visibility.Visible : Visibility.Collapsed;
                            tbJogSpd.IsEnabled = JogInfo.Visibility.Equals(Visibility.Collapsed);
                            return;
                        }
                    }
                }

            EndThis:
                if (Guide)
                {
                    Guide = false;
                    TBtnJogMode.IsChecked = (bool)TBtnJogMode.IsChecked ? false : true;
                }
                else
                    Guide = true;
            }
            catch (Exception ex)
            {
                DO(false, "TBtnJogMode_Checked: " + ex.Message);
            }
        }

        private void SaveCus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Confirmation("Save Custom Position: " + DgvCustomPos1.Text))
                {
                    Utilities.FileLogger.logButton(strMotorName, "Save Custom Position" + DgvCustomPos1.Text + " at Pos:" + intCurrentPos, MethodBase.GetCurrentMethod().ToString());
                    if (DO(GetStatus(), StatusMessage()))
                    {
                        int Pos;
                        if (DO(int.TryParse(DgvCustomPos.Text, out Pos),
                            "SaveCus_Click: Invalid Position" + DgvCustomPos.Text))
                        {
                            TagSharedPosition.Value = Math.Round(Pos * Proportion, 0);
                            if (DO(PLCController.WriteTag(TagSharedPosition) == ResultCode.E_SUCCESS,
                                "SaveCus_Click: Write Fail TagSharedPosition"))
                            {
                                DicTagSaveCus[intCurrentPos].Value = true;
                                if (DO(PLCController.WriteTag(DicTagSaveCus[intCurrentPos]) == ResultCode.E_SUCCESS,
                                "SaveCus_Click: Write Fail DicTagSaveCus[intCurrentPos]"))
                                {
                                    CurrentPointButton_Click(null, e);
                                    UpdateDatagrid();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DO(false, "SaveCus_Click: " + ex.Message);
            }
        }

        private void SaveCur_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Confirmation("Save Current Position: " + CurrentPosition))
                {
                    ToggleButton Tbtn = sender as ToggleButton;
                    Utilities.FileLogger.logButton(strMotorName, "Save Current Position :" + DicTagSaveCur[intCurrentPos].Name, MethodBase.GetCurrentMethod().ToString());
                    if (DO(GetStatus(), StatusMessage()))
                    {
                        DicTagSaveCur[intCurrentPos].Value = true;
                        if (DO(PLCController.WriteTag(DicTagSaveCur[intCurrentPos]) == ResultCode.E_SUCCESS,
                            "SaveCur_Click: Write Fail DicTagSaveCur[intCurrentPos]"))
                        {
                            CurrentPointButton_Click(null, e);
                            UpdateDatagrid();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DO(false, "SaveCur_Click: " + ex.Message);
            }
        }

        private void CurrentPointButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utilities.FileLogger.logButton(strMotorName, "Export Current Position", MethodBase.GetCurrentMethod().ToString());

                if (ExportCurrentPoint != null)
                    ExportCurrentPoint(this, e);
            }
            catch (Exception ex)
            {
                DO(false, "CurrentPointButton_Click: " + ex.Message);
            }
        }

        private void LocationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Utilities.FileLogger.logButton(strMotorName, "Open File Location", MethodBase.GetCurrentMethod().ToString());

                if (OpenFileLocation != null)
                    OpenFileLocation(this, e);
            }
            catch (Exception ex)
            {
                DO(false, "LocationButton_Click: " + ex.Message);
            }
        }

        private void TbtnEdit_Unchecked(object sender, RoutedEventArgs e)
        {

            if (IOUpdate != null)
                IOUpdate(((DataView)DgEditIO.ItemsSource).ToTable().Select("Check = 1"), e);

            DgEditIO.Visibility = Visibility.Collapsed;
        }

        private void DgSavePosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRowView dr = ((DataGrid)sender).SelectedItem as DataRowView;

                if (dr == null)
                {
                    spCurrentPoint.Visibility = Visibility.Collapsed;
                }
                else
                {
                    intCurrentPos = (int)dr["No"];
                    DgvPositionName.Content = dr["Name"];
                    DgvCustomPos.Text = dr["Position"].ToString();
                    DgvCrntSvPos.Text = dr["Position"].ToString();
                    Utilities.FileLogger.logButton(strMotorName, "Selected Save Position :" + DgvPositionName.Content, MethodBase.GetCurrentMethod().ToString());
                    if (spCurrentPoint.Visibility == Visibility.Collapsed)
                        spCurrentPoint.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                DO(false, "DgSavePosition_SelectionChanged: " + ex.Message);
            }
        }

        Tag Temp = new Tag { DataType = Logix.Tag.ATOMIC.BOOL };
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox uc = sender as CheckBox;
                Temp.Name = uc.Tag.ToString(); Temp.Value = uc.IsChecked;
                DO(PLCController.WriteTag(Temp) == ResultCode.E_SUCCESS,
                                $"UCCheckboxLabelType2_ucMouseClick: Write Fail {Temp.Name}");
            }
            catch (Exception ex)
            {
                DO(false, "UCCheckboxLabelType2_ucMouseClick: " + ex.Message);
            }
        }
        #endregion

        private void SaveReset_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strMotorName, "Edit Custom Save Position Reset", MethodBase.GetCurrentMethod().ToString());
            DgvCustomPos.Text = DgvCrntSvPos.Text;
        }

        private void TbtnEdit_Checked(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strMotorName, "Edit IO", MethodBase.GetCurrentMethod().ToString());
            DgEditIO.Visibility = Visibility.Visible;
        }
    }
}
