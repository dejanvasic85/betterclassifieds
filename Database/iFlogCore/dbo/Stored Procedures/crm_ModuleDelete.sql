CREATE PROC [dbo].[crm_ModuleDelete] 
    @ModuleId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Module]
	WHERE  [ModuleId] = @ModuleId

	COMMIT
