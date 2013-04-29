<# *** *** ***  *** *** *** *** *** *** *** *** *** 
Expected Variables

 DatabaseServer			- Specific environment based database server instance name
 DatabaseName			- Specific targeted database for scripts to run for
 BackupBeforeDeploy		- True or False value that indicates whether the database should be backed up before deployment
 OctopusPackageDirectoryPath - Supplied by octopus deployment tool that specifies the directory for the deployed scripts to run
 *** *** ***  *** *** *** *** *** *** *** *** *** #>

Import-Module SQLPS -DisableNameChecking -ErrorAction SilentlyContinue

$DatabaseFolder = $OctopusPackageDirectoryPath

if ( $BackupBeforeDeploy -eq $true )
{
	$currentDateTime = Get-Date -format yyyyMMdd_hhmmss;
	$databaseBackupLocation = $DatabaseFolder + "\" + $DatabaseName + "_" + $currentDateTime + ".bak";

	Write-Host "Backing Database to : " $databaseBackupLocation
	$sqlBackupScript = "BACKUP DATABASE " + $DatabaseName + " TO DISK = N'" + $databaseBackupLocation + "' WITH NOFORMAT, INIT,  NAME = N'" + $DatabaseName +"-Full Database Backup', SKIP, NOREWIND, NOUNLOAD, COMPRESSION,  STATS = 10"

	Invoke-SqlCmd -Query $sqlBackupScript -ServerInstance $databaseServer -Database $databaseName | out-null
}

# Fetch and parse the deploy.xml file that determines the included sql scripts for this release
[xml]$deployScripts = Get-Content "$($DatabaseFolder)\Deploy.xml"

$deployScripts.package.databaseJob | Foreach-Object {    
    $scriptFiles = @()
    
    # Fetch the directories that require all files
    $_.directory | Where-Object { $_.include -eq "*" -or $_.include -eq "all" } | Foreach-Object { 
        Get-ChildItem -path "$($DatabaseFolder)\$($_.path)" -Filter *.sql | Sort-Object | Foreach-Object { $scriptFiles = $scriptFiles + $_.FullName }
    }
    
    # Fetch the directories that require specific files
    $_.directory | Where-Object { $_.include -eq "specified" -and $_.haschildnodes } | Foreach-Object { 
        $scriptFilePath = $_.path
        $_.script | Foreach-Object { 
            $scriptFileName = $_           
            $specificScript = Resolve-Path -path "$($DatabaseFolder)\$($scriptFilePath)\$($scriptFileName)"
            $scriptFiles = $scriptFiles + $specificScript
        }
    }
    
    $outFile = "$($DatabaseFolder)\$($_.outputFile)" 
    New-Item -path $outFile -Type File -Force | out-null
    
    # Insert start transaction SQL
    if ( $_.transaction -eq $true)
    {
@"
`n
	:On Error Exit
	SET XACT_ABORT ON
	GO
	Begin Transaction
`n    
"@ >> $outFile;
    }
            
    foreach ($fullName in $scriptFiles)
    {
        $file = Get-Item -path $fullName 
    
        if ($_.useScriptLog -eq $true -and $file.Name.StartsWith("RunAlways_") -eq $false)
        {
            $result = Invoke-SqlCmd -Query ("SELECT * FROM ScriptLog WHERE ScriptName = '$($file.Name)' ;") -ServerInstance $DatabaseServer -Database $DatabaseName
            if ($result -ne $null)
            {
                Write-Host "Ignoring script : " $file.name
                continue
            }
        }
        
        # Fetch content from the file
        Write-Host "Adding : " $file.name
        Get-Content $file.FullName >> $outFile ;
        
        if ($_.useScriptLog -eq $true -and $file.Name.StartsWith("RunAlways_") -eq $false)
        {
            "INSERT INTO ScriptLog ([ScriptName]) VALUES ( '$($file.Name)' ) ;`n" >> $outFile
        }
    }
    
    # Insert end transaction SQL
    if ( $_.transaction -eq $true)
    {
@"
`n
	IF XACT_STATE() = 1
	BEGIN
		COMMIT TRANSACTION
		PRINT 'Transaction Completed Successfully'
	END
	ELSE IF XACT_STATE() = -1
	BEGIN
		PRINT 'Scripts Failed... Rolling back'
		ROLLBACK TRAN
	END
`n
"@ >> $outFile;
    }
    
    
    # Check if there are no scripts to run
    if ($scriptFiles.Length -eq 0)
    {
        "-- No script files to be run for $($_.outputFile) -- "
    }
    else
    {
        # Execute the database job
        $command = "sqlcmd.exe -S " + $DatabaseServer + " -d " + $DatabaseName + " -i " + $outFile
        Invoke-Expression $command | out-null
        
    	if ( $LASTEXITCODE -ne 0 )
    	{
    		throw 'Database upgrade failed' ;
    	}
        else
        {
            Write-Host "-- Transaction Completed Successfully for $($_.outputFile) --"
        }   
    }    
}