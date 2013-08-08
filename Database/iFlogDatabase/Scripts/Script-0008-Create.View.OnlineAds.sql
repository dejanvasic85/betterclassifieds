USE [iFlog]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Uche Njoku
-- Create date: 8th August 2013
-- =============================================

DROP VIEW [dbo].[OnlineAds]
GO


CREATE VIEW [dbo].[OnlineAds]
AS
SELECT        o.OnlineAdId, o.AdDesignId, o.Heading, o.Description, o.HtmlText, o.Price, o.LocationId, o.LocationAreaId, o.ContactName, o.ContactType, o.ContactValue, 
                         o.NumOfViews, dbo.AdBooking.UserId, dbo.AdBooking.TotalPrice, dbo.AdBooking.StartDate, dbo.AdBooking.EndDate, dbo.AdBooking.BookReference, 
                         dbo.AdBooking.BookingStatus, dbo.AdBooking.MainCategoryId, dbo.AdBooking.Insertions, dbo.AdBooking.BookingDate, dbo.AdBooking.BookingType, 
                         dbo.AdBooking.AdBookingId, dbo.MainCategory.Title AS Category, dbo.MainCategory.ParentId, l.Title, LA.Title AS Area, dbo.Ad.AdId
FROM            dbo.Ad INNER JOIN
                         dbo.AdBooking ON dbo.Ad.AdId = dbo.AdBooking.AdId INNER JOIN
                         dbo.AdDesign ON dbo.Ad.AdId = dbo.AdDesign.AdId INNER JOIN
                         dbo.OnlineAd AS o ON dbo.AdDesign.AdDesignId = o.AdDesignId INNER JOIN
                         dbo.MainCategory ON dbo.AdBooking.MainCategoryId = dbo.MainCategory.MainCategoryId INNER JOIN
                         dbo.Location AS l ON l.LocationId = o.LocationId INNER JOIN
                         dbo.LocationArea AS LA ON LA.LocationAreaId = o.LocationAreaId
WHERE        (dbo.AdBooking.BookingStatus = 1) AND (dbo.AdDesign.AdTypeId = 2)

GO