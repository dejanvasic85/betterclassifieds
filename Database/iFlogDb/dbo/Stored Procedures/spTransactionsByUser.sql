-- =============================================
-- Author:			Dejan Vasic
-- Create date:		7-Jul-2009
-- Modifications
-- Date		Author			Description
-- 18-10-09	Dejan Vasic		Added Where clause not to display unpaid transactions	
-- =============================================
CREATE PROCEDURE [dbo].[spTransactionsByUser]
	-- Add the parameters for the stored procedure here
	@UserId NVARCHAR(50),
	@StartDate DATETIME
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
SELECT	tr.Title as [Ref No], tr.TransactionDate, tr.TransactionType, tr.Amount, tr.UserId,
		(SELECT mc.Title FROM MainCategory mc WHERE mc.MainCategoryId = bk.MainCategoryId) as Category,
		(SELECT count(AdDesignId) 
			FROM AdDesign inner join AdBooking bk1 on bk1.AdId = AdDesign.AdId 
			WHERE bk1.AdBookingId = bk.AdBookingId) as NumOfAds
		
FROM dbo.[Transaction] tr
	INNER JOIN AdBooking bk on bk.BookReference = tr.Title
WHERE	tr.[TransactionDate] > @StartDate AND tr.UserId = @UserId
		and bk.BookingStatus <> 4
ORDER BY tr.[TransactionDate] DESC

END
