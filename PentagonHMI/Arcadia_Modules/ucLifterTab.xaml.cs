using System;
using System.Windows.Controls;

namespace PentagonHMI.Arcadia_Modules
{
    public partial class ucLifterTab : UserControl, IDisposable
    {
        public ucLifterTab(ref LogicClasses.Main main, int Zone, string Position)
        {
            InitializeComponent();
            //LifterMain_Back_Zone1.Setup(ref main, 1, false);
            LifterModule.Setup(ref main, Zone, Position);
        }

        public void Dispose()
        {
        }
    }
}