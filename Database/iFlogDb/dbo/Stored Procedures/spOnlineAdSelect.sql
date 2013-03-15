
-- ==========================================================================================
-- Author:		Dejan Vasic
-- Create date: 6th March 2009

-- Modifications
-- Date			Author			Description
--
-- 11-3-2009	Dejan Vasic		Added parameters @ParentCategoryId, @SubCategoryId, @AreaId,
--								@Keyword. Also created the categories variable table and
--								adjusted the WHERE clause to perform appropriate search.
-- 15-4-2009	Dejan Vasic		Added WHERE parameter for Ad Design Status not to include "cancelled = 3 "
-- ==========================================================================================
CREATE PROCEDURE [dbo].[spOnlineAdSelect]
	-- Add the parameters for the stored procedure here
	@ParentCategoryId INT,
	@SubCategoryId INT,
	@LocationId INT,
	@AreaId INT,
	@KeyWord NVARCHAR(100),
	--@StartDate DATETIME,
	@BookStatus INT
AS

-- ** IMAGES VAR TABLE ** --
DECLARE @tempTb TABLE (AdDesignId INT, DocumentId NVARCHAR(100))

BEGIN
	INSERT INTO @tempTb
		SELECT			AdDesign.AdDesignId, 
						(SELECT TOP 1 DocumentId FROM AdGraphic WHERE AdGraphic.AdDesignId = AdDesign.AdDesignId)
		FROM			AdDesign 
		INNER JOIN		AdBooking bk ON bk.AdId = AdDesign.AdId
		WHERE			bk.BookingStatus = @BookStatus
						AND bk.StartDate < GETDATE() AND bk.StartDate < GETDATE() AND AdDesign.[Status] <> 3
		ORDER BY		bk.StartDate DESC
END
-- ** END IMAGES VAR TABLE ** --

-- ** CATEGORIES VAR TABLE ** --
DECLARE @tmpCat TABLE (MainCategoryId INT, ParentId INT)

BEGIN
	IF @SubCategoryId IS NULL 
		IF @ParentCategoryId IS NULL
			INSERT INTO @tmpCat SELECT MainCategoryId, ParentId FROM Maincategory
		ELSE
			INSERT INTO @tmpCat SELECT MainCategoryId, ParentId FROM MainCategory WHERE MainCategory.ParentId = @ParentCategoryId
	ELSE
		INSERT INTO @tmpCat
		SELECT		MainCategoryId, ParentId FROM MainCategory WHERE MainCategory.MainCategoryId = @SubCategoryId
END
-- ** END CATEGORIES VAR TABLE ** --

-- ** SEARCHING ROUTINE ** --
BEGIN
	SELECT	ad.OnlineAdId, ad.Heading, 
			ad.Price, ad.NumOfViews, bk.StartDate as Posted, bk.EndDate as Ending, ar.Title as Location, 
			LEFT(ad.[Description], 50) + '...' as [AdText], mc.Title As Category, 
			gr.DocumentId AS DocumentId
	FROM OnlineAd ad
		INNER JOIN	AdDesign ds			ON	ds.AdDesignId = ad.AdDesignId
		INNER JOIN	AdBooking bk		ON	bk.AdId = ds.AdId
		INNER JOIN	LocationArea ar		ON	ar.LocationAreaId = ad.LocationAreaId
		INNER JOIN	MainCategory mc		ON	mc.MainCategoryId = bk.MainCategoryId
		LEFT JOIN	@tempTb gr			ON	gr.AdDesignId = ad.AdDesignId
	WHERE 
		(	((@KeyWord = '' AND 1 = 1) OR (@KeyWord <> '' AND ad.[Description] LIKE '% ' + @KeyWord + '%' )) 
		OR 
			((@KeyWord = '' AND 1 = 1) OR (@KeyWord <> '' AND ad.Heading LIKE '% ' + @KeyWord + '%'))	) 
		AND 
		(	((@LocationId IS NULL AND 1=1) OR (@LocationId IS NOT NULL AND ad.LocationId = @LocationId))	)
		AND
		(	((@AreaId IS NULL AND 1=1) OR (@AreaId IS NOT NULL AND ad.LocationAreaId = @AreaId))	)
		AND bk.MainCategoryId IN ( SELECT ct.MainCategoryId FROM @tmpCat ct )
		AND bk.EndDate > GETDATE() AND bk.StartDate <= GETDATE()
		AND bk.BookingStatus = @BookStatus AND ds.[Status] <> 3
		
	ORDER BY bk.StartDate DESC
END
-- ** END SEARCHING ROUTINE ** --


