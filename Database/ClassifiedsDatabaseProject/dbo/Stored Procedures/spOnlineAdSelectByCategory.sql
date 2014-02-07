
-- =============================================
-- Author:		Dejan Vasic
-- Create date: 6th March 2009

-- Modifications
-- Date			Author			Description
--
-- 15-4-2009	Dejan Vasic		Added WHERE parameter for Ad Design Status not to include "cancelled = 3 "
-- ==========================================================================================
CREATE PROCEDURE [dbo].[spOnlineAdSelectByCategory]
	-- Add the parameters for the stored procedure here
	@CategoryId INT,
	--@startDate DATETIME,
	@bookStatus INT
AS

DECLARE @tempTb TABLE (AdDesignId INT, DocumentId NVARCHAR(100))

BEGIN
	-- INSERT INTO TABLE ONLY ONE IMAGE FOR EACH AD DESIGN
	INSERT INTO @tempTb
		SELECT			AdDesign.AdDesignId, 
						(SELECT TOP 1 DocumentId FROM AdGraphic WHERE AdGraphic.AdDesignId = AdDesign.AdDesignId)
		FROM			AdDesign 
		INNER JOIN		AdBooking bk ON bk.AdId = AdDesign.AdId
		WHERE			bk.BookingStatus = @bookStatus AND AdDesign.[Status] <> 3
						AND bk.StartDate < GETDATE() 
		ORDER BY		bk.StartDate DESC
END

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SELECT	ad.OnlineAdId, ad.Heading, 
			--ad.[Description] as AdText,
			ad.Price, ad.NumOfViews, bk.StartDate as Posted, bk.EndDate as Ending, ar.Title as Location, 
			LEFT(ad.[Description], 50) + '...' as [AdText], mc.Title as Category,
			gr.DocumentId AS DocumentId
	FROM OnlineAd ad
		INNER JOIN	AdDesign ds			ON	ds.AdDesignId = ad.AdDesignId
		INNER JOIN	AdBooking bk		ON	bk.AdId = ds.AdId
		INNER JOIN	LocationArea ar		ON	ar.LocationAreaId = ad.LocationAreaId
		INNER JOIN	MainCategory mc		ON	mc.MainCategoryId = bk.MainCategoryId
		LEFT JOIN	@tempTb gr		ON	gr.AdDesignId = ad.AdDesignId
	WHERE 
		bk.EndDate > GETDATE()
		AND bk.StartDate <= GETDATE()
		AND ds.[Status] <> 3
		AND bk.BookingStatus = @bookStatus
		AND bk.MainCategoryId IN (	SELECT ct.MainCategoryId 
									FROM MainCategory ct
									WHERE ct.ParentId = @CategoryId )
	ORDER BY bk.StartDate DESC
END

