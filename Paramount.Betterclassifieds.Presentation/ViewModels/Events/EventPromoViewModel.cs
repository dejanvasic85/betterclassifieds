using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventPromoViewModel
    {
        public long EventPromoCodeId { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public string PromoCode { get; set; }
        public decimal? DiscountPercent { get; set; }
        public bool? IsDisabled { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedDateUtc { get; set; }
    }

    public class ManageEventPromoViewModel
    {
        public int AdId { get; set; }
        public int EventId { get; set; }
        public IEnumerable<EventPromoViewModel> PromoCodes { get; set; }

    }

    public class EventPromoViewModelFactory : IMappingBehaviour
    {
        public ManageEventPromoViewModel CreateManageViewModel(int adId, int eventId, IEnumerable<EventPromoCode> promoCodes)
        {
            return new ManageEventPromoViewModel
            {
                AdId = adId,
                EventId = eventId,
                PromoCodes = promoCodes.Select(CreatePromoCode)
            };
        }

        public EventPromoViewModel CreatePromoCode(EventPromoCode eventPromoCode)
        {
            Guard.NotNull(eventPromoCode);
            return this.Map<EventPromoCode, EventPromoViewModel>(eventPromoCode);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateMap<EventPromoCode, EventPromoViewModel>()
                .ForMember(m => m.CreatedDate, options => options.MapFrom(src => src.CreatedDate.ToIsoDateString()))
                .ForMember(m => m.CreatedDateUtc, options => options.MapFrom(src => src.CreatedDateUtc.ToUtcIsoDateString()));
        }
    }
}