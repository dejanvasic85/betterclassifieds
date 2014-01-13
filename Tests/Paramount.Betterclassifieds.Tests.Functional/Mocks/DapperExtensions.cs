using System;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    public static class DapperExtensions
    {
        public static int Add(this IDbConnection connection, string table , object param)
        {
            var queryBuilder = new StringBuilder(string.Format("INSERT INTO {0} ", table));
            var properties = param.GetType().GetProperties();
            queryBuilder.Append(" (");
            queryBuilder.Append(String.Join(",", properties.Select(p => p.Name)));
            queryBuilder.Append(" ) VALUES ( ");
            queryBuilder.Append(String.Join(",", properties.Select(p => "@" + p.Name)));
            queryBuilder.Append(" ) ");
            queryBuilder.Append(" ; SELECT CAST(SCOPE_IDENTITY() as int)");
            
            return connection.Query<int>(queryBuilder.ToString(), param).Single();
        }

        public static int? GetById(this IDbConnection connection, string table, string findBy, string findByColumnName = "Title")
        {
            var tableIdentifierName = string.Format("{0}{1}", table, "Id");
            var query = string.Format("SELECT {0} FROM {1} WHERE {2} = @queryParam", tableIdentifierName, table, findByColumnName);
            return connection.Query<int?>(query, new { queryParam = findBy }).FirstOrDefault();
        }

        public static int? AddIfNotExists(this IDbConnection connection, string table, object param, string findBy, string findByColumnName = "Title")
        {
            // Check if the table record exists
            var id = connection.GetById(table, findBy, findByColumnName);
            return id.HasValue ? id : connection.Add(table, param);
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