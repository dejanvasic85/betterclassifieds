using System.Linq;
using Paramount.Betterclassifieds.Business.Bookings.SeoSettings;
using Paramount.Betterclassifieds.Business.Managers;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class SeoMappingRepository : ISeoMappingRepository
    {
        private readonly ISeoNameMappingDataSource dataSource;


        public SeoMappingRepository(ISeoNameMappingDataSource dataSource)

        {
            this.dataSource = dataSource;
        }

        public void CreateCategoryMapping(string seoName, int categoryIds)
        {
            var returnValue = dataSource.CreateCategoryMapping(seoName, categoryIds.ToString());
        }

        //public int[] GetCategoryMapping(string seoName)
        //{
            
        //}
    }
}