using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQueryTool.Utils
{
    public abstract class LoggedClass
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    }
}
