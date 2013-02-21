-- =============================================
-- Author:		Dejan Vasic
-- Create date: 26-1-2009
-- Modifications
-- Date			Author			Description
--
-- 18-3-2009	Dejan Vasic		Added Where clause to include pub.Active (only active papers)
-- =============================================
CREATE PROCEDURE [dbo].[spSpecialRatesByCategory]
	-- Add the parameters for the stored procedure here
	@mainCategoryId INT
AS
BEGIN
	SELECT DISTINCT BaseRate.Title, BaseRate.Description, SpecialRate.SpecialRateId, SpecialRate.NumOfInsertions,
		SpecialRate.MaximumWords, SpecialRate.SetPrice, SpecialRate.Discount, SpecialRate.NumOfAds,
		mc.MainCategoryId
	FROM SpecialRate 
	INNER JOIN BaseRate ON BaseRate.BaseRateId = SpecialRate.BaseRateId
	INNER JOIN PublicationSpecialRate spr ON spr.SpecialRateId = SpecialRate.SpecialRateId
	INNER JOIN PublicationCategory pc ON pc.PublicationCategoryId = spr.PublicationCategoryId
	INNER JOIN MainCategory mc ON mc.MainCategoryId = pc.MainCategoryId
	INNER JOIN Publication pub ON pub.PublicationId = pc.PublicationId
	WHERE	mc.MainCategoryId = @mainCategoryId
			AND pub.Active = 1
	ORDER BY BaseRate.Title
END


