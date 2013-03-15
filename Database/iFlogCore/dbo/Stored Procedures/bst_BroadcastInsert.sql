CREATE PROC [dbo].[bst_BroadcastInsert] 
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
	
	-- Begin Return Select <- do not remove
	SELECT [BroadcastId], [EntityCode], [ApplicationName], [Type], [CreatedDateTime]
	FROM   [dbo].[Broadcast]
	WHERE  [BroadcastId] = @BroadcastId
	-- End Return Select <- do not remove
               
	COMMIT
