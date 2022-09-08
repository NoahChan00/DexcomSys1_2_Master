//using TwinCAT.Ads;
//using TwinCAT.Ads.SumCommand;

namespace PentagonHMI.LogicClasses
{
    public class IOLocation //:  IDisposable
    {
        //#region Variables
        //LogicClasses.Main _MainConnection;
        //public delegate void onUpdateHandler();
        //public event onUpdateHandler OnUpdate;
        ////private static TcAdsClient A_tcClient = new TcAdsClient();
        ////SumCreateHandles createHandlesCommand;
        ////SumHandleRead readCommand;
        //#endregion

        //#region Constructor
        //public IOLocation(ref LogicClasses.Main MainConnection)
        //{
        //    _MainConnection = MainConnection;
        //    _MainConnection.OnIOLocUpdate += new LogicClasses.Main.onIOLocUpdateHandler(TcpIOInput_OnUpdate);
        //    _MainConnection.IOLocPageON = true;
        //}
        //#endregion

        //#region Properties

        //#endregion

        //#region Methods
        //public  void Initialization()
        //{
        //    Connect();
        //}
        //private void Connect()
        //{
        //    try
        //    {
        //        A_tcClient.Connect(_MainConnection.AdsNetID, 801);
        //    }
        //    catch
        //    {}
        //}

        //public void Dispose()
        //{
        //    if (_MainConnection != null)
        //    {
        //        _MainConnection.OnIOLocUpdate -= new LogicClasses.Main.onIOLocUpdateHandler(TcpIOInput_OnUpdate);
        //        _MainConnection.IOLocPageON = false;
        //    }
        //}

        //#endregion

        //#region Event
        //void TcpIOInput_OnUpdate()
        //{
        //    try
        //    {
        //        switch (_MainConnection.StationID)
        //        {
        //            case ("0"):
        //                getMainConveyor();
        //                break;
        //            case ("1"):
        //                getEmptyTray();
        //                break;
        //            case ("2"):
        //                getAssetTag();
        //                break;
        //            case ("3"):
        //                getMOBOASRS();
        //                break;
        //            case ("4"):
        //                getMOBORobot();
        //                break;
        //            case ("5"):
        //                getKoolaidTang();
        //                break;
        //            case ("6"):
        //                getAffixFan();
        //                break;
        //            case ("7"):
        //                getBaffle();
        //                break;
        //            case ("8"):
        //                getBIOSCobra();
        //                break;
        //            case ("9"):
        //                getLargeHeatsink();
        //                break;
        //            case ("10"):
        //                getSmallHeatsink();
        //                break;
        //            default:
        //                //main con
        //                break;

        //        }
        //    }
        //    catch
        //    {
        //    }

