using PentagonHMI.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Utilities;
using System.Linq;
using System.Data;
using Logix;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for ZoneView.xaml.
    /// </summary>
    public partial class ZoneView : UserControl
    {
        #region PrivateFields
        private List<ZoneModel> zoneList = new List<ZoneModel>();
        private LogicClasses.Main main = null;
        #endregion

        #region Constructor
        public ZoneView(LogicClasses.Main _main)
        {
            try
            {
                main = _main;
                InitializeComponent();
                initializeZoneList();
                initializeOpc(_main);
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private void initializeZoneList()
        {
            try
            {
                DataTable dtPalletZone = main.SQLer.Exec_DTSelect($"SELECT PalletZoneName, PalletZoneIndex FROM PalletZone WHERE ProjectType = '{GlobalFunctions.ProjectType}' ORDER BY PalletZoneIndex");
                // Generate pallet zone
                main.zoneCount = dtPalletZone.Rows.Count;
                foreach (DataRow dr in dtPalletZone.Rows)
                {
                    ZoneModel zoneModel = new ZoneModel();
                    zoneModel.ZoneName = dr["PalletZoneName"].ToString();
                    int zoneIndex = Convert.ToInt32(dr["PalletZoneIndex"]);

                    DataTable dtPalletZoneStatus = main.SQLer.Exec_DTSelect($"SELECT PalletZoneStatusIndex, PalletZoneStatusName, PalletZoneStatusTagName, PalletZoneStatusTagDataType FROM PalletZoneStatus" +
                                                                            $" WHERE ProjectType = '{GlobalFunctions.ProjectType}' AND PalletZoneIndex = '{zoneIndex}'" +
                                                                            $" ORDER BY PalletZoneStatusIndex ");
                    List<ZoneStatusModel> zoneStatusList = new List<ZoneStatusModel>();
                    //Generate status
                    foreach(DataRow dr1 in dtPalletZoneStatus.Rows)
                    {
                        int statusIndex = Convert.ToInt32(dr1["PalletZoneStatusIndex"]);
                        // Generate Status Description
                        DataTable dtPalletZoneStatusDesc = main.SQLer.Exec_DTSelect($"SELECT PalletZoneStatusIndex, PalletZoneStatusValue, PalletZoneStatusDescription" +
                                                    $" FROM PalletZoneStatusDescription" +
                                                    $" WHERE PROJECTTYPE = '{GlobalFunctions.ProjectType}'" +
                                                    $" AND PalletZoneIndex = '{zoneIndex}'" +
                                                    $" AND PalletZoneStatusIndex = '{statusIndex}'" +
                                                    $" ORDER BY PalletZoneStatusIndex");

                        List<ZoneStatusDescriptionModel> zoneStatusDescriptionList = new List<ZoneStatusDescriptionModel>();
                        foreach (DataRow dr2 in dtPalletZoneStatusDesc.Rows)
                        {
                            zoneStatusDescriptionList.Add(new ZoneStatusDescriptionModel
                            {
                                ZoneStatusIndex = dr2["PalletZoneStatusValue"].ToString(),
                                ZoneStatusDescription = dr2["PalletZoneStatusDescription"].ToString(),
                            });
                        }

                        Tag zoneTag = new Tag();
                        switch (dr1["PalletZoneStatusTagDataType"].ToString())
                        {
                            case "STRING":
                                zoneTag = new Tag(dr1["PalletZoneStatusTagName"].ToString(), Logix.Tag.ATOMIC.STRING);
                                break;
                            case "INT":
                                zoneTag = new Tag(dr1["PalletZoneStatusTagName"].ToString(), Logix.Tag.ATOMIC.INT);
                                break;
                            case "DINT":
                                zoneTag = new Tag(dr1["PalletZoneStatusTagName"].ToString(), Logix.Tag.ATOMIC.DINT);
                                break;
                            case "REAL":
                                zoneTag = new Tag(dr1["PalletZoneStatusTagName"].ToString(), Logix.Tag.ATOMIC.REAL);
                                break;
                        }

                        zoneStatusList.Add(new ZoneStatusModel
                        {
                            ZoneStatusName = dr1["PalletZoneStatusName"].ToString(),
                            ZoneStatusDescriptionList = zoneStatusDescriptionList,
                            ZoneStatusTag = zoneTag,
                        });
                    }

                    // Generate Action
                    DataTable dtPalletZoneAction = main.SQLer.Exec_DTSelect($"SELECT PalletZoneActionName, PalletZoneActionTagName, PalletZoneActionEnable1TagName, PalletZoneActionEnable2TagName FROM PalletZoneAction" +
                                                                            $" WHERE ProjectType = '{GlobalFunctions.ProjectType}' AND PalletZoneIndex = '{zoneIndex}'" +
                                                                            $" ORDER BY PalletZoneActionIndex ");
                    
                    List<ZoneActionModel> zoneActionList = new List<ZoneActionModel>();
                    foreach (DataRow dr1 in dtPalletZoneAction.Rows)
                    {

                        zoneActionList.Add(new ZoneActionModel
                        {
                            ZoneActionName = dr1["PalletZoneActionName"].ToString(),
                            ZoneActionTag = new Tag(dr1["PalletZoneActionTagName"].ToString(), Logix.Tag.ATOMIC.BOOL),
                            ZoneActionEnable1Tag = new Tag(dr1["PalletZoneActionEnable1TagName"].ToString(), Logix.Tag.ATOMIC.BOOL),
                            ZoneActionEnable2Tag = new Tag(dr1["PalletZoneActionEnable2TagName"].ToString(), Logix.Tag.ATOMIC.BOOL),
                        });
                    }

                    zoneModel.ZoneActionList = zoneActionList;
                    zoneModel.ZoneStatusList = zoneStatusList;
                    zoneList.Add(zoneModel);
                }

                #region Comment

                //zoneList = new List<ZoneModel>
                //{
                //    new ZoneModel
                //    {
                //        ZoneStatusList = new List<ZoneStatusModel>
                //        {
                //            new ZoneStatusModel
                //            {
                //                ZoneStatusName = "Asset Tag",
                //                ZoneStatusTag = new Logix.Tag
                //                {
                //                    Name = "Zone_AssetTagZ1",
                //                    DataType = Logix.Tag.ATOMIC.STRING
                //                }
                //            },
                //            new ZoneStatusModel
                //            {
                //                ZoneStatusName = "Status",
                //                ZoneStatusTag = new Logix.Tag
                //                {
                //                    DataType = Logix.Tag.ATOMIC.INT
                //                }
                //            },
                //            new ZoneStatusModel
                //            {
                //                ZoneStatusName = "Part #1 Model",
                //                ZoneStatusTag = new Logix.Tag
                //                {
                //                    Name = "HMI_DisplayZ1_Part1_Model",
                //                    DataType = Logix.Tag.ATOMIC.STRING
                //                }
                //            },
                //            new ZoneStatusModel
                //            {
                //                ZoneStatusName = "Part #2 Model",
                //                ZoneStatusTag = new Logix.Tag
                //                {
                //                    Name = "HMI_DisplayZ1_Part2_Model",
                //                    DataType = Logix.Tag.ATOMIC.STRING
                //                }
                //            },
                //            new ZoneStatusModel
                //            {
                //                ZoneStatusName = "Last Reject Reason",
                //                ZoneStatusTag = new Logix.Tag
                //                {
                //                    Name = "HMI_Zone1_LastRejectReason",
                //                    DataType = Logix.Tag.ATOMIC.STRING
                //                }
                //            }
                //        },
                //        ZoneActionList = new List<ZoneActionModel>
                //        {
                //            new ZoneActionModel
                //            {
                //                ZoneActionName = "Abort Pallet",
                //                ZoneActionTag = new Logix.Tag
                //                {
                //                    Name = "General_Abort_Z1",
                //                    DataType = Logix.Tag.ATOMIC.BOOL
                //                },
                //                ZoneActionEnable1Tag = new Logix.Tag
                //                {
                //                    Name = "MC_System_Tags.MachineRunning",
                //                    DataType = Logix.Tag.ATOMIC.BOOL
                //                },
                //                ZoneActionEnable2Tag = new Logix.Tag
                //                {
                //                    Name = "MC_System_Tags.MachineInitDone",
                //                    DataType = Logix.Tag.ATOMIC.BOOL
                //                }
                //            }
                //        }
                //    },
                //    new ZoneModel
                //    {
                //        ZoneStatusList = new List<ZoneStatusModel>
                //        {
                //            new ZoneStatusModel
                //            {
                //                ZoneStatusName = "Asset Tag",
                //                ZoneStatusTag = new Logix.Tag
                //                {
                //                    Name = "Zone_AssetTagZ2",
                //                    DataType = Logix.Tag.ATOMIC.STRING
                //                }
                //            },
                //            new ZoneStatusModel
                //            {
                //                ZoneStatusName = "Status",
                //                ZoneStatusTag = new Logix.Tag
                //                {
                //                    DataType = Logix.Tag.ATOMIC.INT
                //                }
                //            },
                //            new ZoneStatusModel
                //            {
                //                ZoneStatusName = "Part #1 Model",
                //                ZoneStatusTag = new Logix.Tag
                //                {
                //                    Name = "HMI_DisplayZ2_Part1_Model",
                //                    DataType = Logix.Tag.ATOMIC.STRING
                //                }
                //            },
                //            new ZoneStatusModel
                //            {
                //                ZoneStatusName = "Part #2 Model",
                //                ZoneStatusTag = new Logix.Tag
                //                {
                //                    Name = "HMI_DisplayZ2_Part2_Model",
                //                    DataType = Logix.Tag.ATOMIC.STRING
                //                }
                //            },
                //            new ZoneStatusModel
                //            {
                //                ZoneStatusName = "Last Reject Reason",
                //                ZoneStatusTag = new Logix.Tag
                //                {
                //                    Name = "HMI_Zone2_LastRejectReason",
                //                    DataType = Logix.Tag.ATOMIC.STRING
                //                }
                //            }
                //        },
                //        ZoneActionList = new List<ZoneActionModel>
                //        {
                //            new ZoneActionModel
                //            {
                //                ZoneActionName = "Abort Pallet",
                //                ZoneActionTag = new Logix.Tag
                //                {
                //                    Name = "General_Abort_Z2",
                //                    DataType = Logix.Tag.ATOMIC.BOOL
                //                },
                //                ZoneActionEnable1Tag = new Logix.Tag
                //                {
                //                    Name = "MC_System_Tags.MachineRunning",
                //                    DataType = Logix.Tag.ATOMIC.BOOL
                //                },
                //                ZoneActionEnable2Tag = new Logix.Tag
                //                {
                //                    Name = "MC_System_Tags.MachineInitDone",
                //                    DataType = Logix.Tag.ATOMIC.BOOL
                //                }
                //            }
                //        }
                //    }
                //};



                //if (ProjectType.HDD == GlobalFunctions.ProjectType)
                //{
                //    string[] Ary_NoNeed = new string[] { "ASSET TAG", "PART #1 MODEL", "PART #2 MODEL" };
                //    foreach (var _zonelist in zoneList)
                //    {
                //        _zonelist.ZoneActionList.Clear();
                //    Check:
                //        var item = _zonelist.ZoneStatusList.Where(x => Ary_NoNeed.Contains(x.ZoneStatusName.ToUpper())).FirstOrDefault();
                //        if (item != null)
                //        {
                //            _zonelist.ZoneStatusList.Remove(item);
                //            goto Check;
                //        }
                //    }

                //    int n = 2;
                //    foreach (var zonelist in zoneList)
                //    {
                //        ZoneStatusModel item = new ZoneStatusModel
                //        {
                //            ZoneStatusName = "HDD Tag",
                //            ZoneStatusTag = new Logix.Tag
                //            {
                //                Name = $"Zone_HDDTagZ{n--}",
                //                DataType = Logix.Tag.ATOMIC.STRING
                //            }
                //        };
                //        zonelist.ZoneStatusList.Insert(0, item);
                //    }
                //}

                //string zone1Name = string.Empty;
                //string zone2Name = string.Empty;
                //switch (GlobalFunctions.ProjectType)
                //{
                //    default:
                //    case ProjectType.TLA:
                //    case ProjectType.ARCADIA:
                //    case ProjectType.DIMM:
                //        zone1Name = "Zone #1";
                //        zone2Name = "Zone #2";
                //        break;
                //    case ProjectType.HDD:
                //        zone1Name = "Shuttle #2 (Loading Module)";
                //        zone2Name = "Shuttle #1 (Unloading Module)";
                //        break;
                //}
                //zoneList[0].ZoneName = zone1Name;
                //zoneList[1].ZoneName = zone2Name;

                //string zone1StatusTagName = string.Empty;
                //string zone2StatusTagName = string.Empty;
                //List<ZoneStatusDescriptionModel> zoneStatusDescriptionList = new List<ZoneStatusDescriptionModel>();

                //switch (GlobalFunctions.ProjectType)
                //{
                //    case ProjectType.ARCADIA:
                //        zone1StatusTagName = "ArcdCell_Zone_Z1PalletStatus";
                //        zone2StatusTagName = "ArcdCell_Zone_Z2PalletStatus";
                //        zoneStatusDescriptionList = new List<ZoneStatusDescriptionModel>
                //        {
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "0",
                //                ZoneStatusDescription = "Zone Empty"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "1",
                //                ZoneStatusDescription = "Pallet ready"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "2",
                //                ZoneStatusDescription = "Place MOBO in progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "3",
                //                ZoneStatusDescription = "Place MOBO done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "4",
                //                ZoneStatusDescription = "Robot 1 vacuum off"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "5",
                //                ZoneStatusDescription = "Lock MOBO in progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "6",
                //                ZoneStatusDescription = "Lock MOBO done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "7",
                //                ZoneStatusDescription = "Screw 1 vision in progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "8",
                //                ZoneStatusDescription = "Screw 1 vision done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "9",
                //                ZoneStatusDescription = "Screw 1 tightening in progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "10",
                //                ZoneStatusDescription = "Screw 1 tightening done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "11",
                //                ZoneStatusDescription = "Screw 2 vision in progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "12",
                //                ZoneStatusDescription = "Screw 2 vision done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "13",
                //                ZoneStatusDescription = "Screw 2 tightening in progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "14",
                //                ZoneStatusDescription = "Screw 2 tightening done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "15",
                //                ZoneStatusDescription = "Assembly done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "16",
                //                ZoneStatusDescription = "Pallet result process done"
                //            }
                //        };
                //        break;
                //    case ProjectType.DIMM:
                //        zone1StatusTagName = "DIMM_Zone_Z1PalletStatus";
                //        zone2StatusTagName = "DIMM_Zone_Z2PalletStatus";
                //        zoneStatusDescriptionList = new List<ZoneStatusDescriptionModel>
                //        {
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "0",
                //                ZoneStatusDescription = "Zone Empty"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "1",
                //                ZoneStatusDescription = "Pre-Insert RAM in Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "6",
                //                ZoneStatusDescription = "Group 1,2,3,4 Installed"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "7",
                //                ZoneStatusDescription = "Group 1 Odd Number Slots Pressed"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "8",
                //                ZoneStatusDescription = "Group 1 Even Number Slots Pressed"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "9",
                //                ZoneStatusDescription = "Group 1 Absence/Presence Vision Done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "10",
                //                ZoneStatusDescription = "Group 2 Odd Number Slots Pressed"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "11",
                //                ZoneStatusDescription = "Group 2 Even Number Slots Pressed"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "12",
                //                ZoneStatusDescription = "Group 2 Absence/Presence Vision Done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "13",
                //                ZoneStatusDescription = "Group 3 Odd Number Slots Pressed"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "14",
                //                ZoneStatusDescription = "Group 3 Even Number Slots Pressed"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "15",
                //                ZoneStatusDescription = "Group 3 Absence/Presence Vision Done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "16",
                //                ZoneStatusDescription = "Group 4 Odd Number Slots Pressed"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "17",
                //                ZoneStatusDescription = "Group 4 Even Number Slots Pressed"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "18",
                //                ZoneStatusDescription = "Group 4 Absence/Presence Vision Done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "29",
                //                ZoneStatusDescription = "Group 1 Alignment Vision Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "39",
                //                ZoneStatusDescription = "Group 2 Alignment Vision Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "49",
                //                ZoneStatusDescription = "Group 3 Alignment Vision Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "59",
                //                ZoneStatusDescription = "Group 4 Alignment Vision Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "79",
                //                ZoneStatusDescription = "Group 1 Odd Number Slots Press Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "89",
                //                ZoneStatusDescription = "Group 1 Even Number Slots Press Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "99",
                //                ZoneStatusDescription = "Group 1 Absence/Presence Vision Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "109",
                //                ZoneStatusDescription = "Group 2 Odd Number Slots Press Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "119",
                //                ZoneStatusDescription = "Group 2 Even Number Slots Press Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "129",
                //                ZoneStatusDescription = "Group 2 Absence/Presence Vision Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "139",
                //                ZoneStatusDescription = "Group 3 Odd Number Slots Press Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "149",
                //                ZoneStatusDescription = "Group 3 Even Number Slots Press Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "159",
                //                ZoneStatusDescription = "Group 3 Absence/Presence Vision Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "169",
                //                ZoneStatusDescription = "Group 4 Odd Number Slots Press Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "179",
                //                ZoneStatusDescription = "Group 4 Even Number Slots Press Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "189",
                //                ZoneStatusDescription = "Group 4 Absence/Presence Vision Fail"
                //            }
                //            ,
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "999",
                //                ZoneStatusDescription = "Server Tray Aborted"
                //            }
                //            ,
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "1000",
                //                ZoneStatusDescription = "Update LM"
                //            }
                //        };
                //        break;
                //    case ProjectType.HDD:
                //        zone2StatusTagName = "HDDP1_DataTracking_Shuttle1_Status";
                //        zone1StatusTagName = "HDDP1_DataTracking_Shuttle2_Status";
                //        zoneStatusDescriptionList = new List<ZoneStatusDescriptionModel>
                //        {
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "0",
                //                ZoneStatusDescription = "Zone Empty"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "1",
                //                ZoneStatusDescription = "Inspect Top Lid Screw"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "5",
                //                ZoneStatusDescription = "Inspect Done"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "10",
                //                ZoneStatusDescription = "Remove Screw 1 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "11",
                //                ZoneStatusDescription = "Done Remove Screw 1"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "15",
                //                ZoneStatusDescription = "Remove Screw 2 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "16",
                //                ZoneStatusDescription = "Done Remove Screw 2"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "20",
                //                ZoneStatusDescription = "Remove Screw 3 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "21",
                //                ZoneStatusDescription = "Done Remove Screw 3"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "25",
                //                ZoneStatusDescription = "Remove Screw 4 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "26",
                //                ZoneStatusDescription = "Done Remove Screw 4"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "30",
                //                ZoneStatusDescription = "Remove Screw 5 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "31",
                //                ZoneStatusDescription = "Done Remove Screw 5"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "35",
                //                ZoneStatusDescription = "Remove Screw 6 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "36",
                //                ZoneStatusDescription = "Done Remove Screw 6"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "40",
                //                ZoneStatusDescription = "Remove Screw 7 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "41",
                //                ZoneStatusDescription = "Done Remove Screw 7"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "45",
                //                ZoneStatusDescription = "Remove Screw 8 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "46",
                //                ZoneStatusDescription = "Done Remove Screw 8"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "50",
                //                ZoneStatusDescription = "Remove Screw 9 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "51",
                //                ZoneStatusDescription = "Done Remove Screw 9"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "55",
                //                ZoneStatusDescription = "Remove Screw 10 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "56",
                //                ZoneStatusDescription = "Done Remove Screw 10"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "60",
                //                ZoneStatusDescription = "Remove Screw 11 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "61",
                //                ZoneStatusDescription = "Done Remove Screw 11"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "65",
                //                ZoneStatusDescription = "Remove Screw 12 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "66",
                //                ZoneStatusDescription = "Done Remove Screw 12"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "70",
                //                ZoneStatusDescription = "Remove Top Lid In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "71",
                //                ZoneStatusDescription = "Done Remove Top Lid"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "72",
                //                ZoneStatusDescription = "Remove Silica Bag In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "73",
                //                ZoneStatusDescription = "Done Remove Silica Bag"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "75",
                //                ZoneStatusDescription = "Remove ECM In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "76",
                //                ZoneStatusDescription = "Done Remove ECM"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "85",
                //                ZoneStatusDescription = "Inspect Top Magnet, ECM & Disk Cosmetic"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "86",
                //                ZoneStatusDescription = "Done Inspect Top Magnet, ECM &  Disk Cosmetic"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "90",
                //                ZoneStatusDescription = "Remove Top Magnet Screw In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "91",
                //                ZoneStatusDescription = "Done Remove Top Magnet Screw"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "95",
                //                ZoneStatusDescription = "Remove HSA Screw 1 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "96",
                //                ZoneStatusDescription = "Done Remove HSA Screw 1"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "100",
                //                ZoneStatusDescription = "Remove HSA Screw 2 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "101",
                //                ZoneStatusDescription = "Done Remove HSA Screw 2"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "105",
                //                ZoneStatusDescription = "Remove Top Magnet In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "106",
                //                ZoneStatusDescription = "Done Remove Top Magnet"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "110",
                //                ZoneStatusDescription = "Remove HSA Part In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "111",
                //                ZoneStatusDescription = "Done Remove HSA Part"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "115",
                //                ZoneStatusDescription = "Inspect Bottom Magnet Screw In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "116",
                //                ZoneStatusDescription = "Done Inspect Bottom Magnet Screw"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "120",
                //                ZoneStatusDescription = "Remove Bottom Magnet Screw In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "121",
                //                ZoneStatusDescription = "Done Remove Bottom Magnet Screw"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "125",
                //                ZoneStatusDescription = "Remove Bottom Magnet Screw 2 In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "126",
                //                ZoneStatusDescription = "Done Remove Bottom Magnet Screw 2"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "130",
                //                ZoneStatusDescription = "Remove Bottom Magnet Part In Progress"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "131",
                //                ZoneStatusDescription = "Done Remove Bottom Magnet Part"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "509",
                //                ZoneStatusDescription = "Inspect Top Lid Screw Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "999",
                //                ZoneStatusDescription = "Unload The HDD At Shuttle 1 or 2"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "1009",
                //                ZoneStatusDescription = "Remove Screw 1 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "1509",
                //                ZoneStatusDescription = "Remove Screw 2 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "2009",
                //                ZoneStatusDescription = "Remove Screw 3 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "2509",
                //                ZoneStatusDescription = "Remove Screw 4 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "3009",
                //                ZoneStatusDescription = "Remove Screw 5 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "3509",
                //                ZoneStatusDescription = "Remove Screw 6 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "4009",
                //                ZoneStatusDescription = "Remove Screw 7 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "4509",
                //                ZoneStatusDescription = "Remove Screw 8 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "5009",
                //                ZoneStatusDescription = "Remove Screw 9 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "5509",
                //                ZoneStatusDescription = "Remove Screw 10 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "6009",
                //                ZoneStatusDescription = "Remove Screw 11 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "6509",
                //                ZoneStatusDescription = "Remove Screw 12 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "7009",
                //                ZoneStatusDescription = "Remove Top Lid Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "7209",
                //                ZoneStatusDescription = "Remove Silica Bag Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "7509",
                //                ZoneStatusDescription = "Remove ECM Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "8409",
                //                ZoneStatusDescription = "Inspect Disk Cosmetic Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "8509",
                //                ZoneStatusDescription = "Inspect Top Magnet, ECM Screw Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "9009",
                //                ZoneStatusDescription = "Remove Top Magnet Screw Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "9509",
                //                ZoneStatusDescription = "Remove HSA Screw 1 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "10009",
                //                ZoneStatusDescription = "Remove HSA Screw 2 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "10509",
                //                ZoneStatusDescription = "Remove Top Magnet Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "11009",
                //                ZoneStatusDescription = "Remove HSA Part Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "11509",
                //                ZoneStatusDescription = "Remove HSA Part Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "12009",
                //                ZoneStatusDescription = "Remove Bottom Magnet Screw 1 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "12509",
                //                ZoneStatusDescription = "Remove Bottom Magnet Screw 2 Fail"
                //            },
                //            new ZoneStatusDescriptionModel
                //            {
                //                ZoneStatusIndex = "13009",
                //                ZoneStatusDescription = "Remove Bottom Magnet Part Fail"
                //            },
                //        };
                //        break;
                //    default:
                //        break;
                //}

                //if (!string.IsNullOrEmpty(zone1StatusTagName) && !string.IsNullOrEmpty(zone2StatusTagName))
                //{
                //    var item = zoneList[0].ZoneStatusList.Where(x => x.ZoneStatusName.ToUpper() == "STATUS").FirstOrDefault();
                //    if (item != null)
                //    {
                //        item.ZoneStatusTag.Name = zone1StatusTagName;
                //        item.ZoneStatusDescriptionList = zoneStatusDescriptionList;
                //    };
                //    item = zoneList[1].ZoneStatusList.Where(x => x.ZoneStatusName.ToUpper() == "STATUS").FirstOrDefault();
                //    if (item != null)
                //    {
                //        item.ZoneStatusTag.Name = zone2StatusTagName;
                //        item.ZoneStatusDescriptionList = zoneStatusDescriptionList;
                //    };
                //}
                #endregion

                ZoneItemsControl.ItemsSource = zoneList;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeOpc(LogicClasses.Main _main)
        {
            try
            {
                main = _main;
                main.Home_OnUpdate += main_Home_OnUpdate;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateEventMethods
        private void zoneActionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                ZoneActionModel zoneActionModel = button.Tag as ZoneActionModel;

                if (zoneList.Exists(x => x.ZoneActionList.Exists(y => y == zoneActionModel)))
                {
                    ZoneModel zoneModel = zoneList.Find(x => x.ZoneActionList.Exists(y => y == zoneActionModel));

                    if (MessageBox.Show($"Are you sure you want to {zoneActionModel.ZoneActionName}" +
                        $" ({zoneModel.ZoneName})?", nameof(MessageBoxImage.Question),
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No,
                        MessageBoxOptions.DefaultDesktopOnly) == MessageBoxResult.Yes)
                    {
                        zoneActionModel.ZoneActionTag.Value = true;

                        main.MyPLC.WriteTag(zoneActionModel.ZoneActionTag);
                    }
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void main_Home_OnUpdate()
        {
            try
            {
                foreach (var x in zoneList)
                {
                    try
                    {
                        foreach (var y in x.ZoneStatusList)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(y.ZoneStatusTag.Name))
                                {
                                    main.MyPLC.ReadTag(y.ZoneStatusTag);

                                    if (y.ZoneStatusTag.Value != null)
                                    {
                                        if (y.ZoneStatusName == "Last Reject Reason")
                                        {
                                            if (main.Dic_Reject_Index_Reason.TryGetValue(Convert.ToInt32(y.ZoneStatusTag.Value), out string Reason))
                                            {
                                                if (y.ZoneStatus != Reason)
                                                {
                                                    y.ZoneStatus = Reason;
                                                }
                                            }
                                            else
                                            {
                                                string value = y.ZoneStatusTag.Value.ToString();

                                                if (y.ZoneStatus != value)
                                                {
                                                    y.ZoneStatus = value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            string value = y.ZoneStatusDescriptionList.Exists(
                                                z => z.ZoneStatusIndex == y.ZoneStatusTag.Value.ToString()) ?
                                                y.ZoneStatusDescriptionList.Find(z => z.ZoneStatusIndex ==
                                                y.ZoneStatusTag.Value.ToString()).ZoneStatusDescription :
                                                y.ZoneStatusTag.Value.ToString();

                                            if (y.ZoneStatus != value)
                                            {
                                                y.ZoneStatus = value;
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                FileLogger.logError(exception.Message, exception.ToString());
                            }
                        }

                        foreach (var y in x.ZoneActionList)
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(y.ZoneActionEnable1Tag.Name) &&
                                !string.IsNullOrEmpty(y.ZoneActionEnable2Tag.Name))
                                {
                                    main.MyPLC.ReadTag(y.ZoneActionEnable1Tag);
                                    main.MyPLC.ReadTag(y.ZoneActionEnable2Tag);

                                    if (y.ZoneActionEnable1Tag.Value != null &&
                                    y.ZoneActionEnable2Tag.Value != null)
                                    {
                                        bool value = !Convert.ToBoolean(
                                            y.ZoneActionEnable1Tag.Value) && Convert.ToBoolean(
                                                y.ZoneActionEnable2Tag.Value);

                                        if (y.ZoneActionEnable != value)
                                        {
                                            y.ZoneActionEnable = value;
                                        }
                                    }
                                }
                            }
                            catch (Exception exception)
                            {
                                FileLogger.logError(exception.Message, exception.ToString());
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion
    }
}