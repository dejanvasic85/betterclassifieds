CREATE PROC [dbo].[bst_EmailTemplateDelete] 
    @EmailTemplateId int
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	DELETE
	FROM   [dbo].[EmailTemplate]
	WHERE  [EmailTemplateId] = @EmailTemplateId

	COMMIT
