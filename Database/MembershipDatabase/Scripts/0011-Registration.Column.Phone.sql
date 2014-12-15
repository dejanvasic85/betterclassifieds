
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[Registration]') 
        AND name = 'Phone'
)
begin
	ALTER TABLE Registration
	ADD [Phone] VARCHAR(100)

end;