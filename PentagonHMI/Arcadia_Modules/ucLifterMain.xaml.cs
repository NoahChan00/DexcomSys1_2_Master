using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Logix;
using System.IO;
using GalaSoft.MvvmLight;
using System.Windows.Media;
using System.Windows.Input;

namespace PentagonHMI.Arcadia_Modules
{
    public partial class ucLifterMain : UserControl
    {
        LogicClasses.Main _Main;
        Tag Tag_MaterialName;
        Tag Tag_RackIndexNo;
        Tag Tag_RackRFIDTag;
        Tag Tag_MaxIndex;
        Tag Tag_RackStatus;
        Tag Tag_SkipRemainingSlot;
        Tag Tag_SkipSlot;
        Tag Tag_SkipRack;

        string Tag_WriteRFID;
        string Tag_UpdateIndex_bool;
        string Path = string.Empty;
        string str_EditRackID = "Edit RFID Tag";
        string str_UpdateRackID = "Update RFID Tag";
        string str_Edit = "Edit";
        string str_Update = "Update";
        int LastMaxSlot = 0;

        int LastCurrent = -1;
        List<RackSlotModel> lst_RSM = new List<RackSlotModel>();
        Dictionary<string, BitmapImage> Dic_MaterialName_Image = new Dictionary<string, BitmapImage>();
        Brush Clr_Clicked => Brushes.LightSkyBlue;
        Brush Clr_UnClick => Brushes.Silver;


        public ucLifterMain()
        {
            InitializeComponent();
        }

        public void Setup(ref LogicClasses.Main main, int Zone, string Position)
        {
            try
            {
                _Main = main;
                _Main.OnFastUpdate += new LogicClasses.Main.OnFastUpdateHandler(Update);

                //PLC Address
                Info.OPC.TagGroups.Lifter.AddTag(Tag_MaterialName = new Tag { Name = $"{Position}End_Lifter.str_MaterialNameZ{Zone}", DataType = Logix.Tag.ATOMIC.STRING });
                Info.OPC.TagGroups.Lifter.AddTag(Tag_RackIndexNo = new Tag { Name = $"{Position}End_Lifter.int_RackIndexNoZ{Zone}", DataType = Logix.Tag.ATOMIC.INT });
                Info.OPC.TagGroups.Lifter.AddTag(Tag_RackRFIDTag = new Tag { Name = $"{Position}End_Lifter.str_RackRFIDTagZ{Zone}", DataType = Logix.Tag.ATOMIC.STRING });
                Info.OPC.TagGroups.Lifter.AddTag(Tag_MaxIndex = new Tag { Name = $"{(Position == "Front" ? "F" : "B")}RZ2{(Zone == 1 ? "A" : "B")}_Rack_MaxIndex", DataType = Logix.Tag.ATOMIC.INT });
                Info.OPC.TagGroups.Lifter.AddTag(Tag_RackStatus = new Tag { Name = $"{Position}End_Z{Zone}_RackInfo", DataType = Logix.Tag.ATOMIC.INT });

                Info.OPC.TagGroups.Lifter.AddTag(Tag_SkipRemainingSlot = new Tag { Name = $"{Position}End_Lifter.bool_SlotSkipRemainZ{Zone}", DataType = Logix.Tag.ATOMIC.BOOL });
                Info.OPC.TagGroups.Lifter.AddTag(Tag_SkipSlot = new Tag { Name = $"{Position}End_Lifter.bool_SlotSkipZ{Zone}", DataType = Logix.Tag.ATOMIC.BOOL });
                Info.OPC.TagGroups.Lifter.AddTag(Tag_SkipRack = new Tag { Name = $"{Position}End_Lifter.bool_SkipRackZ{Zone}", DataType = Logix.Tag.ATOMIC.BOOL });

                Tag_WriteRFID = $"{Position}End_Lifter.bool_WriteRFIDZ{Zone}";
                Tag_UpdateIndex_bool = $"{Position}End_Lifter.bool_UpdateIndexZ{Zone}";
                tbx_CurrentRackID.Text = _Main.OPC.Read<string>(Tag_RackRFIDTag.Name);
                btn_UpdateRFIDtag.Content = str_EditRackID;
                btn_SkipSlot.Background = RackSlotModel.Dic_Sts_Clr[RackSlotModel.StsClr.Disabled];
                //Image Path
                Func<string, object> GetItem = (x) => _Main.dt_Config.Select().Where(y => y["Item"].ToString().ToUpper() == x.ToUpper()).First()["Info"];
                Path = @GetItem("LifterImagePath").ToString();

                cbx_CurrentSlotIndex.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            };
        }

