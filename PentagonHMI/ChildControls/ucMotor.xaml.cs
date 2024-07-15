using Logix;
using PentagonHMI.Classes;
using PentagonHMI.UserControls;
using SimpleDatabase;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Utilities;

namespace PentagonHMI.ChildControls
{
    public partial class ucMotor : UserControl, IDisposable
    {
        #region Fields

        private Dictionary<int, UCMotorPage> MotorPages = new Dictionary<int, UCMotorPage>();
        private SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);
        private LogicClasses.Main _MainConnection;
        private string ErrMsg = string.Empty;
        private string strMotor => "MotorPage";
        private UCMotorPage PageNow;
        private string MotorSaveFileName = "MotorAxisRecord_";
        private Controller PLCController = new Controller();
        private Tag EM; private Tag MStatus;

        #endregion Fields

        #region Constructor

        public ucMotor(ref LogicClasses.Main MainConnection)
        {
            try
            {
                InitializeComponent();
                _MainConnection = MainConnection;
                _MainConnection.OnMotorUpdate += new LogicClasses.Main.onMotorHandler(Motor_OnUpdate);
                Initialize();
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion Constructor

        private void Initialize()

        {
            PLCController = _MainConnection.MyPLC;
            loadMotorAxisDetails();

            //EM = new Tag("MC_System_Tags.EngineeringMode");//_MainConnection.StationID == "0" ? new Tag("bool_EngineerMode") : new Tag("bool_EngineeringMode");
            EM = new Tag(Tags.MainPage.EngineeringMode.Name, Tags.MainPage.EngineeringMode.DataType);
            if (GlobalFunctions.IsSystem1)
            {
                MStatus = new Tag("System1_MC_Tag.MachineRunning", Logix.Tag.ATOMIC.BOOL);// _MainConnection.StationID == "0" ? new Tag("Conveyor_Preset.Production_Running") : new Tag("bool_MachineRunning"); 
            }
            else
            {
                MStatus = new Tag("System2_MC_Tag.MachineRunning", Logix.Tag.ATOMIC.BOOL);
            }
        }

        private class Assignment : SavePosition
        {
            public Controller PLCctl { get; set; } = new Controller();
            public string CurrentPosition { get; set; } = string.Empty;
            public string CurrentSpeed { get; set; } = string.Empty;
            public string HomeDone { get; set; } = string.Empty;
            public string StartHome { get; set; } = string.Empty;
            public string Homing { get; set; } = string.Empty;
            public string JogSpeed { get; set; } = string.Empty;
            public string JogMode { get; set; } = string.Empty;
            public string MoveBusy { get; set; } = string.Empty;
            public string AbsPosition { get; set; } = string.Empty;
            public string MoveAbs { get; set; } = string.Empty;
            public string RelPosition { get; set; } = string.Empty;
            public string MoveRel { get; set; } = string.Empty;
            public string ErrorID { get; set; } = string.Empty;
            public int PosMax { get; set; } = 100000;
            public int NegMax { get; set; } = -100000;
            public int SpeedMax { get; set; } = 100;
            public int SpeedMin { get; set; } = 1;
            public int Index { get; set; } = 0;
            public string Header { get; set; } = string.Empty;
            public int Axis { get; set; } = 0;
            public double Proportion { get; set; } = 0;

            public DataTable MotorIOList = new DataTable();
            public ObservableCollection<UCMotorPage.IO> ICollection { get; set; } = new ObservableCollection<UCMotorPage.IO>();
            public ObservableCollection<UCMotorPage.IO> OCollection { get; set; } = new ObservableCollection<UCMotorPage.IO>();
        }

        public class SavePosition
        {
            public DataTable No_Name_Position = new DataTable();

            public DataTable dt_ALL_SPos_Info = new DataTable();
            public string SharedPosition { get; set; } = string.Empty;
            public string Position { get; set; } = string.Empty;
            public string Move { get; set; } = string.Empty;
            public string SaveCurrent { get; set; } = string.Empty;
            public string SaveCustom { get; set; } = string.Empty;
        }

        private void NewAxis(Assignment Asm)
        {
            UCMotorPage Axis = new UCMotorPage(_MainConnection.IValue, _MainConnection.OValue);
            Axis.PLCController = Asm.PLCctl;
            Axis.Tag1CurrentPosition = new Tag { Name = Asm.CurrentPosition, DataType = Logix.Tag.ATOMIC.REAL };
            Axis.Tag2CurrentSpeed = new Tag { Name = Asm.CurrentSpeed, DataType = Logix.Tag.ATOMIC.REAL };
            Axis.Tag3HomeDone = new Tag { Name = Asm.HomeDone, DataType = Logix.Tag.ATOMIC.BOOL };
            Axis.Tag4StartHome = new Tag { Name = Asm.StartHome, DataType = Logix.Tag.ATOMIC.BOOL };
            Axis.Tag5JogSpeed = new Tag { Name = Asm.JogSpeed, DataType = Logix.Tag.ATOMIC.REAL };
            Axis.Tag6JogMode = new Tag { Name = Asm.JogMode, DataType = Logix.Tag.ATOMIC.BOOL };
            Axis.Tag7MoveBusy = new Tag { Name = Asm.MoveBusy, DataType = Logix.Tag.ATOMIC.BOOL };
            Axis.Tag8AbsPosition = new Tag { Name = Asm.AbsPosition, DataType = Logix.Tag.ATOMIC.REAL };
            Axis.Tag9MoveAbs = new Logix.Tag { Name = Asm.MoveAbs, DataType = Logix.Tag.ATOMIC.BOOL };
            Axis.Tag10RelPosition = new Tag { Name = Asm.RelPosition, DataType = Logix.Tag.ATOMIC.REAL };
            Axis.Tag11MoveRel = new Tag { Name = Asm.MoveRel, DataType = Logix.Tag.ATOMIC.BOOL };
            Axis.Tag12Homing = new Tag { Name = Asm.Homing, DataType = Logix.Tag.ATOMIC.BOOL };
            Axis.Tag13ErrorID = new Tag { Name = Asm.ErrorID, DataType = Logix.Tag.ATOMIC.SINT };
            Axis.PosMax = Asm.PosMax;
            Axis.NegMax = Asm.NegMax;
            Axis.SpeedMax = Asm.SpeedMax;
            Axis.SpeedMin = Asm.SpeedMin;
            Axis.Proportion = Asm.Proportion;
            Axis.AxisName = Asm.Header;
            Axis.ICollection = Asm.ICollection;
            Axis.OCollection = Asm.OCollection;
            Axis.Axis = Asm.Axis;
            Axis.SetSavePosition(ref Asm.No_Name_Position, ref Asm.dt_ALL_SPos_Info);
            Axis.SetMotorIOList(ref Asm.MotorIOList);
            Axis.ExportCurrentPoint += new EventHandler(Axis_Save);
            Axis.OpenFileLocation += new EventHandler(OpenFileLocation);
            Axis.IOUpdate += new EventHandler(IOUpdate);
            Axis.strMotorName = Asm.Header;
            MotorTab.Items.Add(new TabItem { Content = Axis, Header = Asm.Header });
            MotorPages.Add(Asm.Index, Axis);
        }

        #region NewMotor

        private void EM_MouseClick(object sender, EventArgs e)
        {
            try
            {
                Utilities.FileLogger.logButton(strMotor, "Engineering Mode Trigger", MethodBase.GetCurrentMethod().ToString());
                ToggleButton Tbtn = sender as ToggleButton;
                EM.Value = (Boolean)Tbtn.IsChecked;
                if(PLCController.WriteTag(EM) == ResultCode.E_SUCCESS)
                    Tbtn.IsChecked = (bool)EM.Value;
                else
                    Tbtn.IsChecked = (bool)Tbtn.IsChecked ? false : true;
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Trigger Engineering Mode Failed IO Page");
            }
        }

        #endregion NewMotor

        #region Events

        private void Motor_OnUpdate()
        {
            try
            {
                Update();

                if(PLCController.ReadTag(MStatus) == ResultCode.E_SUCCESS && MStatus.Value != null)
                    this.Dispatcher.Invoke(new Action(() => tgEM.IsEnabled = !(bool)MStatus.Value));

                if(PLCController.ReadTag(EM) == ResultCode.E_SUCCESS && EM.Value != null)
                    this.Dispatcher.Invoke(new Action(() => EMchk((bool)EM.Value)));
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void Update()
        {
            int index = -1;
            Dispatcher.Invoke(() => index = MotorTab.SelectedIndex);
            if(MotorPages.TryGetValue(index, out PageNow))
            {
                PageNow.Update();
            }
        }

        #endregion Events

        #region Methods

        private void EMchk(bool EMon)
        {
            try
            {
                foreach(TabItem tb in MotorTab.Items)
                    tb.IsEnabled = EMon;

                if(!EMon)
                    MotorTab.SelectedIndex = 0;

                tgEM.IsChecked = EMon;
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion Methods

        public void Dispose()
        {
            _MainConnection.MotorPageON = false;
        }

        #region Destructor

        ~ucMotor()
        {
            Dispose();
        }

        #endregion Destructor

        private void loadMotorAxisDetails()
        {
            try
            {
                int row = 0;
                DataTable DT = SQLer.Exec_DTSelect($"Select * from Motor_Info Where StationID = '{_MainConnection.StationID}'");
                if(DT != null)
                    foreach(DataRow dr in DT.Rows)
                    {
                        int _Axis = (int)dr["MotorAxis"];
                        List<ObservableCollection<UCMotorPage.IO>> CurrentIO = GetCurrentIO(_MainConnection.StationID, _Axis, ref ErrMsg);
                        row++;
                        Assignment ASM = new Assignment
                        {
                            AbsPosition = dr["AbsPosition"].ToString().Trim(),
                            CurrentPosition = dr["CurrentPosition"].ToString().Trim(),
                            CurrentSpeed = dr["CurrentSpeed"].ToString().Trim(),
                            Header = dr["MotorName"].ToString().Trim(),
                            HomeDone = dr["HomeDone"].ToString().Trim(),
                            JogMode = dr["JogMode"].ToString().Trim(),
                            JogSpeed = dr["JogSpeed"].ToString().Trim(),
                            MoveAbs = dr["MoveAbs"].ToString().Trim(),
                            MoveBusy = dr["MoveBusy"].ToString().Trim(),
                            MoveRel = dr["MoveRel"].ToString().Trim(),
                            RelPosition = dr["RelPosition"].ToString().Trim(),
                            StartHome = dr["StartHome"].ToString().Trim(),
                            Homing = dr["Homing"].ToString().Trim(),
                            ErrorID = dr["ErrorID"].ToString().Trim(),

                            Proportion = string.IsNullOrWhiteSpace(dr["Proportion"].ToString().Trim()) ? 1 : Convert.ToDouble(dr["Proportion"].ToString()),
                            NegMax = string.IsNullOrWhiteSpace(dr["NegMax"].ToString().Trim()) ? -1000000 : (int)dr["NegMax"],
                            PosMax = string.IsNullOrWhiteSpace(dr["PosMax"].ToString().Trim()) ? 1000000 : (int)dr["PosMax"],
                            SpeedMax = string.IsNullOrWhiteSpace(dr["SpeedMax"].ToString().Trim()) ? 100 : (int)dr["SpeedMax"],
                            SpeedMin = string.IsNullOrWhiteSpace(dr["SpeedMin"].ToString().Trim()) ? 1 : (int)dr["SpeedMin"],
                            Index = row,
                            Axis = _Axis,
                            PLCctl = PLCController,

                            MotorIOList = dtALLIO(_MainConnection.StationID, _Axis, ref ErrMsg),
                            ICollection = CurrentIO[0],
                            OCollection = CurrentIO[1],

                            No_Name_Position = dtSavePosition(_MainConnection.StationID, _Axis, ref ErrMsg),
                            dt_ALL_SPos_Info = dtSavePositionInfo(_MainConnection.StationID, _Axis, ref ErrMsg),
                        };
                        NewAxis(ASM);
                    }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private DataTable dtSavePosition(string StationID, int MotorAxis, ref string ErrMsg)
        {
            return SQLer.Exec_DTSelect(
            "Select [PosIndex] as 'No', [PositionName] as 'Name', '-Not Available-' as 'Position' from Motor_SavePosition " +
            $"Where StationID = {StationID} and [AxisNumber] = {MotorAxis} ");
        }

        private DataTable dtSavePositionInfo(string StationID, int MotorAxis, ref string ErrMsg)
        {
            return SQLer.Exec_DTSelect(
            "Select [PosIndex] as 'No', * from Motor_SavePosition " +
            $"Where StationID = {StationID} and [AxisNumber] = {MotorAxis} ");
        }

        private DataTable dtALLIO(string StationID, int MotorAxis, ref string ErrMsg)
        {
            DataTable dt = SQLer.Exec_DTSelect(
            "Select [DisplayName],[IO], [TagName]," +
            $"(Select Count(*) from Motor_IO b where b.StationID = {StationID} and MotorAxis = {MotorAxis} " +
            $"and TagName = a.TagName) as 'Check' From IO a Where a.StationID = {StationID} ");
            foreach(DataColumn col in dt.Columns)
                col.ReadOnly = false;
            return dt;
        }

        private List<ObservableCollection<UCMotorPage.IO>> GetCurrentIO(string StationID, int MotorAxis, ref string ErrMsg)
        {
            List<ObservableCollection<UCMotorPage.IO>> CurrentIO = new List<ObservableCollection<UCMotorPage.IO>>();
            try
            {
                DataTable DT = SQLer.Exec_DTSelect(
                "Select a.TagName,[DisplayName],[TagIndex],[MotorAxis],[IO],[OutputTagName] from IO a inner join Motor_IO b on a.TagName = b.TagName " +
                $"where a.stationID = {StationID} and b.stationID = {StationID} and [MotorAxis] = {MotorAxis}");
                ObservableCollection<UCMotorPage.IO> CurrentI = new ObservableCollection<UCMotorPage.IO>();
                ObservableCollection<UCMotorPage.IO> CurrentO = new ObservableCollection<UCMotorPage.IO>();
                if(DT != null)
                    foreach(DataRow dr in DT.Rows)
                    {
                        if(dr["IO"].ToString() == "I")
                            CurrentI.Add(new UCMotorPage.IO
                            {
                                Content = dr["DisplayName"].ToString(),
                                Index = Convert.ToInt32(dr["TagIndex"])
                            });
                        else
                            CurrentO.Add(new UCMotorPage.IO
                            {
                                Content = dr["DisplayName"].ToString(),
                                Index = Convert.ToInt32(dr["TagIndex"]),
                                OutputTagName = dr["OutputTagName"].ToString()
                            });
                    }
                CurrentIO.Add(CurrentI);
                CurrentIO.Add(CurrentO);
                return CurrentIO;
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return CurrentIO;
            }
        }

        private void Axis_Save(object sender, EventArgs e)
        {
            try
            {
                if(MotorPages.TryGetValue(MotorTab.SelectedIndex, out PageNow))
                {
                    string Spliter = ",";
                    string pathCSV = Path.Combine(FileLogger.DefaultLocation_Time, "Motor");
                    string pathCSVMotorSave = pathCSV + Path.DirectorySeparatorChar + MotorSaveFileName + DateTime.Now.ToString("yyyy-MMM-dd_HHmm") + ".txt";
                    string Header = PageNow.AxisName + Spliter;
                    string Body = "Value,";
                    DataTable DT = PageNow.GetSavePosition();
                    if(DT == null)
                    {
                        MessageBox.Show("No Existing Save Position");
                        return;
                    }

                    foreach(DataRow dr in DT.Rows)
                    {
                        Header += dr["Name"] + Spliter;
                        Body += dr["Position"] + Spliter;
                    }

                    if(!Directory.Exists(pathCSV))
                        Directory.CreateDirectory(pathCSV);

                    if(!File.Exists(pathCSVMotorSave))
                    {
                        using(FileStream stream = new FileStream((pathCSVMotorSave), FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                        using(StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Header);
                            writer.WriteLine(Body);
                        }
                    }
                    else
                    {
                        using(FileStream stream = new FileStream((pathCSVMotorSave), FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                        using(StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.WriteLine(Header);
                            writer.WriteLine(Body);
                        }
                    }

                    MessageBox.Show("Saved");
                }
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void OpenFileLocation(object sender, EventArgs e)
        {
            try
            {
                string strPath = Path.Combine(FileLogger.DefaultLocation_Time, "Motor");

                if(!Directory.Exists(@strPath))
                    Directory.CreateDirectory(strPath);

                Process.Start(@strPath);
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void IOUpdate(object sender, EventArgs e)
        {
            try
            {
                DataRow[] DTr = sender as DataRow[];
                SQLer.Exec_DTSelect($"Delete From Motor_IO Where StationID = {_MainConnection.StationID} and MotorAxis = {PageNow.Axis}");
                string Values = string.Empty;
                foreach(DataRow dr in DTr)
                    Values += $"({_MainConnection.StationID},{PageNow.Axis},'{dr["TagName"]}',GetDate()),";

                if(DTr.GetLength(0) > 0)
                    SQLer.Exec_DTSelect($"INSERT INTO [dbo].[Motor_IO]([StationID],[MotorAxis],[TagName],[Updated_On])VALUES"
                        + Values.Remove(Values.Length - 1));

                List<ObservableCollection<UCMotorPage.IO>> CurIO = GetCurrentIO(_MainConnection.StationID, PageNow.Axis, ref ErrMsg);
                PageNow.ICollection = CurIO[0];
                PageNow.OCollection = CurIO[1];
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            }
        }
    }
}