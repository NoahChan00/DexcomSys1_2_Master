using System;
using System.Windows.Media;
using System.Windows.Controls;
using Utilities;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls.Primitives;
using SimpleDatabase;
using System.Data;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using System.Reflection;
using System.Threading.Tasks;

namespace PentagonHMI.ChildControls
{
    public partial class ucDUT : UserControl, IDisposable
    {
        SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc();
        SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);
        LogicClasses.Main _Main;

        ObservableCollection<DUTModel> DUTs = new ObservableCollection<DUTModel>();
        Dictionary<int, Brush> Dic_ResultColor = new Dictionary<int, Brush>();

        //General
        const string DUT = "DUT";

        public ucDUT(LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;
#if !DEBUG
            if (!OPCore.Connect(Info.OPC.IP)) return;
#endif
            Initialize();
            main.OnDUTUpdate += DUTUpdate;
        }

        private void Initialize()
        {
            ItmC_Sockets.ItemsSource = DUTs;

            DataTable dtDUT = SQLer.Exec_DTSelect("Select * From DUT");
            BrushConverter bc = new BrushConverter();
            //DataTable dtDUTLegends = SQLer.Exec_DTSelect("Select * From DUTLegends");
            //foreach (DataRow dr in dtDUTLegends.Rows)
            //{
            //    Dic_ResultColor.Add(Convert.ToInt32(dr["Result"]), (Brush)bc.ConvertFromString(dr["Color"].ToString()));
            //    Legends.Children.Add(new TextBlock
            //    {
            //        FontSize = 15,
            //        Text = dr["Result"].ToString() + " " + dr["Description"].ToString(),
            //        Background = Dic_ResultColor[Convert.ToInt32(dr["Result"])],
            //        Padding = new Thickness(15, 2, 15, 2),
            //        Margin = new Thickness(2)
            //    });
            //}

            foreach (DataRow dr in dtDUT.Rows)
            {
                //int Num = Convert.ToInt32(dr["Length"]);
                DUTModel DTM = new DUTModel
                {
                    Name = dr["Name"].ToString(),
                    //Length = Num,
                    //TagTotalPass = Convert.ToInt32(dr["TotalPass"]),
                    TagTotalPass = dr["TotalPass"].ToString(),
                    TagTotalFail = dr["TotalFail"].ToString(),
                    TagYield = dr["Yield"].ToString(),
                    TagSocketDisable = dr["Socket_Disable"].ToString() 
                    //TagStatus = dr["Status"].ToString()
                };

                //for (int num = 0; num < Num; num++)
                //    DTM.Sockets.Add(new SubDUTModel());
                DTM.Sockets.Add(new SubDUTModel());                
                DUTs.Add(DTM);
            }
        }

        private void DUTUpdate()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {

                    foreach (var DUT in DUTs)
                    {
#if !DEBUG
                        var TotalPasses = OPCore.Read<int>(DUT.TagTotalPass, typeof(int));
                        var TotalFails = OPCore.Read<int>(DUT.TagTotalFail, typeof(int));
                        var Yields = OPCore.Read<int>(DUT.TagYield, typeof(int));
                        var SocketsDisable = OPCore.Read<bool>(DUT.TagSocketDisable, typeof(bool));
                        //var Statuses = OPCore.Read<int[]>(DUT.TagStatus, typeof(int), DUT.Length);
#else                   
                        var TotalPasses = new string[] { "2", "4", "6", "8", "10","12", "14", "16" };
                        var TotalFails = new string[] { "1", "3", "5", "7", "9", "11", "13", "15" };
                        var Yields = new string[] { "22", "44", "66", "88", "11", "33", "55", "77" };
#endif
                        for (int i = 0; i < DUT.Sockets.Count; i++)
                        {
                            DUT.Sockets[i].TotalPass = TotalPasses[i];
                            DUT.Sockets[i].TotalFail = TotalFails[i];
                            DUT.Sockets[i].Yield = Yields[i];
                            //DUT.Sockets[i].Background = Dic_ResultColor[Statuses[i]];
                        }
                    }
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            });
        }

        ~ucDUT()
        {
            Dispose();
        }

        public void Dispose()
        {
            _Main.DUTPageOn = false;
        }

        private void socketToggle_Click(object sender, RoutedEventArgs e)
        {
            FileLogger.logButton(DUT, "Socket Disable", MethodBase.GetCurrentMethod().ToString());
            //OPCore.Read(TagSocketDisable, false);
        }
    }

    public class DUTModel : ViewModelBase
    {
        private string name = string.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }

        //public string TagStatus { get; set; }
        //public int Length { get; set; }
        public string TagTotalPass { get; set; }
        public string TagTotalFail { get; set; }
        public string TagYield { get; set; }
        public string TagSocketDisable { get; set; }


        private ObservableCollection<SubDUTModel> sockets = new ObservableCollection<SubDUTModel>();
        public ObservableCollection<SubDUTModel> Sockets
        {
            get { return sockets; }
            set
            {
                if (sockets != value)
                {
                    sockets = value;
                    RaisePropertyChanged(nameof(Sockets));
                }
            }
        }
    }

    public class SubDUTModel : ViewModelBase
    {
        private Brush background = Brushes.Gray;
        public Brush Background
        {
            get { return background; }
            set
            {
                if (background != value)
                {
                    background = value;
                    RaisePropertyChanged(nameof(Background));
                }
            }
        }

        private string totalPass = "TotalPass";
        public string TotalPass
        {
            get { return totalPass; }
            set
            {
                if (totalPass != value)
                {
                    totalPass = value;
                    RaisePropertyChanged(nameof(TotalPass));
                    //RaisePropertyChanged(nameof(Yield));
                }
            }
        }

        private string totalFail = "TotalFail";
        public string TotalFail
        {
            get { return totalFail; }
            set
            {
                if (totalFail != value)
                {
                    totalFail = value;
                    RaisePropertyChanged(nameof(TotalFail));
                    //RaisePropertyChanged(nameof(Yield));
                }
            }
        }

        private string yield = "Yield";
        public string Yield
        {
            get { return yield; }

            set
            {
                //int x = 100;
                //var p = Convert.ToInt32(TotalPass);
                //var f = Convert.ToInt32(TotalFail);

                //var result = (Convert.ToDouble(TotalPass) / Convert.ToDouble(TotalFail)) * x;
                //RaisePropertyChanged(nameof(result));
                if (Yield != value)
                {
                    yield = value + "%";
                    RaisePropertyChanged(nameof(Yield));
                }
            }
        }

        private bool socketDisable = false;
        public bool SocketDisable
        {
            get { return socketDisable; }

            set
            {
                if (SocketDisable != value)
                {
                    socketDisable = value;
                    RaisePropertyChanged(nameof(SocketDisable));
                }
            }
        }
    }
}