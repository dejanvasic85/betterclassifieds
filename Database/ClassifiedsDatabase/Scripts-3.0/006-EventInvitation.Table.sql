
/****** Object:  Table [dbo].[EventInvitation]    Script Date: 13/05/2016 9:23:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EventInvitation](
	[EventInvitationId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserNetworkId] [int] NOT NULL,
	[EventId] [int] NOT NULL,
	[Token] [varchar](10) NULL,
	[ConfirmedDate] [datetime] NULL,
	[ConfirmedDateUtc] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedDateUtc] [datetime] NULL,
 CONSTRAINT [PK_UserNetworkEvent] PRIMARY KEY CLUSTERED 
(
	[EventInvitationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EventInvitation]  WITH CHECK ADD  CONSTRAINT [FK_EventInvitation_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[EventInvitation] CHECK CONSTRAINT [FK_EventInvitation_Event]
GO

ALTER TABLE [dbo].[EventInvitation]  WITH CHECK ADD  CONSTRAINT [FK_EventInvitation_UserNetwork] FOREIGN KEY([UserNetworkId])
REFERENCES [dbo].[UserNetwork] ([UserNetworkId])
GO

ALTER TABLE [dbo].[EventInvitation] CHECK CONSTRAINT [FK_EventInvitation_UserNetwork]
GO

CREATE INDEX index_Token ON EventInvitation (Token)