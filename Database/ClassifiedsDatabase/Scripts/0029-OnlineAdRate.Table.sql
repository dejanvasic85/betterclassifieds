
/****** Object:  Table [dbo].[OnlineAdRate]    Script Date: 6/10/2014 10:06:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OnlineAdRate](
	[OnlineAdRateId] [int] IDENTITY(1,1) NOT NULL,
	[MainCategoryId] [int] NULL,
	[MinimumCharge] [money] NOT NULL,
 CONSTRAINT [PK_OnlineRate] PRIMARY KEY CLUSTERED 
(
	[OnlineAdRateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[OnlineAdRate]  WITH CHECK ADD  CONSTRAINT [FK_OnlineAdRate_MainCategory] FOREIGN KEY([MainCategoryId])
REFERENCES [dbo].[MainCategory] ([MainCategoryId])
GO

ALTER TABLE [dbo].[OnlineAdRate] CHECK CONSTRAINT [FK_OnlineAdRate_MainCategory]
GO

