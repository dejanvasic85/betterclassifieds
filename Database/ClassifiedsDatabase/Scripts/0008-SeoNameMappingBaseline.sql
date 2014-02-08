SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SeoMapping]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[SeoMapping](
	[SeoNameId] [int] IDENTITY(1,1) NOT NULL,
	[SeoName] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](150) NULL,
	[CategoryIds] varchar(20) NULL,
	[ParentCategoryIds] int NULL,
	[LocationIds] varchar(20) NULL,
	[AreaIds] varchar(20) NULL,
	[SearchTerm] [nvarchar](100) NULL,
 CONSTRAINT [PK_SeoMapping] PRIMARY KEY CLUSTERED 
(
	[SeoNameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END
GO

ALTER TABLE [dbo].[MainCategory]
    ADD [SeoName] NVARCHAR (20) NULL;

	go