using Logix;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Utilities;
using System.Data;
using System.Windows;

namespace PentagonHMI
{
    public partial class MachineInformationView : UserControl
    {
        #region PrivateFields

        private MachineInformationModel machineInformationModel = new MachineInformationModel();
        private LogicClasses.Main _Main = null;
        private Dictionary<string, string> Dic_Name_Value = new Dictionary<string, string>();

        #endregion PrivateFields

        #region Constructor

        public MachineInformationView(LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                _Main = _main;
                initializeMachineInformation();
                initializeOpc();
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion Constructor

        #region PrivateInitializeMethods

        private void initializeMachineInformation()
        {
            try
            {
                Dictionary<string, Tag.ATOMIC> Dic_Type_LogixType = new Dictionary<string, Tag.ATOMIC>
                {
                    ["BOOL"] = Logix.Tag.ATOMIC.BOOL,
                    ["STRING"] = Logix.Tag.ATOMIC.STRING,
                    ["REAL"] = Logix.Tag.ATOMIC.REAL,
                    ["DINT"] = Logix.Tag.ATOMIC.DINT
                };

                machineInformationModel = new MachineInformationModel();
                machineInformationModel.MachineInformationToggleStatusList = new List<MachineInformationToggleStatusModel>();
                machineInformationModel.MachineInformationTextList = new List<MachineInformationTextModel>();

                DataTable DT = _Main.SQLer.Exec_DTSelect($"SELECT * FROM [MACHINEINFO] WHERE [STATIONID] = {_Main.StationID}");
                if (DT != null)
                    foreach (DataRow dr in DT.Rows)
                    {
                        const string DisplayName = "DisplayName";
                        const string InfoType = "InfoType";
                        Tag _tag = new Tag { Name = dr["TagName"].ToString().Trim(), DataType = Dic_Type_LogixType[dr["TagDataType"].ToString().ToUpper()] };

                        if (dr[InfoType].ToString().ToUpper() == "STATUS")
                        {
                            MachineInformationToggleStatusModel SM = new MachineInformationToggleStatusModel
                            {
                                MachineInformationToggleStatusName = dr[DisplayName].ToString(),
                                MachineInformationToggleStatusTag = _tag
                            };
                            machineInformationModel.MachineInformationToggleStatusList.Add(SM);
                        }
                        else if (dr[InfoType].ToString().ToUpper() == "TEXT")
                        {
                            MachineInformationTextModel TM = new MachineInformationTextModel
                            {
                                MachineInformationTextName = dr[DisplayName].ToString(),
                                MachineInformationTextTag = _tag
                            };
                            machineInformationModel.MachineInformationTextList.Add(TM);
                        }
                    }

                MachineInformationGroupBox.DataContext = machineInformationModel;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        public void UpdateInfo(string Name, string Value)
        {
            if (Dic_Name_Value.ContainsKey(Name))
                Dic_Name_Value[Name] = Value;
            else
                Dic_Name_Value.Add(Name, Value);
        }

        private void initializeOpc()
        {
            try
            {
                _Main.OnAlwaysUpdate += main_OnAlwaysUpdate;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateInitializeMethods

        #region PrivateEventMethods

        private void main_OnAlwaysUpdate()
        {
            try
            {
                machineInformationModel.MachineInformationToggleStatusList.ForEach(x =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(x.MachineInformationToggleStatusTag.Name))
                        {
                            if (x.MachineInformationToggleStatusTag.Name == "DB")
                            {
                                x.MachineInformationToggleStatusTag.Value = _Main.SQLer.isConnected();
                            }
                            else
                            {
                                _Main.MyPLC.ReadTag(x.MachineInformationToggleStatusTag);
                            }

                            if (x.MachineInformationToggleStatusTag.Value != null)
                            {
                                bool value = Convert.ToBoolean(
                                    x.MachineInformationToggleStatusTag.Value);

                                if (x.MachineInformationToggleStatus != value)
                                {
                                    x.MachineInformationToggleStatus = value;
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                });

                machineInformationModel.MachineInformationTextList.ForEach(x =>
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(x.MachineInformationTextTag.Name))
                        {
                            if (x.MachineInformationTextName == "Station" ||
                                x.MachineInformationTextName == "Site" ||
                                x.MachineInformationTextName == "Line")
                            {
                                x.MachineInformationTextTag.Value = x.MachineInformationTextTag.Name;
                            }
                            else if (x.MachineInformationTextName == "DateTime")
                            {
                                x.MachineInformationTextTag.Value = DateTime.Now.ToString();
                            }
                            else
                            {
                                _Main.MyPLC.ReadTag(x.MachineInformationTextTag);
                            }

                            if (x.MachineInformationTextTag.Value != null)
                            {
                                string value = x.MachineInformationTextTag.Value.ToString();

                                //string value = x.MachineInformationTextDescriptionList.Exists(
                                //    y => y.MachineInformationTextIndex == x.MachineInformationTextTag.Value.ToString()) ?
                                //    x.MachineInformationTextDescriptionList.Find(y => y.MachineInformationTextIndex ==
                                //    x.MachineInformationTextTag.Value.ToString()).MachineInformationTextDescription :
                                //    x.MachineInformationTextTag.Value.ToString();

                                //if (x.MachineInformationTextName == "Cycle Time" || x.MachineInformationTextName == "OPR Load Time")
                                //{
                                //    //value = $"{Convert.ToDouble(value).ToString("####0.00")} s";
                                //    value = $"{value} s";
                                //}
                                //else if (x.MachineInformationTextName == "Total Angle")
                                //{
                                //    value = $"{value} Degree";
                                //}
                                //else if (x.MachineInformationTextName == "Peak Torque")
                                //{
                                //    value = $"{value} mNm";
                                //}

                                //if (x.MachineInformationTextName.Contains("UPH") || x.MachineInformationTextName == "Throughput")
                                //{
                                //    try
                                //    {
                                //        value = Convert.ToDouble(value).ToString("#####0");
                                //    }
                                //    catch
                                //    {
                                //    }
                                //}

                                if (x.MachineInformationTextValue != value)
                                {
                                    x.MachineInformationTextValue = value;
                                }
                            }
                        }
                        //else if (Dic_Name_Value.ContainsKey(x.MachineInformationTextName))
                        //{
                        //    if (x.MachineInformationTextValue != Dic_Name_Value[x.MachineInformationTextName])
                        //        x.MachineInformationTextValue = Dic_Name_Value[x.MachineInformationTextName];
                        //}
                    }
                    catch (Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                });
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateEventMethods
    }
}