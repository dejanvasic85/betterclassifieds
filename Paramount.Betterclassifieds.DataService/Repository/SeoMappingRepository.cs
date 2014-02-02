using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models.Seo;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.Classifieds;
using Paramount.Betterclassifieds.DataService.Entities;
using Paramount.Betterclassifieds.DataService.SeoSettings;

namespace Paramount.Betterclassifieds.DataService.Repository
{
    public class SeoMappingRepository : ISeoMappingRepository, IMappingBehaviour
    {
        #region Dependencies
        private readonly ISeoNameMappingDataSource dataSource;

        #endregion

        public SeoMappingRepository(ISeoNameMappingDataSource dataSource)

        {
            this.dataSource = dataSource;
        }

        
        public SeoNameMappingModel GetSeoMapping(string seoName)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var seoMappings =
                    context.SeoMappings.Where(
                        seo => seo.SeoName.Equals(seoName.Trim()));

                return seoMappings.Any() ? this.Map<SeoMapping, SeoNameMappingModel>(seoMappings.First()) : null;
            }
        }


        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<SeoNameMappingModel, SeoMapping>();
            configuration.CreateMap<SeoMapping, SeoNameMappingModel>();
        }
    }
}