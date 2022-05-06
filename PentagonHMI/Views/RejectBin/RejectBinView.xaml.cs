using PentagonHMI.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI.Views
{
    /// <summary>
    /// Interaction logic for RejectBinView.xaml.
    /// </summary>
    public partial class RejectBinView : UserControl
    {
        #region PrivateFields
        private List<RejectBinModel> rejectBinList = new List<RejectBinModel>();
        #endregion

        #region Constructor
        public RejectBinView()
        {
            try
            {
                InitializeComponent();
                initializeRejectBinList();
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion

        #region PrivateInitializeMethods
        private void initializeRejectBinList()
        {
            try
            {
                rejectBinList = new List<RejectBinModel>();

                string rejectBinName = string.Empty;

                for (int i = 0; i < 4; i++)
                {
                    try
                    {
                        switch (i)
                        {
                            default:
                                break;
                            case 0:
                                rejectBinName = "REEL";
                                break;
                            case 1:
                                rejectBinName = "AUDIT";
                                break;
                            case 2:
                                rejectBinName = "REJECT A";
                                break;
                            case 3:
                                rejectBinName = "REJECT B";
                                break;
                        }

                        List<RejectBinPartModel> rejectBinPartList = new List<RejectBinPartModel>();

                        for (int j = 0; j < 10; j++)
                        {
                            try
                            {
                                rejectBinPartList.Add(new RejectBinPartModel());
                            }
                            catch (Exception ex)
                            {
                                FileLogger.logError(ex.Message, ex.ToString());
                            }
                        }
                        
                        rejectBinList.Add(new RejectBinModel
                        {
                            RejectBinName = rejectBinName,
                            RejectBinPartList = rejectBinPartList
                        });
                    }
                    catch (Exception ex)
                    {
                        FileLogger.logError(ex.Message, ex.ToString());
                    }
                }

                int n = 0;
                foreach (var item in rejectBinList[0].RejectBinPartList)
                {
                    item.PartQuantity = ++n;
                    item.PartFull = 0;
                }

                //rejectBinList[0].RejectBinPartList[0].PartQuantity = 100;
                //rejectBinList[0].RejectBinPartList[0].PartFull = true;

                //rejectBinList[1].RejectBinPartList[3].PartQuantity = 100;
                //rejectBinList[1].RejectBinPartList[3].PartFull = true;

                //rejectBinList[2].RejectBinPartList[6].PartQuantity = 100;
                //rejectBinList[2].RejectBinPartList[6].PartFull = true;

                RejectBinListItemsControl.ItemsSource = rejectBinList;
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion
    }
}