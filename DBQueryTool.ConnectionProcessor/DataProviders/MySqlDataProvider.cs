using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace DBQueryTool.ConnectionProcessor.DataProviders
{
    public class MySqlDataProvider : IDataProvider
    {
        public string VisibleName { get; set; } = "MySQL";

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
                using (var connection = new MySqlConnection(_queryString))
                {
                    connection.Open();

                    using (var cmd = new MySqlCommand(query, connection))
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
