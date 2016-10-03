IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'GroupsRequired' AND Object_ID = Object_ID(N'Event'))
BEGIN
    
	ALTER TABLE [Event]
	ADD  GroupsRequired BIT NULL
		
END