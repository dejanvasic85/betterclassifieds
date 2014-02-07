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

        public List<int> CategoryIds { get; set; }

        public int? ParentCategoryId { get; set; }

        public List<int> LocationIds { get; set; }

        public List<int> AreaIds { get; set; }

        public string Description { get; set; }

        public string SearchTerm { get; set; }
    }
}