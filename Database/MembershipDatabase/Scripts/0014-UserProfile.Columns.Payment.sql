
IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[UserProfile]') 
        AND name = 'PreferredPaymentMethod'
)
begin
	ALTER TABLE UserProfile
	ADD [PreferredPaymentMethod] VARCHAR(30)

end;





IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[UserProfile]') 
        AND name = 'PayPalEmail'
)
begin
	ALTER TABLE UserProfile
	ADD [PayPalEmail] VARCHAR(30)

end;