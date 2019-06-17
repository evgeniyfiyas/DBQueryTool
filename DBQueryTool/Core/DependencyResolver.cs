using DBQueryTool.Core.Formatters;
using DBQueryTool.Models.DataProviders;
using DBQueryTool.Views.Renderers;
using StructureMap;
using System.Data;

namespace DBQueryTool.Core
{
    static class DependencyResolver
    {
        public static Container Container { get; set; } = new Container(_ =>
        {
            _.For<IFormatter<DataTable>>().Use<MSAccessFormatter>();
            _.For<IDataProvider>().Use<MsAccessDataProvider>();
            _.For<IRenderer>().Use<ExcelRenderer>();
        });
    }
}
