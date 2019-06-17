using NLog;
using System;
using System.Windows;
using DBQueryTool.Core;

namespace DBQueryTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public App()
        {
            var currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            Logger.Error("Inner exception : " + ex.InnerException);
            Logger.Error("UnhandledException caught : " + ex.Message);
            Logger.Error("UnhandledException StackTrace : " + ex.StackTrace);
            Logger.Fatal("Runtime terminating: {0}", e.IsTerminating);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = DependencyResolver.Container.GetInstance<MainWindow>();
            mainWindow.Show();
        }
    }
}
