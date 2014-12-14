
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[UserProfile]') 
        AND name = 'HowYouFoundUs'
)
begin
	ALTER TABLE UserProfile
	ADD [HowYouFoundUs] VARCHAR(100)

end;