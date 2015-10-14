
GO

/****** Object:  Table [dbo].[EventBooking]    Script Date: 14/10/2015 5:08:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventBooking](
	[EventBookingId] [int] IDENTITY(1,1) NOT NULL,
	[EventId][int] NOT NULL,
	[UserId] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](100) NULL,
	[LastName] [nvarchar](100) NULL,
	[Phone][varchar](15) NULL,
	[PostCode][varchar](4) NULL,
	[TotalCost][money] NULL,
	[PaymentMethod][varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedDateTimeUtc] [datetime] NULL,
 CONSTRAINT [PK_dbo.EventBooking] PRIMARY KEY CLUSTERED 
(
	[EventBookingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 
GO



/****** Object:  Table [dbo].[EventBookingTicket]    Script Date: 14/10/2015 8:31:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EventBookingTicket](
	[EventBookingTicketId] [int] IDENTITY(1,1) NOT NULL,
	[EventBookingId] [int] NOT NULL,
	[EventTicketId] [int] NOT NULL,
	[TicketName][varchar](100) NULL,
	[Quantity] [int] NOT NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedDateTimeUtc] [datetime] NULL,
 CONSTRAINT [PK_EventBookingTicket] PRIMARY KEY CLUSTERED 
(
	[EventBookingTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


ALTER TABLE [dbo].[EventBooking]  WITH CHECK ADD  CONSTRAINT [FK_EventBooking_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO
ALTER TABLE [dbo].[EventBooking] CHECK CONSTRAINT [FK_EventBooking_Event]
GO



ALTER TABLE [dbo].[EventBookingTicket]  WITH CHECK ADD  CONSTRAINT [FK_EventBookingTicket_EventBooking] FOREIGN KEY([EventBookingId])
REFERENCES [dbo].[EventBooking] ([EventBookingId])
GO
ALTER TABLE [dbo].[EventBookingTicket] CHECK CONSTRAINT [FK_EventBookingTicket_EventBooking]
GO

ALTER TABLE [dbo].[EventBookingTicket]  WITH CHECK ADD  CONSTRAINT [FK_EventBookingTicket_EventTicket] FOREIGN KEY([EventTicketId])
REFERENCES [dbo].[EventTicket] ([EventTicketId])
GO
ALTER TABLE [dbo].[EventBookingTicket] CHECK CONSTRAINT [FK_EventBookingTicket_EventTicket]
GO

