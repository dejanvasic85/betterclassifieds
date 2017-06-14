using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventModelMockBuilder
    {
        public EventModelMockBuilder Default()
        {
            WithEventId(123);
            WithFutureClosedDate();
            WithLocation("1 Event Street, Earth");
            WithLocationLongitude(249);
            WithLocationLatitude(222);
            WithEventStartDate(DateTime.Now.AddDays(29));
            WithEventEndDate(DateTime.Now.AddDays(30));
            WithOnlineAdId(321);
            WithPromoCode("promo-123");

            return this;
        }

        public EventModelMockBuilder WithPastClosedDate()
        {
            WithBuildStep(p => p.ClosingDateUtc = DateTime.UtcNow.AddDays(-2));
            WithBuildStep(p => p.ClosingDate = DateTime.Now.AddDays(-2));
            return this;
        }

        public EventModelMockBuilder WithFutureClosedDate()
        {
            WithBuildStep(p => p.ClosingDateUtc = DateTime.UtcNow.AddDays(2));
            WithBuildStep(p => p.ClosingDate = DateTime.Now.AddDays(2));
            return this;
        }

        public EventModelMockBuilder WithCustomTicket(params EventTicket[] eventTickets)
        {
            return WithTickets(eventTickets.ToList());
        }

        public EventModelMockBuilder WithOrganiser(EventOrganiser organiser)
        {
            this.WithEventOrganisers(new List<EventOrganiser>
            {
                organiser
            });
            return this;
        }

        public EventModelMockBuilder WithPromoCode(string promoCode)
        {
            this.WithPromoCodes(new List<EventPromoCode>
            {
                new EventPromoCode
                {
                    PromoCode = promoCode
                }
            });
            return this;
        }

        public EventModelMockBuilder WithPromoCode(EventPromoCode promoCode)
        {
            this.WithPromoCodes(new List<EventPromoCode>() { promoCode });
            return this;
        }
    }
}