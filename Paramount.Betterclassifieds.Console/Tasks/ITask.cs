namespace Paramount.Betterclassifieds.Console.Tasks
{
    public interface ITask
    {
        void HandleArgs(TaskArguments args);
        void Run();
        bool Singleton { get; }
    }
}