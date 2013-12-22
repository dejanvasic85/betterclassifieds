namespace Paramount.Betterclassifieds.Console.Tasks
{
    [Help(Description = "Removes an edition from the system and ensures that any bookings made are also extended.")]
    public class RemoveEdition : ITask
    {
        private string[] _editionsToRemove;

        public void HandleArgs(TaskArguments args)
        {
            _editionsToRemove = args.ReadArgument("Editions", isRequired:true).Split(',');
        }

        public void Run()
        {
            // Use the business layer to perform all the work :)

        }

        public bool Singleton { get { return true; } }
    }
}