
GO

/****** Object:  Table [dbo].[EmailDelivery]    Script Date: 29/03/2014 9:36:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EmailDelivery](
	[EmailDeliveryId] [bigint] IDENTITY(1,1) NOT NULL,
	[BroadcastId] [uniqueidentifier] NOT NULL,
	[DocType] [varchar](50) NULL,
	[To] [varchar](200) NOT NULL,
	[Cc] [varchar](200) NULL,
	[Bcc] [varchar](200) NULL,
	[From] [varchar](200) NOT NULL,
	[Subject] [varchar](200) NULL,
	[Body] [nvarchar](max) NULL,
	[IsBodyHtml] [bit] NULL,
	[Attempts] [int] NOT NULL,
	[LastAttemptDate] [datetime] NULL,
	[LastAttemptDateUtc] [datetime] NULL,
	[SentDate] [datetime] NULL,
	[SentDateUtc] [datetime] NULL,
	[LastErrorMessage] [varchar](100) NULL,
	[LastErrorStacktrace] [varchar](max) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedDateUtc] [datetime] NULL,
 CONSTRAINT [PK_EmailDelivery] PRIMARY KEY CLUSTERED 
(
	[EmailDeliveryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EmailDelivery] ADD  CONSTRAINT [DF_EmailDelivery_Attempts]  DEFAULT ((0)) FOR [Attempts]
GO

GO

/****** Object:  Index [IX_EmailDelivery_DocType]    Script Date: 29/03/2014 9:39:09 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmailDelivery_DocType] ON [dbo].[EmailDelivery]
(
	[DocType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


GO

/****** Object:  Index [IX_EmailDelivery_SentDate]    Script Date: 29/03/2014 9:39:50 PM ******/
CREATE NONCLUSTERED INDEX [IX_EmailDelivery_SentDate] ON [dbo].[EmailDelivery]
(
	[SentDate] DESC,
	[SentDateUtc] DESC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

