namespace Paramount.ApplicationBlock.Data
{
    using System;
    using System.Data;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Globalization;
    

    public delegate void Worker(SqlConnection connection, SqlTransaction transaction, ref bool doRollback);

    public static class Executor
    {
        public static void ExecuteNonQuery(
            SqlConnection connection,
            SqlTransaction transaction,
            string procName,
            IEnumerable<Parameter> parameters)
        {
            var parameterList = new List<SqlParameter>();
            foreach (var parameter in parameters)
            {
                parameterList.Add(parameter.SqlParameter);
            }

            ExecuteNonQuery(connection, transaction, procName, parameterList);
        }

        public static void ExecuteNonQuery(
            SqlConnection connection,
            SqlTransaction transaction,
            string procName,
            IEnumerable<SqlParameter> parameters)
        {
            using (var cmd = new SqlCommand(procName, connection))
            {
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                cmd.ExecuteNonQuery();
            }
        }

        public static DataSet ExecuteQuery(
            SqlConnection connection,
            SqlTransaction transaction,
            string procName,
            IEnumerable<SqlParameter> parameters)
        {
            using (var cmd = new SqlCommand(procName, connection))
            {
                cmd.Transaction = transaction;
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                var ds = new DataSet { Locale = CultureInfo.CurrentCulture };
                using (var adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(ds);
                }

                return ds;
            }
        }

        public static DataSet ExecuteQuery(
            SqlConnection connection,
            SqlTransaction transaction,
            string procName,
            IEnumerable<Parameter> parameters)
        {
            var parameterList = new List<SqlParameter>();

            foreach (var parameter in parameters)
            {
                parameterList.Add(parameter.SqlParameter);
            }

            return ExecuteQuery(connection, transaction, procName, parameterList);
        }

        [Obsolete("Call explicit method based on ConnectionString or ConfigSectionName", false)]
        public static void TransactionExecutor(Worker worker, string configSectionName)
        {
            TransactionExecutorByConnectionString(worker, ConfigReader.GetConnectionString(configSectionName));
        }

        public static void TransactionExecutorByConfigSectionName(Worker worker, string configSectionName)
        {
            TransactionExecutorByConnectionString(worker, ConfigReader.GetConnectionString(configSectionName));
        }

        public static void TransactionExecutorByConfigSectionName(
            Worker worker, string configSectionName, string configKey)
        {
            TransactionExecutorByConnectionString(
                worker, ConfigReader.GetConnectionString(configSectionName, configKey));
        }

        public static void TransactionExecutorByConnectionString(Worker worker, string connectionString)
        {
            // Begin Transaction
            var conn = new SqlConnection(GetDotNetFriendlyConnectionString(connectionString));
            conn.Open();
            var transaction = conn.BeginTransaction();

            var rollback = false;
            Exception exception = null;
            var forceRollback = false;

            try
            {
                worker(conn, transaction, ref forceRollback);
            }
            catch (Exception ex)
            {
                rollback = true;
                exception = ex;
            }

            if (rollback || forceRollback)
            {
                transaction.Rollback();
                conn.Close();

                if (forceRollback)
                {
                    return;
                }

                throw new DataException("Sql operation failed", exception);
            }

            transaction.Commit();
            conn.Close();
        }

        private static string GetDotNetFriendlyConnectionString(string connectionString)
        {
            if (connectionString.StartsWith("provider", StringComparison.OrdinalIgnoreCase))
            {
                var providerEndIndex = connectionString.IndexOf(';');
                connectionString = connectionString.Substring(providerEndIndex + 1);
            }

            return connectionString;
        }
    }
}
