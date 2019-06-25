using System;
using System.Windows;
using DBQueryTool.DependencyResolver;
using DBQueryTool.Views.Windows;
using NLog;

namespace DBQueryTool.Views
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILogger Logger = DependencyResolver.DependencyResolver.Container.GetInstance<ILogger>();

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

            var loginWindow = DependencyResolver.DependencyResolver.Container.GetInstance<LoginWindow>();
            loginWindow.Show();
        }
    }
}
