using NLog;
using System.Windows;

namespace DBQueryTool.Views
{
    public class WindowBase : Window
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    }
}
