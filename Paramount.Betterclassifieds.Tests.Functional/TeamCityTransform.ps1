
param (
    [parameter(Mandatory=$true)][string]$CurrentPath
)

Function ReplaceInFile($TargetFile, [HashTable] $Values){
    
    if ( (Test-Path $TargetFile ) -eq $false){
        throw "The target file '$($TargetFile)' does not exist."
    }

    Write-Host " --- Starting custom transformation of file $($TargetFile)"

    $fileContent = Get-Content $TargetFile
    $Values.GetEnumerator() | ForEach-Object { 
        Write-Host "Replacing [$($_.Key)] with [$($_.Value)]"
        $fileContent = $fileContent -replace $_.Key, $_.Value
    }

    [IO.File]::WriteAllText($TargetFile, ($fileContent -join "`r`n"))
    Write-Host "--- Done"
}


# Get the configuration file used for testing
$AppConfig =  Join-Path $CurrentPath -ChildPath "Paramount.Betterclassifieds.Tests.Functional.dll.config" 

# Swap all the values that are used for endpoints for testing 
ReplaceInFile -TargetFile $AppConfig -Values @{
    'Data Source=localhost;Initial Catalog=iFlog;Integrated Security=True' = (Get-ChildItem env:ClassifiedsDbConnection).Value;
	'Data Source=localhost;Initial Catalog=Broadcast;Integrated Security=True' = (Get-ChildItem env:BroadcastDbConnection).Value;
	'Data Source=localhost;Initial Catalog=iFlogAppUser;Integrated Security=True' = (Get-ChildItem env:AppUserDbConnection).Value;
	'http://localhost/iFlog/' = (Get-ChildItem env:BaseUrl).Value;
	'http://localhost/iFlogAdmin/' = (Get-ChildItem env:BaseAdminUrl).Value;
} 
