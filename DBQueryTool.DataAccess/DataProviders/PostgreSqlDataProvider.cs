using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBQueryTool.Utils;
using MySql.Data.MySqlClient;
using Npgsql;

namespace DBQueryTool.DataAccess.DataProviders
{
    public class PostgreSqlDataProvider : LoggedClass, IDataProvider
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
                    Logger.Info("Connected to database using connection string: " + _queryString);

                    var cmd = new NpgsqlCommand(query, connection);
                    var data = new DataTable();
                    data.Load(cmd.ExecuteReader());
                    return data;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Can't connect to database using provided connection string. \n" + ex.StackTrace);
                return null;
            }
        }

        public void Build(string queryString)
        {
            _queryString = queryString;
        }
    }
}
