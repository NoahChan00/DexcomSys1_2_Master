using GalaSoft.MvvmLight.CommandWpf;
using PentagonHMI.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI.Views
{
    public partial class CassetteStatusView : UserControl
    {
        #region PrivateFields

        public List<CassetteStatusCassetteModel> cassetteList = new List<CassetteStatusCassetteModel>();

        #endregion PrivateFields

        #region Constructor

        public CassetteStatusView()
        {
            try
            {
                InitializeComponent();
                initializeCassetteList();
            }
            catch(Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion Constructor

        #region PrivateInitializeMethods

        private void initializeCassetteList()
        {
            try
            {
                cassetteList = new List<CassetteStatusCassetteModel>();

                for(int i = 0; i < 2; i++)
                {
                    try
                    {
                        List<CassetteStatusPositionModel> positionList = new List<CassetteStatusPositionModel>();

                        for(int j = 1; j <= 10; j++)
                        {
                            try
                            {
                                positionList.Add(new CassetteStatusPositionModel
                                {
                                    Position = j,
                                    CassetteSubStatusList = GetCasstteSyvStatusList(i)
                                });
                            }
                            catch(Exception ex)
                            {
                                FileLogger.logError(ex.Message, ex.ToString());
                            }
                        }

                        cassetteList.Add(new CassetteStatusCassetteModel
                        {
                            ScrollChangedCommand = new RelayCommand<EventArgsModel>(scrollChangedCommand),
                            CassetteName = i == 0 ? "Input Cassette" : "Output Cassette",
                            PositionList = positionList
                        });
                    }
                    catch(Exception ex)
                    {
                        FileLogger.logError(ex.Message, ex.ToString());
                    }
                }

                CassetteListItemsControl.ItemsSource = cassetteList;
            }
            catch(Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion PrivateInitializeMethods

        private List<CassetteStatusSubStatusModel> GetCasstteSyvStatusList(int i)
        {
            List<CassetteStatusSubStatusModel> cassetteSubStatusList = new List<CassetteStatusSubStatusModel>
                        {
                            new CassetteStatusSubStatusModel
                            {
                                SubStatusName = "P Field"
                            },
                            new CassetteStatusSubStatusModel
                            {
                                SubStatusName = "Q Field"
                            },
                            new CassetteStatusSubStatusModel
                            {
                                SubStatusName = "S Field"
                            },
                            new CassetteStatusSubStatusModel
                            {
                                SubStatusName = "Z Field"
                            }
                        };

            switch(i)
            {
                default:
                    break;

                case 0:
                    cassetteSubStatusList.Add(new CassetteStatusSubStatusModel
                    {
                        SubStatusName = "Family"
                    });
                    cassetteSubStatusList.Add(new CassetteStatusSubStatusModel
                    {
                        SubStatusName = "OutputPartNum"
                    });
                    break;

                case 1:
                    break;
            }

            return cassetteSubStatusList;
        }

        #region PrivateCommandMethods

        private void scrollChangedCommand(EventArgsModel eventArgsModel)
        {
            try
            {
                ScrollChangedEventArgs scrollChangedEventArgs = eventArgsModel.EventArgs as ScrollChangedEventArgs;
                List<ScrollViewer> scrollViewerList = eventArgsModel.Parameter as List<ScrollViewer>;

                if(scrollViewerList.Count >= 2)
                {
                    scrollViewerList[0].ScrollToHorizontalOffset(scrollChangedEventArgs.HorizontalOffset);
                    scrollViewerList[1].ScrollToVerticalOffset(scrollChangedEventArgs.VerticalOffset);
                }
            }
            catch(Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion PrivateCommandMethods
    }
}