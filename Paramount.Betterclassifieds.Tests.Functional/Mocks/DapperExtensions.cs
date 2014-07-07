using System;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    public static class DapperExtensions
    {
        public static int Add(this IDbConnection connection, string table, object param)
        {
            var queryBuilder = BuildInsertQuery(table, param);
            queryBuilder.Append(" ; SELECT CAST(SCOPE_IDENTITY() as int)");

            return connection.Query<int>(queryBuilder.ToString(), param).Single();
        }

        public static T Add<T>(this IDbConnection connection, string table, object param)
        {
            var queryBuilder = BuildInsertQuery(table, param);
            string typeName;
            switch (typeof(T).Name.ToLower())
            {
                case "string":
                    typeName = "varchar";
                    break;
                case "guid":
                    typeName = "uniqueidentifier";
                    break;
                default:
                    typeName = "int";
                    break;
            }

            queryBuilder.Append(" ; SELECT CAST(SCOPE_IDENTITY() as " + typeName + ")");
            return connection.Query<T>(queryBuilder.ToString(), param).Single();
        }

        private static StringBuilder BuildInsertQuery(string table, object param)
        {
            var queryBuilder = new StringBuilder(string.Format("INSERT INTO {0} ", table));
            var properties = param.GetType().GetProperties();
            queryBuilder.Append(" (");
            queryBuilder.Append(String.Join(",", properties.Select(p => p.Name)));
            queryBuilder.Append(" ) VALUES ( ");
            queryBuilder.Append(String.Join(",", properties.Select(p => "@" + p.Name)));
            queryBuilder.Append(" ) ");
            return queryBuilder;
        }

        public static int Single(this IDbConnection connection, string table, string queryFilter, string findBy = "Title")
        {
            var value = connection.SingleOrDefault(table, queryFilter, findBy);
            if (value == null)
            {
                throw new InvalidOperationException(string.Format("Table {0} does not contain any records for {1} by {2}", table, queryFilter, findBy));
            }
            return value.Value;
        }

        /// <summary>
        /// Returns the record Id as integer for the required query as filter
        /// </summary>
        public static int? SingleOrDefault(this IDbConnection connection, string table, string filterByValue, string findByColumnName = "Title")
        {
            // Use the convention of tableName+Id as the primary key (used for all classifieds tables)
            var primaryKey = string.Format("{0}{1}", table, "Id");

            // Construct the query
            var query = string.Format("SELECT {0} FROM {1} WHERE {2} = @queryParam", primaryKey, table, findByColumnName);
            return connection.Query<int?>(query, new { queryParam = filterByValue }).FirstOrDefault();
        }

        public static int? SingleOrDefault<T>(this IDbConnection connection, string table, T filterByValue, string findByColumnName = "Title")
        {
            return connection.SingleOrDefault(table, filterByValue.ToString(), findByColumnName);
        }

        public static int? AddIfNotExists(this IDbConnection connection, string table, object param, string filterByValue, string findByColumnName = "Title")
        {
            // Check if the table record exists
            var id = connection.SingleOrDefault(table, filterByValue, findByColumnName);
            return id.HasValue ? id : connection.Add(table, param);
        }

        public static bool Any(this IDbConnection connection, string table, string filterByValue, string findByColumnName = "Title")
        {
            var id = connection.SingleOrDefault(table, filterByValue, findByColumnName);
            return id.HasValue;
        }

        public static IDbConnection ExecuteSql(this IDbConnection connection, string query, object param)
        {
            if (connection.Execute(query, param) == 0)
            {
                throw new DataException("No records have been affected");
            }
            return connection; // for chaining methods
        }
    }
}