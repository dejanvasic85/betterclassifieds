CREATE PROC [dbo].[crm_ModuleInsert] 
    @Description nvarchar(MAX) = null,
    @Title nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Module] ([Description], [Title])
	SELECT @Description, @Title
	
	-- Begin Return Select <- do not remove
	SELECT [ModuleId], [Description], [Title]
	FROM   [dbo].[Module]
	WHERE  [ModuleId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
