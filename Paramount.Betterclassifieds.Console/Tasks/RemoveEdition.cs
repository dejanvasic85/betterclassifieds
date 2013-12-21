namespace Paramount.Betterclassifieds.Console.Tasks
{
    /// <summary>
    /// Removes an edition from the system and ensures that any bookings made are also extended
    /// </summary>
    public class RemoveEdition : ITask
    {
        private string[] _editionsToRemove;
        private bool _moveBookings;

        public void HandleArgs(TaskArguments args)
        {
            
        }

        public void Run()
        {
        }

        public bool Singleton { get; private set; }
    }
}