CREATE PROC [dbo].[crm_EntityDelete] 
    @EntityCode nvarchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Entity]
	WHERE  [EntityCode] = @EntityCode

	COMMIT
