using GalaSoft.MvvmLight.CommandWpf;
using PentagonHMI.Classes;
using SimpleDatabase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Utilities;

namespace PentagonHMI.ChildControls
{
    public partial class ucStacker : UserControl, IDisposable
    {
        private SimpleOPC.INGEAR_Opc OPCore = new SimpleOPC.INGEAR_Opc(Info.OPC.IP);
        private SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);


        private LogicClasses.Main _Main;

        // System2 Tag
        const string TagLeftDestacker = "LDestacker_Slot_Tracking[1]";
        const string TagRightDestacker = "RDestacker_Slot_Tracking[1]";
        const string TagStacker = "Stacker_Slot_Tracking[1]";

        // System so far only 1 dimension
        private int Row;
        private int Column;

        private Dictionary<string, Brush> Dic_ResultColor = new Dictionary<string, Brush>();
        private Dictionary<int, string> Dic_TrayMapSize = new Dictionary<int, string>();

        public ucStacker(LogicClasses.Main main)
        {
            InitializeComponent();
            _Main = main;
#if !DEBUG
            if (!OPCore.Connect(Info.OPC.IP))
                return;
#endif
            Initialize();
            main.OnStackerUpdate += StackerUpdate;
        }
        private void Initialize()
        {
            if (GlobalFunctions.IsSystem1)
            {
                //ScrollViewerSystem1.Visibility = Visibility.Visible;
                ScrollViewerSystem2.Visibility = Visibility.Collapsed;
            }
            else
            {
                //ScrollViewerSystem1.Visibility = Visibility.Collapsed;
                ScrollViewerSystem2.Visibility = Visibility.Visible;
            }

            Row = 16;
            Column = 1;

            BrushConverter bc = new BrushConverter();
            DataTable dt = SQLer.Exec_DTSelect("SELECT * FROM TrayMapColor");
            foreach (DataRow dr in dt.Rows)
            {
                //dr["result"] using nchar instead of varchar, trailing white space x 9
                Dic_ResultColor.Add(dr["result"].ToString().Trim(), (Brush)bc.ConvertFromString(dr["color"].ToString()));
                Legends.Children.Add(new Button
                {
                    Background = Dic_ResultColor[dr["result"].ToString().Trim()],
                    BorderBrush = Brushes.Black,
                    Content = dr["result"].ToString().Trim() + ": " + dr["description"].ToString(),
                    FontSize = 15,
                    FontWeight = FontWeights.Bold,
                    Height = 40
                });
            }

            DataTable datatable = SQLer.Exec_DTSelect("SELECT * FROM TrayMap");

            if (GlobalFunctions.IsSystem1)
            {
            }
            else
            {
                ugrd_LeftDestacker.Rows = Row;
                ugrd_LeftDestacker.Columns = Column;

                ugrd_RightDestacker.Rows = Row;
                ugrd_RightDestacker.Columns = Column;

                ugrd_Stacker.Rows = Row;
                ugrd_Stacker.Columns = Column;
            }

            if (GlobalFunctions.IsSystem1)
            {
            }
            else
            {
                int total = Row * Column;
                for (int i = 1; i <= total; i++)
                {
                    ugrd_LeftDestacker.Children.Insert(0, new Button
                    {
                        Tag = TagLeftDestacker.Replace("1", i.ToString()),
                        Width = 150,
                        Height = 14,
                        Margin = new Thickness(1),
                        Command = new RelayCommand<string>(ButtonToggleSlotCommand),
                        CommandParameter = TagLeftDestacker.Replace("1", i.ToString())
                    });
                    ugrd_RightDestacker.Children.Insert(0, new Button
                    {
                        Tag = TagRightDestacker.Replace("1", i.ToString()),
                        Width = 150,
                        Height = 14,
                        Margin = new Thickness(1),
                        Command = new RelayCommand<string>(ButtonToggleSlotCommand),
                        CommandParameter = TagRightDestacker.Replace("1", i.ToString())
                    });
                    ugrd_Stacker.Children.Insert(0, new Button
                    {
                        Tag = TagStacker.Replace("1", i.ToString()),
                        Width = 150,
                        Height = 14,
                        Margin = new Thickness(1),
                        Command = new RelayCommand<string>(ButtonToggleSlotCommand),
                        CommandParameter = TagStacker.Replace("1", i.ToString())
                    });

                }
            }
        }

        private void StackerUpdate()
        {
            Dispatcher.Invoke(() =>
            {
                try
                {
                    short[] LeftDestacker = default;
                    short[] RighDestacker = default;
                    short[] Stacker = default;

                    if (GlobalFunctions.IsSystem1)
                    {
                    }
                    else
                    {
                        LeftDestacker = OPCore.Read<short[]>(TagLeftDestacker, typeof(short), Row * Column);
                        RighDestacker = OPCore.Read<short[]>(TagRightDestacker, typeof(short), Row * Column);
                        Stacker = OPCore.Read<short[]>(TagStacker, typeof(short), Row * Column);
                    }

                    if (GlobalFunctions.IsSystem1)
                    {
                    }
                    else
                    {
                        if (LeftDestacker != null)
                            foreach (var item in ugrd_LeftDestacker.Children)
                            {
                                TextBlock tb = item as TextBlock;
                                string result = LeftDestacker[Convert.ToInt32(tb.Tag) - 1].ToString();
                                tb.Text = "";
                                tb.Background = Dic_ResultColor[result];
                            }
                        if (RighDestacker != null)
                            foreach (var item in ugrd_RightDestacker.Children)
                            {
                                TextBlock tb = item as TextBlock;
                                string result = RighDestacker[Convert.ToInt32(tb.Tag) - 1].ToString();
                                tb.Text = "";
                                tb.Background = Dic_ResultColor[result];
                            }
                        if (Stacker != null)
                            foreach (var item in ugrd_Stacker.Children)
                            {
                                TextBlock tb = item as TextBlock;
                                string result = Stacker[Convert.ToInt32(tb.Tag) - 1].ToString();
                                tb.Text = "";
                                tb.Background = Dic_ResultColor[result];
                            }
                    }
                }
                catch (Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            });
        }

        ~ucStacker()
        {
            Dispose();
        }

        public void Dispose()
        {
            _Main.StackerPageOn = false;
        }

        private void ChangeTray_Click(object sender, RoutedEventArgs e)
        {
            string tag = (sender as Button).Tag.ToString();

            OPCore.Write(tag, true, typeof(bool));
        }

        private void ButtonToggleSlotCommand(string tagName)
        {
            bool engineeringMode = OPCore.Read<bool>(Tags.MainPage.EngineeringMode.Name);

            if (engineeringMode)
            {
                int currentValue = OPCore.Read<short>(tagName);
                if (currentValue != 0)
                {
                    if (MessageBox.Show($"Turn off {tagName}?", "Toggle Slot", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        OPCore.Write(tagName, 0, typeof(short));
                }
                else
                {
                    if (MessageBox.Show($"Turn on {tagName}?", "Toggle Slot", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        OPCore.Write(tagName, 1, typeof(short));
                }
            }
        }
    }
}
