/****** Object:  Table [dbo].[EventTicketReservation]    Script Date: 2/10/2015 9:05:07 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventTicketReservation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[EventTicketReservation](
	[EventTicketReservationId] [int] IDENTITY(1,1) NOT NULL,
	[EventTicketId] [int] NOT NULL,
	[SessionId] [varchar](50) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[ExpiryDate] [datetime] NULL,
	[ExpiryDateUtc] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedDateUtc] [datetime] NULL,
 CONSTRAINT [PK_EventTicketReservation] PRIMARY KEY CLUSTERED 
(
	[EventTicketReservationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventTicketReservation_EventTicket]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventTicketReservation]'))
ALTER TABLE [dbo].[EventTicketReservation]  WITH CHECK ADD  CONSTRAINT [FK_EventTicketReservation_EventTicket] FOREIGN KEY([EventTicketId])
REFERENCES [dbo].[EventTicket] ([EventTicketId])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_EventTicketReservation_EventTicket]') AND parent_object_id = OBJECT_ID(N'[dbo].[EventTicketReservation]'))
ALTER TABLE [dbo].[EventTicketReservation] CHECK CONSTRAINT [FK_EventTicketReservation_EventTicket]
GO
