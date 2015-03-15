SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AdBookingOrder](
	[AdBookingOrderId] [int] IDENTITY(1,1) NOT NULL,
	[AdBookingId] [int] NOT NULL,
	[OrderName] [varchar](50) NULL,
	[BookReference] [varchar](20) NULL,
	[OrderTotal] [money] NOT NULL,
	[PublicationId] [int] NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_AdBookingOrder] PRIMARY KEY CLUSTERED 
(
	[AdBookingOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



/****** Object:  Table [dbo].[AdBookingOrderItem]    Script Date: 15/03/2015 8:40:33 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[AdBookingOrderItem](
	[AdBookingOrderItemId] [int] IDENTITY(1,1) NOT NULL,
	[AdBookingOrderId] [int] NOT NULL,
	[ItemName] [varchar](100) NOT NULL,
	[Amount] [money] NOT NULL,
	[Quantity] [int] NOT NULL,
	[ItemTotal] [money] NULL,
	[Currency] [varchar](3) NULL,
 CONSTRAINT [PK_AdBookingOrderItem] PRIMARY KEY CLUSTERED 
(
	[AdBookingOrderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

/****** Foreign key constraints ******/

ALTER TABLE [dbo].[AdBookingOrderItem]  WITH CHECK ADD  CONSTRAINT [FK_AdBookingOrderItem_AdBookingOrder] FOREIGN KEY([AdBookingOrderId])
REFERENCES [dbo].[AdBookingOrder] ([AdBookingOrderId])
GO

ALTER TABLE [dbo].[AdBookingOrderItem] CHECK CONSTRAINT [FK_AdBookingOrderItem_AdBookingOrder]
GO


ALTER TABLE [dbo].[AdBookingOrder]  WITH CHECK ADD  CONSTRAINT [FK_AdBookingOrder_AdBooking] FOREIGN KEY([AdBookingId])
REFERENCES [dbo].[AdBooking] ([AdBookingId])
GO

ALTER TABLE [dbo].[AdBookingOrder] CHECK CONSTRAINT [FK_AdBookingOrder_AdBooking]
GO

