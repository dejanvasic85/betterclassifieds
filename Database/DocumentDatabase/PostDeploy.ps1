<# Variables Provided by Octopus 
	$BackupDatabase = $true
	$BackupDatabasePath = "D:\"
	$RestoreDatabase = $true
	$DropCreateDatabase = $true
#>

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptPath
[xml]$appConfig = Get-Content .\DocumentDatabase.exe.config

# Use connection builder to get variables we need for backup and stuff
$sqlConnectionBuilder = New-Object -TypeName System.Data.SqlClient.SqlConnectionStringBuilder -ArgumentList $appConfig.configuration.connectionStrings.add.connectionString

$db = Invoke-SqlCmd -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$($sqlConnectionBuilder.InitialCatalog)';" -ServerInstance $sqlConnectionBuilder.DataSource

# Backup-SqlDatabase 
if ( $BackupDatabase -eq $true ){
	$backupFile = $BackupDatabaseLocation + $sqlConnectionBuilder.InitialCatalog + ".bak"    
    Write-Host "Backing Up..."
    Backup-SqlDatabase -ServerInstance $sqlConnectionBuilder.DataSource -Database $sqlConnectionBuilder.InitialCatalog -BackupFile $backupFile -BackupAction Database -Initialize
    Set-Location $scriptPath
}

# Restore-SqlDatabase
if ( $RestoreDatabase -eq $true ){	
    $backupFile = $BackupDatabasePath + $sqlConnectionBuilder.InitialCatalog + ".bak"
	Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource -ErrorAction SilentlyContinue
	Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource -ErrorAction SilentlyContinue

	$mdfRelocate = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile -ArgumentList ("DocumentStorage", "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\$($sqlConnectionBuilder.InitialCatalog).mdf")
    $logRelocate = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile -ArgumentList ("DocumentStorage_log", "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\$($sqlConnectionBuilder.InitialCatalog)_log.ldf")

	Write-Host "Restoring Database $($sqlConnectionBuilder.InitialCatalog) from $($backupFile) ..."
	
    Restore-SqlDatabase -ServerInstance $sqlConnectionBuilder.DataSource -Database $sqlConnectionBuilder.InitialCatalog -ReplaceDatabase -BackupFile $backupFile -RelocateFile ($mdfRelocate, $logRelocate)
}

$db = Invoke-SqlCmd -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$($sqlConnectionBuilder.InitialCatalog)';" -ServerInstance $sqlConnectionBuilder.DataSource

# Drop Create Database
if ( $DropCreateDatabase -eq $true -and $db -ne $null ) {
    Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource -ErrorAction SilentlyContinue
	Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource -ErrorAction SilentlyContinue
	
    Write-Host "Dropping database..."        
    Invoke-Sqlcmd -Query "DROP DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource
    
	Write-Host "Creating Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource

	$db = "DatabaseCreated"
}

if ( $db -eq $null ) {
	Write-Host "Creating Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource	
}

Set-Location $scriptPath

& .\DocumentDatabase.exe | Write-Host