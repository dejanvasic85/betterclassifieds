

ALTER PROCEDURE [dbo].[Editions_GetUpcomingForPublications]
     @publications		varchar(20),
	 @minEditionDate	datetime,
	 @minDeadlineDate	datetime

	 /*
		exec [Editions_GetUpcomingForPublications] 
			@publications = '1,2,3,4,5,6',
			@minEditionDate = '09-MAR-2015',
			@minDeadlineDate = '09-MAR-2015'
	 */
AS
BEGIN
	

	SELECT	DISTINCT EditionDate
	FROM	dbo.Edition e
	JOIN	SplitStringToInt(@publications, ',') p
		ON	p.Data = e.PublicationId
	WHERE	e.EditionDate >= @minEditionDate
	AND		e.Deadline > @minDeadlineDate
	ORDER BY e.EditionDate

END