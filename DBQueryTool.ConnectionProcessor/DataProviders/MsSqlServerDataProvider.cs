using System;
using System.Data;
using System.Data.SqlClient;

namespace DBQueryTool.ConnectionProcessor.DataProviders
{
    public class MsSqlServerDataProvider : IDataProvider
    {
        public string VisibleName { get; set; } = "MsSqlServer";

        private string _queryString;

        public bool? TestConnection()
        {
            if (_queryString == null)
            {
                return null;
            }

            return Query("select 1") != null;
        }

        public DataTable Query(string query)
        {
            try
            {
                using (var connection = new SqlConnection(_queryString))
                {
                    connection.Open();

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        var data = new DataTable();
                        data.Load(cmd.ExecuteReader());
                        return data;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void Build(string queryString)
        {
            _queryString = queryString;
        }
    }
}
