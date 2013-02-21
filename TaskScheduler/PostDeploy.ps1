Import-Module TaskScheduler

$exeFullPath = $OctopusPackageDirectoryPath + "\TaskScheduler.exe"
$Tasks = "iFlog_EmailProcessor~MI5~EMAILPROCESSING/,iFlog_ExpiredEmailSender~DI06:30~EXPAD/1 DAYSBEFOREEXPIRY/6,iFlog_ImageCacheCleaner~DI23:30~IMAGECACHECLEAN/c:\Paramount\ImageCache\"

$tasksWithSchedule = $Tasks.split(',')

foreach( $nameWithDetails in $tasksWithSchedule ) {
    $name = $nameWithDetails.split('~')[0]
    $interval = $nameWithDetails.split('~')[1]
    $arguments = $nameWithDetails.split('~')[2] 
    
    # Replace existing task with the new one until we know how to change definition
    $task = Get-ScheduledTask $name -Hidden
    if ( $task -ne $null) {
        Remove-Task -Name $name
    }
    
    # Write-Host "Registering Task : " $name
    $newTask = New-Task 
    $newTask.Principal.UserId = "S-1-5-18"  # System User
    $newTask.Principal.RunLevel = 1         # Run with highest privilages
    Add-TaskAction -Task $newTask -Path $exeFullPath -Arguments $arguments | out-null
    
    # Configure interval trigger
    if ( $interval.StartsWith("MI") ){
        Add-TaskTrigger -Task $newTask -In (New-Timespan -Seconds 30) -Repeat (New-Timespan -Minutes $interval.Substring(2)) | out-null
    }
    elseif ( $interval.StartsWith("DI") ){
        Add-TaskTrigger -Task $newTask -Daily -At $interval.Substring(2) | out-null
    }
    
    # Register the task with windows server
    Register-ScheduledTask -Name $name -Task $newTask -ComputerName "localhost"
}