CREATE PROC [dbo].[dsl_DocumentStorageSelect] 
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