        //    if (OnUpdate != null)
        //        OnUpdate();
        //}
        //#endregion
        //#region getIO
        //private void getEmptyTray()
        //{
        //    //I10 //Q7
        //    string[] InputList =
        //        {
        //        ".ix0_0", ".ix0_1", ".ix0_2", ".ix0_3", ".ix0_4", ".ix0_5", ".ix0_6", ".ix0_7",
        //        ".ix1_0", ".ix1_1", ".ix1_2", ".ix1_3", ".ix1_4", ".ix1_5", ".ix1_6", ".ix1_7",
        //        ".ix2_0", ".ix2_1", ".ix2_2", ".ix2_3", ".ix2_4", ".ix2_5", ".ix2_6", ".ix2_7",
        //        ".ix3_0", ".ix3_1", ".ix3_2", ".ix3_3", ".ix3_4", ".ix3_5", ".ix3_6", ".ix3_7",
        //        ".ix4_0", ".ix4_1", ".ix4_2", ".ix4_3", ".ix4_4", ".ix4_5", ".ix4_6", ".ix4_7",
        //        ".ix5_0", ".ix5_1", ".ix5_2", ".ix5_3", ".ix5_4", ".ix5_5", ".ix5_6", ".ix5_7",
        //        ".ix6_0", ".ix6_1", ".ix6_2", ".ix6_3", ".ix6_4", ".ix6_5", ".ix6_6", ".ix6_7",
        //        ".ix7_0", ".ix7_1", ".ix7_2", ".ix7_3", ".ix7_4", ".ix7_5", ".ix7_6", ".ix7_7",
        //        ".ix8_0", ".ix8_1", ".ix8_2", ".ix8_3", ".ix8_4", ".ix8_5", ".ix8_6", ".ix8_7",
        //        ".ix9_0", ".ix9_1", ".ix9_2", ".ix9_3", ".ix9_4", ".ix9_5", ".ix9_6", ".ix9_7",
        //        ".ix10_0", ".ix10_1", ".ix10_2", ".ix10_3", ".ix10_4", ".ix10_5", ".ix10_6", ".ix10_7",
        //        ".ix11_0", ".ix11_1", ".ix11_2", ".ix11_3", ".ix11_4", ".ix11_5", ".ix11_6", ".ix11_7",
        //        ".ix12_0", ".ix12_1", ".ix12_2", ".ix12_3", ".ix12_4", ".ix12_5", ".ix12_6", ".ix12_7",
        //        ".ix13_0", ".ix13_1", ".ix13_2", ".ix13_3", ".ix13_4", ".ix13_5", ".ix13_6", ".ix13_7",
        //        ".ix14_0", ".ix14_1", ".ix14_2", ".ix14_3", ".ix14_4", ".ix14_5", ".ix14_6", ".ix14_7",
        //        ".ix15_0", ".ix15_1", ".ix15_2", ".ix15_3", ".ix15_4", ".ix15_5", ".ix15_6", ".ix15_7",
        //        ".Qx0_0", ".Qx0_1", ".Qx0_2", ".Qx0_3", ".Qx0_4", ".Qx0_5", ".Qx0_6", ".Qx0_7",
        //        ".Qx1_0", ".Qx1_1", ".Qx1_2", ".Qx1_3", ".Qx1_4", ".Qx1_5", ".Qx1_6", ".Qx1_7",
        //        ".Qx2_0", ".Qx2_1", ".Qx2_2", ".Qx2_3", ".Qx2_4", ".Qx2_5", ".Qx2_6", ".Qx2_7",
        //        ".Qx3_0", ".Qx3_1", ".Qx3_2", ".Qx3_3", ".Qx3_4", ".Qx3_5", ".Qx3_6", ".Qx3_7",};

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    //Read Input
        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX7_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX8_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX9_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX10_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX11_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX12_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX13_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX14_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX15_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //}
        //private void getAssetTag()
        //{
        //    //I4 //Q4
        //    string[] InputList =
        //        {
        //        ".ix0_0", ".ix0_1", ".ix0_2", ".ix0_3", ".ix0_4", ".ix0_5", ".ix0_6", ".ix0_7",
        //        ".ix1_0", ".ix1_1", ".ix1_2", ".ix1_3", ".ix1_4", ".ix1_5", ".ix1_6", ".ix1_7",
        //        ".ix2_0", ".ix2_1", ".ix2_2", ".ix2_3", ".ix2_4", ".ix2_5", ".ix2_6", ".ix2_7",
        //        ".ix3_0", ".ix3_1", ".ix3_2", ".ix3_3", ".ix3_4", ".ix3_5", ".ix3_6", ".ix3_7",
        //        ".ix4_0", ".ix4_1", ".ix4_2", ".ix4_3", ".ix4_4", ".ix4_5", ".ix4_6", ".ix4_7",
        //        ".Qx0_0", ".Qx0_1", ".Qx0_2", ".Qx0_3", ".Qx0_4", ".Qx0_5", ".Qx0_6", ".Qx0_7",
        //        ".Qx1_0", ".Qx1_1", ".Qx1_2", ".Qx1_3", ".Qx1_4", ".Qx1_5", ".Qx1_6", ".Qx1_7",
        //        ".Qx2_0", ".Qx2_1", ".Qx2_2", ".Qx2_3", ".Qx2_4", ".Qx2_5", ".Qx2_6", ".Qx2_7",
        //        ".Qx3_0", ".Qx3_1", ".Qx3_2", ".Qx3_3", ".Qx3_4", ".Qx3_5", ".Qx3_6", ".Qx3_7",
        //        ".Qx4_0", ".Qx4_1", ".Qx4_2", ".Qx4_3", ".Qx4_4", ".Qx4_5", ".Qx4_6", ".Qx4_7",};

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    //Read Input
        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //}
        //private void getMOBOASRS()
        //{
        //    //I10 //Q7
        //    string[] InputList =
        //        {
        //        ".ix0_0", ".ix0_1", ".ix0_2", ".ix0_3", ".ix0_4", ".ix0_5", ".ix0_6", ".ix0_7",
        //        ".ix1_0", ".ix1_1", ".ix1_2", ".ix1_3", ".ix1_4", ".ix1_5", ".ix1_6", ".ix1_7",
        //        ".ix2_0", ".ix2_1", ".ix2_2", ".ix2_3", ".ix2_4", ".ix2_5", ".ix2_6", ".ix2_7",
        //        ".ix3_0", ".ix3_1", ".ix3_2", ".ix3_3", ".ix3_4", ".ix3_5", ".ix3_6", ".ix3_7",
        //        ".ix4_0", ".ix4_1", ".ix4_2", ".ix4_3", ".ix4_4", ".ix4_5", ".ix4_6", ".ix4_7",
        //        ".ix5_0", ".ix5_1", ".ix5_2", ".ix5_3", ".ix5_4", ".ix5_5", ".ix5_6", ".ix5_7",
        //        ".ix6_0", ".ix6_1", ".ix6_2", ".ix6_3", ".ix6_4", ".ix6_5", ".ix6_6", ".ix6_7",
        //        ".ix7_0", ".ix7_1", ".ix7_2", ".ix7_3", ".ix7_4", ".ix7_5", ".ix7_6", ".ix7_7",
        //        ".ix8_0", ".ix8_1", ".ix8_2", ".ix8_3", ".ix8_4", ".ix8_5", ".ix8_6", ".ix8_7",
        //        ".ix9_0", ".ix9_1", ".ix9_2", ".ix9_3", ".ix9_4", ".ix9_5", ".ix9_6", ".ix9_7",
        //        ".ix10_0", ".ix10_1", ".ix10_2", ".ix10_3", ".ix10_4", ".ix10_5", ".ix10_6", ".ix10_7",
        //        ".ix11_0", ".ix11_1", ".ix11_2", ".ix11_3", ".ix11_4", ".ix11_5", ".ix11_6", ".ix11_7",
        //        ".ix12_0", ".ix12_1", ".ix12_2", ".ix12_3", ".ix12_4", ".ix12_5", ".ix12_6", ".ix12_7",
        //        ".ix13_0", ".ix13_1", ".ix13_2", ".ix13_3", ".ix13_4", ".ix13_5", ".ix13_6", ".ix13_7",
        //        ".ix14_0", ".ix14_1", ".ix14_2", ".ix14_3", ".ix14_4", ".ix14_5", ".ix14_6", ".ix14_7",
        //        ".ix15_0", ".ix15_1", ".ix15_2", ".ix15_3", ".ix15_4", ".ix15_5", ".ix15_6", ".ix15_7",
        //        ".ix16_0", ".ix16_1", ".ix16_2", ".ix16_3", ".ix16_4", ".ix16_5", ".ix16_6", ".ix16_7",
        //        ".Qx0_0", ".Qx0_1", ".Qx0_2", ".Qx0_3", ".Qx0_4", ".Qx0_5", ".Qx0_6", ".Qx0_7",
        //        ".Qx1_0", ".Qx1_1", ".Qx1_2", ".Qx1_3", ".Qx1_4", ".Qx1_5", ".Qx1_6", ".Qx1_7",
        //        ".Qx2_0", ".Qx2_1", ".Qx2_2", ".Qx2_3", ".Qx2_4", ".Qx2_5", ".Qx2_6", ".Qx2_7",
        //        ".Qx3_0", ".Qx3_1", ".Qx3_2", ".Qx3_3", ".Qx3_4", ".Qx3_5", ".Qx3_6", ".Qx3_7",
        //        ".Qx4_0", ".Qx4_1", ".Qx4_2", ".Qx4_3", ".Qx4_4", ".Qx4_5", ".Qx4_6", ".Qx4_7",};

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    //Read Input
        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX7_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX8_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX9_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX10_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX11_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX12_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX13_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX14_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX15_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX16_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //}
        //private void getMOBORobot()
        //{
        //    //I5 //Q5
        //    string[] InputList =
        //        {
        //        ".ix0_0", ".ix0_1", ".ix0_2", ".ix0_3", ".ix0_4", ".ix0_5", ".ix0_6", ".ix0_7",
        //        ".ix1_0", ".ix1_1", ".ix1_2", ".ix1_3", ".ix1_4", ".ix1_5", ".ix1_6", ".ix1_7",
        //        ".ix2_0", ".ix2_1", ".ix2_2", ".ix2_3", ".ix2_4", ".ix2_5", ".ix2_6", ".ix2_7",
        //        ".ix3_0", ".ix3_1", ".ix3_2", ".ix3_3", ".ix3_4", ".ix3_5", ".ix3_6", ".ix3_7",
        //        ".ix4_0", ".ix4_1", ".ix4_2", ".ix4_3", ".ix4_4", ".ix4_5", ".ix4_6", ".ix4_7",
        //        ".ix5_0", ".ix5_1", ".ix5_2", ".ix5_3", ".ix5_4", ".ix5_5", ".ix5_6", ".ix5_7",
        //        ".Qx0_0", ".Qx0_1", ".Qx0_2", ".Qx0_3", ".Qx0_4", ".Qx0_5", ".Qx0_6", ".Qx0_7",
        //        ".Qx1_0", ".Qx1_1", ".Qx1_2", ".Qx1_3", ".Qx1_4", ".Qx1_5", ".Qx1_6", ".Qx1_7",
        //        ".Qx2_0", ".Qx2_1", ".Qx2_2", ".Qx2_3", ".Qx2_4", ".Qx2_5", ".Qx2_6", ".Qx2_7",
        //        ".Qx3_0", ".Qx3_1", ".Qx3_2", ".Qx3_3", ".Qx3_4", ".Qx3_5", ".Qx3_6", ".Qx3_7",
        //        ".Qx4_0", ".Qx4_1", ".Qx4_2", ".Qx4_3", ".Qx4_4", ".Qx4_5", ".Qx4_6", ".Qx4_7",
        //        ".Qx5_0", ".Qx5_1", ".Qx5_2", ".Qx5_3", ".Qx5_4", ".Qx5_5", ".Qx5_6", ".Qx5_7",};

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    //Read Input
        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //}
        //private void getKoolaidTang()
        //{
        //    //I10 //Q5
        //    string[] InputList =
        //        {
        //        ".ix0_0", ".ix0_1", ".ix0_2", ".ix0_3", ".ix0_4", ".ix0_5", ".ix0_6", ".ix0_7",
        //        ".ix1_0", ".ix1_1", ".ix1_2", ".ix1_3", ".ix1_4", ".ix1_5", ".ix1_6", ".ix1_7",
        //        ".ix2_0", ".ix2_1", ".ix2_2", ".ix2_3", ".ix2_4", ".ix2_5", ".ix2_6", ".ix2_7",
        //        ".ix3_0", ".ix3_1", ".ix3_2", ".ix3_3", ".ix3_4", ".ix3_5", ".ix3_6", ".ix3_7",
        //        ".ix4_0", ".ix4_1", ".ix4_2", ".ix4_3", ".ix4_4", ".ix4_5", ".ix4_6", ".ix4_7",
        //        ".ix5_0", ".ix5_1", ".ix5_2", ".ix5_3", ".ix5_4", ".ix5_5", ".ix5_6", ".ix5_7",
        //        ".ix6_0", ".ix6_1", ".ix6_2", ".ix6_3", ".ix6_4", ".ix6_5", ".ix6_6", ".ix6_7",
        //        ".ix7_0", ".ix7_1", ".ix7_2", ".ix7_3", ".ix7_4", ".ix7_5", ".ix7_6", ".ix7_7",
        //        ".ix8_0", ".ix8_1", ".ix8_2", ".ix8_3", ".ix8_4", ".ix8_5", ".ix8_6", ".ix8_7",
        //        ".ix9_0", ".ix9_1", ".ix9_2", ".ix9_3", ".ix9_4", ".ix9_5", ".ix9_6", ".ix9_7",
        //        ".ix10_0", ".ix10_1", ".ix10_2", ".ix10_3", ".ix10_4", ".ix10_5", ".ix10_6", ".ix10_7",
        //        ".Qx0_0", ".Qx0_1", ".Qx0_2", ".Qx0_3", ".Qx0_4", ".Qx0_5", ".Qx0_6", ".Qx0_7",
        //        ".Qx1_0", ".Qx1_1", ".Qx1_2", ".Qx1_3", ".Qx1_4", ".Qx1_5", ".Qx1_6", ".Qx1_7",
        //        ".Qx2_0", ".Qx2_1", ".Qx2_2", ".Qx2_3", ".Qx2_4", ".Qx2_5", ".Qx2_6", ".Qx2_7",
        //        ".Qx3_0", ".Qx3_1", ".Qx3_2", ".Qx3_3", ".Qx3_4", ".Qx3_5", ".Qx3_6", ".Qx3_7",
        //        ".Qx4_0", ".Qx4_1", ".Qx4_2", ".Qx4_3", ".Qx4_4", ".Qx4_5", ".Qx4_6", ".Qx4_7",
        //        ".Qx5_0", ".Qx5_1", ".Qx5_2", ".Qx5_3", ".Qx5_4", ".Qx5_5", ".Qx5_6", ".Qx5_7",};

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    //Read Input
        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX7_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX8_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX9_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX10_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //}
        //private void getAffixFan()
        //{
        //    //I10 //Q5
        //    string[] InputList =
        //        {
        //        ".ix0_0", ".ix0_1", ".ix0_2", ".ix0_3", ".ix0_4", ".ix0_5", ".ix0_6", ".ix0_7",
        //        ".ix1_0", ".ix1_1", ".ix1_2", ".ix1_3", ".ix1_4", ".ix1_5", ".ix1_6", ".ix1_7",
        //        ".ix2_0", ".ix2_1", ".ix2_2", ".ix2_3", ".ix2_4", ".ix2_5", ".ix2_6", ".ix2_7",
        //        ".ix3_0", ".ix3_1", ".ix3_2", ".ix3_3", ".ix3_4", ".ix3_5", ".ix3_6", ".ix3_7",
        //        ".ix4_0", ".ix4_1", ".ix4_2", ".ix4_3", ".ix4_4", ".ix4_5", ".ix4_6", ".ix4_7",
        //        ".ix5_0", ".ix5_1", ".ix5_2", ".ix5_3", ".ix5_4", ".ix5_5", ".ix5_6", ".ix5_7",
        //        ".ix6_0", ".ix6_1", ".ix6_2", ".ix6_3", ".ix6_4", ".ix6_5", ".ix6_6", ".ix6_7",
        //        ".ix7_0", ".ix7_1", ".ix7_2", ".ix7_3", ".ix7_4", ".ix7_5", ".ix7_6", ".ix7_7",
        //        ".ix8_0", ".ix8_1", ".ix8_2", ".ix8_3", ".ix8_4", ".ix8_5", ".ix8_6", ".ix8_7",
        //        ".ix9_0", ".ix9_1", ".ix9_2", ".ix9_3", ".ix9_4", ".ix9_5", ".ix9_6", ".ix9_7",
        //        ".ix10_0", ".ix10_1", ".ix10_2", ".ix10_3", ".ix10_4", ".ix10_5", ".ix10_6", ".ix10_7",
        //        ".Qx0_0", ".Qx0_1", ".Qx0_2", ".Qx0_3", ".Qx0_4", ".Qx0_5", ".Qx0_6", ".Qx0_7",
        //        ".Qx1_0", ".Qx1_1", ".Qx1_2", ".Qx1_3", ".Qx1_4", ".Qx1_5", ".Qx1_6", ".Qx1_7",
        //        ".Qx2_0", ".Qx2_1", ".Qx2_2", ".Qx2_3", ".Qx2_4", ".Qx2_5", ".Qx2_6", ".Qx2_7",
        //        ".Qx3_0", ".Qx3_1", ".Qx3_2", ".Qx3_3", ".Qx3_4", ".Qx3_5", ".Qx3_6", ".Qx3_7",
        //        ".Qx4_0", ".Qx4_1", ".Qx4_2", ".Qx4_3", ".Qx4_4", ".Qx4_5", ".Qx4_6", ".Qx4_7",
        //        ".Qx5_0", ".Qx5_1", ".Qx5_2", ".Qx5_3", ".Qx5_4", ".Qx5_5", ".Qx5_6", ".Qx5_7",};

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    //Read Input
        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX7_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX8_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX9_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX10_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //}
        //private void getBaffle()
        //{
        //    //I10 //Q7
        //    string[] InputList =
        //        {
        //        ".ix0_0", ".ix0_1", ".ix0_2", ".ix0_3", ".ix0_4", ".ix0_5", ".ix0_6", ".ix0_7",
        //        ".ix1_0", ".ix1_1", ".ix1_2", ".ix1_3", ".ix1_4", ".ix1_5", ".ix1_6", ".ix1_7",
        //        ".ix2_0", ".ix2_1", ".ix2_2", ".ix2_3", ".ix2_4", ".ix2_5", ".ix2_6", ".ix2_7",
        //        ".ix3_0", ".ix3_1", ".ix3_2", ".ix3_3", ".ix3_4", ".ix3_5", ".ix3_6", ".ix3_7",
        //        ".ix4_0", ".ix4_1", ".ix4_2", ".ix4_3", ".ix4_4", ".ix4_5", ".ix4_6", ".ix4_7",
        //        ".ix5_0", ".ix5_1", ".ix5_2", ".ix5_3", ".ix5_4", ".ix5_5", ".ix5_6", ".ix5_7",
        //        ".ix6_0", ".ix6_1", ".ix6_2", ".ix6_3", ".ix6_4", ".ix6_5", ".ix6_6", ".ix6_7",
        //        ".ix7_0", ".ix7_1", ".ix7_2", ".ix7_3", ".ix7_4", ".ix7_5", ".ix7_6", ".ix7_7",
        //        ".ix8_0", ".ix8_1", ".ix8_2", ".ix8_3", ".ix8_4", ".ix8_5", ".ix8_6", ".ix8_7",
        //        ".ix9_0", ".ix9_1", ".ix9_2", ".ix9_3", ".ix9_4", ".ix9_5", ".ix9_6", ".ix9_7",
        //        ".ix10_0", ".ix10_1", ".ix10_2", ".ix10_3", ".ix10_4", ".ix10_5", ".ix10_6", ".ix10_7",
        //        ".Qx0_0", ".Qx0_1", ".Qx0_2", ".Qx0_3", ".Qx0_4", ".Qx0_5", ".Qx0_6", ".Qx0_7",
        //        ".Qx1_0", ".Qx1_1", ".Qx1_2", ".Qx1_3", ".Qx1_4", ".Qx1_5", ".Qx1_6", ".Qx1_7",
        //        ".Qx2_0", ".Qx2_1", ".Qx2_2", ".Qx2_3", ".Qx2_4", ".Qx2_5", ".Qx2_6", ".Qx2_7",
        //        ".Qx3_0", ".Qx3_1", ".Qx3_2", ".Qx3_3", ".Qx3_4", ".Qx3_5", ".Qx3_6", ".Qx3_7",
        //        ".Qx4_0", ".Qx4_1", ".Qx4_2", ".Qx4_3", ".Qx4_4", ".Qx4_5", ".Qx4_6", ".Qx4_7",
        //        ".Qx5_0", ".Qx5_1", ".Qx5_2", ".Qx5_3", ".Qx5_4", ".Qx5_5", ".Qx5_6", ".Qx5_7",
        //        ".Qx6_0", ".Qx6_1", ".Qx6_2", ".Qx6_3", ".Qx6_4", ".Qx6_5", ".Qx6_6", ".Qx6_7",
        //        ".Qx7_0", ".Qx7_1", ".Qx7_2", ".Qx7_3", ".Qx7_4", ".Qx7_5", ".Qx7_6", ".Qx7_7",};

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    //Read Input
        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX7_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX8_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX9_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX10_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX7_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //}
        //private void getBIOSCobra()
        //{
        //    //I10 //Q5
        //    string[] InputList =
        //        {
        //        ".ix0_0", ".ix0_1", ".ix0_2", ".ix0_3", ".ix0_4", ".ix0_5", ".ix0_6", ".ix0_7",
        //        ".ix1_0", ".ix1_1", ".ix1_2", ".ix1_3", ".ix1_4", ".ix1_5", ".ix1_6", ".ix1_7",
        //        ".ix2_0", ".ix2_1", ".ix2_2", ".ix2_3", ".ix2_4", ".ix2_5", ".ix2_6", ".ix2_7",
        //        ".ix3_0", ".ix3_1", ".ix3_2", ".ix3_3", ".ix3_4", ".ix3_5", ".ix3_6", ".ix3_7",
        //        ".ix4_0", ".ix4_1", ".ix4_2", ".ix4_3", ".ix4_4", ".ix4_5", ".ix4_6", ".ix4_7",
        //        ".ix5_0", ".ix5_1", ".ix5_2", ".ix5_3", ".ix5_4", ".ix5_5", ".ix5_6", ".ix5_7",
        //        ".ix6_0", ".ix6_1", ".ix6_2", ".ix6_3", ".ix6_4", ".ix6_5", ".ix6_6", ".ix6_7",
        //        ".ix7_0", ".ix7_1", ".ix7_2", ".ix7_3", ".ix7_4", ".ix7_5", ".ix7_6", ".ix7_7",
        //        ".ix8_0", ".ix8_1", ".ix8_2", ".ix8_3", ".ix8_4", ".ix8_5", ".ix8_6", ".ix8_7",
        //        ".ix9_0", ".ix9_1", ".ix9_2", ".ix9_3", ".ix9_4", ".ix9_5", ".ix9_6", ".ix9_7",
        //        ".ix10_0", ".ix10_1", ".ix10_2", ".ix10_3", ".ix10_4", ".ix10_5", ".ix10_6", ".ix10_7",
        //        ".Qx0_0", ".Qx0_1", ".Qx0_2", ".Qx0_3", ".Qx0_4", ".Qx0_5", ".Qx0_6", ".Qx0_7",
        //        ".Qx1_0", ".Qx1_1", ".Qx1_2", ".Qx1_3", ".Qx1_4", ".Qx1_5", ".Qx1_6", ".Qx1_7",
        //        ".Qx2_0", ".Qx2_1", ".Qx2_2", ".Qx2_3", ".Qx2_4", ".Qx2_5", ".Qx2_6", ".Qx2_7",
        //        ".Qx3_0", ".Qx3_1", ".Qx3_2", ".Qx3_3", ".Qx3_4", ".Qx3_5", ".Qx3_6", ".Qx3_7",
        //        ".Qx4_0", ".Qx4_1", ".Qx4_2", ".Qx4_3", ".Qx4_4", ".Qx4_5", ".Qx4_6", ".Qx4_7",
        //        ".Qx5_0", ".Qx5_1", ".Qx5_2", ".Qx5_3", ".Qx5_4", ".Qx5_5", ".Qx5_6", ".Qx5_7",};

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    //Read Input
        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX7_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX8_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX9_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX10_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //}
        //private void getLargeHeatsink()
        //{
        //    //I7 //Q6
        //    string[] InputList =
        //        {
        //        ".ix0_0", ".ix0_1", ".ix0_2", ".ix0_3", ".ix0_4", ".ix0_5", ".ix0_6", ".ix0_7",
        //        ".ix1_0", ".ix1_1", ".ix1_2", ".ix1_3", ".ix1_4", ".ix1_5", ".ix1_6", ".ix1_7",
        //        ".ix2_0", ".ix2_1", ".ix2_2", ".ix2_3", ".ix2_4", ".ix2_5", ".ix2_6", ".ix2_7",
        //        ".ix3_0", ".ix3_1", ".ix3_2", ".ix3_3", ".ix3_4", ".ix3_5", ".ix3_6", ".ix3_7",
        //        ".ix4_0", ".ix4_1", ".ix4_2", ".ix4_3", ".ix4_4", ".ix4_5", ".ix4_6", ".ix4_7",
        //        ".ix5_0", ".ix5_1", ".ix5_2", ".ix5_3", ".ix5_4", ".ix5_5", ".ix5_6", ".ix5_7",
        //        ".ix6_0", ".ix6_1", ".ix6_2", ".ix6_3", ".ix6_4", ".ix6_5", ".ix6_6", ".ix6_7",
        //        ".ix7_0", ".ix7_1", ".ix7_2", ".ix7_3", ".ix7_4", ".ix7_5", ".ix7_6", ".ix7_7",
        //        ".Qx0_0", ".Qx0_1", ".Qx0_2", ".Qx0_3", ".Qx0_4", ".Qx0_5", ".Qx0_6", ".Qx0_7",
        //        ".Qx1_0", ".Qx1_1", ".Qx1_2", ".Qx1_3", ".Qx1_4", ".Qx1_5", ".Qx1_6", ".Qx1_7",
        //        ".Qx2_0", ".Qx2_1", ".Qx2_2", ".Qx2_3", ".Qx2_4", ".Qx2_5", ".Qx2_6", ".Qx2_7",
        //        ".Qx3_0", ".Qx3_1", ".Qx3_2", ".Qx3_3", ".Qx3_4", ".Qx3_5", ".Qx3_6", ".Qx3_7",
        //        ".Qx4_0", ".Qx4_1", ".Qx4_2", ".Qx4_3", ".Qx4_4", ".Qx4_5", ".Qx4_6", ".Qx4_7",
        //        ".Qx5_0", ".Qx5_1", ".Qx5_2", ".Qx5_3", ".Qx5_4", ".Qx5_5", ".Qx5_6", ".Qx5_7",
        //        ".Qx6_0", ".Qx6_1", ".Qx6_2", ".Qx6_3", ".Qx6_4", ".Qx6_5", ".Qx6_6", ".Qx6_7",};

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    //Read Input
        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX7_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //}
        //private void getSmallHeatsink()
        //{
        //    //I7 //Q6
        //    string[] InputList =
        //        {
        //        ".ix0_0", ".ix0_1", ".ix0_2", ".ix0_3", ".ix0_4", ".ix0_5", ".ix0_6", ".ix0_7",
        //        ".ix1_0", ".ix1_1", ".ix1_2", ".ix1_3", ".ix1_4", ".ix1_5", ".ix1_6", ".ix1_7",
        //        ".ix2_0", ".ix2_1", ".ix2_2", ".ix2_3", ".ix2_4", ".ix2_5", ".ix2_6", ".ix2_7",
        //        ".ix3_0", ".ix3_1", ".ix3_2", ".ix3_3", ".ix3_4", ".ix3_5", ".ix3_6", ".ix3_7",
        //        ".ix4_0", ".ix4_1", ".ix4_2", ".ix4_3", ".ix4_4", ".ix4_5", ".ix4_6", ".ix4_7",
        //        ".ix5_0", ".ix5_1", ".ix5_2", ".ix5_3", ".ix5_4", ".ix5_5", ".ix5_6", ".ix5_7",
        //        ".ix6_0", ".ix6_1", ".ix6_2", ".ix6_3", ".ix6_4", ".ix6_5", ".ix6_6", ".ix6_7",
        //        ".ix7_0", ".ix7_1", ".ix7_2", ".ix7_3", ".ix7_4", ".ix7_5", ".ix7_6", ".ix7_7",
        //        ".Qx0_0", ".Qx0_1", ".Qx0_2", ".Qx0_3", ".Qx0_4", ".Qx0_5", ".Qx0_6", ".Qx0_7",
        //        ".Qx1_0", ".Qx1_1", ".Qx1_2", ".Qx1_3", ".Qx1_4", ".Qx1_5", ".Qx1_6", ".Qx1_7",
        //        ".Qx2_0", ".Qx2_1", ".Qx2_2", ".Qx2_3", ".Qx2_4", ".Qx2_5", ".Qx2_6", ".Qx2_7",
        //        ".Qx3_0", ".Qx3_1", ".Qx3_2", ".Qx3_3", ".Qx3_4", ".Qx3_5", ".Qx3_6", ".Qx3_7",
        //        ".Qx4_0", ".Qx4_1", ".Qx4_2", ".Qx4_3", ".Qx4_4", ".Qx4_5", ".Qx4_6", ".Qx4_7",
        //        ".Qx5_0", ".Qx5_1", ".Qx5_2", ".Qx5_3", ".Qx5_4", ".Qx5_5", ".Qx5_6", ".Qx5_7",
        //        ".Qx6_0", ".Qx6_1", ".Qx6_2", ".Qx6_3", ".Qx6_4", ".Qx6_5", ".Qx6_6", ".Qx6_7",};

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),
        //    };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    //Read Input
        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX7_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX8_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX9_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX10_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //}
        //private void getMainConveyor()
        //{
        //    string[] InputList =
        //                    {
        //    ".ix0_0",   ".ix0_1",   ".ix0_3",   ".ix0_4",   ".ix0_5",   ".ix0_6",   ".ix0_7",
        //    ".ix1_0",   ".ix1_1",   ".ix1_3",   ".ix1_4",   ".ix1_5",   ".ix1_6",   ".ix1_7",
        //    ".ix2_0",   ".ix2_1",   ".ix2_3",   ".ix2_4",   ".ix2_5",   ".ix2_6",   ".ix2_7",
        //    ".ix3_0",   ".ix3_1",   ".ix3_3",   ".ix3_4",   ".ix3_5",   ".ix3_6",   ".ix3_7",
        //    ".ix4_0",   ".ix4_1",   ".ix4_3",   ".ix4_4",   ".ix4_5",   ".ix4_6",   ".ix4_7",
        //    ".ix5_0",   ".ix5_1",   ".ix5_3",   ".ix5_4",   ".ix5_5",   ".ix5_6",   ".ix5_7",
        //    ".ix6_0",   ".ix6_1",   ".ix6_3",   ".ix6_4",   ".ix6_5",   ".ix6_6",   ".ix6_7",
        //    ".ix7_0",   ".ix7_1",   ".ix7_3",   ".ix7_4",   ".ix7_5",   ".ix7_6",   ".ix7_7",
        //    ".ix8_0",   ".ix8_1",   ".ix8_3",   ".ix8_4",   ".ix8_5",   ".ix8_6",   ".ix8_7",
        //    ".ix9_0",   ".ix9_1",   ".ix9_3",   ".ix9_4",   ".ix9_5",   ".ix9_6",   ".ix9_7",
        //    ".ix10_0",  ".ix10_1",  ".ix10_3",  ".ix10_4",  ".ix10_5",  ".ix10_6",  ".ix10_7",
        //    };

