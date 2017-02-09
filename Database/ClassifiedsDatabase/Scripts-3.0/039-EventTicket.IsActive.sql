ALTER TABLE [EventTicket]
ADD IsActive BIT NULL 

GO 

UPDATE dbo.[EventTicket]
SET IsActive = 1

GO

ALTER TABLE dbo.[EventTicket]
ALTER COLUMN IsActive BIT NOT NULL

GO