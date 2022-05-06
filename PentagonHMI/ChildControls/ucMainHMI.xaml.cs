using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using PentagonHMI.UserControls;
using System.Data;
using System.Timers;
using System.Threading;

namespace PentagonHMI.ChildControls
{
    public partial class ucMainHMI : UserControl, IDisposable
    {
        #region Variable    
        LogicClasses.Main _MainConnection;
        LogicClasses.IOInput _IOPage;
        string status;
        string CurrentUPH;
        int Listn = 0;
        #endregion
        PentagonHMI.Main Mainxaml;
        //bool displaymenu;
        List<TextBox> OrderTBlist;
        List<TextBox> StatusList;
        List<Rectangle> RecStatus;
        List<ToggleButton> Listtoggle;
        List<StackPanel> SPPart;
        List<UCCheckBoxBed> IObed;
        List<UCPointIO> PonitIOlist;
        List<string> ZoneID;
        List<CheckBox> Z29ChkBox;
        List<Rectangle> Bed;
        List<TextBox> PartNameList;
        List<TextBox> QuantityList;
        List<KeyValuePair<string, long>> _acValueList = new List<KeyValuePair<string, long>>();
        string[] PartModelName = new string[60];
        System.Timers.Timer Time;
        Brush Default;
        Brush EstopDefault;

        List<Rectangle> ShuttleBed;
        List<Grid> FrontShuttleBed;
        //List<StackPanel> ZoneTag;

        //IOBed Input
        string True = "T ";
        string False = "F ";

        Thread IOThread;

