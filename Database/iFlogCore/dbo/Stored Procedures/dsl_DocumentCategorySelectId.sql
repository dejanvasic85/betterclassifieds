CREATE PROCEDURE [dbo].[dsl_DocumentCategorySelectId]
	@CategoryCode INT,
	@DocumentCategoryId INT OUTPUT	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SElECT @DocumentCategoryId = [DocumentCategoryId] 
	FROM DocumentCategory
	WHERE [Code] = @CategoryCode
    
END
