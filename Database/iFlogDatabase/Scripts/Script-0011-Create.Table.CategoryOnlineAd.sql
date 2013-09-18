SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MainCategoryOnlineAd]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[MainCategoryOnlineAd](
	[MainCategoryOnlineAdId] [int] IDENTITY(1,1) NOT NULL,
	[MainCategoryId] [int] NOT NULL,
	[OnlineAdTag] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[LastModifiedBy] [nvarchar](100) NULL,
	[LastModifiedDate] [datetime] NULL
 CONSTRAINT [PK_CategoryOnlineAd] PRIMARY KEY CLUSTERED 
(
	[MainCategoryOnlineAdId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

ALTER TABLE [dbo].[MainCategoryOnlineAd]  WITH CHECK ADD  CONSTRAINT [FK_MainCategoryOnlineAd_MainCategory] FOREIGN KEY([MainCategoryId])
REFERENCES [dbo].[MainCategory] ([MainCategoryId])
GO

ALTER TABLE [dbo].[MainCategoryOnlineAd] ADD  CONSTRAINT [DF_MainCategoryOnlineAd_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO