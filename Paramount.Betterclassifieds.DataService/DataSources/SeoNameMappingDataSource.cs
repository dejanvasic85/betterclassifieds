using Microsoft.WindowsAzure.Storage.Table;
using Paramount.Betterclassifieds.Business.Bookings.SeoSettings;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.DataService.Entities;

namespace Paramount.Betterclassifieds.DataService.DataSources
{
    public class SeoNameMappingDataSource : AzureTableDataSource, ISeoNameMappingDataSource
    {
        private const string CategoryPartition = "categories";
        public SeoNameMappingDataSource(IApplicationConfig applicationConfig) : base(applicationConfig)
        {
        }

        protected override string TableName
        {
            get { return TableNames.SeoNameMappingTable; }
        }
        

        public object CreateCategoryMapping(string seoName, string mapToString)
        {
            var entity = new SeoNameMappingEntity(seoName, mapToString)
            {
                PartitionKey = CategoryPartition,
                RowKey = seoName
            };

            var insertOperation = TableOperation.Insert(entity);

            // Execute the insert operation.
            var result = Table.Execute(insertOperation);

            return result.Result;
        }
    }
}