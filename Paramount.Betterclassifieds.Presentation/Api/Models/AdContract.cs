using System;
using AutoMapper;
using Humanizer;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Booking;
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
        public string EditAdUrl { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryName { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateHumanized { get; set; }
        public string LocationName { get; set; }
        public string LocationAreaName { get; set; }
        public string CategoryFontIcon { get; set; }
        public string PrimaryImage { get; set; }
        public string CategoryAdType { get; set; }

    }

    public class AdContractFactory : IMappingBehaviour
    {
        private readonly IUrl _url;
        private readonly ICategoryAdFactory _categoryAdFactory;

        public AdContractFactory(IUrl url, ICategoryAdFactory categoryAdFactory)
        {
            _url = url;
            _categoryAdFactory = categoryAdFactory;
        }

        public AdContract Create(AdSearchResult searchResult)
        {
            var contract = this.Map<AdSearchResult, AdContract>(searchResult);
            
            // Additional non-auto properties
            contract.AdUrl = _url.AdUrl(searchResult.Heading, searchResult.AdId, searchResult.CategoryAdType);
            contract.StartDateHumanized = searchResult.StartDate.Humanize(utcDate: false);
            contract.EditAdUrl = CreateEditUrl(searchResult.AdId, searchResult.CategoryAdType);

            return contract;
        }

        public AdContract Create(AdBookingModel bookingModel)
        {
            var contract = this.Map<AdBookingModel, AdContract>(bookingModel);

            // Additional non-auto properties
            contract.AdUrl = _url.AdUrl(bookingModel.OnlineAd.Heading, bookingModel.AdBookingId, bookingModel.CategoryAdType);
            contract.StartDateHumanized = bookingModel.StartDate.Humanize(utcDate: false);
            contract.EditAdUrl = CreateEditUrl(bookingModel.AdBookingId, bookingModel.CategoryAdType);

            return contract;
        }

        private string CreateEditUrl(int adId, string categoryAdType)
        {
            if (categoryAdType.HasValue())
            {
                var categoryAdUrlService = _categoryAdFactory.CreateUrlService(categoryAdType);
                return categoryAdUrlService.EditUrl(adId);
            }

            return _url.EditAdUrl(adId);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<AdSearchResult, AdContract>()
                .ForMember(m => m.Title, options => options.MapFrom(s => s.Heading))
                .ForMember(m => m.ShortTitle, options => options.MapFrom(s => s.Heading.TruncateOnWordBoundary(35)))
                ;

            configuration.CreateMap<AdBookingModel, AdContract>()
                .ForMember(m => m.Title, options => options.MapFrom(src => src.OnlineAd.Heading))
                .ForMember(m => m.ShortTitle, options => options.MapFrom(s => s.OnlineAd.Heading.TruncateOnWordBoundary(35)))
                .ForMember(m => m.AdId, options => options.MapFrom(src => src.AdBookingId))
                .ForMember(m => m.PrimaryImage, options => options.MapFrom(src => src.OnlineAd.PrimaryImageId))
                ;

        }
    }
}