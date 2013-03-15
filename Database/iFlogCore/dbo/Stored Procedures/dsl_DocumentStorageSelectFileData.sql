CREATE PROC [dbo].[dsl_DocumentStorageSelectFileData] 
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
