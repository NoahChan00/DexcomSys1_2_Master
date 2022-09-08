using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Data;

namespace PentagonHMI.Models
{
    public class RecipeSelectionModel : ViewModelBase
    {
        #region PublicProperties

        private string selectionName = string.Empty;

        public string SelectionName
        {
            get { return selectionName; }
            set
            {
                if(selectionName != value)
                {
                    selectionName = value;
                    RaisePropertyChanged(nameof(SelectionName));
                }
            }
        }

        private bool selectionEnable = false;

        public bool SelectionEnable
        {
            get { return selectionEnable; }
            set
            {
                if(selectionEnable != value)
                {
                    selectionEnable = value;
                    RaisePropertyChanged(nameof(SelectionEnable));
                }
            }
        }

        private List<RecipeSelectionParameterModel> selectionParameterList = new List<RecipeSelectionParameterModel>();

        public List<RecipeSelectionParameterModel> SelectionParameterList
        {
            get { return selectionParameterList; }
            set
            {
                if(selectionParameterList != value)
                {
                    selectionParameterList = value;
                    RaisePropertyChanged(nameof(SelectionParameterList));
                    RaisePropertyChanged(nameof(SelectionParameterListView));
                }
            }
        }

        private ICollectionView selectionParameterListView = null;

        public ICollectionView SelectionParameterListView
        {
            get
            {
                selectionParameterListView = null;
                selectionParameterListView = CollectionViewSource.GetDefaultView(SelectionParameterList);
                return selectionParameterListView;
            }
        }

        #endregion PublicProperties
    }
}