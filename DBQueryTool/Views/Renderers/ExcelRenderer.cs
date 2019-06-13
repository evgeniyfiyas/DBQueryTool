using System.Collections.Generic;
using ClosedXML.Report;
using NLog;

namespace DBQueryTool.Views.Renderers
{
    class ExcelRenderer
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public void Render(string outputFile, XLTemplate template)
        {
            template.Generate();
            template.SaveAs(outputFile);
            Logger.Info("Successfully exported xls file to: " + outputFile);
        }
    }
}
