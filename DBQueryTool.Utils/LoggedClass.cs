using NLog;

namespace DBQueryTool.Utils
{
    public abstract class LoggedClass
    {
        protected static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
    }
}
