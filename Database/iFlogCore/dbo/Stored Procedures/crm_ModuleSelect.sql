CREATE PROC [dbo].[crm_ModuleSelect] 
    @ModuleId INT = null
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ModuleId], [Description], [Title] 
	FROM   [dbo].[Module] 
	WHERE  ([ModuleId] = @ModuleId OR @ModuleId IS NULL) 

	COMMIT
