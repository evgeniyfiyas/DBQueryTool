using System.Collections.Generic;

namespace DBQueryTool.Core.Formatters
{
    interface IFormatter<T>
    {
        List<object> Format(T formattable);
    }
}
