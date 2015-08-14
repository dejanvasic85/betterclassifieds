
GO

/****** Object:  Table [dbo].[Event]    Script Date: 14/08/2015 5:08:07 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Event](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[Location] [nvarchar](max) NULL,
	[LocationLatitude] [decimal](18, 2) NULL,
	[LocationLongitude] [decimal](18, 2) NULL,
	[EventStartDate] [datetime] NULL,
	[EventEndDate] [datetime] NULL,
 CONSTRAINT [PK_dbo.Event] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO



/****** Object:  Table [dbo].[EventTicket]    Script Date: 28/07/2015 8:31:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EventTicket](
	[TicketId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[TicketName] [varchar](100) NOT NULL,
	[AvailableQuantity] [int] NOT NULL,
	[Price] [money] NULL,
 CONSTRAINT [PK_EventTicket] PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EventTicket]  WITH CHECK ADD  CONSTRAINT [FK_EventTicket_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[EventTicket] CHECK CONSTRAINT [FK_EventTicket_Event]
GO


