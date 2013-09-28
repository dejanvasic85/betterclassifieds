SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Log]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Log](
	[LogId] [uniqueidentifier] NOT NULL,
	[Category] [varchar](25) NOT NULL,
	[TransactionName] [varchar](100) NULL,
	[Application] [varchar](50) NOT NULL,
	[Domain] [varchar](50) NULL,
	[User] [varchar](50) NULL,
	[AccountId] [varchar](50) NULL,
	[Data1] [varchar](max) NULL,
	[Data2] [varchar](max) NULL,
	[DateTimeCreated] [datetime] NOT NULL,
	[ComputerName] [varchar](50) NULL,
	[IPAddress] [varchar](25) NULL,
	[SessionId] [varchar](20) NULL,
	[DateTimeUtcCreated] [datetime] NULL,
	[Browser] [varchar](500) NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO