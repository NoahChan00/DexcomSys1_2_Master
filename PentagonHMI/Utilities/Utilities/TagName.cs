using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class TagName
    {
        public struct HMI
        {

            public static string StartButton = "HMI.StartButton";
            public static string StopButton  = "HMI.StopButton";
            public static string ResetButton = "HMI.ResetButton";
            public static string UPH = "HMI.UPH";
            public static string UPH_Str = "HMI.UPH_Str";
            public static string TotalPass = "HMI.Total_Pass";
            public static string TotalFail = "HMI.Total_Fail";
            public static string StartDateTime = "HMI.Start_Date_Time";
            public static string SystemUptime = "HMI.System_UpTime";
            public static string SystemDowntime = "HMI.System_DownTime";
            public static string SystemOperationTime = "HMI.System_OperationTime";
            public static string IdlingTime = "HMI.System_IdlingTime";
            public static string Softjam = "HMI.Softjam";
            public static string HardJam = "HMI.Hardjam";
            public static string CycleTime = "HMI.CycleTime";
            public static string ResetOEE = "HMI.ResetOEE";
            public static string MachineSpeed = "HMI.Machine_Speed";
            public static string InputAddress_0 = "HMI.Input_0";
            public static string InputAddress_1 = "HMI.Input_1";
            public static string InputAddress_2 = "HMI.Input_2";
            public static string InputAddress_3 = "HMI.Input_3";
            public static string InputAddress_4 = "HMI.Input_4";
            public static string InputAddress_5 = "HMI.Input_5";
            public static string InputAddress_6 = "HMI.Input_6";
            public static string InputAddress_7 = "HMI.Input_7";
            public static string InputAddress_8 = "HMI.Input_8";
            public static string InputAddress_9 = "HMI.Input_9";
            public static string InputAddress_10 = "HMI.Input_10";
            public static string OutputAddress_0 = "HMI.Output_0";
            public static string OutputAddress_1 = "HMI.Output_1";
            public static string OutputAddress_2 = "HMI.Output_2";
            public static string OutputAddress_3 = "HMI.Output_3";
            public static string OutputAddress_4 = "HMI.Output_4";
            public static string OutputAddress_5 = "HMI.Output_5";
            public static string OutputAddress_6 = "HMI.Output_6";
            public static string OutputAddress_7 = "HMI.Output_7";
            public static string OutputAddress_8 = "HMI.Output_8";
            public static string OutputAddress_9 = "HMI.Output_9";
            public static string OutputAddress_10 = "HMI.Output_10";
            public static string Heart_Beat = "HMI.Heart_Beat";
            public static string Rack1a = "Rack1a_status";
            public static string Rack1b = "Rack1b_status";
            public static string Rack2a = "Rack2a_status";
            public static string Rack2b = "Rack2b_status";
            public static string Rack3a = "Rack3a_status";
            public static string Rack3b = "Rack3b_status";
            public static string Rack4a = "Rack4a_status";
            public static string Rack4b = "Rack4b_status";
            public static string Rack5a = "Rack5a_status";
            public static string Rack5b = "Rack5b_status";
            public static string Chamber = "Chamber_status";

            //public static string Rack1a_RFID = "Rack1a_RFID";
            //public static string Rack1b_RFID = "Rack1b_RFID";
            //public static string Rack2a_RFID = "Rack2a_RFID";
            //public static string Rack2b_RFID = "Rack2b_RFID";
            //public static string Rack3a_RFID = "Rack3a_RFID";
            //public static string Rack3b_RFID = "Rack3b_RFID";
            //public static string Rack4a_RFID = "Rack4a_RFID";
            //public static string Rack4b_RFID = "Rack4b_RFID";
            //public static string Rack5a_RFID = "Rack5a_RFID";
            //public static string Rack5b_RFID = "Rack5b_RFID";
            //public static string Chamber_RFID = "Chamber_RFID";



        };

        public struct LotEntry
        {
            public static string Ok_To_EndLot = "Lot_Entry.OK_To_End_Lot";
            public static string StartLot = "Lot_Entry.Start_Lot";
            public static string EndLot = "Lot_Entry.End_Lot";
            public static string Operation_Time = "Lot_Entry.Operation_Time";
            public static string DownTime = "Lot_Entry.Down_Time";
            public static string IdleTime = "Lot_Entry.Idle_Time";
            public static string TotalPass = "Lot_Entry.Total_Pass";
            public static string TotalFail = "Lot_Entry.Total_Fail";
            public static string LotID = "Lot_Entry.Lot_ID";
            public static string StartLotTime = "Lot_Entry.Start_Lot_Time";
        }

        public struct MachineStatus
        {
            public static string Machine_Status = "MachineStatus.MachineStatus";
            public static string WarningCode = "MachineStatus.WarningCode";
            public static string RobotConnectionStatus = "MachineStatus.Robot_Connection";
            public static string ErrorCode = "MachineStatus.ErrorCode";
            public static string ErrorCodeStr = "MachineStatus.ErrorCode_Str";

        };

        public struct OutputAddress
        {
            public static string Q0_0 = "ix00StartButton";
            public static string Q0_1 = "ix01StopButton";
            public static string Q0_2 = "ix02ResetButton";
            public static string Q0_3 = ".Qx0_3";
            public static string Q0_4 = ".Qx0_4";
            public static string Q0_5 = ".Qx0_5";
            public static string Q0_6 = ".Qx0_6";
            public static string Q0_7 = ".Qx0_7";

            public static string Q1_0 = ".Qx1_0";
            public static string Q1_1 = ".Qx1_1";
            public static string Q1_2 = ".Qx1_2";
            public static string Q1_3 = ".Qx1_3";
            public static string Q1_4 = ".Qx1_4";
            public static string Q1_5 = ".Qx1_5";
            public static string Q1_6 = ".Qx1_6";
            public static string Q1_7 = ".Qx1_7";

            public static string Q2_0 = ".Qx2_0";
            public static string Q2_1 = ".Qx2_1";
            public static string Q2_2 = ".Qx2_2";
            public static string Q2_3 = ".Qx2_3";
            public static string Q2_4 = ".Qx2_4";
            public static string Q2_5 = ".Qx2_5";
            public static string Q2_6 = ".Qx2_6";
            public static string Q2_7 = ".Qx2_7";

            public static string Q3_0 = ".Qx3_0";
            public static string Q3_1 = ".Qx3_1";
            public static string Q3_2 = ".Qx3_2";
            public static string Q3_3 = ".Qx3_3";
            public static string Q3_4 = ".Qx3_4";
            public static string Q3_5 = ".Qx3_5";
            public static string Q3_6 = ".Qx3_6";
            public static string Q3_7 = ".Qx3_7";

            public static string Q4_0 = ".Qx4_0";
            public static string Q4_1 = ".Qx4_1";
            public static string Q4_2 = ".Qx4_2";
            public static string Q4_3 = ".Qx4_3";
            public static string Q4_4 = ".Qx4_4";
            public static string Q4_5 = ".Qx4_5";
            public static string Q4_6 = ".Qx4_6";
            public static string Q4_7 = ".Qx4_7";

            public static string Q5_0 = ".Qx5_0";
            public static string Q5_1 = ".Qx5_1";
            public static string Q5_2 = ".Qx5_2";
            public static string Q5_3 = ".Qx5_3";
            public static string Q5_4 = ".Qx5_4";
            public static string Q5_5 = ".Qx5_5";
            public static string Q5_6 = ".Qx5_6";
            public static string Q5_7 = ".Qx5_7";

            public static string Q6_0 = ".Qx6_0";
            public static string Q6_1 = ".Qx6_1";
            public static string Q6_2 = ".Qx6_2";
            public static string Q6_3 = ".Qx6_3";
            public static string Q6_4 = ".Qx6_4";
            public static string Q6_5 = ".Qx6_5";
            public static string Q6_6 = ".Qx6_6";
            public static string Q6_7 = ".Qx6_7";

            public static string Q7_0 = ".Qx7_0";
            public static string Q7_1 = ".Qx7_1";
            public static string Q7_2 = ".Qx7_2";
            public static string Q7_3 = ".Qx7_3";
            public static string Q7_4 = ".Qx7_4";
            public static string Q7_5 = ".Qx7_5";
            public static string Q7_6 = ".Qx7_6";
            public static string Q7_7 = ".Qx7_7";

            public static string Q8_0 = ".Qx8_0";
            public static string Q8_1 = ".Qx8_1";
            public static string Q8_2 = ".Qx8_2";
            public static string Q8_3 = ".Qx8_3";
            public static string Q8_4 = ".Qx8_4";
            public static string Q8_5 = ".Qx8_5";
            public static string Q8_6 = ".Qx8_6";
            public static string Q8_7 = ".Qx8_7";

            public static string Q9_0 = ".Qx9_0";
            public static string Q9_1 = ".Qx9_1";
            public static string Q9_2 = ".Qx9_2";
            public static string Q9_3 = ".Qx9_3";
            public static string Q9_4 = ".Qx9_4";
            public static string Q9_5 = ".Qx9_5";
            public static string Q9_6 = ".Qx9_6";
            public static string Q9_7 = ".Qx9_7";

            public static string Q10_0 = ".Qx10_0";
            public static string Q10_1 = ".Qx10_1";
            public static string Q10_2 = ".Qx10_2";
            public static string Q10_3 = ".Qx10_3";
            public static string Q10_4 = ".Qx10_4";
            public static string Q10_5 = ".Qx10_5";
            public static string Q10_6 = ".Qx10_6";
            public static string Q10_7 = ".Qx10_7";

            public static string Q11_0 = ".Qx11_0";
            public static string Q11_1 = ".Qx11_1";
            public static string Q11_2 = ".Qx11_2";
            public static string Q11_3 = ".Qx11_3";
            public static string Q11_4 = ".Qx11_4";
            public static string Q11_5 = ".Qx11_5";
            public static string Q11_6 = ".Qx11_6";
            public static string Q11_7 = ".Qx11_7";

            public static string Q12_0 = ".Qx12_0";
            public static string Q12_1 = ".Qx12_1";
            public static string Q12_2 = ".Qx12_2";
            public static string Q12_3 = ".Qx12_3";
            public static string Q12_4 = ".Qx12_4";
            public static string Q12_5 = ".Qx12_5";
            public static string Q12_6 = ".Qx12_6";
            public static string Q12_7 = ".Qx12_7";

            public static string Q13_0 = ".Qx13_0";
            public static string Q13_1 = ".Qx13_1";
            public static string Q13_2 = ".Qx13_2";
            public static string Q13_3 = ".Qx13_3";
            public static string Q13_4 = ".Qx13_4";
            public static string Q13_5 = ".Qx13_5";
            public static string Q13_6 = ".Qx13_6";
            public static string Q13_7 = ".Qx13_7";

            public static string Q14_0 = ".Qx14_0";
            public static string Q14_1 = ".Qx14_1";
            public static string Q14_2 = ".Qx14_2";
            public static string Q14_3 = ".Qx14_3";
            public static string Q14_4 = ".Qx14_4";
            public static string Q14_5 = ".Qx14_5";
            public static string Q14_6 = ".Qx14_6";
            public static string Q14_7 = ".Qx14_7";

            public static string Q15_0 = ".Qx15_0";
            public static string Q15_1 = ".Qx15_1";
            public static string Q15_2 = ".Qx15_2";
            public static string Q15_3 = ".Qx15_3";
            public static string Q15_4 = ".Qx15_4";
            public static string Q15_5 = ".Qx15_5";
            public static string Q15_6 = ".Qx15_6";
            public static string Q15_7 = ".Qx15_7";

            public static string Q16_0 = ".Qx16_0";
            public static string Q16_1 = ".Qx16_1";
            public static string Q16_2 = ".Qx16_2";
            public static string Q16_3 = ".Qx16_3";
            public static string Q16_4 = ".Qx16_4";
            public static string Q16_5 = ".Qx16_5";
            public static string Q16_6 = ".Qx16_6";
            public static string Q16_7 = ".Qx16_7";

            public static string Q17_0 = ".Qx17_0";
            public static string Q17_1 = ".Qx17_1";
            public static string Q17_2 = ".Qx17_2";
            public static string Q17_3 = ".Qx17_3";
            public static string Q17_4 = ".Qx17_4";
            public static string Q17_5 = ".Qx17_5";
            public static string Q17_6 = ".Qx17_6";
            public static string Q17_7 = ".Qx17_7";

            public static string Q18_0 = ".Qx18_0";
            public static string Q18_1 = ".Qx18_1";
            public static string Q18_2 = ".Qx18_2";
            public static string Q18_3 = ".Qx18_3";
            public static string Q18_4 = ".Qx18_4";
            public static string Q18_5 = ".Qx18_5";
            public static string Q18_6 = ".Qx18_6";
            public static string Q18_7 = ".Qx18_7";

            public static string Q19_0 = ".Qx19_0";
            public static string Q19_1 = ".Qx19_1";
            public static string Q19_2 = ".Qx19_2";
            public static string Q19_3 = ".Qx19_3";
            public static string Q19_4 = ".Qx19_4";
            public static string Q19_5 = ".Qx19_5";
            public static string Q19_6 = ".Qx19_6";
            public static string Q19_7 = ".Qx19_7";

            public static string Q20_0 = ".Qx20_0";
            public static string Q20_1 = ".Qx20_1";
            public static string Q20_2 = ".Qx20_2";
            public static string Q20_3 = ".Qx20_3";
            public static string Q20_4 = ".Qx20_4";
            public static string Q20_5 = ".Qx20_5";
            public static string Q20_6 = ".Qx20_6";
            public static string Q20_7 = ".Qx20_7";

            public static string Q21_0 = ".Qx21_0";
            public static string Q21_1 = ".Qx21_1";
            public static string Q21_2 = ".Qx21_2";
            public static string Q21_3 = ".Qx21_3";
            public static string Q21_4 = ".Qx21_4";
            public static string Q21_5 = ".Qx21_5";
            public static string Q21_6 = ".Qx21_6";
            public static string Q21_7 = ".Qx21_7";

            public static string Q22_0 = ".Qx22_0";
            public static string Q22_1 = ".Qx22_1";
            public static string Q22_2 = ".Qx22_2";
            public static string Q22_3 = ".Qx22_3";
            public static string Q22_4 = ".Qx22_4";
            public static string Q22_5 = ".Qx22_5";
            public static string Q22_6 = ".Qx22_6";
            public static string Q22_7 = ".Qx22_7";

            public static string Q23_0 = ".Qx23_0";
            public static string Q23_1 = ".Qx23_1";
            public static string Q23_2 = ".Qx23_2";
            public static string Q23_3 = ".Qx23_3";
            public static string Q23_4 = ".Qx23_4";
            public static string Q23_5 = ".Qx23_5";
            public static string Q23_6 = ".Qx23_6";
            public static string Q23_7 = ".Qx23_7";

            public static string Q24_0 = ".Qx24_0";
            public static string Q24_1 = ".Qx24_1";
            public static string Q24_2 = ".Qx24_2";
            public static string Q24_3 = ".Qx24_3";
            public static string Q24_4 = ".Qx24_4";
            public static string Q24_5 = ".Qx24_5";
            public static string Q24_6 = ".Qx24_6";
            public static string Q24_7 = ".Qx24_7";

            public static string Q25_0 = ".Qx25_0";
            public static string Q25_1 = ".Qx25_1";
            public static string Q25_2 = ".Qx25_2";
            public static string Q25_3 = ".Qx25_3";
            public static string Q25_4 = ".Qx25_4";
            public static string Q25_5 = ".Qx25_5";
            public static string Q25_6 = ".Qx25_6";
            public static string Q25_7 = ".Qx25_7";

            public static string Q26_0 = ".Qx26_0";
            public static string Q26_1 = ".Qx26_1";
            public static string Q26_2 = ".Qx26_2";
            public static string Q26_3 = ".Qx26_3";
            public static string Q26_4 = ".Qx26_4";
            public static string Q26_5 = ".Qx26_5";
            public static string Q26_6 = ".Qx26_6";
            public static string Q26_7 = ".Qx26_7";

            public static string Q27_0 = ".Qx27_0";
            public static string Q27_1 = ".Qx27_1";
            public static string Q27_2 = ".Qx27_2";
            public static string Q27_3 = ".Qx27_3";
            public static string Q27_4 = ".Qx27_4";
            public static string Q27_5 = ".Qx27_5";
            public static string Q27_6 = ".Qx27_6";
            public static string Q27_7 = ".Qx27_7";

            public static string Q28_0 = ".Qx28_0";
            public static string Q28_1 = ".Qx28_1";
            public static string Q28_2 = ".Qx28_2";
            public static string Q28_3 = ".Qx28_3";
            public static string Q28_4 = ".Qx28_4";
            public static string Q28_5 = ".Qx28_5";
            public static string Q28_6 = ".Qx28_6";
            public static string Q28_7 = ".Qx28_7";

            public static string Q29_0 = ".Qx29_0";
            public static string Q29_1 = ".Qx29_1";
            public static string Q29_2 = ".Qx29_2";
            public static string Q29_3 = ".Qx29_3";
            public static string Q29_4 = ".Qx29_4";
            public static string Q29_5 = ".Qx29_5";
            public static string Q29_6 = ".Qx29_6";
            public static string Q29_7 = ".Qx29_7";

            public static string Q30_0 = ".Qx30_0";
            public static string Q30_1 = ".Qx30_1";
            public static string Q30_2 = ".Qx30_2";
            public static string Q30_3 = ".Qx30_3";
            public static string Q30_4 = ".Qx30_4";
            public static string Q30_5 = ".Qx30_5";
            public static string Q30_6 = ".Qx30_6";
            public static string Q30_7 = ".Qx30_7";

            public static string Q31_0 = ".Qx31_0";
            public static string Q31_1 = ".Qx31_1";
            public static string Q31_2 = ".Qx31_2";
            public static string Q31_3 = ".Qx31_3";
            public static string Q31_4 = ".Qx31_4";
            public static string Q31_5 = ".Qx31_5";
            public static string Q31_6 = ".Qx31_6";
            public static string Q31_7 = ".Qx31_7";

            public static string Q32_0 = ".Qx32_0";
            public static string Q32_1 = ".Qx32_1";
            public static string Q32_2 = ".Qx32_2";
            public static string Q32_3 = ".Qx32_3";
            public static string Q32_4 = ".Qx32_4";
            public static string Q32_5 = ".Qx32_5";
            public static string Q32_6 = ".Qx32_6";
            public static string Q32_7 = ".Qx32_7";

            public static string Q33_0 = ".Qx33_0";
            public static string Q33_1 = ".Qx33_1";
            public static string Q33_2 = ".Qx33_2";
            public static string Q33_3 = ".Qx33_3";
            public static string Q33_4 = ".Qx33_4";
            public static string Q33_5 = ".Qx33_5";
            public static string Q33_6 = ".Qx33_6";
            public static string Q33_7 = ".Qx33_7";

            public static string Q34_0 = ".Qx34_0";
            public static string Q34_1 = ".Qx34_1";
            public static string Q34_2 = ".Qx34_2";
            public static string Q34_3 = ".Qx34_3";
            public static string Q34_4 = ".Qx34_4";
            public static string Q34_5 = ".Qx34_5";
            public static string Q34_6 = ".Qx34_6";
            public static string Q34_7 = ".Qx34_7";

            public static string Q35_0 = ".Qx35_0";
            public static string Q35_1 = ".Qx35_1";
            public static string Q35_2 = ".Qx35_2";
            public static string Q35_3 = ".Qx35_3";
            public static string Q35_4 = ".Qx35_4";
            public static string Q35_5 = ".Qx35_5";
            public static string Q35_6 = ".Qx35_6";
            public static string Q35_7 = ".Qx35_7";

            public static string Q36_0 = ".Qx36_0";
            public static string Q36_1 = ".Qx36_1";
            public static string Q36_2 = ".Qx36_2";
            public static string Q36_3 = ".Qx36_3";
            public static string Q36_4 = ".Qx36_4";
            public static string Q36_5 = ".Qx36_5";
            public static string Q36_6 = ".Qx36_6";
            public static string Q36_7 = ".Qx36_7";

            public static string Q37_0 = ".Qx37_0";
            public static string Q37_1 = ".Qx37_1";
            public static string Q37_2 = ".Qx37_2";
            public static string Q37_3 = ".Qx37_3";
            public static string Q37_4 = ".Qx37_4";
            public static string Q37_5 = ".Qx37_5";
            public static string Q37_6 = ".Qx37_6";
            public static string Q37_7 = ".Qx37_7";

            public static string Q38_0 = ".Qx38_0";
            public static string Q38_1 = ".Qx38_1";
            public static string Q38_2 = ".Qx38_2";
            public static string Q38_3 = ".Qx38_3";
            public static string Q38_4 = ".Qx38_4";
            public static string Q38_5 = ".Qx38_5";
            public static string Q38_6 = ".Qx38_6";
            public static string Q38_7 = ".Qx38_7";

            public static string Q39_0 = ".Qx39_0";
            public static string Q39_1 = ".Qx39_1";
            public static string Q39_2 = ".Qx39_2";
            public static string Q39_3 = ".Qx39_3";
            public static string Q39_4 = ".Qx39_4";
            public static string Q39_5 = ".Qx39_5";
            public static string Q39_6 = ".Qx39_6";
            public static string Q39_7 = ".Qx39_7";

            public static string Q40_0 = ".Qx40_0";
            public static string Q40_1 = ".Qx40_1";
            public static string Q40_2 = ".Qx40_2";
            public static string Q40_3 = ".Qx40_3";
            public static string Q40_4 = ".Qx40_4";
            public static string Q40_5 = ".Qx40_5";
            public static string Q40_6 = ".Qx40_6";
            public static string Q40_7 = ".Qx40_7";

            public static string Q41_0 = ".Qx41_0";
            public static string Q41_1 = ".Qx41_1";
            public static string Q41_2 = ".Qx41_2";
            public static string Q41_3 = ".Qx41_3";
            public static string Q41_4 = ".Qx41_4";
            public static string Q41_5 = ".Qx41_5";
            public static string Q41_6 = ".Qx41_6";
            public static string Q41_7 = ".Qx41_7";

            public static string Q42_0 = ".Qx42_0";
            public static string Q42_1 = ".Qx42_1";
            public static string Q42_2 = ".Qx42_2";
            public static string Q42_3 = ".Qx42_3";
            public static string Q42_4 = ".Qx42_4";
            public static string Q42_5 = ".Qx42_5";
            public static string Q42_6 = ".Qx42_6";
            public static string Q42_7 = ".Qx42_7";

            public static string Q43_0 = ".Qx43_0";
            public static string Q43_1 = ".Qx43_1";
            public static string Q43_2 = ".Qx43_2";
            public static string Q43_3 = ".Qx43_3";
            public static string Q43_4 = ".Qx43_4";
            public static string Q43_5 = ".Qx43_5";
            public static string Q43_6 = ".Qx43_6";
            public static string Q43_7 = ".Qx43_7";

            public static string Q44_0 = ".Qx44_0";
            public static string Q44_1 = ".Qx44_1";
            public static string Q44_2 = ".Qx44_2";
            public static string Q44_3 = ".Qx44_3";
            public static string Q44_4 = ".Qx44_4";
            public static string Q44_5 = ".Qx44_5";
            public static string Q44_6 = ".Qx44_6";
            public static string Q44_7 = ".Qx44_7";

            public static string Q45_0 = ".Qx45_0";
            public static string Q45_1 = ".Qx45_1";
            public static string Q45_2 = ".Qx45_2";
            public static string Q45_3 = ".Qx45_3";
            public static string Q45_4 = ".Qx45_4";
            public static string Q45_5 = ".Qx45_5";
            public static string Q45_6 = ".Qx45_6";
            public static string Q45_7 = ".Qx45_7";

            public static string Q46_0 = ".Qx46_0";
            public static string Q46_1 = ".Qx46_1";
            public static string Q46_2 = ".Qx46_2";
            public static string Q46_3 = ".Qx46_3";
            public static string Q46_4 = ".Qx46_4";
            public static string Q46_5 = ".Qx46_5";
            public static string Q46_6 = ".Qx46_6";
            public static string Q46_7 = ".Qx46_7";

            public static string Q47_0 = ".Qx47_0";
            public static string Q47_1 = ".Qx47_1";
            public static string Q47_2 = ".Qx47_2";
            public static string Q47_3 = ".Qx47_3";
            public static string Q47_4 = ".Qx47_4";
            public static string Q47_5 = ".Qx47_5";
            public static string Q47_6 = ".Qx47_6";
            public static string Q47_7 = ".Qx47_7";

            public static string Q48_0 = ".Qx48_0";
            public static string Q48_1 = ".Qx48_1";
            public static string Q48_2 = ".Qx48_2";
            public static string Q48_3 = ".Qx48_3";
            public static string Q48_4 = ".Qx48_4";
            public static string Q48_5 = ".Qx48_5";
            public static string Q48_6 = ".Qx48_6";
            public static string Q48_7 = ".Qx48_7";

            public static string Q49_0 = ".Qx49_0";
            public static string Q49_1 = ".Qx49_1";
            public static string Q49_2 = ".Qx49_2";
            public static string Q49_3 = ".Qx49_3";
            public static string Q49_4 = ".Qx49_4";
            public static string Q49_5 = ".Qx49_5";
            public static string Q49_6 = ".Qx49_6";
            public static string Q49_7 = ".Qx49_7";

            public static string Q50_0 = ".Qx50_0";
            public static string Q50_1 = ".Qx50_1";
            public static string Q50_2 = ".Qx50_2";
            public static string Q50_3 = ".Qx50_3";
            public static string Q50_4 = ".Qx50_4";
            public static string Q50_5 = ".Qx50_5";
            public static string Q50_6 = ".Qx50_6";
            public static string Q50_7 = ".Qx50_7";

            public static string Q51_0 = ".Qx51_0";
            public static string Q51_1 = ".Qx51_1";
            public static string Q51_2 = ".Qx51_2";
            public static string Q51_3 = ".Qx51_3";
            public static string Q51_4 = ".Qx51_4";
            public static string Q51_5 = ".Qx51_5";
            public static string Q51_6 = ".Qx51_6";
            public static string Q51_7 = ".Qx51_7";

            public static string Q52_0 = ".Qx52_0";
            public static string Q52_1 = ".Qx52_1";
            public static string Q52_2 = ".Qx52_2";
            public static string Q52_3 = ".Qx52_3";
            public static string Q52_4 = ".Qx52_4";
            public static string Q52_5 = ".Qx52_5";
            public static string Q52_6 = ".Qx52_6";
            public static string Q52_7 = ".Qx52_7";

            public static string Q53_0 = ".Qx53_0";
            public static string Q53_1 = ".Qx53_1";
            public static string Q53_2 = ".Qx53_2";
            public static string Q53_3 = ".Qx53_3";
            public static string Q53_4 = ".Qx53_4";
            public static string Q53_5 = ".Qx53_5";
            public static string Q53_6 = ".Qx53_6";
            public static string Q53_7 = ".Qx53_7";

            public static string Q54_0 = ".Qx54_0";
            public static string Q54_1 = ".Qx54_1";
            public static string Q54_2 = ".Qx54_2";
            public static string Q54_3 = ".Qx54_3";
            public static string Q54_4 = ".Qx54_4";
            public static string Q54_5 = ".Qx54_5";
            public static string Q54_6 = ".Qx54_6";
            public static string Q54_7 = ".Qx54_7";

            public static string Q55_0 = ".Qx55_0";
            public static string Q55_1 = ".Qx55_1";
            public static string Q55_2 = ".Qx55_2";
            public static string Q55_3 = ".Qx55_3";
            public static string Q55_4 = ".Qx55_4";
            public static string Q55_5 = ".Qx55_5";
            public static string Q55_6 = ".Qx55_6";
            public static string Q55_7 = ".Qx55_7";

            public static string Q56_0 = ".Qx56_0";
            public static string Q56_1 = ".Qx56_1";
            public static string Q56_2 = ".Qx56_2";
            public static string Q56_3 = ".Qx56_3";
            public static string Q56_4 = ".Qx56_4";
            public static string Q56_5 = ".Qx56_5";
            public static string Q56_6 = ".Qx56_6";
            public static string Q56_7 = ".Qx56_7";

            public static string Q57_0 = ".Qx57_0";
            public static string Q57_1 = ".Qx57_1";
            public static string Q57_2 = ".Qx57_2";
            public static string Q57_3 = ".Qx57_3";
            public static string Q57_4 = ".Qx57_4";
            public static string Q57_5 = ".Qx57_5";
            public static string Q57_6 = ".Qx57_6";
            public static string Q57_7 = ".Qx57_7";

            public static string Q58_0 = ".Qx58_0";
            public static string Q58_1 = ".Qx58_1";
            public static string Q58_2 = ".Qx58_2";
            public static string Q58_3 = ".Qx58_3";
            public static string Q58_4 = ".Qx58_4";
            public static string Q58_5 = ".Qx58_5";
            public static string Q58_6 = ".Qx58_6";
            public static string Q58_7 = ".Qx58_7";

            public static string Q59_0 = ".Qx59_0";
            public static string Q59_1 = ".Qx59_1";
            public static string Q59_2 = ".Qx59_2";
            public static string Q59_3 = ".Qx59_3";
            public static string Q59_4 = ".Qx59_4";
            public static string Q59_5 = ".Qx59_5";
            public static string Q59_6 = ".Qx59_6";
            public static string Q59_7 = ".Qx59_7";

            public static string Q60_0 = ".Qx60_0";
            public static string Q60_1 = ".Qx60_1";
            public static string Q60_2 = ".Qx60_2";
            public static string Q60_3 = ".Qx60_3";
            public static string Q60_4 = ".Qx60_4";
            public static string Q60_5 = ".Qx60_5";
            public static string Q60_6 = ".Qx60_6";
            public static string Q60_7 = ".Qx60_7";

            public static string Q61_0 = ".Qx61_0";
            public static string Q61_1 = ".Qx61_1";
            public static string Q61_2 = ".Qx61_2";
            public static string Q61_3 = ".Qx61_3";
            public static string Q61_4 = ".Qx61_4";
            public static string Q61_5 = ".Qx61_5";
            public static string Q61_6 = ".Qx61_6";
            public static string Q61_7 = ".Qx61_7";

            public static string Q62_0 = ".Qx62_0";
            public static string Q62_1 = ".Qx62_1";
            public static string Q62_2 = ".Qx62_2";
            public static string Q62_3 = ".Qx62_3";
            public static string Q62_4 = ".Qx62_4";
            public static string Q62_5 = ".Qx62_5";
            public static string Q62_6 = ".Qx62_6";
            public static string Q62_7 = ".Qx62_7";

            public static string Q63_0 = ".Qx63_0";
            public static string Q63_1 = ".Qx63_1";
            public static string Q63_2 = ".Qx63_2";
            public static string Q63_3 = ".Qx63_3";
            public static string Q63_4 = ".Qx63_4";
            public static string Q63_5 = ".Qx63_5";
            public static string Q63_6 = ".Qx63_6";
            public static string Q63_7 = ".Qx63_7";

            public static string Q64_0 = ".Qx64_0";
            public static string Q64_1 = ".Qx64_1";
            public static string Q64_2 = ".Qx64_2";
            public static string Q64_3 = ".Qx64_3";
            public static string Q64_4 = ".Qx64_4";
            public static string Q64_5 = ".Qx64_5";
            public static string Q64_6 = ".Qx64_6";
            public static string Q64_7 = ".Qx64_7";

            public static string Q65_0 = ".Qx65_0";
            public static string Q65_1 = ".Qx65_1";
            public static string Q65_2 = ".Qx65_2";
            public static string Q65_3 = ".Qx65_3";
            public static string Q65_4 = ".Qx65_4";
            public static string Q65_5 = ".Qx65_5";
            public static string Q65_6 = ".Qx65_6";
            public static string Q65_7 = ".Qx65_7";

            public static string Q66_0 = ".Qx66_0";
            public static string Q66_1 = ".Qx66_1";
            public static string Q66_2 = ".Qx66_2";
            public static string Q66_3 = ".Qx66_3";
            public static string Q66_4 = ".Qx66_4";
            public static string Q66_5 = ".Qx66_5";
            public static string Q66_6 = ".Qx66_6";
            public static string Q66_7 = ".Qx66_7";

            public static string Q67_0 = ".Qx67_0";
            public static string Q67_1 = ".Qx67_1";
            public static string Q67_2 = ".Qx67_2";
            public static string Q67_3 = ".Qx67_3";
            public static string Q67_4 = ".Qx67_4";
            public static string Q67_5 = ".Qx67_5";
            public static string Q67_6 = ".Qx67_6";
            public static string Q67_7 = ".Qx67_7";

            public static string Q68_0 = ".Qx68_0";
            public static string Q68_1 = ".Qx68_1";
            public static string Q68_2 = ".Qx68_2";
            public static string Q68_3 = ".Qx68_3";
            public static string Q68_4 = ".Qx68_4";
            public static string Q68_5 = ".Qx68_5";
            public static string Q68_6 = ".Qx68_6";
            public static string Q68_7 = ".Qx68_7";

            public static string Q69_0 = ".Qx69_0";
            public static string Q69_1 = ".Qx69_1";
            public static string Q69_2 = ".Qx69_2";
            public static string Q69_3 = ".Qx69_3";
            public static string Q69_4 = ".Qx69_4";
            public static string Q69_5 = ".Qx69_5";
            public static string Q69_6 = ".Qx69_6";
            public static string Q69_7 = ".Qx69_7";

            public static string Q70_0 = ".Qx70_0";
            public static string Q70_1 = ".Qx70_1";
            public static string Q70_2 = ".Qx70_2";
            public static string Q70_3 = ".Qx70_3";
            public static string Q70_4 = ".Qx70_4";
            public static string Q70_5 = ".Qx70_5";
            public static string Q70_6 = ".Qx70_6";
            public static string Q70_7 = ".Qx70_7";

            public static string Q71_0 = ".Qx71_0";
            public static string Q71_1 = ".Qx71_1";
            public static string Q71_2 = ".Qx71_2";
            public static string Q71_3 = ".Qx71_3";
            public static string Q71_4 = ".Qx71_4";
            public static string Q71_5 = ".Qx71_5";
            public static string Q71_6 = ".Qx71_6";
            public static string Q71_7 = ".Qx71_7";

            public static string Q72_0 = ".Qx72_0";
            public static string Q72_1 = ".Qx72_1";
            public static string Q72_2 = ".Qx72_2";
            public static string Q72_3 = ".Qx72_3";
            public static string Q72_4 = ".Qx72_4";
            public static string Q72_5 = ".Qx72_5";
            public static string Q72_6 = ".Qx72_6";
            public static string Q72_7 = ".Qx72_7";

            public static string Q73_0 = ".Qx73_0";
            public static string Q73_1 = ".Qx73_1";
            public static string Q73_2 = ".Qx73_2";
            public static string Q73_3 = ".Qx73_3";
            public static string Q73_4 = ".Qx73_4";
            public static string Q73_5 = ".Qx73_5";
            public static string Q73_6 = ".Qx73_6";
            public static string Q73_7 = ".Qx73_7";

            public static string Q74_0 = ".Qx74_0";
            public static string Q74_1 = ".Qx74_1";
            public static string Q74_2 = ".Qx74_2";
            public static string Q74_3 = ".Qx74_3";
            public static string Q74_4 = ".Qx74_4";
            public static string Q74_5 = ".Qx74_5";
            public static string Q74_6 = ".Qx74_6";
            public static string Q74_7 = ".Qx74_7";

            public static string Q75_0 = ".Qx75_0";
            public static string Q75_1 = ".Qx75_1";
            public static string Q75_2 = ".Qx75_2";
            public static string Q75_3 = ".Qx75_3";
            public static string Q75_4 = ".Qx75_4";
            public static string Q75_5 = ".Qx75_5";
            public static string Q75_6 = ".Qx75_6";
            public static string Q75_7 = ".Qx75_7";

            public static string Q76_0 = ".Qx76_0";
            public static string Q76_1 = ".Qx76_1";
            public static string Q76_2 = ".Qx76_2";
            public static string Q76_3 = ".Qx76_3";
            public static string Q76_4 = ".Qx76_4";
            public static string Q76_5 = ".Qx76_5";
            public static string Q76_6 = ".Qx76_6";
            public static string Q76_7 = ".Qx76_7";

            public static string Q77_0 = ".Qx77_0";
            public static string Q77_1 = ".Qx77_1";
            public static string Q77_2 = ".Qx77_2";
            public static string Q77_3 = ".Qx77_3";
            public static string Q77_4 = ".Qx77_4";
            public static string Q77_5 = ".Qx77_5";
            public static string Q77_6 = ".Qx77_6";
            public static string Q77_7 = ".Qx77_7";

            public static string Q78_0 = ".Qx78_0";
            public static string Q78_1 = ".Qx78_1";
            public static string Q78_2 = ".Qx78_2";
            public static string Q78_3 = ".Qx78_3";
            public static string Q78_4 = ".Qx78_4";
            public static string Q78_5 = ".Qx78_5";
            public static string Q78_6 = ".Qx78_6";
            public static string Q78_7 = ".Qx78_7";

            public static string Q79_0 = ".Qx79_0";
            public static string Q79_1 = ".Qx79_1";
            public static string Q79_2 = ".Qx79_2";
            public static string Q79_3 = ".Qx79_3";
            public static string Q79_4 = ".Qx79_4";
            public static string Q79_5 = ".Qx79_5";
            public static string Q79_6 = ".Qx79_6";
            public static string Q79_7 = ".Qx79_7";

        };

        public struct MotorAxis1
        {
            public static string Axis1_Stop = ".Axis1_Stop";
            public static string Axis1_Acc = ".Axis1_Acc";
            public static string Axis1_Dcc = ".Axis1_Dcc";
            public static string Axis1_Frequency = ".Axis1_Frequency";
            public static string Axis1_Target = ".Axis1_Target";
            public static string Axis1_GoSet = ".Axis1_GoSet";
            public static string Axis1_Busy = ".Axis1_Busy";
            public static string Axis1_Reached = ".Axis1_Reached";
            public static string Axis1_CurrentPos = ".Axis1_CurrentPos";
            public static string Axis1_JogSpeed = ".Axis1_JogSpeed";
            public static string Axis1_JogAcc = ".Axis1_JogAcc";
            public static string Axis1_JogDcc = ".Axis1_JogDcc";
            public static string Axis1_JogPos = ".Axis1_JogPos";
            public static string Axis1_JogNeg = ".Axis1_JogNeg";
            public static string Axis1_StartHoming = ".Axis1_StartHoming";
            public static string Axis1_CancelHoming = ".Axis1_CancelHoming";
            public static string Axis1_HomeSpeed1 = ".Axis1_HomeSpeed1";
            public static string Axis1_HomeSpeed2 = ".Axis1_HomeSpeed2";
            public static string Axis1_HomeBusy = ".Axis1_HomeBusy";
            public static string Axis1_HomeDone = ".Axis1_HomeDone";
            public static string Axis1_HomeDoneLocal = ".Axis1_HomeDoneLocal";
        };

        public struct MotorAxis2
        {
            public static string Axis2_Stop = ".Axis2_Stop";
            public static string Axis2_Acc = ".Axis2_Acc";
            public static string Axis2_Dcc = ".Axis2_Dcc";
            public static string Axis2_Frequency = ".Axis2_Frequency";
            public static string Axis2_Target = ".Axis2_Target";
            public static string Axis2_GoSet = ".Axis2_GoSet";
            public static string Axis2_Busy = ".Axis2_Busy";
            public static string Axis2_Reached = ".Axis2_Reached";
            public static string Axis2_CurrentPos = ".Axis2_CurrentPos";
            public static string Axis2_JogSpeed = ".Axis2_JogSpeed";
            public static string Axis2_JogAcc = ".Axis2_JogAcc";
            public static string Axis2_JogDcc = ".Axis2_JogDcc";
            public static string Axis2_JogPos = ".Axis2_JogPos";
            public static string Axis2_JogNeg = ".Axis2_JogNeg";
            public static string Axis2_StartHoming = ".Axis2_StartHoming";
            public static string Axis2_CancelHoming = ".Axis2_CancelHoming";
            public static string Axis2_HomeSpeed1 = ".Axis2_HomeSpeed1";
            public static string Axis2_HomeSpeed2 = ".Axis2_HomeSpeed2";
            public static string Axis2_HomeBusy = ".Axis2_HomeBusy";
            public static string Axis2_HomeDone = ".Axis2_HomeDone";
            public static string Axis2_HomeDoneLocal = ".Axis2_HomeDoneLocal";
        };

        public struct MotorAxis3
        {
            public static string Axis3_Stop = ".Axis3_Stop";
            public static string Axis3_Acc = ".Axis3_Acc";
            public static string Axis3_Dcc = ".Axis3_Dcc";
            public static string Axis3_Frequency = ".Axis3_Frequency";
            public static string Axis3_Target = ".Axis3_Target";
            public static string Axis3_GoSet = ".Axis3_GoSet";
            public static string Axis3_Busy = ".Axis3_Busy";
            public static string Axis3_Reached = ".Axis3_Reached";
            public static string Axis3_CurrentPos = ".Axis3_CurrentPos";
            public static string Axis3_JogSpeed = ".Axis3_JogSpeed";
            public static string Axis3_JogAcc = ".Axis3_JogAcc";
            public static string Axis3_JogDcc = ".Axis3_JogDcc";
            public static string Axis3_JogPos = ".Axis3_JogPos";
            public static string Axis3_JogNeg = ".Axis3_JogNeg";
            public static string Axis3_StartHoming = ".Axis3_StartHoming";
            public static string Axis3_CancelHoming = ".Axis3_CancelHoming";
            public static string Axis3_HomeSpeed1 = ".Axis3_HomeSpeed1";
            public static string Axis3_HomeSpeed2 = ".Axis3_HomeSpeed2";
            public static string Axis3_HomeBusy = ".Axis3_HomeBusy";
            public static string Axis3_HomeDone = ".Axis3_HomeDone";
            public static string Axis3_HomeDoneLocal = ".Axis3_HomeDoneLocal";
        };

        public struct MotorAxis4
        {
            public static string Axis4_Stop = ".Axis4_Stop";
            public static string Axis4_Acc = ".Axis4_Acc";
            public static string Axis4_Dcc = ".Axis4_Dcc";
            public static string Axis4_Frequency = ".Axis4_Frequency";
            public static string Axis4_Target = ".Axis4_Target";
            public static string Axis4_GoSet = ".Axis4_GoSet";
            public static string Axis4_Busy = ".Axis4_Busy";
            public static string Axis4_Reached = ".Axis4_Reached";
            public static string Axis4_CurrentPos = ".Axis4_CurrentPos";
            public static string Axis4_JogSpeed = ".Axis4_JogSpeed";
            public static string Axis4_JogAcc = ".Axis4_JogAcc";
            public static string Axis4_JogDcc = ".Axis4_JogDcc";
            public static string Axis4_JogPos = ".Axis4_JogPos";
            public static string Axis4_JogNeg = ".Axis4_JogNeg";
            public static string Axis4_StartHoming = ".Axis4_StartHoming";
            public static string Axis4_CancelHoming = ".Axis4_CancelHoming";
            public static string Axis4_HomeSpeed1 = ".Axis4_HomeSpeed1";
            public static string Axis4_HomeSpeed2 = ".Axis4_HomeSpeed2";
            public static string Axis4_HomeBusy = ".Axis4_HomeBusy";
            public static string Axis4_HomeDone = ".Axis4_HomeDone";
            public static string Axis4_HomeDoneLocal = ".Axis4_HomeDoneLocal";
        };

        public struct MotorAxis5
        {
            public static string Axis5_Stop = ".Axis5_Stop";
            public static string Axis5_Acc = ".Axis5_Acc";
            public static string Axis5_Dcc = ".Axis5_Dcc";
            public static string Axis5_Frequency = ".Axis5_Frequency";
            public static string Axis5_Target = ".Axis5_Target";
            public static string Axis5_GoSet = ".Axis5_GoSet";
            public static string Axis5_Busy = ".Axis5_Busy";
            public static string Axis5_Reached = ".Axis5_Reached";
            public static string Axis5_CurrentPos = ".Axis5_CurrentPos";
            public static string Axis5_JogSpeed = ".Axis5_JogSpeed";
            public static string Axis5_JogAcc = ".Axis5_JogAcc";
            public static string Axis5_JogDcc = ".Axis5_JogDcc";
            public static string Axis5_JogPos = ".Axis5_JogPos";
            public static string Axis5_JogNeg = ".Axis5_JogNeg";
            public static string Axis5_StartHoming = ".Axis5_StartHoming";
            public static string Axis5_CancelHoming = ".Axis5_CancelHoming";
            public static string Axis5_HomeSpeed1 = ".Axis5_HomeSpeed1";
            public static string Axis5_HomeSpeed2 = ".Axis5_HomeSpeed2";
            public static string Axis5_HomeBusy = ".Axis5_HomeBusy";
            public static string Axis5_HomeDone = ".Axis5_HomeDone";
            public static string Axis5_HomeDoneLocal = ".Axis5_HomeDoneLocal";  
        };

        public struct MotorAxis6
        {
            public static string Axis6_Stop = ".Axis6_Stop";
            public static string Axis6_Acc = ".Axis6_Acc";
            public static string Axis6_Dcc = ".Axis6_Dcc";
            public static string Axis6_Frequency = ".Axis6_Frequency";
            public static string Axis6_Target = ".Axis6_Target";
            public static string Axis6_GoSet = ".Axis6_GoSet";
            public static string Axis6_Busy = ".Axis6_Busy";
            public static string Axis6_Reached = ".Axis6_Reached";
            public static string Axis6_CurrentPos = ".Axis6_CurrentPos";
            public static string Axis6_JogSpeed = ".Axis6_JogSpeed";
            public static string Axis6_JogAcc = ".Axis6_JogAcc";
            public static string Axis6_JogDcc = ".Axis6_JogDcc";
            public static string Axis6_JogPos = ".Axis6_JogPos";
            public static string Axis6_JogNeg = ".Axis6_JogNeg";
            public static string Axis6_StartHoming = ".Axis6_StartHoming";
            public static string Axis6_CancelHoming = ".Axis6_CancelHoming";
            public static string Axis6_HomeSpeed1 = ".Axis6_HomeSpeed1";
            public static string Axis6_HomeSpeed2 = ".Axis6_HomeSpeed2";
            public static string Axis6_HomeBusy = ".Axis6_HomeBusy";
            public static string Axis6_HomeDone = ".Axis6_HomeDone";
            public static string Axis6_HomeDoneLocal = ".Axis6_HomeDoneLocal";
        };

        public struct GlobalVariable
        {
            public static string Machine_Recipe = "gw_Machine_Recipe";
            public static string BarcodeRead = "gstr_Barcode_Read";
            public static string BarcodeWrite = "gstr_Barcode_Write";
            public static string ReadBarcodeBit = "gb_Read_Barcode_Bit";
            public static string UnloaderStopFeeding = "gb_Unloader_Stop_Feeding";
            public static string LoaderStopFeeding = "gb_Loader_Stop_Feeding";
            public static string Set_Recipe = "gb_Set_Recipe";
            public static string Stop_Recipe = "gb_Stop_Recipe";
            public static string One_Cycle_Mode = "gb_One_Cycle_Button";
            public static string OutputPageEnableTrigger = "gb_Enable_Output";
            public static string DisableZone5 = "Bypass_Zone5_RFID";
            public static string DisableShutter5 = "Bypass_Shutter_RFID";
            public static string DisableRack1a = "Rack1a_Disable";
            public static string DisableRack1b = "Rack1b_Disable";
            public static string DisableRack2a = "Rack2a_Disable";
            public static string DisableRack2b = "Rack2b_Disable";
            public static string DisableRack3a = "Rack3a_Disable";
            public static string DisableRack3b = "Rack3b_Disable";
            public static string DisableRack4a = "Rack4a_Disable";
            public static string DisableRack4b = "Rack4b_Disable";
            public static string DisableRack5a = "Rack5a_Disable";
            public static string DisableRack5b = "Rack5b_Disable";
            
        };
    }
}
