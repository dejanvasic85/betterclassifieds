
-- =============================================
-- Author:		Dejan Vasic
-- Create date: 9th March 2009

-- Modifications
-- Date			Author			Description
--
-- 15-4-2009	Dejan Vasic		Added WHERE parameter for Ad Design Status not to include "cancelled = 3 "
-- 21-5-2010	Dejan Vasic		Selecting only the graphics that contain a graphic
-- =============================================
CREATE PROCEDURE [dbo].[spOnlineAdSelectRecentlyAdded]
	-- Add the parameters for the stored procedure here
	@bookStatus INT
AS

DECLARE @tempTb TABLE (AdDesignId INT, DocumentId NVARCHAR(100))

BEGIN
	INSERT INTO @tempTb
		SELECT			AdDesign.AdDesignId, 
						(SELECT TOP 1 DocumentId FROM AdGraphic WHERE AdGraphic.AdDesignId = AdDesign.AdDesignId)
		FROM			AdDesign 
		INNER JOIN		AdBooking bk ON bk.AdId = AdDesign.AdId
		WHERE			bk.BookingStatus = @bookStatus
						AND bk.EndDate > GETDATE()
						AND bk.StartDate < GETDATE() AND AdDesign.[Status] <> 3
		ORDER BY		bk.StartDate DESC
END

BEGIN
	SELECT DISTINCT TOP	9	ad.OnlineAdId, 
						LEFT(ad.Heading, 40) + '...' as Heading,
						LEFT(ad.[Description], 80) + '...' as [AdText],  
						gr.DocumentId, bk.StartDate, bk.AdBookingId, 
						(Select main.Title FROM MainCategory main WHERE main.MainCategoryId = mc.ParentId) AS Category,
						(Select main.MainCategoryId FROM MainCategory main WHERE main.MainCategoryId = mc.ParentId) AS CategoryId

	FROM		OnlineAd ad
	INNER JOIN	AdDesign ds ON ds.AdDesignId = ad.AdDesignId
	INNER JOIN	AdBooking bk ON	bk.AdId = ds.AdId
	INNER JOIN	MainCategory mc ON mc.MainCategoryId = bk.MainCategoryId
	LEFT JOIN	@tempTb gr ON gr.AdDesignId = ad.AdDesignId
	WHERE	bk.BookingStatus = @bookStatus
			AND bk.StartDate < GETDATE() 
			AND bk.EndDate > GETDATE()
			AND ds.[Status] <> 3
			AND gr.DocumentID IS NOT NULL
	ORDER BY bk.AdBookingId DESC
END	

