CREATE PROC [dbo].[bst_EmailTemplateUpdate] 
    @EmailTemplateId int,
    @Name nvarchar(100),
    @Description nvarchar(150),
    @EmailContent nvarchar(MAX),
    @Subject nvarchar(50),
    @Sender nvarchar(100),
    @EntityCode nvarchar(10)
AS 
	SET NOCOUNT ON 
	SET XACT_ABORT ON  
	
	BEGIN TRAN

	UPDATE [dbo].[EmailTemplate]
	SET    [Name] = @Name, [Description] = @Description, [EmailContent] = @EmailContent, [Subject] = @Subject, [Sender] = @Sender, [EntityCode] = @EntityCode
	WHERE  [EmailTemplateId] = @EmailTemplateId
	
	-- Begin Return Select <- do not remove
	SELECT [EmailTemplateId], [Name], [Description], [EmailContent], [Subject], [Sender], [EntityCode]
	FROM   [dbo].[EmailTemplate]
	WHERE  [EmailTemplateId] = @EmailTemplateId	
	-- End Return Select <- do not remove

	COMMIT TRAN
