IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'VenueName' AND Object_ID = Object_ID(N'Event'))
BEGIN
    
	ALTER TABLE [Event]
	ADD  VenueName VARCHAR(100)
		
END