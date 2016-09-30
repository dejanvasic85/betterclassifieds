IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'BarcodeImageDocumentId' AND Object_ID = Object_ID(N'EventBookingTicket'))
BEGIN
    
	ALTER TABLE [EventBookingTicket]
	ADD  BarcodeImageDocumentId UNIQUEIDENTIFIER NULL
		
END

GO