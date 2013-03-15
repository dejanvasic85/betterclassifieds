CREATE PROC [dbo].[bst_BroadcastDelete] 
    @BroadcastId uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Broadcast]
	WHERE  [BroadcastId] = @BroadcastId

	COMMIT
