using System;

namespace PentagonHMI.LogicClasses
{
    internal class Motor : IDisposable
    {
        #region Variables

        private LogicClasses.Main _MainConnection;

        public delegate void onUpdateHandler();

        public event onUpdateHandler OnUpdate;

        #endregion Variables

        #region Constructor

        public Motor(ref LogicClasses.Main MainConnection)
        {
            _MainConnection = MainConnection;
            _MainConnection.OnMotorUpdate += new LogicClasses.Main.onMotorHandler(TcpMotor_OnUpdate);
        }

        #endregion Constructor

        #region Methods

        public void Dispose()
        {
            _MainConnection.MotorPageON = false;
        }

        #endregion Methods

        #region Events

        public void TcpMotor_OnUpdate()
        {
            if(OnUpdate != null)
                OnUpdate();
        }

        #endregion Events

        #region Destructor

        ~Motor()
        {
            Dispose();
        }

        #endregion Destructor
    }
}