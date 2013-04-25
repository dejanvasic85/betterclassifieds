SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ScriptLog]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ScriptLog](
	[ScriptName] [varchar](300) NOT NULL,
	[RunDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ScriptLog] PRIMARY KEY CLUSTERED 
(
	[ScriptName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_ScriptLog_RunDate]') AND parent_object_id = OBJECT_ID(N'[dbo].[ScriptLog]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ScriptLog_RunDate]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ScriptLog] ADD  CONSTRAINT [DF_ScriptLog_RunDate]  DEFAULT (getdate()) FOR [RunDate]
END

End
GO

SET ANSI_PADDING OFF
GO
