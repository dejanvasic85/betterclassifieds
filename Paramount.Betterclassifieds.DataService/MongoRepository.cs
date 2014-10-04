using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Driver;
using Paramount.Betterclassifieds.Business;

namespace Paramount.Betterclassifieds.DataService
{
    // Todo - Move this class to more re-usable assembly
    public abstract class MongoRepository<T>
    {
        private readonly string _collectionName;
        private readonly MongoDatabase _database;

        protected MongoRepository(string collectionName)
        {
            _collectionName = collectionName;
            var client = new MongoClient("mongodb://localhost:27017");
            _database = client.GetServer().GetDatabase("classifieds");
        }

        protected MongoCollection<T> Collection
        {
            get { return _database.GetCollection<T>(_collectionName); }
        }

        protected virtual void OnSerializationSettings()
        {
            
        }
    }
}