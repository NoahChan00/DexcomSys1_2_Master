using System;
using System.Collections.Generic;
using System.Data;
using Logix;
using System.Threading;


namespace PentagonHMI.LogicClasses
{
    public class MainHMI : IDisposable
    {
        #region Variables
        LogicClasses.Main _MainConnection;
        public delegate void onUpdateHandler();
        public event onUpdateHandler OnUpdate;
        private VanillaDB.DataDBCall DBCall;
        DataTable dt = new DataTable();
        List<DataTable> DTlist;
        string ErrMsg = "";
        int ATn = 0;
        int StockCnt = 0;
        int TagCnt = 0;
        int IOCnt = 0;
        int Z29n = 0;
        int AssetTagCnt = 0;
        int ORn = 0;
        bool MainHMIOn = true;
        bool MainLoop = true;
        PentagonHMI.ChildControls.ucMainHMI ucMain;

        List<Tag> McStatus = new List<Tag>();
        List<List<Tag>> DisStatus = new List<List<Tag>>();
        List<Tag> DisStn = new List<Tag>();
        List<Tag> DisSte = new List<Tag>();
        Tag[] StockTagList = new Tag[100];
        Tag[] ZoneTagList = new Tag[100];
        Tag[] Z29TagList = new Tag[10];
        Tag[] TagList_MainHMIIOList = new Tag[200];
        Tag Tag_strUPH = new Tag("strUPH");
        public Tag MConline = new Tag("Bool_ConveyorOnline");
        Tag[] Tag_LineTag = new Tag[60];

        Thread Add_Group;
        Thread SensorRead;
        Thread PointIORead;
        //Thread MixedRead;
        Thread AssetTagRead;

        Controller AssetTagPLC = new Controller();
        Controller ZoneStatusPLC = new Controller();
        Controller StatusPLC = new Controller();
        Controller MixedPLC = new Controller();
        Controller MinHMIIOStatusPLC = new Controller();

        class EstopIO
        {
            public Tag First { get; set; }
            public Tag Second { get; set; }
        }

        Tag TagZ0 = new Tag("PIO200_I1.Data.4");
        Tag TagZ3_1 = new Tag("PIO203_I2.Data.6");
        Tag TagZ3_2 = new Tag("PIO203_I2.Data.7");
        Tag TagZ5C_1 = new Tag("PIO206_I5.Data.2");
        Tag TagZ5C_2 = new Tag("PIO206_I5.Data.3");
        Tag TagZ7C_1 = new Tag("PIO208_I5.Data.2");
        Tag TagZ7C_2 = new Tag("PIO208_I5.Data.3");
        Tag TagZ9C_1 = new Tag("PIO210_I5.Data.2");
        Tag TagZ9C_2 = new Tag("PIO210_I5.Data.3");
        Tag TagZ11C_1 = new Tag("PIO211_I4.Data.0");
        Tag TagZ11C_2 = new Tag("PIO211_I4.Data.1");
        Tag TagZ12C_1 = new Tag("PIO212_I4.Data.1");
        Tag TagZ12C_2 = new Tag("PIO212_I4.Data.2");
        Tag TagZ15B_1 = new Tag("PIO216_I5.Data.2");
        Tag TagZ15B_2 = new Tag("PIO216_I5.Data.3");
        Tag TagZ17C_1 = new Tag("PIO218_I5.Data.2");
        Tag TagZ17C_2 = new Tag("PIO218_I5.Data.3");
        Tag TagZ19C_1 = new Tag("PIO220_I5.Data.2");
        Tag TagZ19C_2 = new Tag("PIO220_I5.Data.3");
        Tag TagZ21C_1 = new Tag("PIO222_I5.Data.2");
        Tag TagZ21C_2 = new Tag("PIO222_I5.Data.3");
        Tag TagZ23A_1 = new Tag("PIO223_I4.Data.0");
        Tag TagZ23A_2 = new Tag("PIO223_I4.Data.1");
        Tag TagZ24B_1 = new Tag("PIO224_I2.Data.6");
        Tag TagZ24B_2 = new Tag("PIO224_I2.Data.7");
        Tag TagZ27_1 = new Tag("PIO227_I1.Data.0");
        Tag TagZ27_2 = new Tag("PIO227_I1.Data.1");
        Tag TagZ29D_1 = new Tag("PIO229_I2.Data.5");
        Tag TagZ29D_2 = new Tag("PIO229_I2.Data.6");

        Dictionary<string, EstopIO> EstopDic;

        public string UPHstring { get; set; }
        private string PrevUPHstring { get; set; }

        public string[] _OrderID = new string[60];
        public string[] OrderID { get { return _OrderID; } set { _OrderID = value; } }

        public string[] _StatusList = new string[60];
        public string[] StatusList { get { return _StatusList; } set { _StatusList = value; } }

