CREATE PROC [dbo].[psp_LogDelete] 
    @LogId uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Log]
	WHERE  [LogId] = @LogId

	COMMIT
