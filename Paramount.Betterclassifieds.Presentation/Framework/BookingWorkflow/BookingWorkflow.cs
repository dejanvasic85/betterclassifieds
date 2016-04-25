using System;
using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BookingWorkflow
    {
        public LinkedList<Type> Items { get; private set; }

        public string WorkflowName { get; private set; }

        public BookingWorkflow()
        {
            Items = new LinkedList<Type>();
        }

        public BookingWorkflow WithName(string workflowName)
        {
            WorkflowName = workflowName;
            return this;
        }

        public BookingWorkflow WithStep<TStep>() where TStep : IBookingStep
        {
            Items.AddLast(typeof(TStep)); // Keep appending to the end
            return this;
        }
    }
}