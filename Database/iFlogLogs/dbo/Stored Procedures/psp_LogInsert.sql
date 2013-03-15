CREATE PROC [dbo].[psp_LogInsert] 
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
    @Browser varchar(500) = null
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	declare @eventLogCategory varchar(20)

	set @eventLogCategory = 'EventLog';
	
	if(@DateTimeCreated is null)
	begin 
		select @DateTimeCreated =  GETDATE();
	end 
	
	IF (@DateTimeUtcCreated IS NULL)
	begin
		SET @DateTimeUtcCreated = GETUTCDATE();
	end
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Log] (
		  [LogId]
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
		, [Browser])
	SELECT 
		  @LogId
		, @AccountId
		, @Application
		, @Category
		, @ComputerName
		, @Data1
		, @Data2
		, @DateTimeCreated
		, @DateTimeUtcCreated
		, @Domain
		, @IPAddress
		, @SessionId
		, @TransactionName
		, @User
		, @Browser
	
	
	-- Begin Return Select <- do not remove
	SELECT	  [LogId]
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
			, [Browser]
	FROM   [dbo].[Log]
	WHERE  [LogId] = @LogId
	-- End Return Select <- do not remove
            
            
    -- Send email to Support if category is EventLog
    IF @Category = @eventLogCategory
    begin
    
	DECLARE @emailHTML  NVARCHAR(MAX) ;

	SET @emailHTML =
		N'<H1>Event Log</H1>' +
		N'<table border="1">' +
		N'<tr><th style="width:20%; text-align:left;">Log Entry ID</th><th style="text-align:left;">' + CAST((@LogId) AS VARCHAR(100)) + '</th></tr>' +
		N'<tr><td style="width:20%; text-align:left;">Application</th><td style="text-align:left;">' + @Application + '</td></tr>' +
		N'<tr><td style="width:20%; text-align:left;">User</th><td style="text-align:left;">' + @User + '</td></tr>' +
		N'<tr><td style="width:20%; text-align:left;">DateTimeCreated</th><td style="text-align:left;">' + CAST((@DateTimeCreated) AS VARCHAR(100)) + '</td></tr>' +
		N'<tr><td style="width:20%; text-align:left;">Computer Name</th><td style="text-align:left;">' + @ComputerName + '</td></tr>' +
		N'<tr><td style="width:20%; text-align:left;">IP Address</th><td style="text-align:left;">' + @IPAddress + '</td></tr>' +
		N'<tr><td style="width:20%; text-align:left;">Data 1</th><td style="text-align:left;">' + @Data1 + '</td></tr>' +
		N'</table>' ;
    
	--EXEC msdb.dbo.sp_send_dbmail
	--  @profile_name = 'ParamountMail',
	--  @recipients = 'support@paramountit.com.au',
	--  @body = @emailHTML,
	--  @subject = 'EventLog',
	--  @body_format = 'HTML';
    end
       
	COMMIT
