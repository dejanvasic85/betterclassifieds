<# Variables Provided by Octopus 
	$BackupDatabase = $true
	$BackupDatabasePath = "D:\"
	$RestoreDatabase = $true
	$DropCreateDatabase = $true
	$SqlFilesPath = "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\"
#>


Function Run-Sql{

    param([Parameter(Mandatory=$false)][string] $Query, 
          [Parameter(Mandatory=$false)][string] $InputFile )

    $sqlArgs = @{}

    if ($InputFile) {$sqlArgs.InputFile = $InputFile}
    if ($Query) {$sqlArgs.Query = $Query}

    $sqlArgs.ServerInstance  = $connection.DataSource
    $sqlArgs.QueryTimeout = 0
	$sqlArgs.Database = $connection.InitialCatalog

    if ($connection.IntegratedSecurity -eq $false) {
        $sqlArgs.U = $connection.UserID
        $sqlArgs.P = $connection.Password 
    }

    return Invoke-Sqlcmd @sqlArgs
}

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptPath
[xml]$appConfig = Get-Content .\DocumentDatabase.exe.config

# Use connection builder to get variables we need for backup and stuff
$connection = New-Object -TypeName System.Data.SqlClient.SqlConnectionStringBuilder -ArgumentList $appConfig.configuration.connectionStrings.add.connectionString

$db = Run-Sql -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$($connection.InitialCatalog)';"

# Backup-SqlDatabase 
if ( $BackupDatabase -eq $true -and $db -ne $null ){
	$backupFile = $BackupDatabasePath + "$($DbNameConvention)_DocumentStorage.bak"  
    Write-Host "Backing Up..."
    Backup-SqlDatabase -ServerInstance $connection.DataSource -Database $connection.InitialCatalog -BackupFile $backupFile -BackupAction Database -Initialize
    Set-Location $scriptPath
}

# Restore-SqlDatabase
if ( $RestoreDatabase -eq $true ){	
	$backupFile = $BackupDatabasePath + $BackupDocumentStorageFile
	Run-Sql -Query "ALTER DATABASE [$($connection.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ErrorAction SilentlyContinue
	Run-Sql -Query "ALTER DATABASE [$($connection.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ErrorAction SilentlyContinue

	$mdfRelocate = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile -ArgumentList ("DocumentStorage", "$($SqlFilesPath)$($connection.InitialCatalog).mdf")
    $logRelocate = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile -ArgumentList ("DocumentStorage_log", "$($SqlFilesPath)$($connection.InitialCatalog)_log.ldf")

	Write-Host "Restoring Database $($connection.InitialCatalog) from $($backupFile) ..."
	
    Restore-SqlDatabase -ServerInstance $connection.DataSource -Database $connection.InitialCatalog -ReplaceDatabase -BackupFile $backupFile -RelocateFile ($mdfRelocate, $logRelocate)
	$db = "DatabaseRestored"
}

# Drop Create Database
if ( $DropCreateDatabase -eq $true -and $db -ne $null ) {
    Run-Sql -Query "ALTER DATABASE [$($connection.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ErrorAction SilentlyContinue
	Run-Sql -Query "ALTER DATABASE [$($connection.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ErrorAction SilentlyContinue
	
    Write-Host "Dropping database..."        
    Run-Sql -Query "DROP DATABASE $($connection.InitialCatalog)"
    
	$db = $null
}


# Create database because it does not exist or it was dropped
if ( $db -eq $null ) {
	
	$newDatabaseName = "$($connection.InitialCatalog)"
	$newLogicalName = "DocumentStorage"
	$newMdfFile = "$($SqlFilesPath)$($connection.InitialCatalog).mdf"
	$newLogFile = "$($SqlFilesPath)$($connection.InitialCatalog)_log.ldf"

	Write-Host "Creating Database:  $newDatabaseName"
	Write-Host "Logical Name: $newLogicalName"
	Write-Host "MdfFile: $newMdfFile"
	Write-Host "LogFile: $newLogFile"

    Run-Sql -Query @"
	CREATE DATABASE $($newDatabaseName)	
	CONTAINMENT = NONE ON  PRIMARY ( NAME = N'Classifieds', FILENAME = N'$($newMdfFile)' , SIZE = 5120KB , FILEGROWTH = 1024KB )  
	LOG ON ( NAME = N'$($newLogicalName)_log', FILENAME = N'$($newLogFile)' , SIZE = 1024KB , FILEGROWTH = 10%) 
"@ 


}

Set-Location $scriptPath

& .\DocumentDatabase.exe | Write-Host