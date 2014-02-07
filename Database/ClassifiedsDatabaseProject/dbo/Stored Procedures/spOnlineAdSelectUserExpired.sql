-- =============================================
-- Author:			Dejan Vasic
-- Create date:		30-Jun-2009
-- Modifications
-- Date		Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spOnlineAdSelectUserExpired]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@EndDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT 	des.AdDesignID, bk.BookReference, bk.AdBookingId, des.AdTypeId, bk.EndDate,
			LEFT(onl.Heading, 25) + '...' as Title,
			CASE des.[Status]	WHEN 1 THEN 'Pending' 
								WHEN 2 THEN 'Approved' 
								WHEN 3 THEN 'Cancelled' 
			END AS Status, 
			cat.Title as Category, bk.StartDate, bk.EndDate
	FROM AdBooking bk
	INNER JOIN Ad ad ON ad.Adid = bk.Adid
	INNER JOIN AdDesign des ON des.AdId = bk.AdId
	INNER JOIN MainCategory cat ON cat.MainCategoryId = bk.MainCategoryId
	INNER JOIN AdType typ ON typ.AdTypeId = des.AdTypeId
	INNER JOIN OnlineAd onl ON onl.AdDesignId = des.AdDesignId
	WHERE	bk.UserId = @UserId
			AND bk.EndDate < GETDATE()
			AND bk.EndDate >= @EndDate
			AND typ.Code = 'ONLINE'
			
	ORDER BY bk.EndDate DESC
END






