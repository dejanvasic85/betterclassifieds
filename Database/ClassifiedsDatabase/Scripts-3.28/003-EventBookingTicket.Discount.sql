GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'DiscountPercent'
          AND Object_ID = Object_ID(N'dbo.EventBookingTicket'))
BEGIN
    ALTER TABLE dbo.[EventBookingTicket]
	ADD DiscountPercent MONEY NULL	
END

GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'DiscountAmount'
          AND Object_ID = Object_ID(N'dbo.EventBookingTicket'))
BEGIN
    ALTER TABLE dbo.[EventBookingTicket]
	ADD DiscountAmount MONEY NULL

END

GO