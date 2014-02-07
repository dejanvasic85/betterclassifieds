-- =============================================
-- Author:		Dejan Vasic
-- Create date: 30th January 2009
-- Modifications
-- Date			Author			Description
--
-- 15-4-2009	Dejan Vasic		Added WHERE parameter for Ad Design Status not to include "cancelled = 3 "
-- =============================================
CREATE PROCEDURE [dbo].[spMainCategoryAdCount]
	-- Add the parameters
	@bookStatus INT
AS

-- use a temporary table to store all categories with the number of ad bookings in them
DECLARE @tmp TABLE (MainCategoryId INT, Title NVARCHAR(100), ParentId INT, [Count] INT)

BEGIN
INSERT INTO @tmp
	-- Do the insert into the temporary table.
	SELECT MainCategory.MainCategoryId, MainCategory.Title, ParentId, (
								SELECT COUNT(OnlineAdId) 
								FROM OnlineAd
								INNER JOIN AdDesign ON AdDesign.AdDesignId = OnlineAd.AdDesignId
								INNER JOIN AdBooking ON AdBooking.AdId = AdDesign.AdId
								WHERE 
								AdBooking.MainCategoryId = MainCategory.MainCategoryId
								AND AdBooking.EndDate > GETDATE()
								AND AdBooking.StartDate < GETDATE()
								AND AdDesign.[Status] <> 3
								AND AdBooking.BookingStatus = @bookStatus) AS [Count]
								
	FROM MainCategory
END


SELECT mc.MainCategoryId, mc.Title + ' (' + CAST(SUM(t.[COUNT]) AS NVARCHAR(MAX))  + ')' AS Title
FROM @tmp t
	INNER JOIN MainCategory mc ON mc.MainCategoryId = t.ParentId
GROUP BY mc.Title, mc.MainCategoryId
ORDER BY mc.Title

