
/****** Object:  Table [dbo].[Event]    Script Date: 28/07/2015 8:28:13 PM ******/
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
	[OnlineAdId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Event] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_OnlineAd] FOREIGN KEY([OnlineAdId])
REFERENCES [dbo].[OnlineAd] ([OnlineAdId])
GO

ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_OnlineAd]
GO