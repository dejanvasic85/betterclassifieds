
GO


/****** Object:  StoredProcedure [dbo].[dsl_DocumentStorageSelect]    Script Date: 12/10/2014 9:25:50 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[dsl_DocumentStorageSelect] 
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

	COMMIT

