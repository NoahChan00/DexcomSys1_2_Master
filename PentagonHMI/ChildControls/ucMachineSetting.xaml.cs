using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace PentagonHMI.ChildControls
{
    /// <summary>
    /// Interaction logic for ucMachineSetting.xaml
    /// </summary>
    public partial class ucMachineSetting : UserControl
    {
        //public static readonly RoutedEvent ShowNextEvent = EventManager.RegisterRoutedEvent("ShowNext", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ucMachineSetting));
        public static readonly RoutedEvent ClickAlarmEvent = EventManager.RegisterRoutedEvent("ClickAlarm", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ucMachineSetting));
        public static readonly RoutedEvent ClickAlertEvent = EventManager.RegisterRoutedEvent("ClickAlert", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(ucMachineSetting));


        public ucMachineSetting()
        {
            InitializeComponent();
           
            // Owner = arg_Parent;
        }

        //***************************
        //******Click Alarm*********
        //***************************
        void RaiseClickAlarmEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ucMachineSetting.ClickAlarmEvent);
            RaiseEvent(newEventArgs);
        }
        public event RoutedEventHandler ClickAlarm
        {
            add { AddHandler(ClickAlarmEvent, value); }
            remove { RemoveHandler(ClickAlarmEvent, value); }
        }
        private void Btn_Alarm_Click(object sender, RoutedEventArgs e)
        {
            RaiseClickAlarmEvent();
        }
        //***************************


        //***************************
        //******Click Alert*********
        //***************************   
        void RaiseClickAlertEvent()
        {
            RoutedEventArgs newEventArgs = new RoutedEventArgs(ucMachineSetting.ClickAlertEvent);
            RaiseEvent(newEventArgs);
        }
        public event RoutedEventHandler ClickAlert
        {
            add { AddHandler(ClickAlertEvent, value); }
            remove { RemoveHandler(ClickAlertEvent, value); }
        }
        private void Btn_Alert_Click(object sender, RoutedEventArgs e)
        {
            RaiseClickAlertEvent();
        }
    }
}
