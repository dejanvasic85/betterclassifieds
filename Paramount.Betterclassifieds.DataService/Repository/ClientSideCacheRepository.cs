using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.DataSources;
using Paramount.Utility;

namespace Paramount.Betterclassifieds.DataService.Managers
{
    public class ClientSideCacheRepository : IClientSideCacheRepository
    {
        private readonly IClientSideCacheDataSource cacheDataSource;
        private readonly IClientIdentifierManager cookiesManager;

        public ClientSideCacheRepository(IClientSideCacheDataSource cacheDataSource, IClientIdentifierManager cookiesManager)
        {
            this.cacheDataSource = cacheDataSource;
            this.cookiesManager = cookiesManager;
        }

        public void Add(string key, object entity)
        {
            cacheDataSource.InsertOrReplace(cookiesManager.Identifier, key, entity);
        }

        public object Get<T>(string key)
        {
            var entity = cacheDataSource.Retrieve(cookiesManager.Identifier, key);

            if (entity == null)
            {
                return null;
            }

            return  JsonHelper.JsonDeserialize<T>(entity.Data);
        }

        public bool ContainsKey(string key)
        {
            var entity = cacheDataSource.Retrieve(cookiesManager.Identifier, key);
            return entity == null;
        }
    }
}