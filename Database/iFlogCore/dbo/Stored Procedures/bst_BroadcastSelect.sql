CREATE PROC [dbo].[bst_BroadcastSelect] 
    @BroadcastId UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [BroadcastId], [EntityCode], [ApplicationName], [Type], [CreatedDateTime] 
	FROM   [dbo].[Broadcast] 
	WHERE  ([BroadcastId] = @BroadcastId OR @BroadcastId IS NULL) 

	COMMIT
