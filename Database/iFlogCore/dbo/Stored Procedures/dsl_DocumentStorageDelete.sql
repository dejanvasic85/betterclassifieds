CREATE PROC [dbo].[dsl_DocumentStorageDelete] 
		@DocumentID UNIQUEIDENTIFIER
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	DELETE DocumentStorage 
	WHERE  [DocumentID] = @DocumentID

	COMMIT
