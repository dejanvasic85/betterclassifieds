SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserNetwork]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[UserNetwork](
	[UserNetworkId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](50) NULL,
	[UserNetworkEmail] [nvarchar](max) NULL,
	[LastModifiedDate] [datetime] NULL,
	[IsUserNetworkActive] [bit] NULL
CONSTRAINT [PK_UserNetwork] PRIMARY KEY CLUSTERED 
(
	[UserNetworkId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END