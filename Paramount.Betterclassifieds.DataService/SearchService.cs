using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.DataService
{
    public class SearchService : ISearchService
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
                                  ParentCategoryId = cat.ParentId
                              };

                return results.ToList();
            }
        }
    }
}