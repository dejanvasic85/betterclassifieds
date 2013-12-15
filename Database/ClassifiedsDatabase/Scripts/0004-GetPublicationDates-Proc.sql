GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spPublicationEditions]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[spPublicationEditions]
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
' 
END