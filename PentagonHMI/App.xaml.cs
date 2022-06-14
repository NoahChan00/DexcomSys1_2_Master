using System;
using System.Windows;
using Utilities;

namespace PentagonHMI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
                int CurrentProcessID = System.Diagnostics.Process.GetCurrentProcess().Id;
                string CurrentAssemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

                FileLogger.logEvent(GetType().Name, string.Format("HMI Application started ({0}, ID: {1})", CurrentAssemblyName, CurrentProcessID));

                foreach (System.Diagnostics.Process item in System.Diagnostics.Process.GetProcessesByName(CurrentAssemblyName))
                {
                    try
                    {
                        if (item.Id != CurrentProcessID)
                        {
                            FileLogger.logEvent(GetType().Name, string.Format("About to kill ({0}, ID: {1})", CurrentAssemblyName, item.Id));
                            item.Kill();
                        }
                    }
                    catch (Exception ex)
                    {
                        FileLogger.logEvent(GetType().Name, string.Format("Error: Failed to kill ({0}, ID: {1})", CurrentAssemblyName, item.Id));
                        FileLogger.logError(ex.Message, ex.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.logEvent(GetType().Name, "Error while trying to find and kill Previous Process");
                FileLogger.logError(ex.Message, ex.ToString());
            }

            FileLogger.logEvent(GetType().Name, "HMI Application started");
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            /*
             * Date: 2013-02-04
             * Author: Tammie
             * Description: Change Application to HMI Application for easier identification in the log file.
             */
            //logger.Info("Application ended");
            //log.Info("HMI Application ended");
            //logger.Info("HMI Application ended"); 
            //log4net.LogManager.Shutdown();
            //System.Diagnostics.Process[] proc = System.Diagnostics.Process.GetProcesses();
            //foreach (System.Diagnostics.Process item in System.Diagnostics.Process.GetProcessesByName("PentagonHMI.vshost"))
            //{
            //    item.Kill();
            //}

            //foreach (System.Diagnostics.Process item in System.Diagnostics.Process.GetProcessesByName("PentagonHMI"))
            //{
            //    item.Kill();
            //}
        }

        /*
         * Date: 2022-06-13
         * Author: Leon
         * Description: Log Unhandled Exception, found on dispatcher and current domain
         * PS: Someone remove log4net usage 
         */
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            FileLogger.logError(e.Exception.ToString(), "On dispatcher unhandled exception - Unknown");
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                FileLogger.logError(exception.ToString(), "Current Domain unhandled exception - Unknown");
            }
        }
    }
}
