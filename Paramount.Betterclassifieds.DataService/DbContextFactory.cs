using System;
using System.Configuration;
using Paramount.Betterclassifieds.DataService.Broadcast;
using Paramount.Betterclassifieds.DataService.Classifieds;
using Paramount.Betterclassifieds.DataService.LinqObjects;
using Paramount.Betterclassifieds.DataService.Search;

namespace Paramount.Betterclassifieds.DataService
{
    public interface IDbContextFactory
    {
        ClassifiedsDataContext CreateClassifiedContext();
        ClassifiedsSearchEntitiesDataContext CreateClassifiedSearchContext();
        ClassifiedsEntityContext CreateClassifiedEntitiesContext();
        UserMembershipDataContext CreateMembershipContext();
        BroadcastContext CreateBroadcastContext();
    }


    public class DbContextFactory : IDbContextFactory
    {
        public ClassifiedsDataContext CreateClassifiedContext()
        {
            var connection = ConfigurationManager.ConnectionStrings["ClassifiedConnection"].ConnectionString;
            return new ClassifiedsDataContext(connection);
        }

        public ClassifiedsSearchEntitiesDataContext CreateClassifiedSearchContext()
        {
            var connection = ConfigurationManager.ConnectionStrings["ClassifiedConnection"].ConnectionString;
            return new ClassifiedsSearchEntitiesDataContext(connection);
        }

        public ClassifiedsEntityContext CreateClassifiedEntitiesContext()
        {
            return new ClassifiedsEntityContext();
        }

        public UserMembershipDataContext CreateMembershipContext()
        {
            var connection = ConfigurationManager.ConnectionStrings["AppUserConnection"].ConnectionString;
            return new UserMembershipDataContext(connection);
        }

        public BroadcastContext CreateBroadcastContext()
        {
            return new BroadcastContext();
        }
    }
}
