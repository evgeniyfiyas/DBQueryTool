using System.Collections.Generic;
using ClosedXML.Report;
using NLog;

namespace DBQueryTool.Views.Renderers
{
    class ExcelRenderer
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void Render(List<object> renderable, string outputFile, XLTemplate template)
        {
            template.Generate();
            template.SaveAs(outputFile);
            logger.Info("Successfully exported xls file to: " + outputFile);
        }
    }
}
