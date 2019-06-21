using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBQueryTool.Utils;
using MySql.Data.MySqlClient;


namespace DBQueryTool.DataAccess.DataProviders
{
    public class MySqlDataProvider : LoggedClass, IDataProvider
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
                    Logger.Info("Connected to database using connection string: " + _queryString);

                    var cmd = new MySqlCommand(query, connection);
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
