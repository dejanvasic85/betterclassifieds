
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[OnlineAdView]
WITH SCHEMABINDING 
AS
SELECT        dbo.OnlineAd.OnlineAdId, dbo.OnlineAd.AdDesignId, dbo.OnlineAd.Heading, dbo.OnlineAd.Description, dbo.OnlineAd.HtmlText, dbo.OnlineAd.Price, dbo.OnlineAd.LocationId, dbo.OnlineAd.LocationAreaId, 
                         dbo.OnlineAd.ContactName, dbo.OnlineAd.ContactType, dbo.OnlineAd.ContactValue, dbo.OnlineAd.NumOfViews, dbo.OnlineAd.OnlineAdTag, dbo.AdBooking.StartDate, dbo.AdBooking.EndDate, 
                         dbo.AdBooking.AdBookingId, dbo.AdBooking.BookReference, dbo.AdBooking.UserId, dbo.AdBooking.BookingStatus, dbo.AdBooking.MainCategoryId AS CategoryId, dbo.AdBooking.BookingDate, 
                         dbo.LocationArea.Title AS AreaTitle, dbo.Location.Title AS LocationTitle, dbo.MainCategory.Title AS CategoryTitle, dbo.MainCategory.ParentId AS CategoryParentId, dbo.Ad.AdId
FROM            dbo.AdDesign AS AdDesign INNER JOIN
                         dbo.LocationArea INNER JOIN
                         dbo.OnlineAd ON dbo.LocationArea.LocationAreaId = dbo.OnlineAd.LocationAreaId INNER JOIN
                         dbo.Location ON dbo.LocationArea.LocationId = dbo.Location.LocationId AND dbo.OnlineAd.LocationId = dbo.Location.LocationId ON AdDesign.AdDesignId = dbo.OnlineAd.AdDesignId INNER JOIN
                         dbo.Ad ON AdDesign.AdId = dbo.Ad.AdId INNER JOIN
                         dbo.MainCategory INNER JOIN
                         dbo.AdBooking ON dbo.MainCategory.MainCategoryId = dbo.AdBooking.MainCategoryId ON dbo.AdBooking.AdId = dbo.Ad.AdId 
WHERE        (dbo.AdBooking.BookingStatus = 1) AND (AdDesign.AdTypeId = 2)


GO