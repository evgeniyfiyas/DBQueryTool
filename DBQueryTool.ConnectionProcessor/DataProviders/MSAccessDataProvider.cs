using System;
using System.Data;
using System.Data.OleDb;

namespace DBQueryTool.ConnectionProcessor.DataProviders
{
    public class MsAccessDataProvider : IDataProvider
    {
        private string _queryString;

        public string VisibleName { get; set; } = "MsAccess";

        public void Build(string querystring)
        {
            _queryString = querystring;
        }

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
                using (var connection = new OleDbConnection(_queryString))
                {
                    connection.Open();

                    using (var cmd = new OleDbCommand(query, connection))
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
    }
}