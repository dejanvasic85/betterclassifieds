ALTER TABLE [EventBookingTicket]
ADD IsPublic BIT NULL 

GO 

UPDATE dbo.[EventBookingTicket]
SET IsPublic = 0

GO

ALTER TABLE dbo.[EventBookingTicket]
ALTER COLUMN IsPublic BIT NOT NULL

GO