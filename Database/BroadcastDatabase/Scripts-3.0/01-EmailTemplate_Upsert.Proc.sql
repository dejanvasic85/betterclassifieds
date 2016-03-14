SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EmailTemplate_Upsert]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 14-MAR-2016
-- Description:	Updates or creates an email template with all the condition checking in place
-- =============================================
CREATE PROCEDURE [dbo].[EmailTemplate_Upsert]
	@DocType VARCHAR(50),
	@Brand VARCHAR(50),
	@BodyTemplate NVARCHAR(MAX),

	@SubjectTemplate VARCHAR(100) = null,	
	@Description VARCHAR(200) = null,
	@From VARCHAR(200) = null	
AS
BEGIN
	
	DECLARE @currentDateTime AS DATETIME;
	SET		@currentDateTime = GETDATE();
	DECLARE @currentDateTimeUtc AS DATETIME;
	SET		@currentDateTimeUtc = GETUTCDATE();

	DECLARE @IsBodyHtml BIT = 1,
			@Parser VARCHAR(50) = ''SquareBracketParser'';
	

	IF NOT EXISTS ( SELECT 1 FROM EmailTemplate WHERE DocType = @DocType AND Brand = @Brand )
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
			   (@DocType
			   ,@Description
			   ,@SubjectTemplate
			   ,@BodyTemplate
			   ,@IsBodyHtml
			   ,@From
			   ,''SquareBracketParser''
			   ,''System''
			   ,@currentDateTime
			   ,@currentDateTimeUtc
			   ,@Brand)
	end
	ELSE
	begin
		UPDATE	EmailTemplate
		SET		BodyTemplate = @BodyTemplate
		WHERE	DocType = @DocType AND Brand = @Brand		
	end
END
' 
END
GO
