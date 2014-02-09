using Paramount.Betterclassifieds.DataService.Entities;

namespace Paramount.Betterclassifieds.DataService.DataSources
{
    public interface IClientSideCacheDataSource
    {
        object Insert(string identifier, string key, object data);
        CacheItemEntity Retrieve(string identifier, string key);
        void Delete(string identifier, string key);
        object InsertOrReplace(string identifier, string key, object data);
        void ClearPartiotion(string identifier);
    }
}