/****** Object:  Table [dbo].[EventTicketField]    Script Date: 26/11/2015 10:06:32 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EventTicketField](
	[EventTicketFieldId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NOT NULL,
	[FieldName] [varchar](50) NOT NULL,
	[IsRequired] [bit] NULL,
 CONSTRAINT [PK_EventTicketField] PRIMARY KEY CLUSTERED 
(
	[EventTicketFieldId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EventTicketField]  WITH CHECK ADD  CONSTRAINT [FK_EventTicketField_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO

ALTER TABLE [dbo].[EventTicketField] CHECK CONSTRAINT [FK_EventTicketField_Event]
GO

