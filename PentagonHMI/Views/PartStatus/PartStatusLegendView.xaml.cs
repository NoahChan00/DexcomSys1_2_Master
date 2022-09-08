using System;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI.Views
{
    /// <summary>
    /// Interaction logic for PartStatusLegendView.xaml.
    /// </summary>
    public partial class PartStatusLegendView : UserControl
    {
        #region Constructor

        public PartStatusLegendView()
        {
            try
            {
                InitializeComponent();
            }
            catch(Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        #endregion Constructor
    }
}