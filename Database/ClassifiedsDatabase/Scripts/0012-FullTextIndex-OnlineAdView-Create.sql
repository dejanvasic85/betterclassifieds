
IF NOT EXISTS (SELECT * FROM sysfulltextcatalogs ftc WHERE ftc.name = N'OnlineAdSearch')
begin

	CREATE FULLTEXT CATALOG [OnlineAdSearch] WITH ACCENT_SENSITIVITY = OFF


	CREATE FULLTEXT INDEX ON [dbo].[OnlineAdView](
	[AreaTitle] LANGUAGE [English], 
	[BookReference] LANGUAGE [English], 
	[CategoryTitle] LANGUAGE [English], 
	[Description] LANGUAGE [English], 
	[Heading] LANGUAGE [English], 
	[LocationTitle] LANGUAGE [English], 
	[OnlineAdTag] LANGUAGE [English], 
	[UserId] LANGUAGE [English])
	KEY INDEX [ClusteredIndex-OnlineAdId]ON ([OnlineAdSearch], FILEGROUP [PRIMARY])
	WITH (CHANGE_TRACKING = AUTO, STOPLIST = SYSTEM)

end

GO