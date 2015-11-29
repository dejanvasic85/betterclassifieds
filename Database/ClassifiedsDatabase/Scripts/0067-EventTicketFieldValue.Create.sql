GO

/****** Object:  Table [dbo].[EventBookingTicketField]    Script Date: 29/11/2015 11:11:56 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EventBookingTicketField](
	[EventBookingTicketFIeldId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventBookingTicketId] [int] NOT NULL,
	[FieldName] [varchar](50) NOT NULL,
	[FieldValue] [varchar](255) NULL,
 CONSTRAINT [PK_EventBookingTicketField] PRIMARY KEY CLUSTERED 
(
	[EventBookingTicketFIeldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EventBookingTicketField]  WITH CHECK ADD  CONSTRAINT [FK_EventBookingTicketField_EventBookingTicket] FOREIGN KEY([EventBookingTicketId])
REFERENCES [dbo].[EventBookingTicket] ([EventBookingTicketId])
GO

ALTER TABLE [dbo].[EventBookingTicketField] CHECK CONSTRAINT [FK_EventBookingTicketField_EventBookingTicket]
GO

