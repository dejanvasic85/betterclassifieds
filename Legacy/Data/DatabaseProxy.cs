namespace Paramount.ApplicationBlock.Data
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Configuration;
    using System.Data;
    using System.Globalization;
    using System.Text;
    using System.Data.SqlClient;

    public class DatabaseProxy : IDisposable
    {
        private readonly string _connectionString;
        private readonly Collection<Parameter> _paramList;
        private readonly string _procName;

        public DatabaseProxy(string procName, string connectionName)
        {
            if (string.IsNullOrEmpty(connectionName))
            {
                throw new DatabaseProxyException("configSection cannot be null or empty");
            }

            this._connectionString = ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;

            if (this._connectionString.StartsWith("provider", StringComparison.OrdinalIgnoreCase))
            {
                var providerEndIndex = this._connectionString.IndexOf(';');
                this._connectionString = this._connectionString.Substring(providerEndIndex + 1);
            }

            this._procName = procName;
            this._paramList = new Collection<Parameter>();
        }

        public DatabaseProxy(string procName, string connectionString, Collection<Parameter> parameterList)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new DatabaseProxyException("connectionString cannot be null or empty");
            }

            this._connectionString = connectionString;
            if (this._connectionString.StartsWith("provider", StringComparison.OrdinalIgnoreCase))
            {
                var providerEndIndex = this._connectionString.IndexOf(';');
                this._connectionString = this._connectionString.Substring(providerEndIndex + 1);
            }

            this._procName = procName;
            this._paramList = (parameterList ?? new Collection<Parameter>());
        }

        public void AddParameter(string name, int value)
        {
            this._paramList.Add(new Parameter(name, value));
        }

        public void AddParameter(string name, bool value)
        {
            this._paramList.Add(new Parameter(name, value));
        }

        public void AddParameter(string name, string value, StringType stringType)
        {
            this._paramList.Add(new Parameter(name, value, stringType));
        }

        public void AddParameter(string name, Guid value)
        {
            this.AddParameter(new Parameter(name, value));
        }

        public void AddParameter(string name, DateTime value)
        {
            this.AddParameter(new Parameter(name, value));
        }

        public void AddParameter(string name, decimal value)
        {
            this.AddParameter(new Parameter(name, value));
        }

        public void AddParameter(string name, byte[] value)
        {
            this.AddParameter(new Parameter(name, value));
        }


        public void AddParameter(Parameter parameter)
        {
            this._paramList.Add(parameter);
        }

        public void AddParameterList(Collection<Parameter> parameterList)
        {
            foreach (var parameter in parameterList)
            {
                this.AddParameter(parameter);
            }
        }

        public void ClearParamers()
        {
            if (_paramList != null)
            {
                _paramList.Clear();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Executes a non-transactional non-query
        /// </summary>
        public void ExecuteNonQuery()
        {
            try
            {
                using (var conn = new SqlConnection(this._connectionString))
                {
                    using (var cmd = new SqlCommand(this._procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in this._paramList)
                        {
                            cmd.Parameters.Add(param.SqlParameter);
                        }

                        conn.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        @"ExecuteNonQuery ""{0}"" Failed.{1}{2}",
                        this._procName,
                        Environment.NewLine,
                        GetParamString(this._paramList)),
                    ex);
            }
        }

        public Dictionary<string, object> ExecuteNonQueryWithOutput()
        {
            try
            {
                using (var conn = new SqlConnection(this._connectionString))
                {
                    using (var cmd = new SqlCommand(this._procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in this._paramList)
                        {
                            cmd.Parameters.Add(param.SqlParameter);
                        }

                        conn.Open();
                        var output = new Dictionary<string, object>();
                        try
                        {
                            cmd.ExecuteNonQuery();

                            foreach (SqlParameter parameter in cmd.Parameters)
                            {
                                if (parameter.Direction == ParameterDirection.Output
                                    || parameter.Direction == ParameterDirection.InputOutput)
                                {
                                    output.Add(parameter.ParameterName, parameter.Value);
                                }
                            }
                        }
                        finally
                        {
                            conn.Close();
                        }

                        return output;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        @"ExecuteNonQuery ""{0}"" Failed.{1}{2}",
                        this._procName,
                        Environment.NewLine,
                        GetParamString(this._paramList)),
                    ex);
            }
        }

        /// <summary>
        /// Executes a non-transactional query
        /// </summary>
        /// <returns>Query Result</returns>
        public DataSet ExecuteQuery()
        {
            try
            {
                using (var conn = new SqlConnection(this._connectionString))
                {
                    using (var cmd = new SqlCommand(this._procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in this._paramList)
                        {
                            cmd.Parameters.Add(param.SqlParameter);
                        }

                        using (var dataAdapter = new SqlDataAdapter(cmd))
                        {
                            var ds = new DataSet { Locale = CultureInfo.CurrentCulture };
                            conn.Open();
                            try
                            {
                                dataAdapter.Fill(ds);
                            }
                            finally
                            {
                                conn.Close();
                            }

                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        @"ExecuteQuery ""{0}"" Failed.{1}{2}",
                        this._procName,
                        Environment.NewLine,
                        GetParamString(this._paramList)),
                    ex);
            }
        }

        public DataSetWithOutput ExecuteQueryWithOutput()
        {
            try
            {
                using (var conn = new SqlConnection(this._connectionString))
                {
                    using (var cmd = new SqlCommand(this._procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in this._paramList)
                        {
                            cmd.Parameters.Add(param.SqlParameter);
                        }

                        using (var dataAdapter = new SqlDataAdapter(cmd))
                        {
                            var ds = new DataSet { Locale = CultureInfo.CurrentCulture };
                            conn.Open();

                            var output = new Dictionary<string, object>();
                            try
                            {
                                dataAdapter.Fill(ds);

                                foreach (SqlParameter parameter in cmd.Parameters)
                                {
                                    if (parameter.Direction == ParameterDirection.Output
                                        || parameter.Direction == ParameterDirection.InputOutput)
                                    {
                                        output.Add(parameter.ParameterName, parameter.Value);
                                    }
                                }
                            }
                            finally
                            {
                                conn.Close();
                            }

                            return new DataSetWithOutput(ds, output);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new DataException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        @"ExecuteQuery ""{0}"" Failed.{1}{2}",
                        _procName,
                        Environment.NewLine,
                        GetParamString(_paramList)),
                    ex);
            }
        }

        protected virtual void Dispose(bool disposeManagedResources) { }

        private static string GetParamString(IEnumerable<Parameter> paramList)
        {
            var sb = new StringBuilder();
            foreach (var parameter in paramList)
            {
                sb.AppendLine(string.Format(CultureInfo.InvariantCulture, "{0}={1}", parameter.Name, parameter.Value));
            }

            return sb.ToString();
        }
    }
}
