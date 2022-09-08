using GalaSoft.MvvmLight;
using System.Windows.Input;

namespace PentagonHMI.Models
{
    public class RecipeFieldModel : ViewModelBase
    {
        #region PublicCommandProperties

        public ICommand FieldTextChangedCommand { get; set; }

        #endregion PublicCommandProperties

        #region PublicProperties

        private string fieldName = string.Empty;

        public string FieldName
        {
            get { return fieldName; }
            set
            {
                if(fieldName != value)
                {
                    fieldName = value;
                    RaisePropertyChanged(nameof(FieldName));
                }
            }
        }

        private string field = string.Empty;

        public string Field
        {
            get { return field; }
            set
            {
                if(field != value)
                {
                    field = value;
                    RaisePropertyChanged(nameof(Field));
                }
            }
        }

        private bool fieldIsReadOnly = false;

        public bool FieldIsReadOnly
        {
            get { return fieldIsReadOnly; }
            set
            {
                if(fieldIsReadOnly != value)
                {
                    fieldIsReadOnly = value;
                    RaisePropertyChanged(nameof(FieldIsReadOnly));
                }
            }
        }

        public int Index { get; set; }

        #endregion PublicProperties
    }
}