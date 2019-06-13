using System;
using System.Data.OleDb;
using NLog;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace DBQueryTool.Models.DataProviders
{
    public class MsAccessDataProvider : IDataProvider
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly string _queryString;

        public MsAccessDataProvider(string queryString)
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
                    Logger.Info("Connected to database using connection string: " + _queryString);

                    var cmd = new OleDbCommand(query, connection);
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
    }
}
