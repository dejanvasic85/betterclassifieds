-- =============================================
-- Author:		Dejan Vasic
-- Create date: 16th March 2009
-- Modifications
-- Date			Author			Description
--
-- 18-3-2009	Dejan Vasic		Added Where clause to include pub.Active (only active papers)
-- 29-11-2009	Dejan Vasic		Added Sort order logic
-- =============================================
CREATE PROCEDURE [dbo].[spPublicationEditionAndDeadlines]
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT pub.PublicationId, pub.Title, pub.[Description], pub.ImageUrl, 
		(	SELECT TOP 1 ed.EditionDate 
			FROM Edition ed 
			WHERE ed.PublicationId = pub.PublicationId AND ed.EditionDate >= GETDATE()
			ORDER BY ed.EditionDate) As NextEdition,
			
		(	SELECT TOP 1 ed.Deadline 
			FROM Edition ed 
			WHERE ed.PublicationId = pub.PublicationId AND ed.EditionDate >= GETDATE()
			ORDER BY ed.EditionDate) As NextDeadline
			
	FROM	Publication pub
	INNER JOIN	PublicationType typ ON typ.PublicationTypeId = pub.PublicationTypeId
	WHERE	typ.Code <> 'ONLINE' 
			AND pub.Active = 1
	ORDER BY pub.SortOrder
END

