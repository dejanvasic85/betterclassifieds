-- =============================================
-- Author:		Dejan Vasic
-- Create date: 13th Jan 2010
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spPublicationDeadlineSelect]
	-- Add the parameters for the stored procedure here
	@PublicationID	INT
AS

BEGIN
	SELECT TOP 1 ed.PublicationId
	, ed.EditionId
	, ed.EditionDate
	, ed.Deadline
	, pb.Title as Publication
	FROM Edition ed
	INNER JOIN Publication pb ON pb.PublicationId = ed.PublicationId
	WHERE 
		ed.PublicationId = @PublicationID
		AND ed.Active = 1
		AND ed.EditionDate >= GETDATE()
		AND ed.Deadline >= GETDATE()
	ORDER BY ed.EditionDate
END



