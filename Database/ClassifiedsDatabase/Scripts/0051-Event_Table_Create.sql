
/****** Object:  Table [dbo].[Event]    Script Date: 21/07/2015 12:05:20 AM ******/
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

