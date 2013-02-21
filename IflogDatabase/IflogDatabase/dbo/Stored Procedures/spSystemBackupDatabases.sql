﻿-- =============================================
-- Author:		Dejan Vasic
-- Create date: 11-Sep-09
-- Modifications
-- Date			Author			Description
-- =============================================
CREATE PROCEDURE [dbo].[spSystemBackupDatabases]
	@path NVARCHAR(200)
AS
BEGIN
	DECLARE @name VARCHAR(50) -- database name 
	DECLARE @fileName VARCHAR(256) -- filename for backup 
	DECLARE @fileDate VARCHAR(20) -- used for file name

	SELECT @fileDate = CONVERT(VARCHAR(20),GETDATE(),112)

	DECLARE db_cursor CURSOR FOR 
	SELECT name 
	FROM master.dbo.sysdatabases 
	WHERE name NOT IN ('master','model','msdb','tempdb') 

	OPEN db_cursor  
	FETCH NEXT FROM db_cursor INTO @name  

	WHILE @@FETCH_STATUS = 0  
	BEGIN  
		   SET @fileName = @path + @name +  '.BAK' 
		   BACKUP DATABASE @name TO DISK = @fileName 

		   FETCH NEXT FROM db_cursor INTO @name  
	END  

	CLOSE db_cursor  
	DEALLOCATE db_cursor
	
END