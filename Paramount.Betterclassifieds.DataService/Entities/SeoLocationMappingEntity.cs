using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.DataService.Entities
{
    public class SeoLocationMappingEntity : SeoNameMappingEntity
    {
        public string LocationIds { get; set; }
        
        public SeoLocationMappingEntity()
        {
            
        }

        public SeoLocationMappingEntity(string seoName, IEnumerable<int> locationIds, IEnumerable<int> categoryIds)
            : base(seoName, string.Join(",", categoryIds.EmptyIfNull()))
        {
            LocationIds = string.Join(",", locationIds.EmptyIfNull());
        }

        #region Methods 

        public IEnumerable<int> GetMappedLocationId()
        {
            return LocationIds.Split(new[] { ',' }).Select(i => Convert.ToInt32(i));
        }

        #endregion

    }
}