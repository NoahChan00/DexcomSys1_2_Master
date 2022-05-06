using System;
using System.Collections.Generic;
using System.Text;
using Logix;

namespace Utilities
{
    class AllenB
    {
        public AllenB()
        {
            if (MyPLC == null)
                Connect();
        }
        Controller MyPLC = new Controller();
        public void Connect()
        {
            Controller MyPLCm = new Controller();
            MyPLCm.IPAddress = "191.168.3.21";
            MyPLCm.Path = "0";
            MyPLCm.Timeout = 20;
            MyPLCm.CPUType = Controller.CPU.LOGIX;
            MyPLCm.Simulate = false;
            MyPLCm.Connect();
            try
            {
                if (MyPLCm.Connect() != ResultCode.E_SUCCESS)
                    Utilities.FileLogger.logError("Fail to Connect Allen-Bradley", "Connection Error");
                else
                    MyPLC = MyPLCm;
            }
            catch (Exception ex)
            {
                FileLogger.logError("Fail to Connect Allen-Bradley (" + ex.Message + " )", "Connection Error");
            }
        }

        public Boolean[] _IOlist = new Boolean[1000];
        public Boolean[] IOlist { get { return _IOlist; } set { _IOlist = value; } }

        Controller MainIOPLC = new Controller();
        TagGroup MainIOPLCGroup = new TagGroup();

        Controller IOPLC1 = new Controller();
        TagGroup IOPLCGroup1 = new TagGroup();

        Controller IOPLC2 = new Controller();
        TagGroup IOPLCGroup2 = new TagGroup();

        Controller IOPLC3 = new Controller();
        TagGroup IOPLCGroup3 = new TagGroup();

        Controller IOPLC4 = new Controller();
        TagGroup IOPLCGroup4 = new TagGroup();

        Controller IOPLC5 = new Controller();
        TagGroup IOPLCGroup5 = new TagGroup();

        Controller IOPLC6 = new Controller();
        TagGroup IOPLCGroup6 = new TagGroup();

        Controller IOPLC7 = new Controller();
        TagGroup IOPLCGroup7 = new TagGroup();

        Controller IOPLC8 = new Controller();
        TagGroup IOPLCGroup8 = new TagGroup();

        Controller IOPLC9 = new Controller();
        TagGroup IOPLCGroup9 = new TagGroup();

        Controller IOPLC10 = new Controller();
        TagGroup IOPLCGroup10 = new TagGroup();

        int IOn = 0;

        int ListLength = 50;

        List<string> IOTagName;


        private void Connect2()
        {
            List<string> StationIO = SetTagName("1");
            IOn = 0;

            MainIOPLC = MyPLC;
            MainIOPLCGroup.Controller = MainIOPLC;
            MainIOPLCGroup.ScanStart();


            if (StationIO.Count > ListLength)
            {
                IOPLC1 = MyPLC;
                IOPLCGroup1.Controller = IOPLC1;
                IOPLCGroup1.ScanStart();
            }

            if (StationIO.Count > (ListLength * 2))
            {
                IOPLC2 = MyPLC;
                IOPLCGroup2.Controller = IOPLC2;
                IOPLCGroup2.ScanStart();
            }

            if (StationIO.Count > (ListLength * 3))
            {
                IOPLC3 = MyPLC;
                IOPLCGroup3.Controller = IOPLC3;
                IOPLCGroup3.ScanStart();

                IOPLC4 = MyPLC;
                IOPLCGroup4.Controller = IOPLC4;
                IOPLCGroup4.ScanStart();

                IOPLC5 = MyPLC;
                IOPLCGroup5.Controller = IOPLC5;
                IOPLCGroup5.ScanStart();

                IOPLC6 = MyPLC;
                IOPLCGroup6.Controller = IOPLC6;
                IOPLCGroup6.ScanStart();

                IOPLC7 = MyPLC;
                IOPLCGroup7.Controller = IOPLC7;
                IOPLCGroup7.ScanStart();

                IOPLC8 = MyPLC;
                IOPLCGroup8.Controller = IOPLC8;
                IOPLCGroup8.ScanStart();

                IOPLC9 = MyPLC;
                IOPLCGroup9.Controller = IOPLC9;
                IOPLCGroup9.ScanStart();

                IOPLC10 = MyPLC;
                IOPLCGroup10.Controller = IOPLC10;
                IOPLCGroup10.ScanStart();
            }

            TagAssignment(StationIO);
        }

        private void TagAssignment(List<string> TagNameList)
        {
            foreach (string TagName in TagNameList)
            {
                if (IOn <= ListLength)
                {
                    AddGroup(TagName, MainIOPLC, MainIOPLCGroup);
                }
                else if (IOn <= ListLength * 2)
                {
                    AddGroup(TagName, IOPLC1, IOPLCGroup1);
                }
                else if (IOn <= ListLength * 3)
                {
                    AddGroup(TagName, IOPLC2, IOPLCGroup2);
                }
                else if (IOn <= ListLength * 4)
                {
                    AddGroup(TagName, IOPLC3, IOPLCGroup3);
                }

            }
        }

        private void AddGroup(string TagName, Controller MIOPLC, TagGroup MIOPLCGroup)
        {
            Tag newTag = new Tag(TagName);
            newTag.DataType = Logix.Tag.ATOMIC.BOOL;
            MIOPLC.ReadTag(newTag);
            if (ResultCode.QUAL_GOOD == newTag.QualityCode)
            {
                _IOlist[IOn] = (Boolean)newTag.Value;
                newTag.Changed += new System.EventHandler(newTag_Changed);
                newTag.MyObject = IOn;
                IOn += 1;
                MIOPLCGroup.AddTag(newTag);
            }
        }

        private void newTag_Changed(object sender, EventArgs e)
        {
            DataChangeEventArgs args = (DataChangeEventArgs)e;

            int ItemChanged = (int)args.MyObject;

            if (ResultCode.QUAL_GOOD == args.QualityCode)
            {
                _IOlist[ItemChanged] = (Boolean)args.Value;
            }
        }

