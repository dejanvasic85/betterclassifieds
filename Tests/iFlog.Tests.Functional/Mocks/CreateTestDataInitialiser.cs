using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace iFlog.Tests.Functional.Mocks
{
    public class CreateTestDataInitialiser : CreateDatabaseIfNotExists<ClassifiedEfContext>
    {
        protected override void Seed(ClassifiedEfContext context)
        {
            // Todo - Setup default categories


            context.MainCategories.AddOrUpdate(m => m.Title, 
                new MainCategory { Title = "For Sale" }
                );

            context.SaveChanges();

            // Todo - Setup publications
            
            base.Seed(context);
        }
    }
}