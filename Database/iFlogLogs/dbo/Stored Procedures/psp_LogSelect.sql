CREATE PROC [dbo].[psp_LogSelect] 
    @LogId UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [LogId]
	, [AccountId]
	, [Application]
	, [Category]
	, [ComputerName]
	, [Data1]
	, [Data2]
	, [DateTimeCreated]
	, [DateTimeUtcCreated]
	, [Domain]
	, [IPAddress]
	, [SessionId]
	, [TransactionName]
	, [User] 
	, [Browser]
	FROM   [dbo].[Log] 
	WHERE  ([LogId] = @LogId OR @LogId IS NULL) 

	COMMIT
