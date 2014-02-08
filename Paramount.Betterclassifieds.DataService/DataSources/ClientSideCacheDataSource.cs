using Microsoft.WindowsAzure.Storage.Table;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.DataService.Entities;
using Paramount.Utility;

namespace Paramount.Betterclassifieds.DataService.DataSources
{
    public class ClientSideCacheDataSource : AzureTableDataSource, IClientSideCacheDataSource
    {
        public ClientSideCacheDataSource(IApplicationConfig applicationConfig) : base(applicationConfig)
        {
        }

        protected override string TableName
        {
            get { return "ClientSideCache"; }
        }

        public object Insert(string identifier, string key, object data)
        {
            var entity = new CacheItemEntity()
            {
                PartitionKey = identifier,
                RowKey = key,
                Data = JsonHelper.JsonSerializer(data)
            };

            var insertOperation = TableOperation.Insert(entity);

            // Execute the insert operation.
            var result = Table.Execute(insertOperation);

            return result.Result;
        }

        public CacheItemEntity Retrieve(string identifier, string key)
        {
            var retrieveOperation = TableOperation.Retrieve<CacheItemEntity>(identifier, key);

            // Execute the operation.
            var retrievedResult = Table.Execute(retrieveOperation);

            // Assign the result to a CacheItemEntity object.
            var updateEntity = (CacheItemEntity)retrievedResult.Result;

            return updateEntity;
        }

        public void Delete(string identifier, string key)
        {
            // Create a retrieve operation that expects a CacheItemEntity entity.
            var retrieveOperation = TableOperation.Retrieve<CacheItemEntity>(identifier, key);

            // Execute the operation.
            var retrievedResult = Table.Execute(retrieveOperation);

            // Assign the result to a CacheItemEntity.
            var deleteEntity = (CacheItemEntity)retrievedResult.Result;

            // Create the Delete TableOperation.
            if (deleteEntity == null)
            {
                return;
            }
            var deleteOperation = TableOperation.Delete(deleteEntity);

            // Execute the operation.
            Table.Execute(deleteOperation);
        }

        public object InsertOrReplace(string identifier, string key, object data)
        {
            var entity = Retrieve(identifier, key);
            if (entity == null)
                entity = new CacheItemEntity()
                         {
                             PartitionKey = identifier,
                             RowKey = key,
                             Data = JsonHelper.JsonSerializer(data) 
                         };
            else
            {
                entity.Data = JsonHelper.JsonSerializer(data);
            }

            var insertOperation = TableOperation.InsertOrReplace(entity);

            // Execute the insert operation.
            var result = Table.Execute(insertOperation);

            return result.Result;
        }
    }
}