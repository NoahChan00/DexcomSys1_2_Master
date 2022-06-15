using GalaSoft.MvvmLight.CommandWpf;
using PentagonHMI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Utilities;
using PentagonHMI.ViewModel.Recipe;
using System.Data;
using System.Windows.Input;
using SimpleDatabase;
using SimpleOPC;


namespace PentagonHMI.Views
{

    public partial class RecipeView : UserControl, IDisposable
    {
        #region PrivateFields
        private List<RecipeModel> recipeList = new List<RecipeModel>();
        private List<string> lst_ReelType;
        SQLCarrier SQLer = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName);
        INGEAR_Opc OPC = new INGEAR_Opc(Info.OPC.IP);
        #endregion

        #region Constructor
        public RecipeView()
        {
            try
            {
                InitializeComponent();
                OPC.Connect(Info.OPC.IP);
                InitialReelType();
                initializeRecipeList();
                InitializeData();
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void InitialReelType()
        {
            try
            {
                lst_ReelType = SQLer.Exec_Scalar<string>("Select [Info] From [Petty] Where [Item] = 'ReelType'").Split(';').ToList();
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion

        #region DatabaseRelated
        DataTable DT_Family_OutputNumber;// = new DataTable();
        DataTable DT_InputNumber_Family;
        Dictionary<string, List<string>> Dic_Family_OutputPartNumber = new Dictionary<string, List<string>>();
        Dictionary<string, List<string>> Dic_InputPartNumber_Family = new Dictionary<string, List<string>>();
        List<string> Lst_Family = new List<string>();
        private void InitializeData()
        {
            try
            {
                DT_Family_OutputNumber = Recipe_ViewModel.GetFamily_OutputPartNumber();
                foreach (DataRow dr in DT_Family_OutputNumber.Rows)
                {
                    string strFamily = dr["Family"].ToString();
                    Dic_Family_OutputPartNumber.Add(strFamily, dr["OutputPartNumber"].ToString().Split(';').ToList<string>());
                    Lst_Family.Add(strFamily);
                }

                DT_InputNumber_Family = Recipe_ViewModel.GetInputPartNumber_Family();
                foreach (DataRow dr in DT_InputNumber_Family.Rows)
                {
                    string strInputPartNumber = dr["InputPartNumber"].ToString();
                    Dic_InputPartNumber_Family.Add(strInputPartNumber, dr["Family"].ToString().Split(';').ToList<string>());
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion

        private void UpdateOutputPartNumber(object Obj)
        {
            try
            {
                ComboBox cbx = Obj as ComboBox;
                var raw = cbx.SelectedValue;
                string[] indexes = cbx.Tag as string[];
                List<string> Lst_PartNumber;
                if (Dic_Family_OutputPartNumber.TryGetValue(Convert.ToString(raw), out Lst_PartNumber))
                {
                    var Result = recipeList[Convert.ToInt32(indexes[0]) - 1].SelectionList.Select(x => x)
                        .Where(y => y.SelectionName.Contains(indexes[1]))
                        .First()
                        .SelectionParameterList
                        .Select(z => z)
                        .Where(v => v.SelectionParameterName == "Output Part Number").First();

                    if (Result != null)
                        Result.ComboSource = Lst_PartNumber;
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private bool UpdateFamily(object Obj)
        {
            try
            {
                TextBox Tbx = Obj as TextBox;
                if (Tbx.IsReadOnly)
                    return false;
                var raw = Tbx.Text;
                int index = (int)Tbx.Tag;
                List<string> Lst_Family;
                if (Dic_InputPartNumber_Family.TryGetValue(Convert.ToString(raw), out Lst_Family))
                {
                    var Results = recipeList[index - 1].SelectionList.Select(x => x);
                    foreach (RecipeSelectionModel Mdl in Results)
                        Mdl.SelectionParameterList[0].ComboSource = Lst_Family;
                    return true;
                }
                else
                {
                    MessageTextBlock.Text = $"InputPartNumber: {Tbx.Text} Not Existed";
                    Tbx.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }

            return false;
        }

        #region PrivateInitializeMethods
        private void initializeRecipeList()
        {
            try
            {
                recipeList = new List<RecipeModel>();

                for (int i = 1; i <= 10; i++)
                {
                    try
                    {
                        List<RecipeSelectionModel> selectionList = new List<RecipeSelectionModel>();
                        string selectionName = string.Empty;

                        for (int j = 0; j < 2; j++)
                        {
                            try
                            {
                                selectionName = j == 0 ? "First" : "Second";

                                selectionList.Add(new RecipeSelectionModel
                                {
                                    SelectionName = $"{selectionName} Selection",
                                    SelectionParameterList = new List<RecipeSelectionParameterModel>
                                    {
                                        new RecipeSelectionParameterModel
                                        {
                                            SelectionParameterName = "Family",
                                            SelectoinChangedCommand = new RelayCommand<object>(UpdateOutputPartNumber),
                                            Index = new string[]{ i.ToString() , selectionName }
                                        },
                                        new RecipeSelectionParameterModel
                                        {
                                            SelectionParameterName = "Output Part Number",
                                        }
                                    }
                                });
                            }
                            catch (Exception ex)
                            {
                                FileLogger.logError(ex.Message, ex.ToString());
                            }
                        }

                        List<RecipeSelectionModel> comboList = new List<RecipeSelectionModel>();
                        comboList.Add(new RecipeSelectionModel
                        {
                            SelectionName = "Reel Type",
                            SelectionParameterList = new List<RecipeSelectionParameterModel>
                                    {
                                        new RecipeSelectionParameterModel
                                        {
                                            SelectoinChangedCommand = new RelayCommand<object>(UpdateOutputPartNumber),
                                            Index = new string[]{ i.ToString() , selectionName },
                                            ComboSource = lst_ReelType
                                        }
                                    }
                        });


                        recipeList.Add(new RecipeModel
                        {
                            Position = i.ToString(),
                            FieldList = new List<RecipeFieldModel>
                            {
                                new RecipeFieldModel
                                {
                                    FieldTextChangedCommand = new RelayCommand<object>(fieldTextChangedCommand_InputPartNumber),
                                    FieldName = "P Field\nInput Part Number\n(Raw Material Part Number Field)",
                                    Index = i
                                },
                                new RecipeFieldModel
                                {
                                    FieldTextChangedCommand = new RelayCommand(fieldTextChangedCommand),
                                    FieldName = "Q Field\n(Quantity Field)"
                                },
                                new RecipeFieldModel
                                {
                                    FieldTextChangedCommand = new RelayCommand(fieldTextChangedCommand),
                                    FieldName = "S Field\n(ST batch number Field)"
                                },
                                new RecipeFieldModel
                                {
                                    FieldTextChangedCommand = new RelayCommand(fieldTextChangedCommand),
                                    FieldName = "Z Field\n(Brady batch number Field)"
                                }
                            },
                            SelectionList = selectionList,
                            ComboList = comboList
                        });
                    }
                    catch (Exception ex)
                    {
                        FileLogger.logError(ex.Message, ex.ToString());
                    }
                }

                FieldListItemsControl.DataContext = recipeList;
                SelectionListItemsControl.DataContext = recipeList;
                ictrl_ReelType_Title.DataContext = recipeList;
                RecipeListItemsControl.ItemsSource = recipeList;
                FieldListItemsControl2.ItemsSource = recipeList;
                ictrl_ReelType.ItemsSource = recipeList;


                RecipeListItemsControl2.ItemsSource = recipeList;

                //SelectionParameterListView

                recipeSelection();
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion

        #region PrivateEventMethods
        private void newRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show($"Are you sure you want to new recipe?", nameof(MessageBoxImage.Question),
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    recipeSelection(false, false);
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void proceedToFirstRecipeSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show($"Are you sure you want to proceed to first recipe selection?", nameof(MessageBoxImage.Question),
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    recipeSelection(true, false);
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void proceedToSecondRecipeSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show($"Are you sure you want to proceed to second recipe selection?", nameof(MessageBoxImage.Question),
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    recipeSelection(false, true);
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void proceedToFinalRecipeConfirmationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show($"Are you sure you want to proceed to final recipe confirmation?", nameof(MessageBoxImage.Question),
                    MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                {
                    recipeSelection(true, true);
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void loadRecipeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (OPC.Read<bool>("MC_System_Tags.MachineRunning"))
                {
                    MessageBox.Show("Please Stop the Machine before loading a recipe.");
                }
                else
                {
                    if (MessageBox.Show("Are you sure you want to load recipe?", nameof(MessageBoxImage.Question),
                        MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No) == MessageBoxResult.Yes)
                    {
                        recipeSelection();
                        loadRecipeToPLC();
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        //All start from 1
        List<string> lst_Tag_PQSZ = new List<string>
        {
            "Input_Pcode[{0}]",
            "Input_Qcode[{0}]",
            "Input_Scode[{0}]",
            "Input_Zcode[{0}]"
         };
        string Tag_OutputPartNumber = "Input_OutputPartNumber[{0}]";
        string Tag_Family = "Input_Family[{0}]";
        private void loadRecipeToPLC()
        {
            int n = 0;
            recipeList.ForEach((x) =>
            {
                n++; int n2 = 0;
                //if (!string.IsNullOrWhiteSpace(x.FieldList[0].Field))
                //{
                x.FieldList.ForEach((y) => { OPC.Write(string.Format(lst_Tag_PQSZ[n2++], n), y.Field, typeof(string)); });

                OPC.Write(string.Format(Tag_Family, n), x.SelectionList[0].SelectionParameterList[0].SelectionParameter, typeof(string));
                OPC.Write(string.Format(Tag_OutputPartNumber, n), x.SelectionList[0].SelectionParameterList[1].SelectionParameter, typeof(string));
                //}
            });

            OPC.Write("HMI_Recipe_Select_Ok",true);
        }
        #endregion

        #region PrivateCommandMethods
        private void fieldTextChangedCommand()
        {
            try
            {
                if (Keyboard.IsKeyDown(Key.Return))
                    focusRecipeField();
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void fieldTextChangedCommand_InputPartNumber(object Obj)
        {
            try
            {
                if (Keyboard.IsKeyDown(Key.Return))
                {
                    if (UpdateFamily(Obj))
                        focusRecipeField();
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion

        #region PrivateMethods
        private void focusRecipeField()
        {
            try
            {
                bool focus = false;

                for (int i = 0; i < FieldListItemsControl2.Items.Count; i++)
                {
                    try
                    {
                        ContentPresenter positionContentPresenter = FieldListItemsControl2.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
                        ItemsControl fieldListItemsControl = FindVisualChild<ItemsControl>(positionContentPresenter);

                        for (int j = 0; j < fieldListItemsControl.Items.Count; j++)
                        {
                            try
                            {
                                ContentPresenter fieldContentPresenter = fieldListItemsControl.ItemContainerGenerator.ContainerFromIndex(j) as ContentPresenter;
                                TextBox textBox = FindVisualChild<TextBox>(fieldContentPresenter);

                                if (string.IsNullOrEmpty(textBox.Text))
                                {
                                    textBox.Focus();
                                    focus = true;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                FileLogger.logError(ex.Message, ex.ToString());
                            }
                        }

                        if (focus)
                        {
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        FileLogger.logError(ex.Message, ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }

        private void recipeSelection(bool? firstSelection = null, bool? secondSelection = null)
        {
            try
            {
                Visibility newRecipe = Visibility.Collapsed;
                Visibility proceedToFirstRecipeSelection = Visibility.Collapsed;
                Visibility proceedToSecondRecipeSelection = Visibility.Collapsed;
                Visibility proceedToFinalRecipeConfirmation = Visibility.Collapsed;
                Visibility loadRecipe = Visibility.Collapsed;
                string message = string.Empty;

                if (firstSelection == null && secondSelection == null)
                {
                    recipeList.ForEach(x =>
                    {
                        try
                        {
                            x.FieldList.ForEach(y =>
                            {
                                try
                                {
                                    y.FieldIsReadOnly = true;
                                }
                                catch (Exception ex)
                                {
                                    FileLogger.logError(ex.Message, ex.ToString());
                                }
                            });

                            x.ComboList.ForEach(y =>
                            {
                                y.SelectionParameterList.ForEach(z =>
                                {
                                    try
                                    {
                                        z.SelectionParameterIsReadOnly = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        FileLogger.logError(ex.Message, ex.ToString());
                                    }
                                });
                            });

                            x.SelectionList.ForEach(y =>
                            {
                                try
                                {
                                    y.SelectionParameterList.ForEach(z =>
                                    {
                                        try
                                        {
                                            z.SelectionParameterIsReadOnly = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            FileLogger.logError(ex.Message, ex.ToString());
                                        }
                                    });
                                }
                                catch (Exception ex)
                                {
                                    FileLogger.logError(ex.Message, ex.ToString());
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            FileLogger.logError(ex.Message, ex.ToString());
                        }
                    });

                    newRecipe = Visibility.Visible;
                }
                else if (firstSelection == false && secondSelection == false)
                {
                    recipeList.ForEach(x =>
                    {
                        try
                        {
                            x.FieldList.ForEach(y =>
                            {
                                try
                                {
                                    y.Field = string.Empty;
                                    y.FieldIsReadOnly = false;
                                }
                                catch (Exception ex)
                                {
                                    FileLogger.logError(ex.Message, ex.ToString());
                                }
                            });

                            x.ComboList.ForEach(y =>
                            {
                                y.SelectionParameterList.ForEach(z =>
                                {
                                    try
                                    {
                                        z.SelectionParameter = string.Empty;
                                        z.SelectionParameterIsReadOnly = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        FileLogger.logError(ex.Message, ex.ToString());
                                    }
                                });
                            });


                            x.SelectionList.ForEach(y =>
                            {
                                try
                                {
                                    y.SelectionParameterList.ForEach(z =>
                                    {
                                        try
                                        {
                                            z.SelectionParameter = string.Empty;
                                            z.SelectionParameterIsReadOnly = false;
                                        }
                                        catch (Exception ex)
                                        {
                                            FileLogger.logError(ex.Message, ex.ToString());
                                        }
                                    });
                                }
                                catch (Exception ex)
                                {
                                    FileLogger.logError(ex.Message, ex.ToString());
                                }
                            });
                        }
                        catch (Exception ex)
                        {
                            FileLogger.logError(ex.Message, ex.ToString());
                        }
                    });

                    focusRecipeField();

                    proceedToFirstRecipeSelection = Visibility.Visible;
                }
                else if (firstSelection == true && secondSelection == false)
                {
                    if (recipeList.SelectMany(x => x.SelectionList).SelectMany(x => x.SelectionParameterList).Any(x => x.SelectionParameterMismatch == true))
                    {
                        recipeList.SelectMany(x => x.SelectionList).SelectMany(x => x.SelectionParameterList).Where(
                            x => x.SelectionParameterMismatch == true).ToList().ForEach(x =>
                            {
                                try
                                {
                                    x.SelectionParameterIsReadOnly = false;
                                }
                                catch (Exception ex)
                                {
                                    FileLogger.logError(ex.Message, ex.ToString());
                                }
                            });

                        proceedToFinalRecipeConfirmation = Visibility.Visible;
                    }
                    else
                    {
                        recipeList.ForEach(x =>
                        {
                            try
                            {
                                x.FieldList.ForEach(y =>
                                {
                                    try
                                    {
                                        y.FieldIsReadOnly = true;
                                    }
                                    catch (Exception ex)
                                    {
                                        FileLogger.logError(ex.Message, ex.ToString());
                                    }
                                });

                                x.ComboList.ForEach(y =>
                                {
                                    y.SelectionParameterList.ForEach(z =>
                                    {
                                        try
                                        {
                                            z.SelectionParameterIsReadOnly = true;
                                        }
                                        catch (Exception ex)
                                        {
                                            FileLogger.logError(ex.Message, ex.ToString());
                                        }
                                    });
                                });
                            }
                            catch (Exception ex)
                            {
                                FileLogger.logError(ex.Message, ex.ToString());
                            }
                        });

                        proceedToSecondRecipeSelection = Visibility.Visible;
                    }
                }
                else if (firstSelection == false && secondSelection == true)
                {
                    if (recipeList.SelectMany(x => x.SelectionList).SelectMany(x => x.SelectionParameterList).Any(x => x.SelectionParameterMismatch == true))
                    {
                        recipeList.SelectMany(x => x.SelectionList).SelectMany(x => x.SelectionParameterList).Where(
                            x => x.SelectionParameterMismatch == true).ToList().ForEach(x =>
                            {
                                try
                                {
                                    x.SelectionParameterIsReadOnly = false;
                                }
                                catch (Exception ex)
                                {
                                    FileLogger.logError(ex.Message, ex.ToString());
                                }
                            });

                        proceedToFinalRecipeConfirmation = Visibility.Visible;
                    }

                    proceedToFinalRecipeConfirmation = Visibility.Visible;
                }
                else if (firstSelection == true && secondSelection == true)
                {
                    recipeList.ForEach(x =>
                    {
                        try
                        {
                            for (int i = 0; i < recipeList.SelectMany(y => y.SelectionList).Select(y => y.SelectionParameterList.Count).Max(); i++)
                            {
                                try
                                {
                                    x.SelectionList[0].SelectionParameterList[i].SelectionParameterIsReadOnly = true;
                                    x.SelectionList[1].SelectionParameterList[i].SelectionParameterIsReadOnly = true;

                                    if (x.SelectionList[0].SelectionParameterList[i].SelectionParameter ==
                                    x.SelectionList[1].SelectionParameterList[i].SelectionParameter)
                                    {
                                        x.SelectionList[0].SelectionParameterList[i].SelectionParameterMismatch = false;
                                        x.SelectionList[1].SelectionParameterList[i].SelectionParameterMismatch = false;
                                    }
                                    else
                                    {
                                        x.SelectionList[0].SelectionParameterList[i].SelectionParameterMismatch = true;
                                        x.SelectionList[1].SelectionParameterList[i].SelectionParameterMismatch = true;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    FileLogger.logError(ex.Message, ex.ToString());
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            FileLogger.logError(ex.Message, ex.ToString());
                        }
                    });

                    if (recipeList.SelectMany(x => x.SelectionList).SelectMany(x => x.SelectionParameterList).Any(x => x.SelectionParameterMismatch == true))
                    {
                        proceedToFirstRecipeSelection = Visibility.Visible;
                        proceedToSecondRecipeSelection = Visibility.Visible;
                        message = "Recipe Selection Mismatch! Please Reselect Recipe.";
                    }
                    else
                    {
                        loadRecipe = Visibility.Visible;
                    }
                }

                recipeList.ForEach(x =>
                {
                    try
                    {
                        x.SelectionList[0].SelectionEnable = firstSelection == null ? true : Convert.ToBoolean(firstSelection);
                        x.SelectionList[1].SelectionEnable = secondSelection == null ? true : Convert.ToBoolean(secondSelection);

                        x.RaisePropertyChanged(nameof(RecipeModel.SelectionListView));
                    }
                    catch (Exception ex)
                    {
                        FileLogger.logError(ex.Message, ex.ToString());
                    }
                });

                NewRecipeButton.Visibility = newRecipe;
                ProceedToFirstRecipeSelectionButton.Visibility = proceedToFirstRecipeSelection;
                ProceedToSecondRecipeSelectionButton.Visibility = proceedToSecondRecipeSelection;
                ProceedToFinalRecipeConfirmationButton.Visibility = proceedToFinalRecipeConfirmation;
                LoadRecipeButton.Visibility = loadRecipe;
                ResetButton.Visibility = loadRecipe;
                MessageTextBlock.Text = message;
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }
        }
        #endregion

        #region PublicMethods
        public static T FindVisualChild<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            try
            {
                if (dependencyObject != null)
                {
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
                    {
                        try
                        {
                            DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
                            if (child != null && child is T)
                            {
                                return (T)child;
                            }

                            T childItem = FindVisualChild<T>(child);
                            if (childItem != null)
                            {
                                return childItem;
                            }
                        }
                        catch (Exception ex)
                        {
                            FileLogger.logError(ex.Message, ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.logError(ex.Message, ex.ToString());
            }

            return null;
        }

        public void Dispose()
        {
        }
        #endregion

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            recipeSelection(false, false);
        }
    }
}