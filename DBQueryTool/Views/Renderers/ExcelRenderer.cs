using System.Collections.Generic;
using ClosedXML.Report;
using DBQueryTool.Views.Renderers.Wrappers;
using NLog;

namespace DBQueryTool.Views.Renderers
{
    class ExcelRenderer : IRenderer<ExcelRendererWrapper>
    {
        public void Render(ExcelRendererWrapper renderable)
        {
            var template = renderable.Template;
            var outputFile = renderable.OutputFile;

            template.Generate();
            template.SaveAs(outputFile);
        }
    }
}
