using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBQueryTool.Models.DataProviders
{
    public interface IDataProvider
    {
        bool? TestConnection();
        DataTable Query(string query);
        void Build(string queryString);
    }
}
