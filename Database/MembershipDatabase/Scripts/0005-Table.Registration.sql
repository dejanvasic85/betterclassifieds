
/****** Object:  Table [dbo].[Registration]    Script Date: 6/04/2014 1:41:51 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Registration](
	[RegistrationId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](256) NOT NULL,
	[Email] [nvarchar](256) NOT NULL,
	[Password] [nvarchar](256) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](50) NULL,
	[PostCode] [varchar](6) NULL,
	[Token] [nvarchar](50) NOT NULL,
	[ExpirationDate] [datetime] NULL,
	[ExpirationDateUtc] [datetime] NULL,
	[LastModifiedDate] [datetime] NULL,
	[LastModifiedDateUtc] [datetime] NULL,
 CONSTRAINT [PK_Registration] PRIMARY KEY CLUSTERED 
(
	[RegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

