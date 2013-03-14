namespace Paramount.Products.TaskScheduler
{
    interface IScheduler
    {
        void Run(SchedulerParameters  parameters);
        string Name { get; }
    }
}
