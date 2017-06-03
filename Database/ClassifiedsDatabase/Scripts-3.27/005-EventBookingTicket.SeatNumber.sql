GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'SeatNumber'
          AND Object_ID = Object_ID(N'dbo.EventBookingTicket'))
BEGIN
    ALTER TABLE dbo.[EventBookingTicket]
	ADD SeatNumber VARCHAR(50) NULL
END

GO