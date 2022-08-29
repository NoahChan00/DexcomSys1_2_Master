using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data;
using System.Globalization;
using System.IO;
using Library;
using System.Reflection;
using System.Diagnostics;
using Utilities;

namespace PentagonHMI.ChildControls
{
    public partial class ucEventLog : UserControl, IDisposable
    {
        CultureInfo CN = new CultureInfo("zh-CN");

        private VanillaDB.DataDBCall DBCall;
        string strEventLog => "LogPage";
        private string DefaultPath { get; set; }
        private string currentPath { get; set; } = "";

        PentagonHMI.LogicClasses.Main _Main;

        public ucEventLog(ref LogicClasses.Main MainConnection)
        {
            InitializeComponent();

            _Main = MainConnection;

            DefaultPath = FileLogger.DefaultLocation;

            switch (Classes.GlobalFunctions.ProjectType)
            {
                case ProjectType.DIMM:
                    RamBarcodeIcon.Visibility = Visibility.Visible;
                    VisionResultIcon.Visibility = Visibility.Visible;
                    break;
                case ProjectType.HDD:
                case ProjectType.ARCADIA:
                    RamBarcodeIcon.Visibility = Visibility.Collapsed;
                    break;
                case ProjectType.VTC:
                    RackBarcodeIcon.Visibility = Visibility.Visible;
                    ZoneBarcodeIcon.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

            TorqueDriverResultStackPanel.Visibility = _Main.HasTorqueDriver ? Visibility.Visible : Visibility.Collapsed;

            string Connstr = Properties.Settings.Default.DatabaseConnectionString.ToString();
            DBCall = new VanillaDB.DataDBCall(Connstr);

            datePickerUPH.DisplayDate = DateTime.Now;
            datePickerUPH.Text = DateTime.Now.ToString();


            datePickerLotSummary.DisplayDate = DateTime.Now;
            datePickerLotSummary.Text = DateTime.Now.ToString();

            datePickerAlarm.DisplayDate = DateTime.Now;
            datePickerAlarm.Text = DateTime.Now.ToString();

            datePickerOEE.DisplayDate = DateTime.Now;
            datePickerOEE.Text = DateTime.Now.ToString();

            datePickerEvent.DisplayDate = DateTime.Now;
            datePickerEvent.Text = DateTime.Now.ToString();

            datePickerPLC.DisplayDate = DateTime.Now;
            datePickerPLC.Text = DateTime.Now.ToString();

            datePickerReject.DisplayDate = DateTime.Now;
            datePickerReject.Text = datePickerReject.DisplayDate.ToString();

            datePickerRamBarcode.DisplayDate = DateTime.Now;
            datePickerRamBarcode.Text = datePickerRamBarcode.DisplayDate.ToString();

            datePickerZoneBarcode.DisplayDate = DateTime.Now;
            datePickerZoneBarcode.Text = datePickerZoneBarcode.DisplayDate.ToString();

            datePickerRackBarcode.DisplayDate = DateTime.Now;
            datePickerRackBarcode.Text = datePickerRackBarcode.DisplayDate.ToString();

            // !! 
            datePickerTestCSV.DisplayDate = DateTime.Now;
            datePickerTestCSV.Text = datePickerTestCSV.DisplayDate.ToString();

            datePickerTestStn.DisplayDate = DateTime.Now;
            datePickerTestStn.Text = datePickerTestStn.DisplayDate.ToString();

            datePickerLaserStn.DisplayDate = DateTime.Now;
            datePickerLaserStn.Text = datePickerLaserStn.DisplayDate.ToString();

            datePickerUnldStn.DisplayDate = DateTime.Now;
            datePickerUnldStn.Text = datePickerUnldStn.DisplayDate.ToString();

            datePickerTnRStn.DisplayDate = DateTime.Now;
            datePickerTnRStn.Text = datePickerTnRStn.DisplayDate.ToString();

        }

        #region old
        //private DataTable loadAlarmTable()
        //{
        //    DataTable dtCsv = new DataTable();
        //    try
        //    {
        //        string Fulltext = "";
        //        string strPath = DefaultPath + Path.DirectorySeparatorChar + "Alarm";
        //        string Filename = "Alarm_" + Convert.ToDateTime(datePickerAlarm.Text).ToString("yyyy-MMM-dd") + ".txt";
        //        string strFilePath = strPath + Path.DirectorySeparatorChar + Filename;
        //        if (File.Exists(strFilePath))
        //        {
        //            using (CsvFileReader cFR = new CsvFileReader(strFilePath))
        //            {
        //                while (!cFR.EndOfStream)
        //                {
        //                    Fulltext = cFR.ReadToEnd().ToString().Replace('\r', ' ');//.Replace(',' ,'-').Replace('-',' '); //read full file text 
        //                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
        //                    for (int i = 0; i < rows.Count() - 1; i++)
        //                    {
        //                        string[] rowValues = rows[i].Split(rows[i].Contains('|') ? '|' : ',');
        //                        if (i == 0)
        //                        {
        //                            for (int j = 0; j < rowValues.Count(); j++)
        //                            {
        //                                dtCsv.Columns.Add(rowValues[j].ToString().Trim()); //add headers  
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DataRow dr = dtCsv.NewRow();
        //                            for (int k = 0; k < rowValues.Count(); k++)
        //                            {
        //                                dr[k] = rowValues[k].ToString();
        //                            }
        //                            dtCsv.Rows.Add(dr); //add other rows  
        //                        }
        //                    }

        //                }
        //            }
        //            return dtCsv;
        //        }
        //        else
        //        {
        //            ////MessageBox.Show("No Record");
        //            return dtCsv;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.FileLogger.logError(ex.Message, ex.ToString());
        //        return dtCsv;
        //    }

        //}

        //private DataTable loadOEETable()
        //{
        //    DataTable dtCsv = new DataTable();
        //    try
        //    {
        //        string Fulltext = "";
        //        string strPath = DefaultPath + System.IO.Path.DirectorySeparatorChar + @"OEE";
        //        string Filename = "OEE_" + Convert.ToDateTime(datePickerOEE.Text).ToString("yyyy-MMM-dd") + ".txt";
        //        string strFilePath = strPath + System.IO.Path.DirectorySeparatorChar + Filename;
        //        if (File.Exists(strFilePath))
        //        {
        //            using (CsvFileReader cFR = new CsvFileReader(strFilePath))
        //            {
        //                while (!cFR.EndOfStream)
        //                {
        //                    Fulltext = cFR.ReadToEnd().ToString().Replace('\r', ' ');//.Replace(',' ,'-').Replace('-',' '); //read full file text 
        //                    string[] rows = Fulltext.Split('\n'); //split full file text into rows  
        //                    for (int i = 0; i < rows.Count() - 1; i++)
        //                    {
        //                        string[] rowValues = rows[i].Split(','); ;
        //                        if (i == 0)
        //                        {
        //                            for (int j = 0; j < rowValues.Count(); j++)
        //                            {
        //                                dtCsv.Columns.Add(rowValues[j].ToString().Trim()); //add headers  
        //                            }
        //                        }
        //                        else
        //                        {
        //                            DataRow dr = dtCsv.NewRow();
        //                            for (int k = 0; k < rowValues.Count(); k++)
        //                            {
        //                                dr[k] = rowValues[k].ToString();
        //                            }
        //                            dtCsv.Rows.Add(dr); //add other rows  
        //                        }
        //                    }

        //                }
        //            }
        //            return dtCsv;
        //        }
        //        else
        //        {
        //            ////MessageBox.Show("No Record");
        //            return dtCsv;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Utilities.FileLogger.logError(ex.Message, ex.ToString());
        //        return dtCsv;
        //    }

        //}
        #endregion

        private DataTable loadPLCTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {

                string Fulltext = "";
                string strPath = DefaultPath + System.IO.Path.DirectorySeparatorChar + @"Event";
                string Filename = "PlcEvent_" + Convert.ToDateTime(datePickerPLC.Text).ToString("yyyy-MMM-dd") + ".txt";
                string strFilePath = strPath + System.IO.Path.DirectorySeparatorChar + Filename;
                if (File.Exists(strFilePath))
                {
                    using (CsvFileReader cFR = new CsvFileReader(strFilePath))
                    {
                        while (!cFR.EndOfStream)
                        {
                            Fulltext = cFR.ReadToEnd().ToString().Replace('\r', ' ');//.Replace(',' ,'-').Replace('-',' '); //read full file text 
                            string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                            for (int i = 0; i < rows.Count() - 1; i++)
                            {
                                string[] rowValues = rows[i].Split(','); ;
                                if (i == 0)
                                {
                                    for (int j = 0; j < rowValues.Count(); j++)
                                    {
                                        dtCsv.Columns.Add(rowValues[j].ToString().Trim()); //add headers  
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtCsv.NewRow();
                                    for (int k = 0; k < rowValues.Count(); k++)
                                    {
                                        dr[k] = rowValues[k].ToString();
                                    }
                                    dtCsv.Rows.Add(dr); //add other rows  
                                }
                            }

                        }
                    }
                    return dtCsv;
                }
                else
                {
                    ////MessageBox.Show("No Record");
                    return dtCsv;
                }

            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }

        }

        private DataTable loadVisionTable()
        {

            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerVisionResult.Text).ToString("yyyy-MMM"), "VisionResult");
                string Filename = $"VisionResult_{Convert.ToDateTime(datePickerVisionResult.Text).ToString("yyyyMMdd")}.txt";
                string strFilePath = Path.Combine(LogsPath, Filename);
                currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string[] lines = File.ReadAllLines(strFilePath);
                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] s = lines[i].Split(',');

