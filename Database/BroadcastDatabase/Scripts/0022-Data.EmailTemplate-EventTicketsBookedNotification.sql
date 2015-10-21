

DECLARE @currentDateTime AS DATETIME;
SET		@currentDateTime = GETDATE();
DECLARE @currentDateTimeUtc AS DATETIME;
SET		@currentDateTimeUtc = GETUTCDATE();

DECLARE @docTypeName AS VARCHAR(20) = 'EventTicketsBooked',
		@kandoBayBrand AS VARCHAR(20) = 'KandoBay',
		@theMusicBrand AS VARCHAR(20) = 'TheMusic';

IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE DocType = @docTypeName AND Brand = @kandoBayBrand)
begin
	INSERT INTO [dbo].[EmailTemplate]
			   ([DocType]
			   ,[Description]
			   ,[SubjectTemplate]
			   ,[BodyTemplate]
			   ,[IsBodyHtml]
			   ,[From]
			   ,[ParserName]
			   ,[ModifiedBy]
			   ,[ModifiedDate]
			   ,[ModifiedDateUtc]
			   ,[Brand])
		 VALUES
			   ('EventTicketsBooked'
			   ,'Email directly to customer who purchased the tickets and should contain attachments so they can print.'
			   ,'<todo>' -- Subject
			   ,'<todo>' -- Body
			   ,1
			   ,'<todo>' -- From
			   ,'SquareBracketParser'
			   ,'System'
			   ,@currentDateTime
			   ,@currentDateTimeUtc
			   ,@kandoBayBrand)
end


IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE DocType = @docTypeName AND Brand = @theMusicBrand)
begin
	INSERT INTO [dbo].[EmailTemplate]
			   ([DocType]
			   ,[Description]
			   ,[SubjectTemplate]
			   ,[BodyTemplate]
			   ,[IsBodyHtml]
			   ,[From]
			   ,[ParserName]
			   ,[ModifiedBy]
			   ,[ModifiedDate]
			   ,[ModifiedDateUtc]
			   ,[Brand])
		 VALUES
			   ('EventTicketsBooked'
			   ,'Email directly to customer who purchased the tickets and should contain attachments so they can print.'
			   ,'<todo>' -- Subject
			   ,'<todo>' -- Body
			   ,1
			   ,'<todo>' -- From
			   ,'SquareBracketParser'
			   ,'System'
			   ,@currentDateTime
			   ,@currentDateTimeUtc
			   ,@theMusicBrand)
end

UPDATE	EmailTemplate
SET	[SubjectTemplate]  = 'See you at [/EventName/]',
	[From] = 'kandobay-support@kandobay.com.au',
	[BodyTemplate] = 'Thank you for purchasing tickets with KandoBay. Your tickets should be attached to this email ready to be printed.'
WHERE	DocType	= @docTypeName
	and Brand = @kandoBayBrand

	UPDATE	EmailTemplate
SET	[SubjectTemplate]  = 'See you at [/EventName/]',
	[From] = 'classies@themusic.com.au',
	[BodyTemplate] = 'Thank you for purchasing tickets at TheMusic. Your tickets should be attached to this email ready to be printed.'
WHERE	DocType	= @docTypeName
	and Brand = @theMusicBrand
