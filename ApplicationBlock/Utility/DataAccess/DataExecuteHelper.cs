using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Paramount.Utility.DataAccess
{
    [Obsolete]
    public static class DataExecuteHelper
    {
        public static DataTable GetDataTable(this SqlCommand sqlCommand, string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            sqlCommand.Connection = connection;
            DataTable dataTable = new DataTable();

            connection.Open();
            dataTable.Load(sqlCommand.ExecuteReader());
            connection.Close();
            connection.Dispose();
            sqlCommand.Dispose();

            return dataTable;
        }
    }
}
