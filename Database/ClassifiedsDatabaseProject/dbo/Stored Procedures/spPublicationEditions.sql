CREATE PROCEDURE [dbo].[spPublicationEditions]
	@StartDate DATETIME,
	@PublicationId INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT  e.EditionDate
	FROM	dbo.Edition e
	WHERE	e.PublicationId = @PublicationId
		AND	e.EditionDate >= @StartDate
		AND e.EditionDate NOT IN (
				SELECT	npd.EditionDate 
				FROM	dbo.NonPublicationDate npd 
				WHERE	npd.EditionDate = e.EditionDate 
					AND	npd.PublicationId = e.PublicationId)
END
