-- =============================================
-- Author:		Dejan Vasic
-- Create date: 27th March 2009
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spPublicationCategoriesByPubId]
	-- Add the parameters for the stored procedure here
	@PublicationID INT,
	@ParentID INT
AS

BEGIN
	IF @ParentID IS NULL 
		SELECT pc.PublicationCategoryId, pc.Title, pc.[Description], pc.ImageUrl, pc.ParentId, pc.MainCategoryId, pc.PublicationId 
		FROM PublicationCategory pc
		WHERE pc.PublicationId = @PublicationID AND pc.ParentId IS NULL
		ORDER BY pc.Title
	ELSE
		SELECT pc.PublicationCategoryId, pc.Title, pc.[Description], pc.ImageUrl, pc.ParentId, pc.MainCategoryId, pc.PublicationId 
		FROM PublicationCategory pc
		WHERE pc.PublicationId = @PublicationID AND pc.ParentId = @ParentID	
		ORDER BY pc.Title
END



