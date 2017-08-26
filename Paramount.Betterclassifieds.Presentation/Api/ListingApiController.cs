using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Paramount.Betterclassifieds.Business;
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

        public ListingApiController(ISearchService searchService, 
            AdContractFactory adContractFactory, IUserManager userManager)
        {
            _searchService = searchService;
            _adContractFactory = adContractFactory;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search([FromUri]ListingQuery query)
        {

            var user = query.User ? _userManager.GetCurrentUser().Username : null;

            var results = _searchService.GetAds(query.SearchTerm,
                query.CategoryIds, null, null, query.PageNumber.GetValueOrDefault(0),
                query.PageSize, AdSearchSortOrder.NewestFirst, user);

            var contracts = results.Select(_adContractFactory.Create);

            return Ok(contracts);
        }
    }
}