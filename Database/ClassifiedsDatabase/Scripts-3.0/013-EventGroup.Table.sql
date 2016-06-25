
/****** Object:  Table [dbo].[EventGroup]    Script Date: 25/06/2016 8:11:16 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EventGroup](
	[EventGroupId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[GroupName] [varchar](50) NOT NULL,
	[MaxGuests] [int] NULL,
	[CreatedDateTime] [datetime] NULL,
	[CreatedDateTimeUtc] [datetime] NULL,
	[CreatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_EventGroup] PRIMARY KEY CLUSTERED 
(
	[EventGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EventGroup]  WITH CHECK ADD  CONSTRAINT [FK_EventGroup_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[EventGroup] CHECK CONSTRAINT [FK_EventGroup_Event]
GO

