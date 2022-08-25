using PentagonHMI.Classes;
using System;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for VisionView.xaml.
    /// </summary>
    public partial class VisionView : UserControl
    {
        #region Constructor
        public VisionView(LogicClasses.Main _main)
        {
            try
            {
                Main = _main;
                InitializeComponent();
                initializeVision(_main);
                _main.Home_OnUpdate += Vision_OnUpdate;
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        LogicClasses.Main Main; 
        private void Vision_OnUpdate()
        {
            Dispatcher?.Invoke(() =>
            {
                // Only system2 got this page
                if(!GlobalFunctions.IsSystem1) 
                {
                    ShortShotFailTextBlock.Text = Main.OPC.Read<int>("ShortShot_Fail_Qty").ToString(); 
                    OMFFFailTextBlock.Text = Main.OPC.Read<int>("OMFF_Fail_Qty").ToString(); 
                    BrownStrainFrontFailTextBlock.Text = Main.OPC.Read<int>("BrownStrainF_Fail_Qty").ToString(); 
                    BatteryClipSurfaceFailTextBlock.Text = Main.OPC.Read<int>("BatClipSurface_Fail_Qty").ToString(); 
                    FlashingFailTextBlock.Text = Main.OPC.Read<int>("Flashing_Fail_Qty").ToString(); 
                    FODFailTextBlock.Text = Main.OPC.Read<int>("FOD_Fail_Qty").ToString(); 
                    LiveBugBubbleFailTextBlock.Text = Main.OPC.Read<int>("BugBubble_Fail_Qty").ToString(); 
                    BrownStrainBackFailTextBlock.Text = Main.OPC.Read<int>("BrownStrainB_Fail_Qty").ToString(); 
                    MouseBiteFailTextBlock.Text = Main.OPC.Read<int>("MouseBite_Fail_Qty").ToString(); 
                }
            }); 
        }

        #region PrivateInitializeMethods
        private void initializeVision(LogicClasses.Main _main)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    WindowsFormsHost windowsFormsHost = new WindowsFormsHost
                    {
                        Child = new AxXGXView(_main)
                        //Child = new CvXView()
                    };

                    VisionGroupBox.Children.Add(windowsFormsHost);
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }
        #endregion
    }
}