CREATE PROC [dbo].[bst_BroadcastUpdate] 
    @BroadcastId uniqueidentifier,
    @EntityCode nvarchar(10),
    @ApplicationName nvarchar(50) = '/Default',
    @Type nchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Broadcast]
	SET    [BroadcastId] = @BroadcastId, [EntityCode] = @EntityCode, [ApplicationName] = @ApplicationName, [Type] = @Type, [CreatedDateTime] = GETDATE()
	WHERE  [BroadcastId] = @BroadcastId
	
	-- Begin Return Select <- do not remove
	SELECT [BroadcastId], [EntityCode], [ApplicationName], [Type], [CreatedDateTime]
	FROM   [dbo].[Broadcast]
	WHERE  [BroadcastId] = @BroadcastId	
	-- End Return Select <- do not remove

	COMMIT TRAN
