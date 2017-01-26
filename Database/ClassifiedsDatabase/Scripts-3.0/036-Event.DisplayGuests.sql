ALTER TABLE [Event]
ADD DisplayGuests BIT NULL 


GO 

UPDATE dbo.[Event]
SET DisplayGuests = 0

GO

ALTER TABLE dbo.[Event]
ALTER COLUMN DisplayGuests BIT NOT NULL


GO



/****** Object:  View [dbo].[BookedEvents]    Script Date: 10/01/2017 10:34:50 PM ******/
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
		,e.[EventStartDateUtc]
		,e.[EventEndDate]
		,e.[EventEndDateUtc]
		,e.[ClosingDate]
		,e.[ClosingDateUtc]
		,e.[OpeningDate]
		,e.[OpeningDateUtc]
		,e.[TimeZoneId]
		,e.[TimeZoneName]
		,e.[TimeZoneDaylightSavingsOffsetSeconds]
		,e.[TimeZoneUtcOffsetSeconds]
		,e.[LocationFloorPlanDocumentId]
		,e.[LocationFloorPlanFilename]
		,e.[IncludeTransactionFee]
		,e.[GroupsRequired]
		,e.[DisplayGuests]
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
GO