﻿<# Variables Provided by Octopus 
	$BackupDatabase = $true
	$BackupDatabasePath = "D:\"
	$RestoreDatabase = $true
	$DropCreateDatabase = $true
	$SqlFilesPath = "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\"
#>

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptPath
[xml]$appConfig = Get-Content .\DocumentDatabase.exe.config

# Use connection builder to get variables we need for backup and stuff
$connection = New-Object -TypeName System.Data.SqlClient.SqlConnectionStringBuilder -ArgumentList $appConfig.configuration.connectionStrings.add.connectionString

$db = Invoke-SqlCmd -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$($connection.InitialCatalog)';" -ServerInstance $connection.DataSource -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password

# Backup-SqlDatabase 
if ( $BackupDatabase -eq $true -and $db -ne $null ){
	$backupFile = $BackupDatabasePath + $connection.InitialCatalog + ".bak"    
    Write-Host "Backing Up..."
    Backup-SqlDatabase -ServerInstance $connection.DataSource -Database $connection.InitialCatalog -BackupFile $backupFile -BackupAction Database -Initialize
    Set-Location $scriptPath
}

# Restore-SqlDatabase
if ( $RestoreDatabase -eq $true ){	
	$backupFile = $BackupDatabasePath + "$($Brand)_DocumentStorage.bak"
	Invoke-Sqlcmd "ALTER DATABASE [$($connection.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $connection.DataSource -ErrorAction SilentlyContinue -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
	Invoke-Sqlcmd "ALTER DATABASE [$($connection.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $connection.DataSource -ErrorAction SilentlyContinue -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password

	$mdfRelocate = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile -ArgumentList ("$($DocumentStorageDbFileName)", "$($SqlFilesPath)DATA\$($connection.InitialCatalog).mdf")
    $logRelocate = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile -ArgumentList ("$($DocumentStorageDbFileName)_log", "$($SqlFilesPath)DATA\$($connection.InitialCatalog)_log.ldf")

	Write-Host "Restoring Database $($connection.InitialCatalog) from $($backupFile) ..."
	
    Restore-SqlDatabase -ServerInstance $connection.DataSource -Database $connection.InitialCatalog -ReplaceDatabase -BackupFile $backupFile -RelocateFile ($mdfRelocate, $logRelocate)
	$db = "DatabaseRestored"
}

# Drop Create Database
if ( $DropCreateDatabase -eq $true -and $db -ne $null ) {
    Invoke-Sqlcmd "ALTER DATABASE [$($connection.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $connection.DataSource -ErrorAction SilentlyContinue -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
	Invoke-Sqlcmd "ALTER DATABASE [$($connection.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $connection.DataSource -ErrorAction SilentlyContinue -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
	
    Write-Host "Dropping database..."        
    Invoke-Sqlcmd -Query "DROP DATABASE $($connection.InitialCatalog)" -ServerInstance $connection.DataSource -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
    
	$db = $null
}


# Create database because it does not exist or it was dropped
if ( $db -eq $null ) {
	Write-Host $SqlFilesPath
	return;
	$newDatabaseName = "$($connection.InitialCatalog)"
	$newLogicalName = "DocumentStorage"
	$newMdfFile = "$($SqlFilesPath)$($connection.InitialCatalog).mdf"
	$newLogFile = "$($SqlFilesPath)$($connection.InitialCatalog)_log.ldf"

	Write-Host "Creating Database:  $newDatabaseName"
	Write-Host "Logical Name: $newLogicalName"
	Write-Host "MdfFile: $newMdfFile"
	Write-Host "LogFile: $newLogFile"

    Invoke-Sqlcmd -Query @"
	CREATE DATABASE $($newDatabaseName)	
	CONTAINMENT = NONE ON  PRIMARY ( NAME = N'Classifieds', FILENAME = N'$($newMdfFile)' , SIZE = 5120KB , FILEGROWTH = 1024KB )  
	LOG ON ( NAME = N'$($newLogicalName)_log', FILENAME = N'$($newLogFile)' , SIZE = 1024KB , FILEGROWTH = 10%) 
"@  -ServerInstance $connection.DataSource -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password


}

Set-Location $scriptPath

& .\DocumentDatabase.exe | Write-Host