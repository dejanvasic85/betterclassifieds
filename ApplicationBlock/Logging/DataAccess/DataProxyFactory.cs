namespace Paramount.ApplicationBlock.Logging.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Globalization;
    using System.Text;
    using Enum;

    public class DataProxyFactory : IDisposable
    {
        private readonly string connectionString;
        private readonly Collection<Parameter> paramList;
        private readonly string procName;

        public DataProxyFactory(string procName, string configSectionName) : this(procName, configSectionName, string.Empty) { }

        public DataProxyFactory(string procName, string configSectionName, string configKey)
        {
            if (string.IsNullOrEmpty(configSectionName))
            {
                throw new DataProxyFactoryException("configSection cannot be null or empty");
            }

            this.connectionString = string.IsNullOrEmpty(configKey) ? ConfigReader.GetConnectionString(configSectionName) : ConfigReader.GetConnectionString(configSectionName, configKey);
            if (this.connectionString.StartsWith("provider", StringComparison.OrdinalIgnoreCase))
            {
                var providerEndIndex = this.connectionString.IndexOf(';');
                this.connectionString = this.connectionString.Substring(providerEndIndex + 1);
            }

            this.procName = procName;
            this.paramList = new Collection<Parameter>();
        }

        public DataProxyFactory(string procName, string connectionString, Collection<Parameter> parameterList)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new DataProxyFactoryException("connectionString cannot be null or empty");
            }

            this.connectionString = connectionString;
            if (this.connectionString.StartsWith("provider", StringComparison.OrdinalIgnoreCase))
            {
                var providerEndIndex = this.connectionString.IndexOf(';');
                this.connectionString = this.connectionString.Substring(providerEndIndex + 1);
            }

            this.procName = procName;
            this.paramList = (parameterList ?? new Collection<Parameter>());
        }

        public DataProxyFactory AddParameter(string name, int value)
        {
            this.paramList.Add(new Parameter(name, value));
            return this;
        }

        public DataProxyFactory AddParameter(string name, string value, StringType stringType)
        {
            this.paramList.Add(new Parameter(name, value, stringType));
            return this;
        }

        public DataProxyFactory AddParameter(string name, Guid value)
        {
            this.AddParameter(new Parameter(name, value));
            return this;
        }

        public DataProxyFactory AddParameter(string name, DateTime value)
        {
            this.AddParameter(new Parameter(name, value));
            return this;
        }

        public DataProxyFactory AddParameter(string name, decimal value)
        {
            this.AddParameter(new Parameter(name, value));
            return this;
        }

        public DataProxyFactory AddParameter(Parameter parameter)
        {
            this.paramList.Add(parameter);
            return this;
        }

        public DataProxyFactory AddParameterList(Collection<Parameter> parameterList)
        {
            foreach (var parameter in parameterList)
            {
                this.AddParameter(parameter);
            }
            return this;
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
                using (var conn = new SqlConnection(this.connectionString))
                {
                    using (var cmd = new SqlCommand(this.procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in this.paramList)
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
                        this.procName,
                        Environment.NewLine,
                        GetParamString(this.paramList)),
                    ex);
            }
        }

        public Dictionary<string, object> ExecuteNonQueryWithOutput()
        {
            try
            {
                using (var conn = new SqlConnection(this.connectionString))
                {
                    using (var cmd = new SqlCommand(this.procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in this.paramList)
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
                        this.procName,
                        Environment.NewLine,
                        GetParamString(this.paramList)),
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
                using (var conn = new SqlConnection(this.connectionString))
                {
                    using (var cmd = new SqlCommand(this.procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in this.paramList)
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
                        this.procName,
                        Environment.NewLine,
                        GetParamString(this.paramList)),
                    ex);
            }
        }

        public DataSetWithOutput ExecuteQueryWithOutput()
        {
            try
            {
                using (var conn = new SqlConnection(this.connectionString))
                {
                    using (var cmd = new SqlCommand(this.procName, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        foreach (var param in this.paramList)
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
                        this.procName,
                        Environment.NewLine,
                        GetParamString(this.paramList)),
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
