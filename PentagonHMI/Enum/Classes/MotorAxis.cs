using Library;
using System.Windows;

namespace PentamasterHMI
{
    public class MotorAxis : INotifyPropertyChangedBase
    {
        private Visibility axisVisibility = Visibility.Visible;
        public Visibility AxisVisibility
        {
            get { return axisVisibility; }
            set { axisVisibility = value; OnPropertyChanged("AxisVisibility"); }
        }
        
        private string axisDescription = "Axis";
        public string AxisDescription
        {
            get { return axisDescription; }
            set { axisDescription = value; OnPropertyChanged("AxisDescription"); }
        }

        private string axisSavePoint1 = "Save Point 1";
        public string AxisSavePoint1
        {
            get { return axisSavePoint1; }
            set { axisSavePoint1 = value; OnPropertyChanged("AxisSavePoint1"); }
        }

        private string axisSavePoint2 = "Save Point 2";
        public string AxisSavePoint2
        {
            get { return axisSavePoint2; }
            set { axisSavePoint2 = value; OnPropertyChanged("AxisSavePoint2"); }
        }

        private string axisSavePoint3 = "Save Point 3";
        public string AxisSavePoint3
        {
            get { return axisSavePoint3; }
            set { axisSavePoint3 = value; OnPropertyChanged("AxisSavePoint3"); }
        }

        private string axisSavePoint4 = "Save Point 4";
        public string AxisSavePoint4
        {
            get { return axisSavePoint4; }
            set { axisSavePoint4 = value; OnPropertyChanged("AxisSavePoint4"); }
        }

        private string axisSavePoint5 = "Save Point 5";
        public string AxisSavePoint5
        {
            get { return axisSavePoint5; }
            set { axisSavePoint5 = value; OnPropertyChanged("AxisSavePoint5"); }
        }

        private string axisSavePoint6 = "Save Point 6";
        public string AxisSavePoint6
        {
            get { return axisSavePoint6; }
            set { axisSavePoint6 = value; OnPropertyChanged("AxisSavePoint6"); }
        }

        private string axisSavePoint7 = "Save Point 7";
        public string AxisSavePoint7
        {
            get { return axisSavePoint7; }
            set { axisSavePoint7 = value; OnPropertyChanged("AxisSavePoint7"); }
        }

        private string axisSavePoint8 = "Save Point 8";
        public string AxisSavePoint8
        {
            get { return axisSavePoint8; }
            set { axisSavePoint8 = value; OnPropertyChanged("AxisSavePoint8"); }
        }
        private string axisSavePoint9 = "Save Point 9";
        public string AxisSavePoint9
        {
            get { return axisSavePoint9; }
            set { axisSavePoint9 = value; OnPropertyChanged("AxisSavePoint9"); }
        }

        private string axisSavePoint10 = "Save Point 10";
        public string AxisSavePoint10
        {
            get { return axisSavePoint10; }
            set { axisSavePoint10 = value; OnPropertyChanged("AxisSavePoint10"); }
        }

        private string axisSavePoint11 = "Save Point 11";
        public string AxisSavePoint11
        {
            get { return axisSavePoint11; }
            set { axisSavePoint11 = value; OnPropertyChanged("AxisSavePoint11"); }
        }

        private string axisSavePoint12 = "Save Point 12";
        public string AxisSavePoint12
        {
            get { return axisSavePoint12; }
            set { axisSavePoint12 = value; OnPropertyChanged("AxisSavePoint12"); }
        }

        private string axisSavePoint13 = "Save Point 13";
        public string AxisSavePoint13
        {
            get { return axisSavePoint13; }
            set { axisSavePoint13 = value; OnPropertyChanged("AxisSavePoint13"); }
        }

        private string axisSavePoint14 = "Save Point 14";
        public string AxisSavePoint14
        {
            get { return axisSavePoint14; }
            set { axisSavePoint14 = value; OnPropertyChanged("AxisSavePoint14"); }
        }

        private string axisSavePoint15 = "Save Point 15";
        public string AxisSavePoint15
        {
            get { return axisSavePoint15; }
            set { axisSavePoint15 = value; OnPropertyChanged("AxisSavePoint15"); }
        }

