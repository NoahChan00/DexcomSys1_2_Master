using System;
using System.Windows;
using System.Windows.Controls;
using PentagonHMI.Classes;
using PentagonHMI.Enum;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Windows.Input;
using System.Windows.Threading;
using System.Threading;

namespace PentagonHMI.ChildControls
{

    public partial class ucLotEntry : UserControl, IDisposable
    {
        #region Variables

        OleDbConnection conn;

        DataTable dt;
        DataRow dr;

        int alarm_db_id = 0;

        bool Retest1 = false, Retest2 = false;

        OleDbCommand cmd = new OleDbCommand();

        #endregion

        #region Constructor

        public ucLotEntry(ref LogicClasses.Main MainConnection)
        {
            InitializeComponent();


            conn = Classes.GlobalFunctions.gl_conn;
            LotEntry = new LogicClasses.LotEntry(ref MainConnection);
            LotEntry.OnUpdate += new VirtualClient.VirOEE.onUpdateHandler(LotEntry_OnUpdate);
        
            dt = new DataTable();
            dt.Columns.Add("LogNo", typeof(string));
            dt.Columns.Add("LogLotNumber", typeof(string));

            LotEntry.Initialization();


            try
            {

                LotSummary();

                this.txtLotNumber.Dispatcher.Invoke(new Action(() => this.txtLotNumber.Text = GlobalFunctions.LotID));

                if (GlobalFunctions.StartLot == true)
                {
                    this.btnNewLot.Dispatcher.Invoke(new Action(() => this.btnNewLot.IsEnabled = false));
                }
                else
                {
                    this.btnNewLot.Dispatcher.Invoke(new Action(() => this.btnNewLot.IsEnabled = true));
                }
             
            }
            catch
            {

            }

        }

       

        #endregion

        #region Properties
        private LogicClasses.LotEntry _LotEntry;
        private LogicClasses.LotEntry LotEntry
        {
            get { return _LotEntry; }
            set { _LotEntry = value; }
        }


        #endregion

        #region FormEvent

      

        private void btnNewLot_Click(object sender, RoutedEventArgs e)
        {
            _LotEntry.OPCWrite(Utilities.TagName.LotEntry.LotID, txtLotNumber.Text);
            _LotEntry.OPCWrite(Utilities.TagName.LotEntry.StartLotTime, DateTime.Now.ToString());
            _LotEntry.OPCWrite(Utilities.TagName.LotEntry.StartLot, "True");
             
        }

     

      

     
        #endregion

        #region Events

        void LotEntry_OnUpdate()
        {
            this.txtLotNumber.Dispatcher.Invoke(new Action(() => this.txtLotNumber.Text = LotEntry.LotID));

            if (LotEntry.StartLot == true)
            {
                this.btnNewLot.Dispatcher.Invoke(new Action(() => this.btnNewLot.IsEnabled = false));
            }
            else
            {
                this.btnNewLot.Dispatcher.Invoke(new Action(() => this.btnNewLot.IsEnabled = true));
            }
        }

     

        #endregion

        #region Methods

        public void Dispose()
        {
            //if (LotEntry != null)
            //{
            //    LotEntry.OnOPCUpdate -= new VirtualClient.VirOEE.onOPCUpdateHandler(OEE_OnOPCUpdate);
            //    LotEntry.OnOPC1Update -= new VirtualClient.VirOEE.onOPC1UpdateHandler(LotEntry_OnOPC1Update);
            //    LotEntry.OnOPC2Update -= new VirtualClient.VirOEE.onOPC2UpdateHandler(LotEntry_OnOPC2Update);
            //    LotEntry.OnOPC3Update -= new VirtualClient.VirOEE.onOP3CUpdateHandler(LotEntry_OnOPC3Update);
            //    LotEntry.Dispose();
            //    Classes.GlobalFunctions.Wait(200);
            //    LotEntry = null;
            //}
        }

