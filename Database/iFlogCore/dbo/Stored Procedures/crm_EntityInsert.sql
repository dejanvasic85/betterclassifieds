CREATE PROC [dbo].[crm_EntityInsert] 
    @EntityCode nvarchar(10) output,
    @Active bit = 0,
    @EntityName nvarchar(50),
    @PrimaryContactId int,
    @TimeZone int = 10
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	EXEC	[dbo].[crm_EntityGetNewId] @EntityCode = @EntityCode OUTPUT
	
	INSERT INTO [dbo].[Entity] ([EntityCode], [Active], [EntityName], [PrimaryContactId], [TimeZone])
	SELECT @EntityCode, @Active, @EntityName, @PrimaryContactId, @TimeZone
	
	-- Begin Return Select <- do not remove
	SELECT [EntityCode], [Active], [EntityName], [PrimaryContactId], [TimeZone]
	FROM   [dbo].[Entity]
	WHERE  [EntityCode] = @EntityCode
	-- End Return Select <- do not remove
               
	COMMIT
