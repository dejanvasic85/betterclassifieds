-- =============================================
-- Author:		Dejan Vasic
-- Create date: 27th March 2009
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spPublicationCategories]
	-- Add the parameters for the stored procedure here
	@PublicationID INT,
	@ParentID INT,
	@MainCategoryID INT
AS

BEGIN
	SELECT pc.PublicationCategoryId, pc.Title, pc.[Description], pc.ImageUrl, pc.ParentId, pc.MainCategoryId, pc.PublicationId 
	FROM PublicationCategory pc
	WHERE 
			pc.PublicationId = @PublicationID
			AND pc.MaincategoryId = @MainCategoryID
			AND ((@ParentID IS NULL AND 1=1) OR (@ParentID IS NOT NULL AND pc.ParentId = @ParentID))
END



