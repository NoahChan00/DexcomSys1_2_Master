using PentagonHMI.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using Utilities;

namespace PentagonHMI.Views
{
    /// <summary>
    /// Interaction logic for PartStatusView.xaml.
    /// </summary>
    public partial class PartStatusView : UserControl
    {
        #region PrivateFields
        private List<PartStatusZoneModel> zoneList = new List<PartStatusZoneModel>();
        #endregion

        #region Constructor
        public PartStatusView()
        {
            try
            {
                InitializeComponent();
                initializeZoneList();
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion

        public void Update(int[] PogoReject)
        {
            int n = 0;
            zoneList[2].PartList.ForEach(x =>
            {
                x.PartCount = PogoReject[n++].ToString();
            });
        }

        public void Update_Vision_Status(bool[] PartPresent, bool[] Pass, bool[] Fail)
        {
            int n = 0;
            zoneList[0].PartList.ForEach(x =>
            {
                x.PartStatus = !PartPresent[n] ? Brushes.Black : Pass[n] ? Brushes.Lime : Fail[n] ? Brushes.Red : Brushes.White;
                ++n;
            });
        }

        public void Update_Program_Status(bool[] PartPresent, bool[] Pass, bool[] Fail)
        {
            int n = 0;
            zoneList[1].PartList.ForEach(x =>
            {
                x.PartStatus = !PartPresent[n] ? Brushes.Black : Pass[n] ? Brushes.Lime : Fail[n] ? Brushes.Red : Brushes.White;
                ++n;
            });
        }


        #region PrivateInitializeMethods
        private void initializeZoneList()
        {
            try
            {
                zoneList = new List<PartStatusZoneModel>();

                string zoneName = string.Empty;
                bool enableManualAudit = false;

                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        switch (i)
                        {
                            default:
                                break;
                            case 0:
                                zoneName = "Vision";
                                enableManualAudit = false;
                                break;
                            case 1:
                                zoneName = "Program Station";
                                enableManualAudit = false;
                                break;
                            case 2:
                                zoneName = "Audit & Reject";
                                enableManualAudit = true;
                                break;
                        }

                        List<PartStatusPartModel> partList = new List<PartStatusPartModel>();

                        for (int j = 1; j <= 40; j++)
                        {
                            try
                            {
                                partList.Add(new PartStatusPartModel
                                {
                                    PartIndex = j,
                                    PartSubStatusList = new List<PartStatusSubStatusModel>
                                    {
                                        new PartStatusSubStatusModel
                                        {
                                            SubStatusName = "Fail Code"
                                        }
                                    }
                                });
                            }
                            catch (Exception ex)
                            {
                                FileLogger.logError(ex.Message, ex.ToString());
                            }
                        }
                        
                        zoneList.Add(new PartStatusZoneModel
                        {
                            ZoneName = zoneName,
                            EnableManualAudit = enableManualAudit,
                            PartList = partList
                        });
                    }
                    catch (Exception ex)
                    {
                        FileLogger.logError(ex.Message, ex.ToString());
                    }
                }

                //zoneList[2].PartList.ForEach(x =>
                //{
                //    x.PartCount = "100";
                //});

                ZoneListItemsControl.ItemsSource = zoneList;
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion
    }
}