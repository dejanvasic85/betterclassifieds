-- =============================================
-- Author:		Dejan Vasic
-- Create date: 1st April 2009
-- Modifications
-- Date			Author			Description

-- 10-06-09		Dejan Vasic		Booking Status column accepts 4 now. So we query for only 1 and 3.
-- 15-11-09		Dejan Vasic		Refined entire search routine to allow more search criteria
-- ==================================================================
CREATE PROCEDURE [dbo].[spAdBookingsSearch]

	-- Add the parameters for the stored procedure here
	@AdDesignId			INT = null,
	@BookReference		NVARCHAR(10) = null,
	@Username			NVARCHAR(50) = null,
	@BookingDateStart	DATETIME = null,
	@BookingDateEnd		DATETIME = null,
	@BookingStatus		INT = null,
	@PublicationId		INT = null,
	@ParentCategoryId	INT = null,
	@MainCategoryId		INT = null,
	@AdSearchText		NVARCHAR(50) = null,
	@EditionStartDate	DATETIME,
	@EditionEndDate		DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT DISTINCT	
	bk.AdBookingId, bk.BookReference,
	(	select		MainCategory.Title 
		from		MainCategory 
		where		MainCategory.MainCategoryId = mc.ParentId) 
	AS ParentCategory,
	mc.Title as SubCategory, bk.BookingDate, bk.BookingType,
	bk.TotalPrice, bk.UserId, bk.BookingStatus
	, 0 AS Editions
	, 0 AS Designs	
	FROM AdBooking bk
	INNER JOIN	MainCategory mc		ON mc.MainCategoryId = bk.MainCategoryId
	LEFT JOIN	BookEntry en		ON en.AdBookingId = bk.AdBookingId
	INNER JOIN	AdDesign ds			ON ds.AdId = bk.AdId
	LEFT JOIN	LineAd la			ON ds.AdDesignId = la.AdDesignId
	LEFT JOIN	OnlineAd oa			ON oa.AdDesignId = oa.AdDesignId
	
	WHERE 
			((@AdDesignId IS NULL) 
				OR (@AdDesignId = ds.AdDesignId))
		AND ((@BookReference IS NULL) 
				OR (@BookReference = bk.BookReference))
		AND ((@Username IS NULL) 
				OR (bk.UserId LIKE @Username + '%'))
		AND ((@Username IS NULL) 
				OR (bk.UserId LIKE @Username + '%'))
		AND ((@BookingDateStart IS NULL 
				AND @BookingDateEnd IS NULL) 
				OR (@BookingDateStart <= bk.BookingDate 
					AND @BookingDateEnd >= bk.BookingDate))
		AND ((@BookingStatus IS NULL) 
				OR (@BookingStatus = bk.BookingStatus))
		AND ((@PublicationId IS NULL) 
				OR (@PublicationId = en.PublicationId))
		AND ((@MainCategoryId IS NULL) 
				OR (@MainCategoryId = bk.MainCategoryId))
		AND (	(@AdSearchText IS NULL) 
				OR (la.AdText LIKE '%' + @AdSearchText + '%') 
				OR (la.AdHeader LIKE '%' + @AdSearchText + '%') )
		AND (	(@AdSearchText IS NULL) 
				OR (oa.Heading LIKE '%' + @AdSearchText + '%') 
				OR (oa.[Description] LIKE '%' + @AdSearchText + '%') )	
		AND	((@ParentCategoryId IS NULL) OR (mc.ParentId = @ParentCategoryId))
		AND	((@EditionStartDate IS NULL AND @EditionEndDate IS NULL)
				OR	(@EditionStartDate <= en.EditionDate AND @EditionEndDate >= en.EditionDate))
	
	ORDER BY ParentCategory, SubCategory , bk.BookingDate
END



