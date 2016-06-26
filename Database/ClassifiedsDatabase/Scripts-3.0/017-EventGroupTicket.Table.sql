
/****** Object:  Table [dbo].[EventGroupTicket]    Script Date: 26/06/2016 7:42:10 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventGroupTicket](
	[EventGroupTicketId] [int] IDENTITY(1,1) NOT NULL,
	[EventGroupId] [int] NOT NULL,
	[EventTicketId] [int] NOT NULL,
 CONSTRAINT [PK_EventGroupTicket] PRIMARY KEY CLUSTERED 
(
	[EventGroupTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EventGroupTicket]  WITH CHECK ADD  CONSTRAINT [FK_EventGroupTicket_EventGroup] FOREIGN KEY([EventGroupId])
REFERENCES [dbo].[EventGroup] ([EventGroupId])
GO

ALTER TABLE [dbo].[EventGroupTicket] CHECK CONSTRAINT [FK_EventGroupTicket_EventGroup]
GO

ALTER TABLE [dbo].[EventGroupTicket]  WITH CHECK ADD  CONSTRAINT [FK_EventGroupTicket_EventTicket] FOREIGN KEY([EventTicketId])
REFERENCES [dbo].[EventTicket] ([EventTicketId])
GO

ALTER TABLE [dbo].[EventGroupTicket] CHECK CONSTRAINT [FK_EventGroupTicket_EventTicket]
GO

