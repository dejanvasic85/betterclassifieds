
GO

/****** Object:  Index [IX_DateTimeCreated]    Script Date: 09/28/2013 14:15:38 ******/
CREATE NONCLUSTERED INDEX [IX_DateTimeCreated] ON [dbo].[Log] 
(
	[DateTimeCreated] ASC,
	[DateTimeUtcCreated] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO

