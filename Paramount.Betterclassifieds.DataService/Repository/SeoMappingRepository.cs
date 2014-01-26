using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models.Seo;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.Entities;
using Paramount.Betterclassifieds.DataService.SeoSettings;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class SeoMappingRepository : ISeoMappingRepository
    {
        #region Dependencies
        private readonly ISeoNameMappingDataSource dataSource;

        #endregion

        public SeoMappingRepository(ISeoNameMappingDataSource dataSource)

        {
            this.dataSource = dataSource;
        }

        public void CreateCategoryMapping(string seoName, int categoryIds)
        {
            var returnValue = dataSource.CreateCategoryMapping(seoName, categoryIds.ToString(CultureInfo.InvariantCulture));

        }

        public void RetrieveMappings(string partition)
        {
            dataSource.RetrieveMappings(partition);
        }

        public SeoNameMappingModel GetCategoryMapping(string seoName)
        {
            var entities = dataSource.GetCategoryMapping(seoName).ToList();
            return entities.Any() ? entities.First().Convert() : null;
        }
        
       
    }

    internal static class Extentions
    {
        public static SeoNameMappingModel Convert(this SeoNameMappingEntity entity)
        {
            return new SeoNameMappingModel()
            {
                CategoryIdList = entity.GetMappedCategoryId(),
                LocationIdList = new List<int>(),
                SeoName = entity.SeoName
            };
        }
    }
}