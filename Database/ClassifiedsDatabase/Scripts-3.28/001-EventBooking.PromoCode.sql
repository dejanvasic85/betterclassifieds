GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'PromoCode'
          AND Object_ID = Object_ID(N'dbo.EventBooking'))
BEGIN
    ALTER TABLE dbo.[EventBooking]
	ADD PromoCode VARCHAR(50) NULL

	
END

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'DiscountPercent'
          AND Object_ID = Object_ID(N'dbo.EventBooking'))
BEGIN
    ALTER TABLE dbo.[EventBooking]
	ADD DiscountPercent MONEY NULL

	
END

GO