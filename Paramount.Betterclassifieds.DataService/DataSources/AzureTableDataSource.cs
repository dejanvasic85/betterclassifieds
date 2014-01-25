using System.Web.UI;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Paramount.Betterclassifieds.Business.Managers;

namespace Paramount.Betterclassifieds.DataService.DataSources
{
    public abstract class AzureTableDataSource
    {
        private readonly IApplicationConfig applicationConfig;
        // Retrieve the storage account from the connection string.
        readonly CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

        protected AzureTableDataSource(IApplicationConfig applicationConfig)
        {
            this.applicationConfig = applicationConfig;
            // Create the table client.
            TableClient = storageAccount.CreateCloudTableClient();
            // Create the table if it doesn't exist.
            Table = TableClient.GetTableReference(GetTableName());
            Table.CreateIfNotExists();
        }

        protected CloudTableClient TableClient { get; private set; }
        protected CloudTable Table { get; private set; }

        protected abstract string TableName { get; }

        string GetTableName()
        {
            return string.Format("{0}1{1}1{2}", TableName, applicationConfig.ClientCode, applicationConfig.ConfigurationContext);
        }
 
    }
}