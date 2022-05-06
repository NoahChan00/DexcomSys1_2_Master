using System;

namespace PentagonHMI.LogicClasses
{
    class Motor : IDisposable
    {

        #region Variables
        LogicClasses.Main _MainConnection;
        public delegate void onUpdateHandler();
        public event onUpdateHandler OnUpdate;
        #endregion
        #region Constructor

        public Motor(ref LogicClasses.Main MainConnection)
        {
            _MainConnection = MainConnection;
            _MainConnection.OnMotorUpdate += new LogicClasses.Main.onMotorHandler(TcpMotor_OnUpdate);
        }

        #endregion
        #region Methods

        public void Dispose()
        {
            _MainConnection.MotorPageON = false;
        }
        #endregion
        #region Events
        public void TcpMotor_OnUpdate()
        {
            if (OnUpdate != null)
                OnUpdate();
        }
        #endregion

        #region Destructor
        ~Motor()
        {
            Dispose();
        }
        #endregion
    }
}
