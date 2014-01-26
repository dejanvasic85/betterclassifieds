using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Models.Seo
{
    public class SeoNameMappingModel
    {
        public SeoNameMappingModel()
        {
            SeoName = string.Empty;
            CategoryIdList = new List<int>();
            LocationIdList = new List<int>();
        }
        public string SeoName { get; set; }

        public IEnumerable<int> CategoryIdList { get; set; }

        public IEnumerable<int> LocationIdList { get; set; } 
    }
}