CREATE PROC [dbo].[dsl_DocumentStorageUpdateFileData] 
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
