using System;
using System.Windows.Controls;
using PentagonHMI.UserControls;

namespace PentagonHMI.ChildControls
{
    public partial class ucIOLocation : UserControl, IDisposable
    {
        string StationID;
        LogicClasses.Main _Main;
        private LogicClasses.IOInput IOInput { get; set; }
        private UCIOLocMain LocMain { get; set; }
        public delegate void onUpdateHandler();
        public event onUpdateHandler OnUpdate;      

        #region Constructor
        public ucIOLocation(ref LogicClasses.Main Main, ref LogicClasses.IOInput _IOInput)
        {
            InitializeComponent();
            StationID = Main.StationID;
            ShowTabItem();
            _Main = Main;
            _Main.OnIOUpdate += new LogicClasses.Main.onIOUpdateHandler(IOInput_OnUpdate);
            IOInput = _IOInput;
        }
        #endregion

        #region FormEvents
        private void ShowTabItem()
        {
            switch (StationID)
            {
                case "0":
                    LocMain = new UCIOLocMain();
                    IOLocation.Children.Add(LocMain);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Events
        void IOInput_OnUpdate()
        {
            try
            {
                LocMain.Update(_Main.IValue,_Main.OValue);
                if (OnUpdate != null)
                    OnUpdate();
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, "IOInput_OnUpdate()");
            }
        }
        #endregion

        #region Methods
        private void IO_Update(UCCheckboxLabelType2 cbl, bool? io)
        {
            if (!cbl.Dispatcher.CheckAccess())
            {
                if (io == true)
                    cbl.Dispatcher.Invoke(new Action(() => cbl.ucIsCheck = true));
                else
                    cbl.Dispatcher.Invoke(new Action(() => cbl.ucIsCheck = false));
            }
            else
            {
                if (io == true)
                    cbl.ucIsCheck = true;
                else
                    cbl.ucIsCheck = false;
            }
        }
        #endregion

        #region Destructor
        ~ucIOLocation()
        {
            Dispose();
        }
        public void Dispose()
        {
            _Main.IOLocPageON = false;
        }
        #endregion
    }
}
