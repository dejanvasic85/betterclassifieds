using System;
using System.Collections.Generic;
using System.Linq;
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
            MapToString = mapTostring;
            SeoName = seoName;
        }

        public IEnumerable<int> GetMappedCategoryId()
        {
            return MapToString.Split(new[] { ',' }).Select(i => Convert.ToInt32(i));
        }
    }
}