<# Variables Provided by Octopus 
	$BackupDatabase = $true
	$BackupDatabasePath = "D:\"
	$RestoreDatabase = $true
	$DropCreateDatabase = $true
#>

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptPath
[xml]$appConfig = Get-Content .\LogDatabase.exe.config

# Use connection builder to get variables we need for backup and stuff
$sqlConnectionBuilder = New-Object -TypeName System.Data.SqlClient.SqlConnectionStringBuilder -ArgumentList $appConfig.configuration.connectionStrings.add.connectionString

$db = Invoke-SqlCmd -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$($sqlConnectionBuilder.InitialCatalog)';" -ServerInstance $sqlConnectionBuilder.DataSource

# Drop Create Database
if ( $DropCreateDatabase -eq $true -and $db -ne $null ) {
    Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource -ErrorAction SilentlyContinue
	Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource -ErrorAction SilentlyContinue
    
    Write-Host "Dropping database..."        
    Invoke-Sqlcmd -Query "DROP DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource 
    Write-Host "Creating Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource 
	# Set the DB Variable so it's not created again
	$db = "DatabaseCreated"
}

if ( $db -eq $null ) {
	Write-Host "Creating Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource
}

Set-Location $scriptPath

& .\LogDatabase.exe | Write-Host