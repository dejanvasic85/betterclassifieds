using Microsoft.WindowsAzure.Storage.Table;

namespace Paramount.Betterclassifieds.DataService.Entities
{
    public class CacheItemEntity : TableEntity
    {
        public string Data { get; set; }
    }
}