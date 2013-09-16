SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TutorAd]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[TutorAd](
	[TutorAdId] [int] IDENTITY(1,1) NOT NULL,
	[OnlineAdId] [int] NOT NULL,
	[Subjects] [nvarchar](500) NULL,
	[AgeGroupMin] [int] NULL,
	[AgeGroupMax] [int] NULL,
	[ExpertiseLevel] [nvarchar](100) NULL,
	[TravelOption] [nvarchar](50) NULL,
	[PricingOption] [nvarchar](100) NULL,
	[WhatToBring] [nvarchar](100) NULL,
	[Objective] [nvarchar](200) NULL
 CONSTRAINT [PK_TutorAd] PRIMARY KEY CLUSTERED 
(
	[TutorAdId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

ALTER TABLE [dbo].[TutorAd]  WITH CHECK ADD  CONSTRAINT [FK_TutorAd_OnlineAd] FOREIGN KEY([OnlineAdId])
REFERENCES [dbo].[OnlineAd] ([OnlineAdId])
GO