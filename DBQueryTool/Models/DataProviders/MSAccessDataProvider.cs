using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace DBQueryTool.Models.DataProviders
{
    public static class MSAccessDataProvider
    {
        public static OleDbConnection Connection { get; private set; }

        public static bool Connect(string connectionString)
        {
            try
            {
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
            OleDbCommand cmd = new OleDbCommand(query, Connection);
            return cmd.ExecuteReader();
        }
        public static void Disconnect()
        {
            // TODO: Implement disconnect button and bound it to this method
            Connection.Close();
        }
    }
}
