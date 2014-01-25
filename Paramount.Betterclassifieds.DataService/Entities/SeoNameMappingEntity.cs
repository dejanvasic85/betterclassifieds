using Microsoft.WindowsAzure.Storage.Table;

namespace Paramount.Betterclassifieds.DataService.Entities
{
    public class SeoNameMappingEntity : TableEntity
    {
        public string SeoName { get; set; }
        public string MapToString { get; set; }

        public SeoNameMappingEntity()
        {
            
        }

        public SeoNameMappingEntity(string seoName, string mapTostring)
        {
            this.MapToString = mapTostring;
            SeoName = seoName;
        }
    }
}