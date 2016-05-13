
/****** Object:  Table [dbo].[EventPromotionGuest]    Script Date: 13/05/2016 9:23:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EventPromotionGuest](
	[EventPromotionGuestId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserNetworkId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Token] [varchar](10) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedDateUtc] [datetime] NULL,
 CONSTRAINT [PK_UserNetworkEvent] PRIMARY KEY CLUSTERED 
(
	[EventPromotionGuestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EventPromotionGuest]  WITH CHECK ADD  CONSTRAINT [FK_EventPromotionGuest_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[EventPromotionGuest] CHECK CONSTRAINT [FK_EventPromotionGuest_Event]
GO

ALTER TABLE [dbo].[EventPromotionGuest]  WITH CHECK ADD  CONSTRAINT [FK_EventPromotionGuest_UserNetwork] FOREIGN KEY([UserNetworkId])
REFERENCES [dbo].[UserNetwork] ([UserNetworkId])
GO

ALTER TABLE [dbo].[EventPromotionGuest] CHECK CONSTRAINT [FK_EventPromotionGuest_UserNetwork]
GO