        #region Constructor
        public ucMainHMI(ref LogicClasses.Main MainConnection, PentagonHMI.Main _M, ref LogicClasses.IOInput IOPage)
        {
            try
            {
                _MainConnection = MainConnection;
                _IOPage = IOPage;
                InitializeComponent();
                LogicClasses.MainHMI pMainHMI = new LogicClasses.MainHMI(ref MainConnection, this);
                this.MainHMI = pMainHMI;
                Mainxaml = _M;
                MainHMI.OnUpdate += new LogicClasses.MainHMI.onUpdateHandler(MainHMI_OnUpdate);
                Initialize();
                IOThread = new Thread(UpdateIO);
                IOThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        Polygon PolygonSpare = new Polygon();
        private void TBlist()
        {
            //ZoneTag = new List<StackPanel>
            //{

            //};

            ShuttleBed = new List<Rectangle>
            {
                Zone6A,Zone6B,//Asset Tag
                Zone8A,Zone8B,//Mobo Robot
                Zone10A,Zone10B,//K&G
                Zone13A,Zone13B,//Affix Fan
                Zone16A,Zone16B,//Baffles
                Zone18A,Zone18B,//B&C
                Zone20A,Zone20B,//Large
                Zone22A,Zone22B,//Small
            };

            FrontShuttleBed = new List<Grid>
            {
                s2Zone6A,s2Zone6B,//Asset Tag
                s4Zone8A,s4Zone8B,//Mobo Robot
                s5Zone10A,s5Zone10B,//K&G
                s6Zone13A,s6Zone13B,//Affix Fan
                s7Zone16A,s7Zone16B,//Baffles
                s8Zone18A,s8Zone18B,//B&C
                s9Zone20A,s9Zone20B,//Large
                s10Zone22A,s10Zone22B,//Small
            };

            PartNameList = new List<TextBox>
            {
                P1,P4,P5,P5_2,P6,P6_2,P7,P7_2,P7_3,P7_4,P8,P8_2,P9,P9_2,P10,P10_2,P11,P11_2,P12,
            };

            QuantityList = new List<TextBox>
            {
                Qty1,Qty4,Qty5,Qty5_2,Qty6,Qty6_2,Qty7,Qty7_2,Qty7_3,Qty7_4,Qty8,Qty8_2,Qty9,Qty9_2,Qty10,Qty10_2,Qty11,Qty11_2,Qty12,
            };

            OrderTBlist = new List<TextBox>
             {
                O1, O2, O3, O4, O5, O6, O7, O8, O9, O10,
                O11, O12, O13, O14, O15, O16, O17, O18, O19, O20,
                O21, O22, O23, O24, O25, O26, O27, O28, O29, O30,
                O31, O32, O33, O34, O35, O36, O37, O38, O39, O40,
                O41, O42, O43, O44, O45, O46, O47, //O48, O49, O50,
                //O51,
             };

            StatusList = new List<TextBox>
            {
                M1,M2,M3,M4,M5,M6,M7,M8,M9,M10,M11,M12,
            };

            RecStatus = new List<Rectangle>
            {
                c1,c2,c3,c4,c5,c6,c7,c8,c9,c10,c11,c12,
            };

            Listtoggle = new List<ToggleButton>
            {
                S1,S2,S3,S4,S5,S6,S7,S8,S9,S10,S11,S12,
            };

            SPPart = new List<StackPanel>
            {
                Station1,Station2,Station3,Station4,Station5,Station6,Station7,Station8,Station9,Station10,Station11,Station12,
            };

            IObed = new List<UCCheckBoxBed>
            {
                //47
                L1,L2,L3,L4,L5,L6,L7,L8,L9,L10,L11,L12,L13,L14,L15,L16,L17,L18,L19,L20,L21,L22,L23,L24,L25,L26,L27,
                Lb1,Lb2,Lb3,Lb4,Lb5,Lb6,Lb7,Lb8,Lb9,Lb10,Lb11,Lb12,Lb13,Lb14,Lb15,Lb16,Lb17,Lb18,Lb19,Lb20,Lb21,Lb22,Lb23,Lb24,Lb25,Lb26,Lb27,Lb28,Lb29
            };

            PonitIOlist = new List<UCPointIO>
            {
                Point0,Point1,Point2,Point3,Point4,Point5,Point6,Point7,Point8,Point9,Point10,
                Point11,Point12,Point13,Point14,Point15,Point16,Point17,Point18,Point19,
                Point20,Point21,Point22,Point23,Point24,Point25,Point26,Point27,Point28,
            };

            Bed = new List<Rectangle>
            {
                Zone1,
                Zone2A,Zone2B,
                Zone3,
                Zone5A,Zone5B,Zone5C,
                Zone6A,Zone6B,
                Zone7A,Zone7B,Zone7C,
                Zone8A,Zone8B,
                Zone9A,Zone9B,Zone9C,
                Zone10A,Zone10B,
                Zone11A,Zone11B,Zone11C,
                Zone12A,Zone12B,Zone12C,
                Zone13A,Zone13B,
                Zone14A,Zone14B,
                Zone15A,Zone15B,
                Zone16A,Zone16B,
                Zone17A,Zone17B,Zone17C,
                Zone18A,Zone18B,
                Zone19A,Zone19B,Zone19C,
                Zone20A,Zone20B,
                Zone21A,Zone21B,Zone21C,
                Zone22A,Zone22B,
                Zone23A,Zone23B,Zone23C,
                Zone24A,Zone24B,
                Zone25,
                Zone26,
                Zone27,
                Zone29A,Zone29B,Zone29C,Zone29D
            };

            ZoneID = new List<string>
            {
                "Zone6A","Zone6B",
                "Zone7A","Zone7B","Zone7C",
                "Zone8A","Zone8B",
                "Zone9A","Zone9B","Zone9C",

                "Zone10A","Zone10B",
                "Zone11A","Zone11B","Zone11C",
                "Zone12A","Zone12B","Zone12C",
                "Zone13A","Zone13B",

                "Zone14A","Zone14B",
                "Zone15A","Zone15B",
                "Zone16A","Zone16B",
                "Zone17A","Zone17B","Zone17C",

                "Zone18A","Zone18B",
                "Zone19A","Zone19B","Zone19C",
                "Zone20A","Zone20B",
                "Zone21A","Zone21B","Zone21C",

                "Zone22A","Zone22B",
                "Zone23A","Zone23B","Zone23C",
                "Zone24A","Zone24B",
                "END",

                "Zone29A","Zone29B","Zone29C","Zone29D",
            };

            Z29ChkBox = new List<CheckBox>
            {
                Z29AIn,Z29AOut, Z29BIn,Z29BOut, Z29CIn,Z29COut, Z29DIn,Z29DOut,
            };
        }

        #endregion

        #region Properties     
        bool isChecked = false;
        LogicClasses.MainHMI MainHMI { get; set; }

        #endregion

        bool AlwaysUpdate = true;
        void UpdateIO()
        {
            while (AlwaysUpdate)
            {
                try
                {
                    this.Dispatcher.Invoke(new Action(() => IOAssign()));
                    System.Threading.Thread.Sleep(200);
                }
                catch (Exception ex)
                {
                    Utilities.FileLogger.logError(ex.Message, "Update IO Loop failed");
                }
            }
        }

        #region Events
        void MainHMI_OnUpdate()
        {

            try
            {
                //if (Mainxaml.ZoneErrors != null)
                //    this.Dispatcher.Invoke(new Action(() => BedUpdate()));
                System.Threading.Thread.Sleep(10);
                this.Dispatcher.Invoke(new Action(() => MCStatus(Convert.ToBoolean(MainHMI.MConline.Value))));
                System.Threading.Thread.Sleep(10);
                this.Dispatcher.Invoke(new Action(() => Z29PLC.Background = !(MainHMI.ZoneList[MainHMI.ZoneStatusTagName.Count - 1]) ? Brushes.Green : Brushes.Red));
                System.Threading.Thread.Sleep(10);
                this.Dispatcher.Invoke(new Action(() => OrderInfoAssign()));
                System.Threading.Thread.Sleep(10);
                this.Dispatcher.Invoke(new Action(() => StatusAssign()));
                System.Threading.Thread.Sleep(10);
                this.Dispatcher.Invoke(new Action(() => AssignQTY()));
                //System.Threading.Thread.Sleep(10);
                //this.Dispatcher.Invoke(new Action(() => DisableStatus()));

            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Main HMI Status Loop Failed");
            }
        }
        #endregion
        #region Methods
        private void Initialize()
        {
            Time = new System.Timers.Timer();
            Time.Elapsed += new ElapsedEventHandler(CloseMessage);
            Time.Interval = 10000;
            Time.Enabled = false;

            TBlist();
            Orderinfo.Visibility = Visibility.Collapsed;
            Default = c1.Fill;
            EstopDefault = LBEstopZone.Background;
            foreach (StackPanel SP in SPPart)
            {
                SP.Visibility = Visibility.Collapsed;
            }

            Point0.IP = "PIO200";
            Point1.IP = "PIO201";
            Point2.IP = "PIO202";
            Point3.IP = "PIO203";
            Point4.IP = "PIO204";
            Point5.IP = "PIO205";
            Point6.IP = "PIO206";
            Point7.IP = "PIO207";
            Point8.IP = "PIO208";
            Point9.IP = "PIO209";
            Point10.IP = "PIO210";
            Point11.IP = "PIO211";
            Point12.IP = "PIO212";
            Point13.IP = "PIO213";
            Point14.IP = "PIO214";
            Point15.IP = "PIO215";
            Point16.IP = "PIO216";
            Point17.IP = "PIO217";
            Point18.IP = "PIO218";
            Point19.IP = "PIO219";
            Point20.IP = "PIO220";
            Point21.IP = "PIO221";
            Point22.IP = "PIO222";
            Point23.IP = "PIO223";
            Point24.IP = "PIO224";
            Point25.IP = "PIO225";
            Point26.IP = "PIO226";
            Point27.IP = "PIO227";
            Point28.IP = "PIO228";
            //Point29.IP = "PIO229";

            PartModelName = MainHMI.GetPartModel();
            int nTB = 0;
            foreach (TextBox TB in PartNameList)
            {
                TB.Text = PartModelName[nTB];
                nTB++;
            }
        }

        private void MCStatus(bool Status)
        {
            if (Status)
            {
                ConveyorOnlineStatus.Background = Brushes.Green;
                ConveyorOnlineStatus.Content = "ONLINE";
            }
            else
            {
                ConveyorOnlineStatus.Background = Brushes.Red;
                ConveyorOnlineStatus.Content = "OFFLINE";
            }
        }

        bool blinkbed = true;
        private void BedUpdate()
        {
            try
            {
                bool EStopOn = false;
                string EstopZone = string.Empty;
                int ns = 0;
                foreach (Rectangle Rec in Bed)
                {
                    string a = Rec.Name.ToString().ToUpper().Replace("ZONE", "");
                    if (true)/*(Mainxaml.ZoneErrors.Contains(a))*/
                    {

                        //Canvas.SetZIndex(Rec,1);
                        if (MainHMI.CheckEstop(a))
                        {
                            EStopOn = true;
                            EstopZone += (EstopZone.Length > 0 ? "\n" : string.Empty) + a;
                            Rec.Fill = Brushes.Red;
                        }
                        else
                        {
                            Rec.Opacity = 1;
                            Rec.Fill = Brushes.Orange;
                        }
                    }
                    else
                    {
                        Rec.Fill = Brushes.White;
                        Rec.Opacity = 0.8;
                    }

                    if (ShuttleBed.Contains(Rec))
                    {
                        if (MainHMI.DisStatusShuttle[ns])
                        {
                            //Canvas.SetZIndex(Rec, 1);
                            FrontShuttleBed[ns].Visibility = Visibility.Visible;
                            //Rec.Opacity = 1;
                        }
                        else
                        {
                            FrontShuttleBed[ns].Visibility = Visibility.Collapsed;
                        }
                        ns++;
                    }
                }

                if (!EStopOn)
                {
                    SP_EstopError.Visibility = Visibility.Collapsed;
                }
                else
                {
                    LBEstopZone.Background = Brushes.Yellow;
                    LBEstopZone.Foreground = Brushes.Black;
                    LBEstopZone.Content = EstopZone;
                    SP_EstopError.Visibility = Visibility.Visible;
                }

                if (blinkbed)
                {
                    blinkbed = false;
                }
                else
                {
                    blinkbed = true;
                    //foreach (Rectangle Rec in Bed)
                    //{
                    //    Rec.Fill = Brushes.White;
                    //    Rec.Opacity = 0.8;

                    //}

                    LBEstopZone.Background = EstopDefault;
                    LBEstopZone.Foreground = Brushes.Red;
                }


            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "BedUpdate - Bed Update failed");
            }

        }

        public void UPHupdate(string strUPH)
        {
            try
            {
                int i = 0;
                foreach (string uph in strUPH.Split(','))
                {
                    KeyValuePair<string, long> item = new KeyValuePair<string, long>(i.ToString(), (long)Convert.ToInt64(uph.ToString()));
                    var target_idx = _acValueList.FindIndex(n => n.Key.Equals(item.Key));
                    if (target_idx != -1) { _acValueList[target_idx] = item; }
                    else { _acValueList.Add(item); }
                    i++;
                    CurrentUPH = uph;
                }
                CurrentUPH = _acValueList[DateTime.Now.Hour].Value.ToString();
                this.Dispatcher.Invoke(new Action(() => UPHLine.Content = CurrentUPH));
                this.Dispatcher.Invoke(new Action(() => lineChart.ItemsSource = _acValueList));
                this.Dispatcher.Invoke(new Action(() => TagChart.ItemsSource = _acValueList));
                GC.Collect();
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "UPHupdate - Update UPH failed");
            }

        }

