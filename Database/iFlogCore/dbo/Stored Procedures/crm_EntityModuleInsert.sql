CREATE PROC [dbo].[crm_EntityModuleInsert] 
    @Active bit = 0,
    @EndDate datetime = null,
    @EntityCode nvarchar(10),
    @ModuleId int,
    @StartDate datetime = null
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[EntityModule] ([Active], [EndDate], [EntityCode], [ModuleId], [StartDate])
	SELECT @Active, @EndDate, @EntityCode, @ModuleId, @StartDate
	
	-- Begin Return Select <- do not remove
	SELECT [EntityModuleId], [Active], [EndDate], [EntityCode], [ModuleId], [StartDate]
	FROM   [dbo].[EntityModule]
	WHERE  [EntityModuleId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
