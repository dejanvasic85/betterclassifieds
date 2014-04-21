using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.DataService.Classifieds;

namespace Paramount.Betterclassifieds.DataService
{
    public class SearchService : ISearchService, IMappingBehaviour
    {
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
                                  ImageUrls = onl.AdDesign.AdGraphics.Select(g=> g.DocumentID).ToArray(),
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

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("SearchingProfile");

            // From DB
            configuration.CreateMap<MainCategory, CategorySearchResult>();
            configuration.CreateMap<Location, LocationSearchResult>();
        }
    }
}