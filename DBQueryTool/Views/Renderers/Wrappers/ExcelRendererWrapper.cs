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
        public string OutputFile { get; private set; }
        public XLTemplate Template { get; private set; }

        public ExcelRendererWrapper(string outputFile, XLTemplate template)
        {
            OutputFile = outputFile;
            Template = template;
        }
    }
}
