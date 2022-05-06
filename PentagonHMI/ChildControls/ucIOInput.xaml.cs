using System;
using System.Windows.Controls;
using PentagonHMI.UserControls;
using Logix;
using System.Windows.Controls.Primitives;
using System.Collections.Generic;
using PentagonHMI.LogicClasses;
using System.Data;
using System.Windows;
using VanillaDB;
using System.Reflection;

namespace PentagonHMI.ChildControls
{
    public partial class ucIOInput : UserControl, IDisposable
    {
        #region Constructor
        LogicClasses.Main _Main;
        string ErrMsg;
        string strIO = "IOPage";
        private Database DB = new Database(Properties.Settings.Default.DatabaseConnectionString.ToString());
        DataSet ds = new DataSet();
        Tag ModeTag = new Tag
        {
            Name = "MC_System_Tags.MachineRunning",
            DataType = Logix.Tag.ATOMIC.BOOL
        };

        Tag EMTag = new Tag
        {
            Name = "MC_System_Tags.EngineeringMode",
            DataType = Logix.Tag.ATOMIC.BOOL
        };

        Dictionary<string, List<UCCheckboxLabelType2>> IOButtonList = new Dictionary<string, List<UCCheckboxLabelType2>>();
        Dictionary<int, Tag> OutputTagList = new Dictionary<int, Logix.Tag>();
        private IOInput IOInput { get; set; }

        public ucIOInput(ref LogicClasses.Main MainConnection, ref IOInput _IOInput)
        {
            try
            {
                InitializeComponent();
                _Main = MainConnection;
                ReadIO();
                IOInput = _IOInput;
                T1.IsChecked = IOInput.WorkTag(ref EMTag) && ((bool?)EMTag.Value) == true;
                IOInput.OnUpdate += new IOInput.onUpdateHandler(IOInput_OnUpdate);
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Initialize IO List Front");
            }
        }
        #endregion

