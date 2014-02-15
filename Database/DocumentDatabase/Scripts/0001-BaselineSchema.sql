SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[dsl_DocumentCategorySelectByCode]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROC [dbo].[dsl_DocumentCategorySelectByCode] 
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
	
	COMMIT

' 
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
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentCategory]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentCategory](
	[DocumentCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[Code] [int] NOT NULL,
	[ExpiryPurgeDays] [int] NULL,
	[AcceptedFileTypes] [varchar](255) NULL,
	[MaximumFileSize] [int] NULL,
 CONSTRAINT [PK_DocumentCategory] PRIMARY KEY CLUSTERED 
(
	[DocumentCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DocumentStorage]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[DocumentStorage](
	[DocumentID] [uniqueidentifier] NOT NULL,
	[ApplicationCode] [nvarchar](30) NOT NULL,
	[EntityCode] [nvarchar](10) NOT NULL,
	[AccountID] [uniqueidentifier] NULL,
	[DocumentCategoryId] [int] NOT NULL,
	[Username] [nvarchar](50) NULL,
	[FileType] [nvarchar](max) NOT NULL,
	[FileData] [varbinary](max) NULL,
	[FileLength] [int] NOT NULL,
	[NumberOfChunks] [numeric](18, 0) NULL,
	[FileName] [nvarchar](100) NULL,
	[Reference] [nvarchar](100) NULL,
	[IsPrivate] [bit] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[IsFileComplete] [bit] NULL,
 CONSTRAINT [PK_DocumentStorage] PRIMARY KEY CLUSTERED 
(
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[dbo].[DF_DocumentStorage_IsFileComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[DocumentStorage] ADD  CONSTRAINT [DF_DocumentStorage_IsFileComplete]  DEFAULT ((0)) FOR [IsFileComplete]
END

GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DocumentStorage_DocumentCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentStorage]'))
ALTER TABLE [dbo].[DocumentStorage]  WITH CHECK ADD  CONSTRAINT [FK_DocumentStorage_DocumentCategory] FOREIGN KEY([DocumentCategoryId])
REFERENCES [dbo].[DocumentCategory] ([DocumentCategoryId])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_DocumentStorage_DocumentCategory]') AND parent_object_id = OBJECT_ID(N'[dbo].[DocumentStorage]'))
ALTER TABLE [dbo].[DocumentStorage] CHECK CONSTRAINT [FK_DocumentStorage_DocumentCategory]
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'DocumentCategory', N'COLUMN',N'Code'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Refer to DslDocumentCategory within the Paramount Business Objects' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentCategory', @level2type=N'COLUMN',@level2name=N'Code'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'DocumentCategory', N'COLUMN',N'ExpiryPurgeDays'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Number of days images are kept until purged. NULL value indicates never.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentCategory', @level2type=N'COLUMN',@level2name=N'ExpiryPurgeDays'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'DocumentCategory', N'COLUMN',N'AcceptedFileTypes'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Semi Colon separated list of accepted file extensions' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentCategory', @level2type=N'COLUMN',@level2name=N'AcceptedFileTypes'
GO
IF NOT EXISTS (SELECT * FROM ::fn_listextendedproperty(N'MS_Description' , N'SCHEMA',N'dbo', N'TABLE',N'DocumentCategory', N'COLUMN',N'MaximumFileSize'))
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Maximum size of each file in bytes allowed for this category. 1 megabytes = 1 048 576 bytes' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DocumentCategory', @level2type=N'COLUMN',@level2name=N'MaximumFileSize'
GO
