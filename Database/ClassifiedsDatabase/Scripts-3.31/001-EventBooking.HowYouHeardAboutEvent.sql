GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'HowYouHeardAboutEvent'
          AND Object_ID = Object_ID(N'dbo.EventBooking'))
BEGIN
    ALTER TABLE dbo.[EventBooking]
	ADD HowYouHeardAboutEvent VARCHAR(100) NULL
END

GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'HowYouHeardAboutEventOptions'
          AND Object_ID = Object_ID(N'dbo.Event'))
BEGIN
    ALTER TABLE dbo.[Event]
	ADD HowYouHeardAboutEventOptions VARCHAR(100) NULL
END
