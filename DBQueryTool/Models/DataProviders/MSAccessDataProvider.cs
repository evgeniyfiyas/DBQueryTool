using System;
using System.Data.OleDb;
using NLog;
using System.Data;
using System.ComponentModel;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DBQueryTool.Utils;

namespace DBQueryTool.Models.DataProviders
{
    public class MsAccessDataProvider : LoggedClass, IDataProvider
    {
        private string _queryString;
        
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
