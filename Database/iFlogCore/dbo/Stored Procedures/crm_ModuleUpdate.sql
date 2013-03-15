CREATE PROC [dbo].[crm_ModuleUpdate] 
    @ModuleId int,
    @Description nvarchar(MAX) = null,
    @Title nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Module]
	SET    [Description] = @Description, [Title] = @Title
	WHERE  [ModuleId] = @ModuleId
	
	-- Begin Return Select <- do not remove
	SELECT [ModuleId], [Description], [Title]
	FROM   [dbo].[Module]
	WHERE  [ModuleId] = @ModuleId	
	-- End Return Select <- do not remove

	COMMIT TRAN
