IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Registration]') 
         AND name = 'ConfirmationDate'
)
begin
	ALTER TABLE Registration
	ADD ConfirmationDate DATETIME

	ALTER TABLE Registration
	Add ConfirmationDateUtc DATETIME
end