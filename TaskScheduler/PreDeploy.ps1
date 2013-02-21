Import-Module TaskScheduler

# Declare the tasks to be stopped
$Tasks = 'iFlog_EmailProcessor,iFlog_ExpiredEmailSender,iFlog_ImageCacheCleaner'

$taskNames = $Tasks.split(',')
foreach( $name in $taskNames ) {
       
    # Get the required task
    $task = Get-ScheduledTask $name -Hidden
    if ( $task -eq $null ){
        Write-Host "Task : " $name " does not exist."
        continue;
    }
    # Keep checking every 2 seconds whether task is still running…
    while( $task.status -eq "Running" ){
        Write-Host "Task : " $name " is currently running… Waiting to complete"
        [bool]$isRunning = $true;
        Sleep 2
    }
    $task.enabled = $false
    Write-Host "Task : " $name " is disabled"
}