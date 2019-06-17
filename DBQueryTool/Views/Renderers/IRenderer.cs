using ClosedXML.Report;
using System.Collections.Generic;

namespace DBQueryTool.Views.Renderers
{
    public interface IRenderer
    {
        bool Render(XLTemplate template, Dictionary<string, object> optionalParameters = null);
    }
}
