using System;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Dapper;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    public static class DapperExtensions
    {
        public static int InsertIntoTable(this IDbConnection connection, string table , object param)
        {
            var queryBuilder = new StringBuilder(string.Format("INSERT INTO {0} ", table));
            var properties = param.GetType().GetProperties();
            queryBuilder.Append(" (");
            queryBuilder.Append(String.Join(",", properties.Select(p => p.Name)));
            queryBuilder.Append(" ) VALUES ( ");
            queryBuilder.Append(String.Join(",", properties.Select(p => "@" + p.Name)));
            queryBuilder.Append(" ) ");

            return Insert(connection, queryBuilder.ToString(), param);
        }

        private static int Insert(this IDbConnection connection, string query, object param)
        {
            return connection.Query<int>(query + "; SELECT CAST(SCOPE_IDENTITY() as int)", param).Single();
        }

        public static IDbConnection ExecuteSql(this IDbConnection connection, string query, object param)
        {
            if (connection.Execute(query, param) == 0)
            {
                throw new DataException("No records have been affected");
            }
            return connection; // for chaining methods
        }

        public static int? GetIdForTable(this IDbConnection connection, string table, string findBy, string findByColumnName = "Title")
        {
            var tableIdentifierName = string.Format("{0}{1}", table, "Id");
            var query = string.Format("SELECT {0} FROM {1} WHERE {2} = @queryParam", tableIdentifierName, table, findByColumnName);
            return connection.Query<int?>(query, new { queryParam = findBy }).FirstOrDefault();
        }

        public static int? AddIfNotExists(this IDbConnection connection, string table, object param, string findBy, string findByColumnName = "Title")
        {
            // Check if the table record exists
            var id = connection.GetIdForTable(table, findBy, findByColumnName);
            return id.HasValue ? id : connection.InsertIntoTable(table, param);
        }

        //public static IDbConnection Insert<T>(this IDbConnection connection, string query, object param, Action<T> setId)
        //{
        //    connection.Execute(query, param);
        //    connection.SetIdentity(setId);
        //    return connection;
        //}

        //private static void SetIdentity<T>(this IDbConnection connection, Action<T> setId)
        //{
        //    var identity = connection.Query("SELECT @@IDENTITY AS Id").Single();
        //    T newId = (T)identity.Id;
        //    setId(newId);
        //}
    }
}