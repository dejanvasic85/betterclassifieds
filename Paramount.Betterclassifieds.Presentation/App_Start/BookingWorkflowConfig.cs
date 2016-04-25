using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BookingWorkflowConfig
    {
        public static void Register(IList<BookingWorkflow> workflowCollection)
        {
            workflowCollection.Add(new BookingWorkflow().WithName("Default")
                .WithStep<CategorySelectionStep>()
                .WithStep<DesignOnlineAdStep>()
                .WithStep<ConfirmationStep>()
                .WithStep<SuccessStep>());

            workflowCollection.Add(new BookingWorkflow().WithName("Event")
                .WithStep<CategorySelectionStep>()
                .WithStep<DesignEventStep>()
                .WithStep<DesignEventTicketingStep>()
                .WithStep<ConfirmationStep>()
                .WithStep<SuccessStep>());
        }
    }
}