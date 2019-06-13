using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NLog;

namespace DBQueryTool.Views
{
    public class WindowBase : Window
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    }
}
