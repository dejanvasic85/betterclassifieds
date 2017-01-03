-- Drop from EventBooking 
ALTER TABLE EventBooking
DROP COLUMN TicketsDocumentId

GO

-- Add to EventBookingTicket
IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'TicketDocumentId' AND Object_ID = Object_ID(N'EventBookingTicket'))
BEGIN
    
	ALTER TABLE [EventBookingTicket]
	ADD  TicketDocumentId UNIQUEIDENTIFIER NULL
	
END

GO