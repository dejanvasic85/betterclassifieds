using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BookingWorkflowTable
    {
        private static readonly IList<BookingWorkflow> _availableWorkflows = new List<BookingWorkflow>();
        public static IList<BookingWorkflow> Workflows => _availableWorkflows;

        public static BookingWorkflow Get(string name)
        {
            if (name == null)
                name = "Default";

            return _availableWorkflows.SingleOrDefault(w => w.WorkflowName == name);
        }
    }
}