SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[brn_GetNextBanner]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE proc [dbo].[brn_GetNextBanner]

@ClientCode varchar(10),
@BannerGroupId uniqueidentifier,
@Params NameValueCollectionType  READONLY



as



select top 1 b.* 
from  
Banner b
--inner join dbo.BannerReference br on b.BannerId = br.BannerId
--inner join dbo.BannerReferenceType brt on br.BannerReferenceTypeId = brt.BannerReferenceTypeId
--inner join @Params p on brt.Title = p.Name and br.Value = p.Value
where b.BannerGroupId = @BannerGroupId 
	 -- and getdate() between b.StartDateTime and b.EndDateTime
	  --and b.IsDeleted = 0
	  --and b.ClientCode = @ClientCode
	  order by b.RequestCount asc
	  


' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_Broadcast_Activity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[bst_Broadcast_Activity]
	@ReportDate DATETIME
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @endDate	DATETIME;
	SET @endDate = DATEADD(MINUTE, 1439, @ReportDate)

	SELECT	COUNT(1) AS NumberOfEmailsSent
	FROM	dbo.Broadcast
	WHERE CreatedDateTime BETWEEN @ReportDate AND @endDate
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_BroadcastDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_BroadcastDelete] 
    @BroadcastId uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Broadcast]
	WHERE  [BroadcastId] = @BroadcastId

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_BroadcastInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_BroadcastInsert] 
    @BroadcastId uniqueidentifier,
    @EntityCode nvarchar(10),
    @ApplicationName nvarchar(50) = ''/Default'',
    @Type nchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Broadcast] ([BroadcastId], [EntityCode], [ApplicationName], [Type], [CreatedDateTime])
	SELECT @BroadcastId, @EntityCode, @ApplicationName, @Type, getdate()
	
	-- Begin Return Select <- do not remove
	SELECT [BroadcastId], [EntityCode], [ApplicationName], [Type], [CreatedDateTime]
	FROM   [dbo].[Broadcast]
	WHERE  [BroadcastId] = @BroadcastId
	-- End Return Select <- do not remove
               
	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_BroadcastSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_BroadcastSelect] 
    @BroadcastId UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [BroadcastId], [EntityCode], [ApplicationName], [Type], [CreatedDateTime] 
	FROM   [dbo].[Broadcast] 
	WHERE  ([BroadcastId] = @BroadcastId OR @BroadcastId IS NULL) 

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_BroadcastUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_BroadcastUpdate] 
    @BroadcastId uniqueidentifier,
    @EntityCode nvarchar(10),
    @ApplicationName nvarchar(50) = ''/Default'',
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
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailBroadcastEntryDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create proc [dbo].[bst_EmailBroadcastEntryDelete] 
    @EmailBroadcastEntryId int
as 
	set nocount on 
	set xact_abort on  
	
	begin tran

	delete
	from   [dbo].[EmailBroadcastEntry]
	where  [EmailBroadcastEntryId] = @EmailBroadcastEntryId

	commit
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailBroadcastEntryInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE proc [dbo].[bst_EmailBroadcastEntryInsert] 
	@BroadcastId uniqueidentifier,
    @Email nvarchar(100),
    @EmailContent nvarchar(max),
    @LastRetryDateTime datetime = null,
    @SentDateTime datetime = null,
    @RetryNo int = 0,
    @Subject varchar(50),
    @Sender nvarchar(50),
    @IsBodyHtml bit = ''true'' ,
    @Priority int =0
as 
	set nocount on 
	set xact_abort on  
	
	begin tran
	
	insert into [dbo].[EmailBroadcastEntry] ([Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority], [BroadcastId])
	select @Email, @EmailContent, @LastRetryDateTime, @SentDateTime, @RetryNo, @Subject, @Sender, @IsBodyHtml, @Priority, @BroadcastId
	
	-- Begin Return Select <- do not remove
	select [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority], [BroadcastId]
	from   [dbo].[EmailBroadcastEntry]
	where  [EmailBroadcastEntryId] = scope_identity()
	-- End Return Select <- do not remove
               
	commit
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailBroadcastEntryProcess]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE proc [dbo].[bst_EmailBroadcastEntryProcess] 
    @EmailBroadcastEntryId int,
    @SentDateTime datetime = null,
    @RetryNo int = 0
    
