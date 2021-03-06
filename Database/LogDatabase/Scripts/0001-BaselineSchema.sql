SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ELMAH_GetErrorsXml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ELMAH_GetErrorsXml]
(
    @Application NVARCHAR(60),
    @PageIndex INT = 0,
    @PageSize INT = 15,
    @TotalCount INT OUTPUT
)
AS 

    SET NOCOUNT ON

    DECLARE @FirstTimeUTC DATETIME
    DECLARE @FirstSequence INT
    DECLARE @StartRow INT
    DECLARE @StartRowIndex INT

    SELECT 
        @TotalCount = COUNT(1) 
    FROM 
        [ELMAH_Error]
    WHERE 
        [Application] = @Application

    -- Get the ID of the first error for the requested page

    SET @StartRowIndex = @PageIndex * @PageSize + 1

    IF @StartRowIndex <= @TotalCount
    BEGIN

        SET ROWCOUNT @StartRowIndex

        SELECT  
            @FirstTimeUTC = [TimeUtc],
            @FirstSequence = [Sequence]
        FROM 
            [ELMAH_Error]
        WHERE   
            [Application] = @Application
        ORDER BY 
            [TimeUtc] DESC, 
            [Sequence] DESC

    END
    ELSE
    BEGIN

        SET @PageSize = 0

    END

    -- Now set the row count to the requested page size and get
    -- all records below it for the pertaining application.

    SET ROWCOUNT @PageSize

    SELECT 
        errorId     = [ErrorId], 
        application = [Application],
        host        = [Host], 
        type        = [Type],
        source      = [Source],
        message     = [Message],
        [user]      = [User],
        statusCode  = [StatusCode], 
        time        = CONVERT(VARCHAR(50), [TimeUtc], 126) + ''Z''
    FROM 
        [ELMAH_Error] error
    WHERE
        [Application] = @Application
    AND
        [TimeUtc] <= @FirstTimeUTC
    AND 
        [Sequence] <= @FirstSequence
    ORDER BY
        [TimeUtc] DESC, 
        [Sequence] DESC
    FOR
        XML AUTO' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ELMAH_GetErrorXml]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ELMAH_GetErrorXml]
(
    @Application NVARCHAR(60),
    @ErrorId UNIQUEIDENTIFIER
)
AS

    SET NOCOUNT ON

    SELECT 
        [AllXml]
    FROM 
        [ELMAH_Error]
    WHERE
        [ErrorId] = @ErrorId
    AND
        [Application] = @Application' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ELMAH_LogError]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[ELMAH_LogError]
(
    @ErrorId UNIQUEIDENTIFIER,
    @Application NVARCHAR(60),
    @Host NVARCHAR(30),
    @Type NVARCHAR(100),
    @Source NVARCHAR(60),
    @Message NVARCHAR(500),
    @User NVARCHAR(50),
    @AllXml NVARCHAR(MAX),
    @StatusCode INT,
    @TimeUtc DATETIME
)
AS

    SET NOCOUNT ON

    INSERT
    INTO
        [ELMAH_Error]
        (
            [ErrorId],
            [Application],
            [Host],
            [Type],
            [Source],
            [Message],
            [User],
            [AllXml],
            [StatusCode],
            [TimeUtc]
        )
    VALUES
        (
            @ErrorId,
            @Application,
            @Host,
            @Type,
            @Source,
            @Message,
            @User,
            @AllXml,
            @StatusCode,
            @TimeUtc
        )' 
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
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ELMAH_Error]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ELMAH_Error](
	[ErrorId] [uniqueidentifier] NOT NULL,
	[Application] [nvarchar](60) NOT NULL,
	[Host] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Source] [nvarchar](60) NOT NULL,
	[Message] [nvarchar](500) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[TimeUtc] [datetime] NOT NULL,
	[Sequence] [int] IDENTITY(1,1) NOT NULL,
	[AllXml] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ELMAH_Error] PRIMARY KEY NONCLUSTERED 
(
	[ErrorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Log]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Log](
	[LogId] [uniqueidentifier] NOT NULL,
	[Category] [varchar](25) NOT NULL,
	[TransactionName] [varchar](100) NULL,
	[Application] [varchar](50) NOT NULL,
	[Domain] [varchar](50) NULL,
	[User] [varchar](50) NULL,
	[AccountId] [varchar](50) NULL,
	[Data1] [varchar](max) NULL,
	[Data2] [varchar](max) NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[ComputerName] [varchar](50) NULL,
	[IPAddress] [varchar](25) NULL,
	[SessionId] [varchar](20) NULL,
	[DateTimeUtcCreated] [datetime] NULL,
	[Browser] [varchar](500) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SchemaVersions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SchemaVersions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScriptName] [nvarchar](255) NOT NULL,
	[Applied] [datetime] NOT NULL,
 CONSTRAINT [PK_SchemaVersions_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_ELMAH_Error_ErrorId]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ELMAH_Error] ADD  CONSTRAINT [DF_ELMAH_Error_ErrorId]  DEFAULT (newid()) FOR [ErrorId]
END

GO
