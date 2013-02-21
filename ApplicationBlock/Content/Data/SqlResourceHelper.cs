namespace Paramount.ApplicationBlock.Content.Globalization.Data
{
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.Data;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Text;

    internal static class SqlResourceHelper
    {
        private static string ApplicationName
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ApplicationName"];
            }
        }

        private static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.
                    ConnectionStrings["GLOBALIZATION_RESOURCES"].ToString();
            }
        }

        public static IDictionary GetResources(
               string virtualPath,
               string className, string cultureName,
               bool designMode, IServiceProvider serviceProvider)
        {
            var con = new SqlConnection(ConnectionString);
            var com = new SqlCommand();
            //
            // Build correct select statement to get resource values
            //
            if (!String.IsNullOrEmpty(virtualPath))
            {
                //
                // Get Local resources
                //
                if (string.IsNullOrEmpty(cultureName))
                {
                    // default resource values (no culture defined)
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select resourcename, resourcevalue" +
                                      " from GLOBALIZATION_RESOURCES" +
                                      " where resourceobject = @virtual_path" +
                                      " and culturename is null" +
                                      " and ApplicationName =@applicationName";
                    com.Parameters.AddWithValue("@virtual_path", virtualPath);
                    com.Parameters.AddWithValue("@applicationName", ApplicationName);
                }
                else
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select resourcename, resourcevalue" +
                                      " from GLOBALIZATION_RESOURCES " +
                                      "where resourceobject = @virtual_path " +
                                      "and culturename = @culture_name " +
                                      " and ApplicationName =@applicationName";
                    com.Parameters.AddWithValue("@virtual_path", virtualPath);
                    com.Parameters.AddWithValue("@culture_name", cultureName);
                    com.Parameters.AddWithValue("@applicationName", ApplicationName);
                }
            }
            else if (!String.IsNullOrEmpty(className))
            {
                //
                // Get Global resources
                // 
                if (string.IsNullOrEmpty(cultureName))
                {
                    // default resource values (no culture defined)
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select resourcename, resourcevalue" +
                                      " from GLOBALIZATION_RESOURCES " +
                                      "where resourceobject = @class_name" +
                                      " and culturename is null" +
                                      " and ApplicationName =@applicationName";
                    com.Parameters.AddWithValue("@class_name", className);
                    com.Parameters.AddWithValue("@applicationName", ApplicationName);
                }
                else
                {
                    com.CommandType = CommandType.Text;
                    com.CommandText = "select resourcename, resourcevalue " +
                                      "from GLOBALIZATION_RESOURCES where " +
                                      "resource_object = @class_name and" +
                                      " culture_name = @culture_name " +
                                      " and ApplicationName =@applicationName";
                    com.Parameters.AddWithValue("@class_name", className);
                    com.Parameters.AddWithValue("@culture_name", cultureName);
                    com.Parameters.AddWithValue("@applicationName", ApplicationName);
                }
            }
            else
            {
                //
                // Neither virtualPath or className provided,
                // unknown if Local or Global resource
                //
                throw new Exception("SqlResourceHelper.GetResources()" +
                      " - virtualPath or className missing from parameters.");
            }
            var resources = new ListDictionary();
            try
            {
                com.Connection = con;
                con.Open();
                SqlDataReader sdr = com.ExecuteReader(CommandBehavior.CloseConnection);

                if (sdr != null)
                    while (sdr.Read())
                    {
                        string rn = sdr.GetString(sdr.GetOrdinal("resourcename"));
                        string rv = sdr.GetString(sdr.GetOrdinal("resourcevalue"));
                        resources.Add(rn, rv);
                    }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message, e);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return resources;
        }

        public static void AddResource(string resourceName,
               string virtualPath, string className, string cultureName)
        {
            var con =
              new SqlConnection(ConnectionString);
            var com = new SqlCommand();
            var sb = new StringBuilder();
            sb.Append("insert into GLOBALIZATION_RESOURCES " +
                      "(resourcename ,resourcevalue," +
                      "resourceobject,culturename, applicationName ) ");
            sb.Append(" values (@resource_name ,@resource_value," +
                      "@resource_object,@culture_name, @applicationName) ");
            com.CommandText = sb.ToString();
            com.Parameters.AddWithValue("@resource_name", resourceName);
            com.Parameters.AddWithValue("@resource_value", resourceName +
                                        " * DEFAULT * " +
                                        (String.IsNullOrEmpty(cultureName) ?
                                        string.Empty : cultureName));
            com.Parameters.AddWithValue("@culture_name",
                (String.IsNullOrEmpty(cultureName) ? SqlString.Null : cultureName));
            com.Parameters.AddWithValue("@applicationName", ApplicationName);
            string resourceObject = "UNKNOWN **ERROR**";
            if (!String.IsNullOrEmpty(virtualPath))
            {
                resourceObject = virtualPath;
            }
            else if (!String.IsNullOrEmpty(className))
            {
                resourceObject = className;
            }
            com.Parameters.AddWithValue("@resource_object", resourceObject);

            try
            {
                com.Connection = con;
                con.Open();
                com.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
            }
        }
    }
}