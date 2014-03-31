

DECLARE @currentDateTime AS DATETIME;
SET		@currentDateTime = GETDATE();
DECLARE @currentDateTimeUtc AS DATETIME;
SET		@currentDateTimeUtc = GETUTCDATE();



IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE DocType = 'ActivityReport' )
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
			   ,[ModifiedDateUtc])
		 VALUES
			   ('ActivityReport'
			   ,'Email contains details of what happened for the day (sent at night).'
			   ,'<todo>' -- Subject
			   ,'<todo>' -- Body
			   ,1
			   ,'<todo>' -- From
			   ,'SquareBracketParser'
			   ,'System'
			   ,@currentDateTime
			   ,@currentDateTimeUtc)
end

UPDATE	EmailTemplate
SET	[SubjectTemplate]  = 'Classies HealthCheck',
	[From] = 'classies@themusic.com.au',
	[BodyTemplate] = '<h3>Daily Activity for [/ReportDate/] Environment : [/Environment/]</h3>
[/ClassifiedsTable/]
<br>
<h3>Elmah Errors</h3>
<br>
[/LogTable/]
'
WHERE	DocType	= 'ActivityReport'