        private List<string> SetTagName(string stationID)
        {
            switch (stationID)
            {
                #region TrayASRS
                case "1":
                    IOTagName = new List<string>(new string[]
                  {
                    "ix00StartButton",
                    "ix01StopButton",
                    "ix02ResetButton",
                    "ix03SafetyRelaySignal",
                    "ix04Spare",
                    "ix05Spare",
                    "ix06Spare",
                    "ix07Spare",
                    "ix10R1DoorSwitch",
                    "ix11R1CurtainSens",
                    "ix12R1ProxiSens",
                    "ix13R1ClmpCylExtSignal",
                    "ix14R1ClmpCylRetSignal",
                    "ix15R2DoorSwitch",
                    "ix16R2CurtainSens",
                    "ix17R2ProxiSens",
                    "ix20R2ClmpCylExtSignal",
                    "ix21R2ClmpCylRetSignal",
                    "ix22R3DoorSwitch",
                    "ix23R3CurtainSens",
                    "ix24R3ProxiSens",
                    "ix25R3ClmpCylExtSignal",
                    "ix26R3ClmpCylRetSignal",
                    "ix27R4DoorSwitch",
                    "ix30R4CurtainSens",
                    "ix31R4ProxiSens",
                    "ix32R4ClmpCylExtSignal",
                    "ix33R4ClmpCylRetSignal",
                    "ix34R5DoorSwitch",
                    "ix35R5CurtainSens",
                    "ix36R5ProxiSens",
                    "ix37R5ClmpCylExtSignal",
                    "ix40R5ClmpCylRetSignal",
                    "ix41R6DoorSwitch",
                    "ix42R6CurtainSens",
                    "ix43R6ProxiSens",
                    "ix44R6ClmpCylExtSignal",
                    "ix45R6ClmpCylRetSignal",
                    "ix46Spare",
                    "ix47Spare",
                    "ix50XHomeSens",
                    "ix51XPosLimitSens",
                    "ix52XNegLimitSens",
                    "ix53XMotorAlarm",
                    "ix54XMotorInPos",
                    "ix55YHomeSens",
                    "ix56YPosLimitSens",
                    "ix57YNegLimitSens",
                    "ix60YMotorAlarm",
                    "ix61YMotorInPos",
                    "ix62ZHomeSens",
                    "ix63ZPosLimitSens",
                    "ix64ZNegLimitSens",
                    "ix65ZMotorAlarm",
                    "ix66ZMotorInPos",
                    "ix67Spare",
                    "ix70ASRSTrayPresentSens",
                    "ix71ASRSRackPositioningSens",
                    "ix72YFrontPrecisCylExtSignal",
                    "ix73YFrontPrecisCylRetSignal",
                    "ix74YSidePrecisCylExtSignal",
                    "ix75YSidePrecisCylRetSignal",
                    "ix76Spare",
                    "ix77Spare",
                    "ix80PnPPickInProg",
                    "ix81PnPPickDone",
                    "ix82Spare",
                    "ix83PnPPicked",
                    "ix84Slot01PresentSens",
                    "ix85Slot02PresentSens",
                    "ix86Slot03PresentSens",
                    "ix87Slot04PresentSens",
                    "ix90Slot05PresentSens",
                    "ix91Slot06PresentSens",
                    "ix92Slot07PresentSens",
                    "ix93Slot08PresentSens",
                    "ix94Slot09PresentSens",
                    "ix95Slot10PresentSens",
                    "ix96Slot11PresentSens",
                    "ix97Slot12PresentSens",
                    "ix100Slot13PresentSens",
                    "ix101Slot14PresentSens",
                    "ix102Slot15PresentSens",
                    "ix103Slot16PresentSens",
                    "ix104Slot17PresentSens",
                    "ix105Slot18PresentSens",
                    "ix106Slot19PresentSens",
                    "ix107Slot20PresentSens",
                    "ix110Slot21PresentSens",
                    "ix111Slot22PresentSens",
                    "ix112Slot23PresentSens",
                    "ix113Slot24PresentSens",
                    "ix114Slot25PresentSens",
                    "ix115Slot26PresentSens",
                    "ix116Slot27PresentSens",
                    "ix117Slot28PresentSens",
                    "ix120Slot29PresentSens",
                    "ix121Slot30PresentSens",
                    "ix122Slot31PresentSens",
                    "ix123Slot32PresentSens",
                    "ix124Slot33PresentSens",
                    "ix125Slot34PresentSens",
                    "ix126Slot35PresentSens",
                    "ix127Slot36PresentSens",
                    "ix130Slot37PresentSens",
                    "ix131Slot38PresentSens",
                    "ix132Slot39PresentSens",
                    "ix133Slot40PresentSens",
                    "ix134Slot41PresentSens",
                    "ix135Slot42PresentSens",
                    "ix136Slot43PresentSens",
                    "ix137Slot44PresentSens",
                    "ix140Slot45PresentSens",
                    "ix141Slot46PresentSens",
                    "ix142Slot47PresentSens",
                    "ix143Slot48PresentSens",
                    "ix144Slot49PresentSens",
                    "ix145Slot50PresentSens",
                    "ix146Slot51PresentSens",
                    "ix147Slot52PresentSens",
                    "ix150Slot53PresentSens",
                    "ix151Slot54PresentSens",
                    "ix152Slot55PresentSens",
                    "ix153Slot56PresentSens",
                    "ix154Slot57PresentSens",
                    "ix155Slot58PresentSens",
                    "ix156Slot59PresentSens",
                    "ix157Slot60PresentSens",
                    "ix160EStop1",
                    "ix161Spare",
                    "ix162Spare",
                    "ix163Spare",
                    "ix164Spare",
                    "ix165Spare",
                    "ix166Spare",
                    "ix167Spare"
                      });
                    break;
                #endregion
                #region AssetTag
                case "2":
                    IOTagName = new List<string>(new string[]
                  {
                     "ix00StartButton",
                     "ix01StopButton",
                     "ix02ResetButton",
                     "ix03SafetyRelay",
                     "ix04IncomingAir",
                     "ix05MainDoor",
                     "ix06InCurtainSens",
                     "ix07OutCurtainSens",
                     "ix10ToggleClampProxSens",
                     "ix11Spare",
                     "ix12Z1PalletPresenceSens",
                     "ix13Z2PalletPresenceSens",
                     "ix14EStop1",
                     "ix15EStop2",
                     "ix16EStop3",
                     "ix17EStop4",
                     "ix20RobotReady",
                     "ix21RobotRunning",
                     "ix22RobotError",
                     "ix23RobotPaused",
                     "ix24RobotSpare",
                     "ix25RobotHomeDone",
                     "ix26Spare",
                     "ix27Spare",
                     "ix30Z6TrayZAReadyToTamp",
                     "ix31Z6TrayZBReadyToTamp",
                     "ix32Z6BypassStation",
                     "ix33Z6ConveyorReady",
                     "ix34Z6Spare",
                     "ix35Z6Spare",
                     "ix36Z6Spare",
                     "ix37Z6Spare",
                     "ix40PrintEnd",
                     "ix41PrintError",
                     "ix42PrintDataReady",
                     "ix43PrintLabelOut",
                     "ix44PrintRibbonOut",
                     "ix45PrintLowRibbon",
                     "ix46PrintLowLabel",
                     "ix47PrinterReady"
                     });
                    break;
                #endregion
                #region MoboASRS
                case "3":
                    IOTagName = new List<string>(new string[]
                  {
                    "ix00StartButton",
                    "ix01StopButton",
                    "ix02ResetButton",
                    "ix03SafetyRelaySignal",
                    "ix04IncomingPressureSwitch",
                    "ix05MainDoorSwitch",
                    "ix06Spare",
                    "ix07Spare",
                    "ix10R1DoorSwitch",
                    "ix11R1CurtainSens",
                    "ix12R1ProxiSens",
                    "ix13R1ClmpCylExtSignal",
                    "ix14R1ClmpCylRetSignal",
                    "ix15R2DoorSwitch",
                    "ix16R2CurtainSens",
                    "ix17R2ProxiSens",
                    "ix20R2ClmpCylExtSignal",
                    "ix21R2ClmpCylRetSignal",
                    "ix22R3DoorSwitch",
                    "ix23R3CurtainSens",
                    "ix24R3ProxiSens",
                    "ix25R3ClmpCylExtSignal",
                    "ix26R3ClmpCylRetSignal",
                    "ix27R4DoorSwitch",
                    "ix30R4CurtainSens",
                    "ix31R4ProxiSens",
                    "ix32R4ClmpCylExtSignal",
                    "ix33R4ClmpCylRetSignal",
                    "ix34R5DoorSwitch",
                    "ix35R5CurtainSens",
                    "ix36R5ProxiSens",
                    "ix37R5ClmpCylExtSignal",
                    "ix40R5ClmpCylRetSignal",
                    "ix41R6DoorSwitch",
                    "ix42R6CurtainSens",
                    "ix43R6ProxiSens",
                    "ix44R6ClmpCylExtSignal",
                    "ix45R6ClmpCylRetSignal",
                    "ix46Spare",
                    "ix47Spare",
                    "ix50XHomeSens",
                    "ix51XPosLimitSens",
                    "ix52XNegLimitSens",
                    "ix53XMotorAlarm",
                    "ix54XMotorInPos",
                    "ix55YHomeSens",
                    "ix56YPosLimitSens",
                    "ix57YNegLimitSens",
                    "ix60YMotorAlarm",
                    "ix61YMotorInPos",
                    "ix62ZHomeSens",
                    "ix63ZPosLimitSens",
                    "ix64ZNegLimitSens",
                    "ix65ZMotorAlarm",
                    "ix66ZMotorInPos",
                    "ix67Spare",
                    "ix70ASRSTrayPresentSens",
                    "ix71ASRSRackPositioningSens",
                    "ix72YFrontPrecisCylExtSignal",
                    "ix73YFrontPrecisCylRetSignal",
                    "ix74YSidePrecisCylExtSignal",
                    "ix75YSidePrecisCylRetSignal",
                    "ix76Spare",
                    "ix77Spare",
                    "ix80RobotPickInProg",
                    "ix81RobotPickDone",
                    "ix82RobotAwayFromASRS",
                    "ix83RobotPicked",
                    "ix84Slot01PresentSens",
                    "ix85Slot02PresentSens",
                    "ix86Slot03PresentSens",
                    "ix87Slot04PresentSens",
                    "ix90Slot05PresentSens",
                    "ix91Slot06PresentSens",
                    "ix92Slot07PresentSens",
                    "ix93Slot08PresentSens",
                    "ix94Slot09PresentSens",
                    "ix95Slot10PresentSens",
                    "ix96Slot11PresentSens",
                    "ix97Slot12PresentSens",
                    "ix100Slot13PresentSens",
                    "ix101Slot14PresentSens",
                    "ix102Slot15PresentSens",
                    "ix103Slot16PresentSens",
                    "ix104Slot17PresentSens",
                    "ix105Slot18PresentSens",
                    "ix106Slot19PresentSens",
                    "ix107Slot20PresentSens",
                    "ix110Slot21PresentSens",
                    "ix111Slot22PresentSens",
                    "ix112Slot23PresentSens",
                    "ix113Slot24PresentSens",
                    "ix114Slot25PresentSens",
                    "ix115Slot26PresentSens",
                    "ix116Slot27PresentSens",
                    "ix117Slot28PresentSens",
                    "ix120Slot29PresentSens",
                    "ix121Slot30PresentSens",
                    "ix122Slot31PresentSens",
                    "ix123Slot32PresentSens",
                    "ix124Slot33PresentSens",
                    "ix125Slot34PresentSens",
                    "ix126Slot35PresentSens",
                    "ix127Slot36PresentSens",
                    "ix130Slot37PresentSens",
                    "ix131Slot38PresentSens",
                    "ix132Slot39PresentSens",
                    "ix133Slot40PresentSens",
                    "ix134Slot41PresentSens",
                    "ix135Slot42PresentSens",
                    "ix136Slot43PresentSens",
                    "ix137Slot44PresentSens",
                    "ix140Slot45PresentSens",
                    "ix141Slot46PresentSens",
                    "ix142Slot47PresentSens",
                    "ix143Slot48PresentSens",
                    "ix144Slot49PresentSens",
                    "ix145Slot50PresentSens",
                    "ix146Slot51PresentSens",
                    "ix147Slot52PresentSens",
                    "ix150Slot53PresentSens",
                    "ix151Slot54PresentSens",
                    "ix152Slot55PresentSens",
                    "ix153Slot56PresentSens",
                    "ix154Slot57PresentSens",
                    "ix155Slot58PresentSens",
                    "ix156Slot59PresentSens",
                    "ix157Slot60PresentSens",
                    "ix160EStop1",
                    "ix161EStop2",
                    "ix162EStop3",
                    "ix163EStop4",
                    "ix164Spare",
                    "ix165Spare",
                    "ix166MOBORobotSafe",
                    "ix167Spare"
                         });
                    break;
                #endregion
                #region MoboRobot
                case "4":
                    IOTagName = new List<string>(new string[]
                   {
                    "ix00StartButton",
                    "ix01StopButton",
                    "ix02ResetButton",
                    "ix03SafetyRelay",
                    "ix04IncomingAir",
                    "ix05MainDoor",
                    "ix06InCurtainSens",
                    "ix07OutCurtainSens",
                    "ix10MoboAsrsReady",
                    "ix11MoboAsrsReadyForRobotPick",
                    "ix12MoboAsrsMovedAway",
                    "ix13ReleasedClamp",
                    "ix14Spare",
                    "ix15Spare",
                    "ix16Robot1Safe",
                    "ix17Robot1Paused",
                    "ix20Robot1Ready",
                    "ix21Robot1Running",
                    "ix22Robot1Error",
                    "ix23Robot1HomeDone",
                    "ix24Robot2Ready",
                    "ix25Robot2Running",
                    "ix26Robot2Error",
                    "ix27Robot2HomeDone",
                    "ix30RobotSpare",
                    "ix31Spare",
                    "ix32Spare",
                    "ix33Spare",
                    "ix34Robot2Paused",
                    "ix35Spare",
                    "ix36Spare",
                    "ix37RobotSpare",
                    "ix40Z8TrayZAReadyToPlace",
                    "ix41Z8TrayZBReadyToPlace",
                    "ix42Z8BypassMoboAsrs",
                    "ix43Z8ConveyorReady",
                    "ix44Z8SpareHandshaking",
                    "ix45Z8SpareHandshaking",
                    "ix46Z8SpareHandshaking",
                    "ix47Z8Spare",
                    "ix50EStop1",
                    "ix51EStop2",
                    "ix52EStop3",
                    "ix53EStop4",
                    "ix54Z1PalletPresenceSens",
                    "ix55Z2PalletPresenceSens",
                    "ix56Spare",
                    "ix57MoboAsrsEStop",
                    "ix60MoboAsrsSpare",
                    "ix61MoboAsrsSpare",
                    "ix62Spare",
                    "ix63Spare",
                    "ix64Spare",
                    "ix65Spare",
                    "ix66Spare",
                    "ix67Spare",
                     });
                    break;
                #endregion
                #region Koolaid&Tang
                case "5":
                    IOTagName = new List<string>(new string[]
                  {
                    "ix00StartButton",
                    "ix01StopButton",
                    "ix02ResetButton",
                    "ix03SafetyRelay",
                    "ix04IncomingAir",
                    "ix05MainDoor",
                    "ix06InCurtainSens",
                    "ix07OutCurtainSens",
                    "ix10Rack1DoorSwitch",
                    "ix11Rack1CurtainSens",
                    "ix12Rack1ProxiSens",
                    "ix13Spare",
                    "ix14Elvtr1PartRejectSens",
                    "ix15Elvtr1TrayPresentSens",
                    "ix16Elvtr1RackPosSens",
                    "ix17Elvtr1YFrontPrecisCylExtSignal",
                    "ix20Elvtr1YFrontPrecisCylRetSignal",
                    "ix21Elvtr1YSidePrecisCylExtSignal",
                    "ix22Elvtr1YSidePrecisCylRetSignal",
                    "ix23Elvtr1YHomeSens",
                    "ix24Elvtr1YPosLimitSens",
                    "ix25Elvtr1YNegLimitSens",
                    "ix26Elvtr1YMotorAlarm",
                    "ix27Elvtr1YInPos",
                    "ix30Elvtr1ZHomeSens",
                    "ix31Elvtr1ZPosLimitSens",
                    "ix32Elvtr1ZNegLimitSens",
                    "ix33Elvtr1ZMotorAlarm",
                    "ix34Elvtr1ZInPos",
                    "ix35Elvtr1TrayInPosSens",
                    "ix36EStop1",
                    "ix37EStop2",
                    "ix40Rack2DoorSwitch",
                    "ix41Rack2CurtainSens",
                    "ix42Rack2ProxSens",
                    "ix43Spare",
                    "ix44Elvtr2PartRejectSens",
                    "ix45Elvtr2TrayPresentSens",
                    "ix46Elvtr2RackPosSens",
                    "ix47Elvtr2YFrontPrecisCylExtSignal",
                    "ix50Elvtr2YFrontPrecisCylRetSignal",
                    "ix51Elvtr2YSidePrecisCylExtSignal",
                    "ix52Elvtr2YSidePrecisCylRetSignal",
                    "ix53Elvtr2YHomeSens",
                    "ix54Elvtr2YPosLimitSens",
                    "ix55Elvtr2YNegLimitSens",
                    "ix56Elvtr2YMotorAlarm",
                    "ix57Elvtr2YInPos",
                    "ix60Elvtr2ZHomeSens",
                    "ix61Elvtr2ZPosLimitSens",
                    "ix62Elvtr2ZNegLimitSens",
                    "ix63Elvtr2ZMotorAlarm",
                    "ix64Elvtr2ZInPos",
                    "ix65Elvtr2TrayInPosSens",
                    "ix66EStop3",
                    "ix67EStop4",
                    "ix70RobotReady",
                    "ix71RobotRunning",
                    "ix72RobotError",
                    "ix73RobotPaused",
                    "ix74Spare",
                    "ix75Spare",
                    "ix76Spare",
                    "ix77Spare",
                    "ix80RobotPlaceZ1Done",
                    "ix81RobotPick1InProg",
                    "ix82RobotPick2InProg",
                    "ix83RobotHomeDone",
                    "ix84Spare",
                    "ix85Spare",
                    "ix86Z1PalletPresentSens",
                    "ix87Z2PalletPresentSens",
                    "ix90Z10Z1ReqPlaceKoolaid",
                    "ix91Z10Z1ReqPlaceTang",
                    "ix92RobotSpare",
                    "ix93RobotSpare",
                    "ix94Z10Z2ReqPlaceKoolaid",
                    "ix95Z10Z2ReqPlaceTang",
                    "ix96RobotSpare",
                    "ix97RobotSpare",
                    "ix100Rack1Tray1PresntSens",
                    "ix101Rack1Tray2PresntSens",
                    "ix102Rack1Tray3PresntSens",
                    "ix103Rack1Tray4PresntSens",
                    "ix104Rack2Tray1PresntSens",
                    "ix105Rack2Tray2PresntSens",
                    "ix106Rack2Tray3PresntSens",
                    "ix107Rack2Tray4PresntSens"
                    });
                    break;
                #endregion
                #region AffixFan
                case "6":
                    IOTagName = new List<string>(new string[]
                    {
                    "ix00StartButton",
                    "ix01StopButton",
                    "ix02ResetButton",
                    "ix03SafetyRelay",
                    "ix04IncomingAir",
                    "ix05MainDoor",
                    "ix06InCurtainSens",
                    "ix07OutCurtainSens",
                    "ix10TorqueDriverYCylExtSignal",
                    "ix11TorqueDriverYCylRetSignal",
                    "ix12TorqueDriverReady",
                    "ix13TorqueDriverGood",
                    "ix14TorqueDriverNG",
                    "ix15TorqueDriverXHomeSens",
                    "ix16TorqueDriverXPosLimitSens",
                    "ix17TorqueDriverXNegLimitSens",
                    "ix20TorqueDriverXMotorAlarm",
                    "ix21TorqueDriverXInPos",
                    "ix22EStop1",
                    "ix23EStop2",
                    "ix24EStop3",
                    "ix25EStop4",
                    "ix26Z1PalletPresentSens",
                    "ix27Z2PalletPresentSens",
                    "ix30Shuttle1InPartPresentSens1",
                    "ix31Shuttle1InPartOverheadSens1",
                    "ix32Shuttle1OutPartPresentSens1",
                    "ix33Shuttle1OutPartOverheadSens1",
                    "ix34Shuttle1CurtainSens",
                    "ix35Shuttle1SendButton",
                    "ix36Shuttle1EStop",
                    "ix37Shuttle1InPartPresentSens2",
                    "ix40Shuttle1YHomeSens",
                    "ix41Shuttle1YPosLimitSens",
                    "ix42Shuttle1YNegLimitSens",
                    "ix43Shuttle1YMotorAlarm",
                    "ix44Shuttle1YInPos",
                    "ix45Shuttle1InPartOverheadSens2",
                    "ix46Shuttle1OutPartPresentSens2",
                    "ix47Shuttle1OutPartOverheadSens2",
                    "ix50Shuttle2InPartPresentSens1",
                    "ix51Shuttle2InPartOverheadSens1",
                    "ix52Shuttle2OutPartPresentSens1",
                    "ix53Shuttle2OutPartOverheadSens1",
                    "ix54Shuttle2CurtainSens",
                    "ix55Shuttle2SendButton",
                    "ix56Shuttle2Estop",
                    "ix57Shuttle2InPartPresentSens2",
                    "ix60Shuttle2YHomeSens",
                    "ix61Shuttle2YPosLimitSens",
                    "ix62Shuttle2YNegLimitSens",
                    "ix63Shuttle2YMotorAlarm",
                    "ix64Shuttle2YInPos",
                    "ix65Shuttle2InPartOverheadSens2",
                    "ix66Shuttle2OutPartPresentSens2",
                    "ix67Shuttle2OutPartOverheadSens2",
                    "ix70RobotReady",
                    "ix71RobotRunning",
                    "ix72RobotError",
                    "ix73RobotPaused",
                    "ix74Spare",
                    "ix75Spare",
                    "ix76RobotSpare",
                    "ix77RobotHomeDone",
                    "ix80Spare",
                    "ix81Spare",
                    "ix82Spare",
                    "ix83Spare",
                    "ix84Spare",
                    "ix85Spare",
                    "ix86TorqueDriverCylRetSignal",
                    "ix87TorqueDriverError",
                    "ix90Shuttle1PartRejectSens",
                    "ix91Spare",
                    "ix92Spare",
                    "ix93Spare",
                    "ix94Spare",
                    "ix95Spare",
                    "ix96Spare",
                    "ix97Spare",
                    "ix100Z1ReadyToPlace",
                    "ix101Z2ReadyToPlace",
                    "ix102Z1ReadyToTight",
                    "ix103Z2ReadyToTight",
                    "ix104Z13Spare",
                    "ix105Z13Spare",
                    "ix106Z13Spare",
                    "ix107Z13Spare"
                         });
                    break;
                #endregion
                #region Baffles
                case "7":
                    IOTagName = new List<string>(new string[]
                  {
                    "ix00StartButton",
                    "ix01StopButton",
                    "ix02ResetButton",
                    "ix03SafetyRelaySignal",
                    "ix04IncomingPressureSwitch",
                    "ix05MainDoorSwitch",
                    "ix06InCurtainSens",
                    "ix07OutCurtainSens",
                    "ix10TorqueDriverYCylExtSignal",
                    "ix11TorqueDriverYCylRetSignal",
                    "ix12TorqueDriverReady",
                    "ix13TorqueDriverGood",
                    "ix14TorqueDriverNG",
                    "ix15TorqueDriverXHomeSens",
                    "ix16TorqueDriverXPosLimitSens",
                    "ix17TorqueDriverXNegLimitSens",
                    "ix20TorqueDriverXMotorAlarm",
                    "ix21TorqueDriverXInPos",
                    "ix22EStop1",
                    "ix23EStop2",
                    "ix24EStop3",
                    "ix25EStop4",
                    "ix26Z1PalletPresentSens",
                    "ix27Z2PalletPresentSens",
                    "ix30Shuttle2InPartPresentSens1",
                    "ix31Shuttle2InPartOverheadSens1",
                    "ix32Shuttle2OutPartPresentSens1",
                    "ix33Shuttle2OutPartOverheadSens1",
                    "ix34Shuttle2CurtainSens",
                    "ix35Shuttle2SendButton",
                    "ix36Shuttle2EStop",
                    "ix37Shuttle2InPartPresentSens2",
                    "ix40Shuttle2YHomeSens",
                    "ix41Shuttle2YPosLimitSens",
                    "ix42Shuttle2YNegLimitSens",
                    "ix43Shuttle2YMotorAlarm",
                    "ix44Shuttle2YInPos",
                    "ix45Shuttle2InPartOverheadSens2",
                    "ix46Shuttle2OutPartPresentSens2",
                    "ix47Shuttle2OutPartOverheadSens2",
                    "ix50Shuttle1InPartPresentSens1",
                    "ix51Shuttle1InPartOverheadSens1",
                    "ix52Shuttle1OutPartPresentSens1",
                    "ix53Shuttle1OutPartOverheadSens1",
                    "ix54Shuttle1CurtainSens",
                    "ix55Shuttle1SendButton",
                    "ix56Shuttle1EStop",
                    "ix57Shuttle1InPartPresentSens2",
                    "ix60Shuttle1YHomeSens",
                    "ix61Shuttle1YPosLimitSens",
                    "ix62Shuttle1YNegLimitSens",
                    "ix63Shuttle1YMotorAlarm",
                    "ix64Shuttle1YInPos",
                    "ix65Shuttle1InPartOverheadSens2",
                    "ix66Shuttle1OutPartPresentSens2",
                    "ix67Shuttle1OutPartOverheadSens2",
                    "ix70Robot1Ready",
                    "ix71Robot1Running",
                    "ix72Robot1Error",
                    "ix73Robot1Paused",
                    "ix74Z1LeftBafflesHoleSens1",
                    "ix75Z1LeftBafflesHoleSens2",
                    "ix76Z1RightBafflesHoleSens1",
                    "ix77Z1RightBafflesHoleSens2",
                    "ix80Spare",
                    "ix81Spare",
                    "ix82Robot1Spare",
                    "ix83Robot1HomeDone",
                    "ix84LeftZCylExtSens",
                    "ix85RightZCylExtSens",
                    "ix86TorqueDriverCylRetSignal",
                    "ix87TorqueDriverError",
                    "ix90Robot2Ready",
                    "ix91Robot2Running",
                    "ix92Robot2Error",
                    "ix93Robot2Paused",
                    "ix94Z2LeftBafflesHoleSens1",
                    "ix95Z2LeftBafflesHoleSens2",
                    "ix96Z2RightBafflesHoleSens1",
                    "ix97Z2RightBafflesHoleSens2",
                    "ix100Spare",
                    "ix101Spare",
                    "ix102RobotSpare",
                    "ix103Robot2HomeDone",
                    "ix104Spare",
                    "ix105Spare",
                    "ix106Shuttle1PartRejectSens",
                    "ix107Shuttle2PartRejectSens",
                    "ix110Z16Z1ReadyToPlace",
                    "ix111Z16Z2ReadyToPlace",
                    "ix112Z16Spare",
                    "ix113Z16Spare",
                    "ix114Z16Spare",
                    "ix115Z16Spare",
                    "ix116Z16Spare",
                    "ix117Z16Spare",
                    "ix120Z1HolderYCylExtSens",
                    "ix121Z1HolderYCylRetSens",
                    "ix122Z1HolderZCylExtSens",
                    "ix123Z1HolderZCylRetSens",
                    "ix124Z1HolderGripperCylExtSens",
                    "ix125Z1HolderGripperCylRetSens",
                    "ix126Z1HolderLaserCylExtSens",
                    "ix127Z1HolderLaserCylRetSens",
                    "ix130Z2HolderYCylExtSens",
                    "ix131Z2HolderYCylRetSens",
                    "ix132Z2HolderZCylExtSens",
                    "ix133Z2HolderZCylRetSens",
                    "ix134Z2HolderGripperCylExtSens",
                    "ix135Z2HolderGripperCylRetSens",
                    "ix136Z2HolderLaserCylExtSens",
                    "ix137Z2HolderLaserCylRetSens",
                         });
                    break;
                #endregion
                #region BOIS&COBRA
                case "8":
                    IOTagName = new List<string>(new string[]
                   {
                    "ix00StartButton",
                    "ix01StopButton",
                    "ix02ResetButton",
                    "ix03SafetyRelaySignal",
                    "ix04IncomingPressureSwitch",
                    "ix05MainDoorSwitch",
                    "ix06InCurtainSens",
                    "ix07OutCurtainSens",
                    "ix10Rack1DoorSwitch",
                    "ix11Rack1CurtainSens",
                    "ix12Rack1ProxiSens",
                    "ix13Spare",
                    "ix14Spare",
                    "ix15Elvtr1TrayPresentSens",
                    "ix16Elvtr1RackPosSens",
                    "ix17Elvtr1YFrontPrecisCylExtSignal",
                    "ix20Elvtr1YFrontPrecisCylRetSignal",
                    "ix21Elvtr1YSidePrecisCylExtSignal",
                    "ix22Elvtr1YSidePrecisCylRetSignal",
                    "ix23Elvtr1YHomeSens",
                    "ix24Elvtr1YPosLimitSens",
                    "ix25Elvtr1YNegLimitSens",
                    "ix26Elvtr1YMotorAlarm",
                    "ix27Elvtr1YInPos",
                    "ix30Elvtr1ZHomeSens",
                    "ix31Elvtr1ZPosLimitSens",
                    "ix32Elvtr1ZNegLimitSens",
                    "ix33Elvtr1ZMotorAlarm",
                    "ix34Elvtr1ZInPos",
                    "ix35Elvtr1TRAYInPosSens",
                    "ix36Elvtr1BiosChip180DegPos",
                    "ix37Elvtr1BiosChip0DegPos",
                    "ix40Rack2DoorSwitch",
                    "ix41Rack2CurtainSens",
                    "ix42Rack2ProxiSens",
                    "ix43Spare",
                    "ix44Elvtr2PartRejectSens",
                    "ix45Elvtr2TrayPresentSens",
                    "ix46Elvtr2RackPosSens",
                    "ix47Elvtr2YFrontPrecisCylExtSignal",
                    "ix50Elvtr2YFrontPrecisCylRetSignal",
                    "ix51Elvtr2YSidePrecisCylExtSignal",
                    "ix52Elvtr2YSidePrecisCylRetSignal",
                    "ix53Elvtr2YHomeSens",
                    "ix54Elvtr2YPosLimitSens",
                    "ix55Elvtr2YNegLimitSens",
                    "ix56Elvtr2YMotorAlarm",
                    "ix57Elvtr2YInPos",
                    "ix60Elvtr2ZHomeSens",
                    "ix61Elvtr2ZPosLimitSens",
                    "ix62Elvtr2ZNegLimitSens",
                    "ix63Elvtr2ZMotorAlarm",
                    "ix64Elvtr2ZInPos",
                    "ix65Elvtr2TRAYInPosSens",
                    "ix66Spare",
                    "ix67Elvtr1BIOSChipPresentSens",
                    "ix70Robot1Ready",
                    "ix71Robot1Running",
                    "ix72Robot1Error",
                    "ix73Robot1Paused",
                    "ix74Robot1HomeDone",
                    "ix75Robot1Spare",
                    "ix76Spare",
                    "ix77Spare",
                    "ix80EStop1",
                    "ix81EStop2",
                    "ix82EStop3",
                    "ix83EStop4",
                    "ix84Z1PalletPresentSens",
                    "ix85Z2PalletPresentSens",
                    "ix86Spare",
                    "ix87GripperPartPresentSens",
                    "ix90Z18Z1ReqToPlaceBios",
                    "ix91Z18Z1ReqToPlaceCobra",
                    "ix92Z18Spare",
                    "ix93Z18Spare",
                    "ix94Z18Z2ReqToPlaceBios",
                    "ix95Z18Z2ReqToPlaceCobra",
                    "ix96Z18Spare",
                    "ix97Z18Spare",
                    "ix100Rack1Tray1PresenceSens",
                    "ix101Rack1Tray2PresenceSens",
                    "ix102Rack1Tray3PresenceSens",
                    "ix103Rack1Tray4PresenceSens",
                    "ix104Rack2Tray1PresenceSens",
                    "ix105Rack2Tray2PresenceSens",
                    "ix106Rack2Tray3PresenceSens",
                    "ix107Rack2Tray4PresenceSens",
                    "ix110Robot2Ready",
                    "ix111Robot2Running",
                    "ix112Robot2Error",
                    "ix113Robot2Paused",
                    "ix114Robot2Spare",
                    "ix115Robot2HomeDone",
                    "ix116Spare",
                    "ix117Spare",
                    "ix120Spare",
                    "ix121Spare",
                    "ix122Spare",
                    "ix123Spare",
                    "ix124Spare",
                    "ix125Spare",
                    "ix126Spare",
                    "ix127Spare",
                    "ix130Z2HolderYCylExtSens",
                    "ix131Z2HolderYCylRetSens",
                    "ix132Z2HolderZCylExtSens",
                    "ix133Z2HolderZCylRetSens",
                    "ix134Z2HolderGripperCylExtSens",
                    "ix135Z2HolderGripperCylRetSens",
                    "ix136Z2HolderLaserCylExtSens",
                    "ix137Z2HolderLaserCylRetSens"
                         });
                    break;
                #endregion
                #region LHS
                case "9":
                    IOTagName = new List<string>(new string[]
                    {
                    "ix00StartButton",
                    "ix01StopButton",
                    "ix02ResetButton",
                    "ix03SafetyRelaySignal",
                    "ix04IncomingPressureSwitch",
                    "ix05MainDoorSwitch",
                    "ix06InCurtainSens",
                    "ix07OutCurtainSens",
                    "ix10ShuttleInPartPresentSens1",
                    "ix11ShuttleInPartOverheadSens1",
                    "ix12Spare",
                    "ix13ShuttleInPartOverheadSens2",
                    "ix14ShuttleCurtainSens",
                    "ix15ShuttleSendButton",
                    "ix16ShuttleEStop",
                    "ix17PartRejectSens",
                    "ix20ShuttleYHomeSens",
                    "ix21ShuttleYPosLimitSens",
                    "ix22ShuttleYNegLimitSens",
                    "ix23ShuttleYMotorAlarm",
                    "ix24ShuttleYInPos",
                    "ix25Z1PalletPresentSens",
                    "ix26Z2PalletPresentSens",
                    "ix27Spare",
                    "ix30Robot1Ready",
                    "ix31Robot1Running",
                    "ix32Robot1Error",
                    "ix33Robot1Paused",
                    "ix34Spare",
                    "ix35Spare",
                    "ix36Spare",
                    "ix37Spare",
                    "ix40Robot1Spare",
                    "ix41Robot1HomeDone",
                    "ix42Purge",
                    "ix43ShuttleOutPartPresentSens1",
                    "ix44ShuttleOutPartOverheadSens1",
                    "ix45Spare",
                    "ix46ShuttleOutPartOverheadSensor2",
                    "ix47Spare",
                    "ix50Robot2Ready",
                    "ix51Robot2Running",
                    "ix52Robot2Error",
                    "ix53Robot2Paused",
                    "ix54Spare",
                    "ix55Spare",
                    "ix56Robot2Spare",
                    "ix57Robot2HomeDone",
                    "ix60Spare",
                    "ix61Spare",
                    "ix62Spare",
                    "ix63Spare",
                    "ix64EStop1",
                    "ix65EStop2",
                    "ix66EStop3",
                    "ix67EStop4",
                    "ix70Z20Z1ReadyToPlaceTight",
                    "ix71Z20Z2ReadyToPlaceTight",
                    "ix72Z20Spare",
                    "ix73Z20Spare",
                    "ix74Z20Spare",
                    "ix75Z20Spare",
                    "ix76Z20Spare",
                    "ix77Z20Spare",
                    "ix80EStop1",
                    "ix81EStop2",
                    "ix82EStop3",
                    "ix83EStop4",
                    "ix84Z1PalletPresentSens",
                    "ix85Z2PalletPresentSens",
                    "ix86Spare",
                    "ix87GripperPartPresentSens",
                    "ix90Z18Z1ReqToPlaceBios",
                    "ix91Z18Z1ReqToPlaceCobra",
                    "ix92Z18Spare",
                    "ix93Z18Spare",
                    "ix94Z18Z2ReqToPlaceBios",
                    "ix95Z18Z2ReqToPlaceCobra",
                    "ix96Z18Spare",
                    "ix97Z18Spare",
                    "ix100Rack1Tray1PresenceSens",
                    "ix101Rack1Tray2PresenceSens",
                    "ix102Rack1Tray3PresenceSens",
                    "ix103Rack1Tray4PresenceSens",
                    "ix104Rack2Tray1PresenceSens",
                    "ix105Rack2Tray2PresenceSens",
                    "ix106Rack2Tray3PresenceSens",
                    "ix107Rack2Tray4PresenceSens",
                    "ix110Robot2Ready",
                    "ix111Robot2Running",
                    "ix112Robot2Error",
                    "ix113Robot2Paused",
                    "ix114Robot2Spare",
                    "ix115Robot2HomeDone",
                    "ix116Spare",
                    "ix117Spare",
                    "ix120Spare",
                    "ix121Spare",
                    "ix122Spare",
                    "ix123Spare",
                    "ix124Spare",
                    "ix125Spare",
                    "ix126Spare",
                    "ix127Spare",
                    "ix130Z2HolderYCylExtSens",
                    "ix131Z2HolderYCylRetSens",
                    "ix132Z2HolderZCylExtSens",
                    "ix133Z2HolderZCylRetSens",
                    "ix134Z2HolderGripperCylExtSens",
                    "ix135Z2HolderGripperCylRetSens",
                    "ix136Z2HolderLaserCylExtSens",
                    "ix137Z2HolderLaserCylRetSens"
                         });
                    break;
                #endregion
                #region SHS
                case "10":
                    IOTagName = new List<string>(new string[]
                   {
                    "ix00StartButton",
                    "ix01StopButton",
                    "ix02ResetButton",
                    "ix03SafetyRelaySignal",
                    "ix04IncomingPressureSwitch",
                    "ix05MainDoorSwitch",
                    "ix06InCurtainSens",
                    "ix07OutCurtainSens",
                    "ix10ShuttleInPartPresentSens1",
                    "ix11ShuttleInPartOverheadSens1",
                    "ix12Spare",
                    "ix13ShuttleInPartOverheadSens2",
                    "ix14ShuttleCurtainSens",
                    "ix15ShuttleSendButton",
                    "ix16ShuttleEStop",
                    "ix17PartRejectSens",
                    "ix20ShuttleYHomeSens",
                    "ix21ShuttleYPosLimitSens",
                    "ix22ShuttleYNegLimitSens",
                    "ix23ShuttleYMotorAlarm",
                    "ix24ShuttleYInPos",
                    "ix25Z1PalletPresentSens",
                    "ix26Z2PalletPresentSens",
                    "ix27Spare",
                    "ix30Robot1Ready",
                    "ix31Robot1Running",
                    "ix32Robot1Error",
                    "ix33Robot1Paused",
                    "ix34Spare",
                    "ix35Spare",
                    "ix36Spare",
                    "ix37Spare",
                    "ix40Robot1Spare",
                    "ix41Robot1HomeDone",
                    "ix42Purge",
                    "ix43ShuttleOutPartPresentSens1",
                    "ix44ShuttleOutPartOverheadSens1",
                    "ix45Spare",
                    "ix46ShuttleOutPartOverheadSensor2",
                    "ix47Spare",
                    "ix50Robot2Ready",
                    "ix51Robot2Running",
                    "ix52Robot2Error",
                    "ix53Robot2Paused",
                    "ix54Spare",
                    "ix55Spare",
                    "ix56Robot2Spare",
                    "ix57Robot2HomeDone",
                    "ix60Spare",
                    "ix61Spare",
                    "ix62Spare",
                    "ix63Spare",
                    "ix64EStop1",
                    "ix65EStop2",
                    "ix66EStop3",
                    "ix67EStop4",
                    "ix70Z22Z1ReadyToPlaceTight",
                    "ix71Z22Z2ReadyToPlaceTight",
                    "ix72Z22Spare",
                    "ix73Z22Spare",
                    "ix74Z22Spare",
                    "ix75Z22Spare",
                    "ix76Z22Spare",
                    "ix77Z22Spare",
                    "ix80EStop1",
                    "ix81EStop2",
                    "ix82EStop3",
                    "ix83EStop4",
                    "ix84Z1PalletPresentSens",
                    "ix85Z2PalletPresentSens",
                    "ix86Spare",
                    "ix87GripperPartPresentSens",
                    "ix90Z18Z1ReqToPlaceBios",
                    "ix91Z18Z1ReqToPlaceCobra",
                    "ix92Z18Spare",
                    "ix93Z18Spare",
                    "ix94Z18Z2ReqToPlaceBios",
                    "ix95Z18Z2ReqToPlaceCobra",
                    "ix96Z18Spare",
                    "ix97Z18Spare",
                    "ix100Rack1Tray1PresenceSens",
                    "ix101Rack1Tray2PresenceSens",
                    "ix102Rack1Tray3PresenceSens",
                    "ix103Rack1Tray4PresenceSens",
                    "ix104Rack2Tray1PresenceSens",
                    "ix105Rack2Tray2PresenceSens",
                    "ix106Rack2Tray3PresenceSens",
                    "ix107Rack2Tray4PresenceSens",
                    "ix110Robot2Ready",
                    "ix111Robot2Running",
                    "ix112Robot2Error",
                    "ix113Robot2Paused",
                    "ix114Robot2Spare",
                    "ix115Robot2HomeDone",
                    "ix116Spare",
                    "ix117Spare",
                    "ix120Spare",
                    "ix121Spare",
                    "ix122Spare",
                    "ix123Spare",
                    "ix124Spare",
                    "ix125Spare",
                    "ix126Spare",
                    "ix127Spare",
                    "ix130Z2HolderYCylExtSens",
                    "ix131Z2HolderYCylRetSens",
                    "ix132Z2HolderZCylExtSens",
                    "ix133Z2HolderZCylRetSens",
                    "ix134Z2HolderGripperCylExtSens",
                    "ix135Z2HolderGripperCylRetSens",
                    "ix136Z2HolderLaserCylExtSens",
                    "ix137Z2HolderLaserCylRetSens",
                         });
                    break;
                #endregion
                #region MainConveyor
                case "0":
                    IOTagName = new List<string>(new string[]
                    {
                         });
                    break;
                    #endregion
            }

            return IOTagName;
        }
    }
}
