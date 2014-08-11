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
$connection = New-Object -TypeName System.Data.SqlClient.SqlConnectionStringBuilder -ArgumentList $appConfig.configuration.connectionStrings.add.connectionString

$db = Invoke-SqlCmd -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$($connection.InitialCatalog)';" -ServerInstance $connection.DataSource -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password

# Drop Create Database
if ( $DropCreateDatabase -eq $true -and $db -ne $null ) {
    Invoke-Sqlcmd "ALTER DATABASE [$($connection.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $connection.DataSource -ErrorAction SilentlyContinue -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
	Invoke-Sqlcmd "ALTER DATABASE [$($connection.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $connection.DataSource -ErrorAction SilentlyContinue -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
    
    Write-Host "Dropping database..."        
    Invoke-Sqlcmd -Query "DROP DATABASE $($connection.InitialCatalog)" -ServerInstance $connection.DataSource  -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
    Write-Host "Creating Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($connection.InitialCatalog)" -ServerInstance $connection.DataSource  -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
	# Set the DB Variable so it's not created again
	$db = "DatabaseCreated"
}

if ( $db -eq $null ) {
	Write-Host "Creating Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($connection.InitialCatalog)" -ServerInstance $connection.DataSource -QueryTimeout 0 -Username $connection.UserID -Password $connection.Password
}

Set-Location $scriptPath

& .\LogDatabase.exe | Write-Host