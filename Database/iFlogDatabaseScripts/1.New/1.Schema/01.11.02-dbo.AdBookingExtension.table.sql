SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdBookingExtension]') AND type in (N'U'))
BEGIN

CREATE TABLE [dbo].[AdBookingExtension](
	[AdBookingExtensionId] [int] IDENTITY(1,1) NOT NULL,
	[AdBookingId] [int] NOT NULL,
	[Insertions] [int] NULL,
	[ExtensionPrice] [money] NULL,
	[Status] [int] NOT NULL,
	[LastModifiedUserId] [varchar](50) NULL,
	[LastModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_AdBookingExtension] PRIMARY KEY CLUSTERED 
(
	[AdBookingExtensionId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

End
GO

SET ANSI_PADDING OFF
GO
