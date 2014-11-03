using System.Configuration;
using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.DataService.Classifieds;
using Paramount.Betterclassifieds.DataService.LinqObjects;
using Paramount.Betterclassifieds.DataService.Search;

namespace Paramount.Betterclassifieds.DataService
{
    public class DataContextFactory
    {
        private const string SectionName = "paramount/services";

        public static ClassifiedsDataContext CreateClassifiedContext()
        {
            var connection = ConfigReader.GetConnectionString(SectionName, "BetterclassifiedsConnection");
            return new ClassifiedsDataContext(connection);
        }

        public static ClassifiedsSearchEntitiesDataContext CreateClassifiedSearchContext()
        {
            var connection = ConfigReader.GetConnectionString(SectionName, "BetterclassifiedsConnection");
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
