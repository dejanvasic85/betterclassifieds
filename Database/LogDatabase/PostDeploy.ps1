<# Variables Provided by Octopus 
	$BackupDatabase = $true
	$BackupDatabasePath = "D:\"
	$RestoreDatabase = $true
	$DropCreateDatabase = $true
#>

Function Run-Sql{
    param([Parameter(Mandatory=$false)][string] $Query, 
          [Parameter(Mandatory=$false)][string] $InputFile )
    
	$sqlArgs = @{}

    if ( $InputFile ) {$sqlArgs.InputFile = $InputFile}
    if ( $Query ) {
		$sqlArgs.Query = $Query

		if ( $false -eq ($Query -match "DROP DATABASE" -or $Query -match "CREATE DATABASE"  -or $Query -match "ALTER DATABASE") ) { 
			$sqlArgs.Database = $connection.InitialCatalog
			Write-Host "Set database " $sqlArgs.Database			
		}
	}
    $sqlArgs.ServerInstance  = $connection.DataSource
    $sqlArgs.QueryTimeout = 0
	
    if ($connection.IntegratedSecurity -eq $false) {
        $sqlArgs.U = $connection.UserID
        $sqlArgs.P = $connection.Password 
    }

	Write-Host "Executing: $($Query)"

    return Invoke-Sqlcmd @sqlArgs
}

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptPath
[xml]$appConfig = Get-Content .\LogDatabase.exe.config

# Use connection builder to get variables we need for backup and stuff
$connection = New-Object -TypeName System.Data.SqlClient.SqlConnectionStringBuilder -ArgumentList $appConfig.configuration.connectionStrings.add.connectionString

$db = Run-Sql -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$($connection.InitialCatalog)';"

# Drop Create Database
if ( $DropCreateDatabase -eq $true -and $db -ne $null ) {
    Run-Sql "ALTER DATABASE [$($connection.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ErrorAction SilentlyContinue
	Run-Sql "ALTER DATABASE [$($connection.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ErrorAction SilentlyContinue
    
    Write-Host "Dropping database..."
    Run-Sql -Query "DROP DATABASE $($connection.InitialCatalog)"
	
	# Set the DB Variable so it's created again
	$db = $null
}

# Create database because it does not exist or it was dropped
if ( $db -eq $null ) {
	
	$newDatabaseName = "$($connection.InitialCatalog)"
	$newLogicalName = "Logs"
	$newMdfFile = "$($SqlFilesPath)$($connection.InitialCatalog).mdf"
	$newLogFile = "$($SqlFilesPath)$($connection.InitialCatalog)_log.ldf"

	Write-Host "Creating Database:  $newDatabaseName"
	Write-Host "Logical Name: $newLogicalName"
	Write-Host "MdfFile: $newMdfFile"
	Write-Host "LogFile: $newLogFile"

    Run-Sql -Query @"
	CREATE DATABASE $($newDatabaseName)	
	CONTAINMENT = NONE ON  PRIMARY ( NAME = N'$($newLogicalName)', FILENAME = N'$($newMdfFile)' , SIZE = 5120KB , FILEGROWTH = 1024KB )  
	LOG ON ( NAME = N'$($newLogicalName)_log', FILENAME = N'$($newLogFile)' , SIZE = 1024KB , FILEGROWTH = 10%) 
"@ 

}

Set-Location $scriptPath

& .\LogDatabase.exe | Write-Host