using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQueryTool.Views.Renderers
{
    interface IRenderer<IRendererWrapper>
    {
        bool Render(IRendererWrapper renderable);
    }
}