        private void Update()
        {
            try
            {
                for (int n = 0; n < Info.OPC.TagGroups.Lifter.Count; n++)
                {
                    Tag Temp = Info.OPC.TagGroups.Lifter.Tags[n] as Tag;
                    _Main.OPC.ReadTag(ref Temp, Temp.DataType);
                }
                //Ctrl.GroupRead(Info.OPC.TagGroups.Lifter);
                Dispatcher.Invoke(() => UpdateUI());
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
            };
        }

        string CurrentRack;
        private void UpdateUI()
        {
            bool IsOperator = _Main.UserAccessLevel.ToUpper() == "OPERATOR";

            if (IsOperator)
            {
                btn_UpdateRFIDtag.IsEnabled = false;
            }
            else
            {
                btn_UpdateRFIDtag.IsEnabled = true;
            }

            foreach (Tag _tag in Info.OPC.TagGroups.Lifter.Tags)
            {
                if (_tag.Name == Tag_MaterialName.Name)
                {
                    if (CurrentRack != _tag.ToTagString())
                    {
                        CurrentRack = _tag.ToTagString();

                        if (CurrentRack.ToUpper().Equals("BIOS")) ShowBIOSMsg(true); else ShowBIOSMsg(null);

                        BitmapImage img;
                        Img_CurrentRackInLifter.Source = null;
                        if (Dic_MaterialName_Image.TryGetValue(CurrentRack.ToUpper(), out img))
                            Img_CurrentRackInLifter.Source = img;
                        else
                        {
                            string Imgpath = Path + CurrentRack;
                            if (File.Exists(Imgpath + ".png"))
                            {
                                Dic_MaterialName_Image.Add(CurrentRack.ToUpper(), new BitmapImage(new Uri(Imgpath + ".png")));
                                Img_CurrentRackInLifter.Source = Dic_MaterialName_Image[CurrentRack.ToUpper()];
                            }
                        }
                        tbx_CurrentRackInLifter.Text = _tag.ToTagString();
                    }
                }
                else if (_tag.Name == Tag_MaxIndex.Name)
                {
                    int NewMaxSlot = Convert.ToInt32(_tag.Value?.ToString() ?? "0");
                    if (NewMaxSlot != LastMaxSlot)
                    {
                        lst_RSM = new List<RackSlotModel>();
                        LastMaxSlot = NewMaxSlot;
                        //for (int n = 1; n <= NewMaxSlot; n++)
                        for (int n = NewMaxSlot; n > 0; n--)
                            lst_RSM.Add(new RackSlotModel { RackIndex = n.ToString(), Status = RackSlotModel.Dic_Sts_Clr[RackSlotModel.StsClr.Clean] });
                        itmctrl_RackLayout.ItemsSource = lst_RSM;
                        cbx_CurrentSlotIndex.ItemsSource = lst_RSM.Select(x => x.RackIndex).OrderBy(x => Convert.ToInt32(x));
                    }
                }
                else if (_tag.Name == Tag_RackIndexNo.Name)
                {
                    tbx_CurrentRackIndexNumber.Text = _tag.ToTagString();
                    int n;
                    if (int.TryParse(_tag.ToTagString(), out n) && lst_RSM.Count >= n && lst_RSM.Count > 0)
                    {
                        --n;
                        LastCurrent = n;
                        n = LastMaxSlot - 1 - n;
                        lst_RSM[n].Status = lst_RSM[n].Status == lst_RSM[n].bk_clr ? RackSlotModel.Dic_Sts_Clr[RackSlotModel.StsClr.Current] : lst_RSM[n].bk_clr;
                    }
                }
                else if (_tag.Name == Tag_RackStatus.Name)
                {
                    int n;
                    if (int.TryParse(_tag.ToTagString(), out n) && lst_RSM.Count > 0)
                    {
                        char[] Binary = Convert.ToString(n, 2).PadRight(LastMaxSlot, '0').ToCharArray().Reverse().ToArray();
                        //char[] Binary = Convert.ToString(n, 2).PadRight(16, '0').ToCharArray();
                        for (int i = 0; i < lst_RSM.Count; i++)
                        {
                            Brush clr = RackSlotModel.Dic_Sts_Clr[Binary[i] == '1' ? RackSlotModel.StsClr.Loaded : RackSlotModel.StsClr.Clean];
                            if (i != (LastMaxSlot - 1 - LastCurrent)) lst_RSM[i].Status = clr;
                            lst_RSM[i].bk_clr = clr;
                        }
                    }
                }
                else if (_tag.Name == Tag_RackRFIDTag.Name && IsOperator)
                    tbx_CurrentRackID.Text = _tag.ToTagString();
                else if (_tag.Name == Tag_SkipRack.Name)
                    btn_SkipRack.Background = _tag.ToTagBool() ? Clr_Clicked : Clr_UnClick;
                else if (_tag.Name == Tag_SkipRemainingSlot.Name)
                    btn_SkipRemaining.Background = _tag.ToTagBool() ? Clr_Clicked : Clr_UnClick;
                else if (_tag.Name == Tag_SkipSlot.Name)
                    btn_SkipSlot.Background = _tag.ToTagBool() ? Clr_Clicked : Clr_UnClick;
            }
        }

