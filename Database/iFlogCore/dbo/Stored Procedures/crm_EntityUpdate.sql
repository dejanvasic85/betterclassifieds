CREATE PROC [dbo].[crm_EntityUpdate] 
    @EntityCode nvarchar(10),
    @Active bit = 0,
    @EntityName nvarchar(50),
    @PrimaryContactId int,
    @TimeZone int = 10 
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Entity]
	SET    [EntityCode] = @EntityCode, [Active] = @Active, [EntityName] = @EntityName, [PrimaryContactId] = @PrimaryContactId, [TimeZone] = @TimeZone
	WHERE  [EntityCode] = @EntityCode
	
	-- Begin Return Select <- do not remove
	SELECT [EntityCode], [Active], [EntityName], [PrimaryContactId], [TimeZone]
	FROM   [dbo].[Entity]
	WHERE  [EntityCode] = @EntityCode	
	-- End Return Select <- do not remove

	COMMIT TRAN
