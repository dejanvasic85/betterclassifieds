namespace Paramount.TaskScheduler
{
    interface IScheduler
    {
        void Run(SchedulerParameters  parameters);
        string Name { get; }
    }
}
