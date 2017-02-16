GO

/****** Object:  Table [dbo].[EventOrganiser]    Script Date: 10/02/2017 6:37:06 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EventOrganiser](
	[EventOrganiserId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[UserId] [nvarchar](100) NOT NULL,
	[LastModifiedBy] [nvarchar](100) NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[LastModifiedDateUtc] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_EventOrganiser] PRIMARY KEY CLUSTERED 
(
	[EventOrganiserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[EventOrganiser]  WITH CHECK ADD  CONSTRAINT [FK_EventOrganiser_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[EventOrganiser] CHECK CONSTRAINT [FK_EventOrganiser_Event]
GO