        #region FormEvents
        private void EM_MouseClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Utilities.FileLogger.logButton(strIO, "Engineering Mode", MethodBase.GetCurrentMethod().ToString());
                ToggleButton TB = sender as ToggleButton;
                if (IOInput.WorkTag(ref EMTag, (Boolean)(TB.IsChecked)))
                    TB.Dispatcher.Invoke(() => TB.IsChecked = EMTag.Value.ToString() == "True" ? true : false);
                else
                    TB.Dispatcher.Invoke(() => TB.IsChecked = (Boolean)TB.IsChecked ? false : true);
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Trigger Engineering Mode Failed IO Page");
            }

        }

        private void IO_ucMouseClick(object sender, EventArgs e)
        {
            try
            {
                UCCheckboxLabelType2 cb = (UCCheckboxLabelType2)sender;
                string TempTag = OutputTagList[Convert.ToInt32(cb.Tag)].Name;
                bool NextValue = cb.ucIsCheck;

                if (_Main.OPC.Read<bool>(EMTag.Name))
                {
                    _Main.OPC.Read<bool>(TempTag);
                    _Main.OPC.Write(TempTag, NextValue);
                }

                //UCCheckboxLabelType2 cb = (UCCheckboxLabelType2)sender;
                //if (IOInput.WorkTag(ref EMTag) && (Boolean)EMTag.Value)
                //{
                //    Tag TempTag = OutputTagList[Convert.ToInt32(cb.Tag)];
                //    Utilities.FileLogger.logButton(strIO, "IO Trigger - " + TempTag.Name, MethodBase.GetCurrentMethod().ToString());
                //    IOInput.WorkTag(ref TempTag);
                //    if (!IOInput.WorkTag(ref TempTag, cb.ucIsCheck))
                //    { Utilities.FileLogger.logError("Trigger:" + TempTag.Name, "Write IO Failed"); }
                //}
                //else
                //    Utilities.FileLogger.logError("Trigger:" + EMTag.Name, "Write IO Failed");
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Write IO Failed");
            }
        }

        private void SelectionChangedMethod(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TabControl TC = (TabControl)sender;
            GOdeeper:
                TabItem TI = TC.SelectedItem as TabItem;
                if (TI == null) return;
                if (TI.Header.ToString() != "Input" && TI.Header.ToString() != "Output")
                {
                    TC = TI.Content as TabControl;
                    goto GOdeeper;
                }
                IOInput.Locnow = TI.Tag.ToString();
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "SelectionChangedMethod");
            }
        }

        ScrollViewer AddUIIO(string IO, string Loc, string Module)
        {
            ScrollViewer SclV = new ScrollViewer { VerticalScrollBarVisibility = ScrollBarVisibility.Disabled, HorizontalScrollBarVisibility = ScrollBarVisibility.Auto };
            WrapPanel WP = new WrapPanel { Orientation = Orientation.Vertical };
            List<UCCheckboxLabelType2> TempCBList = new List<UCCheckboxLabelType2>();
            List<string> TNlist = new List<string>();
            SclV.Content = WP;
            try
            {
                WP.SetValue(DockPanel.DockProperty, Dock.Top);
                //"SELECT * FROM IOList Where Status = 'A' 
                //    and IO = '{IO}' and LEFT(DisplayName,3) = '{Loc}' ORDER BY 1 , 2"
                DataTable DT = DB.ExecuteQueryDT_Select($"SELECT * FROM [IO] Where [Status] = 'A' " +
                    $"and [IO] = '{(IO.Equals("IN") ? "I" : "O")}' and [IOIndex] = '{Loc}' and [StationID] = {_Main.StationID} and [IOType] = '{Module}' ORDER BY CAST([TAGINDEX] AS INT)", ref ErrMsg);
                foreach (DataRow dr in DT.Rows)
                {
                    UCCheckboxLabelType2 IOnew = new UCCheckboxLabelType2();

                    if (!string.IsNullOrWhiteSpace(dr["OutputTagName"].ToString()))
                        OutputTagList.Add(Convert.ToInt16(dr["TagIndex"]), new Tag { Name = dr["OutputTagName"].ToString(), DataType = Logix.Tag.ATOMIC.BOOL });

                    IOnew.ucMouseClick += new EventHandler(IO_ucMouseClick);
                    IOnew.Tag = Convert.ToInt16(dr["TagIndex"]);
                    IOnew.FontSize = 16;
                    IOnew.ucIsHitTestVisible = IO.Equals("IN") ? false : true;
                    IOnew.ucLabelTitleContent = dr["DisplayName"].ToString();//.Replace(Loc, string.Empty);
                    WP.Children.Add(IOnew);
                    TempCBList.Add(IOnew);

                    TNlist.Add(dr["TagName"].ToString());
                }
                IOButtonList.Add(IO + Loc + Module, TempCBList);
                return SclV;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Set Up IO Wrap Panel Failed");
                return SclV;
            }
            finally
            {
                WP = null; TempCBList = null; TNlist = null; SclV = null;
            }

        }

        void ReadIO()
        {
            try
            {
                //"SELECT [Module], LEFT(DisplayName,3) as Loc FROM 
                //    IOList Where Status = 'A' Group by LEFT(DisplayName,3), [Module] order by 1,2"
                DataTable DT = DB.ExecuteQueryDT_Select($"SELECT IOType as 'Module', IOIndex as 'Loc' FROM IO " +
                    $"Where Status = 'A' and StationID = {_Main.StationID} Group by IOIndex, IOType order by 1, 2", ref ErrMsg);
                TabControl TCM = new TabControl() { TabStripPlacement = Dock.Left };
                TabControl TC = new TabControl();
                TabItem TIKing = new TabItem();
                TabItem TIM = new TabItem();
                TabItem TI;
                int count = 0;
                string Name = string.Empty;
                foreach (DataRow dr in DT.Rows)
                {
                    TC = new TabControl { TabStripPlacement = Dock.Bottom };
                    RegisterName(dr["Module"].ToString() + dr["Loc"].ToString(), TC);

                    Name = "IN" + dr["Loc"].ToString() + dr["Module"].ToString();
                    TI = new TabItem
                    {
                        Header = "Input",
                        Visibility = Visibility.Visible,
                        Content = AddUIIO("IN", dr["Loc"].ToString(), dr["Module"].ToString()),
                        Tag = Name
                    };
                    TC.Items.Add(TI);

                    Name = "OUT" + dr["Loc"].ToString() + dr["Module"].ToString();
                    TI = new TabItem
                    {
                        Header = "Output",
                        Visibility = Visibility.Visible,
                        Content = AddUIIO("OUT", dr["Loc"].ToString(), dr["Module"].ToString()),
                        Tag = Name
                    };
                    TC.Items.Add(TI);

                    TIM = new TabItem { Header = dr["Loc"].ToString(), Content = TC, Visibility = Visibility.Visible };
                    TCM.Items.Add(TIM);

                    if (DT.Rows.Count == count + 1 || (DT.Rows.Count > count && DT.Rows[count + 1]["Module"].ToString() != dr["Module"].ToString()))
                    {
                        TIKing = new TabItem { Header = new TextBlock { Text = dr["Module"].ToString() }, Content = TCM, Visibility = Visibility.Visible };
                        TCM = new TabControl { TabStripPlacement = Dock.Left };
                        IOMajorTabControl.Items.Add(TIKing);
                    }
                    count++;
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Add IO Fail");
            }
        }
        #endregion

        #region Events
        void IOInput_OnUpdate()
        {
            try
            {
                if (!_Main.IOLocPageON)
                {
                    ModeCheck();
                    IO_UPDATE();
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Not able to Read Status from IO Status List");
            }
        }
        #endregion

        #region Methods
        void ModeCheck()
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() => T1.IsEnabled =
                IOInput.WorkTag(ref ModeTag) && ModeTag.Value?.ToString().ToUpper() == "FALSE"));

                this.Dispatcher.Invoke(new Action(() => T1.IsChecked =
                IOInput.WorkTag(ref EMTag) && EMTag.Value?.ToString().ToUpper() == "TRUE"));
            }
            catch (Exception ex)
            { Utilities.FileLogger.logError(ex.Message, "ModeCheck()"); }
        }


        int ns = 0; public static string Exactnow;
        private void IO_UPDATE()
        {
            try
            {
                ns = 0;
                Exactnow = IOInput.Locnow;
                foreach (UCCheckboxLabelType2 uc in IOButtonList[Exactnow])
                {
                    this.Dispatcher.Invoke(new Action(() => ns = Convert.ToInt16(uc.Tag)));
                    if (Exactnow.Contains("IN") && _Main.IValue.Count != 0)
                        IO_Update(uc, _Main.IValue[ns]);
                    else if (Exactnow.Contains("OUT") && _Main.OValue.Count != 0)
                        IO_Update(uc, _Main.OValue[ns]);
                }
            }
            catch (Exception ex)
            { Utilities.FileLogger.logError("IO_UPDATE", ex.ToString()); }
        }

        private void IO_Update(UCCheckboxLabelType2 cbl, bool io)
        {
            if (!cbl.Dispatcher.CheckAccess())
                cbl.Dispatcher.Invoke(new Action(() => cbl.ucIsCheck = io));
            else
                cbl.ucIsCheck = io;
        }

        public void Dispose()
        {
            _Main.IOPageON = false;
        }
        #endregion

        #region Destructor
        ~ucIOInput()
        {
            Dispose();
        }
        #endregion
    }
}
