
/****** Object:  View [dbo].[OnlineAdView]    Script Date: 6/07/2014 8:51:12 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


ALTER VIEW [dbo].[OnlineAdView]
WITH SCHEMABINDING 
AS
SELECT        dbo.OnlineAd.OnlineAdId, dbo.OnlineAd.AdDesignId, dbo.OnlineAd.Heading, dbo.OnlineAd.Description, dbo.OnlineAd.HtmlText, dbo.OnlineAd.Price, dbo.OnlineAd.LocationId, dbo.OnlineAd.LocationAreaId, 
                         dbo.OnlineAd.ContactName, dbo.OnlineAd.ContactPhone, dbo.OnlineAd.ContactEmail, dbo.OnlineAd.NumOfViews, dbo.OnlineAd.OnlineAdTag, dbo.AdBooking.StartDate, dbo.AdBooking.EndDate, 
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

SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO

/****** Object:  Index [ClusteredIndex-OnlineAdId]    Script Date: 6/07/2014 8:51:42 PM ******/
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-OnlineAdId] ON [dbo].[OnlineAdView]
(
	[OnlineAdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

DROP FULLTEXT CATALOG [OnlineAdSearch] 
CREATE FULLTEXT CATALOG [OnlineAdSearch] WITH ACCENT_SENSITIVITY = OFF

CREATE FULLTEXT INDEX ON [dbo].[OnlineAdView](
[AreaTitle] LANGUAGE [English], 
[BookReference] LANGUAGE [English], 
[CategoryTitle] LANGUAGE [English], 
[Description] LANGUAGE [English], 
[Heading] LANGUAGE [English], 
[LocationTitle] LANGUAGE [English], 
[OnlineAdTag] LANGUAGE [English], 
[UserId] LANGUAGE [English])
KEY INDEX [ClusteredIndex-OnlineAdId]ON ([OnlineAdSearch], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)

GO


