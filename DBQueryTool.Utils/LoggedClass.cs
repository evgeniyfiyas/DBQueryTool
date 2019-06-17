using NLog;

namespace DBQueryTool.Utils
{
    public abstract class LoggedClass
    {
        protected static readonly Logger Logger = LogManager.GetCurrentClassLogger();
    }
}
