using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Reflection;
using DbUp;
using DbUp.Helpers;

namespace iFlogDatabase
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["iFlogDb"].ConnectionString;

            CreateDbIfNotExists(connectionString);

            var upgrader =
                DeployChanges.To
                    .SqlDatabase(connectionString)
                    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                    .LogToConsole()
                    .Build();

            var result = upgrader.PerformUpgrade();

            if (!result.Successful)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(result.Error);
                Console.ResetColor();
                return -1;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Success!");
            Console.ResetColor();
            return 0;
        }

        /// <summary>
        /// Creates the db if not exists.
        /// </summary>
        private static void CreateDbIfNotExists(string rawConnectionString)
        {
            var connectionString = new SqlConnectionStringBuilder(rawConnectionString);
            var dbName = connectionString.InitialCatalog;

            var serverConnectionString = string.Format("Data Source={0};Integrated Security=SSPI;", connectionString.DataSource);
            var sqlRunner = new AdHocSqlRunner(() => new SqlConnection(serverConnectionString), "dbo");

            var createDbSql = string.Format(@"IF db_id('{0}') IS NULL BEGIN CREATE DATABASE {0} END", dbName);
            System.Console.WriteLine(createDbSql);
            sqlRunner.ExecuteNonQuery(createDbSql);
        }
    }
}
