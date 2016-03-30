IF NOT EXISTS(SELECT * FROM sys.columns 
            WHERE Name = N'IncludeTransactionFee' AND Object_ID = Object_ID(N'Event'))
BEGIN
    
	ALTER TABLE [Event]
	ADD  IncludeTransactionFee BIT NULL
	ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_IncludeTransactionFee]  DEFAULT ((1)) FOR [IncludeTransactionFee]
	
END