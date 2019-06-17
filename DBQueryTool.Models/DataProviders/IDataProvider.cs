using System.Data;

namespace DBQueryTool.Models.DataProviders
{
    public interface IDataProvider
    {
        bool? TestConnection();
        DataTable Query(string query);
        void Build(string queryString);
    }
}
