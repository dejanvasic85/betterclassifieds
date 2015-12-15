
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


IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[UserProfile]') 
        AND name = 'BankName'
)
begin
	ALTER TABLE UserProfile
	ADD [BankName] VARCHAR(50)

end;



IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[UserProfile]') 
        AND name = 'BankAccountName'
)
begin
	ALTER TABLE UserProfile
	ADD [BankAccountName] VARCHAR(100)

end;




IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[UserProfile]') 
        AND name = 'BankAccountNumber'
)
begin
	ALTER TABLE UserProfile
	ADD [BankAccountNumber] VARCHAR(20)

end;


IF NOT EXISTS (
  SELECT * 
  FROM   sys.columns 
  WHERE  object_id = OBJECT_ID(N'[dbo].[UserProfile]') 
        AND name = 'BankBsbNumber'
)
begin
	ALTER TABLE UserProfile
	ADD [BankBsbNumber] VARCHAR(10)

end;