        private void ShowBIOSMsg(bool? Yes)
        {
            if (Yes == null)
            {
                brd_BiosMsg.Visibility = Visibility.Collapsed;
                brd_ShowBiosMsg.Visibility = Visibility.Collapsed;
            }
            else if (Yes == true)
            {
                brd_BiosMsg.Visibility = Visibility.Visible;
                brd_ShowBiosMsg.Visibility = Visibility.Collapsed;
            }
            else if (Yes == false)
            {
                brd_BiosMsg.Visibility = Visibility.Collapsed;
                brd_ShowBiosMsg.Visibility = Visibility.Visible;
            }
        }

        private void CurrentSlotStatus_Click(object sender, MouseButtonEventArgs e)
        {
            Label btn = sender as Label;//            Button btn = sender as Button;
            string Key = btn.Tag.ToString().ToUpper();
            bool value = btn.Background == Clr_Clicked ? false : true;
            btn.Dispatcher.Invoke(() => { btn.Background = btn.Background == Clr_Clicked ? Clr_UnClick : Clr_Clicked; });
            bool ResetSkipSlot = true, ResetSkipRSlot = true, ResetSkipRack = true;
            switch (Key)
            {
                case "SKIPSLOT":
                    lst_RSM[LastCurrent].isSkip = value;
                    _Main.OPC.Write(Tag_SkipSlot.Name, value);
                    ResetSkipSlot = false;
                    goto default;
                case "SKIPRSLOT":
                    _Main.OPC.Write(Tag_SkipRemainingSlot.Name, value);
                    ResetSkipRSlot = false;
                    goto default;
                case "SKIPRACK":
                    _Main.OPC.Write(Tag_SkipRack.Name, value);
                    ResetSkipRack = false;
                    goto default;
                default:
                    value = false;
                    if (ResetSkipRSlot) goto case "SKIPRSLOT";
                    if (ResetSkipSlot) goto case "SKIPSLOT";
                    if (ResetSkipRack) goto case "SKIPRACK";
                    break;
            }
        }

