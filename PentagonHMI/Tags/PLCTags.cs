using Logix;
using PentagonHMI.Classes;

namespace PentagonHMI.Tags
{
    public static class MainPage
    {
        public static string prefix = GlobalFunctions.IsSystem1 ? "System1_" : "System2_";

        public static Tag MachineStatus = new Tag(prefix + "MachineStatus.str_MachineStatus", Tag.ATOMIC.STRING);

        // Some of the Tag Name read from Database, Table MachineInfo
        public static Tag EngineeringMode = new Tag(prefix + "MC_Tag.EngineeringMode", Tag.ATOMIC.BOOL);
        public static Tag StationJogMode = new Tag(prefix + "MC_Tag.StationJogMode", Tag.ATOMIC.BOOL);
        public static Tag StationCycleMode = new Tag(prefix + "MC_Tag.StationCycleMode", Tag.ATOMIC.BOOL);
        public static Tag str_MachineSpeed = new Tag(prefix + "MC_Tag.str_MachineSpeed", Tag.ATOMIC.STRING);
        public static Tag dint_ProductionUPH = new Tag("Lot_OEE_Tags.dint_ProductionUPH", Tag.ATOMIC.DINT);
        public static Tag dint_SprintUPH = new Tag("Lot_OEE_Tags.dint_SprintUPH", Tag.ATOMIC.DINT);
        public static Tag dint_TaktTime = new Tag("Lot_OEE_Tags.dint_TaktTime", Tag.ATOMIC.DINT);


        public static Tag StartButton = new Tag("HMI_Tags.StartButton", Tag.ATOMIC.BOOL);
        public static Tag StopButton = new Tag("HMI_Tags.StopButton", Tag.ATOMIC.BOOL);
        public static Tag ResetButton = new Tag("HMI_Tags.ResetButton", Tag.ATOMIC.BOOL);
        public static Tag MachineInit = new Tag(prefix + "MC_Tag.MachineInit", Tag.ATOMIC.BOOL);
        //public static Tag EngineeringMode = new Tag("", Tag.ATOMIC.BOOL); Refer to above


        public static Tag HMI_End_Lot_Bit = new Tag("Lot_Info.HMI_End_Lot_Bit", Tag.ATOMIC.BOOL);
        public static Tag HMI_Purge_Lot_Bit = new Tag("Lot_Info.HMI_Purge_Lot_Bit", Tag.ATOMIC.BOOL);

        public static Tag HMI_Pause_Lot_Prompt = new Tag("Lot_Info.HMI_Pause_Lot_Prompt", Tag.ATOMIC.BOOL);

        public static Tag dint_Quality = new Tag("Lot_OEE_Tags.dint_Quality", Tag.ATOMIC.DINT); // Data Type not stated int HMI List
        // System1
        public static Tag PickFailPromptWindowPCBA = new Tag("b_PCBATray_Error", Tag.ATOMIC.BOOL);
        // Not require
        public static Tag PickFailPromptWindowBattery= new Tag("b_BatteryTray_Error", Tag.ATOMIC.BOOL);
        // System2
        public static Tag PickFailPromptWindowRShuttle = new Tag("b_RShuttle_Error", Tag.ATOMIC.BOOL);
        public static Tag PickFailPromptWindowLShuttle = new Tag("b_LShuttle_Error", Tag.ATOMIC.BOOL);

        public static Tag PickFailSkipPick = new Tag("b_HMI_AckSkipPick", Tag.ATOMIC.BOOL);
        public static Tag PickFailRetryPick = new Tag("b_HMI_AckRetryPick", Tag.ATOMIC.BOOL);

