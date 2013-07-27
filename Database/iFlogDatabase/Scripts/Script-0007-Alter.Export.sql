USE [iFlog]
GO
/****** Object:  StoredProcedure [dbo].[spLineAdExportList]    Script Date: 07/27/2013 21:19:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Dejan Vasic
-- Create date: 18th August 2009
-- Modifications
-- Date			Author			Description
-- 29-11-2009	Dejan Vasic		Added Sorting on concatenated header and text
-- 21-12-2009	Dejan Vasic		Added ISNULL on the concetanation so it always appends text
-- 21-05-2010	Dejan Vasic		Reconstructed entire proc to display the right iFlog id.
-- 30-05-2010	Dejan Vasic		Added ISNULL on the Use Photo to ensure it's coming back with value always
-- =============================================
/*
	e.g 
	exec spLineAdExportList
		@publicationId = 1,
		@editionDate = '30-JUL-2013';
*/
ALTER PROCEDURE [dbo].[spLineAdExportList]
	-- Add the parameters for the stored procedure here
	@publicationId INT,
	@editionDate DATETIME
AS
declare @lineAdTypeId	int
declare @onlineAdTypeId	int
declare @statusBooked	int

set @lineAdTypeId	= 1;
set @onlineAdTypeId	= 2;
set @statusBooked	= 1;

DECLARE @onlineAds TABLE (
	AdBookingId INT,
	OnlineAdId INT
);

INSERT INTO @onlineAds
SELECT	bk.AdBookingId, o.OnlineAdId
FROM	dbo.AdBooking bk
JOIN	dbo.AdDesign ds
	ON	ds.AdId = bk.AdId
JOIN	dbo.OnlineAd o
	ON	o.AdDesignId = ds.AdDesignId
JOIN	dbo.BookEntry be
	ON  be.AdBookingId = bk.AdBookingId
WHERE	be.PublicationId = @publicationId
AND		be.EditionDate = @editionDate

BEGIN
	SELECT DISTINCT 
		ISNULL((	SELECT	AdDesignId 
					FROM	AdDesign ks
					JOIN	Ad 
						ON	Ad.AdId = ks.AdId
					WHERE	Ad.AdId = ak.AdId and ks.AdTypeId = @onlineAdTypeId
				), ds.AdDesignId) AS iflogId
		, bk.AdBookingId
		, bk.TotalPrice
		, ds.AdDesignId
		, la.LineAdId
		, o.OnlineAdId
		, (	SELECT	mc.MainCategoryId 
			FROM	MainCategory mc 
			WHERE	mc.MainCategoryId = sc.ParentId) AS MainCategoryId
		, (	SELECT	mc.Title 
			FROM	MainCategory mc 
			WHERE	mc.MainCategoryId = sc.ParentId) as MainCategory
		, sc.MainCategoryId AS SubCategoryId
		, sc.Title as SubCategory
		, (ISNULL(la.AdHeader,'') + la.AdText) as HeaderAndText
		, la.AdText
		, la.AdHeader
		, la.NumOfWords
		, (ISNULL(la.UsePhoto, 0)) as UsePhoto
		, la.UseBoldHeader
		, bk.UserId
		, en.BookEntryId
		, la.BoldHeadingColourCode
		, la.BackgroundColourCode
		, la.BorderColourCode
		, la.IsSuperBoldHeading
	FROM	AdBooking bk
	JOIN	BookEntry en 
		ON	en.AdBookingId = bk.AdBookingId
	JOIN	AdDesign ds 
		ON	ds.AdId = bk.AdId
	JOIN	LineAd la 
		ON	la.AdDesignId = ds.AdDesignId
	JOIN	AdType tp 
		ON	tp.AdTypeId = ds.AdTypeId
	JOIN	MainCategory sc 
		ON	sc.MainCategoryId = bk.MainCategoryId
	JOIN	Ad ak 
		ON	ak.AdId = ds.AdId
	LEFT JOIN @onlineAds o
		ON	o.AdBookingId = bk.AdBookingId
	WHERE	en.PublicationId = @publicationId
			AND en.EditionDate = @editionDate
			AND tp.AdTypeId = @lineAdTypeId
			AND bk.BookingStatus = @statusBooked

	ORDER BY MainCategory, sc.Title, HeaderAndText
END

