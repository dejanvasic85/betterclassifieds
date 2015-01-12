using System;
using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business.Print;

namespace Paramount.Betterclassifieds.Console.Tasks
{
    [Help(Description = "Removes an edition from the system and ensures that any bookings made are also extended.")]
    internal class RemoveEdition : ITask
    {
        private IEnumerable<DateTime> _editionsToRemove;
        private readonly IEditionManager _editionManager;

        public RemoveEdition(IEditionManager editionManager)
        {
            _editionManager = editionManager;
        }

        public void HandleArgs(TaskArguments args)
        {
            _editionsToRemove = args.ReadArgument("Editions", isRequired: true)
                .Split('|')
                .Select(DateTime.Parse);
        }

        public void Run()
        {
            // Use the business layer to perform all the work :)
            _editionsToRemove.ForEach(edition =>
                {
                    System.Console.WriteLine("Processing edition {0}", edition);
                    _editionManager.RemoveEditionAndExtendBookings(edition);
                });
        }

        public bool Singleton { get { return true; } }
    }
}