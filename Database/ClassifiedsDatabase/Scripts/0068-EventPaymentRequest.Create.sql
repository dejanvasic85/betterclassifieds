
/****** Object:  Table [dbo].[EventPaymentRequest]    Script Date: 14/12/2015 10:25:59 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EventPaymentRequest](
	[EventPaymentRequestId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[RequestedAmount] [money] NOT NULL,
	[PaymentMethod] [varchar](50) NOT NULL,
	[CreatedByUser] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedDateUtc] [datetime] NULL,
	[IsPaymentProcessed] [bit] NULL,
	[PaymentProcessedDate] [datetime] NULL,
	[PaymentProcessedDateUtc] [datetime] NULL,
	[PaymentProcessedBy] [nvarchar](50) NULL,
	[PaymentReference] [varchar](100) NULL,
 CONSTRAINT [PK_EventPaymentRequest] PRIMARY KEY CLUSTERED 
(
	[EventPaymentRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EventPaymentRequest]  WITH CHECK ADD  CONSTRAINT [FK_EventPaymentRequest_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[EventPaymentRequest] CHECK CONSTRAINT [FK_EventPaymentRequest_Event]
GO

