using System.Data;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.SqlClient;
using AutoMapper;
using Paramount.ApplicationBlock.Data;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.DataService.Classifieds;
using Paramount.Betterclassifieds.DataService.Search;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.DataService
{
    public class SearchService : ISearchService, IMappingBehaviour
    {
        public List<AdSearchResult> GetAds(string searchterm, IEnumerable<int> categoryIds, IEnumerable<int> locationIds, IEnumerable<int> areaIds, int index = 0, int pageSize = 25, AdSearchSortOrder sortOrder = AdSearchSortOrder.MostRelevant)
        {
            using (var context = DataContextFactory.CreateClassifiedSearchContext())
            {
                // Get the latest online ads
                var categoryList = string.Join(",", categoryIds.EmptyIfNull()).NullIfEmpty();
                var locationList = string.Join(",", locationIds.EmptyIfNull()).NullIfEmpty();
                var areaList = string.Join(",", areaIds.EmptyIfNull()).NullIfEmpty();

                searchterm = searchterm.NullIfEmpty();

                var results = context.spSearchBookedAds(searchterm,
                    categoryList,
                    locationList,
                    areaList,
                    (int)sortOrder,
                    index,
                    pageSize).ToList();

                return this.MapList<BookedAd, AdSearchResult>(results);
            }
        }

        public List<AdSearchResult> GetAdsWithAdo(string searchterm, IEnumerable<int> categoryIds, IEnumerable<int> locationIds, IEnumerable<int> areaIds, int index = 0, int pageSize = 25, AdSearchSortOrder sortOrder = AdSearchSortOrder.MostRelevant)
        {
            DatabaseProxy proxy = new DatabaseProxy("spSearchBookedAds", "paramount/services", "BetterclassifiedsConnection");
            proxy.AddParameter("searchTerm", searchterm, StringType.VarChar);

            var dt = proxy.ExecuteQuery().Tables[0];
            var results = dt.Rows.OfType<DataRow>().Select(row => new AdSearchResult
            {
                AdId = row.Field<int>("AdId"),
                Heading = row.Field<string>("Heading"),
                Description = row.Field<string>("Description"),
                HtmlText = row.Field<string>("HtmlText"),
                LocationId = row.Field<int>("LocationId"),
                CategoryName = row.Field<string>("CategoryName")
            }).ToList();

            return results;
        }

        public List<AdSearchResult> GetAds(string searchterm, int? categoryId, int? locationId, int index = 0, int pageSize = 25, AdSearchSortOrder order = AdSearchSortOrder.MostRelevant)
        {
            return GetAds(searchterm,
                categoryId.HasValue ? new[] { categoryId.Value } : null,
                locationId.HasValue ? new[] { locationId.Value } : null,
                null,
                index,
                pageSize,
                order);
        }

        public List<AdSearchResult> GetLatestAds(int pageSize = 10)
        {
            return GetAds(searchterm: null, categoryIds: null, locationIds: null, areaIds: null, index: 0, pageSize: pageSize, sortOrder: AdSearchSortOrder.NewestFirst);
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
                                  Heading = onl.Heading,
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
                                  Heading = onl.Heading,
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

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("SearchingProfile");

            // From DB
            configuration.CreateMap<MainCategory, CategorySearchResult>();
            configuration.CreateMap<Location, LocationSearchResult>();

            configuration.CreateMap<BookedAd, AdSearchResult>()
                .ForMember(member => member.Username, options => options.MapFrom(source => source.UserId))
                .ForMember(member => member.ImageUrls, options => options.MapFrom(source => source.DocumentIds.HasValue() ? source.DocumentIds.Split(',') : new string[0]))
                .ForMember(member => member.Publications, options => options.MapFrom(source => source.Publications.HasValue() ? source.Publications.Split(',') : new string[0]))
                ;

            configuration.CreateMap<SeoMapping, SeoNameMappingModel>()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.CategoryIds.IsNullOrEmpty() ? new List<int>() : src.CategoryIds.Split(',').Select(int.Parse).ToList()))
                .ForMember(dest => dest.LocationIds, opt => opt.MapFrom(src => src.LocationIds.IsNullOrEmpty() ? new List<int>() : src.LocationIds.Split(',').Select(int.Parse).ToList()))
                .ForMember(dest => dest.AreaIds, opt => opt.MapFrom(src => src.AreaIds.IsNullOrEmpty() ? new List<int>() : src.AreaIds.Split(',').Select(int.Parse).ToList()))
                .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryIds))
                ;

            // To DB
            configuration.CreateMap<SeoNameMappingModel, SeoMapping>();


        }
    }
}