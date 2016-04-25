using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
    public interface IBookingStep
    {
        string ViewName { get; }
    }

    public class ConfirmationStep : IBookingStep
    {
        public string ViewName => "Step3";
    }

    public class CategorySelectionStep : IBookingStep
    {
        public string ViewName => "Step1";
    }

    public class DesignOnlineAdStep : IBookingStep
    {
        public string ViewName => "Step2";
    }

    public class DesignEventStep : IBookingStep
    {
        public string ViewName => "Step2_Event";
    }

    public class DesignEventTicketingStep : IBookingStep
    {
        public string ViewName => "Step2_EventTickets";
    }

    public class SuccessStep : IBookingStep
    {
        public string ViewName => "Success";
    }

    public class BookingWorkflowController<TStep> where TStep : IBookingStep, new()
    {
        private readonly UrlHelper _urlHelper;
        private readonly IBookingCart _bookingCart;

        public TStep CurrentStep { get; private set; }
        public IBookingStep NextStep { get; private set; }
        public IBookingStep PreviousStep { get; private set; }

        public BookingWorkflowController(UrlHelper urlHelper, IBookingCart bookingCart)
        {
            _urlHelper = urlHelper;
            _bookingCart = bookingCart;

            var workflow = BookingWorkflowTable.Get(bookingCart.CategoryAdType);

            var target = workflow.Items.Find(typeof(TStep));
            if (target == null)
                throw new InvalidOperationException("The target step cannot be found in the workflow");

            CurrentStep = new TStep();
            if (target.Previous != null)
                PreviousStep = Activator.CreateInstance(target.Previous.Value) as IBookingStep;

            if (target.Next != null)
                NextStep = Activator.CreateInstance(target.Next.Value) as IBookingStep;
        }
    }

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