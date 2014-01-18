Import-Module "sqlps" -DisableNameChecking

Set-Location $scriptPath

<# Variables Provided by Octopus #>
$BackupDatabase = $false
$BackupDatabasePath = "D:\"
$RestoreDatabase = $true
$DropCreateDatabase = $false

[xml]$appConfig = Get-Content .\ClassifiedsDatabase.exe.config
$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition

# Use connection builder to get variables we need for backup and stuff
$sqlConnectionBuilder = New-Object -TypeName System.Data.SqlClient.SqlConnectionStringBuilder -ArgumentList $appConfig.configuration.connectionStrings.add.connectionString
$backupFile = $BackupDatabaseLocation + $sqlConnectionBuilder.InitialCatalog + ".bak"

Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource

Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource

# Backup-SqlDatabase 
if ( $BackupDatabase -eq $true ){
    
    Write-Host "Backing Up..."
    Backup-SqlDatabase -ServerInstance $sqlConnectionBuilder.DataSource -Database $sqlConnectionBuilder.InitialCatalog -BackupFile $backupFile -BackupAction Database -Initialize
    Set-Location $scriptPath
}

# Restore-SqlDatabase
if ( $RestoreDatabase -eq $true ){
    Write-Host "Restoring ..."
    Restore-SqlDatabase -ServerInstance $sqlConnectionBuilder.DataSource -Database $sqlConnectionBuilder.InitialCatalog -BackupFile $backupFile -ReplaceDatabase
}

# Drop Create Database
if ( $DropCreateDatabase -eq $true ) {
    $db = Invoke-SqlCmd -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$($sqlConnectionBuilder.InitialCatalog)';" -ServerInstance $sqlConnectionBuilder.DataSource
    if ( $db -ne $null ){
        Write-Host "Dropping database..."        
        Invoke-Sqlcmd -Query "DROP DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource
    }
    Write-Host "Createing Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource
}

# Execute upgrade script
& .\ClassifiedsDatabase.exe