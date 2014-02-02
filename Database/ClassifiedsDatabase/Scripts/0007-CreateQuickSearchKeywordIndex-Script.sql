DROP INDEX [NonClusteredIndex-QuickSearch] ON [dbo].[OnlineAd]
GO

/****** Object:  Index [NonClusteredIndex-QuickSearch]    Script Date: 2/02/2014 10:34:36 PM ******/
CREATE NONCLUSTERED INDEX [NonClusteredIndex-QuickSearch] ON [dbo].[OnlineAd]
(
	[OnlineAdId] ASC
)
INCLUDE ( 	[Heading],
	[Description]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


