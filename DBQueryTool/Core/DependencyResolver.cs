using DBQueryTool.Core.Formatters;
using StructureMap;
using System.Data;

namespace DBQueryTool.Core
{
    static class DependencyResolver
    {
        public static Container Container { get; set; } = new Container(_ =>
        {
            _.For<IFormatter<DataTable>>().Use<MSAccessFormatter>();
        });
    }
}
