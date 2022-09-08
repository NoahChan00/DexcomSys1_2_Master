using System;
using System.Collections.Generic;

namespace PentagonHMI.LogicClasses
{
    //public class Home : VirtualClient.VirHome, IDisposable
    public class Home : IDisposable
    {
        #region Variables

        // TCPCommunicator.TCPHome TcpHome;
        private LogicClasses.Main _MainConnection;

        public delegate void onUpdateHandler();

        public event onUpdateHandler OnUpdate;

        #endregion Variables

        #region Constructor

        public Home(ref LogicClasses.Main MainConnection)
        {
            _MainConnection = MainConnection;
            _MainConnection.Home_OnUpdate += new LogicClasses.Main.onHomeUpdateHandler(TcpHome_OnUpdate);
            _MainConnection.HomePageON = true;
            Initialization();
        }

        #endregion Constructor

        #region Property

        public long UPH
        {
            get;
            set;
        }

        public double Yield
        {
            get;
            set;
        }

        public long TotalCount
        {
            get;
            set;
        }

        public long TotalPass
        {
            get;
            set;
        }

        public long TotalFailed
        {
            get;
            set;
        }

        public int HardJam
        {
            get;
            set;
        }

        public virtual List<KeyValuePair<string, long>> UPHItems
        {
            get;
            set;
        }

        #endregion Property

        #region Methods

        public void Dispose()
        {
            _MainConnection.Home_OnUpdate -= new LogicClasses.Main.onHomeUpdateHandler(TcpHome_OnUpdate);
            _MainConnection.HomePageON = false;
            //if (TcpHome != null)
            //{
            //    //TcpHome.Send("PGHM;S0");
            //    TcpHome.OnUpdate -= new onUpdateHandler(TcpHome_OnUpdate);
            //    TcpHome.Dispose();
            //    //Classes.GlobalFunctions.Wait(200);
            //    TcpHome = null;
            //}
        }

        public void Initialization()
        {
            //  TcpHome.Initialization();
        }

        public void Send(string Message)
        {
            //  TcpHome.Send(Message);
        }

        public void OPCWrite(string TagName, string value)
        {
            //  TcpHome.OPCWrite(TagName, value);
        }

        #endregion Methods

        #region Events

        private void TcpHome_OnUpdate()
        {
            //LEK 20180403 GEt HOME Info

            //this.UPH = TcpHome.UPH;
            //this.TotalCount = TcpHome.TotalCount;
            //this.TotalFailed = TcpHome.TotalFailed;
            //this.TotalPass = TcpHome.TotalPass;
            //this.UPHItems = TcpHome.UPHItems;
            //this.TotalCount = TotalPass + TotalFailed;
            //this.Machine_Recipe = TcpHome.Machine_Recipe;
            //this.setRecipe = TcpHome.setRecipe;
            //this.OneCycleMode = TcpHome.OneCycleMode;
            //this.Result1 = TcpHome.Result1;
            //this.Result2 = TcpHome.Result2;
            //this.Result3 = TcpHome.Result3;
            //this.Result4 = TcpHome.Result4;
            //this.Result5 = TcpHome.Result5;
            //this.Result6 = TcpHome.Result6;
            //this.Result7 = TcpHome.Result7;
            //this.Result8 = TcpHome.Result8;
            //this.Result9 = TcpHome.Result9;
            //this.Result10 = TcpHome.Result10;
            //this.Result11 = TcpHome.Result11;

            if(TotalPass > 0)
            {
                Yield = (Convert.ToDouble(TotalPass) / Convert.ToDouble(TotalCount)) * 100;
            }

            if(OnUpdate != null)
                OnUpdate();
        }

        #endregion Events

        #region Destructor

        ~Home()
        {
            //Dispose();
        }

        #endregion Destructor
    }
}