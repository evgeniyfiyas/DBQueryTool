using System.Collections.Generic;
using ClosedXML.Report;
using DBQueryTool.Utils;
using DBQueryTool.Views.Renderers.Wrappers;
using Microsoft.Win32;
using NLog;

namespace DBQueryTool.Views.Renderers
{
    class ExcelRenderer : LoggedClass, IRenderer<ExcelRendererWrapper>
    {
        public bool Render(ExcelRendererWrapper renderable)
        {
            var template = renderable.Template;

            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Microsoft Excel Spreadsheet (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx"
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                var outputFile = saveFileDialog.FileName;
                
                template.Generate();
                template.SaveAs(outputFile);

                Logger.Info("Successfully exported xls file to: " + outputFile);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
