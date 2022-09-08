using Library;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows.Controls;

namespace PentagonHMI.UserControls
{
    public partial class UCAlarmTable : UserControl, INotifyPropertyChanged
    {
        public UCAlarmTable()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void NotifyChange(PropertyChangedEventArgs e)
        {
            if(PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Update(DataTable DT)
        {
            if(DT == null)
                return;

            ObservableCollection<Bar> Alert = new ObservableCollection<Bar>();
            foreach(DataRow dr in DT.Rows)
            {
                Bar bar = new Bar();
                bar.ErrorCode = dr["msgErrorCode"].ToString();
                bar.Action = dr["msgAction"].ToString();
                bar.Module = dr["msgModule"].ToString();
                bar.DateTime = dr["msgDateTime"].ToString();
                bar.Message = dr["msgError"].ToString();
                //bar.ErrorCode = dr["ErrorCode"].ToString();
                //bar.Action = dr["Action"].ToString();
                //bar.Module = dr["Module"].ToString();
                //bar.DateTime = dr["DateTime"].ToString();
                //bar.Message = dr["Message"].ToString();

                Alert.Add(bar);
            }

            AlarmCollection = Alert;
        }

        public class Bar : INotifyPropertyChangedBase
        {
            public string ErrorCode { get; set; } = "N/A";
            public string Message { get; set; } = "N/A";
            public string Action { get; set; } = "N/A";
            public string Module { get; set; } = "N/A";
            public string DateTime { get; set; } = "N/A";
        }

        private ObservableCollection<Bar> _AlarmCollection = new ObservableCollection<Bar>();

        public ObservableCollection<Bar> AlarmCollection
        {
            get { return _AlarmCollection; }
            set { _AlarmCollection = value; NotifyChange(new PropertyChangedEventArgs("AlarmCollection")); }
        }
    }
}