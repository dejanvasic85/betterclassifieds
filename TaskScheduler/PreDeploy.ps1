# The following Tasks variable is defined in the Octopus Deploy application variables
# It is a comma delimited convention based tasks definition
# $Tasks = "iFlog_EmailProcessor~MI5~EMAILPROCESSING/,iFlog_ExpiredEmailSender~DI06:30~EXPAD/1 DAYSBEFOREEXPIRY/6,iFlog_ImageCacheCleaner~DI23:30~IMAGECACHECLEAN/c:\Paramount\ImageCache\,iFlog_HealthCheckAlert~DI23:45~SYSTEMHEALTHCHECKALERT/support@paramountit.com.au"

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