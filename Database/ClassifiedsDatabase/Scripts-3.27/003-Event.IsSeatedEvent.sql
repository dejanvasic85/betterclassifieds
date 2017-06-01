GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'ColourCode'
          AND Object_ID = Object_ID(N'dbo.Event'))
BEGIN
    ALTER TABLE dbo.[Event]
	ADD IsSeatedEvent BIT NULL
END

GO