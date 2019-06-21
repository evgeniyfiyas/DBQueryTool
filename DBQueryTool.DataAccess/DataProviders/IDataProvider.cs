using System.Data;

namespace DBQueryTool.DataAccess.DataProviders
{
    public interface IDataProvider
    {
        string VisibleName { get; set; }

        bool? TestConnection();
        DataTable Query(string query);
        void Build(string queryString);
    }
}