as 
	set nocount on 
	set xact_abort on  
	
	begin tran

	update [dbo].[EmailBroadcastEntry]
	set    [LastRetryDateTime] = GETDATE(), [SentDateTime] = @SentDateTime, [RetryNo] = [RetryNo] + @RetryNo
	where  [EmailBroadcastEntryId] = @EmailBroadcastEntryId
	
	-- Begin Return Select <- do not remove
	select [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority]
	from   [dbo].[EmailBroadcastEntry]
	where  [EmailBroadcastEntryId] = @EmailBroadcastEntryId	
	-- End Return Select <- do not remove

	commit
--===============================================
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailBroadcastEntrySelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE proc [dbo].[bst_EmailBroadcastEntrySelect] 
    @EmailBroadcastEntryId int
as 
	set nocount on 
	set xact_abort on  

	begin tran

	select [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority], [BroadcastId]
	from   [dbo].[EmailBroadcastEntry] 
	where  ([EmailBroadcastEntryId] = @EmailBroadcastEntryId or @EmailBroadcastEntryId is null) 

	commit
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailBroadcastEntryUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create proc [dbo].[bst_EmailBroadcastEntryUpdate] 
    @EmailBroadcastEntryId int,
    @Email nvarchar(100),
    @EmailContent nvarchar(max),
    @LastRetryDateTime datetime = null,
    @SentDateTime datetime = null,
    @RetryNo int = 0,
    @Subject varchar(50),
    @Sender nvarchar(50),
    @IsBodyHtml bit = ''true'',
    @Priority int = 0
