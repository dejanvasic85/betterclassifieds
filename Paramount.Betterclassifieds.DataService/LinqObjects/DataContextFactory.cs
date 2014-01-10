using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.DataService.Classifieds;

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
    }
}
