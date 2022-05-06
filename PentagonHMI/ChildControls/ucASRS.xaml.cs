using System;
using System.Windows.Controls;
using System.Windows.Media;
using Logix;


namespace PentagonHMI.ChildControls
{

    public partial class ucASRS : UserControl, IDisposable
    {
        LogicClasses.Main _MainConnection;

        Controller PLCController;

        Tag ASRSStockTag = new Tag("strStock_Qty");

        #region Properties
        #endregion
        public ucASRS(ref LogicClasses.Main MainConnection, ref LogicClasses.IOInput IOlist)
        {
            InitializeComponent();

            _MainConnection = MainConnection;
            ASRS = new LogicClasses.ASRS(ref MainConnection, ref IOlist);
            ASRS.OnUpdate += new LogicClasses.ASRS.OnUpdateHandle(ASRS_OnUpdate);

            ASRS.Initialization();
            //for (int i = 0; i < 10; i++)
            //    for (int j = 0; j < 6; j++)
            //        ASRS.binColor[i, j] = Brushes.LightGreen;
            PLCController = _MainConnection.MyPLC;
            ASRSStockTag.DataType = Logix.Tag.ATOMIC.STRING;

            refreshAllBinColor(false);

        }
        private LogicClasses.ASRS ASRS { get; set; }

        public void ASRS_OnUpdate()
        {
            refreshAllBinColor(true);
        }
        private void refreshAllBinColor(bool onTread)
        {
            try
            {
                if (!onTread)
                {
                    ASRSRefresh();
                }
                else
                {
                    try
                    {
                        this.Dispatcher.Invoke(new Action(() => ASRSRefresh()));
                    }
                    catch (Exception ex)
                    {
                        Utilities.FileLogger.logError("ASRS Error: Fail to refresh the page, " + ex.Message, ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError("refreshAllBinColor ASRS, " + ex.Message, ex.ToString());
            }
        }

        private void ASRSRefresh()
        {
            PLCController.ReadTag(ASRSStockTag);
            if (ResultCode.QUAL_GOOD == ASRSStockTag.QualityCode)
            {
                if (ASRSStockTag.Value != null)
                    if (ASRSStockTag.Value.ToString().Length == 0)
                        ASRSQtyTB.Text = "NA";
                    else
                        ASRSQtyTB.Text = ASRSStockTag.Value.ToString();
            }

            int n = 5; int r = 0;
            R1_C1.Background = ASRS.binColor[n, r]; r++;
            R2_C1.Background = ASRS.binColor[n, r]; r++;
            R3_C1.Background = ASRS.binColor[n, r]; r++;
            R4_C1.Background = ASRS.binColor[n, r]; r++;
            R5_C1.Background = ASRS.binColor[n, r]; r++;
            R6_C1.Background = ASRS.binColor[n, r]; r++;
            R7_C1.Background = ASRS.binColor[n, r]; r++;
            R8_C1.Background = ASRS.binColor[n, r]; r++;
            R9_C1.Background = ASRS.binColor[n, r]; r++;
            R10_C1.Background = ASRS.binColor[n, r]; r++;

            n--; r = 0;
            R1_C2.Background = ASRS.binColor[n, r]; r++;
            R2_C2.Background = ASRS.binColor[n, r]; r++;
            R3_C2.Background = ASRS.binColor[n, r]; r++;
            R4_C2.Background = ASRS.binColor[n, r]; r++;
            R5_C2.Background = ASRS.binColor[n, r]; r++;
            R6_C2.Background = ASRS.binColor[n, r]; r++;
            R7_C2.Background = ASRS.binColor[n, r]; r++;
            R8_C2.Background = ASRS.binColor[n, r]; r++;
            R9_C2.Background = ASRS.binColor[n, r]; r++;
            R10_C2.Background = ASRS.binColor[n, r]; r++;

            n--; r = 0;
            R1_C3.Background = ASRS.binColor[n, r]; r++;
            R2_C3.Background = ASRS.binColor[n, r]; r++;
            R3_C3.Background = ASRS.binColor[n, r]; r++;
            R4_C3.Background = ASRS.binColor[n, r]; r++;
            R5_C3.Background = ASRS.binColor[n, r]; r++;
            R6_C3.Background = ASRS.binColor[n, r]; r++;
            R7_C3.Background = ASRS.binColor[n, r]; r++;
            R8_C3.Background = ASRS.binColor[n, r]; r++;
            R9_C3.Background = ASRS.binColor[n, r]; r++;
            R10_C3.Background = ASRS.binColor[n, r]; r++;

            n--; r = 0;
            R1_C4.Background = ASRS.binColor[n, r]; r++;
            R2_C4.Background = ASRS.binColor[n, r]; r++;
            R3_C4.Background = ASRS.binColor[n, r]; r++;
            R4_C4.Background = ASRS.binColor[n, r]; r++;
            R5_C4.Background = ASRS.binColor[n, r]; r++;
            R6_C4.Background = ASRS.binColor[n, r]; r++;
            R7_C4.Background = ASRS.binColor[n, r]; r++;
            R8_C4.Background = ASRS.binColor[n, r]; r++;
            R9_C4.Background = ASRS.binColor[n, r]; r++;
            R10_C4.Background = ASRS.binColor[n, r]; r++;

            n--; r = 0;
            R1_C5.Background = ASRS.binColor[n, r]; r++;
            R2_C5.Background = ASRS.binColor[n, r]; r++;
            R3_C5.Background = ASRS.binColor[n, r]; r++;
            R4_C5.Background = ASRS.binColor[n, r]; r++;
            R5_C5.Background = ASRS.binColor[n, r]; r++;
            R6_C5.Background = ASRS.binColor[n, r]; r++;
            R7_C5.Background = ASRS.binColor[n, r]; r++;
            R8_C5.Background = ASRS.binColor[n, r]; r++;
            R9_C5.Background = ASRS.binColor[n, r]; r++;
            R10_C5.Background = ASRS.binColor[n, r]; r++;

            n--; r = 0;
            R1_C6.Background = ASRS.binColor[n, r]; r++;
            R2_C6.Background = ASRS.binColor[n, r]; r++;
            R3_C6.Background = ASRS.binColor[n, r]; r++;
            R4_C6.Background = ASRS.binColor[n, r]; r++;
            R5_C6.Background = ASRS.binColor[n, r]; r++;
            R6_C6.Background = ASRS.binColor[n, r]; r++;
            R7_C6.Background = ASRS.binColor[n, r]; r++;
            R8_C6.Background = ASRS.binColor[n, r]; r++;
            R9_C6.Background = ASRS.binColor[n, r]; r++;
            R10_C6.Background = ASRS.binColor[n, r]; r++;
        }


        public void Dispose()
        {
            if (ASRS != null)
            {
                ASRS.OnUpdate -= new LogicClasses.ASRS.OnUpdateHandle(ASRS_OnUpdate);
                _MainConnection.ASRSPageOn = false;
            }
        }

        #region Destructor
        ~ucASRS()
        {
            Dispose();
        }
        #endregion


    }

}
