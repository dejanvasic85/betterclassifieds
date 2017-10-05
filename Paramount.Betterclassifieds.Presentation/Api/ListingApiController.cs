using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Api.Models;

namespace Paramount.Betterclassifieds.Presentation.Api
{
    [RoutePrefix("api/listings")]
    public class ListingApiController : ApiController
    {
        private readonly ISearchService _searchService;
        private readonly AdContractFactory _adContractFactory;
        private readonly IUserManager _userManager;
        private readonly IBookingManager _bookingManager;

        public ListingApiController(ISearchService searchService,
            AdContractFactory adContractFactory, IUserManager userManager,
            IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
            _searchService = searchService;
            _adContractFactory = adContractFactory;
            _userManager = userManager;
        }


        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search([FromUri]ListingQuery query)
        {
            if (query.User)
            {
                var userAds = _bookingManager.GetBookingsForUser(_userManager.GetCurrentUser().Username, query.PageSize);

                return Ok(userAds.Select(_adContractFactory.Create));
            }

            var results = _searchService.GetAds(query.SearchTerm,
                query.CategoryIds, null, null, query.PageNumber.GetValueOrDefault(0),
                query.PageSize, AdSearchSortOrder.NewestFirst, null);

            var contracts = results.Select(_adContractFactory.Create);

            return Ok(contracts);
        }
    }
}