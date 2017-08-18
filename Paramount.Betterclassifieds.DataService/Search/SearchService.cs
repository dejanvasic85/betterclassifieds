using AutoMapper;
using Paramount.Betterclassifieds.Business.Print;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.DataService.Classifieds;
using Paramount.Betterclassifieds.DataService.Search;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.DataService
{
    public class SearchService : ISearchService, IMappingBehaviour
    {
        private readonly IDbContextFactory _dbContextFactory;

        public SearchService(IDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public List<AdSearchResult> GetAds(string searchterm, IEnumerable<int> categoryIds, IEnumerable<int> locationIds, IEnumerable<int> areaIds, int index = 0, int pageSize = 25, AdSearchSortOrder sortOrder = AdSearchSortOrder.MostRelevant)
        {
            using (var context = _dbContextFactory.CreateClassifiedSearchContext())
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

        public AdSearchResult GetByAdId(int id)
        {
            using (var context = _dbContextFactory.CreateClassifiedSearchContext())
            {
                // Simply get the booked at view
                return this.Map<BookedAd, AdSearchResult>(context.BookedAd_GetById(id).FirstOrDefault());
            }
        }

        public AdSearchResult GetByAdOnlineId(int id)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var onlineAd = context.OnlineAds.SingleOrDefault(o => o.OnlineAdId == id);
                if (onlineAd == null)
                    return null;

                var b = context.AdDesigns.Single(a => a.AdDesignId == onlineAd.AdDesignId).Ad.AdBookings.Single();
                return GetByAdId(b.AdBookingId);
            }
        }

        public List<LocationAreaSearchResult> GetLocationAreas(int? locationId, bool includeAllAreas = true)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var query = context.LocationAreas.Where(l => l.LocationId == locationId);
                if (includeAllAreas)
                {
                    query = query.Union(context.LocationAreas.Where(l => l.Title.Trim().ToLower().Equals("any area")));
                }

                return this.MapList<LocationArea, LocationAreaSearchResult>(query.ToList());
            }
        }

        public List<LocationSearchResult> GetLocations()
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                return this.MapList<Location, LocationSearchResult>(context.Locations.ToList());
            }
        }

        public List<CategorySearchResult> GetTopLevelCategories()
        {
            return GetCategories().Where(c => c.ParentId == null).ToList();
        }

        public List<CategorySearchResult> GetCategories()
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var categories = context.MainCategories.OrderBy(c => c.Title).ToList();
                return this.MapList<MainCategory, CategorySearchResult>(categories);
            }
        }

        public SeoNameMappingModel GetSeoMapping(string seoName)
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var seoMappings = context.SeoMappings.FirstOrDefault(seo => seo.SeoName.Equals(seoName.Trim()));
                if (seoMappings == null)
                    return null;

                return this.Map<SeoMapping, SeoNameMappingModel>(seoMappings);
            }
        }

        public List<PublicationModel> GetPublications()
        {
            using (var context = _dbContextFactory.CreateClassifiedContext())
            {
                var publications = context.Publications
                    .Where(p => (p.PublicationTypeId == Constants.NewsPublicationTypeId ||
                        p.PublicationTypeId == Constants.MagPublicationTypeId) && p.Active == true)
                    .ToList();

                return this.MapList<Publication, PublicationModel>(publications);
            }
        }

        public IEnumerable<EventSearchResult> GetEvents(int? eventId = null)
        {
            using (var context = _dbContextFactory.CreateClassifiedSearchContext())
            {
                return context.BookedEvent_GetCurrent(eventId).Select(be => new EventSearchResult
                {
                    AdSearchResult = this.Map<BookedEvent, AdSearchResult>(be),
                    EventDetails = this.Map<BookedEvent, EventModel>(be),
                    Address = this.Map<BookedEvent, Address>(be),
                    TicketData = this.Map<BookedEvent, EventSearchResultTicketData>(be)
                }).ToList();
            }
        }

        public EventSearchResult GetEvent(int eventId)
        {
            return GetEvents(eventId).SingleOrDefault();
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

            configuration.CreateMap<BookedEvent, AdSearchResult>()
                .ForMember(member => member.Username, options => options.MapFrom(source => source.UserId))
                .ForMember(member => member.ImageUrls, options => options.MapFrom(source => source.DocumentIds.HasValue() ? source.DocumentIds.Split(',') : new string[0]))
                ;
            configuration.CreateMap<BookedEvent, EventModel>();
            configuration.CreateMap<BookedEvent, Address>();
            configuration.CreateMap<BookedEvent, EventSearchResultTicketData>();

            configuration.CreateMap<SeoMapping, SeoNameMappingModel>()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.CategoryIds.IsNullOrEmpty() ? new List<int>() : src.CategoryIds.Split(',').Select(int.Parse).ToList()))
                .ForMember(dest => dest.LocationIds, opt => opt.MapFrom(src => src.LocationIds.IsNullOrEmpty() ? new List<int>() : src.LocationIds.Split(',').Select(int.Parse).ToList()))
                .ForMember(dest => dest.AreaIds, opt => opt.MapFrom(src => src.AreaIds.IsNullOrEmpty() ? new List<int>() : src.AreaIds.Split(',').Select(int.Parse).ToList()))
                .ForMember(dest => dest.ParentCategoryId, opt => opt.MapFrom(src => src.ParentCategoryIds))
                ;

            configuration.CreateMap<Publication, PublicationModel>();

            // To DB
            configuration.CreateMap<SeoNameMappingModel, SeoMapping>();
        }
    }
}