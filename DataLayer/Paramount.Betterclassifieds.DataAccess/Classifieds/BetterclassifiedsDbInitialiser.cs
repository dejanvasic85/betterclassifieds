using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace Paramount.Betterclassifieds.DataAccess.Classifieds
{
    using Domain;
    
    /// <summary>
    /// Sets up all the required test data required for BDD scenarios
    /// </summary>
    public class BetterclassifiedsDbInitialiser : CreateDatabaseIfNotExists<BetterclassifiedsDbContext>
    {
        public new void Seed(BetterclassifiedsDbContext context)
        {
            var seleniumCategory = new MainCategory { Title = "Selenium Category" };
            context.MainCategories.AddOrUpdate(seleniumCategory);

            var seleniumSubCategory = new MainCategory { Title = "Selenium Sub Category", ParentCategory = seleniumCategory };
            context.MainCategories.AddOrUpdate(seleniumSubCategory);
            
            context.SaveChanges();
        }
    }
}