        //    string[] InputList1 =
        //                   {
        //    ".ix11_0",  ".ix11_1",  ".ix11_3",  ".ix11_4",  ".ix11_5",  ".ix11_6",  ".ix11_7",
        //    ".ix12_0",  ".ix12_1",  ".ix12_3",  ".ix12_4",  ".ix12_5",  ".ix12_6",  ".ix12_7",
        //    ".ix13_0",  ".ix13_1",  ".ix13_3",  ".ix13_4",  ".ix13_5",  ".ix13_6",  ".ix13_7",
        //    ".ix14_0",  ".ix14_1",  ".ix14_3",  ".ix14_4",  ".ix14_5",  ".ix14_6",  ".ix14_7",
        //    ".ix15_0",  ".ix15_1",  ".ix15_3",  ".ix15_4",  ".ix15_5",  ".ix15_6",  ".ix15_7",
        //    ".ix16_0",  ".ix16_1",  ".ix16_3",  ".ix16_4",  ".ix16_5",  ".ix16_6",  ".ix16_7",
        //    ".ix17_0",  ".ix17_1",  ".ix17_3",  ".ix17_4",  ".ix17_5",  ".ix17_6",  ".ix17_7",
        //    ".ix18_0",  ".ix18_1",  ".ix18_3",  ".ix18_4",  ".ix18_5",  ".ix18_6",  ".ix18_7",
        //    ".ix19_0",  ".ix19_1",  ".ix19_3",  ".ix19_4",  ".ix19_5",  ".ix19_6",  ".ix19_7",
        //    ".ix20_0",  ".ix20_1",  ".ix20_3",  ".ix20_4",  ".ix20_5",  ".ix20_6",  ".ix20_7",
        //    };

