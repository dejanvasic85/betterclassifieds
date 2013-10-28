using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace iFlog.Tests.Functional.Mocks.iFlogDb
{
    /// <summary>
    /// Sets up all the required test data required for BDD scenarios
    /// </summary>
    public class iFlogDbInitialiser : CreateDatabaseIfNotExists<iFlogContext>
    {
        protected override void Seed(iFlogContext context)
        {
            var seleniumCategory = new MainCategory { Title = "Selenium Category" };
            var seleniumSubCategory = new MainCategory { Title = "Selenium Sub Category", ParentCategory = seleniumCategory };

            context.MainCategories.AddOrUpdate(mc => mc.Title,
                seleniumCategory,
                seleniumSubCategory);

            context.SaveChanges();
        }
    }
}