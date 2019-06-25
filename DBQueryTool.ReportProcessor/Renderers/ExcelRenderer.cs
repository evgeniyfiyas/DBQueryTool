using System.Collections.Generic;
using ClosedXML.Report;
using Microsoft.Win32;

namespace DBQueryTool.ReportProcessor.Renderers
{
    public class ExcelRenderer : IRenderer
    {
        public bool Render(XLTemplate template, Dictionary<string, object> optionalParameters = null)
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Microsoft Excel Spreadsheet (*.xlsx)|*.xlsx",
                DefaultExt = "xlsx"
            };
            var dialogResult = saveFileDialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                template.Generate();
                template.SaveAs(saveFileDialog.FileName);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
