using DBQueryTool.Core.Formatters;
using StructureMap;
using System.Data;
using DBQueryTool.Models.DataProviders;
using DBQueryTool.Views.Renderers;
using DBQueryTool.Views.Renderers.Wrappers;

namespace DBQueryTool.Core
{
    static class DependencyResolver
    {
        public static Container Container { get; set; } = new Container(_ =>
        {
            _.For<IFormatter<DataTable>>().Use<MSAccessFormatter>();
            _.For<IDataProvider>().Use<MsAccessDataProvider>();
            _.For<IRendererWrapper>().Use<ExcelRendererWrapper>();
            _.For<IRenderer<ExcelRendererWrapper>>().Use<ExcelRenderer>();
        });
    }
}
