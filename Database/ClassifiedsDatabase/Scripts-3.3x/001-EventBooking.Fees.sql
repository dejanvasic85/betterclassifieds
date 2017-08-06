GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'FeePercentage'
          AND Object_ID = Object_ID(N'dbo.EventBooking'))
BEGIN
    ALTER TABLE dbo.[EventBooking]
	ADD FeePercentage MONEY NULL
END

GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'FeeCents'
          AND Object_ID = Object_ID(N'dbo.EventBooking'))
BEGIN
    ALTER TABLE dbo.[EventBooking]
	ADD FeeCents MONEY NULL
END

GO

UPDATE EventBooking
SET	FeePercentage = .021;

UPDATE EventBooking
SET	FeeCents = 30;
