$instance = "localhost"
$dotNetVersion = "4.0"
$regKey = "HKLM:\software\Microsoft\MSBuild\ToolsVersions\$dotNetVersion"
$regProperty = "MSBuildToolsPath"

function Main(){
    
    $msbuild = Join-Path -Path (Get-ItemProperty $regKey).$regProperty -ChildPath "msbuild.exe"

    & $msbuild "Betterclassifieds.sln" "/p:Configuration=Debug"

    # Create the database for bdd and set connections
    DropCreateDatabase -databaseName "bdd_classifieds" -dbUpPath "\Database\ClassifiedsDatabase\bin\Debug\ClassifiedsDatabase.exe"
    DropCreateDatabase -databaseName "bdd_membership" -dbUpPath "\Database\MembershipDatabase\bin\Debug\MembershipDatabase.exe"
    DropCreateDatabase -databaseName "bdd_core" -dbUpPath "\Database\CoreDatabase\bin\Debug\CoreDatabase.exe"

	# Set location back to the executing script
	$scriptPath = Join-Path -Path $PSScriptRoot -ChildPath ""
	Set-Location $scriptPath
}

function DropCreateDatabase($databaseName, $dbUpPath){
        
    $bdd_database = Invoke-SqlCmd -Query "SELECT name from master.dbo.sysdatabases WHERE name = '$databaseName';" -ServerInstance $instance

    if ($bdd_database -ne $null){
        # Kill any current sessions
        Invoke-Sqlcmd "alter database [$databaseName] set single_user with rollback immediate" -ServerInstance $instance

        Write-Host "Dropping database $databaseName"
        Invoke-Sqlcmd -Query "DROP DATABASE $databaseName" -ServerInstance $instance
    }

    # Create database 
    Write-Host "Creating database $databaseName"
    Invoke-Sqlcmd -Query "CREATE DATABASE $databaseName" -ServerInstance $instance
    
    # Set the user security (currently not required - just make sure your windows login is a sql sa)
    # $currentUser = ("$($env:COMPUTERNAME)\$($env:USERNAME)")
    #Invoke-Sqlcmd -Query "EXEC sp_adduser '$currentUser', '$currentUser', 'db_owner'" -Database $databaseName -ServerInstance "localhost"

    # todo - Overwrite the connection string in the app config for DbUp

    # Run scripts
    Write-Host "Running DBUp scripts"
    $fullExePath = Join-Path -Path $PSScriptRoot -ChildPath $dbUpPath
    
    Invoke-Expression $fullExePath
}

# Start by going to Main function
Main