CREATE PROC [dbo].[crm_EntityModuleDelete] 
    @EntityModuleId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[EntityModule]
	WHERE  [EntityModuleId] = @EntityModuleId

	COMMIT
