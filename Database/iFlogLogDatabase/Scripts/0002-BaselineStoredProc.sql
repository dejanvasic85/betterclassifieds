
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[psp_LogUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[psp_LogUpdate] 
    @LogId uniqueidentifier,
    @AccountId varchar(50) = null,
    @Application varchar(50) =''/'',
    @Category varchar(25),
    @ComputerName varchar(50) = null,
    @Data1 varchar(MAX)= null,
    @Data2 varchar(MAX)=null,
    @DateTimeCreated datetime = null,
    @DateTimeUtcCreated datetime = null,
    @Domain varchar(50) = ''none'',
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[psp_LogSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[psp_LogSelect] 
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[psp_LogInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[psp_LogInsert] 
    @LogId uniqueidentifier,
    @AccountId varchar(50) = null,
    @Application varchar(50) =''/'',
    @Category varchar(25),
    @ComputerName varchar(50) = null,
    @Data1 varchar(MAX)= null,
    @Data2 varchar(MAX)=null,
    @DateTimeCreated datetime = null,
    @DateTimeUtcCreated datetime = null,
    @Domain varchar(50) = ''none'',
    @TransactionName varchar(100) =null,
    @IPAddress varchar(25) = null,
    @User varchar(50) = null,
    @SessionId varchar(20) = null,
    @Browser varchar(500) = null
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	declare @eventLogCategory varchar(20)

	set @eventLogCategory = ''EventLog'';
	
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
		N''<H1>Event Log</H1>'' +
		N''<table border="1">'' +
		N''<tr><th style="width:20%; text-align:left;">Log Entry ID</th><th style="text-align:left;">'' + CAST((@LogId) AS VARCHAR(100)) + ''</th></tr>'' +
		N''<tr><td style="width:20%; text-align:left;">Application</th><td style="text-align:left;">'' + @Application + ''</td></tr>'' +
		N''<tr><td style="width:20%; text-align:left;">User</th><td style="text-align:left;">'' + @User + ''</td></tr>'' +
		N''<tr><td style="width:20%; text-align:left;">DateTimeCreated</th><td style="text-align:left;">'' + CAST((@DateTimeCreated) AS VARCHAR(100)) + ''</td></tr>'' +
		N''<tr><td style="width:20%; text-align:left;">Computer Name</th><td style="text-align:left;">'' + @ComputerName + ''</td></tr>'' +
		N''<tr><td style="width:20%; text-align:left;">IP Address</th><td style="text-align:left;">'' + @IPAddress + ''</td></tr>'' +
		N''<tr><td style="width:20%; text-align:left;">Data 1</th><td style="text-align:left;">'' + @Data1 + ''</td></tr>'' +
		N''</table>'' ;
    
	--EXEC msdb.dbo.sp_send_dbmail
	--  @profile_name = ''ParamountMail'',
	--  @recipients = ''support@paramountit.com.au'',
	--  @body = @emailHTML,
	--  @subject = ''EventLog'',
	--  @body_format = ''HTML'';
    end
       
	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[psp_LogDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[psp_LogDelete] 
    @LogId uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Log]
	WHERE  [LogId] = @LogId

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[psp_ActivitySummary]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[psp_ActivitySummary] 
	@ReportDate DATETIME
AS
BEGIN

DECLARE @endDate	DATETIME;

SET @endDate = DATEADD(MINUTE, 1439, @ReportDate)

SELECT TOP 10 COUNT (1) Occurrances, [Application], Data1 AS ErrorMessage
FROM [dbo].[Log]
WHERE DateTimeCreated BETWEEN @ReportDate AND @endDate
AND Category = ''EventLog''
GROUP BY [Application], Data1
ORDER BY COUNT(1) DESC

END
' 
END
GO
