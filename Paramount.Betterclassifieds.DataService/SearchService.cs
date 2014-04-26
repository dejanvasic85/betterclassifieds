using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Models.Seo;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.DataService.Classifieds;

namespace Paramount.Betterclassifieds.DataService
{
    public class SearchService : ISearchService, IMappingBehaviour
    {
        public List<OnlineListingModel> SearchOnlineListing(string searchterm, IEnumerable<int> categoryIds, IEnumerable<int> locationIds, IEnumerable<int> areaIds, int index = 0, int pageSize = 25, int order = 4)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                // Get the latest online ads
                var categoryList = string.Join(",", categoryIds.EmptyIfNull()).NullIfEmpty();
                var locationList = string.Join(",", locationIds.EmptyIfNull()).NullIfEmpty();
                var areaList = string.Join(",", areaIds.EmptyIfNull()).NullIfEmpty();

                searchterm = searchterm.NullIfEmpty();

                List<OnlineAdView> ads = context.spSearchOnlineAdFREETEXT(searchterm, categoryList, locationList, areaList, order, index,pageSize).ToList();

                // Map to the models
                return this.MapList<OnlineAdView, OnlineListingModel>(ads);
            }
        }

        public AdSearchResult GetAdById(int id)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var results = from onl in context.OnlineAds
                              join ds in context.AdDesigns on onl.AdDesignId equals ds.AdDesignId
                              join bk in context.AdBookings on ds.AdId equals bk.AdId
                              join cat in context.MainCategories on bk.MainCategoryId equals cat.MainCategoryId
                              where bk.AdBookingId == id
                              select new AdSearchResult
                              {
                                  AdId = bk.AdBookingId,
                                  Title = onl.Heading,
                                  Description = onl.Description,
                                  CategoryName = cat.Title,
                                  CategoryId = cat.MainCategoryId,
                                  ParentCategoryId = cat.ParentId,
                                  ImageUrls = onl.AdDesign.AdGraphics.Select(g => g.DocumentID).ToArray(),
                                  Publications = bk.BookEntries.Select(be => be.Publication.Title).Distinct().ToArray()
                              };

                return results.FirstOrDefault();
            }
        }

        public IEnumerable<AdSearchResult> Search()
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var results = from onl in context.OnlineAds
                              join ds in context.AdDesigns on onl.AdDesignId equals ds.AdDesignId
                              join bk in context.AdBookings on ds.AdId equals bk.AdId
                              join cat in context.MainCategories on bk.MainCategoryId equals cat.MainCategoryId
                              where bk.StartDate <= DateTime.Today &&
                              bk.EndDate >= DateTime.Today &&
                              bk.BookingStatus == (int)BookingStatusType.Booked
                              select new AdSearchResult
                              {
                                  AdId = bk.AdBookingId,
                                  Title = onl.Heading,
                                  Description = onl.Description,
                                  CategoryName = cat.Title,
                                  CategoryId = cat.MainCategoryId,
                                  ParentCategoryId = cat.ParentId,
                                  ImageUrls = onl.AdDesign.AdGraphics.Select(g => g.DocumentID).ToArray(),
                                  Publications = bk.BookEntries.Select(be => be.Publication.Title).Distinct().ToArray()
                              };

                return results.ToList();
            }
        }


        public List<LocationSearchResult> GetLocations()
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                return this.MapList<Location, LocationSearchResult>(context.Locations.ToList());
            }
        }

        public List<CategorySearchResult> GetTopLevelCategories()
        {
            return GetCategories().Where(c => c.ParentId.IsNullValue()).ToList();
        }

        public List<CategorySearchResult> GetCategories()
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var categories = context.MainCategories.ToList();
                return this.MapList<MainCategory, CategorySearchResult>(categories);
            }
        }

        public SeoNameMappingModel GetSeoMapping(string seoName)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var seoMappings = context.SeoMappings.FirstOrDefault(seo => seo.SeoName.Equals(seoName.Trim()));
                if (seoMappings == null)
                    return null;

                return this.Map<SeoMapping, SeoNameMappingModel>(seoMappings);
            }
        }


        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("SearchingProfile");

            // From DB
            configuration.CreateMap<MainCategory, CategorySearchResult>();
            configuration.CreateMap<Location, LocationSearchResult>();
            configuration.CreateMap<OnlineAdView, OnlineListingModel>();
            configuration.CreateMap<SeoNameMappingModel, SeoMapping>();
            configuration.CreateMap<SeoMapping, SeoNameMappingModel>().ForMember(dest => dest.CategoryIds,
                opt => opt.MapFrom(src => src.CategoryIds.IsNullOrEmpty() ? new List<int>() : src.CategoryIds.Split(',').Select(int.Parse).ToList()));
            configuration.CreateMap<SeoMapping, SeoNameMappingModel>().ForMember(dest => dest.LocationIds,
                opt => opt.MapFrom(src => src.LocationIds.IsNullOrEmpty() ? new List<int>() : src.LocationIds.Split(',').Select(int.Parse).ToList()));
            configuration.CreateMap<SeoMapping, SeoNameMappingModel>().ForMember(dest => dest.AreaIds,
                opt => opt.MapFrom(src => src.AreaIds.IsNullOrEmpty() ? new List<int>() : src.AreaIds.Split(',').Select(int.Parse).ToList()));
            configuration.CreateMap<SeoMapping, SeoNameMappingModel>().ForMember(dest => dest.ParentCategoryId,
                opt => opt.MapFrom(src => src.ParentCategoryIds));

        }
    }
}