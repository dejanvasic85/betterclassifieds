GO

/****** Object:  Table [dbo].[EmailTemplate]    Script Date: 29/03/2014 9:40:35 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[EmailTemplate](
	[EmailTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[DocType] [varchar](50) NOT NULL,
	[Description] [varchar](200) NULL,
	[SubjectTemplate] [varchar](100) NOT NULL,
	[BodyTemplate] [nvarchar](max) NOT NULL,
	[IsBodyHtml] [bit] NULL,
	[From] [varchar](200) NULL,
	[ParserName] [varchar](50) NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedDateUtc] [datetime] NULL,
 CONSTRAINT [PK_EmailTemplate] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