                        if (i == 0)
                        {
                            for (int j = 0; j < s.Length; j++)
                            {
                                dtCsv.Columns.Add(s[j]);
                            }
                        }
                        else
                        {
                            DataRow dataRow = dtCsv.NewRow();
                            for (int j = 0; j < s.Length; j++)
                            {
                                dataRow[j] = s[j];
                            }
                            dtCsv.Rows.Add(dataRow);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }
        }

        private DataTable loadProduction()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string Fulltext;
                string LogsPath = Path.Combine(FileLogger.DefaultLocation_Time, "AutoCOCO_Production_Report");
                string Filename = $"AutoCOCOProductionReport_{Convert.ToDateTime(datePickerProduction.Text).ToString("yyyyMMdd")}.txt";
                string strFilePath = Path.Combine(LogsPath, Filename);
                if (File.Exists(strFilePath))
                {
                    using (CsvFileReader cFR = new CsvFileReader(strFilePath))
                    {
                        while (!cFR.EndOfStream)
                        {
                            Fulltext = cFR.ReadToEnd().ToString().Replace('\r', ' ');//.Replace(',' ,'-').Replace('-',' '); //read full file text 
                            string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                            for (int j = 1; j <= 72; j++)
                                dtCsv.Columns.Add(j.ToString()); //add headers  

                            for (int i = 0; i < rows.Count() - 1; i++)
                            {
                                string[] rowValues = rows[i].Split(','); ;
                                DataRow dr = dtCsv.NewRow();
                                for (int k = 0; k < rowValues.Count(); k++)
                                    dr[k] = rowValues[k].ToString();
                                dtCsv.Rows.Add(dr); //add other rows  
                            }
                        }
                    }
                    return dtCsv;
                }
                else
                {
                    ////MessageBox.Show("No Record");
                    return dtCsv;
                }

            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }

        }

        private DataTable loadAlarmTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerAlarm.Text).ToString("yyyy-MMM"), "Alarm");
                string Filename = $"Alarm_{Convert.ToDateTime(datePickerAlarm.Text).ToString("yyyy-MMM-dd")}.txt";
                string strFilePath = Path.Combine(LogsPath, Filename);
                    currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }

        }

        private DataTable loadRamBarcodeTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerRamBarcode.Text).ToString("yyyy-MMM"), "RamBarcodeInstalled");
                string Filename = $"RamBarcodeInstalled_{Convert.ToDateTime(datePickerRamBarcode.Text).ToString("yyyyMMdd")}.txt";
                string strFilePath = Path.Combine(LogsPath, Filename);
                    currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }

        }

        private DataTable loadZoneBarcodeTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerZoneBarcode.Text).ToString("yyyy-MMM"), "Barcode/InputZoneBarcodeLog");
                string Filename = $"InputZoneBarcode_{Convert.ToDateTime(datePickerZoneBarcode.Text).ToString("yyyyMMdd")}.txt";
                string strFilePath = Path.Combine(LogsPath, Filename);
                    currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim(),typeof(string));
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }

        }

        private DataTable loadRackBarcodeTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerRackBarcode.Text).ToString("yyyy-MMM"), "Barcode/RackBarcodeLog");
                string Filename = $"RackBarcode_{Convert.ToDateTime(datePickerRackBarcode.Text).ToString("yyyyMMdd")}.txt";
                string strFilePath = Path.Combine(LogsPath, Filename);

                    currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {

                        }
                        // Rackbarcode first row is header
                        else if (stringList[1] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }

        }

        private DataTable loadOEETable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerOEE.Text).ToString("yyyy-MMM"), "OEE");
                string Filename = $"OEE_{Convert.ToDateTime(datePickerOEE.Text).ToString("yyyy-MMM-dd")}.txt";
                string strFilePath = Path.Combine(LogsPath, Filename);
                    currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }

        }

        private DataTable loadLotSummaryTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                //string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerLotSummary.Text).ToString("yyyy-MMM"), "LotSummary");
                //string Filename = $"LotSummary_{Convert.ToDateTime(datePickerLotSummary.Text).ToString("yyyy-MMM-dd")}.csv";
                //string strFilePath = Path.Combine(LogsPath, Filename);
                //currentPath = strFilePath;
                //if (File.Exists(strFilePath))
                //{
                //    string allLine = "";
                //    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                //    using (var sr = new StreamReader(fs))
                //    {
                //        allLine = sr.ReadToEnd();
                //        sr.Close();
                //        fs.Close();
                //    }

                //    string[] stringList = allLine.Split('\n');
                //    foreach (string row in stringList)
                //    {
                //        string[] cell = row.Split(',');
                //        if (stringList[0] == row)
                //        {
                //            foreach (string column in cell)
                //            {
                //                dtCsv.Columns.Add(column.Trim());
                //            }
                //        }
                //        else
                //        {
                //            DataRow dr = dtCsv.NewRow();
                //            for (int i = 0; i < cell.Length; i++)
                //            {
                //                dr[i] = cell[i];
                //            }
                //            dtCsv.Rows.Add(dr);
                //        }
                //    }
                //}
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }
        }
        private DataTable loadTestCSVTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerTestCSV.Text).ToString("yyyy-MMM"), "TestCSV");
                string Filename = $"TestCSV_{Convert.ToDateTime(datePickerTestCSV.Text).ToString("yyyy-MMM-dd")}.csv";
                string strFilePath = Path.Combine(LogsPath, Filename);
                currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }
        }

        private DataTable loadTestStnTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerTestStn.Text).ToString("yyyy-MMM"), "TesterStnUnitTracker");
                string Filename = $"TesterStnUnitTracker_{Convert.ToDateTime(datePickerTestStn.Text).ToString("yyyy-MMM-dd")}.csv";
                string strFilePath = Path.Combine(LogsPath, Filename);
                currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }
        }

        private DataTable loadLaserStnTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerLaserStn.Text).ToString("yyyy-MMM"), "LaserStnUnitTracker");
                string Filename = $"LaserStnUnitTracker_{Convert.ToDateTime(datePickerLaserStn.Text).ToString("yyyy-MMM-dd")}.csv";
                string strFilePath = Path.Combine(LogsPath, Filename);
                currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }
        }

        private DataTable loadUnldStnTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerUnldStn.Text).ToString("yyyy-MMM"), "UnldStnUnitTracker");
                string Filename = $"UnldStnUnitTracker_{Convert.ToDateTime(datePickerUnldStn.Text).ToString("yyyy-MMM-dd")}.csv";
                string strFilePath = Path.Combine(LogsPath, Filename);
                currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }
        }

        private DataTable loadTnRStnTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerTnRStn.Text).ToString("yyyy-MMM"), "TnRStnUnitTracker");
                string Filename = $"TnRStnUnitTracker_{Convert.ToDateTime(datePickerTnRStn.Text).ToString("yyyy-MMM-dd")}.csv";
                string strFilePath = Path.Combine(LogsPath, Filename);
                currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }
        }

        private DataTable loadRejectTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerReject.Text).ToString("yyyy-MMM"), Classes.GlobalFunctions.ProjectType == ProjectType.VTC ? "TrayRejectReasonLog" : "RejectReason");
                string Filename = $"RejectReason_{Convert.ToDateTime(datePickerReject.Text).ToString("yyyyMMdd")}.txt";
                string strFilePath = Path.Combine(LogsPath, Filename);
                currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;

                    //using (CsvFileReader cFR = new CsvFileReader(strFilePath))
                    //{
                    //    while (!cFR.EndOfStream)
                    //    {
                    //        Fulltext = cFR.ReadToEnd().ToString().Replace('\r', ' ');//.Replace(',' ,'-').Replace('-',' '); //read full file text 
                    //        string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                    //        for (int i = 0; i < rows.Count() - 1; i++)
                    //        {
                    //            string[] rowValues = rows[i].Split(','); ;
                    //            if (i == 0)
                    //            {
                    //                for (int j = 0; j < rowValues.Count(); j++)
                    //                {
                    //                    dtCsv.Columns.Add(rowValues[j].ToString().Trim()); //add headers  
                    //                }
                    //            }
                    //            else
                    //            {
                    //                DataRow dr = dtCsv.NewRow();
                    //                for (int k = 0; k < rowValues.Count(); k++)
                    //                {
                    //                    dr[k] = rowValues[k].ToString();
                    //                }
                    //                dtCsv.Rows.Add(dr); //add other rows  
                    //            }
                    //        }
                    //    }
                    //}
                    //return dtCsv;
                ////}
                //else
                //{
                //    //MessageBox.Show("No Record");
                //    return dtCsv;
                //}

            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }

        }

        private DataTable loadEventTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {

                string Fulltext = "";
                string strPath = DefaultPath + System.IO.Path.DirectorySeparatorChar + @"Event";
                string Filename = "Event_" + Convert.ToDateTime(datePickerPLC.Text).ToString("yyyy-MMM-dd") + ".txt";
                string strFilePath = strPath + System.IO.Path.DirectorySeparatorChar + Filename;
                if (File.Exists(strFilePath))
                {
                    using (CsvFileReader cFR = new CsvFileReader(strFilePath))
                    {
                        while (!cFR.EndOfStream)
                        {
                            Fulltext = cFR.ReadToEnd().ToString().Replace('\r', ' ');//.Replace(',' ,'-').Replace('-',' '); //read full file text 
                            string[] rows = Fulltext.Split('\n'); //split full file text into rows  
                            for (int i = 0; i < rows.Count() - 1; i++)
                            {
                                string[] rowValues = rows[i].Split(','); ;
                                if (i == 0)
                                {
                                    for (int j = 0; j < rowValues.Count(); j++)
                                    {
                                        dtCsv.Columns.Add(rowValues[j].ToString().Trim()); //add headers  
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtCsv.NewRow();
                                    for (int k = 0; k < rowValues.Count(); k++)
                                    {
                                        dr[k] = rowValues[k].ToString();
                                    }
                                    dtCsv.Rows.Add(dr); //add other rows  
                                }
                            }

                        }
                    }
                    return dtCsv;
                }
                else
                {
                    //MessageBox.Show("No Record");
                    return dtCsv;
                }

            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }

        }

        public void Dispose()
        {
            if (CN != null)
            {
                CN = null;
            }
        }

        ~ucEventLog()
        {
            Dispose();
        }

        private void DatePickerRamBarcode_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Ram Barcode Log", MethodBase.GetCurrentMethod().ToString());
            gridRamBarcode.ItemsSource = loadRamBarcodeTable().DefaultView;
        }

        private void DatePickerZoneBarcode_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Zone Barcode Log", MethodBase.GetCurrentMethod().ToString());
            gridZoneBarcode.ItemsSource = loadZoneBarcodeTable().DefaultView;
        }

        private void DatePickerRackBarcode_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Rack Barcode Log", MethodBase.GetCurrentMethod().ToString());
            gridRackBarcode.ItemsSource = loadRackBarcodeTable().DefaultView;
        }

        private void DatePickerAlarm_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Alarm Log", MethodBase.GetCurrentMethod().ToString());
            gridAlarmHistory.ItemsSource = loadAlarmTable().DefaultView;
        }

        private void DatePickerReject_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Reject Log", MethodBase.GetCurrentMethod().ToString());
            gridRejectHistory.ItemsSource = loadRejectTable().DefaultView;
        }

        private void DatePickerOEE_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve OEE Log", MethodBase.GetCurrentMethod().ToString());
            gridOEE.ItemsSource = loadOEETable().DefaultView;
        }

        // !!
        private void DatePickerTestCSV_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve TestCSV Log", MethodBase.GetCurrentMethod().ToString());
            gridTestCSV.ItemsSource = loadTestCSVTable().DefaultView;
        }

        private void DatePickerTestStn_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Test Station Log", MethodBase.GetCurrentMethod().ToString());
            gridTestStn.ItemsSource = loadTestStnTable().DefaultView;
        }

        private void DatePickerLaserStn_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Laser Station Log", MethodBase.GetCurrentMethod().ToString());
            gridLaserStn.ItemsSource = loadLaserStnTable().DefaultView;
        }

        private void DatePickerUnldStn_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Unld Station Log", MethodBase.GetCurrentMethod().ToString());
            gridUnldStn.ItemsSource = loadUnldStnTable().DefaultView;
        }

        private void DatePickerTnRStn_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve TnR Station Log", MethodBase.GetCurrentMethod().ToString());
            gridTnRStn.ItemsSource = loadTnRStnTable().DefaultView;
        }

        private void btnAlarmSubmit_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Alarm Log", MethodBase.GetCurrentMethod().ToString());
            gridAlarmHistory.ItemsSource = loadAlarmTable().DefaultView;
        }
        private void btnOEESubmit_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve OEE Log", MethodBase.GetCurrentMethod().ToString());
            gridOEE.ItemsSource = loadOEETable().DefaultView;
        }
        private void btnPLCSubmit_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve PLC Log", MethodBase.GetCurrentMethod().ToString());
            gridPLCEvent.ItemsSource = loadPLCTable().DefaultView;
        }

        private void btnVisionResult_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Vision Result Log", MethodBase.GetCurrentMethod().ToString());
            gridVisionResult.ItemsSource = loadVisionTable().DefaultView;
        }

        private void BtnEventSubmit_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Event Log", MethodBase.GetCurrentMethod().ToString());
            gridEventHistory.ItemsSource = loadEventTable().DefaultView;
        }

        private void btnProduction_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Vision Result Log", MethodBase.GetCurrentMethod().ToString());
            gridProduction.ItemsSource = loadProduction().DefaultView;
        }

        private void DatePickerUPH_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            string errMsg = "";
            DateTime fromDate = Convert.ToDateTime(datePickerUPH.SelectedDate);
            DataTable dt = DBCall.Select_UPH(_Main.StationID, fromDate.Day.ToString(), fromDate.Month.ToString(), fromDate.Year.ToString(), ref errMsg);

            if (dt != null)
            {
                gridUPHEvent.ItemsSource = dt.DefaultView;
            }
        }

        private void StackPanel_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel SP = (StackPanel)sender;
            Utilities.FileLogger.logButton(strEventLog, $"Select Log {SP.Tag.ToString().ToUpper()}", MethodBase.GetCurrentMethod().ToString());
            switch (SP.Tag.ToString().ToUpper())
            {
                case "ALARM":
                    //item = Path.Combine(FileLogger.DefaultLocation_Time, "Alarm");
                    //if (!Directory.Exists(item))
                    //    Directory.CreateDirectory(item);
                    //Process.Start(item);
                    datePickerAlarm.DisplayDate = DateTime.Now;
                    datePickerAlarm.Text = datePickerAlarm.DisplayDate.ToString();
                    AlarmHistoryTab.Visibility = Visibility.Visible;
                    AlarmHistoryTab.IsSelected = true;
                    DatePickerAlarm_SelectedDateChanged(null, null);
                    break;
                case "PLC":
                    PLCEventTab.Visibility = Visibility.Visible;
                    PLCEventTab.IsSelected = true;
                    break;
                case "OEE":
                    //item = Path.Combine(FileLogger.DefaultLocation_Time, "OEE");
                    //if (!Directory.Exists(item))
                    //    Directory.CreateDirectory(item);
                    //Process.Start(item);
                    datePickerOEE.DisplayDate = DateTime.Now;
                    datePickerOEE.Text = datePickerOEE.DisplayDate.ToString();
                    OEETab.Visibility = Visibility.Visible;
                    OEETab.IsSelected = true;
                    DatePickerOEE_SelectedDateChanged(null, null);
                    break;
                case "TESTCSV": 
                    datePickerTestCSV.DisplayDate = DateTime.Now;
                    datePickerTestCSV.Text = datePickerTestCSV.DisplayDate.ToString();
                    TestCSVTab.Visibility = Visibility.Visible;
                    TestCSVTab.IsSelected = true;
                    DatePickerTestCSV_SelectedDateChanged(null, null);
                    break;
                case "TESTSTN":
                    datePickerTestStn.DisplayDate = DateTime.Now;
                    datePickerTestStn.Text = datePickerTestStn.DisplayDate.ToString();
                    TestStnTab.Visibility = Visibility.Visible;
                    TestStnTab.IsSelected = true;
                    DatePickerTestStn_SelectedDateChanged(null, null);
                    break;
                case "LASERSTN":
                    datePickerLaserStn.DisplayDate = DateTime.Now;
                    datePickerLaserStn.Text = datePickerLaserStn.DisplayDate.ToString();
                    LaserStnTab.Visibility = Visibility.Visible;
                    LaserStnTab.IsSelected = true;
                    DatePickerLaserStn_SelectedDateChanged(null, null);
                    break;
                case "UNLDSTN":
                    datePickerUnldStn.DisplayDate = DateTime.Now;
                    datePickerUnldStn.Text = datePickerUnldStn.DisplayDate.ToString();
                    UnldStnTab.Visibility = Visibility.Visible;
                    UnldStnTab.IsSelected = true;
                    DatePickerUnldStn_SelectedDateChanged(null, null);
                    break;
                case "TNRSTN":
                    datePickerTnRStn.DisplayDate = DateTime.Now;
                    datePickerTnRStn.Text = datePickerTnRStn.DisplayDate.ToString();
                    TnRStnTab.Visibility = Visibility.Visible;
                    TnRStnTab.IsSelected = true;
                    DatePickerTnRStn_SelectedDateChanged(null, null);
                    break;
                case "EVENT":
                    EventLogTab.Visibility = Visibility.Visible;
                    EventLogTab.IsSelected = true;
                    break;
                case "REJECT":
                    datePickerReject.DisplayDate = DateTime.Now;
                    datePickerReject.Text = datePickerReject.DisplayDate.ToString();
                    RejectHistoryTab.Visibility = Visibility.Visible;
                    RejectHistoryTab.IsSelected = true;
                    DatePickerReject_SelectedDateChanged(null, null);
                    break;
                case "UPH":
                    datePickerUPH.DisplayDate = DateTime.Now;
                    datePickerUPH.Text = datePickerUPH.DisplayDate.ToString();
                    UPHLogTab.Visibility = Visibility.Visible;
                    UPHLogTab.IsSelected = true;
                    DatePickerUPH_SelectedDateChanged(null, null);
                    break;
                case "VISIONRESULT":
                    //VisionResultTab.Visibility = Visibility.Visible;
                    //VisionResultTab.IsSelected = true;
                    //btnVisionResult_Click(null, null);

                    datePickerVisionResult.DisplayDate = DateTime.Now;
                    datePickerVisionResult.Text = datePickerVisionResult.DisplayDate.ToString();
                    VisionResultTab.Visibility = Visibility.Visible;
                    VisionResultTab.IsSelected = true;
                    DatePickerVisionResult_SelectedDateChanged(null, null);
                    break;
                case "PRODUCTION":
                    ProductionTab.Visibility = Visibility.Visible;
                    ProductionTab.IsSelected = true;
                    btnProduction_Click(null, null);
                    break;
                case "RAMBARCODE":
                    datePickerRamBarcode.DisplayDate = DateTime.Now;
                    datePickerRamBarcode.Text = datePickerRamBarcode.DisplayDate.ToString();
                    RamBarcodeTab.Visibility = Visibility.Visible;
                    RamBarcodeTab.IsSelected = true;
                    DatePickerRamBarcode_SelectedDateChanged(null, null);
                    break;
                case "ZONEBARCODE":
                    datePickerZoneBarcode.DisplayDate = DateTime.Now;
                    datePickerZoneBarcode.Text = datePickerZoneBarcode.DisplayDate.ToString();
                    ZoneBarcodeTab.Visibility = Visibility.Visible;
                    ZoneBarcodeTab.IsSelected = true;
                    DatePickerZoneBarcode_SelectedDateChanged(null, null);
                    break;
                case "RACKBARCODE":
                    datePickerRackBarcode.DisplayDate = DateTime.Now;
                    datePickerRackBarcode.Text = datePickerRackBarcode.DisplayDate.ToString();
                    RackBarcodeTab.Visibility = Visibility.Visible;
                    RackBarcodeTab.IsSelected = true;
                    DatePickerRackBarcode_SelectedDateChanged(null, null);
                    break;
                case "TORQUEDRIVERRESULT":
                    datePickerTorqueDriverResult.DisplayDate = DateTime.Now;
                    datePickerTorqueDriverResult.Text = datePickerTorqueDriverResult.DisplayDate.ToString();
                    TorqueDriverResultTabItem.Visibility = Visibility.Visible;
                    TorqueDriverResultTabItem.IsSelected = true;
                    DatePickerTorqueDriverResult_SelectedDateChanged(null, null);
                    break;
                case "DRYRUN":
                    break;
                case "LOTSUMMARY":
                    string LogsPathLotSummary = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerLotSummary.Text).ToString("yyyy-MMM"), "LotSummary");
                    string FilenameLotSummary = $"LotSummary_{Convert.ToDateTime(datePickerLotSummary.Text).ToString("yyyy-MMM-dd")}.csv";
                    currentPath = Path.Combine(LogsPathLotSummary, FilenameLotSummary);

                    if (!string.IsNullOrEmpty(currentPath))
                    {
                        if (File.Exists(currentPath))
                        {
                            FileInfo fi = new FileInfo(currentPath);
                            Process.Start(fi.Directory.FullName);
                        }
                        else
                        {
                            currentPath = currentPath.Substring(0, currentPath.LastIndexOf('\\'));
                            if (!Directory.Exists(currentPath))
                                Directory.CreateDirectory(currentPath);
                            Process.Start(currentPath);
                        }
                    }
                    break;
            }

            LogMenu.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Back Button", MethodBase.GetCurrentMethod().ToString());
            //Back Button
            LogMenu.Visibility = Visibility.Visible;
            RejectHistoryTab.Visibility = Visibility.Collapsed;
            EventLogTab.Visibility = Visibility.Collapsed;
            PLCEventTab.Visibility = Visibility.Collapsed;
            OEETab.Visibility = Visibility.Collapsed;
            TestCSVTab.Visibility = Visibility.Collapsed;
            TestStnTab.Visibility = Visibility.Collapsed;
            LaserStnTab.Visibility = Visibility.Collapsed;
            UnldStnTab.Visibility = Visibility.Collapsed;
            TnRStnTab.Visibility = Visibility.Collapsed;
            AlarmHistoryTab.Visibility = Visibility.Collapsed;
            UPHLogTab.Visibility = Visibility.Collapsed;
            ProductionTab.Visibility = Visibility.Collapsed;
            RamBarcodeTab.Visibility = Visibility.Collapsed;
            ZoneBarcodeTab.Visibility = Visibility.Collapsed;
            RackBarcodeTab.Visibility = Visibility.Collapsed;
            TorqueDriverResultTabItem.Visibility = Visibility.Collapsed;
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Open File Button", MethodBase.GetCurrentMethod().ToString());
         
            if (!string.IsNullOrEmpty(currentPath))
            {
                if (File.Exists(currentPath))
                    Process.Start(currentPath);
                else
                {
                    MessageBox.Show("File not exists.");
                }
            }
        }

        private void OpenFolder_Click(object sender, RoutedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Open Folder Button", MethodBase.GetCurrentMethod().ToString());

            if (!string.IsNullOrEmpty(currentPath))
            {
                if (File.Exists(currentPath))
                {
                    FileInfo fi = new FileInfo(currentPath);
                    Process.Start(fi.Directory.FullName);
                }
                else
                {
                    currentPath = currentPath.Substring(0, currentPath.LastIndexOf('\\'));
                    if (!Directory.Exists(currentPath))
                        Directory.CreateDirectory(currentPath);
                    Process.Start(currentPath);
                }
            }
        }

        private void DatePickerVisionResult_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Vision Result", MethodBase.GetCurrentMethod().ToString());
            gridVisionResult.ItemsSource = loadVisionTable().DefaultView;
        }

        private void DatePickerTorqueDriverResult_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Torque Driver Result", MethodBase.GetCurrentMethod().ToString());
            gridTorqueDriverResult.ItemsSource = loadTorqueDriverResultTable().DefaultView;
        }
        private void DatePickerLotSummary_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Utilities.FileLogger.logButton(strEventLog, "Retrieve Lot Summary Log", MethodBase.GetCurrentMethod().ToString());
            gridLotSummary.ItemsSource = loadLotSummaryTable().DefaultView;
        }

        private DataTable loadTorqueDriverResultTable()
        {
            DataTable dtCsv = new DataTable();
            try
            {
                string LogsPath = Path.Combine(FileLogger.DefaultLocation + "Logs_" + Convert.ToDateTime(datePickerTorqueDriverResult.Text).ToString("yyyy-MMM"), "TorqueDriverResult");
                string Filename = $"TorqueDriverResult_{Convert.ToDateTime(datePickerTorqueDriverResult.Text).ToString("yyyyMMdd")}.txt";
                string strFilePath = Path.Combine(LogsPath, Filename);
                currentPath = strFilePath;
                if (File.Exists(strFilePath))
                {
                    string allLine = "";
                    var fs = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        allLine = sr.ReadToEnd();
                        sr.Close();
                        fs.Close();
                    }

                    string[] stringList = allLine.Split('\n');
                    foreach (string row in stringList)
                    {
                        string[] cell = row.Split(',');
                        if (stringList[0] == row)
                        {
                            foreach (string column in cell)
                            {
                                dtCsv.Columns.Add(column.Trim());
                            }
                        }
                        else
                        {
                            DataRow dr = dtCsv.NewRow();
                            for (int i = 0; i < cell.Length; i++)
                            {
                                dr[i] = cell[i];
                            }
                            dtCsv.Rows.Add(dr);
                        }
                    }
                }
                return dtCsv;
            }
            catch (Exception ex)
            {
                Utilities.FileLogger.logError(ex.Message, ex.ToString());
                return dtCsv;
            }

        }
    }
}
