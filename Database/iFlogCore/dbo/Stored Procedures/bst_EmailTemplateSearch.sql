CREATE PROC [dbo].[bst_EmailTemplateSearch] 
    @EntityCode nvarchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  

	BEGIN TRAN

	SELECT [EmailTemplateId], [Name], [Description], [EmailContent], [Subject], [Sender], [EntityCode] 
	FROM   [dbo].[EmailTemplate] 
	WHERE  ([EntityCode] = @EntityCode ) 

	COMMIT