        //    string[] InputList2 =
        //       {
        //    ".ix21_0",  ".ix21_1",  ".ix21_3",  ".ix21_4",  ".ix21_5",  ".ix21_6",  ".ix21_7",
        //    ".ix22_0",  ".ix22_1",  ".ix22_3",  ".ix22_4",  ".ix22_5",  ".ix22_6",  ".ix22_7",
        //    ".ix23_0",  ".ix23_1",  ".ix23_3",  ".ix23_4",  ".ix23_5",  ".ix23_6",  ".ix23_7",
        //    ".ix24_0",  ".ix24_1",  ".ix24_3",  ".ix24_4",  ".ix24_5",  ".ix24_6",  ".ix24_7",
        //    ".ix25_0",  ".ix25_1",  ".ix25_3",  ".ix25_4",  ".ix25_5",  ".ix25_6",  ".ix25_7",
        //    ".ix26_0",  ".ix26_1",  ".ix26_3",  ".ix26_4",  ".ix26_5",  ".ix26_6",  ".ix26_7",
        //    ".ix27_0",  ".ix27_1",  ".ix27_3",  ".ix27_4",  ".ix27_5",  ".ix27_6",  ".ix27_7",
        //    ".ix28_0",  ".ix28_1",  ".ix28_3",  ".ix28_4",  ".ix28_5",  ".ix28_6",  ".ix28_7",
        //    ".ix29_0",  ".ix29_1",  ".ix29_3",  ".ix29_4",  ".ix29_5",  ".ix29_6",  ".ix29_7",
        //    ".ix30_0",  ".ix30_1",  ".ix30_3",  ".ix30_4",  ".ix30_5",  ".ix30_6",  ".ix30_7",
        //    };

        //    string[] InputList3 =
        //       {
        //    ".ix31_0",  ".ix31_1",  ".ix31_3",  ".ix31_4",  ".ix31_5",  ".ix31_6",  ".ix31_7",
        //    ".ix32_0",  ".ix32_1",  ".ix32_3",  ".ix32_4",  ".ix32_5",  ".ix32_6",  ".ix32_7",
        //    ".ix33_0",  ".ix33_1",  ".ix33_3",  ".ix33_4",  ".ix33_5",  ".ix33_6",  ".ix33_7",
        //    ".ix34_0",  ".ix34_1",  ".ix34_3",  ".ix34_4",  ".ix34_5",  ".ix34_6",  ".ix34_7",
        //    ".ix35_0",  ".ix35_1",  ".ix35_3",  ".ix35_4",  ".ix35_5",  ".ix35_6",  ".ix35_7",
        //    ".ix36_0",  ".ix36_1",  ".ix36_3",  ".ix36_4",  ".ix36_5",  ".ix36_6",  ".ix36_7",
        //    ".ix37_0",  ".ix37_1",  ".ix37_3",  ".ix37_4",  ".ix37_5",  ".ix37_6",  ".ix37_7",
        //    ".ix38_0",  ".ix38_1",  ".ix38_3",  ".ix38_4",  ".ix38_5",  ".ix38_6",  ".ix38_7",
        //    ".ix39_0",  ".ix39_1",  ".ix39_3",  ".ix39_4",  ".ix39_5",  ".ix39_6",  ".ix39_7",
        //    ".ix40_0",  ".ix40_1",  ".ix40_3",  ".ix40_4",  ".ix40_5",  ".ix40_6",  ".ix40_7",
        //    };

        //    string[] InputList4 =
        //       {
        //    ".ix41_0",  ".ix41_1",  ".ix41_3",  ".ix41_4",  ".ix41_5",  ".ix41_6",  ".ix41_7",
        //    ".ix42_0",  ".ix42_1",  ".ix42_3",  ".ix42_4",  ".ix42_5",  ".ix42_6",  ".ix42_7",
        //    ".ix43_0",  ".ix43_1",  ".ix43_3",  ".ix43_4",  ".ix43_5",  ".ix43_6",  ".ix43_7",
        //    ".ix44_0",  ".ix44_1",  ".ix44_3",  ".ix44_4",  ".ix44_5",  ".ix44_6",  ".ix44_7",
        //    ".ix45_0",  ".ix45_1",  ".ix45_3",  ".ix45_4",  ".ix45_5",  ".ix45_6",  ".ix45_7",
        //    ".ix46_0",  ".ix46_1",  ".ix46_3",  ".ix46_4",  ".ix46_5",  ".ix46_6",  ".ix46_7",
        //    ".ix47_0",  ".ix47_1",  ".ix47_3",  ".ix47_4",  ".ix47_5",  ".ix47_6",  ".ix47_7",
        //    ".ix48_0",  ".ix48_1",  ".ix48_3",  ".ix48_4",  ".ix48_5",  ".ix48_6",  ".ix48_7",
        //    ".ix49_0",  ".ix49_1",  ".ix49_3",  ".ix49_4",  ".ix49_5",  ".ix49_6",  ".ix49_7",
        //    ".ix50_0",  ".ix50_1",  ".ix50_3",  ".ix50_4",  ".ix50_5",  ".ix50_6",  ".ix50_7",
        //    };

        //    string[] InputList5 =
        //       {
        //    ".ix51_0",  ".ix51_1",  ".ix51_3",  ".ix51_4",  ".ix51_5",  ".ix51_6",  ".ix51_7",
        //    ".ix52_0",  ".ix52_1",  ".ix52_3",  ".ix52_4",  ".ix52_5",  ".ix52_6",  ".ix52_7",
        //    ".ix53_0",  ".ix53_1",  ".ix53_3",  ".ix53_4",  ".ix53_5",  ".ix53_6",  ".ix53_7",
        //    ".ix54_0",  ".ix54_1",  ".ix54_3",  ".ix54_4",  ".ix54_5",  ".ix54_6",  ".ix54_7",
        //    ".ix55_0",  ".ix55_1",  ".ix55_3",  ".ix55_4",  ".ix55_5",  ".ix55_6",  ".ix55_7",
        //    ".ix56_0",  ".ix56_1",  ".ix56_3",  ".ix56_4",  ".ix56_5",  ".ix56_6",  ".ix56_7",
        //    ".ix57_0",  ".ix57_1",  ".ix57_3",  ".ix57_4",  ".ix57_5",  ".ix57_6",  ".ix57_7",
        //    ".ix58_0",  ".ix58_1",  ".ix58_3",  ".ix58_4",  ".ix58_5",  ".ix58_6",  ".ix58_7",
        //    ".ix59_0",  ".ix59_1",  ".ix59_3",  ".ix59_4",  ".ix59_5",  ".ix59_6",  ".ix59_7",
        //    ".ix60_0",  ".ix60_1",  ".ix60_3",  ".ix60_4",  ".ix60_5",  ".ix60_6",  ".ix60_7",
        //    };

        //    string[] InputList6 =
        //       {
        //    ".ix61_0",  ".ix61_1",  ".ix61_3",  ".ix61_4",  ".ix61_5",  ".ix61_6",  ".ix61_7",
        //    ".ix62_0",  ".ix62_1",  ".ix62_3",  ".ix62_4",  ".ix62_5",  ".ix62_6",  ".ix62_7",
        //    ".ix63_0",  ".ix63_1",  ".ix63_3",  ".ix63_4",  ".ix63_5",  ".ix63_6",  ".ix63_7",
        //    ".ix64_0",  ".ix64_1",  ".ix64_3",  ".ix64_4",  ".ix64_5",  ".ix64_6",  ".ix64_7",
        //    ".ix65_0",  ".ix65_1",  ".ix65_3",  ".ix65_4",  ".ix65_5",  ".ix65_6",  ".ix65_7",
        //    ".ix66_0",  ".ix66_1",  ".ix66_3",  ".ix66_4",  ".ix66_5",  ".ix66_6",  ".ix66_7",
        //    ".ix67_0",  ".ix67_1",  ".ix67_3",  ".ix67_4",  ".ix67_5",  ".ix67_6",  ".ix67_7",
        //    ".ix68_0",  ".ix68_1",  ".ix68_3",  ".ix68_4",  ".ix68_5",  ".ix68_6",  ".ix68_7",
        //    ".ix69_0",  ".ix69_1",  ".ix69_3",  ".ix69_4",  ".ix69_5",  ".ix69_6",  ".ix69_7",
        //    ".ix70_0",  ".ix70_1",  ".ix70_3",  ".ix70_4",  ".ix70_5",  ".ix70_6",  ".ix70_7",
        //    };

