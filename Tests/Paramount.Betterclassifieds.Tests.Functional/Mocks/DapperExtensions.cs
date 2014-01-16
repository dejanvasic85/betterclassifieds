﻿using System;
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
            switch (typeof(T).Name)
            {
                case "string":
                    typeName = "varchar";
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

        public static int? GetById(this IDbConnection connection, string table, string queryFilter, string findBy = "Title")
        {
            var tableIdentifierName = string.Format("{0}{1}", table, "Id");
            var query = string.Format("SELECT {0} FROM {1} WHERE {2} = @queryParam", tableIdentifierName, table, findBy);
            return connection.Query<int?>(query, new { queryParam = queryFilter }).FirstOrDefault();
        }

        public static int? AddIfNotExists(this IDbConnection connection, string table, object param, string queryFilter, string findBy = "Title")
        {
            // Check if the table record exists
            var id = connection.GetById(table, queryFilter, findBy);
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