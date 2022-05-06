using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI.Models
{
    public class RecipeModel : ViewModelBase
    {
        #region PublicProperties
        private string position = string.Empty;
        public string Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    RaisePropertyChanged(nameof(Position));
                }
            }
        }

        private List<RecipeFieldModel> fieldList = new List<RecipeFieldModel>();
        public List<RecipeFieldModel> FieldList
        {
            get { return fieldList; }
            set
            {
                if (fieldList != value)
                {
                    fieldList = value;
                    RaisePropertyChanged(nameof(FieldList));
                    RaisePropertyChanged(nameof(FieldListView));
                }
            }
        }
        
        private ICollectionView fieldListView = null;
        public ICollectionView FieldListView
        {
            get
            {
                fieldListView = null;
                fieldListView = CollectionViewSource.GetDefaultView(FieldList);
                return fieldListView;
            }
        }

        private List<RecipeSelectionModel> selectionList = new List<RecipeSelectionModel>();
        public List<RecipeSelectionModel> SelectionList
        {
            get { return selectionList; }
            set
            {
                if (selectionList != value)
                {
                    selectionList = value;
                    RaisePropertyChanged(nameof(SelectionList));
                    RaisePropertyChanged(nameof(SelectionListView));
                }
            }
        }
        
        private ICollectionView selectionListView = null;
        public ICollectionView SelectionListView
        {
            get
            {
                selectionListView = null;
                selectionListView = CollectionViewSource.GetDefaultView(SelectionList);
                selectionListView.Filter = x =>
                {
                    RecipeSelectionModel recipeSelectionModel = x as RecipeSelectionModel;
                    return recipeSelectionModel.SelectionEnable == true;
                };
                return selectionListView;
            }
        }


        private List<RecipeSelectionModel> comboList = new List<RecipeSelectionModel>();
        public List<RecipeSelectionModel> ComboList
        {
            get { return comboList; }
            set
            {
                if (comboList != value)
                {
                    comboList = value;
                    RaisePropertyChanged(nameof(ComboList));
                    RaisePropertyChanged(nameof(ComboListView));
                }
            }
        }

        private ICollectionView comboListView = null;
        public ICollectionView ComboListView
        {
            get
            {
                comboListView = null;
                comboListView = CollectionViewSource.GetDefaultView(ComboList);
                comboListView.Filter = x =>
                {
                    RecipeSelectionModel recipeSelectionModel = x as RecipeSelectionModel;
                    return recipeSelectionModel.SelectionEnable == true;
                };
                return comboListView;
            }
        }
        #endregion
    }
}