
/****** Object:  View [dbo].[BookedAds]    Script Date: 6/07/2014 8:49:37 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[BookedAds]
/*
	select * from BookedAds
*/
WITH SCHEMABINDING 
AS
SELECT	  o.[OnlineAdId]
		, o.[AdDesignId]
		, o.[Heading]
		, o.[Description]
		, o.[HtmlText]
		, o.[Price]
		, o.[LocationId]
		, o.[LocationAreaId]
		, o.[ContactName]
		, o.[ContactPhone]
		, o.[ContactEmail]
		, o.[NumOfViews]
		, o.[OnlineAdTag]
		, bk.AdBookingId as AdId
		, bk.BookingDate
		, bk.EndDate
		, bk.UserId
		, bk.StartDate 		
		, c.Title as CategoryName
		, bk.MainCategoryId as CategoryId
		, c.ParentId as ParentCategoryId
		, lo.Title as LocationName
		, la.Title as LocationAreaName
FROM    dbo.OnlineAd o
	INNER JOIN	dbo.AdDesign ds ON ds.AdDesignId = o.AdDesignId
	INNER JOIN	dbo.AdBooking bk ON bk.AdId = ds.AdId
	INNER JOIN	dbo.MainCategory c ON c.MainCategoryId = bk.MainCategoryId
	INNER JOIN	dbo.Location lo ON lo.LocationId = o.LocationId
	INNER JOIN	dbo.LocationArea la ON la.LocationAreaId = o.LocationAreaId
WHERE bk.BookingStatus = 1
GO



SET ARITHABORT ON
SET CONCAT_NULL_YIELDS_NULL ON
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
SET NUMERIC_ROUNDABORT OFF

GO

/****** Object:  Index [ClusteredIndex-BookedAds-OnlineAdId]    Script Date: 6/07/2014 8:49:10 PM ******/
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-BookedAds-OnlineAdId] ON [dbo].[BookedAds]
(
	[OnlineAdId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


DROP FULLTEXT CATALOG [BookedAdsFullText]
GO
CREATE FULLTEXT CATALOG [BookedAdsFullText] WITH ACCENT_SENSITIVITY = OFF

CREATE FULLTEXT INDEX ON [dbo].[BookedAds](
[LocationAreaName] LANGUAGE [English], 
[CategoryName] LANGUAGE [English], 
[Description] LANGUAGE [English], 
[Heading] LANGUAGE [English], 
[LocationName] LANGUAGE [English], 
[OnlineAdTag] LANGUAGE [English], 
[UserId] LANGUAGE [English])
KEY INDEX [ClusteredIndex-BookedAds-OnlineAdId] ON ([BookedAdsFullText], FILEGROUP [PRIMARY])
WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)