as 
	set nocount on 
	set xact_abort on  
	
	begin tran

	update [dbo].[EmailBroadcastEntry]
	set    [Email] = @Email, [EmailContent] = @EmailContent, [LastRetryDateTime] = @LastRetryDateTime, [SentDateTime] = @SentDateTime, [RetryNo] = @RetryNo, [Subject] = @Subject, [Sender] = @Sender, [IsBodyHtml] = @IsBodyHtml, [Priority] = @Priority
	where  [EmailBroadcastEntryId] = @EmailBroadcastEntryId
	
	-- Begin Return Select <- do not remove
	select [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority]
	from   [dbo].[EmailBroadcastEntry]
	where  [EmailBroadcastEntryId] = @EmailBroadcastEntryId	
	-- End Return Select <- do not remove

	commit tran
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailBroadcastInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailBroadcastInsert] 
    @TemplateName nvarchar(100),
    @BroadcastId uniqueidentifier,
    @EntityCode nvarchar(10),
    @ApplicationName nvarchar(50) = ''/Default'',
    @Type nchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Broadcast] ([BroadcastId], [EntityCode], [ApplicationName], [Type], [CreatedDateTime])
	SELECT @BroadcastId, @EntityCode, @ApplicationName, @Type, getdate()
	
	INSERT INTO [dbo].[EmailBroadcast] ([TemplateName], [BroadcastId])
	SELECT @TemplateName, @BroadcastId
	
	-- Begin Return Select <- do not remove
	SELECT [EmailBroadcastId], [TemplateName], [BroadcastId]
	FROM   [dbo].[EmailBroadcast]
	WHERE  [EmailBroadcastId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTemplateDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTemplateDelete] 
    @EmailTemplateId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[EmailTemplate]
	WHERE  [EmailTemplateId] = @EmailTemplateId

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTemplateInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTemplateInsert] 
    @Name nvarchar(100),
    @Description nvarchar(150),
    @EmailContent nvarchar(MAX),
    @Subject nvarchar(50),
    @Sender nvarchar(100),
    @EntityCode nvarchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[EmailTemplate] ([Name], [Description], [EmailContent], [Subject], [Sender], [EntityCode])
	SELECT @Name, @Description, @EmailContent, @Subject, @Sender, @EntityCode
	
	-- Begin Return Select <- do not remove
	SELECT [EmailTemplateId], [Name], [Description], [EmailContent], [Subject], [Sender], [EntityCode]
	FROM   [dbo].[EmailTemplate]
	WHERE  [EmailTemplateId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTemplateSearch]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTemplateSearch] 
    @EntityCode nvarchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EmailTemplateId], [Name], [Description], [EmailContent], [Subject], [Sender], [EntityCode] 
	FROM   [dbo].[EmailTemplate] 
	WHERE  ([EntityCode] = @EntityCode ) 

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTemplateSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTemplateSelect] 
    @EmailTemplateId INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EmailTemplateId], [Name], [Description], [EmailContent], [Subject], [Sender], [EntityCode] 
	FROM   [dbo].[EmailTemplate] 
	WHERE  ([EmailTemplateId] = @EmailTemplateId OR @EmailTemplateId IS NULL) 

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTemplateSelectById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTemplateSelectById] 
    @EmailTemplateId INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EmailTemplateId], [Name], [Description], [EmailContent] , [Subject], [Sender]
	FROM   [dbo].[EmailTemplate] 
	WHERE  ([EmailTemplateId] = @EmailTemplateId OR @EmailTemplateId IS NULL) 

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTemplateSelectByName]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTemplateSelectByName] 
    @TemplateName nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EmailTemplateId], [Name], [Description], [EmailContent], [Subject], [Sender]
	FROM   [dbo].[EmailTemplate] 
	WHERE  ([Name] = @TemplateName) 

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTemplateUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTemplateUpdate] 
    @EmailTemplateId int,
    @Name nvarchar(100),
    @Description nvarchar(150),
    @EmailContent nvarchar(MAX),
    @Subject nvarchar(50),
    @Sender nvarchar(100),
    @EntityCode nvarchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[EmailTemplate]
	SET    [Name] = @Name, [Description] = @Description, [EmailContent] = @EmailContent, [Subject] = @Subject, [Sender] = @Sender, [EntityCode] = @EntityCode
	WHERE  [EmailTemplateId] = @EmailTemplateId
	
	-- Begin Return Select <- do not remove
	SELECT [EmailTemplateId], [Name], [Description], [EmailContent], [Subject], [Sender], [EntityCode]
	FROM   [dbo].[EmailTemplate]
	WHERE  [EmailTemplateId] = @EmailTemplateId	
	-- End Return Select <- do not remove

	COMMIT TRAN
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTrackerDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTrackerDelete] 
    @EmailTrackerId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[EmailTracker]
	WHERE  [EmailTrackerId] = @EmailTrackerId

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTrackerInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTrackerInsert] 
    @EmailBroadcastEntryId uniqueidentifier,
    @Page nvarchar(MAX) = null,
    @IpAddress nvarchar(50) = null,
    @Browser nvarchar(50) =null,
    @Country nvarchar(150)= null,
    @Region nvarchar(150) =null,
    @City nvarchar(150) = null,
    @Postcode nvarchar(50) =null,
    @Latitude nvarchar(50)=null,
    @Longitude nvarchar(50)=null,
    @TimeZone nchar(10) =null
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[EmailTracker] ([EmailBroadcastEntryId], [Page], [IpAddress], [Browser], [DateTime], [Country], [Region], [City], [Postcode], [Latitude], [Longitude], [TimeZone])
	SELECT @EmailBroadcastEntryId, @Page, @IpAddress, @Browser, GETDATE(), @Country, @Region, @City, @Postcode, @Latitude, @Longitude, @TimeZone
	
	-- Begin Return Select <- do not remove
	SELECT [EmailTrackerId], [EmailBroadcastEntryId], [Page], [IpAddress], [Browser], [DateTime], [Country], [Region], [City], [Postcode], [Latitude], [Longitude], [TimeZone]
	FROM   [dbo].[EmailTracker]
	WHERE  [EmailTrackerId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTrackerSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTrackerSelect] 
    @EmailTrackerId INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EmailTrackerId], [EmailBroadcastEntryId], [Page], [IpAddress], [Browser], [DateTime], [Country], [Region], [City], [Postcode], [Latitude], [Longitude], [TimeZone] 
	FROM   [dbo].[EmailTracker] 
	WHERE  ([EmailTrackerId] = @EmailTrackerId OR @EmailTrackerId IS NULL) 

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_EmailTrackerUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[bst_EmailTrackerUpdate] 
    @EmailTrackerId int,
    @EmailBroadcastEntryId uniqueidentifier,
    @Page nvarchar(MAX),
    @IpAddress nvarchar(50),
    @Browser nvarchar(50),
    @DateTime datetime,
    @Country nvarchar(150),
    @Region nvarchar(150),
    @City nvarchar(150),
    @Postcode nvarchar(50),
    @Latitude nvarchar(50),
    @Longitude nvarchar(50),
    @TimeZone nchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[EmailTracker]
	SET    [EmailBroadcastEntryId] = @EmailBroadcastEntryId, [Page] = @Page, [IpAddress] = @IpAddress, [Browser] = @Browser, [DateTime] = @DateTime, [Country] = @Country, [Region] = @Region, [City] = @City, [Postcode] = @Postcode, [Latitude] = @Latitude, [Longitude] = @Longitude, [TimeZone] = @TimeZone
	WHERE  [EmailTrackerId] = @EmailTrackerId
	
	-- Begin Return Select <- do not remove
	SELECT [EmailTrackerId], [EmailBroadcastEntryId], [Page], [IpAddress], [Browser], [DateTime], [Country], [Region], [City], [Postcode], [Latitude], [Longitude], [TimeZone]
	FROM   [dbo].[EmailTracker]
	WHERE  [EmailTrackerId] = @EmailTrackerId	
	-- End Return Select <- do not remove

	COMMIT TRAN
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_GetUnsentEmailBroadcastEntry]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE proc [dbo].[bst_GetUnsentEmailBroadcastEntry] 
as 
	set nocount on 
	set xact_abort on  

	begin tran

	select top 1000 [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority], [BroadcastId]
	from   [dbo].[EmailBroadcastEntry] 
	where  ([SentDateTime] is null)

	commit
	
	
	SET ANSI_NULLS ON
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[bst_GetUnsentEmailBroadcastEntryById]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE proc [dbo].[bst_GetUnsentEmailBroadcastEntryById] 
    @BroadcastId uniqueidentifier
