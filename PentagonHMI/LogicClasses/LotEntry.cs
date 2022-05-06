using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PentagonHMI.LogicClasses
{
    //class LotEntry : VirtualClient.VirOEE, IDisposable
    class LotEntry :  IDisposable
    {

        #region Variables
        public delegate void onUpdateHandler();
        public event onUpdateHandler OnUpdate;

        LogicClasses.Main _MainConnection;

        #endregion

        #region Constructor
        public void Initialization()
        {
            return;
        }
        public LotEntry(ref LogicClasses.Main MainConnection)
        {
            _MainConnection = MainConnection;
        }

        #endregion

        #region Properties

        TimeSpan _tsMTBF;
        public TimeSpan MTBF
        {
            get
            {
                return _tsMTBF;
            }
            set
            {
                _tsMTBF = value;
            }
        }

        TimeSpan _tsMTBA;
        public TimeSpan MTBA
        {
            get
            {
                return _tsMTBA;
            }
            set
            {
                _tsMTBA = value;
            }
        }

        double _dblAvailability = 0.00;
        public double Availability
        {
            get
            {
                return _dblAvailability;
            }
            set
            {
                _dblAvailability = value;
            }
        }

        double _dblPerformanceRate = 0.00;
        public double PerformanceRate
        {
            get
            {
                return _dblPerformanceRate;
            }
            set
            {
                _dblPerformanceRate = value;
            }
        }

        double _dblEfficiencyRate = 0.00;
        public double EfficiencyRate
        {
            get
            {
                return _dblEfficiencyRate;
            }
            set
            {
                _dblEfficiencyRate = value;
            }
        }

        double _dblUPH = 0;
        public double UPH
        {
            get
            {
                return _dblUPH;
            }
            set
            {
                _dblUPH = value;
            }
        }

        ///*
        // * Date: 2013-02-05
        // * Author: Tammie
        // * Description: Getter/Setter for OEE
        // */
        //double _dblOEE = 0.00;
        //public double calcOEE
        //{
        //    get
        //    {
        //        return _dblOEE;
        //    }
        //    set
        //    {
        //        _dblOEE = value;
        //    }
        //}

        #endregion

        #region Methods

        public void Dispose()
        {
            //if (TcpLotEntry != null)
            //{
            //    TcpLotEntry.OnUpdate -= new onUpdateHandler(TcpLotEntry_OnUpdate);
            //    TcpLotEntry.Dispose();
            // //   Classes.GlobalFunctions.Wait(200);
            //    TcpLotEntry = null;
            //}
        }

        //public override void Initialization()
        //{
        ////    TcpLotEntry.Initialization();
        //    /*
        //     * Date: 2013-02-04
        //     * Author: Jimmy
        //     * Description: Update OEE page on click.
        //     */
            
        //    //TcpOEE_OnUpdate();

        //  //  TcpOEE.Send("OEEP;");
        //}

        public void Send(string Message)
        {
          //  TcpLotEntry.Send(Message);
        }

        public void OPCWrite(string TagName, string value)
        {
         //   TcpLotEntry.OPCWrite(TagName, value);
        }

        #endregion

        #region Events

        void TcpLotEntry_OnUpdate()
        {
            
            //this.TotalFail = Utilities.Converter.ToInt32(TcpLotEntry.TotalFail);
            //this.TotalPass = Utilities.Converter.ToInt32(TcpLotEntry.TotalPass);
            //this.SystemUpTime = Utilities.Converter.ToTimeSpan(TcpLotEntry.SystemUpTime);
            //this.IdealCycleTime = Utilities.Converter.ToTimeSpan(TcpLotEntry.IdealCycleTime);
            //this.IdlingTime = Utilities.Converter.ToTimeSpan(TcpLotEntry.IdlingTime);
            //this.DownTime = Utilities.Converter.ToTimeSpan(TcpLotEntry.DownTime);
            //this.SoftJam = Utilities.Converter.ToInt32(TcpLotEntry.SoftJam);
            //this.HardJam = Utilities.Converter.ToInt32(TcpLotEntry.HardJam);
            //this.OperationTime = Utilities.Converter.ToTimeSpan(TcpLotEntry.OperationTime);
            //this.StartDateTime = Utilities.Converter.ToDateTime(TcpLotEntry.StartDateTime);

            //TotalIn = TotalPass + TotalFail;

            //LotID = TcpLotEntry.LotID;
            //StartLot = TcpLotEntry.StartLot;

            if (OnUpdate != null)
                OnUpdate();
        }

        #endregion

        #region Destructor
        ~LotEntry()
        {
            Dispose();
        }
        #endregion
    }
}
