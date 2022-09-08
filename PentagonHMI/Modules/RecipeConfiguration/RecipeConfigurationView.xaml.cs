using SimpleDatabase;
using SimpleOPC;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for RecipeConfigurationView.xaml.
    /// </summary>
    public partial class RecipeConfigurationView : UserControl, IDisposable
    {
        #region PrivateFields

        private INGEAR_Opc iNGEAR_Opc = null;
        private List<RecipeConfigurationMPNModel> editRecipeConfigurationMPNList = new List<RecipeConfigurationMPNModel>();
        private List<RecipeConfigurationMPNModel> currentRecipeConfigurationMPNList = new List<RecipeConfigurationMPNModel>();
        private List<RecipeConfigurationModel> recipeConfigurationList = new List<RecipeConfigurationModel>();
        private SQLCarrier sQLCarrier = new SQLCarrier(Info.SQL.ServerName, Info.SQL.DatabaseName, Info.SQL.IntegratedSecurity, Info.SQL.PersistSecurityInfo, Info.SQL.UserID, Info.SQL.Password);
        private const string mPNListTagName = "HMI_Recipe_MPN_List_Recipe[{0},{1}]";

        #endregion PrivateFields

        #region Constructor

        public RecipeConfigurationView(INGEAR_Opc _iNGEAR_Opc)
        {
            try
            {
                InitializeComponent();
                initializeOpc(_iNGEAR_Opc);
                initializeRecipeConfigurationMPNList();
                initializeRecipeConfiguration();
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion Constructor

        #region PrivateInitializeMethods

        private void initializeOpc(INGEAR_Opc _iNGEAR_Opc)
        {
            try
            {
                iNGEAR_Opc = _iNGEAR_Opc;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeRecipeConfigurationMPNList()
        {
            try
            {
                editRecipeConfigurationMPNList = new List<RecipeConfigurationMPNModel>();
                EditMPNListDataGrid.ItemsSource = editRecipeConfigurationMPNList;

                currentRecipeConfigurationMPNList = new List<RecipeConfigurationMPNModel>();
                CurrentMPNListDataGrid.ItemsSource = currentRecipeConfigurationMPNList;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        private void initializeRecipeConfiguration()
        {
            try
            {
                recipeConfigurationList = new List<RecipeConfigurationModel>();

                DataTable dataTable = sQLCarrier.Exec_DTSelect(
                    $"SELECT [{nameof(RecipeConfigurationModel.RecipeName)}]," +
                    $"[{nameof(RecipeConfigurationModel.MPNMaxCount)}]," +
                    $"[{nameof(RecipeConfigurationModel.TrayImage)}]" +
                    $" FROM [RackConfig] ORDER BY [RecipeIndex]");

                for(int i = 0; i < dataTable.Rows.Count; i++)
                {
                    try
                    {
                        recipeConfigurationList.Add(new RecipeConfigurationModel
                        {
                            RecipeName = Convert.ToString(dataTable.Rows[i][nameof(RecipeConfigurationModel.RecipeName)]),
                            MPNMaxCount = Convert.ToInt32(dataTable.Rows[i][nameof(RecipeConfigurationModel.MPNMaxCount)]),
                            TrayImage = Convert.ToString(dataTable.Rows[i][nameof(RecipeConfigurationModel.TrayImage)])
                        });
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }

                RecipeConfigurationComboBox.ItemsSource = recipeConfigurationList;
                RecipeConfigurationComboBox.SelectedIndex = 0;
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateInitializeMethods

        #region PrivateCommandMethods

        private void recipeConfigurationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    if(RecipeConfigurationComboBox.SelectedIndex >= 0)
                    {
                        switch(iNGEAR_Opc.Read<int>(
                            $"HMI_Recipe_MPN_Height_mm[{RecipeConfigurationComboBox.SelectedIndex}]"))
                        {
                            case 28:
                                CurrentRAMHeightTallCheckBox.IsChecked = false;
                                CurrentRAMHeightShortCheckBox.IsChecked = true;
                                break;

                            case 32:
                                CurrentRAMHeightTallCheckBox.IsChecked = true;
                                CurrentRAMHeightShortCheckBox.IsChecked = false;
                                break;

                            default:
                                CurrentRAMHeightTallCheckBox.IsChecked = false;
                                CurrentRAMHeightShortCheckBox.IsChecked = false;
                                break;
                        }

                        CurrentTrayDefinitionRowsTextBlock.Text = iNGEAR_Opc.Read<string>(
                            $"HMI_Recipe_Tray_Row_Recipe[{RecipeConfigurationComboBox.SelectedIndex}]");

                        CurrentTrayDefinitionColumnsTextBlock.Text = iNGEAR_Opc.Read<string>(
                            $"HMI_Recipe_Tray_Column_Recipe[{RecipeConfigurationComboBox.SelectedIndex}]");

                        editRecipeConfigurationMPNList.Clear();
                        currentRecipeConfigurationMPNList.Clear();

                        //((IEnumerable<string>)iNGEAR_Opc.Read(string.Format(mPNListTagName,
                        //    RecipeConfigurationComboBox.SelectedIndex, 0), typeof(string),
                        //    recipeConfigurationList[RecipeConfigurationComboBox.SelectedIndex].MPNMaxCount)).ToList().ForEach(x =>
                        //    {
                        //        try
                        //        {
                        //            editRecipeConfigurationMPNList.Add(new RecipeConfigurationMPNModel
                        //            {
                        //                MPN = x
                        //            });

                        //            currentRecipeConfigurationMPNList.Add(new RecipeConfigurationMPNModel
                        //            {
                        //                MPN = x
                        //            });
                        //        }
                        //        catch (Exception exception)
                        //        {
                        //            FileLogger.logError(exception.Message, exception.ToString());
                        //        }
                        //    });

                        for(int i = 0; i < recipeConfigurationList[RecipeConfigurationComboBox.SelectedIndex].MPNMaxCount; i++)
                        {
                            try
                            {
                                string mpn = iNGEAR_Opc.Read<string>(string.Format(mPNListTagName, RecipeConfigurationComboBox.SelectedIndex, i));
                                editRecipeConfigurationMPNList.Add(new RecipeConfigurationMPNModel
                                {
                                    MPN = mpn
                                });

                                currentRecipeConfigurationMPNList.Add(new RecipeConfigurationMPNModel
                                {
                                    MPN = mpn
                                });
                            }
                            catch(Exception exception)
                            {
                                FileLogger.logError(exception.Message, exception.ToString());
                            }
                        }

                        EditMPNListDataGrid.Items.Refresh();
                        CurrentMPNListDataGrid.Items.Refresh();
                    }
                }
                catch(Exception exception)
                {
                    FileLogger.logError(exception.Message, exception.ToString());
                }
            }));
        }

        private void saveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            bool value = true;

            try
            {
                for(int i = 0; i < editRecipeConfigurationMPNList.Count; i++)
                {
                    try
                    {
                        if(!iNGEAR_Opc.WriteStringTag(string.Format(mPNListTagName,
                            RecipeConfigurationComboBox.SelectedIndex, i),
                            editRecipeConfigurationMPNList[i].MPN))
                        {
                            value = false;
                            break;
                        }
                    }
                    catch(Exception exception)
                    {
                        FileLogger.logError(exception.Message, exception.ToString());
                    }
                }
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }

            try
            {
                if(value)
                {
                    int currentRecipeConfigurationComboBoxSelectedIndex = RecipeConfigurationComboBox.SelectedIndex;
                    RecipeConfigurationComboBox.SelectedIndex = -1;
                    RecipeConfigurationComboBox.SelectedIndex = currentRecipeConfigurationComboBoxSelectedIndex;
                }

                MessageBox.Show(value == true ? "Settings saved." : "Failed to save settings.");
            }
            catch(Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }

        #endregion PrivateCommandMethods

        #region PublicMethods

        public void Dispose()
        {
        }

        #endregion PublicMethods
    }
}