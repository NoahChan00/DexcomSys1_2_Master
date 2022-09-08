using Logix;
using System;
using System.Linq;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for UphView.xaml.
    /// </summary>
    public partial class UphView : UserControl
    {
        #region PrivateFields

        private LogicClasses.Main main = null;
        private readonly Tag uphTag = new Tag { Name = "OEE_Tags.str_Hourly_UPH", DataType = Logix.Tag.ATOMIC.STRING };

        #endregion PrivateFields

        #region PublicFields

        public int UPH { get; set; }
        public int[] PastUPH { get; set; }
        public double[] UPHWidth = new double[4];
        public int PreHrs = -1;
        public const int FixWidth = 300;

        #endregion PublicFields

        #region Constructor

        public UphView(LogicClasses.Main _main)
        {
            try
            {
                InitializeComponent();
                //if (ProjectType.HDD == Classes.GlobalFunctions.ProjectType || ProjectType.DIMM == Classes.GlobalFunctions.ProjectType)
                //    grp_UPH.Header = "Hourly UPH";
                initializeOpc(_main);
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion Constructor

        #region PrivateInitializeMethods

        private void initializeOpc(LogicClasses.Main _main)
        {
            try
            {
                main = _main;
                main.Home_OnUpdate += main_Home_OnUpdate;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateInitializeMethods

        #region PrivateEventMethods

        private void main_Home_OnUpdate()
        {
            try
            {
                int Now = DateTime.Now.Hour;
                string[] Lst_UPH = main.Lst_UPH;

                UPH = Convert.ToInt32(Lst_UPH[Now]);

                PastUPH = new int[] {
                    Convert.ToInt32(Lst_UPH[DateTime.Now.AddHours(-1).Hour]),
                    Convert.ToInt32(Lst_UPH[DateTime.Now.AddHours(-2).Hour]),
                    Convert.ToInt32(Lst_UPH[DateTime.Now.AddHours(-3).Hour])};

                double Max = PastUPH.Max() > UPH ? PastUPH.Max() : UPH;
                int n = 0;
                UPHWidth[n] = (PastUPH[2] / Max);
                n++;
                UPHWidth[n] = (PastUPH[1] / Max);
                n++;
                UPHWidth[n] = (PastUPH[0] / Max);
                n++;
                UPHWidth[n] = (UPH / Max);
                n++;
                if(Now != PreHrs && DateTime.Now.Minute > 1)
                {
                    PreHrs = Now;
                }

                this.Dispatcher.Invoke(new Action(() =>
                {
                    UPHCur.Text = UPH.ToString();
                    UPHRec3.Width = UPHWidth[0] * FixWidth;
                    UPHRec2.Width = UPHWidth[1] * FixWidth;
                    UPHRec1.Width = UPHWidth[2] * FixWidth;
                    UPHRecCur.Width = UPHWidth[3] * FixWidth;
                }));

                if(PastUPH != null && PastUPH.Count() > 1)
                {
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        UPH3.Text = PastUPH[2].ToString();
                        UPH2.Text = PastUPH[1].ToString();
                        UPH1.Text = PastUPH[0].ToString();
                        UPHL1.Content = DateTime.Now.AddHours(-1).ToString("dd MMMM hh:00-hh:59 tt");
                        UPHL2.Content = DateTime.Now.AddHours(-2).ToString("dd MMMM hh:00-hh:59 tt");
                        UPHL3.Content = DateTime.Now.AddHours(-3).ToString("dd MMMM hh:00-hh:59 tt");
                    }));
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateEventMethods
    }
}