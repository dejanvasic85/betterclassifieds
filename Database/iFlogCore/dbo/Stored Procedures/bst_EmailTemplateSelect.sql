CREATE PROC [dbo].[bst_EmailTemplateSelect] 
    @EmailTemplateId INT
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EmailTemplateId], [Name], [Description], [EmailContent], [Subject], [Sender], [EntityCode] 
	FROM   [dbo].[EmailTemplate] 
	WHERE  ([EmailTemplateId] = @EmailTemplateId OR @EmailTemplateId IS NULL) 

	COMMIT
