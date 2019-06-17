using System.Data;
using DBQueryTool.Core.Formatters;
using DBQueryTool.Core.Renderers;
using DBQueryTool.Models.DataProviders;
using StructureMap;

namespace DBQueryTool.Core
{
    public static class DependencyResolver
    {
        public static Container Container { get; set; } = new Container(_ =>
        {
            _.For<IFormatter<DataTable>>().Use<MsAccessFormatter>();
            _.For<IDataProvider>().Use<MsAccessDataProvider>();
            _.For<IRenderer>().Use<ExcelRenderer>();
        });
    }
}