        //    string[] InputList7 =
        //       {
        //    ".ix71_0",  ".ix71_1",  ".ix71_3",  ".ix71_4",  ".ix71_5",  ".ix71_6",  ".ix71_7",
        //    ".ix72_0",  ".ix72_1",  ".ix72_3",  ".ix72_4",  ".ix72_5",  ".ix72_6",  ".ix72_7",
        //    ".ix73_0",  ".ix73_1",  ".ix73_3",  ".ix73_4",  ".ix73_5",  ".ix73_6",  ".ix73_7",
        //    ".ix74_0",  ".ix74_1",  ".ix74_3",  ".ix74_4",  ".ix74_5",  ".ix74_6",  ".ix74_7",
        //    ".ix75_0",  ".ix75_1",  ".ix75_3",  ".ix75_4",  ".ix75_5",  ".ix75_6",  ".ix75_7",
        //    ".ix76_0",  ".ix76_1",  ".ix76_3",  ".ix76_4",  ".ix76_5",  ".ix76_6",  ".ix76_7",
        //    ".ix77_0",  ".ix77_1",  ".ix77_3",  ".ix77_4",  ".ix77_5",  ".ix77_6",  ".ix77_7",
        //    ".ix78_0",  ".ix78_1",  ".ix78_3",  ".ix78_4",  ".ix78_5",  ".ix78_6",  ".ix78_7",
        //    ".ix79_0",  ".ix79_1",  ".ix79_3",  ".ix79_4",  ".ix79_5",  ".ix79_6",  ".ix79_7",
        //    ".ix80_0",  ".ix80_1",  ".ix80_3",  ".ix80_4",  ".ix80_5",  ".ix80_6",  ".ix80_7",
        //    };

        //    string[] InputList8 =
        //       {
        //    ".ix81_0",  ".ix81_1",  ".ix81_3",  ".ix81_4",  ".ix81_5",  ".ix81_6",  ".ix81_7",
        //    ".ix82_0",  ".ix82_1",  ".ix82_3",  ".ix82_4",  ".ix82_5",  ".ix82_6",  ".ix82_7",
        //    ".ix83_0",  ".ix83_1",  ".ix83_3",  ".ix83_4",  ".ix83_5",  ".ix83_6",  ".ix83_7",
        //    ".ix84_0",  ".ix84_1",  ".ix84_3",  ".ix84_4",  ".ix84_5",  ".ix84_6",  ".ix84_7",
        //    ".ix85_0",  ".ix85_1",  ".ix85_3",  ".ix85_4",  ".ix85_5",  ".ix85_6",  ".ix85_7",
        //    ".ix86_0",  ".ix86_1",  ".ix86_3",  ".ix86_4",  ".ix86_5",  ".ix86_6",  ".ix86_7",
        //    ".ix87_0",  ".ix87_1",  ".ix87_3",  ".ix87_4",  ".ix87_5",  ".ix87_6",  ".ix87_7",
        //    ".ix88_0",  ".ix88_1",  ".ix88_3",  ".ix88_4",  ".ix88_5",  ".ix88_6",  ".ix88_7",
        //    ".ix89_0",  ".ix89_1",  ".ix89_3",  ".ix89_4",  ".ix89_5",  ".ix89_6",  ".ix89_7",
        //    ".ix90_0",  ".ix90_1",  ".ix90_3",  ".ix90_4",  ".ix90_5",  ".ix90_6",  ".ix90_7",
        //    ".ix91_0",  ".ix91_1",  ".ix91_3",  ".ix91_4",  ".ix91_5",  ".ix91_6",  ".ix91_7",
        //    ".ix92_0",  ".ix92_1",  ".ix92_3",  ".ix92_4",  ".ix92_5",  ".ix92_6",  ".ix92_7",
        //    ".ix93_0",  ".ix93_1",  ".ix93_3",  ".ix93_4",  ".ix93_5",  ".ix93_6",  ".ix93_7",
        //    ".ix94_0",  ".ix94_1",  ".ix94_3",  ".ix94_4",  ".ix94_5",  ".ix94_6",  ".ix94_7",
        //    };

        //    string[] InputListQ =
        //        {
        //    ".Qx0_0",   ".Qx0_1",   ".Qx0_3",   ".Qx0_4",   ".Qx0_5",   ".Qx0_6",   ".Qx0_7",
        //    ".Qx1_0",   ".Qx1_1",   ".Qx1_3",   ".Qx1_4",   ".Qx1_5",   ".Qx1_6",   ".Qx1_7",
        //    ".Qx2_0",   ".Qx2_1",   ".Qx2_3",   ".Qx2_4",   ".Qx2_5",   ".Qx2_6",   ".Qx2_7",
        //    ".Qx3_0",   ".Qx3_1",   ".Qx3_3",   ".Qx3_4",   ".Qx3_5",   ".Qx3_6",   ".Qx3_7",
        //    ".Qx4_0",   ".Qx4_1",   ".Qx4_3",   ".Qx4_4",   ".Qx4_5",   ".Qx4_6",   ".Qx4_7",
        //    ".Qx5_0",   ".Qx5_1",   ".Qx5_3",   ".Qx5_4",   ".Qx5_5",   ".Qx5_6",   ".Qx5_7",
        //    ".Qx6_0",   ".Qx6_1",   ".Qx6_3",   ".Qx6_4",   ".Qx6_5",   ".Qx6_6",   ".Qx6_7",
        //    ".Qx7_0",   ".Qx7_1",   ".Qx7_3",   ".Qx7_4",   ".Qx7_5",   ".Qx7_6",   ".Qx7_7",
        //    ".Qx8_0",   ".Qx8_1",   ".Qx8_3",   ".Qx8_4",   ".Qx8_5",   ".Qx8_6",   ".Qx8_7",
        //    ".Qx9_0",   ".Qx9_1",   ".Qx9_3",   ".Qx9_4",   ".Qx9_5",   ".Qx9_6",   ".Qx9_7",
        //    ".Qx10_0",  ".Qx10_1",  ".Qx10_3",  ".Qx10_4",  ".Qx10_5",  ".Qx10_6",  ".Qx10_7",
        //    };

        //    string[] InputListQ1 =
        //        {
        //    ".Qx11_0",  ".Qx11_1",  ".Qx11_3",  ".Qx11_4",  ".Qx11_5",  ".Qx11_6",  ".Qx11_7",
        //    ".Qx12_0",  ".Qx12_1",  ".Qx12_3",  ".Qx12_4",  ".Qx12_5",  ".Qx12_6",  ".Qx12_7",
        //    ".Qx13_0",  ".Qx13_1",  ".Qx13_3",  ".Qx13_4",  ".Qx13_5",  ".Qx13_6",  ".Qx13_7",
        //    ".Qx14_0",  ".Qx14_1",  ".Qx14_3",  ".Qx14_4",  ".Qx14_5",  ".Qx14_6",  ".Qx14_7",
        //    ".Qx15_0",  ".Qx15_1",  ".Qx15_3",  ".Qx15_4",  ".Qx15_5",  ".Qx15_6",  ".Qx15_7",
        //    ".Qx16_0",  ".Qx16_1",  ".Qx16_3",  ".Qx16_4",  ".Qx16_5",  ".Qx16_6",  ".Qx16_7",
        //    ".Qx17_0",  ".Qx17_1",  ".Qx17_3",  ".Qx17_4",  ".Qx17_5",  ".Qx17_6",  ".Qx17_7",
        //    ".Qx18_0",  ".Qx18_1",  ".Qx18_3",  ".Qx18_4",  ".Qx18_5",  ".Qx18_6",  ".Qx18_7",
        //    ".Qx19_0",  ".Qx19_1",  ".Qx19_3",  ".Qx19_4",  ".Qx19_5",  ".Qx19_6",  ".Qx19_7",
        //    ".Qx20_0",  ".Qx20_1",  ".Qx20_3",  ".Qx20_4",  ".Qx20_5",  ".Qx20_6",  ".Qx20_7",
        //    };

        //    string[] InputListQ2 =
        //        {
        //    ".Qx21_0",  ".Qx21_1",  ".Qx21_3",  ".Qx21_4",  ".Qx21_5",  ".Qx21_6",  ".Qx21_7",
        //    ".Qx22_0",  ".Qx22_1",  ".Qx22_3",  ".Qx22_4",  ".Qx22_5",  ".Qx22_6",  ".Qx22_7",
        //    ".Qx23_0",  ".Qx23_1",  ".Qx23_3",  ".Qx23_4",  ".Qx23_5",  ".Qx23_6",  ".Qx23_7",
        //    ".Qx24_0",  ".Qx24_1",  ".Qx24_3",  ".Qx24_4",  ".Qx24_5",  ".Qx24_6",  ".Qx24_7",
        //    ".Qx25_0",  ".Qx25_1",  ".Qx25_3",  ".Qx25_4",  ".Qx25_5",  ".Qx25_6",  ".Qx25_7",
        //    ".Qx26_0",  ".Qx26_1",  ".Qx26_3",  ".Qx26_4",  ".Qx26_5",  ".Qx26_6",  ".Qx26_7",
        //    ".Qx27_0",  ".Qx27_1",  ".Qx27_3",  ".Qx27_4",  ".Qx27_5",  ".Qx27_6",  ".Qx27_7",
        //    ".Qx28_0",  ".Qx28_1",  ".Qx28_3",  ".Qx28_4",  ".Qx28_5",  ".Qx28_6",  ".Qx28_7",
        //    ".Qx29_0",  ".Qx29_1",  ".Qx29_3",  ".Qx29_4",  ".Qx29_5",  ".Qx29_6",  ".Qx29_7",
        //    ".Qx30_0",  ".Qx30_1",  ".Qx30_3",  ".Qx30_4",  ".Qx30_5",  ".Qx30_6",  ".Qx30_7",
        //    };

        //    string[] InputListQ3 =
        //        {
        //    ".Qx31_0",  ".Qx31_1",  ".Qx31_3",  ".Qx31_4",  ".Qx31_5",  ".Qx31_6",  ".Qx31_7",
        //    ".Qx32_0",  ".Qx32_1",  ".Qx32_3",  ".Qx32_4",  ".Qx32_5",  ".Qx32_6",  ".Qx32_7",
        //    ".Qx33_0",  ".Qx33_1",  ".Qx33_3",  ".Qx33_4",  ".Qx33_5",  ".Qx33_6",  ".Qx33_7",
        //    ".Qx34_0",  ".Qx34_1",  ".Qx34_3",  ".Qx34_4",  ".Qx34_5",  ".Qx34_6",  ".Qx34_7",
        //    ".Qx35_0",  ".Qx35_1",  ".Qx35_3",  ".Qx35_4",  ".Qx35_5",  ".Qx35_6",  ".Qx35_7",
        //    ".Qx36_0",  ".Qx36_1",  ".Qx36_3",  ".Qx36_4",  ".Qx36_5",  ".Qx36_6",  ".Qx36_7",
        //    ".Qx37_0",  ".Qx37_1",  ".Qx37_3",  ".Qx37_4",  ".Qx37_5",  ".Qx37_6",  ".Qx37_7",
        //    ".Qx38_0",  ".Qx38_1",  ".Qx38_3",  ".Qx38_4",  ".Qx38_5",  ".Qx38_6",  ".Qx38_7",
        //    ".Qx39_0",  ".Qx39_1",  ".Qx39_3",  ".Qx39_4",  ".Qx39_5",  ".Qx39_6",  ".Qx39_7",
        //    ".Qx40_0",  ".Qx40_1",  ".Qx40_3",  ".Qx40_4",  ".Qx40_5",  ".Qx40_6",  ".Qx40_7",
        //    };

