-- =============================================
-- Author:			Dejan Vasic
-- Create date:		28-Jun-2009
-- Modifications
-- Date		Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spAdBookingSelectUserActive]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@BookingStatus INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT	bk.AdBookingId, bk.StartDate, bk.EndDate, bk.TotalPrice, bk.BookReference, cat.Title,
			(SELECT count(AdDesignId) 
			FROM AdDesign inner join AdBooking bk1 on bk1.AdId = AdDesign.AdId 
			WHERE bk1.AdBookingId = bk.AdBookingId) as NumOfAds
	FROM AdBooking bk
	INNER JOIN MainCategory cat ON cat.MainCategoryId = bk.MainCategoryId
	WHERE bk.UserId = @UserId AND bk.EndDate > GETDATE() AND bk.BookingStatus = @BookingStatus
	ORDER BY bk.StartDate DESC
END


