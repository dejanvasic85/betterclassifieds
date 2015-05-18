
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Registration]') 
        AND name = 'ConfirmationAttempts'
)
begin
	ALTER TABLE Registration
	ADD [ConfirmationAttempts] INT

end;