using System.Collections.Generic;
using ClosedXML.Report;
using DBQueryTool.Utils;
using Microsoft.Win32;

namespace DBQueryTool.Views.Renderers
{
    class ExcelRenderer : LoggedClass, IRenderer
    {
        public bool Render(XLTemplate template, Dictionary<string, object> optionalParameters = null)
        {
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