        //    string[] InputListQ4 =
        //        {
        //    ".Qx41_0",  ".Qx41_1",  ".Qx41_3",  ".Qx41_4",  ".Qx41_5",  ".Qx41_6",  ".Qx41_7",
        //    ".Qx42_0",  ".Qx42_1",  ".Qx42_3",  ".Qx42_4",  ".Qx42_5",  ".Qx42_6",  ".Qx42_7",
        //    ".Qx43_0",  ".Qx43_1",  ".Qx43_3",  ".Qx43_4",  ".Qx43_5",  ".Qx43_6",  ".Qx43_7",
        //    ".Qx44_0",  ".Qx44_1",  ".Qx44_3",  ".Qx44_4",  ".Qx44_5",  ".Qx44_6",  ".Qx44_7",
        //    ".Qx45_0",  ".Qx45_1",  ".Qx45_3",  ".Qx45_4",  ".Qx45_5",  ".Qx45_6",  ".Qx45_7",
        //    ".Qx46_0",  ".Qx46_1",  ".Qx46_3",  ".Qx46_4",  ".Qx46_5",  ".Qx46_6",  ".Qx46_7",
        //    ".Qx47_0",  ".Qx47_1",  ".Qx47_3",  ".Qx47_4",  ".Qx47_5",  ".Qx47_6",  ".Qx47_7",
        //    ".Qx48_0",  ".Qx48_1",  ".Qx48_3",  ".Qx48_4",  ".Qx48_5",  ".Qx48_6",  ".Qx48_7",
        //    ".Qx49_0",  ".Qx49_1",  ".Qx49_3",  ".Qx49_4",  ".Qx49_5",  ".Qx49_6",  ".Qx49_7",
        //    ".Qx50_0",  ".Qx50_1",  ".Qx50_3",  ".Qx50_4",  ".Qx50_5",  ".Qx50_6",  ".Qx50_7",
        //    };

        //    string[] InputListQ5 =
        //        {
        //    ".Qx51_0",  ".Qx51_1",  ".Qx51_3",  ".Qx51_4",  ".Qx51_5",  ".Qx51_6",  ".Qx51_7",
        //    ".Qx52_0",  ".Qx52_1",  ".Qx52_3",  ".Qx52_4",  ".Qx52_5",  ".Qx52_6",  ".Qx52_7",
        //    ".Qx53_0",  ".Qx53_1",  ".Qx53_3",  ".Qx53_4",  ".Qx53_5",  ".Qx53_6",  ".Qx53_7",
        //    ".Qx54_0",  ".Qx54_1",  ".Qx54_3",  ".Qx54_4",  ".Qx54_5",  ".Qx54_6",  ".Qx54_7",
        //    ".Qx55_0",  ".Qx55_1",  ".Qx55_3",  ".Qx55_4",  ".Qx55_5",  ".Qx55_6",  ".Qx55_7",
        //    ".Qx56_0",  ".Qx56_1",  ".Qx56_3",  ".Qx56_4",  ".Qx56_5",  ".Qx56_6",  ".Qx56_7",
        //    ".Qx57_0",  ".Qx57_1",  ".Qx57_3",  ".Qx57_4",  ".Qx57_5",  ".Qx57_6",  ".Qx57_7",
        //    ".Qx58_0",  ".Qx58_1",  ".Qx58_3",  ".Qx58_4",  ".Qx58_5",  ".Qx58_6",  ".Qx58_7",
        //    ".Qx59_0",  ".Qx59_1",  ".Qx59_3",  ".Qx59_4",  ".Qx59_5",  ".Qx59_6",  ".Qx59_7",
        //    ".Qx60_0",  ".Qx60_1",  ".Qx60_3",  ".Qx60_4",  ".Qx60_5",  ".Qx60_6",  ".Qx60_7",
        //    };

        //    string[] InputListQ6 =
        //        {
        //    ".Qx61_0",  ".Qx61_1",  ".Qx61_3",  ".Qx61_4",  ".Qx61_5",  ".Qx61_6",  ".Qx61_7",
        //    ".Qx62_0",  ".Qx62_1",  ".Qx62_3",  ".Qx62_4",  ".Qx62_5",  ".Qx62_6",  ".Qx62_7",
        //    ".Qx63_0",  ".Qx63_1",  ".Qx63_3",  ".Qx63_4",  ".Qx63_5",  ".Qx63_6",  ".Qx63_7",
        //    ".Qx64_0",  ".Qx64_1",  ".Qx64_3",  ".Qx64_4",  ".Qx64_5",  ".Qx64_6",  ".Qx64_7",
        //    ".Qx65_0",  ".Qx65_1",  ".Qx65_3",  ".Qx65_4",  ".Qx65_5",  ".Qx65_6",  ".Qx65_7",
        //    ".Qx66_0",  ".Qx66_1",  ".Qx66_3",  ".Qx66_4",  ".Qx66_5",  ".Qx66_6",  ".Qx66_7",
        //    ".Qx67_0",  ".Qx67_1",  ".Qx67_3",  ".Qx67_4",  ".Qx67_5",  ".Qx67_6",  ".Qx67_7",
        //    ".Qx68_0",  ".Qx68_1",  ".Qx68_3",  ".Qx68_4",  ".Qx68_5",  ".Qx68_6",  ".Qx68_7",
        //    ".Qx69_0",  ".Qx69_1",  ".Qx69_3",  ".Qx69_4",  ".Qx69_5",  ".Qx69_6",  ".Qx69_7",
        //    ".Qx70_0",  ".Qx70_1",  ".Qx70_3",  ".Qx70_4",  ".Qx70_5",  ".Qx70_6",  ".Qx70_7",
        //    };