        private string axisSavePoint16 = "Save Point 16";
        public string AxisSavePoint16
        {
            get { return axisSavePoint16; }
            set { axisSavePoint16 = value; OnPropertyChanged("AxisSavePoint16"); }
        }

        private string axisSavePoint17 = "Save Point 17";
        public string AxisSavePoint17
        {
            get { return axisSavePoint17; }
            set { axisSavePoint17 = value; OnPropertyChanged("AxisSavePoint17"); }
        }

        private string axisSavePoint18 = "Save Point 18";
        public string AxisSavePoint18
        {
            get { return axisSavePoint18; }
            set { axisSavePoint18 = value; OnPropertyChanged("AxisSavePoint18"); }
        }

        private string axisSavePoint19 = "Save Point 19";
        public string AxisSavePoint19
        {
            get { return axisSavePoint19; }
            set { axisSavePoint19 = value; OnPropertyChanged("AxisSavePoint19"); }
        }

        private string axisSavePoint20 = "Save Point 20";
        public string AxisSavePoint20
        {
            get { return axisSavePoint20; }
            set { axisSavePoint20 = value; OnPropertyChanged("AxisSavePoint20"); }
        }

        private string axisSavePoint21 = "Save Point 21";
        public string AxisSavePoint21
        {
            get { return axisSavePoint21; }
            set { axisSavePoint21 = value; OnPropertyChanged("AxisSavePoint21"); }
        }

        private string axisSavePoint22 = "Save Point 22";
        public string AxisSavePoint22
        {
            get { return axisSavePoint22; }
            set { axisSavePoint22 = value; OnPropertyChanged("AxisSavePoint22"); }
        }

        private string axisSavePoint23 = "Save Point 23";
        public string AxisSavePoint23
        {
            get { return axisSavePoint23; }
            set { axisSavePoint23 = value; OnPropertyChanged("AxisSavePoint23"); }
        }

        private string axisSavePoint24 = "Save Point 24";
        public string AxisSavePoint24
        {
            get { return axisSavePoint24; }
            set { axisSavePoint24 = value; OnPropertyChanged("AxisSavePoint24"); }
        }

        private string axisSavePoint25 = "Save Point 25";
        public string AxisSavePoint25
        {
            get { return axisSavePoint25; }
            set { axisSavePoint25 = value; OnPropertyChanged("AxisSavePoint25"); }
        }

        private string axisSavePoint26 = "Save Point 26";
        public string AxisSavePoint26
        {
            get { return axisSavePoint26; }
            set { axisSavePoint26 = value; OnPropertyChanged("AxisSavePoint26"); }
        }

        private string axisSavePoint27 = "Save Point 27";
        public string AxisSavePoint27
        {
            get { return axisSavePoint27; }
            set { axisSavePoint27 = value; OnPropertyChanged("AxisSavePoint27"); }
        }

        private string axisSavePoint28 = "Save Point 28";
        public string AxisSavePoint28
        {
            get { return axisSavePoint28; }
            set { axisSavePoint28 = value; OnPropertyChanged("AxisSavePoint28"); }
        }

        private string axisSavePoint29 = "Save Point 29";
        public string AxisSavePoint29
        {
            get { return axisSavePoint29; }
            set { axisSavePoint29 = value; OnPropertyChanged("AxisSavePoint29"); }
        }

        private string axisSavePoint30 = "Save Point 30";
        public string AxisSavePoint30
        {
            get { return axisSavePoint30; }
            set { axisSavePoint30 = value; OnPropertyChanged("AxisSavePoint30"); }
        }

        private string axisSavePoint31 = "Save Point 31";
        public string AxisSavePoint31
        {
            get { return axisSavePoint31; }
            set { axisSavePoint31 = value; OnPropertyChanged("AxisSavePoint31"); }
        }

        private string axisSavePoint32 = "Save Point 32";
        public string AxisSavePoint32
        {
            get { return axisSavePoint32; }
            set { axisSavePoint32 = value; OnPropertyChanged("AxisSavePoint32"); }
        }
    }
}