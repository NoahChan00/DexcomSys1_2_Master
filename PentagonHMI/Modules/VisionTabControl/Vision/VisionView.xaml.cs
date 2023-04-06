using PentagonHMI.Classes;
using PentagonHMI.LogicClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.Integration;
using System.Windows.Media;
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
                if (GlobalFunctions.IsSystem1)
                {
                    VisionGroupBox.Visibility = Visibility.Collapsed;
                    visionInfoBorder.SetValue(Grid.ColumnProperty , 0);
                    BatteryInfoPanel.SetValue(Grid.ColumnProperty , 1);
                    col1.Width = new GridLength(1, GridUnitType.Star);
                    col2.Width = new GridLength(1, GridUnitType.Star);
                    #region Insertion
                    Contentlblcol1.Content = "Insertion Check (Cam 3)";
                    VisionFailInfoStackPanel.Children.Add(new Label() { Content = "Total PCBA Not Found" });
                    VisionFailInfoStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = "VisionFail_Insertion.PCBA_NotFound" } });

                    VisionFailInfoStackPanel.Children.Add(new Label() { Content = "Total Battery Not Found" });
                    VisionFailInfoStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = "VisionFail_Insertion.Battery_NotFound" } });

                    VisionFailInfoStackPanel.Children.Add(new Label() { Content = "Total Battery Insert Fail" });
                    VisionFailInfoStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = "VisionFail_Insertion.Battery_InsertFail" } });

                    VisionFailInfoStackPanel.Children.Add(new Label() { Content = "Total Insertion Yield (%)" });
                    VisionFailInfoStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = "VisionFail_Insertion.Total_Yield" } });

                    //auto deletion function add here
                    //#region Auto Deletion Insertion (Not Completed) 
                    //VisionFailInfoStackPanel.Children.Add(new Label() { Content = "Fail Folder" });
                    //VisionFailInfoStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = "Folder Icon" } });
                    //#endregion

                    #endregion

                    #region Battery
                    Contentlbl.Content = "Battery Check (Cam 2)";
                    System1BatteryStackPanel.Children.Add(new Label() { Content = "Total Battery Present" });
                    System1BatteryStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = "VisionFail_BatteryTray.Total_Present_Qty" } });

                    System1BatteryStackPanel.Children.Add(new Label() { Content = "Total Battery Empty" });
                    System1BatteryStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = "VisionFail_BatteryTray.Total_Empty_Qty" } });

                    System1BatteryStackPanel.Children.Add(new Label() { Content = "Total Battery Undetected" });
                    System1BatteryStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = "VisionFail_BatteryTray.Total_Undetected_Qty" } });

                    System1BatteryStackPanel.Children.Add(new Label() { Content = "Total Battery Yield (%)" });
                    System1BatteryStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = "VisionFail_BatteryTray.Total_Yield" } });

                    //auto deletion function here
                    //#region Auto Deletion Battery (Not Completed) 
                    //System1BatteryStackPanel.Children.Add(new Label() { Content = "Fail Folder" });
                    //System1BatteryStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = "Folder Icon" } });
                    //#endregion

                    #endregion

                }
                else
                {
                    BatteryInfoPanel.Visibility = Visibility.Collapsed;
                    VisionGroupBox.Visibility = Visibility.Visible;
                    visionInfoBorder.SetValue(Grid.ColumnProperty, 1);
                    BatteryInfoPanel.SetValue(Grid.ColumnProperty, 2);
                    col1.Width = GridLength.Auto;
                    col2.Width = new GridLength(1, GridUnitType.Star);
                    var data = Main.SQLer.Exec_DTSelect("SELECT * FROM dbo.VisionFailInfo");
                    foreach (DataRow row in data.Rows)
                    {
                        VisionFailInfoStackPanel.Children.Add(new Label() { Content = row["DisplayName"] });
                        VisionFailInfoStackPanel.Children.Add(new Border() { Child = new TextBlock() { Tag = row["Tagname"] } });
                    }
                }
//#if !DEBUG
                if (!GlobalFunctions.IsSystem1)
                {
                    initializeVision(_main);
                }
                _main.Home_OnUpdate += Vision_OnUpdate;
//#endif
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

#endregion Constructor

        private LogicClasses.Main Main;

        private void Vision_OnUpdate()
        {
            Dispatcher?.Invoke(() =>
            {
                // Only system2 got this page
                if (GlobalFunctions.IsSystem1)
                {
                    foreach (var o in System1BatteryStackPanel.Children)
                    {
                        if (o is Border b && b.Child is TextBlock tb)
                        {
                            tb.Text = Main.OPC.Read<int>((string)tb.Tag).ToString();
                        }
                    }
                    foreach (var o in VisionFailInfoStackPanel.Children)
                    {
                        if(o is Border b && b.Child is TextBlock tb)
                        {
                            tb.Text = Main.OPC.Read<int>((string)tb.Tag).ToString();
                        }
                    }
                }
                else
                {
                    foreach (var o in VisionFailInfoStackPanel.Children)
                    {
                        if(o is Border b && b.Child is TextBlock tb)
                        {
                            tb.Text = Main.OPC.Read<int>((string)tb.Tag).ToString();
                        }
                    }
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
                catch(Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }

        #endregion PrivateInitializeMethods
    }
}