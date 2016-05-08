IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'TransactionFee' AND Object_ID = Object_ID(N'EventBooking'))
BEGIN
    
	ALTER TABLE [EventBooking]
	ADD  TransactionFee MONEY NULL
	
END

GO

IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'Cost' AND Object_ID = Object_ID(N'EventBooking'))
BEGIN
    
	ALTER TABLE [EventBooking]
	ADD  Cost MONEY NULL
	
END