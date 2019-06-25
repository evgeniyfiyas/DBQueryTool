using System.Collections.Generic;
using ClosedXML.Report;

namespace DBQueryTool.ReportProcessor.Renderers
{
    public interface IRenderer
    {
        bool Render(XLTemplate template, Dictionary<string, object> optionalParameters = null);
    }
}