        private void UpdateRFIDtag_Click(object sender, RoutedEventArgs e)
        {
            if (btn_UpdateRFIDtag.Content.ToString() == str_EditRackID)
            {
                tbx_CurrentRackID.IsEnabled = true;
                btn_UpdateRFIDtag.Content = str_UpdateRackID;
            }
            else if (btn_UpdateRFIDtag.Content.ToString() == str_UpdateRackID)
            {
                tbx_CurrentRackID.IsEnabled = false;
                btn_UpdateRFIDtag.Content = str_EditRackID;

                _Main.OPC.Read<string>(Tag_RackRFIDTag.Name);
                if (_Main.OPC.Write(Tag_RackRFIDTag.Name, tbx_CurrentRackID.Text, typeof(string)))
                    if (!_Main.OPC.Write(Tag_WriteRFID, true))
                        Dispatcher.Invoke(() => tbx_CurrentRackID.Text = _Main.OPC.Read<string>(Tag_RackRFIDTag.Name));
            }
        }

        private void Rack_Index_Update_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Content.ToString() == str_Edit)
            {
                btn.Content = str_Update;
                cbx_CurrentSlotIndex.Visibility = Visibility.Visible;
                cbx_CurrentSlotIndex.SelectedIndex = LastCurrent > 0 ? LastCurrent : 0;
            }
            else if (btn.Content.ToString() == str_Update)
            {
                btn.Content = str_Edit;
                cbx_CurrentSlotIndex.Visibility = Visibility.Collapsed;
                int n;
                if (int.TryParse(cbx_CurrentSlotIndex.SelectedValue?.ToString(), out n))
                {
                    _Main.OPC.Write(Tag_RackIndexNo.Name, n, typeof(Int16));
                    _Main.OPC.Write(Tag_UpdateIndex_bool, true);
                }
            }
        }

        private void RC_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string TagName = string.Empty;
            if (btn.Content.ToString().ToUpper() == "START")
                TagName = "HMI_Start_RackConv";
            else if (btn.Content.ToString().ToUpper() == "STOP")
                TagName = "HMI_Stop_RackConv";
            else if (btn.Content.ToString().ToUpper() == "RESET")
                TagName = "HMI_Reset_RackConv";
            else
                return;

            _Main.OPC.Write(TagName, true);
            MessageBox.Show($"Rack Conveyor {btn.Content} Triggered");

        }

        private void btn_BiosMsgNoted_Click(object sender, RoutedEventArgs e)
        {
            ShowBIOSMsg(false);
        }

        private void btn_ShowMsg_Click(object sender, RoutedEventArgs e)
        {
            ShowBIOSMsg(true);
        }
    }

    public static class Extension
    {
        public static string ToTagString(this Tag _tag)
        {
            return _tag?.Value?.ToString() ?? string.Empty;
        }
        public static bool ToTagBool(this Tag _tag)
        {
            return Convert.ToBoolean(_tag?.Value ?? "False");
        }
    }

    public class RackSlotModel : ViewModelBase
    {
        //Status
        private string rackIndex = string.Empty;
        public string RackIndex
        {
            get { return rackIndex; }
            set
            {
                if (rackIndex != value)
                {
                    rackIndex = value;
                    RaisePropertyChanged(nameof(RackIndex));
                }
            }
        }

        public enum StsClr
        {
            Loaded,
            Disabled,
            Current,
            Clean
        }

        public static Dictionary<StsClr, Brush> Dic_Sts_Clr = new Dictionary<StsClr, Brush>
        {
            [StsClr.Clean] = Brushes.White,
            [StsClr.Disabled] = Brushes.Gray,
            [StsClr.Current] = Brushes.Yellow,
            [StsClr.Loaded] = Brushes.SpringGreen,

        };

        private Brush status = Brushes.White;
        public Brush Status
        {
            get { return status; }
            set
            {
                if (status != value)
                {
                    status = value;
                    RaisePropertyChanged(nameof(Status));
                }
            }
        }
        public Brush bk_clr { get; set; } = Brushes.White;

        public bool isSkip = false;
    }

}
