GO

IF NOT EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'ColourCode'
          AND Object_ID = Object_ID(N'dbo.EventTicket'))
BEGIN
    ALTER TABLE EventTicket
	ADD ColourCode VARCHAR(10)
END

GO