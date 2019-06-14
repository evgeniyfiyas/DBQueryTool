using System.Collections.Generic;

namespace DBQueryTool.Core.Formatters
{
    public interface IFormatter<IEnumerable>
    {
        List<object> Format(IEnumerable formattable);
    }
}
