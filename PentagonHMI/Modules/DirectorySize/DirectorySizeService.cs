using System;
using System.Data;
using System.IO;
using System.Linq;
using Utilities;

namespace PentagonHMI
{
    public class DirectorySizeService
    {
        #region PrivateFields
        private LogicClasses.Main main = null;
        private DirectoryInfo directoryInfo = null;
        private Logix.Tag directorySizeTag = null;
        #endregion

        #region PublicFields
        public bool EnableDirectorySizeService = false;
        public string DirectoryPath = string.Empty;
        public string DirectorySizeTagName = string.Empty;
        #endregion

        #region Constructors
        /// <summary>
        /// INSERT INTO [gdb].[dbo].[DirectorySizeService] (Name,Value)
        /// 
        /// VALUES
        /// 
        /// ('EnableDirectorySizeService','false'),
        /// ('DirectoryPath',''),
        /// ('DirectorySizeTagName','')
        /// </summary>
        /// <param name="_main"></param>
        public DirectorySizeService(LogicClasses.Main _main)
        {
            try
            {
                main = _main;

                DataTable dataTable = main.SQLer.Exec_DTSelect($"SELECT * FROM [{nameof(DirectorySizeService)}]");
                Func<string, object> getItem = (x) => dataTable.Select().Where(y => y["Name"].ToString().ToUpper() == x.ToUpper()).First()["Value"];
                
                EnableDirectorySizeService = Convert.ToBoolean(getItem(nameof(EnableDirectorySizeService)));
                DirectoryPath = getItem(nameof(DirectoryPath)).ToString();
                DirectorySizeTagName = getItem(nameof(DirectorySizeTagName)).ToString();
                
                if (EnableDirectorySizeService)
                {
                    directoryInfo = new DirectoryInfo(DirectoryPath);
                    directorySizeTag = new Logix.Tag
                    {
                        Name = DirectorySizeTagName,
                        DataType = Logix.Tag.ATOMIC.INT
                    };

                    main.OnAlwaysUpdate += main_OnAlwaysUpdate;
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion
        
        #region PrivateEventMethods
        private void main_OnAlwaysUpdate()
        {
            try
            {
                if (EnableDirectorySizeService)
                {
                    long sizeOfDir = directorySize(directoryInfo, true);
                    directorySizeTag.Value = string.Format("{0:N0}", ((double)sizeOfDir) / (1024 * 1024));
                    main.MyPLC.WriteTag(directorySizeTag);
                }
            }
            catch (Exception exception)
            {
                FileLogger.logError(exception.Message, exception.ToString());
            }
        }
        #endregion

        #region PrivateMethods
        private long directorySize(DirectoryInfo dInfo, bool includeSubDir)
        {
            long totalSize = dInfo.EnumerateFiles().Sum(file => file.Length);
            
            if (includeSubDir)
            {
                totalSize += dInfo.EnumerateDirectories().Sum(dir => directorySize(dir, true));
            }
            
            return totalSize;
        }
        #endregion
    }
}