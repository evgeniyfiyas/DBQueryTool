using System.Collections.Generic;
using ClosedXML.Report;

namespace DBQueryTool.Views.Renderers
{
    class ExcelRenderer
    {
        public void Render(List<object> renderable, string outputFile, XLTemplate template)
        {
            template.Generate();
            template.SaveAs(outputFile);
        }
    }
}
