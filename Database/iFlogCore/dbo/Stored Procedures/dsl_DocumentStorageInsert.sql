CREATE PROC [dbo].[dsl_DocumentStorageInsert] 
    @DocumentID uniqueidentifier,
    @AccountID uniqueidentifier = NULL,
    @CategoryCode int,
    @ApplicationCode nvarchar(30) = '/Default',
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