        //private void DisableStatus()
        //{

        //}

        private void AssignQTY()
        {
            try
            {
                int nTB = 0;
                foreach (TextBox TB in QuantityList)
                {
                    TB.Text = Convert.ToString(MainHMI.StockQtyList[nTB]);
                    nTB++;
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "AssignQTY - Assign Quantity Failed");
            }
        }

        private void IOAssign()
        {
            try
            {
                int index = 0;
                foreach (CheckBox CB in Z29ChkBox)
                {
                    CB.IsChecked = MainHMI.BoolZ29[index];
                    index++;
                }

                int Bedn = 0;
                Listn = 0;
                string output = "";

                foreach (UCPointIO _PIO in PonitIOlist)
                {
                    _PIO.Status = MainHMI.ZoneList[Listn] ? false : true;
                    Listn++;
                }

                Listn = 0;

                foreach (UCCheckBoxBed _IO in IObed)
                {
                    output = "";
                    output += MainHMI.BoolMainHMIIOList[Bedn] == true ? True : False; Bedn++;
                    output += MainHMI.BoolMainHMIIOList[Bedn] == true ? True : False; Bedn++;
                    if (!_IO.NoPresentSensor)
                    {
                        output += MainHMI.BoolMainHMIIOList[Bedn] == true ? True : False; Bedn++;
                    }
                    IObed[Listn].IOvalue = output;

                    Listn++;
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Main HMI Status IO Assign Failed");
            }

        }

        private void OrderInfoAssign()
        {
            try
            {
                Listn = 0;
                foreach (TextBox TB in OrderTBlist)
                {
                    if (MainHMI.OrderID[Listn] != null)
                    {
                        string[] Info = MainHMI.OrderID[Listn].Split('|');
                        TB.Text = Info[0];
                        SolidColorBrush Coloring = (Info[1] == "In-Progress") ? Brushes.Green : Brushes.Gray;
                        TB.Background = Coloring;
                    }

                    Listn++;
                }
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "Main HMI Order Assign Failed");
            }
        }

