using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class BeckHoff
    {
        public struct Input
        {
            public static bool IX0_00 { get; set; }
            public static bool IX0_01 { get; set; }
            public static bool IX0_02 { get; set; }
            public static bool IX0_03 { get; set; }
            public static bool IX0_04 { get; set; }
            public static bool IX0_05 { get; set; }
            public static bool IX0_06 { get; set; }
            public static bool IX0_07 { get; set; }

            public static bool IX1_00 { get; set; }
            public static bool IX1_01 { get; set; }
            public static bool IX1_02 { get; set; }
            public static bool IX1_03 { get; set; }
            public static bool IX1_04 { get; set; }
            public static bool IX1_05 { get; set; }
            public static bool IX1_06 { get; set; }
            public static bool IX1_07 { get; set; }

            public static bool IX2_00 { get; set; }
            public static bool IX2_01 { get; set; }
            public static bool IX2_02 { get; set; }
            public static bool IX2_03 { get; set; }
            public static bool IX2_04 { get; set; }
            public static bool IX2_05 { get; set; }
            public static bool IX2_06 { get; set; }
            public static bool IX2_07 { get; set; }

            public static bool IX3_00 { get; set; }
            public static bool IX3_01 { get; set; }
            public static bool IX3_02 { get; set; }
            public static bool IX3_03 { get; set; }
            public static bool IX3_04 { get; set; }
            public static bool IX3_05 { get; set; }
            public static bool IX3_06 { get; set; }
            public static bool IX3_07 { get; set; }

            public static bool IX4_00 { get; set; }
            public static bool IX4_01 { get; set; }
            public static bool IX4_02 { get; set; }
            public static bool IX4_03 { get; set; }
            public static bool IX4_04 { get; set; }
            public static bool IX4_05 { get; set; }
            public static bool IX4_06 { get; set; }
            public static bool IX4_07 { get; set; }

            public static bool IX5_00 { get; set; }
            public static bool IX5_01 { get; set; }
            public static bool IX5_02 { get; set; }
            public static bool IX5_03 { get; set; }
            public static bool IX5_04 { get; set; }
            public static bool IX5_05 { get; set; }
            public static bool IX5_06 { get; set; }
            public static bool IX5_07 { get; set; }

            public static bool IX6_00 { get; set; }
            public static bool IX6_01 { get; set; }
            public static bool IX6_02 { get; set; }
            public static bool IX6_03 { get; set; }
            public static bool IX6_04 { get; set; }
            public static bool IX6_05 { get; set; }
            public static bool IX6_06 { get; set; }
            public static bool IX6_07 { get; set; }

            public static bool IX7_00 { get; set; }
            public static bool IX7_01 { get; set; }
            public static bool IX7_02 { get; set; }
            public static bool IX7_03 { get; set; }
            public static bool IX7_04 { get; set; }
            public static bool IX7_05 { get; set; }
            public static bool IX7_06 { get; set; }
            public static bool IX7_07 { get; set; }

            public static bool IX8_00 { get; set; }
            public static bool IX8_01 { get; set; }
            public static bool IX8_02 { get; set; }
            public static bool IX8_03 { get; set; }
            public static bool IX8_04 { get; set; }
            public static bool IX8_05 { get; set; }
            public static bool IX8_06 { get; set; }
            public static bool IX8_07 { get; set; }

            public static bool IX9_00 { get; set; }
            public static bool IX9_01 { get; set; }
            public static bool IX9_02 { get; set; }
            public static bool IX9_03 { get; set; }
            public static bool IX9_04 { get; set; }
            public static bool IX9_05 { get; set; }
            public static bool IX9_06 { get; set; }
            public static bool IX9_07 { get; set; }

            public static bool IX10_00 { get; set; }
            public static bool IX10_01 { get; set; }
            public static bool IX10_02 { get; set; }
            public static bool IX10_03 { get; set; }
            public static bool IX10_04 { get; set; }
            public static bool IX10_05 { get; set; }
            public static bool IX10_06 { get; set; }
            public static bool IX10_07 { get; set; }

            public static bool IX11_00 { get; set; }
            public static bool IX11_01 { get; set; }
            public static bool IX11_02 { get; set; }
            public static bool IX11_03 { get; set; }
            public static bool IX11_04 { get; set; }
            public static bool IX11_05 { get; set; }
            public static bool IX11_06 { get; set; }
            public static bool IX11_07 { get; set; }

            public static bool IX12_00 { get; set; }
            public static bool IX12_01 { get; set; }
            public static bool IX12_02 { get; set; }
            public static bool IX12_03 { get; set; }
            public static bool IX12_04 { get; set; }
            public static bool IX12_05 { get; set; }
            public static bool IX12_06 { get; set; }
            public static bool IX12_07 { get; set; }

            public static bool IX13_00 { get; set; }
            public static bool IX13_01 { get; set; }
            public static bool IX13_02 { get; set; }
            public static bool IX13_03 { get; set; }
            public static bool IX13_04 { get; set; }
            public static bool IX13_05 { get; set; }
            public static bool IX13_06 { get; set; }
            public static bool IX13_07 { get; set; }

            public static bool IX14_00 { get; set; }
            public static bool IX14_01 { get; set; }
            public static bool IX14_02 { get; set; }
            public static bool IX14_03 { get; set; }
            public static bool IX14_04 { get; set; }
            public static bool IX14_05 { get; set; }
            public static bool IX14_06 { get; set; }
            public static bool IX14_07 { get; set; }

            public static bool IX15_00 { get; set; }
            public static bool IX15_01 { get; set; }
            public static bool IX15_02 { get; set; }
            public static bool IX15_03 { get; set; }
            public static bool IX15_04 { get; set; }
            public static bool IX15_05 { get; set; }
            public static bool IX15_06 { get; set; }
            public static bool IX15_07 { get; set; }

            public static bool IX16_00 { get; set; }
            public static bool IX16_01 { get; set; }
            public static bool IX16_02 { get; set; }
            public static bool IX16_03 { get; set; }
            public static bool IX16_04 { get; set; }
            public static bool IX16_05 { get; set; }
            public static bool IX16_06 { get; set; }
            public static bool IX16_07 { get; set; }

            public static bool IX17_00 { get; set; }
            public static bool IX17_01 { get; set; }
            public static bool IX17_02 { get; set; }
            public static bool IX17_03 { get; set; }
            public static bool IX17_04 { get; set; }
            public static bool IX17_05 { get; set; }
            public static bool IX17_06 { get; set; }
            public static bool IX17_07 { get; set; }

            public static bool IX18_00 { get; set; }
            public static bool IX18_01 { get; set; }
            public static bool IX18_02 { get; set; }
            public static bool IX18_03 { get; set; }
            public static bool IX18_04 { get; set; }
            public static bool IX18_05 { get; set; }
            public static bool IX18_06 { get; set; }
            public static bool IX18_07 { get; set; }

            public static bool IX19_00 { get; set; }
            public static bool IX19_01 { get; set; }
            public static bool IX19_02 { get; set; }
            public static bool IX19_03 { get; set; }
            public static bool IX19_04 { get; set; }
            public static bool IX19_05 { get; set; }
            public static bool IX19_06 { get; set; }
            public static bool IX19_07 { get; set; }

            public static bool IX20_00 { get; set; }
            public static bool IX20_01 { get; set; }
            public static bool IX20_02 { get; set; }
            public static bool IX20_03 { get; set; }
            public static bool IX20_04 { get; set; }
            public static bool IX20_05 { get; set; }
            public static bool IX20_06 { get; set; }
            public static bool IX20_07 { get; set; }

            public static bool IX21_00 { get; set; }
            public static bool IX21_01 { get; set; }
            public static bool IX21_02 { get; set; }
            public static bool IX21_03 { get; set; }
            public static bool IX21_04 { get; set; }
            public static bool IX21_05 { get; set; }
            public static bool IX21_06 { get; set; }
            public static bool IX21_07 { get; set; }

            public static bool IX22_00 { get; set; }
            public static bool IX22_01 { get; set; }
            public static bool IX22_02 { get; set; }
            public static bool IX22_03 { get; set; }
            public static bool IX22_04 { get; set; }
            public static bool IX22_05 { get; set; }
            public static bool IX22_06 { get; set; }
            public static bool IX22_07 { get; set; }

            public static bool IX23_00 { get; set; }
            public static bool IX23_01 { get; set; }
            public static bool IX23_02 { get; set; }
            public static bool IX23_03 { get; set; }
            public static bool IX23_04 { get; set; }
            public static bool IX23_05 { get; set; }
            public static bool IX23_06 { get; set; }
            public static bool IX23_07 { get; set; }

            public static bool IX24_00 { get; set; }
            public static bool IX24_01 { get; set; }
            public static bool IX24_02 { get; set; }
            public static bool IX24_03 { get; set; }
            public static bool IX24_04 { get; set; }
            public static bool IX24_05 { get; set; }
            public static bool IX24_06 { get; set; }
            public static bool IX24_07 { get; set; }

            public static bool IX25_00 { get; set; }
            public static bool IX25_01 { get; set; }
            public static bool IX25_02 { get; set; }
            public static bool IX25_03 { get; set; }
            public static bool IX25_04 { get; set; }
            public static bool IX25_05 { get; set; }
            public static bool IX25_06 { get; set; }
            public static bool IX25_07 { get; set; }

            public static bool IX26_00 { get; set; }
            public static bool IX26_01 { get; set; }
            public static bool IX26_02 { get; set; }
            public static bool IX26_03 { get; set; }
            public static bool IX26_04 { get; set; }
            public static bool IX26_05 { get; set; }
            public static bool IX26_06 { get; set; }
            public static bool IX26_07 { get; set; }

            public static bool IX27_00 { get; set; }
            public static bool IX27_01 { get; set; }
            public static bool IX27_02 { get; set; }
            public static bool IX27_03 { get; set; }
            public static bool IX27_04 { get; set; }
            public static bool IX27_05 { get; set; }
            public static bool IX27_06 { get; set; }
            public static bool IX27_07 { get; set; }

            public static bool IX28_00 { get; set; }
            public static bool IX28_01 { get; set; }
            public static bool IX28_02 { get; set; }
            public static bool IX28_03 { get; set; }
            public static bool IX28_04 { get; set; }
            public static bool IX28_05 { get; set; }
            public static bool IX28_06 { get; set; }
            public static bool IX28_07 { get; set; }

            public static bool IX29_00 { get; set; }
            public static bool IX29_01 { get; set; }
            public static bool IX29_02 { get; set; }
            public static bool IX29_03 { get; set; }
            public static bool IX29_04 { get; set; }
            public static bool IX29_05 { get; set; }
            public static bool IX29_06 { get; set; }
            public static bool IX29_07 { get; set; }

            public static bool IX30_00 { get; set; }
            public static bool IX30_01 { get; set; }
            public static bool IX30_02 { get; set; }
            public static bool IX30_03 { get; set; }
            public static bool IX30_04 { get; set; }
            public static bool IX30_05 { get; set; }
            public static bool IX30_06 { get; set; }
            public static bool IX30_07 { get; set; }

            public static bool IX31_00 { get; set; }
            public static bool IX31_01 { get; set; }
            public static bool IX31_02 { get; set; }
            public static bool IX31_03 { get; set; }
            public static bool IX31_04 { get; set; }
            public static bool IX31_05 { get; set; }
            public static bool IX31_06 { get; set; }
            public static bool IX31_07 { get; set; }

            public static bool IX32_00 { get; set; }
            public static bool IX32_01 { get; set; }
            public static bool IX32_02 { get; set; }
            public static bool IX32_03 { get; set; }
            public static bool IX32_04 { get; set; }
            public static bool IX32_05 { get; set; }
            public static bool IX32_06 { get; set; }
            public static bool IX32_07 { get; set; }

            public static bool IX33_00 { get; set; }
            public static bool IX33_01 { get; set; }
            public static bool IX33_02 { get; set; }
            public static bool IX33_03 { get; set; }
            public static bool IX33_04 { get; set; }
            public static bool IX33_05 { get; set; }
            public static bool IX33_06 { get; set; }
            public static bool IX33_07 { get; set; }

            public static bool IX34_00 { get; set; }
            public static bool IX34_01 { get; set; }
            public static bool IX34_02 { get; set; }
            public static bool IX34_03 { get; set; }
            public static bool IX34_04 { get; set; }
            public static bool IX34_05 { get; set; }
            public static bool IX34_06 { get; set; }
            public static bool IX34_07 { get; set; }

            public static bool IX35_00 { get; set; }
            public static bool IX35_01 { get; set; }
            public static bool IX35_02 { get; set; }
            public static bool IX35_03 { get; set; }
            public static bool IX35_04 { get; set; }
            public static bool IX35_05 { get; set; }
            public static bool IX35_06 { get; set; }
            public static bool IX35_07 { get; set; }

            public static bool IX36_00 { get; set; }
            public static bool IX36_01 { get; set; }
            public static bool IX36_02 { get; set; }
            public static bool IX36_03 { get; set; }
            public static bool IX36_04 { get; set; }
            public static bool IX36_05 { get; set; }
            public static bool IX36_06 { get; set; }
            public static bool IX36_07 { get; set; }

            public static bool IX37_00 { get; set; }
            public static bool IX37_01 { get; set; }
            public static bool IX37_02 { get; set; }
            public static bool IX37_03 { get; set; }
            public static bool IX37_04 { get; set; }
            public static bool IX37_05 { get; set; }
            public static bool IX37_06 { get; set; }
            public static bool IX37_07 { get; set; }

            public static bool IX38_00 { get; set; }
            public static bool IX38_01 { get; set; }
            public static bool IX38_02 { get; set; }
            public static bool IX38_03 { get; set; }
            public static bool IX38_04 { get; set; }
            public static bool IX38_05 { get; set; }
            public static bool IX38_06 { get; set; }
            public static bool IX38_07 { get; set; }

            public static bool IX39_00 { get; set; }
            public static bool IX39_01 { get; set; }
            public static bool IX39_02 { get; set; }
            public static bool IX39_03 { get; set; }
            public static bool IX39_04 { get; set; }
            public static bool IX39_05 { get; set; }
            public static bool IX39_06 { get; set; }
            public static bool IX39_07 { get; set; }

            public static bool IX40_00 { get; set; }
            public static bool IX40_01 { get; set; }
            public static bool IX40_02 { get; set; }
            public static bool IX40_03 { get; set; }
            public static bool IX40_04 { get; set; }
            public static bool IX40_05 { get; set; }
            public static bool IX40_06 { get; set; }
            public static bool IX40_07 { get; set; }

            public static bool IX41_00 { get; set; }
            public static bool IX41_01 { get; set; }
            public static bool IX41_02 { get; set; }
            public static bool IX41_03 { get; set; }
            public static bool IX41_04 { get; set; }
            public static bool IX41_05 { get; set; }
            public static bool IX41_06 { get; set; }
            public static bool IX41_07 { get; set; }

            public static bool IX42_00 { get; set; }
            public static bool IX42_01 { get; set; }
            public static bool IX42_02 { get; set; }
            public static bool IX42_03 { get; set; }
            public static bool IX42_04 { get; set; }
            public static bool IX42_05 { get; set; }
            public static bool IX42_06 { get; set; }
            public static bool IX42_07 { get; set; }

            public static bool IX43_00 { get; set; }
            public static bool IX43_01 { get; set; }
            public static bool IX43_02 { get; set; }
            public static bool IX43_03 { get; set; }
            public static bool IX43_04 { get; set; }
            public static bool IX43_05 { get; set; }
            public static bool IX43_06 { get; set; }
            public static bool IX43_07 { get; set; }

            public static bool IX44_00 { get; set; }
            public static bool IX44_01 { get; set; }
            public static bool IX44_02 { get; set; }
            public static bool IX44_03 { get; set; }
            public static bool IX44_04 { get; set; }
            public static bool IX44_05 { get; set; }
            public static bool IX44_06 { get; set; }
            public static bool IX44_07 { get; set; }

            public static bool IX45_00 { get; set; }
            public static bool IX45_01 { get; set; }
            public static bool IX45_02 { get; set; }
            public static bool IX45_03 { get; set; }
            public static bool IX45_04 { get; set; }
            public static bool IX45_05 { get; set; }
            public static bool IX45_06 { get; set; }
            public static bool IX45_07 { get; set; }

            public static bool IX46_00 { get; set; }
            public static bool IX46_01 { get; set; }
            public static bool IX46_02 { get; set; }
            public static bool IX46_03 { get; set; }
            public static bool IX46_04 { get; set; }
            public static bool IX46_05 { get; set; }
            public static bool IX46_06 { get; set; }
            public static bool IX46_07 { get; set; }

            public static bool IX47_00 { get; set; }
            public static bool IX47_01 { get; set; }
            public static bool IX47_02 { get; set; }
            public static bool IX47_03 { get; set; }
            public static bool IX47_04 { get; set; }
            public static bool IX47_05 { get; set; }
            public static bool IX47_06 { get; set; }
            public static bool IX47_07 { get; set; }

            public static bool IX48_00 { get; set; }
            public static bool IX48_01 { get; set; }
            public static bool IX48_02 { get; set; }
            public static bool IX48_03 { get; set; }
            public static bool IX48_04 { get; set; }
            public static bool IX48_05 { get; set; }
            public static bool IX48_06 { get; set; }
            public static bool IX48_07 { get; set; }

            public static bool IX49_00 { get; set; }
            public static bool IX49_01 { get; set; }
            public static bool IX49_02 { get; set; }
            public static bool IX49_03 { get; set; }
            public static bool IX49_04 { get; set; }
            public static bool IX49_05 { get; set; }
            public static bool IX49_06 { get; set; }
            public static bool IX49_07 { get; set; }

            public static bool IX50_00 { get; set; }
            public static bool IX50_01 { get; set; }
            public static bool IX50_02 { get; set; }
            public static bool IX50_03 { get; set; }
            public static bool IX50_04 { get; set; }
            public static bool IX50_05 { get; set; }
            public static bool IX50_06 { get; set; }
            public static bool IX50_07 { get; set; }

            public static bool IX51_00 { get; set; }
            public static bool IX51_01 { get; set; }
            public static bool IX51_02 { get; set; }
            public static bool IX51_03 { get; set; }
            public static bool IX51_04 { get; set; }
            public static bool IX51_05 { get; set; }
            public static bool IX51_06 { get; set; }
            public static bool IX51_07 { get; set; }

            public static bool IX52_00 { get; set; }
            public static bool IX52_01 { get; set; }
            public static bool IX52_02 { get; set; }
            public static bool IX52_03 { get; set; }
            public static bool IX52_04 { get; set; }
            public static bool IX52_05 { get; set; }
            public static bool IX52_06 { get; set; }
            public static bool IX52_07 { get; set; }

            public static bool IX53_00 { get; set; }
            public static bool IX53_01 { get; set; }
            public static bool IX53_02 { get; set; }
            public static bool IX53_03 { get; set; }
            public static bool IX53_04 { get; set; }
            public static bool IX53_05 { get; set; }
            public static bool IX53_06 { get; set; }
            public static bool IX53_07 { get; set; }

            public static bool IX54_00 { get; set; }
            public static bool IX54_01 { get; set; }
            public static bool IX54_02 { get; set; }
            public static bool IX54_03 { get; set; }
            public static bool IX54_04 { get; set; }
            public static bool IX54_05 { get; set; }
            public static bool IX54_06 { get; set; }
            public static bool IX54_07 { get; set; }

            public static bool IX55_00 { get; set; }
            public static bool IX55_01 { get; set; }
            public static bool IX55_02 { get; set; }
            public static bool IX55_03 { get; set; }
            public static bool IX55_04 { get; set; }
            public static bool IX55_05 { get; set; }
            public static bool IX55_06 { get; set; }
            public static bool IX55_07 { get; set; }

            public static bool IX56_00 { get; set; }
            public static bool IX56_01 { get; set; }
            public static bool IX56_02 { get; set; }
            public static bool IX56_03 { get; set; }
            public static bool IX56_04 { get; set; }
            public static bool IX56_05 { get; set; }
            public static bool IX56_06 { get; set; }
            public static bool IX56_07 { get; set; }

            public static bool IX57_00 { get; set; }
            public static bool IX57_01 { get; set; }
            public static bool IX57_02 { get; set; }
            public static bool IX57_03 { get; set; }
            public static bool IX57_04 { get; set; }
            public static bool IX57_05 { get; set; }
            public static bool IX57_06 { get; set; }
            public static bool IX57_07 { get; set; }

            public static bool IX58_00 { get; set; }
            public static bool IX58_01 { get; set; }
            public static bool IX58_02 { get; set; }
            public static bool IX58_03 { get; set; }
            public static bool IX58_04 { get; set; }
            public static bool IX58_05 { get; set; }
            public static bool IX58_06 { get; set; }
            public static bool IX58_07 { get; set; }

            public static bool IX59_00 { get; set; }
            public static bool IX59_01 { get; set; }
            public static bool IX59_02 { get; set; }
            public static bool IX59_03 { get; set; }
            public static bool IX59_04 { get; set; }
            public static bool IX59_05 { get; set; }
            public static bool IX59_06 { get; set; }
            public static bool IX59_07 { get; set; }

            public static bool IX60_00 { get; set; }
            public static bool IX60_01 { get; set; }
            public static bool IX60_02 { get; set; }
            public static bool IX60_03 { get; set; }
            public static bool IX60_04 { get; set; }
            public static bool IX60_05 { get; set; }
            public static bool IX60_06 { get; set; }
            public static bool IX60_07 { get; set; }

            public static bool IX61_00 { get; set; }
            public static bool IX61_01 { get; set; }
            public static bool IX61_02 { get; set; }
            public static bool IX61_03 { get; set; }
            public static bool IX61_04 { get; set; }
            public static bool IX61_05 { get; set; }
            public static bool IX61_06 { get; set; }
            public static bool IX61_07 { get; set; }

            public static bool IX62_00 { get; set; }
            public static bool IX62_01 { get; set; }
            public static bool IX62_02 { get; set; }
            public static bool IX62_03 { get; set; }
            public static bool IX62_04 { get; set; }
            public static bool IX62_05 { get; set; }
            public static bool IX62_06 { get; set; }
            public static bool IX62_07 { get; set; }

            public static bool IX63_00 { get; set; }
            public static bool IX63_01 { get; set; }
            public static bool IX63_02 { get; set; }
            public static bool IX63_03 { get; set; }
            public static bool IX63_04 { get; set; }
            public static bool IX63_05 { get; set; }
            public static bool IX63_06 { get; set; }
            public static bool IX63_07 { get; set; }

            public static bool IX64_00 { get; set; }
            public static bool IX64_01 { get; set; }
            public static bool IX64_02 { get; set; }
            public static bool IX64_03 { get; set; }
            public static bool IX64_04 { get; set; }
            public static bool IX64_05 { get; set; }
            public static bool IX64_06 { get; set; }
            public static bool IX64_07 { get; set; }

            public static bool IX65_00 { get; set; }
            public static bool IX65_01 { get; set; }
            public static bool IX65_02 { get; set; }
            public static bool IX65_03 { get; set; }
            public static bool IX65_04 { get; set; }
            public static bool IX65_05 { get; set; }
            public static bool IX65_06 { get; set; }
            public static bool IX65_07 { get; set; }

            public static bool IX66_00 { get; set; }
            public static bool IX66_01 { get; set; }
            public static bool IX66_02 { get; set; }
            public static bool IX66_03 { get; set; }
            public static bool IX66_04 { get; set; }
            public static bool IX66_05 { get; set; }
            public static bool IX66_06 { get; set; }
            public static bool IX66_07 { get; set; }

            public static bool IX67_00 { get; set; }
            public static bool IX67_01 { get; set; }
            public static bool IX67_02 { get; set; }
            public static bool IX67_03 { get; set; }
            public static bool IX67_04 { get; set; }
            public static bool IX67_05 { get; set; }
            public static bool IX67_06 { get; set; }
            public static bool IX67_07 { get; set; }

            public static bool IX68_00 { get; set; }
            public static bool IX68_01 { get; set; }
            public static bool IX68_02 { get; set; }
            public static bool IX68_03 { get; set; }
            public static bool IX68_04 { get; set; }
            public static bool IX68_05 { get; set; }
            public static bool IX68_06 { get; set; }
            public static bool IX68_07 { get; set; }

            public static bool IX69_00 { get; set; }
            public static bool IX69_01 { get; set; }
            public static bool IX69_02 { get; set; }
            public static bool IX69_03 { get; set; }
            public static bool IX69_04 { get; set; }
            public static bool IX69_05 { get; set; }
            public static bool IX69_06 { get; set; }
            public static bool IX69_07 { get; set; }

            public static bool IX70_00 { get; set; }
            public static bool IX70_01 { get; set; }
            public static bool IX70_02 { get; set; }
            public static bool IX70_03 { get; set; }
            public static bool IX70_04 { get; set; }
            public static bool IX70_05 { get; set; }
            public static bool IX70_06 { get; set; }
            public static bool IX70_07 { get; set; }

            public static bool IX71_00 { get; set; }
            public static bool IX71_01 { get; set; }
            public static bool IX71_02 { get; set; }
            public static bool IX71_03 { get; set; }
            public static bool IX71_04 { get; set; }
            public static bool IX71_05 { get; set; }
            public static bool IX71_06 { get; set; }
            public static bool IX71_07 { get; set; }

            public static bool IX72_00 { get; set; }
            public static bool IX72_01 { get; set; }
            public static bool IX72_02 { get; set; }
            public static bool IX72_03 { get; set; }
            public static bool IX72_04 { get; set; }
            public static bool IX72_05 { get; set; }
            public static bool IX72_06 { get; set; }
            public static bool IX72_07 { get; set; }

            public static bool IX73_00 { get; set; }
            public static bool IX73_01 { get; set; }
            public static bool IX73_02 { get; set; }
            public static bool IX73_03 { get; set; }
            public static bool IX73_04 { get; set; }
            public static bool IX73_05 { get; set; }
            public static bool IX73_06 { get; set; }
            public static bool IX73_07 { get; set; }

            public static bool IX74_00 { get; set; }
            public static bool IX74_01 { get; set; }
            public static bool IX74_02 { get; set; }
            public static bool IX74_03 { get; set; }
            public static bool IX74_04 { get; set; }
            public static bool IX74_05 { get; set; }
            public static bool IX74_06 { get; set; }
            public static bool IX74_07 { get; set; }

            public static bool IX75_00 { get; set; }
            public static bool IX75_01 { get; set; }
            public static bool IX75_02 { get; set; }
            public static bool IX75_03 { get; set; }
            public static bool IX75_04 { get; set; }
            public static bool IX75_05 { get; set; }
            public static bool IX75_06 { get; set; }
            public static bool IX75_07 { get; set; }

            public static bool IX76_00 { get; set; }
            public static bool IX76_01 { get; set; }
            public static bool IX76_02 { get; set; }
            public static bool IX76_03 { get; set; }
            public static bool IX76_04 { get; set; }
            public static bool IX76_05 { get; set; }
            public static bool IX76_06 { get; set; }
            public static bool IX76_07 { get; set; }

            public static bool IX77_00 { get; set; }
            public static bool IX77_01 { get; set; }
            public static bool IX77_02 { get; set; }
            public static bool IX77_03 { get; set; }
            public static bool IX77_04 { get; set; }
            public static bool IX77_05 { get; set; }
            public static bool IX77_06 { get; set; }
            public static bool IX77_07 { get; set; }

            public static bool IX78_00 { get; set; }
            public static bool IX78_01 { get; set; }
            public static bool IX78_02 { get; set; }
            public static bool IX78_03 { get; set; }
            public static bool IX78_04 { get; set; }
            public static bool IX78_05 { get; set; }
            public static bool IX78_06 { get; set; }
            public static bool IX78_07 { get; set; }

            public static bool IX79_00 { get; set; }
            public static bool IX79_01 { get; set; }
            public static bool IX79_02 { get; set; }
            public static bool IX79_03 { get; set; }
            public static bool IX79_04 { get; set; }
            public static bool IX79_05 { get; set; }
            public static bool IX79_06 { get; set; }
            public static bool IX79_07 { get; set; }

            public static bool IX80_00 { get; set; }
            public static bool IX80_01 { get; set; }
            public static bool IX80_02 { get; set; }
            public static bool IX80_03 { get; set; }
            public static bool IX80_04 { get; set; }
            public static bool IX80_05 { get; set; }
            public static bool IX80_06 { get; set; }
            public static bool IX80_07 { get; set; }

            public static bool IX81_00 { get; set; }
            public static bool IX81_01 { get; set; }
            public static bool IX81_02 { get; set; }
            public static bool IX81_03 { get; set; }
            public static bool IX81_04 { get; set; }
            public static bool IX81_05 { get; set; }
            public static bool IX81_06 { get; set; }
            public static bool IX81_07 { get; set; }

            public static bool IX82_00 { get; set; }
            public static bool IX82_01 { get; set; }
            public static bool IX82_02 { get; set; }
            public static bool IX82_03 { get; set; }
            public static bool IX82_04 { get; set; }
            public static bool IX82_05 { get; set; }
            public static bool IX82_06 { get; set; }
            public static bool IX82_07 { get; set; }

            public static bool IX83_00 { get; set; }
            public static bool IX83_01 { get; set; }
            public static bool IX83_02 { get; set; }
            public static bool IX83_03 { get; set; }
            public static bool IX83_04 { get; set; }
            public static bool IX83_05 { get; set; }
            public static bool IX83_06 { get; set; }
            public static bool IX83_07 { get; set; }

            public static bool IX84_00 { get; set; }
            public static bool IX84_01 { get; set; }
            public static bool IX84_02 { get; set; }
            public static bool IX84_03 { get; set; }
            public static bool IX84_04 { get; set; }
            public static bool IX84_05 { get; set; }
            public static bool IX84_06 { get; set; }
            public static bool IX84_07 { get; set; }

            public static bool IX85_00 { get; set; }
            public static bool IX85_01 { get; set; }
            public static bool IX85_02 { get; set; }
            public static bool IX85_03 { get; set; }
            public static bool IX85_04 { get; set; }
            public static bool IX85_05 { get; set; }
            public static bool IX85_06 { get; set; }
            public static bool IX85_07 { get; set; }

            public static bool IX86_00 { get; set; }
            public static bool IX86_01 { get; set; }
            public static bool IX86_02 { get; set; }
            public static bool IX86_03 { get; set; }
            public static bool IX86_04 { get; set; }
            public static bool IX86_05 { get; set; }
            public static bool IX86_06 { get; set; }
            public static bool IX86_07 { get; set; }

            public static bool IX87_00 { get; set; }
            public static bool IX87_01 { get; set; }
            public static bool IX87_02 { get; set; }
            public static bool IX87_03 { get; set; }
            public static bool IX87_04 { get; set; }
            public static bool IX87_05 { get; set; }
            public static bool IX87_06 { get; set; }
            public static bool IX87_07 { get; set; }

            public static bool IX88_00 { get; set; }
            public static bool IX88_01 { get; set; }
            public static bool IX88_02 { get; set; }
            public static bool IX88_03 { get; set; }
            public static bool IX88_04 { get; set; }
            public static bool IX88_05 { get; set; }
            public static bool IX88_06 { get; set; }
            public static bool IX88_07 { get; set; }

            public static bool IX89_00 { get; set; }
            public static bool IX89_01 { get; set; }
            public static bool IX89_02 { get; set; }
            public static bool IX89_03 { get; set; }
            public static bool IX89_04 { get; set; }
            public static bool IX89_05 { get; set; }
            public static bool IX89_06 { get; set; }
            public static bool IX89_07 { get; set; }

            public static bool IX90_00 { get; set; }
            public static bool IX90_01 { get; set; }
            public static bool IX90_02 { get; set; }
            public static bool IX90_03 { get; set; }
            public static bool IX90_04 { get; set; }
            public static bool IX90_05 { get; set; }
            public static bool IX90_06 { get; set; }
            public static bool IX90_07 { get; set; }

            public static bool IX91_00 { get; set; }
            public static bool IX91_01 { get; set; }
            public static bool IX91_02 { get; set; }
            public static bool IX91_03 { get; set; }
            public static bool IX91_04 { get; set; }
            public static bool IX91_05 { get; set; }
            public static bool IX91_06 { get; set; }
            public static bool IX91_07 { get; set; }

            public static bool IX92_00 { get; set; }
            public static bool IX92_01 { get; set; }
            public static bool IX92_02 { get; set; }
            public static bool IX92_03 { get; set; }
            public static bool IX92_04 { get; set; }
            public static bool IX92_05 { get; set; }
            public static bool IX92_06 { get; set; }
            public static bool IX92_07 { get; set; }

            public static bool IX93_00 { get; set; }
            public static bool IX93_01 { get; set; }
            public static bool IX93_02 { get; set; }
            public static bool IX93_03 { get; set; }
            public static bool IX93_04 { get; set; }
            public static bool IX93_05 { get; set; }
            public static bool IX93_06 { get; set; }
            public static bool IX93_07 { get; set; }

            public static bool IX94_00 { get; set; }
            public static bool IX94_01 { get; set; }
            public static bool IX94_02 { get; set; }
            public static bool IX94_03 { get; set; }
            public static bool IX94_04 { get; set; }
            public static bool IX94_05 { get; set; }
            public static bool IX94_06 { get; set; }
            public static bool IX94_07 { get; set; }

            public static bool IX95_00 { get; set; }
            public static bool IX95_01 { get; set; }
            public static bool IX95_02 { get; set; }
            public static bool IX95_03 { get; set; }
            public static bool IX95_04 { get; set; }
            public static bool IX95_05 { get; set; }
            public static bool IX95_06 { get; set; }
            public static bool IX95_07 { get; set; }

            public static bool IX96_00 { get; set; }
            public static bool IX96_01 { get; set; }
            public static bool IX96_02 { get; set; }
            public static bool IX96_03 { get; set; }
            public static bool IX96_04 { get; set; }
            public static bool IX96_05 { get; set; }
            public static bool IX96_06 { get; set; }
            public static bool IX96_07 { get; set; }

            public static bool IX97_00 { get; set; }
            public static bool IX97_01 { get; set; }
            public static bool IX97_02 { get; set; }
            public static bool IX97_03 { get; set; }
            public static bool IX97_04 { get; set; }
            public static bool IX97_05 { get; set; }
            public static bool IX97_06 { get; set; }
            public static bool IX97_07 { get; set; }

            public static bool IX98_00 { get; set; }
            public static bool IX98_01 { get; set; }
            public static bool IX98_02 { get; set; }
            public static bool IX98_03 { get; set; }
            public static bool IX98_04 { get; set; }
            public static bool IX98_05 { get; set; }
            public static bool IX98_06 { get; set; }
            public static bool IX98_07 { get; set; }

            public static bool IX99_00 { get; set; }
            public static bool IX99_01 { get; set; }
            public static bool IX99_02 { get; set; }
            public static bool IX99_03 { get; set; }
            public static bool IX99_04 { get; set; }
            public static bool IX99_05 { get; set; }
            public static bool IX99_06 { get; set; }
            public static bool IX99_07 { get; set; }

            public static bool IX100_00 { get; set; }
            public static bool IX100_01 { get; set; }
            public static bool IX100_02 { get; set; }
            public static bool IX100_03 { get; set; }
            public static bool IX100_04 { get; set; }
            public static bool IX100_05 { get; set; }
            public static bool IX100_06 { get; set; }
            public static bool IX100_07 { get; set; }

        }

        public struct Output
        {
            public static bool QX0_00 { get; set; }
            public static bool QX0_01 { get; set; }
            public static bool QX0_02 { get; set; }
            public static bool QX0_03 { get; set; }
            public static bool QX0_04 { get; set; }
            public static bool QX0_05 { get; set; }
            public static bool QX0_06 { get; set; }
            public static bool QX0_07 { get; set; }

            public static bool QX1_00 { get; set; }
            public static bool QX1_01 { get; set; }
            public static bool QX1_02 { get; set; }
            public static bool QX1_03 { get; set; }
            public static bool QX1_04 { get; set; }
            public static bool QX1_05 { get; set; }
            public static bool QX1_06 { get; set; }
            public static bool QX1_07 { get; set; }

            public static bool QX2_00 { get; set; }
            public static bool QX2_01 { get; set; }
            public static bool QX2_02 { get; set; }
            public static bool QX2_03 { get; set; }
            public static bool QX2_04 { get; set; }
            public static bool QX2_05 { get; set; }
            public static bool QX2_06 { get; set; }
            public static bool QX2_07 { get; set; }

            public static bool QX3_00 { get; set; }
            public static bool QX3_01 { get; set; }
            public static bool QX3_02 { get; set; }
            public static bool QX3_03 { get; set; }
            public static bool QX3_04 { get; set; }
            public static bool QX3_05 { get; set; }
            public static bool QX3_06 { get; set; }
            public static bool QX3_07 { get; set; }

            public static bool QX4_00 { get; set; }
            public static bool QX4_01 { get; set; }
            public static bool QX4_02 { get; set; }
            public static bool QX4_03 { get; set; }
            public static bool QX4_04 { get; set; }
            public static bool QX4_05 { get; set; }
            public static bool QX4_06 { get; set; }
            public static bool QX4_07 { get; set; }

            public static bool QX5_00 { get; set; }
            public static bool QX5_01 { get; set; }
            public static bool QX5_02 { get; set; }
            public static bool QX5_03 { get; set; }
            public static bool QX5_04 { get; set; }
            public static bool QX5_05 { get; set; }
            public static bool QX5_06 { get; set; }
            public static bool QX5_07 { get; set; }

            public static bool QX6_00 { get; set; }
            public static bool QX6_01 { get; set; }
            public static bool QX6_02 { get; set; }
            public static bool QX6_03 { get; set; }
            public static bool QX6_04 { get; set; }
            public static bool QX6_05 { get; set; }
            public static bool QX6_06 { get; set; }
            public static bool QX6_07 { get; set; }

            public static bool QX7_00 { get; set; }
            public static bool QX7_01 { get; set; }
            public static bool QX7_02 { get; set; }
            public static bool QX7_03 { get; set; }
            public static bool QX7_04 { get; set; }
            public static bool QX7_05 { get; set; }
            public static bool QX7_06 { get; set; }
            public static bool QX7_07 { get; set; }

            public static bool QX8_00 { get; set; }
            public static bool QX8_01 { get; set; }
            public static bool QX8_02 { get; set; }
            public static bool QX8_03 { get; set; }
            public static bool QX8_04 { get; set; }
            public static bool QX8_05 { get; set; }
            public static bool QX8_06 { get; set; }
            public static bool QX8_07 { get; set; }

            public static bool QX9_00 { get; set; }
            public static bool QX9_01 { get; set; }
            public static bool QX9_02 { get; set; }
            public static bool QX9_03 { get; set; }
            public static bool QX9_04 { get; set; }
            public static bool QX9_05 { get; set; }
            public static bool QX9_06 { get; set; }
            public static bool QX9_07 { get; set; }

            public static bool QX10_00 { get; set; }
            public static bool QX10_01 { get; set; }
            public static bool QX10_02 { get; set; }
            public static bool QX10_03 { get; set; }
            public static bool QX10_04 { get; set; }
            public static bool QX10_05 { get; set; }
            public static bool QX10_06 { get; set; }
            public static bool QX10_07 { get; set; }

            public static bool QX11_00 { get; set; }
            public static bool QX11_01 { get; set; }
            public static bool QX11_02 { get; set; }
            public static bool QX11_03 { get; set; }
            public static bool QX11_04 { get; set; }
            public static bool QX11_05 { get; set; }
            public static bool QX11_06 { get; set; }
            public static bool QX11_07 { get; set; }

            public static bool QX12_00 { get; set; }
            public static bool QX12_01 { get; set; }
            public static bool QX12_02 { get; set; }
            public static bool QX12_03 { get; set; }
            public static bool QX12_04 { get; set; }
            public static bool QX12_05 { get; set; }
            public static bool QX12_06 { get; set; }
            public static bool QX12_07 { get; set; }

            public static bool QX13_00 { get; set; }
            public static bool QX13_01 { get; set; }
            public static bool QX13_02 { get; set; }
            public static bool QX13_03 { get; set; }
            public static bool QX13_04 { get; set; }
            public static bool QX13_05 { get; set; }
            public static bool QX13_06 { get; set; }
            public static bool QX13_07 { get; set; }

            public static bool QX14_00 { get; set; }
            public static bool QX14_01 { get; set; }
            public static bool QX14_02 { get; set; }
            public static bool QX14_03 { get; set; }
            public static bool QX14_04 { get; set; }
            public static bool QX14_05 { get; set; }
            public static bool QX14_06 { get; set; }
            public static bool QX14_07 { get; set; }

            public static bool QX15_00 { get; set; }
            public static bool QX15_01 { get; set; }
            public static bool QX15_02 { get; set; }
            public static bool QX15_03 { get; set; }
            public static bool QX15_04 { get; set; }
            public static bool QX15_05 { get; set; }
            public static bool QX15_06 { get; set; }
            public static bool QX15_07 { get; set; }

            public static bool QX16_00 { get; set; }
            public static bool QX16_01 { get; set; }
            public static bool QX16_02 { get; set; }
            public static bool QX16_03 { get; set; }
            public static bool QX16_04 { get; set; }
            public static bool QX16_05 { get; set; }
            public static bool QX16_06 { get; set; }
            public static bool QX16_07 { get; set; }

            public static bool QX17_00 { get; set; }
            public static bool QX17_01 { get; set; }
            public static bool QX17_02 { get; set; }
            public static bool QX17_03 { get; set; }
            public static bool QX17_04 { get; set; }
            public static bool QX17_05 { get; set; }
            public static bool QX17_06 { get; set; }
            public static bool QX17_07 { get; set; }

            public static bool QX18_00 { get; set; }
            public static bool QX18_01 { get; set; }
            public static bool QX18_02 { get; set; }
            public static bool QX18_03 { get; set; }
            public static bool QX18_04 { get; set; }
            public static bool QX18_05 { get; set; }
            public static bool QX18_06 { get; set; }
            public static bool QX18_07 { get; set; }

            public static bool QX19_00 { get; set; }
            public static bool QX19_01 { get; set; }
            public static bool QX19_02 { get; set; }
            public static bool QX19_03 { get; set; }
            public static bool QX19_04 { get; set; }
            public static bool QX19_05 { get; set; }
            public static bool QX19_06 { get; set; }
            public static bool QX19_07 { get; set; }

            public static bool QX20_00 { get; set; }
            public static bool QX20_01 { get; set; }
            public static bool QX20_02 { get; set; }
            public static bool QX20_03 { get; set; }
            public static bool QX20_04 { get; set; }
            public static bool QX20_05 { get; set; }
            public static bool QX20_06 { get; set; }
            public static bool QX20_07 { get; set; }

            public static bool QX21_00 { get; set; }
            public static bool QX21_01 { get; set; }
            public static bool QX21_02 { get; set; }
            public static bool QX21_03 { get; set; }
            public static bool QX21_04 { get; set; }
            public static bool QX21_05 { get; set; }
            public static bool QX21_06 { get; set; }
            public static bool QX21_07 { get; set; }

            public static bool QX22_00 { get; set; }
            public static bool QX22_01 { get; set; }
            public static bool QX22_02 { get; set; }
            public static bool QX22_03 { get; set; }
            public static bool QX22_04 { get; set; }
            public static bool QX22_05 { get; set; }
            public static bool QX22_06 { get; set; }
            public static bool QX22_07 { get; set; }

            public static bool QX23_00 { get; set; }
            public static bool QX23_01 { get; set; }
            public static bool QX23_02 { get; set; }
            public static bool QX23_03 { get; set; }
            public static bool QX23_04 { get; set; }
            public static bool QX23_05 { get; set; }
            public static bool QX23_06 { get; set; }
            public static bool QX23_07 { get; set; }

            public static bool QX24_00 { get; set; }
            public static bool QX24_01 { get; set; }
            public static bool QX24_02 { get; set; }
            public static bool QX24_03 { get; set; }
            public static bool QX24_04 { get; set; }
            public static bool QX24_05 { get; set; }
            public static bool QX24_06 { get; set; }
            public static bool QX24_07 { get; set; }

            public static bool QX25_00 { get; set; }
            public static bool QX25_01 { get; set; }
            public static bool QX25_02 { get; set; }
            public static bool QX25_03 { get; set; }
            public static bool QX25_04 { get; set; }
            public static bool QX25_05 { get; set; }
            public static bool QX25_06 { get; set; }
            public static bool QX25_07 { get; set; }

            public static bool QX26_00 { get; set; }
            public static bool QX26_01 { get; set; }
            public static bool QX26_02 { get; set; }
            public static bool QX26_03 { get; set; }
            public static bool QX26_04 { get; set; }
            public static bool QX26_05 { get; set; }
            public static bool QX26_06 { get; set; }
            public static bool QX26_07 { get; set; }

            public static bool QX27_00 { get; set; }
            public static bool QX27_01 { get; set; }
            public static bool QX27_02 { get; set; }
            public static bool QX27_03 { get; set; }
            public static bool QX27_04 { get; set; }
            public static bool QX27_05 { get; set; }
            public static bool QX27_06 { get; set; }
            public static bool QX27_07 { get; set; }

            public static bool QX28_00 { get; set; }
            public static bool QX28_01 { get; set; }
            public static bool QX28_02 { get; set; }
            public static bool QX28_03 { get; set; }
            public static bool QX28_04 { get; set; }
            public static bool QX28_05 { get; set; }
            public static bool QX28_06 { get; set; }
            public static bool QX28_07 { get; set; }

            public static bool QX29_00 { get; set; }
            public static bool QX29_01 { get; set; }
            public static bool QX29_02 { get; set; }
            public static bool QX29_03 { get; set; }
            public static bool QX29_04 { get; set; }
            public static bool QX29_05 { get; set; }
            public static bool QX29_06 { get; set; }
            public static bool QX29_07 { get; set; }

            public static bool QX30_00 { get; set; }
            public static bool QX30_01 { get; set; }
            public static bool QX30_02 { get; set; }
            public static bool QX30_03 { get; set; }
            public static bool QX30_04 { get; set; }
            public static bool QX30_05 { get; set; }
            public static bool QX30_06 { get; set; }
            public static bool QX30_07 { get; set; }

            public static bool QX31_00 { get; set; }
            public static bool QX31_01 { get; set; }
            public static bool QX31_02 { get; set; }
            public static bool QX31_03 { get; set; }
            public static bool QX31_04 { get; set; }
            public static bool QX31_05 { get; set; }
            public static bool QX31_06 { get; set; }
            public static bool QX31_07 { get; set; }

            public static bool QX32_00 { get; set; }
            public static bool QX32_01 { get; set; }
            public static bool QX32_02 { get; set; }
            public static bool QX32_03 { get; set; }
            public static bool QX32_04 { get; set; }
            public static bool QX32_05 { get; set; }
            public static bool QX32_06 { get; set; }
            public static bool QX32_07 { get; set; }

            public static bool QX33_00 { get; set; }
            public static bool QX33_01 { get; set; }
            public static bool QX33_02 { get; set; }
            public static bool QX33_03 { get; set; }
            public static bool QX33_04 { get; set; }
            public static bool QX33_05 { get; set; }
            public static bool QX33_06 { get; set; }
            public static bool QX33_07 { get; set; }

            public static bool QX34_00 { get; set; }
            public static bool QX34_01 { get; set; }
            public static bool QX34_02 { get; set; }
            public static bool QX34_03 { get; set; }
            public static bool QX34_04 { get; set; }
            public static bool QX34_05 { get; set; }
            public static bool QX34_06 { get; set; }
            public static bool QX34_07 { get; set; }

            public static bool QX35_00 { get; set; }
            public static bool QX35_01 { get; set; }
            public static bool QX35_02 { get; set; }
            public static bool QX35_03 { get; set; }
            public static bool QX35_04 { get; set; }
            public static bool QX35_05 { get; set; }
            public static bool QX35_06 { get; set; }
            public static bool QX35_07 { get; set; }

            public static bool QX36_00 { get; set; }
            public static bool QX36_01 { get; set; }
            public static bool QX36_02 { get; set; }
            public static bool QX36_03 { get; set; }
            public static bool QX36_04 { get; set; }
            public static bool QX36_05 { get; set; }
            public static bool QX36_06 { get; set; }
            public static bool QX36_07 { get; set; }

            public static bool QX37_00 { get; set; }
            public static bool QX37_01 { get; set; }
            public static bool QX37_02 { get; set; }
            public static bool QX37_03 { get; set; }
            public static bool QX37_04 { get; set; }
            public static bool QX37_05 { get; set; }
            public static bool QX37_06 { get; set; }
            public static bool QX37_07 { get; set; }

            public static bool QX38_00 { get; set; }
            public static bool QX38_01 { get; set; }
            public static bool QX38_02 { get; set; }
            public static bool QX38_03 { get; set; }
            public static bool QX38_04 { get; set; }
            public static bool QX38_05 { get; set; }
            public static bool QX38_06 { get; set; }
            public static bool QX38_07 { get; set; }

            public static bool QX39_00 { get; set; }
            public static bool QX39_01 { get; set; }
            public static bool QX39_02 { get; set; }
            public static bool QX39_03 { get; set; }
            public static bool QX39_04 { get; set; }
            public static bool QX39_05 { get; set; }
            public static bool QX39_06 { get; set; }
            public static bool QX39_07 { get; set; }

            public static bool QX40_00 { get; set; }
            public static bool QX40_01 { get; set; }
            public static bool QX40_02 { get; set; }
            public static bool QX40_03 { get; set; }
            public static bool QX40_04 { get; set; }
            public static bool QX40_05 { get; set; }
            public static bool QX40_06 { get; set; }
            public static bool QX40_07 { get; set; }

            public static bool QX41_00 { get; set; }
            public static bool QX41_01 { get; set; }
            public static bool QX41_02 { get; set; }
            public static bool QX41_03 { get; set; }
            public static bool QX41_04 { get; set; }
            public static bool QX41_05 { get; set; }
            public static bool QX41_06 { get; set; }
            public static bool QX41_07 { get; set; }

            public static bool QX42_00 { get; set; }
            public static bool QX42_01 { get; set; }
            public static bool QX42_02 { get; set; }
            public static bool QX42_03 { get; set; }
            public static bool QX42_04 { get; set; }
            public static bool QX42_05 { get; set; }
            public static bool QX42_06 { get; set; }
            public static bool QX42_07 { get; set; }

            public static bool QX43_00 { get; set; }
            public static bool QX43_01 { get; set; }
            public static bool QX43_02 { get; set; }
            public static bool QX43_03 { get; set; }
            public static bool QX43_04 { get; set; }
            public static bool QX43_05 { get; set; }
            public static bool QX43_06 { get; set; }
            public static bool QX43_07 { get; set; }

            public static bool QX44_00 { get; set; }
            public static bool QX44_01 { get; set; }
            public static bool QX44_02 { get; set; }
            public static bool QX44_03 { get; set; }
            public static bool QX44_04 { get; set; }
            public static bool QX44_05 { get; set; }
            public static bool QX44_06 { get; set; }
            public static bool QX44_07 { get; set; }

            public static bool QX45_00 { get; set; }
            public static bool QX45_01 { get; set; }
            public static bool QX45_02 { get; set; }
            public static bool QX45_03 { get; set; }
            public static bool QX45_04 { get; set; }
            public static bool QX45_05 { get; set; }
            public static bool QX45_06 { get; set; }
            public static bool QX45_07 { get; set; }

            public static bool QX46_00 { get; set; }
            public static bool QX46_01 { get; set; }
            public static bool QX46_02 { get; set; }
            public static bool QX46_03 { get; set; }
            public static bool QX46_04 { get; set; }
            public static bool QX46_05 { get; set; }
            public static bool QX46_06 { get; set; }
            public static bool QX46_07 { get; set; }

            public static bool QX47_00 { get; set; }
            public static bool QX47_01 { get; set; }
            public static bool QX47_02 { get; set; }
            public static bool QX47_03 { get; set; }
            public static bool QX47_04 { get; set; }
            public static bool QX47_05 { get; set; }
            public static bool QX47_06 { get; set; }
            public static bool QX47_07 { get; set; }

            public static bool QX48_00 { get; set; }
            public static bool QX48_01 { get; set; }
            public static bool QX48_02 { get; set; }
            public static bool QX48_03 { get; set; }
            public static bool QX48_04 { get; set; }
            public static bool QX48_05 { get; set; }
            public static bool QX48_06 { get; set; }
            public static bool QX48_07 { get; set; }

            public static bool QX49_00 { get; set; }
            public static bool QX49_01 { get; set; }
            public static bool QX49_02 { get; set; }
            public static bool QX49_03 { get; set; }
            public static bool QX49_04 { get; set; }
            public static bool QX49_05 { get; set; }
            public static bool QX49_06 { get; set; }
            public static bool QX49_07 { get; set; }

            public static bool QX50_00 { get; set; }
            public static bool QX50_01 { get; set; }
            public static bool QX50_02 { get; set; }
            public static bool QX50_03 { get; set; }
            public static bool QX50_04 { get; set; }
            public static bool QX50_05 { get; set; }
            public static bool QX50_06 { get; set; }
            public static bool QX50_07 { get; set; }

            public static bool QX51_00 { get; set; }
            public static bool QX51_01 { get; set; }
            public static bool QX51_02 { get; set; }
            public static bool QX51_03 { get; set; }
            public static bool QX51_04 { get; set; }
            public static bool QX51_05 { get; set; }
            public static bool QX51_06 { get; set; }
            public static bool QX51_07 { get; set; }

            public static bool QX52_00 { get; set; }
            public static bool QX52_01 { get; set; }
            public static bool QX52_02 { get; set; }
            public static bool QX52_03 { get; set; }
            public static bool QX52_04 { get; set; }
            public static bool QX52_05 { get; set; }
            public static bool QX52_06 { get; set; }
            public static bool QX52_07 { get; set; }

            public static bool QX53_00 { get; set; }
            public static bool QX53_01 { get; set; }
            public static bool QX53_02 { get; set; }
            public static bool QX53_03 { get; set; }
            public static bool QX53_04 { get; set; }
            public static bool QX53_05 { get; set; }
            public static bool QX53_06 { get; set; }
            public static bool QX53_07 { get; set; }

            public static bool QX54_00 { get; set; }
            public static bool QX54_01 { get; set; }
            public static bool QX54_02 { get; set; }
            public static bool QX54_03 { get; set; }
            public static bool QX54_04 { get; set; }
            public static bool QX54_05 { get; set; }
            public static bool QX54_06 { get; set; }
            public static bool QX54_07 { get; set; }

            public static bool QX55_00 { get; set; }
            public static bool QX55_01 { get; set; }
            public static bool QX55_02 { get; set; }
            public static bool QX55_03 { get; set; }
            public static bool QX55_04 { get; set; }
            public static bool QX55_05 { get; set; }
            public static bool QX55_06 { get; set; }
            public static bool QX55_07 { get; set; }

            public static bool QX56_00 { get; set; }
            public static bool QX56_01 { get; set; }
            public static bool QX56_02 { get; set; }
            public static bool QX56_03 { get; set; }
            public static bool QX56_04 { get; set; }
            public static bool QX56_05 { get; set; }
            public static bool QX56_06 { get; set; }
            public static bool QX56_07 { get; set; }

            public static bool QX57_00 { get; set; }
            public static bool QX57_01 { get; set; }
            public static bool QX57_02 { get; set; }
            public static bool QX57_03 { get; set; }
            public static bool QX57_04 { get; set; }
            public static bool QX57_05 { get; set; }
            public static bool QX57_06 { get; set; }
            public static bool QX57_07 { get; set; }

            public static bool QX58_00 { get; set; }
            public static bool QX58_01 { get; set; }
            public static bool QX58_02 { get; set; }
            public static bool QX58_03 { get; set; }
            public static bool QX58_04 { get; set; }
            public static bool QX58_05 { get; set; }
            public static bool QX58_06 { get; set; }
            public static bool QX58_07 { get; set; }

            public static bool QX59_00 { get; set; }
            public static bool QX59_01 { get; set; }
            public static bool QX59_02 { get; set; }
            public static bool QX59_03 { get; set; }
            public static bool QX59_04 { get; set; }
            public static bool QX59_05 { get; set; }
            public static bool QX59_06 { get; set; }
            public static bool QX59_07 { get; set; }

            public static bool QX60_00 { get; set; }
            public static bool QX60_01 { get; set; }
            public static bool QX60_02 { get; set; }
            public static bool QX60_03 { get; set; }
            public static bool QX60_04 { get; set; }
            public static bool QX60_05 { get; set; }
            public static bool QX60_06 { get; set; }
            public static bool QX60_07 { get; set; }

            public static bool QX61_00 { get; set; }
            public static bool QX61_01 { get; set; }
            public static bool QX61_02 { get; set; }
            public static bool QX61_03 { get; set; }
            public static bool QX61_04 { get; set; }
            public static bool QX61_05 { get; set; }
            public static bool QX61_06 { get; set; }
            public static bool QX61_07 { get; set; }

            public static bool QX62_00 { get; set; }
            public static bool QX62_01 { get; set; }
            public static bool QX62_02 { get; set; }
            public static bool QX62_03 { get; set; }
            public static bool QX62_04 { get; set; }
            public static bool QX62_05 { get; set; }
            public static bool QX62_06 { get; set; }
            public static bool QX62_07 { get; set; }

            public static bool QX63_00 { get; set; }
            public static bool QX63_01 { get; set; }
            public static bool QX63_02 { get; set; }
            public static bool QX63_03 { get; set; }
            public static bool QX63_04 { get; set; }
            public static bool QX63_05 { get; set; }
            public static bool QX63_06 { get; set; }
            public static bool QX63_07 { get; set; }

            public static bool QX64_00 { get; set; }
            public static bool QX64_01 { get; set; }
            public static bool QX64_02 { get; set; }
            public static bool QX64_03 { get; set; }
            public static bool QX64_04 { get; set; }
            public static bool QX64_05 { get; set; }
            public static bool QX64_06 { get; set; }
            public static bool QX64_07 { get; set; }

            public static bool QX65_00 { get; set; }
            public static bool QX65_01 { get; set; }
            public static bool QX65_02 { get; set; }
            public static bool QX65_03 { get; set; }
            public static bool QX65_04 { get; set; }
            public static bool QX65_05 { get; set; }
            public static bool QX65_06 { get; set; }
            public static bool QX65_07 { get; set; }

            public static bool QX66_00 { get; set; }
            public static bool QX66_01 { get; set; }
            public static bool QX66_02 { get; set; }
            public static bool QX66_03 { get; set; }
            public static bool QX66_04 { get; set; }
            public static bool QX66_05 { get; set; }
            public static bool QX66_06 { get; set; }
            public static bool QX66_07 { get; set; }

            public static bool QX67_00 { get; set; }
            public static bool QX67_01 { get; set; }
            public static bool QX67_02 { get; set; }
            public static bool QX67_03 { get; set; }
            public static bool QX67_04 { get; set; }
            public static bool QX67_05 { get; set; }
            public static bool QX67_06 { get; set; }
            public static bool QX67_07 { get; set; }

            public static bool QX68_00 { get; set; }
            public static bool QX68_01 { get; set; }
            public static bool QX68_02 { get; set; }
            public static bool QX68_03 { get; set; }
            public static bool QX68_04 { get; set; }
            public static bool QX68_05 { get; set; }
            public static bool QX68_06 { get; set; }
            public static bool QX68_07 { get; set; }

            public static bool QX69_00 { get; set; }
            public static bool QX69_01 { get; set; }
            public static bool QX69_02 { get; set; }
            public static bool QX69_03 { get; set; }
            public static bool QX69_04 { get; set; }
            public static bool QX69_05 { get; set; }
            public static bool QX69_06 { get; set; }
            public static bool QX69_07 { get; set; }

            public static bool QX70_00 { get; set; }
            public static bool QX70_01 { get; set; }
            public static bool QX70_02 { get; set; }
            public static bool QX70_03 { get; set; }
            public static bool QX70_04 { get; set; }
            public static bool QX70_05 { get; set; }
            public static bool QX70_06 { get; set; }
            public static bool QX70_07 { get; set; }

            public static bool QX71_00 { get; set; }
            public static bool QX71_01 { get; set; }
            public static bool QX71_02 { get; set; }
            public static bool QX71_03 { get; set; }
            public static bool QX71_04 { get; set; }
            public static bool QX71_05 { get; set; }
            public static bool QX71_06 { get; set; }
            public static bool QX71_07 { get; set; }

            public static bool QX72_00 { get; set; }
            public static bool QX72_01 { get; set; }
            public static bool QX72_02 { get; set; }
            public static bool QX72_03 { get; set; }
            public static bool QX72_04 { get; set; }
            public static bool QX72_05 { get; set; }
            public static bool QX72_06 { get; set; }
            public static bool QX72_07 { get; set; }

            public static bool QX73_00 { get; set; }
            public static bool QX73_01 { get; set; }
            public static bool QX73_02 { get; set; }
            public static bool QX73_03 { get; set; }
            public static bool QX73_04 { get; set; }
            public static bool QX73_05 { get; set; }
            public static bool QX73_06 { get; set; }
            public static bool QX73_07 { get; set; }

            public static bool QX74_00 { get; set; }
            public static bool QX74_01 { get; set; }
            public static bool QX74_02 { get; set; }
            public static bool QX74_03 { get; set; }
            public static bool QX74_04 { get; set; }
            public static bool QX74_05 { get; set; }
            public static bool QX74_06 { get; set; }
            public static bool QX74_07 { get; set; }

            public static bool QX75_00 { get; set; }
            public static bool QX75_01 { get; set; }
            public static bool QX75_02 { get; set; }
            public static bool QX75_03 { get; set; }
            public static bool QX75_04 { get; set; }
            public static bool QX75_05 { get; set; }
            public static bool QX75_06 { get; set; }
            public static bool QX75_07 { get; set; }

            public static bool QX76_00 { get; set; }
            public static bool QX76_01 { get; set; }
            public static bool QX76_02 { get; set; }
            public static bool QX76_03 { get; set; }
            public static bool QX76_04 { get; set; }
            public static bool QX76_05 { get; set; }
            public static bool QX76_06 { get; set; }
            public static bool QX76_07 { get; set; }

            public static bool QX77_00 { get; set; }
            public static bool QX77_01 { get; set; }
            public static bool QX77_02 { get; set; }
            public static bool QX77_03 { get; set; }
            public static bool QX77_04 { get; set; }
            public static bool QX77_05 { get; set; }
            public static bool QX77_06 { get; set; }
            public static bool QX77_07 { get; set; }

            public static bool QX78_00 { get; set; }
            public static bool QX78_01 { get; set; }
            public static bool QX78_02 { get; set; }
            public static bool QX78_03 { get; set; }
            public static bool QX78_04 { get; set; }
            public static bool QX78_05 { get; set; }
            public static bool QX78_06 { get; set; }
            public static bool QX78_07 { get; set; }

            public static bool QX79_00 { get; set; }
            public static bool QX79_01 { get; set; }
            public static bool QX79_02 { get; set; }
            public static bool QX79_03 { get; set; }
            public static bool QX79_04 { get; set; }
            public static bool QX79_05 { get; set; }
            public static bool QX79_06 { get; set; }
            public static bool QX79_07 { get; set; }

            public static bool QX80_00 { get; set; }
            public static bool QX80_01 { get; set; }
            public static bool QX80_02 { get; set; }
            public static bool QX80_03 { get; set; }
            public static bool QX80_04 { get; set; }
            public static bool QX80_05 { get; set; }
            public static bool QX80_06 { get; set; }
            public static bool QX80_07 { get; set; }

            public static bool QX81_00 { get; set; }
            public static bool QX81_01 { get; set; }
            public static bool QX81_02 { get; set; }
            public static bool QX81_03 { get; set; }
            public static bool QX81_04 { get; set; }
            public static bool QX81_05 { get; set; }
            public static bool QX81_06 { get; set; }
            public static bool QX81_07 { get; set; }

            public static bool QX82_00 { get; set; }
            public static bool QX82_01 { get; set; }
            public static bool QX82_02 { get; set; }
            public static bool QX82_03 { get; set; }
            public static bool QX82_04 { get; set; }
            public static bool QX82_05 { get; set; }
            public static bool QX82_06 { get; set; }
            public static bool QX82_07 { get; set; }

            public static bool QX83_00 { get; set; }
            public static bool QX83_01 { get; set; }
            public static bool QX83_02 { get; set; }
            public static bool QX83_03 { get; set; }
            public static bool QX83_04 { get; set; }
            public static bool QX83_05 { get; set; }
            public static bool QX83_06 { get; set; }
            public static bool QX83_07 { get; set; }

            public static bool QX84_00 { get; set; }
            public static bool QX84_01 { get; set; }
            public static bool QX84_02 { get; set; }
            public static bool QX84_03 { get; set; }
            public static bool QX84_04 { get; set; }
            public static bool QX84_05 { get; set; }
            public static bool QX84_06 { get; set; }
            public static bool QX84_07 { get; set; }

            public static bool QX85_00 { get; set; }
            public static bool QX85_01 { get; set; }
            public static bool QX85_02 { get; set; }
            public static bool QX85_03 { get; set; }
            public static bool QX85_04 { get; set; }
            public static bool QX85_05 { get; set; }
            public static bool QX85_06 { get; set; }
            public static bool QX85_07 { get; set; }

            public static bool QX86_00 { get; set; }
            public static bool QX86_01 { get; set; }
            public static bool QX86_02 { get; set; }
            public static bool QX86_03 { get; set; }
            public static bool QX86_04 { get; set; }
            public static bool QX86_05 { get; set; }
            public static bool QX86_06 { get; set; }
            public static bool QX86_07 { get; set; }

            public static bool QX87_00 { get; set; }
            public static bool QX87_01 { get; set; }
            public static bool QX87_02 { get; set; }
            public static bool QX87_03 { get; set; }
            public static bool QX87_04 { get; set; }
            public static bool QX87_05 { get; set; }
            public static bool QX87_06 { get; set; }
            public static bool QX87_07 { get; set; }

            public static bool QX88_00 { get; set; }
            public static bool QX88_01 { get; set; }
            public static bool QX88_02 { get; set; }
            public static bool QX88_03 { get; set; }
            public static bool QX88_04 { get; set; }
            public static bool QX88_05 { get; set; }
            public static bool QX88_06 { get; set; }
            public static bool QX88_07 { get; set; }

            public static bool QX89_00 { get; set; }
            public static bool QX89_01 { get; set; }
            public static bool QX89_02 { get; set; }
            public static bool QX89_03 { get; set; }
            public static bool QX89_04 { get; set; }
            public static bool QX89_05 { get; set; }
            public static bool QX89_06 { get; set; }
            public static bool QX89_07 { get; set; }

            public static bool QX90_00 { get; set; }
            public static bool QX90_01 { get; set; }
            public static bool QX90_02 { get; set; }
            public static bool QX90_03 { get; set; }
            public static bool QX90_04 { get; set; }
            public static bool QX90_05 { get; set; }
            public static bool QX90_06 { get; set; }
            public static bool QX90_07 { get; set; }

            public static bool QX91_00 { get; set; }
            public static bool QX91_01 { get; set; }
            public static bool QX91_02 { get; set; }
            public static bool QX91_03 { get; set; }
            public static bool QX91_04 { get; set; }
            public static bool QX91_05 { get; set; }
            public static bool QX91_06 { get; set; }
            public static bool QX91_07 { get; set; }

            public static bool QX92_00 { get; set; }
            public static bool QX92_01 { get; set; }
            public static bool QX92_02 { get; set; }
            public static bool QX92_03 { get; set; }
            public static bool QX92_04 { get; set; }
            public static bool QX92_05 { get; set; }
            public static bool QX92_06 { get; set; }
            public static bool QX92_07 { get; set; }

            public static bool QX93_00 { get; set; }
            public static bool QX93_01 { get; set; }
            public static bool QX93_02 { get; set; }
            public static bool QX93_03 { get; set; }
            public static bool QX93_04 { get; set; }
            public static bool QX93_05 { get; set; }
            public static bool QX93_06 { get; set; }
            public static bool QX93_07 { get; set; }

            public static bool QX94_00 { get; set; }
            public static bool QX94_01 { get; set; }
            public static bool QX94_02 { get; set; }
            public static bool QX94_03 { get; set; }
            public static bool QX94_04 { get; set; }
            public static bool QX94_05 { get; set; }
            public static bool QX94_06 { get; set; }
            public static bool QX94_07 { get; set; }

            public static bool QX95_00 { get; set; }
            public static bool QX95_01 { get; set; }
            public static bool QX95_02 { get; set; }
            public static bool QX95_03 { get; set; }
            public static bool QX95_04 { get; set; }
            public static bool QX95_05 { get; set; }
            public static bool QX95_06 { get; set; }
            public static bool QX95_07 { get; set; }

            public static bool QX96_00 { get; set; }
            public static bool QX96_01 { get; set; }
            public static bool QX96_02 { get; set; }
            public static bool QX96_03 { get; set; }
            public static bool QX96_04 { get; set; }
            public static bool QX96_05 { get; set; }
            public static bool QX96_06 { get; set; }
            public static bool QX96_07 { get; set; }

            public static bool QX97_00 { get; set; }
            public static bool QX97_01 { get; set; }
            public static bool QX97_02 { get; set; }
            public static bool QX97_03 { get; set; }
            public static bool QX97_04 { get; set; }
            public static bool QX97_05 { get; set; }
            public static bool QX97_06 { get; set; }
            public static bool QX97_07 { get; set; }

            public static bool QX98_00 { get; set; }
            public static bool QX98_01 { get; set; }
            public static bool QX98_02 { get; set; }
            public static bool QX98_03 { get; set; }
            public static bool QX98_04 { get; set; }
            public static bool QX98_05 { get; set; }
            public static bool QX98_06 { get; set; }
            public static bool QX98_07 { get; set; }

            public static bool QX99_00 { get; set; }
            public static bool QX99_01 { get; set; }
            public static bool QX99_02 { get; set; }
            public static bool QX99_03 { get; set; }
            public static bool QX99_04 { get; set; }
            public static bool QX99_05 { get; set; }
            public static bool QX99_06 { get; set; }
            public static bool QX99_07 { get; set; }

            public static bool QX100_00 { get; set; }
            public static bool QX100_01 { get; set; }
            public static bool QX100_02 { get; set; }
            public static bool QX100_03 { get; set; }
            public static bool QX100_04 { get; set; }
            public static bool QX100_05 { get; set; }
            public static bool QX100_06 { get; set; }
            public static bool QX100_07 { get; set; }


        }
        public struct Motor
        {
            public static int ReadingMotor { get; set; }
            public static int Axis_Number { get; set; }
            public static bool Check_Save_Motor { get; set; }
            public static bool _Axis1_Stop { get; set; }
            public static UInt16 _Axis1_Acc { get; set; }
            public static UInt32 _Axis1_Dcc { get; set; }
            public static UInt16 _Axis1_Frequency { get; set; }
            public static UInt32 _Axis1_Target { get; set; }
            public static bool _Axis1_GoSet { get; set; }
            public static bool _Axis1_Busy { get; set; }
            public static bool _Axis1_Reached { get; set; }
            public static UInt32 _Axis1_CurrentPos { get; set; }
            public static UInt16 _Axis1_JogSpeed { get; set; }
            public static UInt16 _Axis1_JogAcc { get; set; }
            public static UInt16 _Axis1_JogDcc { get; set; }
            public static bool _Axis1_JogPos { get; set; }
            public static bool _Axis1_JogNeg { get; set; }
            public static bool _Axis1_StartHoming { get; set; }
            public static bool _Axis1_CancelHoming { get; set; }
            public static UInt16 _Axis1_HomeSpeed1 { get; set; }
            public static UInt16 _Axis1_HomeSpeed2 { get; set; }
            public static bool _Axis1_HomeBusy { get; set; }
            public static bool _Axis1_HomeDone { get; set; }
            public static bool _Axis1_HomeDoneLocal { get; set; }

            public static bool _Axis2_Stop { get; set; }
            public static UInt16 _Axis2_Acc { get; set; }
            public static UInt16 _Axis2_Dcc { get; set; }
            public static UInt16 _Axis2_Frequency { get; set; }
            public static UInt32 _Axis2_Target { get; set; }
            public static bool _Axis2_GoSet { get; set; }
            public static bool _Axis2_Busy { get; set; }
            public static bool _Axis2_Reached { get; set; }
            public static UInt32 _Axis2_CurrentPos { get; set; }
            public static UInt16 _Axis2_JogSpeed { get; set; }
            public static UInt16 _Axis2_JogAcc { get; set; }
            public static UInt16 _Axis2_JogDcc { get; set; }
            public static bool _Axis2_JogPos { get; set; }
            public static bool _Axis2_JogNeg { get; set; }
            public static bool _Axis2_StartHoming { get; set; }
            public static bool _Axis2_CancelHoming { get; set; }
            public static UInt16 _Axis2_HomeSpeed1 { get; set; }
            public static UInt16 _Axis2_HomeSpeed2 { get; set; }
            public static bool _Axis2_HomeBusy { get; set; }
            public static bool _Axis2_HomeDone { get; set; }
            public static bool _Axis2_HomeDoneLocal { get; set; }

            public static bool _Axis3_Stop { get; set; }
            public static UInt16 _Axis3_Acc { get; set; }
            public static UInt16 _Axis3_Dcc { get; set; }
            public static UInt16 _Axis3_Frequency { get; set; }
            public static UInt32 _Axis3_Target { get; set; }
            public static bool _Axis3_GoSet { get; set; }
            public static bool _Axis3_Busy { get; set; }
            public static bool _Axis3_Reached { get; set; }
            public static UInt32 _Axis3_CurrentPos { get; set; }
            public static UInt16 _Axis3_JogSpeed { get; set; }
            public static UInt16 _Axis3_JogAcc { get; set; }
            public static UInt16 _Axis3_JogDcc { get; set; }
            public static bool _Axis3_JogPos { get; set; }
            public static bool _Axis3_JogNeg { get; set; }
            public static bool _Axis3_StartHoming { get; set; }
            public static bool _Axis3_CancelHoming { get; set; }
            public static UInt16 _Axis3_HomeSpeed1 { get; set; }
            public static UInt16 _Axis3_HomeSpeed2 { get; set; }
            public static bool _Axis3_HomeBusy { get; set; }
            public static bool _Axis3_HomeDone { get; set; }
            public static bool _Axis3_HomeDoneLocal { get; set; }

            public static bool _Axis4_Stop { get; set; }
            public static UInt16 _Axis4_Acc { get; set; }
            public static UInt16 _Axis4_Dcc { get; set; }
            public static UInt16 _Axis4_Frequency { get; set; }
            public static UInt32 _Axis4_Target { get; set; }
            public static bool _Axis4_GoSet { get; set; }
            public static bool _Axis4_Busy { get; set; }
            public static bool _Axis4_Reached { get; set; }
            public static UInt32 _Axis4_CurrentPos { get; set; }
            public static UInt16 _Axis4_JogSpeed { get; set; }
            public static UInt16 _Axis4_JogAcc { get; set; }
            public static UInt16 _Axis4_JogDcc { get; set; }
            public static bool _Axis4_JogPos { get; set; }
            public static bool _Axis4_JogNeg { get; set; }
            public static bool _Axis4_StartHoming { get; set; }
            public static bool _Axis4_CancelHoming { get; set; }
            public static UInt16 _Axis4_HomeSpeed1 { get; set; }
            public static UInt16 _Axis4_HomeSpeed2 { get; set; }
            public static bool _Axis4_HomeBusy { get; set; }
            public static bool _Axis4_HomeDone { get; set; }
            public static bool _Axis4_HomeDoneLocal { get; set; }

            public static bool _Axis5_Stop { get; set; }
            public static UInt16 _Axis5_Acc { get; set; }
            public static UInt16 _Axis5_Dcc { get; set; }
            public static UInt16 _Axis5_Frequency { get; set; }
            public static UInt32 _Axis5_Target { get; set; }
            public static bool _Axis5_GoSet { get; set; }
            public static bool _Axis5_Busy { get; set; }
            public static bool _Axis5_Reached { get; set; }
            public static UInt32 _Axis5_CurrentPos { get; set; }
            public static UInt16 _Axis5_JogSpeed { get; set; }
            public static UInt16 _Axis5_JogAcc { get; set; }
            public static UInt16 _Axis5_JogDcc { get; set; }
            public static bool _Axis5_JogPos { get; set; }
            public static bool _Axis5_JogNeg { get; set; }
            public static bool _Axis5_StartHoming { get; set; }
            public static bool _Axis5_CancelHoming { get; set; }
            public static UInt16 _Axis5_HomeSpeed1 { get; set; }
            public static UInt16 _Axis5_HomeSpeed2 { get; set; }
            public static bool _Axis5_HomeBusy { get; set; }
            public static bool _Axis5_HomeDone { get; set; }
            public static bool _Axis5_HomeDoneLocal { get; set; }

        }
    }
}
