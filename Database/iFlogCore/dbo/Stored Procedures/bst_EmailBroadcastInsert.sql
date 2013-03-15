CREATE PROC [dbo].[bst_EmailBroadcastInsert] 
    @TemplateName nvarchar(100),
    @BroadcastId uniqueidentifier,
    @EntityCode nvarchar(10),
    @ApplicationName nvarchar(50) = '/Default',
    @Type nchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Broadcast] ([BroadcastId], [EntityCode], [ApplicationName], [Type], [CreatedDateTime])
	SELECT @BroadcastId, @EntityCode, @ApplicationName, @Type, getdate()
	
	INSERT INTO [dbo].[EmailBroadcast] ([TemplateName], [BroadcastId])
	SELECT @TemplateName, @BroadcastId
	
	-- Begin Return Select <- do not remove
	SELECT [EmailBroadcastId], [TemplateName], [BroadcastId]
	FROM   [dbo].[EmailBroadcast]
	WHERE  [EmailBroadcastId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
