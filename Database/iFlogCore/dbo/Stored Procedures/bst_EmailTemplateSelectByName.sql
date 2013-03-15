CREATE PROC [dbo].[bst_EmailTemplateSelectByName] 
    @TemplateName nvarchar(100)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EmailTemplateId], [Name], [Description], [EmailContent], [Subject], [Sender]
	FROM   [dbo].[EmailTemplate] 
	WHERE  ([Name] = @TemplateName) 

	COMMIT
