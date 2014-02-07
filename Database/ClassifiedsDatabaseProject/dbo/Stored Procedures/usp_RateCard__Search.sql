-- =============================================
-- Author:		Dejan Vasic
-- Create date: 01-JAN-2010
-- =============================================
CREATE PROCEDURE [dbo].[usp_RateCard__Search]
AS
/*
	exec usp_RateCard__Search
*/
BEGIN

	declare @publicationRateCount	table (	PublicationId	INT,
											RatecardId		INT);
											
	-- Publication Count
	INSERT INTO @publicationRateCount
	select	p.PublicationId, pr.RatecardId
	from	Publication p
	JOIN	PublicationCategory pc
		ON	pc.PublicationId = p.PublicationId
	JOIN	PublicationRate pr
		ON	pr.PublicationCategoryId = pc.PublicationCategoryId
	GROUP BY	p.PublicationId, pr.RatecardId
	
	-- MAIN RESULTS
	SELECT	  r.RatecardId
			, br.Title
			, r.MinCharge
			, r.MaxCharge
			, r.CreatedDate
			, r.CreatedByUser
			, COUNT(prc.PublicationId) as PublicationCount
	FROM	Ratecard r
	JOIN	BaseRate br
		ON	br.BaseRateId = r.BaseRateId
	LEFT JOIN	@publicationRateCount prc
		ON	prc.RatecardId = r.RatecardId
	GROUP BY
			 r.RatecardId
			, br.Title
			, r.MinCharge
			, r.MaxCharge
			, r.CreatedDate
			, r.CreatedByUser
END


