namespace Paramount.Betterclassifieds.DataService
{
    public class DataServiceInitialiser
    {
        /// <summary>
        /// Entity Framework initializes on the first query and it's too slow. This will be forced on app startup instead now
        /// </summary>
        public static void InitializeContexts()
        {
            var dbContextFactory = new DbContextFactory();

            using (var entitiesContext = dbContextFactory.CreateClassifiedEntitiesContext())
            using (var eventContext = dbContextFactory.CreateEventContext())
            using (var documentContext = dbContextFactory.CreateDocumentContext())
            {
                entitiesContext.Database.Initialize(false);
                eventContext.Database.Initialize(false);
                documentContext.Database.Initialize(false);
            }
        }
    }
}