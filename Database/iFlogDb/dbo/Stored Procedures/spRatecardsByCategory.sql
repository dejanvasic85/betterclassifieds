-- =============================================
-- Author:		Dejan Vasic
-- Create date: 16th March 2009
-- Modifications
-- Date			Author			Description
--
-- 18-3-2009	Dejan Vasic		Added Where clause to include pub.Active (only active papers)
-- 9-12-2009	Dejan Vasic		Added order by sort order on publications
-- =============================================
CREATE PROCEDURE [dbo].[spRatecardsByCategory]
	-- Add the parameters for the stored procedure here
	@categoryId INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT	pub.Title as Publication, br.Title as Rate, br.[Description],
		ra.MinCharge, ra.MaxCharge, ra.RatePerMeasureUnit, ra.MeasureUnitLimit, ra.PhotoCharge, ra.BoldHeading, ra.OnlineEditionBundle, pubType.Code
	FROM Ratecard ra
	INNER JOIN PublicationRate pubRate ON pubRate.RatecardId = ra.RatecardId
	INNER JOIN PublicationCategory pubCat ON pubCat.PublicationCategoryId = pubRate.PublicationCategoryId
	INNER JOIN PublicationAdType pubAdType ON pubAdType.PublicationAdTypeId = pubRate.PublicationAdTypeId
	INNER JOIN Publication pub ON pub.PublicationId = pubAdType.PublicationId
	INNER JOIN PublicationType pubType ON pubType.PublicationTypeId = pub.PublicationTypeId
	INNER JOIN BaseRate br ON br.BaseRateId = ra.BaseRateId
	WHERE	pubCat.MainCategoryId = @categoryId 
			AND pub.Active = 1
	ORDER BY pub.SortOrder
END


