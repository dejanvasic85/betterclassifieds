using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Tests
{
    internal partial class EventModelMockBuilder
    {
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

            return this;
        }
    }

    internal partial class AdSearchResultMockBuilder
    {
        public AdSearchResultMockBuilder Default()
        {
            WithAdId(789);
            WithBookingDate(DateTime.Now.AddDays(-1));
            WithCategoryAdType(null);
            WithParentCategoryId(90);
            WithCategoryId(444);
            WithCategoryName("Employment");
            WithContactEmail("contact@email.com");
            WithContactName("John Smith");
            WithContactPhone("03 9999 9999");
            WithDescription("description mock ad only");
            WithHeading("heading of mock ad");
            WithStartDate(DateTime.Now.AddDays(-1));
            WithEndDate(DateTime.Now.AddMonths(2));
            WithHtmlText("<h1>Description mock ad only</h1>");
            WithLocationAreaId(10);
            WithLocationAreaName("area");
            WithLocationId(1);
            WithLocationName("Location");
            WithNumOfViews(100);
            WithPrice(15);
            WithUsername("user123");
            WithImageUrls(new[] { "img1", "img2" });
            WithOnlineAdId(1000);
            return this;
        }
    }

}