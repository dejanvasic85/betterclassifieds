GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'SeatNumber'
          AND Object_ID = Object_ID(N'dbo.EventTicketReservation'))
BEGIN
    ALTER TABLE dbo.[EventTicketReservation]
	ADD SeatNumber VARCHAR(50) NULL
END

GO