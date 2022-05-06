using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Data;
using System.Windows.Input;

namespace PentagonHMI.Models
{
    public class RecipeSelectionParameterModel : ViewModelBase
    {
        #region PublicProperties
        private string selectionParameterName = string.Empty;
        public string SelectionParameterName
        {
            get { return selectionParameterName; }
            set
            {
                if (selectionParameterName != value)
                {
                    selectionParameterName = value;
                    RaisePropertyChanged(nameof(SelectionParameterName));
                }
            }
        }

        private string selectionParameter = string.Empty;
        public string SelectionParameter
        {
            get { return selectionParameter; }
            set
            {
                if (selectionParameter != value)
                {
                    selectionParameter = value;
                    RaisePropertyChanged(nameof(SelectionParameter));
                }
            }
        }
        
        private bool selectionParameterIsReadOnly = false;
        public bool SelectionParameterIsReadOnly
        {
            get { return selectionParameterIsReadOnly; }
            set
            {
                if (selectionParameterIsReadOnly != value)
                {
                    selectionParameterIsReadOnly = value;
                    RaisePropertyChanged(nameof(SelectionParameterIsReadOnly));
                    //!!!Foong
                    SelectionParameterIsEnabled = !value;
                }
            }
        }

        //!!!Foong
        private bool selectionParameterIsEnabled = false;
        public bool SelectionParameterIsEnabled
        {
            get { return selectionParameterIsEnabled; }
            set
            {
                if (selectionParameterIsEnabled != value)
                {
                    selectionParameterIsEnabled = value;
                    RaisePropertyChanged(nameof(SelectionParameterIsEnabled));
                }
            }
        }

        //!!!Foong
        private List<string> comboSource = new List<string>();
        public List<string> ComboSource
        {
            get { return comboSource; }
            set
            {
                if (comboSource != value)
                {
                    comboSource = value;
                    RaisePropertyChanged(nameof(ComboSource));
                }
            }
        }

        //!!!Foong
        public ICommand SelectoinChangedCommand { get; set; }

        //!!!Foong
        public string[] Index { get; set; } = new string[2];

        private bool selectionParameterMismatch = false;
        public bool SelectionParameterMismatch
        {
            get { return selectionParameterMismatch; }
            set
            {
                if (selectionParameterMismatch != value)
                {
                    selectionParameterMismatch = value;
                    RaisePropertyChanged(nameof(SelectionParameterMismatch));
                }
            }
        }
        #endregion
    }
}