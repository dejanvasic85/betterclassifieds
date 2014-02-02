using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business.Models.Seo
{
    public class SeoNameMappingModel
    {
        public SeoNameMappingModel()
        {
            SeoName = string.Empty;
        }
        public string SeoName { get; set; }

        public int? CategoryId { get; set; }

        public int? ParentCategoryId { get; set; }

        public int? LocationId{ get; set; }

        public int? AreaId { get; set; }

        public string Description { get; set; }

        public string SearchTerm { get; set; }
    }
}