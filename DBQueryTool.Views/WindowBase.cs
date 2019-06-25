using System.Windows;
using NLog;

namespace DBQueryTool.Views
{
    public class WindowBase : Window
    {
        protected static readonly ILogger Logger = DependencyResolver.DependencyResolver.Container.GetInstance<ILogger>();
    }
}
