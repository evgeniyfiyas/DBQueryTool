using System.Collections.Generic;

namespace DBQueryTool.ReportProcessor.Formatters
{
    public interface IFormatter<IEnumerable>
    {
        List<object> Format(IEnumerable formattable);
    }
}
