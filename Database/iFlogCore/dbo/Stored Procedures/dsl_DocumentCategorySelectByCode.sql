
CREATE PROC [dbo].[dsl_DocumentCategorySelectByCode] 
		@CategoryCode INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT 
		    [DocumentCategoryId]
		  , [Title]
		  , [Code]
		  , [ExpiryPurgeDays]
		  , [AcceptedFileTypes]
		  , [MaximumFileSize]
	FROM   [dbo].[DocumentCategory]
	WHERE  [Code] = @CategoryCode
	
	COMMIT