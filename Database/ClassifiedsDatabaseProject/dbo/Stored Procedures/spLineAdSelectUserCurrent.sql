-- =============================================
-- Author:			Dejan Vasic
-- Create date:		28-Jun-2009
-- Modifications
-- Date		Author			Description
-- 24-1-10	Dejan Vasic		Altered procedure not to consider the online publication
-- =============================================
CREATE PROCEDURE [dbo].[spLineAdSelectUserCurrent]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@BookingStatus INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT ds.AdDesignId
		, bk.BookReference
		, LEFT(la.AdHeader, 25) + '...' as Title
		, mc.Title as Category
		, bk.AdBookingId
		, bk.EndDate
	FROM dbo.AdBooking bk
		
		INNER JOIN (	SELECT MIN(be.EditionDate) AS EditionDate, be.AdBookingId
						FROM BookEntry be
						INNER JOIN Publication pb ON pb.PublicationId = be.PublicationId
						INNER JOIN PublicationType pbt ON pbt.PublicationTypeId = pb.PublicationTypeId
						WHERE pbt.Code <> 'ONLINE'
						GROUP BY be.AdBookingId ) AS FirstDateEntry ON FirstDateEntry.AdBookingId = bk.AdBookingId

		INNER JOIN (	SELECT MAX(be.EditionDate) AS EditionDate, be.AdBookingId 
						FROM BookEntry be
						INNER JOIN Publication pb ON pb.PublicationId = be.PublicationId
						INNER JOIN PublicationType pbt ON pbt.PublicationTypeId = pb.PublicationTypeId
						WHERE pbt.Code <> 'ONLINE'
						GROUP BY be.AdBookingId ) AS LastDateEntry ON LastDateEntry.AdBookingId = bk.AdBookingId
						
		INNER JOIN MainCategory mc ON mc.MainCategoryId = bk.MainCategoryId
		INNER JOIN AdDesign ds ON ds.AdId = bk.AdId
		INNER JOIN AdType tp ON tp.AdTypeId = ds.AdTypeId
		INNER JOIN Ad ad ON ad.AdId = bk.AdId
		INNER JOIN LineAd la ON la.AdDesignId = ds.AdDesignId
	WHERE	LastDateEntry.EditionDate >= GETDATE() 
			AND FirstDateEntry.EditionDate <= GETDATE()
			AND bk.UserId = @UserId
			AND bk.BookingStatus = @BookingStatus
			AND tp.Code = 'LINE'
	ORDER BY FirstDateEntry.EditionDate DESC

END
