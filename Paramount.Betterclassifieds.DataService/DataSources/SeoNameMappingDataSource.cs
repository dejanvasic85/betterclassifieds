using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models.Seo;
using Paramount.Betterclassifieds.DataService.Entities;
using Paramount.Betterclassifieds.DataService.SeoSettings;

namespace Paramount.Betterclassifieds.DataService.DataSources
{
    public class SeoNameMappingDataSource : AzureTableDataSource, ISeoNameMappingDataSource
    {
        private const string CategoryPartition = "categories";
        private const string LocationPartition = "location";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="applicationConfig"></param>
        public SeoNameMappingDataSource(IApplicationConfig applicationConfig) : base(applicationConfig)
        {
        }

        #region Properties
        protected override string TableName
        {
            get { return TableNames.SeoNameMappingTable; }
        }
        #endregion


        #region Public methods
        public SeoNameMappingEntity CreateCategoryMapping(string seoName, string mapToString)
        {
            var entity = new SeoNameMappingEntity(seoName, mapToString)
            {
                PartitionKey = CategoryPartition,
                RowKey = seoName
            };

            var insertOperation = TableOperation.Insert(entity);

            // Execute the insert operation.
            var result = Table.Execute(insertOperation);

            return (SeoNameMappingEntity) result.Result;
        }

        public SeoLocationMappingEntity CreateLocationMapping(string seoName, IEnumerable<int> locationIds, IEnumerable<int> categoryIds)
        {
            var entity = new SeoLocationMappingEntity(seoName, locationIds, categoryIds)
            {
                PartitionKey = LocationPartition,
                RowKey = seoName.Trim().ToLower()
            };

            var insertOperation = TableOperation.Insert(entity);

            // Execute the insert operation.
            var result = Table.Execute(insertOperation);

            return (SeoLocationMappingEntity)result.Result;
        }


        public void RetrieveMappings(string partition)
        {
            var query = SeoNameMappingEntities(partition);
            IEnumerable<SeoNameMappingEntity> t = Table.ExecuteQuery(query).ToList();
        }

        public IEnumerable<SeoNameMappingEntity> GetCategoryMapping(string seoName)
        {

            var query = SeoNameMappingEntities(CategoryPartition).Where(TableQuery.GenerateFilterCondition("RowKey",
                    QueryComparisons.Equal, seoName.Trim().ToLower()));

            var entities = Table.ExecuteQuery(query).ToList();

            return entities;
        }

        #endregion

        #region private method
        private static TableQuery<SeoNameMappingEntity> SeoNameMappingEntities(string partition)
        {
            var query =
                new TableQuery<SeoNameMappingEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey",
                    QueryComparisons.Equal, partition));
            return query;
        }
        #endregion



    }
}