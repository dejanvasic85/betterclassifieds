using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Models.Seo;

namespace Paramount.Betterclassifieds.Business.Repository
{
    public interface ISeoMappingRepository
    {
        SeoNameMappingModel GetSeoMapping(string seoName);
    }
}