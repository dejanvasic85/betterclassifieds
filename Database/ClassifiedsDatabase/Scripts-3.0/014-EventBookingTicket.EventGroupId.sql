
ALTER TABLE [dbo].[EventBookingTicket] 
ADD EventGroupId INT NULL

GO

ALTER TABLE [dbo].[EventBookingTicket]  
WITH CHECK ADD  CONSTRAINT [FK_EventBookingTicket_EventGroup] 
FOREIGN KEY([EventGroupId])
REFERENCES [dbo].[EventGroup] ([EventGroupId])