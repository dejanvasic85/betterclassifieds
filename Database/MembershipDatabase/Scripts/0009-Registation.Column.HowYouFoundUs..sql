IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Registration]') 
         AND name = 'HowYouFoundUs'
)
begin
	ALTER TABLE Registration
	ADD [HowYouFoundUs] VARCHAR(100)

end;
