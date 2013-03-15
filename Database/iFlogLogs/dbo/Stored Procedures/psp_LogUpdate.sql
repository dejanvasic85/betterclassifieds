CREATE PROC [dbo].[psp_LogUpdate] 
    @LogId uniqueidentifier,
    @AccountId varchar(50) = null,
    @Application varchar(50) ='/',
    @Category varchar(25),
    @ComputerName varchar(50) = null,
    @Data1 varchar(MAX)= null,
    @Data2 varchar(MAX)=null,
    @DateTimeCreated datetime = null,
    @DateTimeUtcCreated datetime = null,
    @Domain varchar(50) = 'none',
    @TransactionName varchar(100) =null,
    @IPAddress varchar(25) = null,
    @User varchar(50) = null,  
    @SessionId varchar(20) = null,
    @Browser  varchar(500) = null
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	if(@DateTimeCreated is null)
	begin 
		select @DateTimeCreated =  GETDATE()
	end 
	BEGIN TRAN

	UPDATE [dbo].[Log]
	SET    [LogId] = @LogId
		, [AccountId] = @AccountId
		, [Application] = @Application
		, [Category] = @Category
		, [ComputerName] = @ComputerName
		, [Data1] = @Data1
		, [Data2] = @Data2
		, [DateTimeCreated] = @DateTimeCreated
		, [DateTimeUtcCreated] = @DateTimeUtcCreated
		, [Domain] = @Domain
		, [IPAddress] = @IPAddress
		, [SessionId] = @SessionId
		, [TransactionName] = @TransactionName
		, [User] = @User
		, [Browser] = @Browser
	WHERE  [LogId] = @LogId;
	
	-- Begin Return Select <- do not remove
	SELECT    [LogId]
			, [AccountId]
			, [Application]
			, [Category]
			, [ComputerName]
			, [Data1]
			, [Data2]
			, [DateTimeCreated]
			, [Domain]
			, [IPAddress]
			, [SessionId]
			, [TransactionName]
			, [User]
	FROM   [dbo].[Log]
	WHERE  [LogId] = @LogId	
	-- End Return Select <- do not remove

	COMMIT TRAN
