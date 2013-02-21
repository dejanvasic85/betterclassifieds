-- =============================================
-- Author:		Dejan Vasic
-- Create date: 03-JAN-2010
-- =============================================
CREATE PROCEDURE [dbo].[usp_MainCategory__SelectForRateCard]
(
	@RateCardId		INT
)
AS
/*
	exec usp_RateCard__SelectMainCategories
		@RateCardId	= 15;
		
*/
BEGIN

	SELECT	DISTINCT pc.MainCategoryId
	FROM	PublicationCategory pc
	JOIN	PublicationRate pr
		ON	pr.PublicationCategoryId = pc.PublicationCategoryId
	WHERE	pr.RatecardId = @RateCardId;
END


