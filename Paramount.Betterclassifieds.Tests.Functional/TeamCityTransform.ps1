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

$CurrentPath = ( split-path $MyInvocation.MyCommand.Path ) 


# Swap everything for File.txt
$AppConfig =  Join-Path $CurrentPath -ChildPath "Paramount.Betterclassifieds.Tests.Functional.dll.config" 


ReplaceInFile -TargetFile $AppConfig -Values @{
    'Data Source=localhost;Initial Catalog=iFlog;Integrated Security=True' = (Get-ChildItem env:ClassifiedsDbConnection).Value;
	'Data Source=localhost;Initial Catalog=Broadcast;Integrated Security=True' = (Get-ChildItem env:BroadcastDbConnection).Value;
	'Data Source=localhost;Initial Catalog=iFlogAppUser;Integrated Security=True' = (Get-ChildItem env:AppUserDbConnection).Value;
} 
