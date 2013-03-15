-- =============================================
-- Author:		Dejan Vasic
-- Create date: 16th March 2009
-- Modifications
-- Date			Author			Description
--
-- 18-3-2009	Dejan Vasic		Added Where clause to include pub.Active (only active papers)
-- =============================================
CREATE PROCEDURE [dbo].[spLineAdById]
	-- Add the parameters for the stored procedure here
	@LineAdId INT
AS

BEGIN
	SELECT	la.LineAdId, la.AdHeader, la.AdText, la.NumOfWords, la.UsePhoto, la.UseBoldHeader, gr.DocumentId
	FROM LineAd la
	LEFT JOIN AdGraphic gr ON gr.AdDesignId = la.AdDesignId
	WHERE la.LineAdId = @LineAdId
END


