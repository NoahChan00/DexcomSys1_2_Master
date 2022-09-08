using PentagonHMI.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI.Views
{
    /// <summary>
    /// Interaction logic for YieldView.xaml.
    /// </summary>
    public partial class YieldView : UserControl
    {
        #region PrivateFields

        private List<YieldStatusModel> yieldStatusList = new List<YieldStatusModel>();

        #endregion PrivateFields

        #region Constructor

        public YieldView()
        {
            try
            {
                InitializeComponent();
                initializeYieldStatusList();
            }
            catch(Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion Constructor

        #region PrivateInitializeMethods

        private void initializeYieldStatusList()
        {
            try
            {
                yieldStatusList = new List<YieldStatusModel>
                {
                    new YieldStatusModel
                    {
                        YieldStatusName = "Pass"
                    },
                    new YieldStatusModel
                    {
                        YieldStatusName = "Fail"
                    }
                };

                YieldStatusListItemsControl.ItemsSource = yieldStatusList;
            }
            catch(Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion PrivateInitializeMethods
    }
}