CREATE PROC [dbo].[crm_EntityModuleUpdate] 
    @EntityModuleId int,
    @Active bit,
    @EndDate datetime,
    @EntityCode nvarchar(10),
    @ModuleId int,
    @StartDate datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[EntityModule]
	SET    [Active] = @Active, [EndDate] = @EndDate, [EntityCode] = @EntityCode, [ModuleId] = @ModuleId, [StartDate] = @StartDate
	WHERE  [EntityModuleId] = @EntityModuleId
	
	-- Begin Return Select <- do not remove
	SELECT [EntityModuleId], [Active], [EndDate], [EntityCode], [ModuleId], [StartDate]
	FROM   [dbo].[EntityModule]
	WHERE  [EntityModuleId] = @EntityModuleId	
	-- End Return Select <- do not remove

	COMMIT TRAN
