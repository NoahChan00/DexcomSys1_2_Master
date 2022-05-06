using System;
using System.Windows.Controls;
using Utilities;
using Logix;
using SimpleOPC;
using System.Threading.Tasks;
using System.Linq;

namespace PentagonHMI.Views
{
    public partial class MainView : UserControl, IDisposable
    {
        LogicClasses.Main _Main;

        #region Constructor
        public MainView(ref LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                _Main = _main;
                _Main.Home_OnUpdate += new LogicClasses.Main.onHomeUpdateHandler(Update);
                //Task.Factory.StartNew(UpdateAsync, TaskCreationOptions.LongRunning);
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }


        string[] Value_Input_OutputPartNumber = new string[10];
        string[] Value_Input_Family = new string[10];
        string[] Value_Input_PCode = new string[10];
        string[] Value_Input_QCode = new string[10];
        string[] Value_Input_SCode = new string[10];
        string[] Value_Input_ZCode = new string[10];
        string[] Value_Output_PCode = new string[10];
        string[] Value_Output_QCode = new string[10];
        string[] Value_Output_SCode = new string[10];
        string[] Value_Output_ZCode = new string[10];
        int[] Value_Pogo_Reject = new int[40];
        bool[] Value_Vision_PartPresent = new bool[40];
        bool[] Value_Program_PartPresent = new bool[40];
        bool[] Value_Vision_Pass = new bool[40];
        bool[] Value_Program_Pass = new bool[40];
        bool[] Value_Vision_Fail = new bool[40];
        bool[] Value_Program_Fail = new bool[40];
        public void Update()
        {
            lcl_UPHView.Update(_Main.Lst_UPH);

            foreach (Tag _tag in Info.OPC.TagGroups.MainView.Tags)
            {
                if (_tag.Name == Info.OPC.Tags.MainView.CurrentInput_PCode_str)
                    Dispatcher.Invoke(() => txb_PField.Text = _tag.Value?.ToString());
                else if (_tag.Name == Info.OPC.Tags.MainView.CurrentInput_QCode_str)
                    Dispatcher.Invoke(() => txb_QField.Text = _tag.Value?.ToString());
                else if (_tag.Name == Info.OPC.Tags.MainView.CurrentInput_SCode_str)
                    Dispatcher.Invoke(() => txb_SField.Text = _tag.Value?.ToString());
                else if (_tag.Name == Info.OPC.Tags.MainView.CurrentInput_ZCode_str)
                    Dispatcher.Invoke(() => txb_ZField.Text = _tag.Value?.ToString());
                else if (_tag.Name == Info.OPC.Tags.MainView.CurrentInput_Family_str)
                    Dispatcher.Invoke(() => txb_Family.Text = _tag.Value?.ToString());
                else if (_tag.Name == Info.OPC.Tags.MainView.CurrentInput_OutputPartNumber_str)
                    Dispatcher.Invoke(() => txb_OutputPartNumber.Text = _tag.Value?.ToString());
                else if (_tag.Name == string.Format(Info.OPC.Tags.MainView.Array_Input_Family_str, 1))
                    Value_Input_Family = (string[])_tag.Value;
                else if (_tag.Name == string.Format(Info.OPC.Tags.MainView.Array_Input_OutputPartNumber_str, 1))
                    Value_Input_OutputPartNumber = (string[])_tag.Value;
                else if (_tag.Name == string.Format(Info.OPC.Tags.MainView.Array_Input_PCode_str, 1))
                    Value_Input_PCode = (string[])_tag.Value;
                else if (_tag.Name == string.Format(Info.OPC.Tags.MainView.Array_Input_QCode_str, 1))
                    Value_Input_QCode = (string[])_tag.Value;
                else if (_tag.Name == string.Format(Info.OPC.Tags.MainView.Array_Input_ZCode_str, 1))
                    Value_Input_ZCode = (string[])_tag.Value;
                else if (_tag.Name == string.Format(Info.OPC.Tags.MainView.Array_Input_SCode_str, 1))
                    Value_Input_SCode = (string[])_tag.Value;
                else if (_tag.Name == string.Format(Info.OPC.Tags.MainView.Array_Output_PCode_str, 1))
                    Value_Output_PCode = (string[])_tag.Value;
                else if (_tag.Name == string.Format(Info.OPC.Tags.MainView.Array_Output_QCode_str, 1))
                    Value_Output_QCode = (string[])_tag.Value;
                else if (_tag.Name == string.Format(Info.OPC.Tags.MainView.Array_Output_ZCode_str, 1))
                    Value_Output_ZCode = (string[])_tag.Value;
                else if (_tag.Name == string.Format(Info.OPC.Tags.MainView.Array_Output_SCode_str, 1))
                    Value_Output_SCode = (string[])_tag.Value;
                else if (_tag.Name == Info.OPC.Tags.MainView.Array_Reject_PogoNumber_dint)
                    Value_Pogo_Reject = (int[])_tag.Value;

                //public static string Array_PartPresent_bool => "Machine_ChipStatus[{0}].PartPresent";
                //public static string Array_Pass_bool => "Machine_ChipStatus[{0}].Pass";
                //public static string Array_Fail_bool => "Machine_ChipStatus[{0}].Fail";
                else if (_tag.Name.Contains("Machine_ChipStatus"))
                {
                    if (_tag.Name.Contains("PartPresent"))
                    {
                        int Index = Convert.ToInt32(_tag.Name.Remove(_tag.Name.Length - 13).Remove(0, 19));
                        if (Index >= 40)
                            Value_Program_PartPresent[Index - 40] = Convert.ToBoolean(_tag.Value);
                        else
                            Value_Vision_PartPresent[Index] = Convert.ToBoolean(_tag.Value);
                    }
                    else if (_tag.Name.Contains("Pass"))
                    {
                        int Index = Convert.ToInt32(_tag.Name.Remove(_tag.Name.Length - 6).Remove(0, 19));
                        if (Index >= 40)
                            Value_Program_Pass[Index - 40] = Convert.ToBoolean(_tag.Value);
                        else
                            Value_Vision_Pass[Index] = Convert.ToBoolean(_tag.Value);
                    }
                    else if (_tag.Name.Contains("Fail"))
                    {
                        int Index = Convert.ToInt32(_tag.Name.Remove(_tag.Name.Length - 6).Remove(0, 19));
                        if (Index >= 40)
                            Value_Program_Fail[Index - 40] = Convert.ToBoolean(_tag.Value);
                        else
                            Value_Vision_Fail[Index] = Convert.ToBoolean(_tag.Value);
                    }
                }
            }

            Custom_PartStatus.Update(Value_Pogo_Reject);
            Custom_PartStatus.Update_Program_Status(Value_Program_PartPresent, Value_Program_Pass, Value_Program_Fail); 
            Custom_PartStatus.Update_Vision_Status(Value_Vision_PartPresent, Value_Vision_Pass, Value_Vision_Fail);

            int Loop = 0;
            foreach (var Pos in Cassettle.cassetteList[0].PositionList)
            {
                foreach (var Value in Pos.CassetteSubStatusList)
                {
                    if (Value.SubStatusName == "P Field" && Value_Input_PCode != null)
                        Value.SubStatus = Value_Input_PCode[Loop];
                    else if (Value.SubStatusName == "Q Field" && Value_Input_QCode != null)
                        Value.SubStatus = Value_Input_QCode[Loop];
                    else if (Value.SubStatusName == "S Field" && Value_Input_SCode != null)
                        Value.SubStatus = Value_Input_SCode[Loop];
                    else if (Value.SubStatusName == "Z Field" && Value_Input_ZCode != null)
                        Value.SubStatus = Value_Input_ZCode[Loop];
                    else if (Value.SubStatusName == "Family" && Value_Input_Family != null)
                        Value.SubStatus = Value_Input_Family[Loop];
                    else if (Value.SubStatusName == "OutputPartNum" && Value_Input_OutputPartNumber != null)
                        Value.SubStatus = Value_Input_OutputPartNumber[Loop];
                }
                Loop++;
            }

            Loop = 0;
            foreach (var Pos in Cassettle.cassetteList[1].PositionList)
            {
                foreach (var Value in Pos.CassetteSubStatusList)
                {
                    if (Value.SubStatusName == "P Field")
                        Value.SubStatus = Value_Output_PCode[Loop];
                    else if (Value.SubStatusName == "Q Field")
                        Value.SubStatus = Value_Output_QCode[Loop];
                    else if (Value.SubStatusName == "S Field")
                        Value.SubStatus = Value_Output_SCode[Loop];
                    else if (Value.SubStatusName == "Z Field")
                        Value.SubStatus = Value_Output_ZCode[Loop];
                }
                Loop++;
            }

        }

        //private async Task UpdateAsync()
        //{
        //    while(true)
        //    {
        //        if(IsRuning)
        //        {


        //            await Task.Delay(200);
        //        }
        //        else
        //        {
        //            await Task.Delay(5000);
        //        }

        //    }
        //}

        public void Dispose()
        {
            _Main.HomePageON = false;
            Info.OPC.TagGroups.MainView.Active = false;
        }
        #endregion
    }
}