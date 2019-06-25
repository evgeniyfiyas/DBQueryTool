using System.Collections.ObjectModel;
using DBQueryTool.DataAccess.Models;
using DBQueryTool.DataAccess.Service.Interfaces.Common;

namespace DBQueryTool.DataAccess.Service.Interfaces
{
    public interface ITemplateService : ICRUD<Template>
    {
        ObservableCollection<Template> GetObservableCollection();
    }
}
