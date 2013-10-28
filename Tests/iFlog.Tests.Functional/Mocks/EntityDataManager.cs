using System;
using System.Data.Entity;

namespace iFlog.Tests.Functional.Mocks
{
    using iFlogDb;

    public class EntityDataManager : IDataManager
    {
        public IDataManager Initialise()
        {
            using (iFlogContext iflogDb = new iFlogContext())
            {
                iflogDb.Database.Initialize(true); // Force the initialisation with seed data
            }
            return this;
        }

        public int AddOrUpdateOnlineAd(string adTitle)
        {
            using (iFlogContext context = new iFlogContext())
            {
                var category = context.MainCategories.Find(1);
                return category.MainCategoryId;
            }
        }
    }
}