IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'TransactionFee' AND Object_ID = Object_ID(N'EventTicketReservation'))
BEGIN
    
	ALTER TABLE [EventTicketReservation]
	ADD  TransactionFee MONEY NULL
		
END