        bool blink = true;
        string Disbaling = string.Empty;
        private void StatusAssign()
        {
            Listn = 0;
            if (blink)
            {
                //blink = false;
                foreach (TextBox TB in StatusList)
                {
                    status = MainHMI.StatusList[Listn] == null ? "" : MainHMI.StatusList[Listn].ToUpper();
                    TB.Text = MainHMI.StatusList[Listn];
                    RecStatus[Listn].Fill = Default;

                    if (status == "RUNNING")
                    {
                        TB.Background = Brushes.Green;
                        TB.Foreground = Brushes.Snow;                        
                    }
                    else if (status == "STOPPED")
                    {
                        TB.Text = "Waiting";
                        TB.Background = Brushes.Yellow;
                        TB.Foreground = Brushes.Black;
                        RecStatus[Listn].Fill = Brushes.Yellow;
                    }
                    else if (status == "IDLE")
                    {
                        TB.Background = Brushes.Blue;
                        TB.Foreground = Brushes.Snow;
                        RecStatus[Listn].Fill = Brushes.Blue;
                    }
                    else if (status == "INITIALIZE")
                    {
                        TB.Background = Brushes.Blue;
                        TB.Foreground = Brushes.Snow;
                        RecStatus[Listn].Fill = Brushes.Blue;
                    }
                    else if (status == "ERROR")
                    {
                        TB.Background = Brushes.Red;
                        TB.Foreground = Brushes.Yellow;
                        RecStatus[Listn].Fill = Brushes.Red;
                    }
                    else if (status == "WARNING")
                    {
                        TB.Background = Brushes.Yellow;
                        TB.Foreground = Brushes.Black;
                        RecStatus[Listn].Fill = Brushes.Yellow;
                    }
                    else if (status == "ALARM")
                    {
                        TB.Background = Brushes.Yellow;
                        TB.Foreground = Brushes.Black;
                        RecStatus[Listn].Fill = Brushes.Yellow;
                    }
                    else
                    {
                        TB.Background = Brushes.Yellow;
                        TB.Foreground = Brushes.Black;
                    }

                    if (MainHMI.DisStatusStation[Listn])
                    {
                        RecStatus[Listn].Fill = Brushes.Black;
                        RecStatus[Listn].Stroke = Brushes.Red;
                        RecStatus[Listn].Opacity = 0.8;
                    }
                    else
                    {
                        RecStatus[Listn].Opacity = 0.6;
                        RecStatus[Listn].Stroke = Brushes.Gray;
                    }

                    Listn++;
                }
            }
            //else
            //{
            //    blink = true;
            //    foreach (Rectangle Rec in RecStatus)
            //    {
            //        Rec.Fill = Brushes.Red;
            //        //Rec.Stroke = Brushes.Red;
            //    }
            //}
        }
        public void Dispose()
        {
            //main.Change2NORMAL();
            //if (this.MainHMI != null)
            //{
            //    this.MainHMI.OnUpdate -= new LogicClasses.MainHMI.onUpdateHandler(MainHMI_OnUpdate);
            //}

        }
        #endregion
        #region Destructor
        ~ucMainHMI()
        {
            Dispose();
        }

