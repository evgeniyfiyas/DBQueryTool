using Npgsql;
using System;
using System.Data;

namespace DBQueryTool.ConnectionProcessor.DataProviders
{
    public class PostgreSqlDataProvider : IDataProvider
    {
        public string VisibleName { get; set; } = "PostgreSQL";

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
                using (var connection = new NpgsqlConnection(_queryString))
                {
                    connection.Open();

                    using (var cmd = new NpgsqlCommand(query, connection))
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
