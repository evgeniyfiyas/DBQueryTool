using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Windows;
using NLog;
using System.Data;

namespace DBQueryTool.Models.DataProviders
{
    public class MSAccessDataProvider
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private string _queryString;

        public MSAccessDataProvider(string queryString)
        {
            _queryString = queryString;
        }

        public bool TestConnection()
        {
            return Query("select 1") != null;
        }


        public DataTable Query(string query)
        {
            try
            {
                using (var connection = new OleDbConnection(_queryString))
                {
                    connection.Open();
                    logger.Info("Connected to database using connection string: " + _queryString);

                    // TODO: Move this to validator logic
                    // var regex = new Regex(@"(?i)(SELECT).*");
                    // var match = regex.Match(query);

                    var cmd = new OleDbCommand(query, connection);
                    var data = new DataTable();
                    data.Load(cmd.ExecuteReader());
                    return data;
                }
            }
            catch (Exception ex)
            {
                logger.Error("Can't connect to database using provided connection string. \n" + ex.StackTrace);
                return null;
            }
        }
    }
}
