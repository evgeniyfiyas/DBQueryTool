using System.Data;
using DBQueryTool.ConnectionProcessor.DataProviders;
using DBQueryTool.DataAccess.Service.Impl;
using DBQueryTool.DataAccess.Service.Interfaces;
using DBQueryTool.ReportProcessor.Formatters;
using DBQueryTool.ReportProcessor.Renderers;
using NLog;
using StructureMap;

namespace DBQueryTool.DependencyResolver
{
    public static class DependencyResolver
    {
        public static Container Container { get; set; } = new Container(_ =>
        {
            _.For<ILogger>().Use<Utils.Logger.Logger>();
            _.For<IDataProvider>().Use<MsAccessDataProvider>();
            _.For<IDataProvider>().Use<MsSqlServerDataProvider>();
            _.For<IDataProvider>().Use<MySqlDataProvider>();
            _.For<IDataProvider>().Use<PostgreSqlDataProvider>();
            _.For<IFormatter<DataTable>>().Use<MsAccessFormatter>();
            _.For<IRenderer>().Use<ExcelRenderer>();
            _.For<IReportService>().Use<ReportService>();
            _.For<ITemplateService>().Use<TemplateService>();
            _.For<ITemplateTypeService>().Use<TemplateTypeService>();
            _.For<IUserService>().Use<UserService>();
        });
    }
}
