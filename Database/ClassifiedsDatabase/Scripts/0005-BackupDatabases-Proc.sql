SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[spBackupDatabases]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		Dejan Vasic
-- Create date: 28-12-2013
-- Modifications
-- Date			Author			Description
-- ==================================================================
CREATE proc [dbo].[spBackupDatabases]

	-- Add the parameters for the stored procedure here
	@BackupLocation		varchar(500) = ''C:\Backup\''

AS

	SET NOCOUNT ON;

	DECLARE @name VARCHAR(50) -- database name 
	DECLARE @path VARCHAR(256) -- path for backup files 
	DECLARE @fileName VARCHAR(256) -- filename for backup 
	DECLARE @fileDate VARCHAR(20) -- used for file name

	SET		@path = @BackupLocation

	DECLARE db_cursor CURSOR FOR 
	SELECT name
	FROM master.dbo.sysdatabases
	WHERE name NOT IN (''master'',''model'',''msdb'',''tempdb'') 

	OPEN db_cursor  
	FETCH NEXT FROM db_cursor INTO @name

	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		   SET @fileName = @path + @name + ''.bak'' 
		   BACKUP DATABASE @name TO DISK = @fileName WITH NOFORMAT, INIT, SKIP, NOREWIND, NOUNLOAD,  STATS = 10
		   --WITH COMPRESSION
 
		   FETCH NEXT FROM db_cursor INTO @name
	END  
 
	CLOSE db_cursor  
	DEALLOCATE db_cursor

' 
END
GO