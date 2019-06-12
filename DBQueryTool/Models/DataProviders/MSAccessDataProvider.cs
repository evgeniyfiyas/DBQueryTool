using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using System.Windows;

namespace DBQueryTool.Models.DataProviders
{
    public static class MSAccessDataProvider
    {
        public static OleDbConnection Connection { get; private set; }

        public static bool Connect(string connectionString)
        {
            try
            {
                // using?
                Connection = new OleDbConnection(connectionString);
                Connection.Open();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static OleDbDataReader Query(string query)
        {
            try
            {
                var cmd = new OleDbCommand(query, Connection);

                // Allowing only Select queries
                var regex = new Regex(@"(?i)(SELECT).*");
                var match = regex.Match(query);
                if (match.Success)
                {
                    return cmd.ExecuteReader();
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Invalid query", "DBQueryTool", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }
        public static void Disconnect()
        {
            // TODO: Implement disconnect button and bound it to this method
            Connection.Close();
        }
    }
}
