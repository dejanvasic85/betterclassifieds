CREATE PROC [dbo].[bst_EmailTemplateInsert] 
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
	
	INSERT INTO [dbo].[EmailTemplate] ([Name], [Description], [EmailContent], [Subject], [Sender], [EntityCode])
	SELECT @Name, @Description, @EmailContent, @Subject, @Sender, @EntityCode
	
	-- Begin Return Select <- do not remove
	SELECT [EmailTemplateId], [Name], [Description], [EmailContent], [Subject], [Sender], [EntityCode]
	FROM   [dbo].[EmailTemplate]
	WHERE  [EmailTemplateId] = SCOPE_IDENTITY()
	-- End Return Select <- do not remove
               
	COMMIT
