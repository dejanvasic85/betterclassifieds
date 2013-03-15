CREATE PROC [dbo].[bst_EmailTrackerDelete] 
    @EmailTrackerId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[EmailTracker]
	WHERE  [EmailTrackerId] = @EmailTrackerId

	COMMIT