        public static Tag LotID = new Tag("Lot_Info.HMI_LotID", Tag.ATOMIC.STRING);
    }
    public class EngineeringPLCTags
    {
        #region PCBA_Robot
        public static Tag Engr_PCBARobot_JogMode = new Tag { Name = $"HMI_StationJogMode_PCBARobot", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_PCBARobot_StationCycleMode = new Tag { Name = $"HMI_StationCycleMode_PCBARobot", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_PCBARobot_StationInitStatus = new Tag { Name = $"System1_MC_Tag.MachineInitDone", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_PCBARobot_StationStartInit = new Tag { Name = $"System1_MC_Tag.MachineInit", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_PCBARobot_PickFromTray = new Tag { Name = $"HMI_PCBA_PickFromTray", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_PCBARobot_PlaceToTurret = new Tag { Name = $"HMI_PCBA_PlaceToTurret", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_PCBARobot_RowNo = new Tag { Name = $"HMI_PCBA_RowNo", DataType = Tag.ATOMIC.INT };
        public static Tag Engr_PCBARobot_ColumnNo = new Tag { Name = $"HMI_PCBA_ColumnNo", DataType = Tag.ATOMIC.INT };

        #endregion

        #region Battery_Robot
        public static Tag Engr_BtryRobot_JogMode = new Tag { Name = $"HMI_StationJogMode_BatteryRobot", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_BtryRobot_StationCycleMode = new Tag { Name = $"HMI_StationCycleMode_PCBARobot", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_BtryRobot_StationInitStatus = new Tag { Name = $"System1_MC_Tag.MachineInitDone", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_BtryRobot_StationStartInit = new Tag { Name = $"System1_MC_Tag.MachineInit", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_BtryRobot_PickFromTray = new Tag { Name = $"HMI_Battery_PickFromTray", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_BtryRobot_PlaceToTurret = new Tag { Name = $"HMI_Battery_PlaceToTurret", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_BtryRobot_RowNo = new Tag { Name = $"HMI_Battery_RowNo", DataType = Tag.ATOMIC.INT };
        public static Tag Engr_BtryRobot_ColumnNo = new Tag { Name = $"HMI_Battery_ColumnNo", DataType = Tag.ATOMIC.INT };
        public static Tag Engr_BtryRobot_BatteryType = new Tag { Name = $"HMI_Battery_Type", DataType = Tag.ATOMIC.DINT };
        #endregion

        #region Unload_Robot
        public static Tag Engr_UnloadRobot_JogMode = new Tag { Name = $"HMI_StationJogMode_UnloadRobot", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_UnloadRobot_StationCycleMode = new Tag { Name = $"HMI_StationCycleMode_UnloadRobot", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_UnloadRobot_StationInitStatus = new Tag { Name = $"System1_MC_Tag.MachineInitDone", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_UnloadRobot_StationStartInit = new Tag { Name = $"System1_MC_Tag.MachineInit", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_UnloadRobot_PickFromTurret = new Tag { Name = $"HMI_Unload_PickFromTurret", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_UnloadRobot_L_PlaceToTurret = new Tag { Name = $"HMI_Unload_PlaceToLeftShuttle", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_UnloadRobot_R_PlaceToTurret = new Tag { Name = $"HMI_Unload_PlaceToRightShuttle", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_UnloadRobot_RowNo = new Tag { Name = $"HMI_Battery_RowNo", DataType = Tag.ATOMIC.INT };
        public static Tag Engr_UnloadRobot_ColumnNo = new Tag { Name = $"HMI_Battery_ColumnNo", DataType = Tag.ATOMIC.INT };
        #endregion

        #region PCBA_Rack
        public static Tag Engr_PCBARack_JogMode = new Tag { Name = $"HMI_StationJogMode_PCBARack", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_PCBARack_StationCycleMode = new Tag { Name = $"HMI_StationCycleMode_PCBARack", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_PCBARack_StationInitStatus = new Tag { Name = $"System1_MC_Tag.MachineInitDone", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_PCBARack_StationStartInit = new Tag { Name = $"System1_MC_Tag.MachineInit", DataType = Tag.ATOMIC.BOOL };
        #endregion

        #region Battery_Rack
        public static Tag Engr_BtryRack_JogMode = new Tag { Name = $"HMI_StationJogMode_BatteryRack", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_BtryRack_StationCycleMode = new Tag { Name = $"HMI_StationCycleMode_BatteryRack", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_BtryRack_StationInitStatus = new Tag { Name = $"System1_MC_Tag.MachineInitDone", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_BtryRack_StationStartInit = new Tag { Name = $"System1_MC_Tag.MachineInit", DataType = Tag.ATOMIC.BOOL };
        #endregion

        #region Input_Robot
        public static Tag Engr_InputRobot_JogMode = new Tag { Name = $"HMI_StationJogMode_InputRobot", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_InputRobot_StationCycleMode = new Tag { Name = $"HMI_StationCycleMode_InputRobot", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_InputRobot_StationInitStatus = new Tag { Name = $"System2_MC_Tag.MachineInitDone", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_InputRobot_StationStartInit = new Tag { Name = $"System2_MC_Tag.MachineInit", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_InputRobot_PickFromLeft = new Tag { Name = $"HMI_Input_PickFromLShuttle", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_InputRobot_PickFromRight = new Tag { Name = $"HMI_Input_PickFromRShuttle", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_InputRobot_PlaceToTurret = new Tag { Name = $"HMI_Input_PlaceToTurret", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_InputRobot_RowNo = new Tag { Name = $"HMI_Input_RowNo", DataType = Tag.ATOMIC.INT };
        public static Tag Engr_InputRobot_ColumnNo = new Tag { Name = $"HMI_Input_ColumnNo", DataType = Tag.ATOMIC.INT };
        #endregion

        #region Output_Robot
        public static Tag Engr_OutputRobot_JogMode = new Tag { Name = $"HMI_StationJogMode_OutputRobot", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_OutputRobot_StationCycleMode = new Tag { Name = $"HMI_StationCycleMode_OutputRobot", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_OutputRobot_StationInitStatus = new Tag { Name = $"System2_MC_Tag.MachineInitDone", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_OutputRobot_StationStartInit = new Tag { Name = $"System2_MC_Tag.MachineInit", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_OutputRobot_PickFromTurret = new Tag { Name = $"HMI_Output_PickFromTurret", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_OutputRobot_PlaceToTray = new Tag { Name = $"HMI_Output_PlaceToTray", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_OutputRobot_RowNo = new Tag { Name = $"HMI_Output_RowNo", DataType = Tag.ATOMIC.INT };
        public static Tag Engr_OutputRobot_ColumnNo = new Tag { Name = $"HMI_Output_ColumnNo", DataType = Tag.ATOMIC.INT };
        #endregion

        #region Input_Rack
        public static Tag Engr_InputRack_JogMode = new Tag { Name = $"HMI_StationJogMode_InputRack", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_InputRack_StationCycleMode = new Tag { Name = $"HMI_StationCycleMode_InputRack", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_InputRack_StationInitStatus = new Tag { Name = $"System2_MC_Tag.MachineInitDone", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_InputRack_StationStartInit = new Tag { Name = $"System2_MC_Tag.MachineInit", DataType = Tag.ATOMIC.BOOL };
        #endregion

        #region Output_Rack
        public static Tag Engr_OutputRack_JogMode = new Tag { Name = $"HMI_StationJogMode_OutputRack", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_OutputRack_StationCycleMode = new Tag { Name = $"HMI_StationCycleMode_OutputRack", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_OutputRack_StationInitStatus = new Tag { Name = $"System2_MC_Tag.MachineInitDone", DataType = Tag.ATOMIC.BOOL };
        public static Tag Engr_OutputRack_StationStartInit = new Tag { Name = $"System2_MC_Tag.MachineInit", DataType = Tag.ATOMIC.BOOL };
        #endregion

        #region Rotary_Table
        public static Tag Engr_RotaryTable_TurretIndex = new Tag { Name = $"HMI_Turret_Index", DataType = Tag.ATOMIC.BOOL };
        #endregion
    }
}
