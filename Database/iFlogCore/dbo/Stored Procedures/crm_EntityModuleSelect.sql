CREATE PROC [dbo].[crm_EntityModuleSelect] 
    @EntityModuleId INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EntityModuleId], [Active], [EndDate], [EntityCode], [ModuleId], [StartDate] 
	FROM   [dbo].[EntityModule] 
	WHERE  ([EntityModuleId] = @EntityModuleId OR @EntityModuleId IS NULL) 

	COMMIT
