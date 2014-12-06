using AutoMapper;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.DataService.Classifieds;
using Paramount.Betterclassifieds.DataService.Search;
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

                var results = context.BookedAd_Search(searchterm,
                    categoryList,
                    locationList,
                    areaList,
                    (int)sortOrder,
                    index,
                    pageSize).ToList();

                return this.MapList<BookedAd, AdSearchResult>(results);
            }
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
            using (var context = DataContextFactory.CreateClassifiedSearchContext())
            {
                // Simply get the booked at view
                return this.Map<BookedAd, AdSearchResult>(context.BookedAd_GetById(id).FirstOrDefault());
            }
        }
        public List<LocationAreaSearchResult> GetLocationAreas(int locationId, bool includeAllAreas = true)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                if (includeAllAreas)
                {
                    return this.MapList<LocationArea, LocationAreaSearchResult>(
                        context.LocationAreas.Where(l => l.LocationId == locationId || l.Title.Trim().Equals("Any Area")).ToList());
                }

                return this.MapList<LocationArea, LocationAreaSearchResult>(context.LocationAreas.Where(l => l.LocationId == locationId).ToList());
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
                var categories = context.MainCategories.OrderBy(c => c.Title).ToList();
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

        public TutorAdModel GetTutorAd(int id)
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var ad = from adBooking in context.AdBookings
                         where adBooking.AdBookingId == id
                         join adDesign in context.AdDesigns on adBooking.AdId equals adDesign.AdId
                         join onlineAd in context.OnlineAds on adDesign.AdDesignId equals onlineAd.AdDesignId
                         join tutorAd in context.TutorAds on onlineAd.OnlineAdId equals tutorAd.OnlineAdId
                         select tutorAd;

                return this.Map<TutorAd, TutorAdModel>(ad.SingleOrDefault());
            }
        }

        public List<PublicationModel> GetPublications()
        {
            using (var context = DataContextFactory.CreateClassifiedContext())
            {
                var publications = context.Publications
                    .Where(p => (p.PublicationTypeId == Constants.NewsPublicationTypeId ||
                        p.PublicationTypeId == Constants.MagPublicationTypeId) && p.Active == true)
                    .ToList();

                return this.MapList<Publication, PublicationModel>(publications);
            }
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("SearchingProfile");

            // From DB
            configuration.CreateMap<MainCategory, CategorySearchResult>();
            configuration.CreateMap<Location, LocationSearchResult>();
            configuration.CreateMap<LocationArea, LocationAreaSearchResult>();

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

            configuration.CreateMap<TutorAd, TutorAdModel>();
            configuration.CreateMap<Publication, PublicationModel>();

            // To DB
            configuration.CreateMap<SeoNameMappingModel, SeoMapping>();
        }
    }
}