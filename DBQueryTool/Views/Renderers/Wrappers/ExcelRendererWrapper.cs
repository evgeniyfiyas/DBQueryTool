using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Report;

namespace DBQueryTool.Views.Renderers.Wrappers
{
    class ExcelRendererWrapper : IRendererWrapper
    {
        public XLTemplate Template { get; private set; }

        public ExcelRendererWrapper(XLTemplate template)
        {
            Template = template;
        }
    }
}
