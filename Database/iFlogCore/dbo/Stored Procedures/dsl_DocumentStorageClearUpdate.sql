CREATE PROC [dbo].[dsl_DocumentStorageClearUpdate] 
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
