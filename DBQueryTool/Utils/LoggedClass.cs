using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQueryTool.Utils
{
    // Possible merge with WindowBase?
    // https://ardalis.com/configure-nlog-with-structuremap
    public class LoggedClass
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    }
}
