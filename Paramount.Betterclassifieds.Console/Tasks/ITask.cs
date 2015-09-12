namespace Paramount.Betterclassifieds.Console
{
    public interface ITask
    {
        void HandleArgs(TaskArguments args);
        void Run();
        bool Singleton { get; }
    }
}