
/****** Object:  Index [IX_Registration_Token]    Script Date: 6/04/2014 10:02:16 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Registration_Token] ON [dbo].[Registration]
(
	[Token] ASC
)
INCLUDE ( 	[Username]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


