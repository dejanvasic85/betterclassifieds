-- =============================================
-- Author:		Dejan Vasic
-- Create date: 28th January 2009
-- Modifications
-- Date			Author			Description
-- 18-03-2009	Dejan Vasic		Added Where clause to include pub.Active (only active papers)
-- 29-11-2009	Dejan Vasic		Added Sort order logic
-- =============================================
CREATE PROCEDURE [dbo].[spSpecialRatePublications]
	-- Add the parameters for the stored procedure here
	@specialRateId INT
AS
BEGIN
	SELECT DISTINCT p.Title as Publication, at.Code as AdType, p.PublicationId, p.ImageUrl, p.SortOrder
	FROM SpecialRate sr
	INNER JOIN PublicationSpecialRate psr ON psr.SpecialRateId = sr.SpecialRateId
	INNER JOIN PublicationAdType pat ON pat.PublicationAdTypeId = psr.PublicationAdTypeId
	INNER JOIN Publication p ON p.PublicationId = pat.PublicationId
	INNER JOIN AdType at ON at.AdTypeId = pat.AdTypeId
	WHERE	sr.SpecialRateId = @specialRateId
			AND p.Active = 1
	ORDER BY p.SortOrder
END


