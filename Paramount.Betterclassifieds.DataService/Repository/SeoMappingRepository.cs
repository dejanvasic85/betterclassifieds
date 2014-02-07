using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Models.Seo;
using Paramount.Betterclassifieds.Business.Repository;
using Paramount.Betterclassifieds.DataService.Classifieds;
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
            
            configuration.CreateMap<SeoMapping, SeoNameMappingModel>().ForMember(dest => dest.CategoryIds,
                opt => opt.MapFrom(
                    src => string.IsNullOrEmpty(src.CategoryIds) 
                        ? new List<int>()
                        : src.CategoryIds.Split(',').Select(int.Parse).ToList()));

            configuration.CreateMap<SeoMapping, SeoNameMappingModel>().ForMember(dest => dest.LocationIds,
                opt => opt.MapFrom(
                    src => string.IsNullOrEmpty(src.LocationIds)
                        ? new List<int>()
                        : src.LocationIds.Split(',').Select(int.Parse).ToList()));

            configuration.CreateMap<SeoMapping, SeoNameMappingModel>().ForMember(dest => dest.AreaIds,
                opt => opt.MapFrom(
                    src => string.IsNullOrEmpty(src.AreaIds)
                        ? new List<int>()
                        : src.AreaIds.Split(',').Select(int.Parse).ToList()));
        }
    }
}