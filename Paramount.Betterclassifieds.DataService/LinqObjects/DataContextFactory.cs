using System.Configuration;
using Paramount.Betterclassifieds.DataService.Classifieds;
using Paramount.Betterclassifieds.DataService.LinqObjects;
using Paramount.Betterclassifieds.DataService.Search;

namespace Paramount.Betterclassifieds.DataService
{
    public class DataContextFactory
    {

        public static ClassifiedsDataContext CreateClassifiedContext()
        {
            var connection = ConfigurationManager.ConnectionStrings["ClassifiedConnection"].ConnectionString;
            return new ClassifiedsDataContext(connection);
        }

        public static ClassifiedsSearchEntitiesDataContext CreateClassifiedSearchContext()
        {
            var connection = ConfigurationManager.ConnectionStrings["ClassifiedConnection"].ConnectionString;
            return new ClassifiedsSearchEntitiesDataContext(connection);
        }

        public static ClassifiedsEntityContext CreateClassifiedEntitiesContext()
        {
            return new ClassifiedsEntityContext();
        }

        public static UserMembershipDataContext CreateMembershipContext()
        {
            var connection = ConfigurationManager.ConnectionStrings["AppUserConnection"].ConnectionString;
            return new UserMembershipDataContext(connection);
        }
    }
}