        #endregion

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {

            if (((RadioButton)sender).IsChecked == true && isChecked)
            {
                ((RadioButton)sender).IsChecked = false;
                isChecked = false;
            }
            else
                isChecked = true;

            if (((RadioButton)sender).IsChecked == true)
            {
                foreach (StackPanel SP in SPPart)
                {
                    SP.Visibility = Visibility.Visible;
                }
                foreach (ToggleButton TB in Listtoggle)
                {
                    TB.IsChecked = true;
                    TB.Foreground = Brushes.Black;
                    TB.Content = TB.Content.ToString().Replace("^ ", "");
                    TB.Content = "^ " + TB.Content;
                }
            }
            else
            {
                foreach (StackPanel SP in SPPart)
                {
                    SP.Visibility = Visibility.Collapsed;
                }
                foreach (ToggleButton TB in Listtoggle)
                {
                    TB.IsChecked = false;
                    TB.Foreground = Brushes.White;
                    TB.Content = TB.Content.ToString().Replace("^ ", "");
                }
            }

        }

        private void Menu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //Mainxaml.MainAlertShow(true);
        }

        public bool uphvisbility;
        private void UPHshow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            uphvisbility = uphvisbility ? false : true;
            UPHchart.Visibility = uphvisbility ? Visibility.Visible : Visibility.Collapsed;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            OrderDetail.Visibility = Visibility.Collapsed;
        }

        private void TB2_click(object sender, RoutedEventArgs e)
        {
            ((ToggleButton)sender).IsChecked = false;
        }

        private void TB_click(object sender, RoutedEventArgs e)
        {
            if (isChecked == false)
            {
                ToggleButton TB = (ToggleButton)sender;
                TB.Foreground = TB.IsChecked == true ? Brushes.Black : Brushes.White;
                TB.Content = TB.IsChecked == true ? TB.Content = "^ " + TB.Content : TB.Content = TB.Content.ToString().Replace("^ ", "");
                Visibility v = TB.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
                switch (TB.Name.ToString())
                {
                    case "S1":
                        Station1.Visibility = v;
                        break;
                    case "S2":
                        Station2.Visibility = v;
                        break;
                    case "S3":
                        Station3.Visibility = v;
                        break;
                    case "S4":
                        Station4.Visibility = v;
                        break;
                    case "S5":
                        Station5.Visibility = v;
                        break;
                    case "S6":
                        Station6.Visibility = v;
                        break;
                    case "S7":
                        Station7.Visibility = v;
                        break;
                    case "S8":
                        Station8.Visibility = v;
                        break;
                    case "S9":
                        Station9.Visibility = v;
                        break;
                    case "S10":
                        Station10.Visibility = v;
                        break;
                    case "S11":
                        Station11.Visibility = v;
                        break;
                    case "S12":
                        Station12.Visibility = v;
                        break;
                    default:
                        break;
                }
            }
        }
        private void CloseMessage(object source, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() => Orderinfo.Visibility = Visibility.Collapsed));
            Time.Enabled = false;
        }
        private void O1_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBox TB = (TextBox)sender;
            if (TB.Text.Length > 0)
            {
                int n = Convert.ToInt32(TB.Name.ToString().Replace("O", "")) - 1;
                List<DataTable> DTList = MainHMI.OrderInfo(n);
                if (DTList[0] != null)
                    gridOrder.DataContext = DTList[0].DefaultView;
                if (DTList[1] != null)
                {
                    DTList[1].Columns.Remove("OrderID");
                    gridOrder2.DataContext = DTList[1].DefaultView;
                }
                if (DTList[2] != null)
                {
                    DTList[2].Columns.Remove("OrderID");
                    gridOrder3.DataContext = DTList[2].DefaultView;
                }

                Orderinfo.Visibility = Visibility.Visible;
                OrderinfoTitle.Content = "Order Information (" + ZoneID[n] + ")";
                Time.Stop();
                Time.Start();
                Time.Enabled = true;
            }
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    Orderinfo.Visibility = Visibility.Collapsed;
        //    Time.Enabled = false;
        //}

        bool inDrag = false;
        Point anchorPoint;
        private void Orderinfo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            anchorPoint = PointToScreen(e.GetPosition(this));
            inDrag = true;
            Mouse.Capture(((StackPanel)sender));
            e.Handled = true;
        }

        double lengthleft;
        double lengthtop;
        private void Orderinfo_MouseMove(object sender, MouseEventArgs e)
        {
            if (inDrag)
            {
                StackPanel SP = (StackPanel)sender;
                Point currentPoint = PointToScreen(e.GetPosition(this));
                lengthleft = (Canvas.GetLeft(SP) + currentPoint.X - anchorPoint.X);
                lengthleft = Canvas1.ActualWidth - SP.ActualWidth < lengthleft ? Canvas1.ActualWidth - SP.ActualWidth : lengthleft = lengthleft < 0 ? 0 : lengthleft;

                lengthtop = (Canvas.GetTop(SP) + currentPoint.Y - anchorPoint.Y);
                lengthtop = Canvas1.ActualHeight - SP.ActualHeight < lengthtop ? Canvas1.ActualHeight - SP.ActualHeight : lengthtop = lengthtop < 0 ? 0 : lengthtop;

                Canvas.SetLeft(SP, lengthleft);
                Canvas.SetTop(SP, lengthtop);
                anchorPoint = currentPoint;
            }
        }
        private void Orderinfo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (inDrag)
            {
                Mouse.Capture(null);
                inDrag = false;
                e.Handled = true;
            }
        }
        private void Z29_Click(object sender, RoutedEventArgs e)
        {
            ((CheckBox)sender).IsChecked = ((CheckBox)sender).IsChecked == true ? false : true;
        }
        private void help_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Legend.Visibility == Visibility.Visible)
            {
                this.Dispatcher.Invoke(new Action(() => Legend.Visibility = Visibility.Collapsed));
            }
            else
            {
                this.Dispatcher.Invoke(new Action(() => Legend.Visibility = Visibility.Visible));
            }
        }

        private void Menu2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
        }

        private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Orderinfo.Visibility = Visibility.Collapsed;
            Time.Enabled = false;
        }

    }
}
