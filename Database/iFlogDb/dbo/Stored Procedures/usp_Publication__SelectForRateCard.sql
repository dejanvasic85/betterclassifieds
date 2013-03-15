-- =============================================
-- Author:		Dejan Vasic
-- Create date: 03-JAN-2010
-- =============================================
CREATE PROCEDURE [dbo].[usp_Publication__SelectForRateCard]
(
	@RateCardId		INT
)
AS
/*
	exec usp_RateCard__SelectPublications
		@RateCardId	= 13;
		
*/
BEGIN

	SELECT	DISTINCT p.PublicationId
	FROM	Publication p
	JOIN	PublicationCategory pc
		ON	pc.PublicationId = p.PublicationId
	JOIN	PublicationRate pr
		ON	pr.PublicationCategoryId = pc.PublicationCategoryId
	WHERE	pr.RatecardId = @RateCardId;
END


