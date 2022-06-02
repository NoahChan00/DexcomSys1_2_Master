using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PentagonHMI.Tags
{
    public class EngineeringPLCTags
    {
        #region PCBA_Robot
        public static Logix.Tag Engr_PCBARobot_JogMode = new Logix.Tag { Name = $"HMI_StationJogMode_PCBARobot", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_PCBARobot_StationCycleMode = new Logix.Tag { Name = $"HMI_StationCycleMode_PCBARobot", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_PCBARobot_StationInitStatus = new Logix.Tag { Name = $"System1_MC_Tag.MachineInitDone", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_PCBARobot_StationStartInit = new Logix.Tag { Name = $"System1_MC_Tag.MachineInit", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_PCBARobot_PickFromTray = new Logix.Tag { Name = $"HMI_PCBA_PickFromTray", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_PCBARobot_PlaceToTurret = new Logix.Tag { Name = $"HMI_PCBA_PlaceToTurret", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_PCBARobot_RowNo = new Logix.Tag { Name = $"HMI_PCBA_RowNo", DataType = Logix.Tag.ATOMIC.INT };
        public static Logix.Tag Engr_PCBARobot_ColumnNo = new Logix.Tag { Name = $"HMI_PCBA_ColumnNo", DataType = Logix.Tag.ATOMIC.INT };
        #endregion

        #region Battery_Robot
        public static Logix.Tag Engr_BtryRobot_JogMode = new Logix.Tag { Name = $"HMI_StationJogMode_BatteryRobot", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_BtryRobot_StationCycleMode = new Logix.Tag { Name = $"HMI_StationCycleMode_PCBARobot", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_BtryRobot_StationInitStatus = new Logix.Tag { Name = $"System1_MC_Tag.MachineInitDone", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_BtryRobot_StationStartInit = new Logix.Tag { Name = $"System1_MC_Tag.MachineInit", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_BtryRobot_PickFromTray = new Logix.Tag { Name = $"HMI_Battery_PickFromTray", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_BtryRobot_PlaceToTurret = new Logix.Tag { Name = $"HMI_Battery_PlaceToTurret", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_BtryRobot_RowNo = new Logix.Tag { Name = $"HMI_Battery_RowNo", DataType = Logix.Tag.ATOMIC.INT };
        public static Logix.Tag Engr_BtryRobot_ColumnNo = new Logix.Tag { Name = $"HMI_Battery_ColumnNo", DataType = Logix.Tag.ATOMIC.INT };
        public static Logix.Tag Engr_BtryRobot_BatteryType = new Logix.Tag { Name = $"HMI_Battery_Type", DataType = Logix.Tag.ATOMIC.DINT };
        #endregion

        #region Unload_Robot
        public static Logix.Tag Engr_UnloadRobot_JogMode = new Logix.Tag { Name = $"HMI_StationJogMode_UnloadRobot", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_UnloadRobot_StationCycleMode = new Logix.Tag { Name = $"HMI_StationCycleMode_UnloadRobot", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_UnloadRobot_StationInitStatus = new Logix.Tag { Name = $"System1_MC_Tag.MachineInitDone", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_UnloadRobot_StationStartInit = new Logix.Tag { Name = $"System1_MC_Tag.MachineInit", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_UnloadRobot_PickFromTurret = new Logix.Tag { Name = $"HMI_Unload_PickFromTurret", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_UnloadRobot_L_PlaceToTurret = new Logix.Tag { Name = $"HMI_Unload_PlaceToLeftShuttle", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_UnloadRobot_R_PlaceToTurret = new Logix.Tag { Name = $"HMI_Unload_PlaceToRightShuttle", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_UnloadRobot_RowNo = new Logix.Tag { Name = $"HMI_Battery_RowNo", DataType = Logix.Tag.ATOMIC.INT };
        public static Logix.Tag Engr_UnloadRobot_ColumnNo = new Logix.Tag { Name = $"HMI_Battery_ColumnNo", DataType = Logix.Tag.ATOMIC.INT };
        #endregion

        #region PCBA_Rack
        public static Logix.Tag Engr_PCBARack_JogMode = new Logix.Tag { Name = $"HMI_StationJogMode_PCBARack", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_PCBARack_StationCycleMode = new Logix.Tag { Name = $"HMI_StationCycleMode_PCBARack", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_PCBARack_StationInitStatus = new Logix.Tag { Name = $"System1_MC_Tag.MachineInitDone", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_PCBARack_StationStartInit = new Logix.Tag { Name = $"System1_MC_Tag.MachineInit", DataType = Logix.Tag.ATOMIC.BOOL };
        #endregion

        #region Battery_Rack
        public static Logix.Tag Engr_BtryRack_JogMode = new Logix.Tag { Name = $"HMI_StationJogMode_BatteryRack", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_BtryRack_StationCycleMode = new Logix.Tag { Name = $"HMI_StationCycleMode_BatteryRack", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_BtryRack_StationInitStatus = new Logix.Tag { Name = $"System1_MC_Tag.MachineInitDone", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_BtryRack_StationStartInit = new Logix.Tag { Name = $"System1_MC_Tag.MachineInit", DataType = Logix.Tag.ATOMIC.BOOL };
        #endregion

        #region Input_Robot
        public static Logix.Tag Engr_InputRobot_JogMode = new Logix.Tag { Name = $"HMI_StationJogMode_InputRobot", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_InputRobot_StationCycleMode = new Logix.Tag { Name = $"HMI_StationCycleMode_InputRobot", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_InputRobot_StationInitStatus = new Logix.Tag { Name = $"System2_MC_Tag.MachineInitDone", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_InputRobot_StationStartInit = new Logix.Tag { Name = $"System2_MC_Tag.MachineInit", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_InputRobot_PickFromLeft = new Logix.Tag { Name = $"HMI_Input_PickFromLShuttle", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_InputRobot_PickFromRight = new Logix.Tag { Name = $"HMI_Input_PickFromRShuttle", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_InputRobot_PlaceToTurret = new Logix.Tag { Name = $"HMI_Input_PlaceToTurret", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_InputRobot_RowNo = new Logix.Tag { Name = $"HMI_Input_RowNo", DataType = Logix.Tag.ATOMIC.INT };
        public static Logix.Tag Engr_InputRobot_ColumnNo = new Logix.Tag { Name = $"HMI_Input_ColumnNo", DataType = Logix.Tag.ATOMIC.INT };
        #endregion

        #region Output_Robot
        public static Logix.Tag Engr_OutputRobot_JogMode = new Logix.Tag { Name = $"HMI_StationJogMode_OutputRobot", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_OutputRobot_StationCycleMode = new Logix.Tag { Name = $"HMI_StationCycleMode_OutputRobot", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_OutputRobot_StationInitStatus = new Logix.Tag { Name = $"System2_MC_Tag.MachineInitDone", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_OutputRobot_StationStartInit = new Logix.Tag { Name = $"System2_MC_Tag.MachineInit", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_OutputRobot_PickFromTurret = new Logix.Tag { Name = $"HMI_Output_PickFromTurret", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_OutputRobot_PlaceToTray = new Logix.Tag { Name = $"HMI_Output_PlaceToTray", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_OutputRobot_RowNo = new Logix.Tag { Name = $"HMI_Output_RowNo", DataType = Logix.Tag.ATOMIC.INT };
        public static Logix.Tag Engr_OutputRobot_ColumnNo = new Logix.Tag { Name = $"HMI_Output_ColumnNo", DataType = Logix.Tag.ATOMIC.INT };
        #endregion

        #region Input_Rack
        public static Logix.Tag Engr_InputRack_JogMode = new Logix.Tag { Name = $"HMI_StationJogMode_InputRack", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_InputRack_StationCycleMode = new Logix.Tag { Name = $"HMI_StationCycleMode_InputRack", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_InputRack_StationInitStatus = new Logix.Tag { Name = $"System2_MC_Tag.MachineInitDone", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_InputRack_StationStartInit = new Logix.Tag { Name = $"System2_MC_Tag.MachineInit", DataType = Logix.Tag.ATOMIC.BOOL };
        #endregion

        #region Output_Rack
        public static Logix.Tag Engr_OutputRack_JogMode = new Logix.Tag { Name = $"HMI_StationJogMode_OutputRack", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_OutputRack_StationCycleMode = new Logix.Tag { Name = $"HMI_StationCycleMode_OutputRack", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_OutputRack_StationInitStatus = new Logix.Tag { Name = $"System2_MC_Tag.MachineInitDone", DataType = Logix.Tag.ATOMIC.BOOL };
        public static Logix.Tag Engr_OutputRack_StationStartInit = new Logix.Tag { Name = $"System2_MC_Tag.MachineInit", DataType = Logix.Tag.ATOMIC.BOOL };
        #endregion

        #region Rotary_Table
        public static Logix.Tag Engr_RotaryTable_TurretIndex = new Logix.Tag { Name = $"HMI_Turret_Index", DataType = Logix.Tag.ATOMIC.BOOL };
        #endregion
    }
}