        public bool[] _DisStatusShuttle = new bool[30];
        public bool[] DisStatusShuttle { get { return _DisStatusShuttle; } set { _DisStatusShuttle = value; } }

        public bool[] _DisStatusStation = new bool[30];
        public bool[] DisStatusStation { get { return _DisStatusStation; } set { _DisStatusStation = value; } }

        public bool[] _ZoneList = new bool[60];
        public bool[] ZoneList { get { return _ZoneList; } set { _ZoneList = value; } }

        public int[] StockQtyList = new int[60];

        public bool[] ZoneStatusList = new bool[60];

        private string[] StockModelList = new string[60];

        private bool[] _BoolMainHMIIOList = new bool[200];
        public bool[] BoolMainHMIIOList { get { return _BoolMainHMIIOList; } set { _BoolMainHMIIOList = value; } }

        private string[] AssetTagList = new string[60];

        private bool[] _BoolZ29 = new bool[200];
        public bool[] BoolZ29 { get { return _BoolZ29; } set { _BoolZ29 = value; } }

        string strUPH = "strUPH";

        List<string> ListStationID = new List<string> { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

        List<string> StationStatus = new List<string> {
                "str_EmptyStnMCStatus",
                "str_AssetTagStnMCStatus",
                "str_MOBOASRS_MCStatus",
                "str_MOBOAssy_MCStatus",
                "str_KnG_MCStatus",
                "str_Affix_MCStatus",
                "str_Baffle_MCStatus",
                "str_BnC_MCStatus",
                "str_LHS_MCStatus",
                "str_SHS_MCStatus",
                "str_PowerCable_MCStatus",//Power
                "str_SATA_MCStatus",//Sata
        };
        List<string> DisableShuttleStatus = new List<string> {
                "bool_Disable_AssetTag_Z1",
                "bool_Disable_AssetTag_Z2",
                "bool_Disable_MOBO_Z1",
                "bool_Disable_MOBO_Z2",
                "bool_Disable_KnG_Z1",
                "bool_Disable_KnG_Z2",
                "bool_Disable_Affix_Z1",
                "bool_Disable_Affix_Z2",
                "bool_Disable_Baffles_Z1",
                "bool_Disable_Baffles_Z2",
                "bool_Disable_BnC_Z1",
                "bool_Disable_BnC_Z2",
                "bool_Disable_LHS_Z1",
                "bool_Disable_LHS_Z2",
                "bool_Disable_SHS_Z1",
                "bool_Disable_SHS_Z2",
        };
        List<string> DisableStationStatus = new List<string> {
                "Empty_ASRS.Disable_Station",
                "AssetTag.Disable_Station",
                "MOBO_ASRS.Disable_Station",
                "MOBO.Disable_Station",
                "KnG.Disable_Station",
                "AffixFan.Disable_Station",
                "Baffles.Disable_Station",
                "BnC.Disable_Station",
                "LHS.Disable_Station",
                "SHS.Disable_Station",
        };

        public List<string> ZoneStatusTagName = new List<string>
        {
            //Zone 0
            "bool_ErrorBit[5]",
            //Zone1
            "bool_ErrorBit[3]", //1//IN OUT
            //Zone2
            "bool_ErrorBit[36]",//2
            //Zone3
            "bool_ErrorBit[72]",//1
            //Zone4
            "bool_ErrorBit[93]",//1
            //Zone5
            "bool_ErrorBit[128]",//3        
            //Zone6
            "bool_ErrorBit[163]",//2
            //Zone7
            "bool_ErrorBit[198]",//3
            //Zone8
            "bool_ErrorBit[223]",//2
            //Zone9
            "bool_ErrorBit[252]",//3
            //Zone10
            "bool_ErrorBit[283]",//2
            //Zone11
            "bool_ErrorBit[307]",//3
            //Zone12
            "bool_ErrorBit[344]",//3
            //Zone13
            "bool_ErrorBit[373]",//2
            //Zone14
            "bool_ErrorBit[395]",//2
            //Zone15
            "bool_ErrorBit[429]",//2
            //Zone16
            "bool_ErrorBit[463]",//2
            //Zone17
            "bool_ErrorBit[492]",//3
            //Zone18
            "bool_ErrorBit[523]",//2
            //Zone19
            "bool_ErrorBit[552]",//3
            //Zone20
            "bool_ErrorBit[583]",//2
            //Zone21
            "bool_ErrorBit[612]",//3
            //Zone22
            "bool_ErrorBit[643]",//2
            //Zone23
            "bool_ErrorBit[668]",//3
            //Zone24
            "bool_ErrorBit[698]",//2
            //Zone25
            "bool_ErrorBit[736]",//1
            //Zone26
            "bool_ErrorBit[754]",//1
            //Zone27
            "bool_ErrorBit[783]",//1 IN OUT
            //Zone28
            "bool_ErrorBit[826]",//1 NO Sensor
            //Zone29 
            "bool_ErrorBit[846]",//4
        };
        List<string> StockQtyTagName = new List<string> {
                    "intEmptyTrayStockQty",
                    "intMoboStockQty",
                    "intGNATStockQty",
                    "intKoolaidStockQty",
                    "intAffixS1StockQty",
                    "intAffixS2StockQty",
                    "intLBaffleS1StockQty",
                    "intRBaffleS1StockQty",
                    "intLBaffleS2StockQty",
                    "intRBaffleS2StockQty",
                    "intBiosStockQty",
                    "intCobraStockQty",
                    "intLHSCPUStockQty",
                    "intLHSSubStockQty",
                    "intSHSCPUStockQty",                                        
                    "intSHSSubStockQty",
                    "intPwrCblStockQty",
                    "intVelcroStockQty",
                    "intSATAStockQty",

        };
        List<string> StockModelTagName = new List<string> {
                   "strEmptyTrayModel",
                    "strMoboModel",
                    "strGNATModel",
                    "strKoolaidModel",
                    "strAffixS1Model",
                    "strAffixS2Model",
                    "strLBaffleS1Model",
                    "strRBaffleS1Model",
                    "strLBaffleS2Model",
                    "strRBaffleS2Model",
                    "strBiosModel",
                    "strCobraModel",
                    "strLHSModel",
                    "strLHSSubModel",                    
                    "strSHSSModel",
                    "strSHSSubModel",
                    "strPwrCblModel",
                    "strVelcroModel",
                    "strSATAModel",
        };
        List<string> LineTagName = new List<string> {
                    "strLabelZone28",
                    "strLabelZone29",
                    "Z7A_T_CVDATA.Asset_Tag",
                    "Z7B_T_CVDATA.Asset_Tag",
                    "strMoboVerifyAssetTag",
                    "strMoboZone48",
                    "strMoboZone49",
                    "Z9A_T_CVDATA.Asset_Tag",
                    "Z9B_T_CVDATA.Asset_Tag",
                    "strKnGVerifyAssetTag",
                    "strKnGZone58",
                    "strKnGZone59",
                    "Z11A_T_CVDATA.Asset_Tag",
                    "Z11B_T_CVDATA.Asset_Tag",
                    "Z11C_T_CVDATA.Asset_Tag",
                    "strPowerCableZone118",
                    "Z12B_T_CVDATA.Asset_Tag",
                    "strAffixVerifyAssetTag",
                    "strAffixZone68",
                    "strAffixZone69",
                    "Z14A_T_CVDATA.Asset_Tag",
                    "Z14B_T_CVDATA.Asset_Tag",
                    "Z15A_T_CVDATA.Asset_Tag",
                    "strBafflesVerifyAssetTag",
                    "strBafflesZone78",
                    "strBafflesZone79",
                    "Z17A_T_CVDATA.Asset_Tag",
                    "Z17B_T_CVDATA.Asset_Tag",
                    "strBnCVerifyAssetTag",
                    "strBnCZone88",
                    "strBnCZone89",
                    "Z19A_T_CVDATA.Asset_Tag",
                    "Z19B_T_CVDATA.Asset_Tag",
                    "strLHVerifyAssetTag",
                    "strLHZone98",
                    "strLHZone99",
                    "Z21A_T_CVDATA.Asset_Tag",
                    "Z21B_T_CVDATA.Asset_Tag",
                    "strSHVerifyAssetTag",
                    "strSHZone108",
                    "strSHZone109",
                    "Z23A_T_CVDATA.Asset_Tag",
                    "Z23B_T_CVDATA.Asset_Tag",
                    "Z23C_T_CVDATA.Asset_Tag",
                    "strSataZone128",
                    "Z24B_T_CVDATA.Asset_Tag",
                    "strEndOrderAssetTag",
                    "strEndOrderAssetTag",
                    "strEndOrderAssetTag",
                    "strEndOrderAssetTag",
                    "strEndOrderAssetTag",
        };
        List<string> MainHMIIOList = new List<string>
        {
                //Zone 1  
                "PIO201_I2.Data.0",
                "PIO201_I2.Data.1",

                //Z2A
                "PIO202_I1.Data.0",
                "PIO202_I1.Data.1",
                "PIO202_I1.Data.2",

                //Z2B
                "PIO202_I2.Data.0",
                "PIO202_I2.Data.1",
                "PIO202_I2.Data.2",

                //Z3
                "PIO203_I1.Data.0",
                "PIO203_I1.Data.1",
                "PIO203_I1.Data.2",

                //Z5A
                "PIO205_I1.Data.0",
                "PIO205_I1.Data.1",
                "PIO205_I1.Data.2",

                //Z5B
                "PIO205_I1.Data.7",
                "PIO205_I2.Data.0",
                "PIO205_I2.Data.1",

                //Z5C
                "PIO205_I3.Data.0",
                "PIO205_I3.Data.1",
                "PIO205_I3.Data.2",

                //Z6A
                "PIO206_I1.Data.0",
                "PIO206_I1.Data.1",
                "PIO206_I1.Data.2",

                //Z6B
                "PIO206_I3.Data.0",
                "PIO206_I3.Data.1",
                "PIO206_I3.Data.2",

                //Z7A
                "PIO207_I1.Data.0",
                "PIO207_I1.Data.1",
                "PIO207_I1.Data.2",

                //Z7B
                "PIO207_I1.Data.7",
                "PIO207_I2.Data.0",
                "PIO207_I2.Data.1",

                //Z7C
                "PIO207_I3.Data.0",
                "PIO207_I3.Data.1",
                "PIO207_I3.Data.2",

                //Z8A
                "PIO208_I1.Data.0",
                "PIO208_I1.Data.1",
                "PIO208_I1.Data.2",

                //Z8B
                "PIO208_I3.Data.1",
                "PIO208_I3.Data.2",
                "PIO208_I3.Data.3",

                //Z9A
                "PIO209_I1.Data.0",
                "PIO209_I1.Data.1",
                "PIO209_I1.Data.2",

                //Z9B
                "PIO209_I1.Data.7",
                "PIO209_I2.Data.0",
                "PIO209_I2.Data.1",

                //Z9C
                "PIO209_I3.Data.0",
                "PIO209_I3.Data.1",
                "PIO209_I3.Data.2",

                //Z10A
                "PIO210_I1.Data.0",
                "PIO210_I1.Data.1",
                "PIO210_I1.Data.2",

                //Z10B
                "PIO210_I3.Data.0",
                "PIO210_I3.Data.1",
                "PIO210_I3.Data.2",

                //Z11A
                "PIO211_I1.Data.0",
                "PIO211_I1.Data.1",
                "PIO211_I1.Data.2",

                //Z11B
                "PIO211_I1.Data.7",
                "PIO211_I2.Data.0",
                "PIO211_I2.Data.1",

                //Z11C
                "PIO211_I3.Data.0",
                "PIO211_I3.Data.1",
                "PIO211_I3.Data.2",

                //Z12A
                "PIO212_I1.Data.0",
                "PIO212_I1.Data.1",
                "PIO212_I1.Data.2",

                //Z12B
                "PIO212_I2.Data.1",
                "PIO212_I2.Data.2",
                "PIO212_I2.Data.3",

                //Z13A
                "PIO212_I3.Data.0",
                "PIO212_I3.Data.1",
                "PIO212_I3.Data.2",

                "PIO213_I1.Data.0",
                "PIO213_I1.Data.1",
                "PIO213_I1.Data.2",

                "PIO213_I3.Data.0",
                "PIO213_I3.Data.1",
                "PIO213_I3.Data.2",

                "PIO214_I1.Data.0",
                "PIO214_I1.Data.1",
                "PIO214_I1.Data.2",

                "PIO214_I1.Data.7",
                "PIO214_I2.Data.0",
                "PIO214_I2.Data.1",

                "PIO215_I1.Data.0",
                "PIO215_I1.Data.1",
                "PIO215_I1.Data.2",

                "PIO215_I2.Data.0",
                "PIO215_I2.Data.1",
                "PIO215_I2.Data.2",

                "PIO216_I1.Data.0",
                "PIO216_I1.Data.1",
                "PIO216_I1.Data.2",

                "PIO216_I3.Data.0",
                "PIO216_I3.Data.1",
                "PIO216_I3.Data.2",

                "PIO217_I1.Data.0",
                "PIO217_I1.Data.1",
                "PIO217_I1.Data.2",

                "PIO217_I1.Data.7",
                "PIO217_I2.Data.0",
                "PIO217_I2.Data.1",

                "PIO217_I3.Data.0",
                "PIO217_I3.Data.1",
                "PIO217_I3.Data.2",

                "PIO218_I1.Data.0",
                "PIO218_I1.Data.1",
                "PIO218_I1.Data.2",

                "PIO218_I3.Data.0",
                "PIO218_I3.Data.1",
                "PIO218_I3.Data.2",

                "PIO219_I1.Data.0",
                "PIO219_I1.Data.1",
                "PIO219_I1.Data.2",

                "PIO219_I1.Data.7",
                "PIO219_I2.Data.0",
                "PIO219_I2.Data.1",

                "PIO219_I3.Data.0",
                "PIO219_I3.Data.1",
                "PIO219_I3.Data.2",

                "PIO220_I1.Data.0",
                "PIO220_I1.Data.1",
                "PIO220_I1.Data.2",

                "PIO220_I3.Data.0",
                "PIO220_I3.Data.1",
                "PIO220_I3.Data.2",

                "PIO221_I1.Data.0",
                "PIO221_I1.Data.1",
                "PIO221_I1.Data.2",

                "PIO221_I1.Data.7",
                "PIO221_I2.Data.0",
                "PIO221_I2.Data.1",

                "PIO221_I3.Data.0",
                "PIO221_I3.Data.1",
                "PIO221_I3.Data.2",

                "PIO222_I1.Data.0",
                "PIO222_I1.Data.1",
                "PIO222_I1.Data.2",

                "PIO222_I3.Data.0",
                "PIO222_I3.Data.1",
                "PIO222_I3.Data.2",

                "PIO223_I1.Data.0",
                "PIO223_I1.Data.1",
                "PIO223_I1.Data.2",

                "PIO223_I1.Data.7",
                "PIO223_I2.Data.0",
                "PIO223_I2.Data.1",

                "PIO223_I3.Data.0",
                "PIO223_I3.Data.1",
                "PIO223_I3.Data.2",

                "PIO224_I1.Data.0",
                "PIO224_I1.Data.1",
                "PIO224_I1.Data.2",

                "PIO224_I2.Data.1",
                "PIO224_I2.Data.2",
                "PIO224_I2.Data.3",

                "PIO225_I1.Data.0",
                "PIO225_I1.Data.1",
                "PIO225_I1.Data.2",

                "PIO226_I1.Data.0",
                "PIO226_I1.Data.1",
                "PIO226_I1.Data.2",

                //Zone27
                "PIO227_I2.Data.0",
                "PIO227_I2.Data.1",

        };
        List<string> Z29TagNameStr = new List<string>
        {
            "PIO229_I1.Data.0",
            "PIO229_I1.Data.1",
            "PIO229_I1.Data.3",
            "PIO229_I1.Data.4",
            "PIO229_I1.Data.6",
            "PIO229_I1.Data.7",
            "PIO229_I2.Data.1",
            "PIO229_I2.Data.2",
        };

        #endregion

        #region Property
        #endregion

        #region Constructor
        public MainHMI(ref LogicClasses.Main MainConnection, PentagonHMI.ChildControls.ucMainHMI _ucMain)
        {
            try
            {
                ucMain = _ucMain;
                _MainConnection = MainConnection;
                _MainConnection.MainHMI_OnUpdate += new LogicClasses.Main.onMainHMIUpdateHandler(TcpMainHMI_OnUpdate);
                _MainConnection.MainHMIPageOn = true;
                Initialization();
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Initializing Main HMI Failed");
            }
        }
        #endregion

        #region Methods
        public void Dispose()
        {
            //if (_MainConnection != null)
            //{
            //    _MainConnection.MainHMI_OnUpdate -= new LogicClasses.Main.onMainHMIUpdateHandler(TcpMainHMI_OnUpdate);
            //    _MainConnection.MainHMIPageOn = false;
            //}

            //if (DBCall != null)
            //{
            //    DBCall = null;
            //}

        }

        public void Initialization()
        {
            try
            {
                string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
                DBCall = new VanillaDB.DataDBCall(Connstr);

                AssetTagPLC = _MainConnection.MyPLC;
                StatusPLC = _MainConnection.MyPLC;
                StatusPLC = _MainConnection.MyPLC;
                ZoneStatusPLC = _MainConnection.MyPLC;
                MixedPLC = _MainConnection.MyPLC;
                MinHMIIOStatusPLC = _MainConnection.MyPLC2;

                Add_Group = new Thread(AddGroup);
                Add_Group.Start();

                OrderListFirstPull();
                GetEstopList();
                PrevUPHstring = "";
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Initializing Main HMI Failed");
            }

        }

        private void GetEstopList()
        {
            EstopDic = new Dictionary<string, EstopIO>()
            {
                {"0",new EstopIO {First= TagZ0 } },
                {"3",new EstopIO {First= TagZ3_1, Second= TagZ3_2} },
                {"5C",new EstopIO {First = TagZ5C_1, Second = TagZ5C_2 } },
                {"7C",new EstopIO {First = TagZ7C_1, Second = TagZ7C_2 } },
                {"9C",new EstopIO {First = TagZ9C_1, Second = TagZ9C_2 } },
                {"11C",new EstopIO {First = TagZ11C_1, Second = TagZ11C_2 } },
                {"12C",new EstopIO {First = TagZ12C_1, Second = TagZ12C_2 } },
                {"15B",new EstopIO {First = TagZ15B_1, Second = TagZ15B_2 } },
                {"17C",new EstopIO {First = TagZ17C_1, Second = TagZ17C_2 } },
                {"19C",new EstopIO {First = TagZ19C_1, Second = TagZ19C_2 } },
                {"21C",new EstopIO {First = TagZ21C_1, Second = TagZ21C_2 } },
                {"23A",new EstopIO {First = TagZ23A_1, Second = TagZ23A_2 } },
                {"24B",new EstopIO {First = TagZ24B_1, Second = TagZ24B_2 } },
                {"27",new EstopIO {First = TagZ27_1, Second = TagZ27_2 } },
                {"29D",new EstopIO {First = TagZ29D_1, Second = TagZ29D_2 } },
            };
        }

        #endregion
        #region Events
        public string[] GetPartModel()
        {
            int ATn = 0;
            foreach (string TagName in StockModelTagName)
            {
                int loop = 0;
                Tag newTag = new Tag(TagName);
                newTag.DataType = Logix.Tag.ATOMIC.STRING;
                newTag.MyObject = ATn;
                Reread:
                loop++;
                MixedPLC.ReadTag(newTag);
                if (ResultCode.QUAL_GOOD == newTag.QualityCode)
                {
                    StockModelList[ATn] = (string)newTag.Value;
                }
                else
                {
                    if (loop < 5)
                        goto Reread;
                }
                ATn += 1;
            }
            return StockModelList;
        }

        public List<DataTable> OrderInfo(int n)
        {
            try
            {
                string assettag = AssetTagList[n];
                Utilities.FileLogger.logError("MAINHMI get Order Info", assettag);
                DataTable dt = DBCall.Order_Select_ByAssetTag(assettag, ref ErrMsg);
                DataTable dt2 = DBCall.Order_Select_Part_Installed(assettag, ref ErrMsg);
                DataTable dt3 = DBCall.Order_Select_Part_Rejected(assettag, ref ErrMsg);
                DTlist = new List<DataTable>
            {
                dt,dt2,dt3
            };
                return DTlist;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Failed to get Order Info");
                return DTlist;
            }
        }

        private void AddGroup()
        {
            MConline.DataType = Tag.ATOMIC.BOOL;

            ATn = 0;
            foreach (string TagName in LineTagName)
            {
                Tag newTag = new Tag(TagName);
                newTag.DataType = Logix.Tag.ATOMIC.STRING;
                newTag.MyObject = ATn;
                AssetTagPLC.ReadTag(newTag);
                if (ResultCode.QUAL_GOOD == newTag.QualityCode)
                {
                    AssetTagList[ATn] = (string)newTag.Value;
                }
                Tag_LineTag[ATn] = newTag;
                ATn += 1;
                AssetTagCnt = ATn;
            }

            ATn = 0;
            foreach (string TagName in ZoneStatusTagName)
            {
                Tag newTag = new Tag(TagName);
                newTag.DataType = Logix.Tag.ATOMIC.BOOL;
                newTag.MyObject = ATn;
                ZoneStatusPLC.ReadTag(newTag);
                if (ResultCode.QUAL_GOOD == newTag.QualityCode)
                {
                    ZoneStatusList[ATn] = (bool)newTag.Value;
                }
                ZoneTagList[ATn] = newTag;
                ATn += 1;
                TagCnt = ATn;
            }
            ATn = 0;

            foreach (string TagName in StockQtyTagName)
            {
                Tag newTag = new Tag(TagName);
                newTag.DataType = Logix.Tag.ATOMIC.INT;
                MixedPLC.ReadTag(newTag);
                newTag.MyObject = ATn;
                if (ResultCode.QUAL_GOOD == newTag.QualityCode)
                {
                    StockQtyList[ATn] = (int)newTag.Value;
                }
                StockTagList[ATn] = newTag;
                ATn += 1;
                StockCnt++;
            }
            ATn = 0;
            foreach (string TagName in StationStatus)
            {
                Tag newTag = new Tag(TagName);
                newTag.DataType = Logix.Tag.ATOMIC.STRING;
                newTag.MyObject = ATn;
                StatusPLC.ReadTag(newTag);
                if (ResultCode.QUAL_GOOD == newTag.QualityCode)
                {
                    StatusList[ATn] = (string)newTag.Value;
                }
                McStatus.Add(newTag);
                ATn += 1;
            }
            ATn = 0;
            foreach (string TagName in DisableStationStatus)
            {
                Tag newTag = new Tag(TagName);
                newTag.DataType = Logix.Tag.ATOMIC.STRING;
                newTag.MyObject = ATn;
                StatusPLC.ReadTag(newTag);
                if (ResultCode.QUAL_GOOD == newTag.QualityCode)
                    DisStatusStation[ATn] = Convert.ToBoolean(newTag.Value); //(string)newTag.Value;
                else
                    DisStatusStation[ATn] = false;
                ATn += 1;

                DisStn.Add(newTag);
            }
            ATn = 0;
            foreach (string TagName in DisableShuttleStatus)
            {
                Tag newTag = new Tag(TagName);
                newTag.DataType = Logix.Tag.ATOMIC.STRING;
                newTag.MyObject = ATn;
                StatusPLC.ReadTag(newTag);
                if (ResultCode.QUAL_GOOD == newTag.QualityCode)
                    DisStatusShuttle[ATn] = Convert.ToBoolean(newTag.Value); //(string)newTag.Value;
                else
                    DisStatusShuttle[ATn] = false;
                ATn += 1;

                DisSte.Add(newTag);
            }

            DisStatus.Add(DisStn);
            DisStatus.Add(DisSte);

            ATn = 0;
            foreach (string TagName in MainHMIIOList)
            {
                Tag newTag = new Tag(TagName);
                newTag.DataType = Logix.Tag.ATOMIC.BOOL;
                newTag.MyObject = ATn;
                MinHMIIOStatusPLC.ReadTag(newTag);
                if (ResultCode.QUAL_GOOD == newTag.QualityCode)
                {
                    BoolMainHMIIOList[ATn] = (bool)newTag.Value;
                }
                TagList_MainHMIIOList[ATn] = newTag;
                ATn += 1;
                IOCnt = ATn;
            }
            ATn = 0;
            foreach (string TagName in Z29TagNameStr)
            {
                Tag newTag = new Tag(TagName);
                newTag.DataType = Logix.Tag.ATOMIC.BOOL;
                ZoneStatusPLC.ReadTag(newTag);
                newTag.MyObject = ATn;

                if (ResultCode.QUAL_GOOD == newTag.QualityCode)
                {
                    _BoolZ29[ATn] = (bool)newTag.Value;
                }
                Z29TagList[ATn] = newTag;
                ATn += 1;
                Z29n = ATn;
            }
            Tag newTag2 = new Tag(strUPH);
            newTag2.DataType = Logix.Tag.ATOMIC.STRING;
            newTag2.MyObject = 0;
            MixedPLC.ReadTag(newTag2);
            if (ResultCode.QUAL_GOOD == newTag2.QualityCode)
            {
                UPHstring = (string)newTag2.Value;
                ucMain.UPHupdate(UPHstring);
            }

            PointIORead = new Thread(PointIO_Read);
            SensorRead = new Thread(Sensor_Read);
            //MixedRead = new Thread(Mixed_Read);
            AssetTagRead = new Thread(AssetTag_Read);

            PointIORead.Start();
            SensorRead.Start();
            //MixedRead.Start();
            AssetTagRead.Start();

            Add_Group.Abort();

        }

        private void OrderListUpdate(int n, string NewAssetTag)
        {
            if (NewAssetTag == null)
                NewAssetTag = "NoValue";
            if (NewAssetTag != "")
            {
                dt = DBCall.Order_Select_Info(NewAssetTag, ref ErrMsg);
                if (dt != null && dt.Rows.Count > 0)
                    _OrderID[n] = dt.Rows[0]["OrderID"].ToString() + '|' + dt.Rows[0]["Status"].ToString();
                else
                    _OrderID[n] = NewAssetTag + "|" + "Not Exists";
            }
            else
            { _OrderID[n] = "|"; }
        }
        private void OrderListFirstPull()
        {
            ORn = 0;
            foreach (string assettag in AssetTagList)
            {
                string AssetT = assettag;
                if (AssetT == null)
                    AssetT = "NoValue";
                if (AssetT != "")
                {
                    dt = DBCall.Order_Select_Info(AssetT, ref ErrMsg);
                    if (dt != null && dt.Rows.Count > 0)
                        _OrderID[ORn] = dt.Rows[0]["OrderID"].ToString() + '|' + dt.Rows[0]["Status"].ToString();
                    else
                        _OrderID[ORn] = "|";
                }
                else { _OrderID[ORn] = "|"; }
                ORn++;

            }
        }

        public bool CheckEstop(string Zone)
        {
            try
            {
                if (!EstopDic.ContainsKey(Zone))
                    return false;

                Tag Tag1 = EstopDic[Zone].First;                
                if(Tag1 != null)
                {
                    MixedPLC.ReadTag(Tag1);
                    if(Tag1.QualityCode == ResultCode.QUAL_GOOD)
                    {
                        if (Tag1.Value.ToString().ToUpper() == "FALSE")
                            return true;
                    }
                }
                Tag Tag2 = EstopDic[Zone].Second;
                if (Tag2 != null)
                {
                    MixedPLC.ReadTag(Tag2);
                    if (Tag2.QualityCode == ResultCode.QUAL_GOOD)
                    {
                        if (Tag2.Value.ToString().ToUpper() == "FALSE")
                            return true;
                    }
                }

                return false;
            }
            catch(Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Check E Stop Status Failed");
                return false;
            }
        } 

        public void CheckDisable()
        {
            int n = 0;
            foreach (List<Tag> Taglist in DisStatus)
            {
                int c = 0;
                foreach (Tag Tag in Taglist)
                {
                    MixedPLC.ReadTag(Tag);
                    if (ResultCode.QUAL_GOOD == Tag.QualityCode)
                    {
                        if(n == 0)
                            DisStatusStation[c] = (bool)Tag.Value;                        
                        else if(n == 1)
                            DisStatusShuttle[c] = (bool)Tag.Value;                         
                    }
                    c++;
                }
                n++;
            }
        }


        void TcpMainHMI_OnUpdate()
        {

            try
            {
                Mixed_Read();

                if (OnUpdate != null)
                    OnUpdate();
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Select Part Failed");
            }

        }
        #endregion
        #region Destructor
        ~MainHMI()
        {
            Dispose();
        }
        #endregion
        private void Sensor_Read()
        {
            while (MainLoop)
            {
                try
                {
                    Thread.Sleep(100);

                    if (MainHMIOn)
                    {
                        int int_LoopIOn = 0;

                        while (int_LoopIOn < IOCnt)
                        {
                            MinHMIIOStatusPLC.ReadTag(TagList_MainHMIIOList[int_LoopIOn]);
                            if (ResultCode.QUAL_GOOD == TagList_MainHMIIOList[int_LoopIOn].QualityCode)
                            {
                                BoolMainHMIIOList[int_LoopIOn] = (bool)TagList_MainHMIIOList[int_LoopIOn].Value;
                            }
                            int_LoopIOn++;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Utilities.FileLogger.logError(ex.Message, "Sensor_Read Failed");
                }
            }
        }

        private void AssetTag_Read()
        {
            while (MainLoop)
            {
                try
                {
                    Thread.Sleep(100);

                    if (MainHMIOn)
                    {
                        int int_LoopIOn = 0;

                        while (int_LoopIOn < AssetTagCnt)
                        {
                            AssetTagPLC.ReadTag(Tag_LineTag[int_LoopIOn]);
                            if (ResultCode.QUAL_GOOD == Tag_LineTag[int_LoopIOn].QualityCode)
                            {
                                AssetTagList[int_LoopIOn] = (string)Tag_LineTag[int_LoopIOn].Value;
                                OrderListUpdate(int_LoopIOn, (string)Tag_LineTag[int_LoopIOn].Value);
                            }
                            int_LoopIOn++;
                        }
                    }

                }
                catch (Exception ex)
                {
                    Utilities.FileLogger.logError(ex.Message, "AssetTag_Read Failed");
                }
            }
        }


        private void PointIO_Read()
        {
            while (MainLoop)
            {
                try
                {
                    Thread.Sleep(100);

                    if (MainHMIOn)
                    {
                        int int_LoopIOn = 0;
                        while (int_LoopIOn < TagCnt)
                        {
                            ZoneStatusPLC.ReadTag(ZoneTagList[int_LoopIOn]);
                            if (ResultCode.QUAL_GOOD == ZoneTagList[int_LoopIOn].QualityCode)
                            {
                                ZoneStatusList[int_LoopIOn] = (bool)ZoneTagList[int_LoopIOn].Value;
                            }
                            else
                            {
                                ZoneStatusList[int_LoopIOn] = true;
                            }
                            int_LoopIOn++;
                        }

                        int_LoopIOn = 0;
                        while (int_LoopIOn < Z29n)
                        {
                            ZoneStatusPLC.ReadTag(Z29TagList[int_LoopIOn]);
                            if (ResultCode.QUAL_GOOD == Z29TagList[int_LoopIOn].QualityCode)
                            {
                                _BoolZ29[int_LoopIOn] = (bool)Z29TagList[int_LoopIOn].Value;
                            }
                            int_LoopIOn++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utilities.FileLogger.logError(ex.Message, "PointIO_Read Failed");
                }
            }
        }


        private void Mixed_Read()
        {
            //while (MainLoop)
            //{
            try
            {
                //Thread.Sleep(200);

                if (MainHMIOn)
                {
                    MixedPLC.ReadTag(MConline);

                    MixedPLC.ReadTag(Tag_strUPH);
                    if (ResultCode.QUAL_GOOD == Tag_strUPH.QualityCode)
                    {
                        UPHstring = (string)Tag_strUPH.Value;
                    }

                    if (PrevUPHstring != UPHstring && !string.IsNullOrWhiteSpace(UPHstring))
                    {
                        PrevUPHstring = UPHstring;
                        ucMain.UPHupdate(UPHstring);
                    }
                }

                int n = 0;
                foreach (Tag tag in McStatus)
                {
                    MixedPLC.ReadTag(tag);
                    if (ResultCode.QUAL_GOOD == tag.QualityCode)
                    {
                        StatusList[n] = Convert.ToString(tag.Value);
                        n++;
                    }
                }

                int int_LoopIOn = 0;
                while (int_LoopIOn < StockCnt)
                {
                    MixedPLC.ReadTag(StockTagList[int_LoopIOn]);
                    if (ResultCode.QUAL_GOOD == StockTagList[int_LoopIOn].QualityCode)
                    {
                        StockQtyList[int_LoopIOn] = (Int32)StockTagList[int_LoopIOn].Value;
                    }
                    int_LoopIOn++;
                }

                //18/11/1
                CheckDisable();
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Mixed_Read Failed");
            }
        }
        //}
    }
}