        public void LotSummary()
        {
            if (conn.State.Equals(ConnectionState.Open))
            {
                conn.Close();
            }

            conn.Open();
            OleDbCommand getLotNumber = new OleDbCommand("SELECT TOP 1 * FROM LotInfo ORDER BY StartDateTime DESC ;", conn);
            OleDbDataReader drgetLotNumber = getLotNumber.ExecuteReader();
            if (drgetLotNumber.Read() == true)
            {

                this.ucStartDateTime.Dispatcher.Invoke(new Action(() => this.ucStartDateTime.ucText = drgetLotNumber["StartDateTime"].ToString()));
                this.ucEndDateTime.Dispatcher.Invoke(new Action(() => this.ucEndDateTime.ucText = drgetLotNumber["EndTime"].ToString()));
                this.ucLotNumber.Dispatcher.Invoke(new Action(() => this.ucLotNumber.ucText = drgetLotNumber["LotNumber"].ToString()));
                this.ucTotalPass.Dispatcher.Invoke(new Action(() => this.ucTotalPass.ucText = drgetLotNumber["TotalPass"].ToString()));
                this.ucTotalFail.Dispatcher.Invoke(new Action(() => this.ucTotalFail.ucText = drgetLotNumber["TotalFail"].ToString()));
                this.ucDowntime.Dispatcher.Invoke(new Action(() => this.ucDowntime.ucText = drgetLotNumber["Downtime"].ToString()));
                this.ucIdleTime.Dispatcher.Invoke(new Action(() => this.ucIdleTime.ucText = drgetLotNumber["IdleTime"].ToString()));
                this.ucOperation.Dispatcher.Invoke(new Action(() => this.ucOperation.ucText = drgetLotNumber["OperationTime"].ToString()));

                int totalcount = Convert.ToInt32(drgetLotNumber["TotalPass"]) + Convert.ToInt32(drgetLotNumber["TotalFail"]);
                this.ucTotalCount.Dispatcher.Invoke(new Action(() => this.ucTotalCount.ucText = totalcount.ToString()));


            }


            getLotNumber = new OleDbCommand("SELECT LotNumber FROM LotInfo ORDER BY StartDateTime DESC ;", conn);
            drgetLotNumber = getLotNumber.ExecuteReader();
            int counter = 1;
            while (drgetLotNumber.Read() == true)
            {
                dr = dt.NewRow();
                dr["LogNo"] = counter.ToString();
                dr["LogLotNumber"] = drgetLotNumber["LotNumber"].ToString();
                dt.Rows.Add(dr);
                counter++;
            }
            conn.Close();

            dgLog.ItemsSource = dt.DefaultView;
        }



       
        #endregion

        #region Destructor

        ~ucLotEntry()
        {
            Dispose();
        }
        #endregion

        private void dgLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            object item = dgLog.SelectedItem;

            if (!string.IsNullOrWhiteSpace((dgLog.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text))
               alarm_db_id = Convert.ToInt32((dgLog.SelectedCells[1].Column.GetCellContent(item) as TextBlock).Text);

            
            if (conn.State.Equals(ConnectionState.Open))
            {
                conn.Close();
            }

            conn.Open();
            OleDbCommand getLotNumber = new OleDbCommand("SELECT * FROM LotInfo WHERE LotNumber ='" + alarm_db_id.ToString() + "' ;", conn);
            OleDbDataReader drgetLotNumber = getLotNumber.ExecuteReader();
            if (drgetLotNumber.Read() == true)
            {
                this.ucStartDateTime.Dispatcher.Invoke(new Action(() => this.ucStartDateTime.ucText = drgetLotNumber["StartDateTime"].ToString()));
                this.ucEndDateTime.Dispatcher.Invoke(new Action(() => this.ucEndDateTime.ucText = drgetLotNumber["EndTime"].ToString()));
                this.ucLotNumber.Dispatcher.Invoke(new Action(() => this.ucLotNumber.ucText = drgetLotNumber["LotNumber"].ToString()));
                this.ucTotalPass.Dispatcher.Invoke(new Action(() => this.ucTotalPass.ucText = drgetLotNumber["TotalPass"].ToString()));
                this.ucTotalFail.Dispatcher.Invoke(new Action(() => this.ucTotalFail.ucText = drgetLotNumber["TotalFail"].ToString()));
                this.ucDowntime.Dispatcher.Invoke(new Action(() => this.ucDowntime.ucText = drgetLotNumber["Downtime"].ToString()));
                this.ucIdleTime.Dispatcher.Invoke(new Action(() => this.ucIdleTime.ucText = drgetLotNumber["IdleTime"].ToString()));
                this.ucOperation.Dispatcher.Invoke(new Action(() => this.ucOperation.ucText = drgetLotNumber["OperationTime"].ToString()));

                int totalcount = Convert.ToInt32(drgetLotNumber["TotalPass"]) + Convert.ToInt32(drgetLotNumber["TotalFail"]);
                this.ucTotalCount.Dispatcher.Invoke(new Action(() => this.ucTotalCount.ucText = totalcount.ToString()));
            }
        }

   

        
        

     

     
       

  
        

      
      













    }
}
