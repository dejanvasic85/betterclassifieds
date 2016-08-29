IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'IsActive' AND Object_ID = Object_ID(N'EventBookingTicket'))
BEGIN
    
	ALTER TABLE [EventBookingTicket]
	ADD  IsActive BIT NULL
		
END

GO

UPDATE [EventBookingTicket]
SET IsActive = 1
WHERE IsActive IS NULL