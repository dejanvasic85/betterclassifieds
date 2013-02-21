CREATE VIEW [dbo].[RssFeed]
AS
SELECT     TOP 100  dbo.OnlineAd.OnlineAdId, dbo.AdBooking.StartDate, dbo.AdBooking.EndDate, dbo.AdBooking.BookReference, dbo.AdBooking.UserId, 
                      dbo.AdBooking.MainCategoryId, dbo.Ad.AdId, dbo.Ad.Title, dbo.OnlineAd.Heading, dbo.OnlineAd.HtmlText, dbo.OnlineAd.Description, dbo.OnlineAd.Price, 
                      dbo.OnlineAd.ContactName, dbo.MainCategory.ParentId, dbo.AdBooking.BookingDate, dbo.MainCategory.Title as CategoryTitle
FROM         dbo.OnlineAd INNER JOIN
                      dbo.AdDesign ON dbo.OnlineAd.AdDesignId = dbo.AdDesign.AdDesignId INNER JOIN
                      dbo.Ad ON dbo.AdDesign.AdId = dbo.Ad.AdId INNER JOIN
                      dbo.AdBooking ON dbo.Ad.AdId = dbo.AdBooking.AdId INNER JOIN
                      dbo.MainCategory ON dbo.AdBooking.MainCategoryId = dbo.MainCategory.MainCategoryId
WHERE     (dbo.AdBooking.BookingStatus = 1)
ORDER BY dbo.AdBooking.BookingDate DESC
