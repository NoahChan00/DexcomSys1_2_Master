using Logix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interop;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for PopUpView.xaml.
    /// </summary>
    public partial class PopUpView : Window, IDisposable
    {
        #region PrivateFields
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private List<PopUpModel> popUpList = new List<PopUpModel>();
        private ICollectionView popUpListView = null;
        private LogicClasses.Main _Main = null;
        #endregion

        #region Constructor
        public PopUpView(ref LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                _Main = _main;
                initializePopUp();
                initializeOpc();
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private void initializePopUp()
        {
            try
            {
                popUpList = new List<PopUpModel>();
                DataTable DT = _Main.SQLer.Exec_DTSelect($"SELECT * FROM [POPUPINFO] WHERE [STATIONID] = {_Main.StationID}");
                if (DT != null)
                    foreach (DataRow dr in DT.Rows)
                    {
                        List<PopUpMessageDescriptionModel> lst_Desc = new List<PopUpMessageDescriptionModel>();
                        List<PopUpActionModel> lst_Action = new List<PopUpActionModel>();
                        string Desc_Index = "DESCRIPTION";
                        if (!string.IsNullOrWhiteSpace(Desc_Index = dr[Desc_Index]?.ToString() ?? string.Empty))
                        {
                            string[] Ary_Desc_Index = Desc_Index.Split(';');
                            for (int n = 0; n < Ary_Desc_Index.Count();)
                            {
                                lst_Desc.Add(new PopUpMessageDescriptionModel
                                {
                                    PopUpMessageIndex = Ary_Desc_Index[n++],
                                    PopUpMessageDescription = Ary_Desc_Index[n++]
                                });
                            }
                        }

                        string Name_Address = "ACTION";
                        if (!string.IsNullOrWhiteSpace(Name_Address = dr[Name_Address]?.ToString() ?? string.Empty))
                        {
                            string[] Ary_Name_Address = Name_Address.Split(';');
                            for (int n = 0; n < Ary_Name_Address.Count();)
                            {
                                lst_Action.Add(new PopUpActionModel
                                {
                                    PopUpActionName = Ary_Name_Address[n++],
                                    PopUpActionTag = new Tag { Name = Ary_Name_Address[n++], DataType = Logix.Tag.ATOMIC.BOOL }
                                });
                            }
                        }

                        Dictionary<string, Tag.ATOMIC> Dic_Type_LogixType = new Dictionary<string, Tag.ATOMIC>
                        {
                            ["BOOL"] = Logix.Tag.ATOMIC.BOOL,
                            ["STRING"] = Logix.Tag.ATOMIC.STRING,
                            ["REAL"] = Logix.Tag.ATOMIC.REAL,
                            ["DINT"] = Logix.Tag.ATOMIC.DINT,
                            ["INT"] = Logix.Tag.ATOMIC.INT,
                        };

                        string[] Trigger_TagName_Type = dr["TriggerTag"].ToString().Contains(';') ? dr["TriggerTag"].ToString().Split(';') : new string[] { "", "Bool" };
                        string[] Message_TagName_Type = dr["MessageTag"].ToString().Contains(';') ? dr["MessageTag"].ToString().Split(';') : new string[] { "", "Bool" };
                        popUpList.Add(
                        new PopUpModel
                        {
                            PopUpTag = string.IsNullOrWhiteSpace(Trigger_TagName_Type[0]) ? new Tag() : new Tag { Name = Trigger_TagName_Type[0], DataType = Dic_Type_LogixType[Trigger_TagName_Type[1].ToUpper()] },
                            PopUpMessageTag = string.IsNullOrWhiteSpace(Message_TagName_Type[0]) ? new Tag() : new Tag { Name = Message_TagName_Type[0], DataType = Dic_Type_LogixType[Message_TagName_Type[1].ToUpper()] },
                            PopUpMessage = dr["Message"].ToString(),
                            PopUpMessageDescriptionList = lst_Desc,
                            PopUpActionList = lst_Action
                        });
                    }

                popUpListView = CollectionViewSource.GetDefaultView(popUpList);
                PopUpItemsControl.ItemsSource = popUpListView;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeOpc()
        {
            try
            {
                _Main.OnAlwaysUpdate += main_OnPopUpUpdate;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateEventMethods
        private void popUpWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void popUpWindow_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                e.Cancel = true;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void popUpActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                PopUpActionModel popUpActionModel = button.Tag as PopUpActionModel;

                if (popUpList.Exists(x => x.PopUpActionList.Exists(y => y == popUpActionModel)))
                {
                    PopUpModel popUpModel = popUpList.Find(
                        x => x.PopUpActionList.Exists(y => y == popUpActionModel));

                    if (MessageBox.Show($"{popUpModel.PopUpMessage}!{Environment.NewLine}" +
                        $"Are you sure you want to {popUpActionModel.PopUpActionName}?",
                        nameof(MessageBoxImage.Question), MessageBoxButton.YesNo,
                        MessageBoxImage.Question, MessageBoxResult.No,
                        MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        popUpModel.PopUpTag.Value = false;
                        popUpActionModel.PopUpActionTag.Value = true;

                        _Main.MyPLC.WriteTag(popUpModel.PopUpTag);
                        _Main.MyPLC.WriteTag(popUpActionModel.PopUpActionTag);

                        Hide();
                    }
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void main_OnPopUpUpdate()
        {
            try
            {
                popUpList.ForEach(x =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(x.PopUpTag.Name))
                        {
                            _Main.MyPLC.ReadTag(x.PopUpTag);
                        }

                        if (!string.IsNullOrEmpty(x.PopUpMessageTag.Name))
                        {
                            _Main.MyPLC.ReadTag(x.PopUpMessageTag);
                            if (x.PopUpMessageTag.Value != null)
                            {
                                string value = x.PopUpMessageDescriptionList.Exists(
                                    y => y.PopUpMessageIndex == x.PopUpMessageTag.Value.ToString()) ?
                                    x.PopUpMessageDescriptionList.Find(y => y.PopUpMessageIndex ==
                                    x.PopUpMessageTag.Value.ToString()).PopUpMessageDescription :
                                    x.PopUpMessageTag.Value.ToString();

                                if (x.PopUpMessage != value)
                                {
                                    x.PopUpMessage = value;
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                });

                Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Background, new Action(() =>
                {
                    popUpListView.Filter = x =>
                {
                    try
                    {
                        PopUpModel popUpModel = x as PopUpModel;

                        if (popUpModel.PopUpTag.Value == null)
                        {
                            return false;
                        }
                        else
                        {
                            if (popUpModel.PopUpTag.DataType == Logix.Tag.ATOMIC.BOOL)
                            {
                                return Convert.ToBoolean(popUpModel.PopUpTag.Value) == true;
                            }
                            else if (popUpModel.PopUpTag.DataType == Logix.Tag.ATOMIC.INT)
                            {
                                return Convert.ToInt32(popUpModel.PopUpTag.Value) != 0;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                        return false;
                    }
                };

                    if (popUpList.Where(x => x.PopUpTag.DataType == Logix.Tag.ATOMIC.BOOL).ToList().Exists(
                        x => Convert.ToBoolean(x.PopUpTag.Value) == true) || popUpList.Where(
                            x => x.PopUpTag.DataType == Logix.Tag.ATOMIC.INT).ToList().Exists(
                            x => Convert.ToInt32(x.PopUpTag.Value) != 0))
                    {
                        Show();
                    }
                    else
                    {
                        Hide();
                    }
                }));
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PublicInterfaceMethods
        public void Dispose()
        {
        }
        #endregion
    }
}