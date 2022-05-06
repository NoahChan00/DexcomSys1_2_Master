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
using System.Threading.Tasks;

namespace PentagonHMI.ChildControls
{
    public partial class ucDUT : UserControl, IDisposable
    {
        SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc();
        SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName);
        LogicClasses.Main _Main;

        ObservableCollection<DUTModel> DUTs = new ObservableCollection<DUTModel>();
        Dictionary<int, Brush> Dic_ResultColor = new Dictionary<int, Brush>();

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
            DataTable dtDUTLegends = SQLer.Exec_DTSelect("Select * From DUTLegends");
            foreach (DataRow dr in dtDUTLegends.Rows)
            {
                Dic_ResultColor.Add(Convert.ToInt32(dr["Result"]), (Brush)bc.ConvertFromString(dr["Color"].ToString()));
                Legends.Children.Add(new TextBlock
                {
                    FontSize = 15,
                    Text = dr["Result"].ToString() + " " + dr["Description"].ToString(),
                    Background = Dic_ResultColor[Convert.ToInt32(dr["Result"])],
                    Padding = new Thickness(15, 2, 15, 2),
                    Margin = new Thickness(2)
                });
            }

            foreach (DataRow dr in dtDUT.Rows)
            {
                int Num = Convert.ToInt32(dr["Length"]);
                DUTModel DTM = new DUTModel
                {
                    Name = dr["Name"].ToString(),
                    Length = Num,
                    TagBarcode = dr["Barcode"].ToString(),
                    TagStatus = dr["Status"].ToString(),
                    TagVirtual = dr["Virtual"].ToString()
                };

                for (int num = 0; num < Num; num++)
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
                        var VirtualIDs = OPCore.Read<string[]>(DUT.TagVirtual, typeof(string), DUT.Length);
                        var Barcodes = OPCore.Read<string[]>(DUT.TagBarcode, typeof(string), DUT.Length);
                        var Statuses = OPCore.Read<int[]>(DUT.TagStatus, typeof(int), DUT.Length);

                        for (int i = 0; i < DUT.Sockets.Count; i++)
                        {
                            DUT.Sockets[i].Barcode = Barcodes[i];
                            DUT.Sockets[i].VirtualID = VirtualIDs[i];
                            DUT.Sockets[i].Background = Dic_ResultColor[Statuses[i]];
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

        public string TagStatus { get; set; }
        public string TagBarcode { get; set; }
        public string TagVirtual { get; set; }
        public int Length { get; set; }


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

        private string barcode = "Barcode";
        public string Barcode
        {
            get { return barcode; }
            set
            {
                if (barcode != value)
                {
                    barcode = value;
                    RaisePropertyChanged(nameof(Barcode));
                }
            }
        }

        private string virtualID = "VirtualID";
        public string VirtualID
        {
            get { return virtualID; }
            set
            {
                if (virtualID != value)
                {
                    virtualID = value;
                    RaisePropertyChanged(nameof(VirtualID));
                }
            }
        }
    }

}
