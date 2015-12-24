using System;

namespace Paramount.Betterclassifieds.Tests
{
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