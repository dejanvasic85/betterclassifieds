IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'TransactionFee' AND Object_ID = Object_ID(N'EventBookingTicket'))
BEGIN
    
	ALTER TABLE [EventBookingTicket]
	ADD  TransactionFee MONEY NULL
	
END

GO

IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'TotalPrice' AND Object_ID = Object_ID(N'EventBookingTicket'))
BEGIN
    
	ALTER TABLE [EventBookingTicket]
	ADD  TotalPrice MONEY NULL
	
END