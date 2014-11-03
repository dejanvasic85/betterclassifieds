﻿<# Variables Provided by Octopus 
	$BackupDatabase = $true
	$BackupDatabasePath = "D:\"
	$RestoreDatabase = $true
	$DropCreateDatabase = $true
	$SqlFilesPath = "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\"
#>

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptPath
[xml]$appConfig = Get-Content .\MembershipDatabase.exe.config

# Use connection builder to get variables we need for backup and stuff
$connection = New-Object -TypeName System.Data.SqlClient.SqlConnectionStringBuilder -ArgumentList $appConfig.configuration.connectionStrings.add.connectionString

$db = Invoke-SqlCmd -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$($connection.InitialCatalog)';" -ServerInstance $connection.DataSource  -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password

# Backup-SqlDatabase 
if ( $BackupDatabase -eq $true -and $db -ne $null ){
	$backupFile = $BackupDatabasePath + $connection.InitialCatalog + ".bak"    
    Write-Host "Backing Up..."
    Backup-SqlDatabase -ServerInstance $connection.DataSource -Database $connection.InitialCatalog -BackupFile $backupFile -BackupAction Database -Initialize
    Set-Location $scriptPath
}

# Restore-SqlDatabase
if ( $RestoreDatabase -eq $true ){	
    $backupFile = $BackupDatabasePath + "iFlogAppUser.bak"
	Invoke-Sqlcmd "ALTER DATABASE [$($connection.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $connection.DataSource -QueryTimeout 0  -Username $connection.UserID -Password $connection.Password -ErrorAction SilentlyContinue  
	Invoke-Sqlcmd "ALTER DATABASE [$($connection.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $connection.DataSource -QueryTimeout 0  -Username $connection.UserID -Password $connection.Password -ErrorAction SilentlyContinue

	$mdfRelocate = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile -ArgumentList ("AppUserTest", "$($SqlFilesPath)DATA\$($connection.InitialCatalog).mdf")
    $logRelocate = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile -ArgumentList ("AppUserTest_log", "$($SqlFilesPath)DATA\$($connection.InitialCatalog)_log.ldf")

	Write-Host "Restoring Database $($connection.InitialCatalog) from $($backupFile) ..."
	
    Restore-SqlDatabase -ServerInstance $connection.DataSource -Database $connection.InitialCatalog -ReplaceDatabase -BackupFile $backupFile -RelocateFile ($mdfRelocate, $logRelocate)
	$db = "DatabaseRestored"
}

# Drop Create Database
if ( $DropCreateDatabase -eq $true -and $db -ne $null ) {
    Invoke-Sqlcmd "ALTER DATABASE [$($connection.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $connection.DataSource -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password -ErrorAction SilentlyContinue
	Invoke-Sqlcmd "ALTER DATABASE [$($connection.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $connection.DataSource -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password -ErrorAction SilentlyContinue
	
    Write-Host "Dropping database..."        
    Invoke-Sqlcmd -Query "DROP DATABASE $($connection.InitialCatalog)" -ServerInstance $connection.DataSource -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
    Write-Host "Creating Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($connection.InitialCatalog)" -ServerInstance $connection.DataSource -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
	$db = "DatabaseCreated"
}

if ( $db -eq $null ) {
	Write-Host "Creating Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($connection.InitialCatalog)" -ServerInstance $connection.DataSource -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
}

# Sanitize database
if ( $SanitizeDatabase -eq $true ) {	
	Write-Host "Sanitization = Updating Membership with $($Sanitize_Email) email"
	Invoke-SqlCmd "UPDATE aspnet_Membership SET Email = '$($Sanitize_Email)', LoweredEmail = '$($Sanitize_Email)'" -ServerInstance $connection.DataSource -Database $connection.InitialCatalog -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
	Write-Host "Sanitization = Updating Profiles with $($Sanitize_Email) email"
	Invoke-SqlCmd "UPDATE UserProfile SET Email = '$($Sanitize_Email)'" -ServerInstance $connection.DataSource -Database $connection.InitialCatalog -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
}

Set-Location $scriptPath

& .\MembershipDatabase.exe | Write-Host