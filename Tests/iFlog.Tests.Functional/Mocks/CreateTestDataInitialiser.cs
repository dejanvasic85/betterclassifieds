using System.Data.Entity;

namespace iFlog.Tests.Functional.Mocks
{
    public class CreateTestDataInitialiser : CreateDatabaseIfNotExists<ClassifiedEfContext>
    {
        protected override void Seed(ClassifiedEfContext context)
        {
            // Todo - Setup default categories

            // Todo - Setup publications
            
            base.Seed(context);
        }  
    }
}