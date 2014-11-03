using DbUp;
using System;
using System.Configuration;
using System.Reflection;

namespace ClassifiedsDatabase
{
    class Program
    {
        static int Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ClassifiedConnection"].ConnectionString;

            //CreateDbIfNotExists(connectionString);

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

    }
}