        //    string[] InputListQ7 =
        //        {
        //    ".Qx71_0",  ".Qx71_1",  ".Qx71_3",  ".Qx71_4",  ".Qx71_5",  ".Qx71_6",  ".Qx71_7",
        //    ".Qx72_0",  ".Qx72_1",  ".Qx72_3",  ".Qx72_4",  ".Qx72_5",  ".Qx72_6",  ".Qx72_7",
        //    ".Qx73_0",  ".Qx73_1",  ".Qx73_3",  ".Qx73_4",  ".Qx73_5",  ".Qx73_6",  ".Qx73_7",
        //    ".Qx74_0",  ".Qx74_1",  ".Qx74_3",  ".Qx74_4",  ".Qx74_5",  ".Qx74_6",  ".Qx74_7",
        //    ".Qx75_0",  ".Qx75_1",  ".Qx75_3",  ".Qx75_4",  ".Qx75_5",  ".Qx75_6",  ".Qx75_7",
        //    ".Qx76_0",  ".Qx76_1",  ".Qx76_3",  ".Qx76_4",  ".Qx76_5",  ".Qx76_6",  ".Qx76_7",
        //    ".Qx77_0",  ".Qx77_1",  ".Qx77_3",  ".Qx77_4",  ".Qx77_5",  ".Qx77_6",  ".Qx77_7",
        //    ".Qx78_0",  ".Qx78_1",  ".Qx78_3",  ".Qx78_4",  ".Qx78_5",  ".Qx78_6",  ".Qx78_7",
        //    ".Qx79_0",  ".Qx79_1",  ".Qx79_3",  ".Qx79_4",  ".Qx79_5",  ".Qx79_6",  ".Qx79_7",
        //    };

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList);
        //    uint[] inputhandles = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList1);
        //    uint[] inputhandles1 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList2);
        //    uint[] inputhandles2 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList3);
        //    uint[] inputhandles3 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList4);
        //    uint[] inputhandles4 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList5);
        //    uint[] inputhandles5 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList6);
        //    uint[] inputhandles6 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList7);
        //    uint[] inputhandles7 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputList8);
        //    uint[] inputhandles8 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputListQ);
        //    uint[] inputhandlesQ = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputListQ1);
        //    uint[] inputhandlesQ1 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputListQ2);
        //    uint[] inputhandlesQ2 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputListQ3);
        //    uint[] inputhandlesQ3 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputListQ4);
        //    uint[] inputhandlesQ4 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputListQ5);
        //    uint[] inputhandlesQ5 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputListQ6);
        //    uint[] inputhandlesQ6 = createHandlesCommand.CreateHandles();

        //    createHandlesCommand = new SumCreateHandles(A_tcClient, InputListQ7);
        //    uint[] inputhandlesQ7 = createHandlesCommand.CreateHandles();

        //    Type[] InputvalueTypes = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//0
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//1
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//2
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//3
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//4
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//5
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//6
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//7
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//8
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//9
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//10
        //    };

        //    Type[] InputvalueTypes2 = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//0
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//1
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//2
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//3
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//4
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//5
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//6
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//7
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//8
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//9
        //    };

        //    Type[] InputvalueTypes3 = new Type[]
        //    {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//0
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//1
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//2
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//3
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//4
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//5
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//6
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//7
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//8
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//9
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//10
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//11
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//12
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//13
        //    };

        //    Type[] InputvalueTypes4 = new Type[]
        //   {typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//0
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//1
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//2
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//3
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//4
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//5
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//6
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//7
        //    typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),typeof(Boolean),//8
        //   };

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValues = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles1, InputvalueTypes2);
        //    object[] readValues1 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles2, InputvalueTypes2);
        //    object[] readValues2 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles3, InputvalueTypes2);
        //    object[] readValues3 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles4, InputvalueTypes2);
        //    object[] readValues4 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles5, InputvalueTypes2);
        //    object[] readValues5 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles6, InputvalueTypes2);
        //    object[] readValues6 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles7, InputvalueTypes2);
        //    object[] readValues7 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles8, InputvalueTypes3);
        //    object[] readValues8 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles, InputvalueTypes);
        //    object[] readValuesQ = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles1, InputvalueTypes2);
        //    object[] readValuesQ1 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles2, InputvalueTypes2);
        //    object[] readValuesQ2 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles3, InputvalueTypes2);
        //    object[] readValuesQ3 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles4, InputvalueTypes2);
        //    object[] readValuesQ4 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles5, InputvalueTypes2);
        //    object[] readValuesQ5 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles6, InputvalueTypes2);
        //    object[] readValuesQ6 = readCommand.Read();

        //    readCommand = new SumHandleRead(A_tcClient, inputhandles7, InputvalueTypes4);
        //    object[] readValuesQ7 = readCommand.Read();

        //    int i = 0;
        //    Utilities.BeckHoff.Input.IX0_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX0_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX1_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX1_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX2_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX2_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX3_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX3_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX4_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX4_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX5_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX5_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX6_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX6_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX7_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX7_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX8_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX8_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX9_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX9_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX10_00 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_01 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_02 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_03 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_04 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_05 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_06 = Convert.ToBoolean(readValues[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX10_07 = Convert.ToBoolean(readValues[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Input.IX11_00 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_01 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_02 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_03 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_04 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_05 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_06 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX11_07 = Convert.ToBoolean(readValues1[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX12_00 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_01 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_02 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_03 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_04 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_05 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_06 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX12_07 = Convert.ToBoolean(readValues1[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX13_00 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_01 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_02 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_03 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_04 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_05 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_06 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX13_07 = Convert.ToBoolean(readValues1[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX14_00 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_01 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_02 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_03 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_04 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_05 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_06 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX14_07 = Convert.ToBoolean(readValues1[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX15_00 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_01 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_02 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_03 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_04 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_05 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_06 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX15_07 = Convert.ToBoolean(readValues1[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX16_00 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_01 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_02 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_03 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_04 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_05 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_06 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX16_07 = Convert.ToBoolean(readValues1[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX17_00 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX17_01 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX17_02 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX17_03 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX17_04 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX17_05 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX17_06 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX17_07 = Convert.ToBoolean(readValues1[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX18_00 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX18_01 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX18_02 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX18_03 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX18_04 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX18_05 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX18_06 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX18_07 = Convert.ToBoolean(readValues1[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX19_00 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX19_01 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX19_02 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX19_03 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX19_04 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX19_05 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX19_06 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX19_07 = Convert.ToBoolean(readValues1[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX20_00 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX20_01 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX20_02 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX20_03 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX20_04 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX20_05 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX20_06 = Convert.ToBoolean(readValues1[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX20_07 = Convert.ToBoolean(readValues1[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Input.IX21_00 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX21_01 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX21_02 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX21_03 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX21_04 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX21_05 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX21_06 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX21_07 = Convert.ToBoolean(readValues2[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX22_00 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX22_01 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX22_02 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX22_03 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX22_04 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX22_05 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX22_06 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX22_07 = Convert.ToBoolean(readValues2[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX23_00 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX23_01 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX23_02 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX23_03 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX23_04 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX23_05 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX23_06 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX23_07 = Convert.ToBoolean(readValues2[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX24_00 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX24_01 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX24_02 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX24_03 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX24_04 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX24_05 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX24_06 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX24_07 = Convert.ToBoolean(readValues2[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX25_00 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX25_01 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX25_02 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX25_03 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX25_04 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX25_05 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX25_06 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX25_07 = Convert.ToBoolean(readValues2[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX26_00 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX26_01 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX26_02 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX26_03 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX26_04 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX26_05 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX26_06 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX26_07 = Convert.ToBoolean(readValues2[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX27_00 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX27_01 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX27_02 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX27_03 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX27_04 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX27_05 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX27_06 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX27_07 = Convert.ToBoolean(readValues2[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX28_00 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX28_01 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX28_02 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX28_03 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX28_04 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX28_05 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX28_06 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX28_07 = Convert.ToBoolean(readValues2[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX29_00 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX29_01 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX29_02 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX29_03 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX29_04 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX29_05 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX29_06 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX29_07 = Convert.ToBoolean(readValues2[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX30_00 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX30_01 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX30_02 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX30_03 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX30_04 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX30_05 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX30_06 = Convert.ToBoolean(readValues2[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX30_07 = Convert.ToBoolean(readValues2[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Input.IX31_00 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX31_01 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX31_02 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX31_03 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX31_04 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX31_05 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX31_06 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX31_07 = Convert.ToBoolean(readValues3[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX32_00 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX32_01 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX32_02 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX32_03 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX32_04 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX32_05 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX32_06 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX32_07 = Convert.ToBoolean(readValues3[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX33_00 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX33_01 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX33_02 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX33_03 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX33_04 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX33_05 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX33_06 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX33_07 = Convert.ToBoolean(readValues3[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX34_00 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX34_01 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX34_02 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX34_03 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX34_04 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX34_05 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX34_06 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX34_07 = Convert.ToBoolean(readValues3[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX35_00 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX35_01 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX35_02 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX35_03 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX35_04 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX35_05 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX35_06 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX35_07 = Convert.ToBoolean(readValues3[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX36_00 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX36_01 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX36_02 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX36_03 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX36_04 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX36_05 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX36_06 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX36_07 = Convert.ToBoolean(readValues3[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX37_00 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX37_01 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX37_02 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX37_03 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX37_04 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX37_05 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX37_06 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX37_07 = Convert.ToBoolean(readValues3[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX38_00 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX38_01 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX38_02 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX38_03 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX38_04 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX38_05 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX38_06 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX38_07 = Convert.ToBoolean(readValues3[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX39_00 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX39_01 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX39_02 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX39_03 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX39_04 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX39_05 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX39_06 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX39_07 = Convert.ToBoolean(readValues3[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX40_00 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX40_01 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX40_02 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX40_03 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX40_04 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX40_05 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX40_06 = Convert.ToBoolean(readValues3[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX40_07 = Convert.ToBoolean(readValues3[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Input.IX41_00 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX41_01 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX41_02 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX41_03 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX41_04 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX41_05 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX41_06 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX41_07 = Convert.ToBoolean(readValues4[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX42_00 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX42_01 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX42_02 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX42_03 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX42_04 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX42_05 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX42_06 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX42_07 = Convert.ToBoolean(readValues4[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX43_00 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX43_01 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX43_02 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX43_03 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX43_04 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX43_05 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX43_06 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX43_07 = Convert.ToBoolean(readValues4[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX44_00 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX44_01 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX44_02 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX44_03 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX44_04 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX44_05 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX44_06 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX44_07 = Convert.ToBoolean(readValues4[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX45_00 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX45_01 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX45_02 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX45_03 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX45_04 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX45_05 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX45_06 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX45_07 = Convert.ToBoolean(readValues4[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX46_00 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX46_01 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX46_02 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX46_03 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX46_04 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX46_05 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX46_06 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX46_07 = Convert.ToBoolean(readValues4[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX47_00 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX47_01 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX47_02 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX47_03 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX47_04 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX47_05 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX47_06 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX47_07 = Convert.ToBoolean(readValues4[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX48_00 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX48_01 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX48_02 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX48_03 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX48_04 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX48_05 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX48_06 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX48_07 = Convert.ToBoolean(readValues4[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX49_00 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX49_01 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX49_02 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX49_03 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX49_04 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX49_05 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX49_06 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX49_07 = Convert.ToBoolean(readValues4[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX50_00 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX50_01 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX50_02 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX50_03 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX50_04 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX50_05 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX50_06 = Convert.ToBoolean(readValues4[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX50_07 = Convert.ToBoolean(readValues4[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Input.IX51_00 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX51_01 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX51_02 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX51_03 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX51_04 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX51_05 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX51_06 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX51_07 = Convert.ToBoolean(readValues5[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX52_00 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX52_01 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX52_02 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX52_03 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX52_04 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX52_05 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX52_06 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX52_07 = Convert.ToBoolean(readValues5[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX53_00 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX53_01 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX53_02 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX53_03 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX53_04 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX53_05 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX53_06 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX53_07 = Convert.ToBoolean(readValues5[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX54_00 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX54_01 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX54_02 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX54_03 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX54_04 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX54_05 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX54_06 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX54_07 = Convert.ToBoolean(readValues5[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX55_00 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX55_01 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX55_02 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX55_03 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX55_04 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX55_05 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX55_06 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX55_07 = Convert.ToBoolean(readValues5[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX56_00 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX56_01 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX56_02 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX56_03 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX56_04 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX56_05 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX56_06 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX56_07 = Convert.ToBoolean(readValues5[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX57_00 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX57_01 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX57_02 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX57_03 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX57_04 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX57_05 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX57_06 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX57_07 = Convert.ToBoolean(readValues5[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX58_00 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX58_01 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX58_02 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX58_03 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX58_04 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX58_05 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX58_06 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX58_07 = Convert.ToBoolean(readValues5[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX59_00 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX59_01 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX59_02 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX59_03 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX59_04 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX59_05 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX59_06 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX59_07 = Convert.ToBoolean(readValues5[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX60_00 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX60_01 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX60_02 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX60_03 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX60_04 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX60_05 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX60_06 = Convert.ToBoolean(readValues5[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX60_07 = Convert.ToBoolean(readValues5[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Input.IX61_00 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX61_01 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX61_02 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX61_03 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX61_04 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX61_05 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX61_06 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX61_07 = Convert.ToBoolean(readValues6[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX62_00 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX62_01 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX62_02 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX62_03 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX62_04 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX62_05 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX62_06 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX62_07 = Convert.ToBoolean(readValues6[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX63_00 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX63_01 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX63_02 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX63_03 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX63_04 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX63_05 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX63_06 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX63_07 = Convert.ToBoolean(readValues6[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX64_00 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX64_01 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX64_02 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX64_03 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX64_04 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX64_05 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX64_06 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX64_07 = Convert.ToBoolean(readValues6[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX65_00 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX65_01 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX65_02 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX65_03 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX65_04 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX65_05 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX65_06 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX65_07 = Convert.ToBoolean(readValues6[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX66_00 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX66_01 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX66_02 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX66_03 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX66_04 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX66_05 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX66_06 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX66_07 = Convert.ToBoolean(readValues6[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX67_00 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX67_01 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX67_02 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX67_03 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX67_04 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX67_05 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX67_06 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX67_07 = Convert.ToBoolean(readValues6[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX68_00 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX68_01 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX68_02 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX68_03 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX68_04 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX68_05 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX68_06 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX68_07 = Convert.ToBoolean(readValues6[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX69_00 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX69_01 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX69_02 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX69_03 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX69_04 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX69_05 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX69_06 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX69_07 = Convert.ToBoolean(readValues6[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX70_00 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX70_01 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX70_02 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX70_03 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX70_04 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX70_05 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX70_06 = Convert.ToBoolean(readValues6[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX70_07 = Convert.ToBoolean(readValues6[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Input.IX71_00 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX71_01 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX71_02 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX71_03 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX71_04 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX71_05 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX71_06 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX71_07 = Convert.ToBoolean(readValues7[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX72_00 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX72_01 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX72_02 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX72_03 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX72_04 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX72_05 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX72_06 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX72_07 = Convert.ToBoolean(readValues7[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX73_00 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX73_01 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX73_02 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX73_03 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX73_04 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX73_05 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX73_06 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX73_07 = Convert.ToBoolean(readValues7[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX74_00 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX74_01 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX74_02 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX74_03 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX74_04 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX74_05 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX74_06 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX74_07 = Convert.ToBoolean(readValues7[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX75_00 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX75_01 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX75_02 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX75_03 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX75_04 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX75_05 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX75_06 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX75_07 = Convert.ToBoolean(readValues7[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX76_00 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX76_01 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX76_02 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX76_03 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX76_04 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX76_05 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX76_06 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX76_07 = Convert.ToBoolean(readValues7[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX77_00 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX77_01 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX77_02 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX77_03 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX77_04 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX77_05 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX77_06 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX77_07 = Convert.ToBoolean(readValues7[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX78_00 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX78_01 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX78_02 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX78_03 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX78_04 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX78_05 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX78_06 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX78_07 = Convert.ToBoolean(readValues7[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX79_00 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX79_01 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX79_02 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX79_03 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX79_04 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX79_05 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX79_06 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX79_07 = Convert.ToBoolean(readValues7[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX80_00 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX80_01 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX80_02 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX80_03 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX80_04 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX80_05 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX80_06 = Convert.ToBoolean(readValues7[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX80_07 = Convert.ToBoolean(readValues7[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Input.IX81_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX81_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX81_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX81_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX81_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX81_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX81_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX81_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX82_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX82_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX82_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX82_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX82_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX82_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX82_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX82_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX83_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX83_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX83_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX83_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX83_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX83_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX83_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX83_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX84_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX84_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX84_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX84_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX84_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX84_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX84_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX84_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX85_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX85_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX85_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX85_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX85_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX85_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX85_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX85_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX86_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX86_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX86_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX86_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX86_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX86_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX86_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX86_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX87_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX87_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX87_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX87_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX87_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX87_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX87_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX87_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX88_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX88_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX88_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX88_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX88_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX88_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX88_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX88_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX89_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX89_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX89_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX89_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX89_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX89_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX89_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX89_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX90_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX90_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX90_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX90_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX90_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX90_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX90_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX90_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX91_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX91_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX91_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX91_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX91_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX91_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX91_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX91_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX92_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX92_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX92_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX92_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX92_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX92_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX92_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX92_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX93_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX93_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX93_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX93_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX93_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX93_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX93_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX93_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    Utilities.BeckHoff.Input.IX94_00 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX94_01 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX94_02 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX94_03 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX94_04 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX94_05 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX94_06 = Convert.ToBoolean(readValues8[i]); i = i + 1;
        //    Utilities.BeckHoff.Input.IX94_07 = Convert.ToBoolean(readValues8[i]); i = i + 1;

        //    //Output
        //    i = 0;
        //    Utilities.BeckHoff.Output.QX0_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX0_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX1_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX1_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX2_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX2_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX3_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX3_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX4_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX4_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX5_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX5_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX6_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX6_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX7_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX7_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX8_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX8_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX8_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX8_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX8_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX8_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX8_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX8_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX9_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX9_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX9_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX9_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX9_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX9_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX9_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX9_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX10_00 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX10_01 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX10_02 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX10_03 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX10_04 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX10_05 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX10_06 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX10_07 = Convert.ToBoolean(readValuesQ[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Output.QX11_00 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX11_01 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX11_02 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX11_03 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX11_04 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX11_05 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX11_06 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX11_07 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX12_00 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX12_01 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX12_02 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX12_03 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX12_04 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX12_05 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX12_06 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX12_07 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX13_00 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX13_01 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX13_02 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX13_03 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX13_04 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX13_05 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX13_06 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX13_07 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX14_00 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX14_01 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX14_02 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX14_03 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX14_04 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX14_05 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX14_06 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX14_07 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX15_00 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX15_01 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX15_02 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX15_03 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX15_04 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX15_05 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX15_06 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX15_07 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX16_00 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX16_01 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX16_02 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX16_03 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX16_04 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX16_05 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX16_06 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX16_07 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX17_00 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX17_01 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX17_02 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX17_03 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX17_04 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX17_05 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX17_06 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX17_07 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX18_00 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX18_01 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX18_02 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX18_03 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX18_04 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX18_05 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX18_06 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX18_07 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX19_00 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX19_01 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX19_02 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX19_03 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX19_04 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX19_05 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX19_06 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX19_07 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX20_00 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX20_01 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX20_02 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX20_03 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX20_04 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX20_05 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX20_06 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX20_07 = Convert.ToBoolean(readValuesQ1[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Output.QX21_00 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX21_01 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX21_02 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX21_03 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX21_04 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX21_05 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX21_06 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX21_07 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX22_00 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX22_01 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX22_02 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX22_03 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX22_04 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX22_05 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX22_06 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX22_07 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX23_00 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX23_01 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX23_02 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX23_03 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX23_04 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX23_05 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX23_06 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX23_07 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX24_00 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX24_01 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX24_02 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX24_03 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX24_04 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX24_05 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX24_06 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX24_07 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX25_00 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX25_01 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX25_02 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX25_03 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX25_04 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX25_05 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX25_06 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX25_07 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX26_00 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX26_01 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX26_02 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX26_03 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX26_04 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX26_05 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX26_06 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX26_07 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX27_00 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX27_01 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX27_02 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX27_03 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX27_04 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX27_05 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX27_06 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX27_07 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX28_00 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX28_01 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX28_02 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX28_03 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX28_04 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX28_05 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX28_06 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX28_07 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX29_00 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX29_01 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX29_02 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX29_03 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX29_04 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX29_05 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX29_06 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX29_07 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX30_00 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX30_01 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX30_02 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX30_03 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX30_04 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX30_05 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX30_06 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX30_07 = Convert.ToBoolean(readValuesQ2[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Output.QX31_00 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX31_01 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX31_02 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX31_03 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX31_04 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX31_05 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX31_06 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX31_07 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX32_00 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX32_01 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX32_02 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX32_03 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX32_04 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX32_05 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX32_06 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX32_07 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX33_00 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX33_01 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX33_02 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX33_03 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX33_04 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX33_05 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX33_06 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX33_07 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX34_00 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX34_01 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX34_02 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX34_03 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX34_04 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX34_05 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX34_06 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX34_07 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX35_00 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX35_01 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX35_02 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX35_03 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX35_04 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX35_05 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX35_06 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX35_07 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX36_00 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX36_01 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX36_02 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX36_03 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX36_04 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX36_05 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX36_06 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX36_07 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX37_00 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX37_01 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX37_02 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX37_03 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX37_04 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX37_05 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX37_06 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX37_07 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX38_00 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX38_01 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX38_02 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX38_03 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX38_04 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX38_05 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX38_06 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX38_07 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX39_00 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX39_01 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX39_02 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX39_03 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX39_04 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX39_05 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX39_06 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX39_07 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX40_00 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX40_01 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX40_02 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX40_03 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX40_04 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX40_05 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX40_06 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX40_07 = Convert.ToBoolean(readValuesQ3[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Output.QX41_00 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX41_01 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX41_02 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX41_03 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX41_04 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX41_05 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX41_06 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX41_07 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX42_00 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX42_01 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX42_02 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX42_03 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX42_04 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX42_05 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX42_06 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX42_07 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX43_00 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX43_01 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX43_02 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX43_03 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX43_04 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX43_05 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX43_06 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX43_07 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX44_00 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX44_01 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX44_02 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX44_03 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX44_04 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX44_05 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX44_06 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX44_07 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX45_00 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX45_01 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX45_02 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX45_03 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX45_04 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX45_05 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX45_06 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX45_07 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX46_00 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX46_01 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX46_02 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX46_03 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX46_04 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX46_05 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX46_06 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX46_07 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX47_00 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX47_01 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX47_02 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX47_03 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX47_04 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX47_05 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX47_06 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX47_07 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX48_00 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX48_01 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX48_02 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX48_03 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX48_04 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX48_05 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX48_06 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX48_07 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX49_00 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX49_01 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX49_02 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX49_03 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX49_04 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX49_05 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX49_06 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX49_07 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX50_00 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX50_01 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX50_02 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX50_03 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX50_04 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX50_05 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX50_06 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX50_07 = Convert.ToBoolean(readValuesQ4[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Output.QX51_00 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX51_01 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX51_02 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX51_03 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX51_04 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX51_05 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX51_06 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX51_07 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX52_00 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX52_01 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX52_02 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX52_03 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX52_04 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX52_05 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX52_06 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX52_07 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX53_00 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX53_01 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX53_02 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX53_03 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX53_04 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX53_05 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX53_06 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX53_07 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX54_00 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX54_01 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX54_02 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX54_03 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX54_04 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX54_05 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX54_06 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX54_07 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX55_00 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX55_01 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX55_02 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX55_03 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX55_04 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX55_05 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX55_06 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX55_07 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX56_00 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX56_01 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX56_02 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX56_03 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX56_04 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX56_05 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX56_06 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX56_07 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX57_00 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX57_01 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX57_02 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX57_03 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX57_04 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX57_05 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX57_06 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX57_07 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX58_00 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX58_01 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX58_02 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX58_03 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX58_04 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX58_05 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX58_06 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX58_07 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX59_00 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX59_01 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX59_02 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX59_03 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX59_04 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX59_05 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX59_06 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX59_07 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX60_00 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX60_01 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX60_02 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX60_03 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX60_04 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX60_05 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX60_06 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX60_07 = Convert.ToBoolean(readValuesQ5[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Output.QX61_00 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX61_01 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX61_02 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX61_03 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX61_04 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX61_05 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX61_06 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX61_07 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX62_00 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX62_01 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX62_02 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX62_03 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX62_04 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX62_05 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX62_06 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX62_07 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX63_00 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX63_01 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX63_02 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX63_03 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX63_04 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX63_05 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX63_06 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX63_07 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX64_00 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX64_01 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX64_02 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX64_03 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX64_04 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX64_05 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX64_06 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX64_07 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX65_00 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX65_01 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX65_02 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX65_03 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX65_04 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX65_05 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX65_06 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX65_07 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX66_00 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX66_01 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX66_02 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX66_03 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX66_04 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX66_05 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX66_06 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX66_07 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX67_00 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX67_01 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX67_02 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX67_03 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX67_04 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX67_05 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX67_06 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX67_07 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX68_00 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX68_01 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX68_02 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX68_03 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX68_04 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX68_05 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX68_06 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX68_07 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX69_00 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX69_01 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX69_02 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX69_03 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX69_04 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX69_05 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX69_06 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX69_07 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX70_00 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX70_01 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX70_02 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX70_03 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX70_04 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX70_05 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX70_06 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX70_07 = Convert.ToBoolean(readValuesQ6[i]); i = i + 1;

        //    i = 0;
        //    Utilities.BeckHoff.Output.QX71_00 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX71_01 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX71_02 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX71_03 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX71_04 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX71_05 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX71_06 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX71_07 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX72_00 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX72_01 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX72_02 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX72_03 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX72_04 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX72_05 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX72_06 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX72_07 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX73_00 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX73_01 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX73_02 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX73_03 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX73_04 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX73_05 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX73_06 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX73_07 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX74_00 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX74_01 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX74_02 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX74_03 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX74_04 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX74_05 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX74_06 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX74_07 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX75_00 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX75_01 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX75_02 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX75_03 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX75_04 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX75_05 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX75_06 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX75_07 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX76_00 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX76_01 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX76_02 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX76_03 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX76_04 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX76_05 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX76_06 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX76_07 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX77_00 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX77_01 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX77_02 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX77_03 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX77_04 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX77_05 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX77_06 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX77_07 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX78_00 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX78_01 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX78_02 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX78_03 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX78_04 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX78_05 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX78_06 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX78_07 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;

        //    Utilities.BeckHoff.Output.QX79_00 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX79_01 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX79_02 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX79_03 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX79_04 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX79_05 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX79_06 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;
        //    Utilities.BeckHoff.Output.QX79_07 = Convert.ToBoolean(readValuesQ7[i]); i = i + 1;

        //}
        //#endregion
        //#region Destructor
        //~IOLocation()
        //{
        //    Dispose();
        //}
        //#endregion
    }
}