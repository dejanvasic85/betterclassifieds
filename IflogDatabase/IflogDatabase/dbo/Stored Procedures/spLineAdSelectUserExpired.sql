-- =============================================
-- Author:			Dejan Vasic
-- Create date:		30-Jun-2009
-- Modifications
-- Date		Author			Description
-- 19-1-10	Dejan Vasic		Adjusted the end date so that it doesn't use Booking table but BookEntry instead.
-- =============================================
CREATE PROCEDURE [dbo].[spLineAdSelectUserExpired]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@EndDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SET NOCOUNT ON;
	SELECT ds.AdDesignId
			, bk.BookReference
			, LEFT(la.AdHeader, 25) + '...' as Title
			, mc.Title as Category, bk.AdBookingId
			, ds.AdTypeId
			, BookEntry.EditionDate as EndDate
	FROM dbo.AdBooking bk
		INNER JOIN (	SELECT MAX(be.EditionDate) as EditionDate, be.AdBookingId 
						FROM BookEntry be
						INNER JOIN Publication pb ON pb.PublicationId = be.PublicationId
						INNER JOIN PublicationType pbt ON pbt.PublicationTypeId = pb.PublicationTypeId
						WHERE pbt.Code <> 'ONLINE'
						GROUP BY be.AdBookingId ) as BookEntry ON BookEntry.AdBookingId = bk.AdBookingId
		INNER JOIN MainCategory mc ON mc.MainCategoryId = bk.MainCategoryId
		INNER JOIN AdDesign ds ON ds.AdId = bk.AdId
		INNER JOIN AdType tp ON tp.AdTypeId = ds.AdTypeId
		INNER JOIN Ad ad ON ad.AdId = bk.AdId
		INNER JOIN LineAd la ON la.AdDesignId = ds.AdDesignId
	WHERE	
			bk.UserId = @UserId
			AND tp.Code = 'LINE'
			AND BookEntry.EditionDate < GETDATE()
			AND BookEntry.EditionDate > @EndDate
	ORDER BY BookEntry.EditionDate

END



