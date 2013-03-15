-- =============================================
-- Author:		Dejan Vasic
-- Create date: 9th April 2009
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spMainCategoriesUnassigned]
	-- Add the parameters for the stored procedure here
	@isParent BIT,
	@publicationCategoryId INT,
	@publicationId INT
AS

BEGIN
	
	IF @isParent = 1 
		SELECT MainCategoryId, Title FROM MainCategory
		WHERE	NOT EXISTS (	SELECT * FROM PublicationCategory 
								WHERE PublicationCategory.MainCategoryId = MainCategory.MainCategoryId
								AND PublicationCategory.PublicationId = @publicationId)
				AND (	(@isParent = 1 AND MainCategory.ParentId IS NULL) 
						OR 
						(@isParent = 0  AND MainCategory.ParentId = @publicationCategoryId))
	ELSE
		BEGIN
			DECLARE @tempTb TABLE (MainCategoryId INT, Title NVARCHAR(100))

			BEGIN
				INSERT INTO @tempTb 
					SELECT mc.MainCategoryId, mc.Title FROM MainCategory mc
					INNER JOIN PublicationCategory pc ON pc.MainCategoryId = mc.MainCategoryId
					WHERE pc.ParentId = @publicationCategoryId
			END

			BEGIN
				SELECT mc.MainCategoryId, mc.Title FROM MainCategory mc
				INNER JOIN PublicationCategory pc ON pc.MainCategoryId = mc.ParentId
				WHERE pc.PublicationCategoryId = @publicationCategoryId
					AND NOT EXISTS (SELECT * FROM @tempTb tb WHERE tb.MainCategoryId = mc.MainCategoryId)
			END
		END
END