as 
	set nocount on 
	set xact_abort on  

	begin tran

	select [EmailBroadcastEntryId], [Email], [EmailContent], [LastRetryDateTime], [SentDateTime], [RetryNo], [Subject], [Sender], [IsBodyHtml], [Priority], [BroadcastId]
	from   [dbo].[EmailBroadcastEntry] 
	where  ([SentDateTime] is  null) and		
	([BroadcastId] = @BroadcastId) 

	commit' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_EntityDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_EntityDelete] 
    @EntityCode nvarchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Entity]
	WHERE  [EntityCode] = @EntityCode

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_EntityGetNewId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_EntityGetNewId] 
@EntityCode varchar(10) output
AS 
   
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	INSERT INTO [dbo].[EntityCounter] ([DateTimeCreated])
	SELECT GETDATE()
	
	-- Begin Return Select <- do not remove
	
	 SELECT @EntityCode =  ''P'' + REPLICATE(''0'', 9 - LEN(CounterId)) + cast(CounterId AS nvarchar) 
	FROM   [dbo].[EntityCounter]
	WHERE  [CounterId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_EntityInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_EntityInsert] 
    @EntityCode nvarchar(10) output,
    @Active bit = 0,
    @EntityName nvarchar(50),
    @PrimaryContactId int,
    @TimeZone int = 10
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	EXEC	[dbo].[crm_EntityGetNewId] @EntityCode = @EntityCode OUTPUT
	
	INSERT INTO [dbo].[Entity] ([EntityCode], [Active], [EntityName], [PrimaryContactId], [TimeZone])
	SELECT @EntityCode, @Active, @EntityName, @PrimaryContactId, @TimeZone
	
	-- Begin Return Select <- do not remove
	SELECT [EntityCode], [Active], [EntityName], [PrimaryContactId], [TimeZone]
	FROM   [dbo].[Entity]
	WHERE  [EntityCode] = @EntityCode
	-- End Return Select <- do not remove
               
	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_EntityModuleDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_EntityModuleDelete] 
    @EntityModuleId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[EntityModule]
	WHERE  [EntityModuleId] = @EntityModuleId

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_EntityModuleInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_EntityModuleInsert] 
    @Active bit = 0,
    @EndDate datetime = null,
    @EntityCode nvarchar(10),
    @ModuleId int,
    @StartDate datetime = null
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[EntityModule] ([Active], [EndDate], [EntityCode], [ModuleId], [StartDate])
	SELECT @Active, @EndDate, @EntityCode, @ModuleId, @StartDate
	
	-- Begin Return Select <- do not remove
	SELECT [EntityModuleId], [Active], [EndDate], [EntityCode], [ModuleId], [StartDate]
	FROM   [dbo].[EntityModule]
	WHERE  [EntityModuleId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_EntityModuleSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_EntityModuleSelect] 
    @EntityModuleId INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EntityModuleId], [Active], [EndDate], [EntityCode], [ModuleId], [StartDate] 
	FROM   [dbo].[EntityModule] 
	WHERE  ([EntityModuleId] = @EntityModuleId OR @EntityModuleId IS NULL) 

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_EntityModuleUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_EntityModuleUpdate] 
    @EntityModuleId int,
    @Active bit,
    @EndDate datetime,
    @EntityCode nvarchar(10),
    @ModuleId int,
    @StartDate datetime
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[EntityModule]
	SET    [Active] = @Active, [EndDate] = @EndDate, [EntityCode] = @EntityCode, [ModuleId] = @ModuleId, [StartDate] = @StartDate
	WHERE  [EntityModuleId] = @EntityModuleId
	
	-- Begin Return Select <- do not remove
	SELECT [EntityModuleId], [Active], [EndDate], [EntityCode], [ModuleId], [StartDate]
	FROM   [dbo].[EntityModule]
	WHERE  [EntityModuleId] = @EntityModuleId	
	-- End Return Select <- do not remove

	COMMIT TRAN
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_EntitySelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[crm_EntitySelect] 
    @EntityCode NVARCHAR(10) = null,
    @PageSize int = -1,
	@PageIndex int = 0,
	@TotalPopulationSize int output
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON 
	 
    BEGIN TRAN
    
    select @TotalPopulationSize = count(*)
    FROM   [dbo].[Entity] 
	WHERE  ([EntityCode] = @EntityCode OR @EntityCode IS NULL) 
	
		-- Setup paging
	if (@pageSize <= 0)
	begin
		set @pageSize = @totalPopulationSize
		set @pageIndex = 0
	end
	
	
	;with [Entities] as
	(
		select e.*, ROW_NUMBER() OVER(ORDER by e.EntityCode) as RowNumber
	 from  dbo.[Entity] e
	 WHERE  ([EntityCode] = @EntityCode OR @EntityCode IS NULL)
	 )
	 
	Select * from [Entities] where RowNumber between ((@pageSize * @pageIndex) + 1) and (@pageSize * (@pageIndex + 1))

	COMMIT

' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_EntityUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_EntityUpdate] 
    @EntityCode nvarchar(10),
    @Active bit = 0,
    @EntityName nvarchar(50),
    @PrimaryContactId int,
    @TimeZone int = 10 
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Entity]
	SET    [EntityCode] = @EntityCode, [Active] = @Active, [EntityName] = @EntityName, [PrimaryContactId] = @PrimaryContactId, [TimeZone] = @TimeZone
	WHERE  [EntityCode] = @EntityCode
	
	-- Begin Return Select <- do not remove
	SELECT [EntityCode], [Active], [EntityName], [PrimaryContactId], [TimeZone]
	FROM   [dbo].[Entity]
	WHERE  [EntityCode] = @EntityCode	
	-- End Return Select <- do not remove

	COMMIT TRAN
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_ModuleDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_ModuleDelete] 
    @ModuleId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[Module]
	WHERE  [ModuleId] = @ModuleId

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_ModuleInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_ModuleInsert] 
    @Description nvarchar(MAX) = null,
    @Title nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	INSERT INTO [dbo].[Module] ([Description], [Title])
	SELECT @Description, @Title
	
	-- Begin Return Select <- do not remove
	SELECT [ModuleId], [Description], [Title]
	FROM   [dbo].[Module]
	WHERE  [ModuleId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_ModuleSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_ModuleSelect] 
    @ModuleId INT = null
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [ModuleId], [Description], [Title] 
	FROM   [dbo].[Module] 
	WHERE  ([ModuleId] = @ModuleId OR @ModuleId IS NULL) 

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_ModuleSelectByEntity]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'create PROC [dbo].[crm_ModuleSelectByEntity] 
    @EntityCode INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT m.*
	FROM   [dbo].[Module]  m
	inner join dbo.EntityModule em
	on em.ModuleId = m.ModuleId
	WHERE  em.[EntityCode] = @EntityCode  

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[crm_ModuleUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[crm_ModuleUpdate] 
    @ModuleId int,
    @Description nvarchar(MAX) = null,
    @Title nvarchar(50)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[Module]
	SET    [Description] = @Description, [Title] = @Title
	WHERE  [ModuleId] = @ModuleId
	
	-- Begin Return Select <- do not remove
	SELECT [ModuleId], [Description], [Title]
	FROM   [dbo].[Module]
	WHERE  [ModuleId] = @ModuleId	
	-- End Return Select <- do not remove

	COMMIT TRAN
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dsl_DocumentCategorySelectByCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROC [dbo].[dsl_DocumentCategorySelectByCode] 
		@CategoryCode INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT 
		    [DocumentCategoryId]
		  , [Title]
		  , [Code]
		  , [ExpiryPurgeDays]
		  , [AcceptedFileTypes]
		  , [MaximumFileSize]
	FROM   [dbo].[DocumentCategory]
	WHERE  [Code] = @CategoryCode
	
	COMMIT' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dsl_DocumentCategorySelectId]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[dsl_DocumentCategorySelectId]
	@CategoryCode INT,
	@DocumentCategoryId INT OUTPUT	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SElECT @DocumentCategoryId = [DocumentCategoryId] 
	FROM DocumentCategory
	WHERE [Code] = @CategoryCode
    
END
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dsl_DocumentStorageClearUpdate]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[dsl_DocumentStorageClearUpdate] 
    @DocumentId uniqueidentifier
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	-- DFB Nulls the column
	UPDATE	DocumentStorage	
	SET		FileData = CONVERT(varbinary(max), 0)
	WHERE DocumentID = @DocumentId
	
	-- DFB Writes
	UPDATE	DocumentStorage
	SET			UpdatedDate = GETDATE()
			,	FileData.WRITE (NULL, 0, 0)
			,	FileLength = 0
			,	NumberOfChunks = 1
	WHERE	DocumentID = @DocumentId
		       
	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dsl_DocumentStorageDelete]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[dsl_DocumentStorageDelete] 
		@DocumentID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	DELETE DocumentStorage 
	WHERE  [DocumentID] = @DocumentID

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dsl_DocumentStorageInsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[dsl_DocumentStorageInsert] 
    @DocumentID uniqueidentifier,
    @AccountID uniqueidentifier = NULL,
    @CategoryCode int,
    @ApplicationCode nvarchar(30) = ''/Default'',
    @EntityCode nvarchar(10),
    @FileType nvarchar(MAX),
    @FileName nvarchar(MAX),
    @IsPrivate bit = 0,
    @Reference nvarchar(100) = NULL,
    @StartDate datetime = NULL,
    @EndDate datetime = null,
    @Username nvarchar(50) = NULL
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	DECLARE @CreatedDate DATETIME
	SET @CreatedDate = GETDATE()
	DECLARE @UpdateDateTime datetime
	SET @UpdateDateTime = GETDATE()
	DECLARE @FileLength INT
	SET @FileLength = 0
	-- Get the ID of the Document Category to store again
	DECLARE @DocumentCategoryId INT
	EXEC [dsl_DocumentCategorySelectId] @CategoryCode, @DocumentCategoryId = @DocumentCategoryId OUTPUT
		
	BEGIN
		IF @StartDate IS NULL
			SET @StartDate = GETDATE()
	END
	
	INSERT INTO [dbo].[DocumentStorage] 
		(	[DocumentID],
			[AccountID], 
			[DocumentCategoryId], 
			[ApplicationCode], 
			[EndDate], 
			[EntityCode],
			[FileType],
			[FileLength],
			[FileName], 
			[IsPrivate], 
			[Reference], 
			[StartDate], 
			[CreatedDate], 
			[Username], 
			[UpdatedDate])
			
	SELECT 
		@DocumentID,
		@AccountID, 
		@DocumentCategoryId,
		@ApplicationCode, 
		@EndDate, 
		@EntityCode, 
		@FileType,
		@FileLength, 
		@FileName, 
		@IsPrivate, 
		@Reference, 
		@StartDate, 
		@CreatedDate, 
		@Username, 
		@UpdateDateTime
	
	-- Begin Return Select <- do not remove
	SELECT [DocumentID]
			, [AccountID]
			, [DocumentCategoryId]
			, [ApplicationCode]
			, [EndDate]
			, [EntityCode]
			, [FileType]
			, [FileLength]
			, [FileName]
			, [IsPrivate]
			, [Reference]
			, [StartDate]
			, [CreatedDate]
			, [UpdatedDate]
			, [Username]
	
	FROM   [dbo].[DocumentStorage]
	WHERE  [DocumentID] = @DocumentID
	-- End Return Select <- do not remove
               
	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dsl_DocumentStorageSelect]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[dsl_DocumentStorageSelect] 
		@DocumentID UNIQUEIDENTIFIER
    ,	@EntityCode NVARCHAR(10) = null
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT 
		  [DocumentID]
		, [ApplicationCode]
		, [EntityCode]
		, [AccountID] 
		, [DocumentCategoryId]
		, [Username]
		, [FileType]
		, [FileLength]
		, [FileName]
		, [Reference]
		, [IsPrivate]
		, [StartDate]
		, [EndDate]
		, [CreatedDate]
		, [UpdatedDate]
	FROM   [dbo].[DocumentStorage]
	WHERE  
			([DocumentID] = @DocumentID) 
			AND (([IsPrivate] = 1 AND @EntityCode = [EntityCode]) OR [IsPrivate] = 0)

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dsl_DocumentStorageSelectFileData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[dsl_DocumentStorageSelectFileData] 
    @DocumentID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT 
		  [FileData]
	FROM   [dbo].[DocumentStorage]
	WHERE  ([DocumentID] = @DocumentID) 

	COMMIT
' 
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dsl_DocumentStorageUpdateFileData]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[dsl_DocumentStorageUpdateFileData] 
    @DocumentId		uniqueidentifier,
    @Offset int= -1,
    @FileDataChunk	varbinary(max)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN
	
	-- DFB Nulls the column
	if (@Offset = 0)
	begin
		UPDATE	DocumentStorage	
		SET		FileData=@FileDataChunk
		WHERE	DocumentID = @DocumentId
	end
	else
	begin
		UPDATE	DocumentStorage	
		SET		FileData.Write(@FileDataChunk, null, null)
		WHERE	DocumentID = @DocumentId
	end	
	-- DFB Writes
	UPDATE	DocumentStorage
	SET			UpdatedDate = GETDATE()
			,	FileLength = LEN(FileData)
			,	NumberOfChunks = NumberOfChunks + 1
	WHERE	DocumentID = @DocumentId
		       
	COMMIT
' 
END
GO
