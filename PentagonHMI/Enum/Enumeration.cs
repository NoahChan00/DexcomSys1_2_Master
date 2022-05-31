using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PentagonHMI.Enum
{
    public class Enumeration
    {
        public enum MsgFileType
        {
            Alert,
            Warning,
            Version
        }

        public enum Recipe
        {
            J33,
            J34,
            J34N_1
        }

        public enum PageEnvironment
        {
            New = 1,
            Edit = 2
        }

        public enum IOSignal
        {
            gl_True =1,
            gl_False =2
        }

        public enum Active
        {
            Yes = 1,
            No = 0 
        }

        public enum ModuleType
        {
            Singulator,FNB,UPC,Sorting
        }

        public enum RobotType
        {
            Pick,PickBackup,Vision,Standby,Reject
        }
        public enum StepperType
        {
            TicMark,OQC,Barcode,Reject1,Reject2,Conveyor
        }

        public enum ServoType
        {
            Conveyor
        }

        public enum CameraType
        {
            Barcode,TicMark,OQC
        }

        public enum EngineeringTabs
        {
            PCBARobot,
            BatteryRobot,
            UnloadRobot,
            PCBARack,
            BatteryRack,
            RotaryTable

        }
     
    }
}
