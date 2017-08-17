
/****** Object: View [dbo].[BookedEvents] Script Date: 15/08/2017 7:17:56 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[BookedEvents]
/*
	select * from BookedEvents
*/



WITH SCHEMABINDING 
AS
SELECT	 e.[EventId]
		,e.[Location]
		,e.[LocationLatitude]
		,e.[LocationLongitude]
		,e.[EventStartDate]
		,e.[EventEndDate]
		,e.[ClosingDate]
		,e.[ClosingDateUtc]
		,e.[TimeZoneId]
		,e.[TimeZoneName]
		,e.[TimeZoneDaylightSavingsOffsetSeconds]
		,e.[TimeZoneUtcOffsetSeconds]
		,e.[LocationFloorPlanDocumentId]
		,e.[LocationFloorPlanFilename]
		,e.[IncludeTransactionFee]
		,e.[GroupsRequired]
		,addr.[AddressId]
		,addr.[StreetNumber]
		,addr.[StreetName]
		,addr.[Suburb]
		,addr.[State]
		,addr.[Postcode]
		,addr.[Country]
		, o.[OnlineAdId]
		, o.[AdDesignId]
		, o.[Heading]
		, o.[Description]
		, o.[HtmlText]
		, o.[Price]
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
		, bk.[Priority]
		, c.Title as CategoryName
		, bk.MainCategoryId as CategoryId
		, c.ParentId as ParentCategoryId
		, c.CategoryAdType
FROM    dbo.[Event] e
	INNER JOIN dbo.OnlineAd o ON o.OnlineAdId = e.OnlineAdId
	INNER JOIN	dbo.AdDesign ds ON ds.AdDesignId = o.AdDesignId
	INNER JOIN	dbo.AdBooking bk ON bk.AdId = ds.AdId
	INNER JOIN	dbo.MainCategory c ON c.MainCategoryId = bk.MainCategoryId
	INNER JOIN	dbo.[Address] addr ON addr.AddressId = e.AddressId
WHERE bk.BookingStatus = 1