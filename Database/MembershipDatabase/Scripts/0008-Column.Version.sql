IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Registration]') 
         AND name = 'Version'
)
begin
	ALTER TABLE Registration
	ADD [Version] Timestamp

end