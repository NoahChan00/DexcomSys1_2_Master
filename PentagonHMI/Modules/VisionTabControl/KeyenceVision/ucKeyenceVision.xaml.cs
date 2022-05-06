using Keyence.IV.Sdk;
using System;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Forms.Integration;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for ucKeyenceVision.xaml
    /// </summary>
    public partial class ucKeyenceVision : UserControl
    {
        private LogicClasses.Main _Main;
        private IVisionSensor sensor;
        private VisionSensorStore store = new VisionSensorStore();
        private Keyence.IV.Sdk.Sample_CSharp.Parts.ImageDisplayControl imageDisplayControl = new Keyence.IV.Sdk.Sample_CSharp.Parts.ImageDisplayControl();
        Timer timer = new Timer();

        public ucKeyenceVision(LogicClasses.Main _main)
        {
            InitializeComponent();
            timer.Interval = 20;
            timer.Elapsed += TimerTick;
            _Main = _main;
            initializeVision();   
        }

        public void initializeVision()
        {
            // 193.168.3.21  8500
            //VisionSensorStore.StartPoint = System.Net.IPAddress.Parse("193.168.3.99");
            //sensor = store.Create(System.Net.IPAddress.Parse("193.168.3.25"), 63000);
            VisionSensorStore.StartPoint = System.Net.IPAddress.Parse("193.168.3.21");
            sensor = store.Create(System.Net.IPAddress.Parse("193.168.3.21"), 8500);
            timer.Start();

            sensor.EventEnable = true;
            imageDisplayControl.RefreshState();
            //sensor.Trigger();

            imageDisplayControl.Initialize(sensor);
            
            WindowsFormsHost Host = new WindowsFormsHost
            {               
                Child = imageDisplayControl
            };
            VisionContent.Children.Add(Host);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (sensor == null)
            {
                return;
            }
            try
            {
                sensor.TickTack();
            }
            catch (ConnectionLostException)
            {
                sensor.Dispose();
                sensor = null;
                timer.Stop();
            }
        }
    }
}
