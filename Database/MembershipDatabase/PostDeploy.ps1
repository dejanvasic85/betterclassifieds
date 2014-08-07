<# Variables Provided by Octopus 
	$BackupDatabase = $true
	$BackupDatabasePath = "D:\"
	$RestoreDatabase = $true
	$DropCreateDatabase = $true
#>

$scriptPath = Split-Path -Parent $MyInvocation.MyCommand.Definition
Set-Location $scriptPath
[xml]$appConfig = Get-Content .\MembershipDatabase.exe.config

# Use connection builder to get variables we need for backup and stuff
$sqlConnectionBuilder = New-Object -TypeName System.Data.SqlClient.SqlConnectionStringBuilder -ArgumentList $appConfig.configuration.connectionStrings.add.connectionString

$db = Invoke-SqlCmd -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$($sqlConnectionBuilder.InitialCatalog)';" -ServerInstance $sqlConnectionBuilder.DataSource  -QueryTimeout 0

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
	Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource  -QueryTimeout 0
	Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource  -QueryTimeout 0

	$mdfRelocate = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile -ArgumentList ("AppUserTest", "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\$($sqlConnectionBuilder.InitialCatalog).mdf")
    $logRelocate = New-Object Microsoft.SqlServer.Management.Smo.RelocateFile -ArgumentList ("AppUserTest_log", "C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\$($sqlConnectionBuilder.InitialCatalog)_log.ldf")

	Write-Host "Restoring Database $($sqlConnectionBuilder.InitialCatalog) from $($backupFile) ..."
	
    Restore-SqlDatabase -ServerInstance $sqlConnectionBuilder.DataSource -Database $sqlConnectionBuilder.InitialCatalog -ReplaceDatabase -BackupFile $backupFile -RelocateFile ($mdfRelocate, $logRelocate)
}

# Drop Create Database
if ( $DropCreateDatabase -eq $true -and $db -ne $null ) {
    Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set SINGLE_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource -ErrorAction SilentlyContinue  -QueryTimeout 0
	Invoke-Sqlcmd "ALTER DATABASE [$($sqlConnectionBuilder.InitialCatalog)] set RESTRICTED_USER with rollback immediate;" -ServerInstance $sqlConnectionBuilder.DataSource -ErrorAction SilentlyContinue  -QueryTimeout 0
	
    Write-Host "Dropping database..."        
    Invoke-Sqlcmd -Query "DROP DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource  -QueryTimeout 0
    Write-Host "Creating Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource -QueryTimeout 0
	$db = "DatabaseCreated"
}

if ( $db -eq $null ) {
	Write-Host "Creating Database..."
    Invoke-Sqlcmd -Query "CREATE DATABASE $($sqlConnectionBuilder.InitialCatalog)" -ServerInstance $sqlConnectionBuilder.DataSource -QueryTimeout 0
}

# Sanitize database
if ( $SanitizeDatabase -eq $true ) {	
	Write-Host "Sanitization = Updating Membership with $($Sanitize_Email) email"
	Invoke-SqlCmd "UPDATE aspnet_Membership SET Email = '$($Sanitize_Email)', LoweredEmail = '$($Sanitize_Email)'" -ServerInstance $sqlConnectionBuilder.DataSource -Database $sqlConnectionBuilder.InitialCatalog -QueryTimeout 0
	Write-Host "Sanitization = Updating Profiles with $($Sanitize_Email) email"
	Invoke-SqlCmd "UPDATE UserProfile SET Email = '$($Sanitize_Email)'" -ServerInstance $sqlConnectionBuilder.DataSource -Database $sqlConnectionBuilder.InitialCatalog -QueryTimeout 0
}

Set-Location $scriptPath

& .\MembershipDatabase.exe | Write-Host