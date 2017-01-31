ALTER TABLE [EventTicketReservation]
ADD IsPublic BIT NULL 

GO 

UPDATE dbo.[EventTicketReservation]
SET IsPublic = 0

GO

ALTER TABLE dbo.[EventTicketReservation]
ALTER COLUMN IsPublic BIT NOT NULL

GO