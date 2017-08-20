using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Api.Models;

namespace Paramount.Betterclassifieds.Presentation.Api
{
    [RoutePrefix("api/listings")]
    public class ListingApiController : ApiController
    {
        private readonly ISearchService _searchService;
        private readonly AdContractFactory _adContractFactory;

        public ListingApiController(ISearchService searchService, 
            AdContractFactory adContractFactory)
        {
            _searchService = searchService;
            _adContractFactory = adContractFactory;
        }

        [HttpGet]
        [Route("search")]
        public IHttpActionResult Search([FromUri]ListingQuery query)
        {
            var results = _searchService.GetAds(query.SearchTerm,
                query.CategoryIds, null, null, query.PageNumber.GetValueOrDefault(0),
                query.PageSize, AdSearchSortOrder.NewestFirst);

            var contracts = results.Select(_adContractFactory.Create);

            return Ok(contracts);
        }
    }
}