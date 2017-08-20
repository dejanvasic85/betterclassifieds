using System;
using AutoMapper;
using Humanizer;
using Paramount.Betterclassifieds.Business.Search;
using Paramount.Betterclassifieds.Presentation.Services;

namespace Paramount.Betterclassifieds.Presentation.Api.Models
{
    public class AdContract
    {
        public int AdId { get; set; }
        public string Title { get; set; }
        public string ShortTitle { get; set; }
        public string AdUrl { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryName { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateHumanized { get; set; }
        public string LocationName { get; set; }
        public string LocationAreaName { get; set; }
        public string CategoryFontIcon { get; set; }
        public string PrimaryImage { get; set; }

    }

    public class AdContractFactory : IMappingBehaviour
    {
        private readonly IUrl _url;

        public AdContractFactory(IUrl url)
        {
            _url = url;
        }

        public AdContract Create(AdSearchResult searchResult)
        {
            var contract = this.Map<AdSearchResult, AdContract>(searchResult);

            // Additional non-auto properties
            contract.AdUrl = _url.AdUrl(searchResult.Heading, searchResult.AdId, searchResult.CategoryAdType);
            contract.StartDateHumanized = searchResult.StartDate.Humanize(utcDate: false);

            return contract;
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<AdSearchResult, AdContract>()
                .ForMember(m => m.Title, options => options.MapFrom(s => s.Heading))
                .ForMember(m => m.ShortTitle, options => options.MapFrom(s => s.Heading.TruncateOnWordBoundary(35)))
                ;

        }
    }
}