GO

/****** Object:  Table [dbo].[EventPromoCode]   ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventPromoCode](
	[EventPromoCodeId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[PromoCode] [varchar](50) NOT NULL,
	[DiscountPercent][money] NULL,
	[IsDisabled] [bit] NULL,
	[CreatedDate][datetime] NULL,
	[CreatedDateUtc][datetime] NULL
	CONSTRAINT [PK_EventPromoCode] PRIMARY KEY CLUSTERED 
(
	[EventPromoCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EventPromoCode]  WITH CHECK ADD  CONSTRAINT [FK_EventPromoCode_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[EventPromoCode] CHECK CONSTRAINT [FK_EventPromoCode_Event]
GO

