namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface IClientSideCacheRepository
    {
        void Add(string key, object entity);
        object Get<T>(string key);

        bool ContainsKey(string key);
    }
}