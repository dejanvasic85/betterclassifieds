GO

/****** Object:  Table [dbo].[EventSeatBooking]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventSeatBooking](
	[EventSeatBookingId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventTicketId] [int] NOT NULL,
	[EventBookingTicketId] [int] NULL,
	[RowNumber] varchar(50) NULL,
	[RowOrder] int NULL,
	[SeatNumber] [varchar](50) NOT NULL,
	[SeatOrder] [int] NOT NULL,
	[NotAvailableToPublic] [bit] NULL
	CONSTRAINT [PK_EventSeatBooking] PRIMARY KEY CLUSTERED 
(
	[EventSeatBookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EventSeatBooking]  WITH CHECK ADD  CONSTRAINT [FK_EventSeatBooking_EventTicket] FOREIGN KEY([EventTicketId])
REFERENCES [dbo].[EventTicket] ([EventTicketId])
GO

ALTER TABLE [dbo].[EventSeatBooking] CHECK CONSTRAINT [FK_EventSeatBooking_EventTicket]
GO



GO

ALTER TABLE [dbo].[EventSeatBooking]  WITH CHECK ADD  CONSTRAINT [FK_EventSeatBooking_EventBookingTicket] FOREIGN KEY([EventBookingTicketId])
REFERENCES [dbo].[EventBookingTicket] ([EventBookingTicketId])
GO

ALTER TABLE [dbo].[EventSeatBooking] CHECK CONSTRAINT [FK_EventSeatBooking_EventBookingTicket]
GO
