using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for RejectBinDisplayView.xaml.
    /// </summary>
    public partial class RejectBinDisplayView : UserControl, IDisposable
    {
        #region PrivateFields

        private RejectBinDisplayModel rejectBinDisplayModel = new RejectBinDisplayModel();
        private List<RejectReason> rejectReasonList = new List<RejectReason>();
        private LogicClasses.Main main = null;

        #endregion PrivateFields

        #region Constructor

        public RejectBinDisplayView(ref LogicClasses.Main _main, int RejectBinCount, string rejectBinDisplayStatusTagKey)
        {
            try
            {
                InitializeComponent();
                main = _main;
                initializeRejectBinDisplay(RejectBinCount, rejectBinDisplayStatusTagKey);
                initializeRejectReason();
                initializeOpc(_main);
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion Constructor

        #region PrivateInitializeMethods

        private void initializeRejectBinDisplay(int BinCount, string rejectBinDisplayStatusTagKey)
        {
            try
            {
                rejectBinDisplayModel = new RejectBinDisplayModel();

                for(int i = 0; i < BinCount; i++)
                {
                    try
                    {
                        rejectBinDisplayModel.RejectBinDisplayStatusList.Add(new RejectBinDisplayStatusModel
                        {
                            RejectBinDisplayStatusName = $"Reject Bin Slot #{i + 1}",
                            RejectBinDisplayStatusTag = new Logix.Tag
                            {
                                Name = $"{rejectBinDisplayStatusTagKey}[{i}]",
                                DataType = Logix.Tag.ATOMIC.STRING
                            }
                        });
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }

                RejectBinDisplayGroupBox.DataContext = rejectBinDisplayModel;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeRejectReason()
        {
            try
            {
                rejectReasonList = new List<RejectReason>();

                DataTable dataTable = main.SQLer.Exec_DTSelect($"SELECT * FROM [{nameof(RejectReason)}]");
                if(dataTable != null)
                {
                    foreach(DataRow dataRow in dataTable.Rows)
                    {
                        object rejectCode = dataRow[nameof(RejectReason.RejectCode)];
                        object description = dataRow[nameof(RejectReason.Description)];

                        if(rejectCode != null && description != null)
                        {
                            rejectReasonList.Add(new RejectReason
                            {
                                RejectCode = rejectCode.ToString(),
                                Description = description.ToString()
                            });
                        }
                    }
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeOpc(LogicClasses.Main _main)
        {
            try
            {
                main.OnRejectBinDisplayUpdate += main_OnRejectBinDisplayUpdate;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateInitializeMethods

        #region PrivateEventMethods

        private void main_OnRejectBinDisplayUpdate()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    rejectBinDisplayModel.RejectBinDisplayStatusList.ForEach(x =>
                    {
                        try
                        {
                            if(!string.IsNullOrEmpty(x.RejectBinDisplayStatusTag.Name))
                            {
                                main.MyPLC.ReadTag(x.RejectBinDisplayStatusTag);

                                if(x.RejectBinDisplayStatusTag.Value != null)
                                {
                                    string[] split = x.RejectBinDisplayStatusTag.Value.ToString().Split(',');
                                    for(int i = 0; i < split.Length; i++)
                                    {
                                        if(rejectReasonList.Exists(y => y.RejectCode == split[i]))
                                        {
                                            split[i] = rejectReasonList.Find(y => y.RejectCode == split[i]).Description;
                                        }
                                    }

                                    string value = string.Join(" , ", split);

                                    if(x.RejectBinDisplayStatus != value)
                                    {
                                        x.RejectBinDisplayStatus = value;
                                    }
                                }
                            }
                        }
                        catch(Exception exception)
                        {
                            FileLogger.logError(exception.Message, exception.ToString());
                        }
                    });
                }
                catch(Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }

        #endregion PrivateEventMethods

        #region PublicInterfaceMethods

        public void Dispose()
        {
            try
            {
                main.RejectBinDisplayPageOn = false;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PublicInterfaceMethods
    }
}