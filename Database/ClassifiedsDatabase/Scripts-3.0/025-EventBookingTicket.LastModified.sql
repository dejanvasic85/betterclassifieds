IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'LastModifiedDate' AND Object_ID = Object_ID(N'EventBookingTicket'))
BEGIN
    
	ALTER TABLE [EventBookingTicket]
	ADD  LastModifiedDate DATETIME NULL
		
END

GO

IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'LastModifiedDateUTC' AND Object_ID = Object_ID(N'EventBookingTicket'))
BEGIN
    
	ALTER TABLE [EventBookingTicket]
	ADD  LastModifiedDateUtc DATETIME NULL

END

IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'LastModifiedBy' AND Object_ID = Object_ID(N'EventBookingTicket'))
BEGIN
    
	ALTER TABLE [EventBookingTicket]
	ADD  LastModifiedBy NVARCHAR(50) NULL
		
END