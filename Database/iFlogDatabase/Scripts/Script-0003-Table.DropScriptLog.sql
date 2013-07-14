IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ScriptLog]') AND type in (N'U'))
begin
	DROP TABLE dbo.ScriptLog
end