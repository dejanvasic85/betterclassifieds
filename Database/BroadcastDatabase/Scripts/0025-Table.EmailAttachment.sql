USE [Broadcast]
GO

/****** Object:  Table [dbo].[EmailAttachment]    Script Date: 29/12/2015 12:22:28 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EmailAttachment](
	[EmailAttachmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmailDeliveryId] [bigint] NOT NULL,
	[FileName] [varchar](100) NULL,
	[ContentType] [varchar](50) NULL,
	[AttachmentContent] [varbinary](max) NULL,
 CONSTRAINT [PK_EmailAttachment] PRIMARY KEY CLUSTERED 
(
	[EmailAttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[EmailAttachment]  WITH CHECK ADD  CONSTRAINT [FK_EmailAttachment_EmailDelivery] FOREIGN KEY([EmailDeliveryId])
REFERENCES [dbo].[EmailDelivery] ([EmailDeliveryId])
GO

ALTER TABLE [dbo].[EmailAttachment] CHECK CONSTRAINT [FK_EmailAttachment_EmailDelivery]
GO

