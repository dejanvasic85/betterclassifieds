
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[UserProfile]') 
        AND name = 'RequiresEventOrganiserConfirmation'
)
begin
	ALTER TABLE UserProfile
	ADD [RequiresEventOrganiserConfirmation] BIT NULL

end;