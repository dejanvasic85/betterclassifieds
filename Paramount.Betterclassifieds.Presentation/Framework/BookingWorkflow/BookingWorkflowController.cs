using System;
using System.Collections.Generic;
using System.Monads;
using System.Web.Mvc;
using System.Web.Routing;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.Presentation
{
    public class BookingWorkflowController<TStep> where TStep : IBookingStep, new()
    {
        private const string BookingControllerName = "Booking";
        private readonly UrlBuilder _urlBuilder;
        private readonly UrlHelper _urlHelper;
        private readonly IBookingCart _bookingCart;
        private readonly LinkedListNode<Type> _currentNode;

        public TStep CurrentStep { get; private set; }
        public IBookingStep NextStep { get; private set; }
        public IBookingStep PreviousStep { get; private set; }

        public BookingWorkflowController(UrlHelper urlHelper, IBookingCart bookingCart)
        {
            _urlBuilder = new UrlBuilder(urlHelper);
            _urlHelper = urlHelper;
            _bookingCart = bookingCart;

            var workflow = BookingWorkflowTable.Get(bookingCart.CategoryAdType);

            _currentNode = workflow.Items.Find(typeof(TStep));
            if (_currentNode == null)
                throw new InvalidOperationException("The target step cannot be found in the workflow");

            CurrentStep = new TStep();
            if (_currentNode.Previous != null)
                PreviousStep = Activator.CreateInstance(_currentNode.Previous.Value) as IBookingStep;

            if (_currentNode.Next != null)
                NextStep = Activator.CreateInstance(_currentNode.Next.Value) as IBookingStep;
        }
        
        public string GetNextStepUrl(object routeValues = null)
        {
            return _urlBuilder.WithAction(NextStep.ActionName, BookingControllerName).WithRouteValues(routeValues);
        }

        public string GetPreviousUrl(object routeValues = null)
        {
            return _urlBuilder.WithAction(PreviousStep.ActionName, BookingControllerName).WithRouteValues(routeValues);
        }

        private RedirectToRouteResult Redirect(string action)
        {
            return new RedirectToRouteResult(new RouteValueDictionary
            {
                {"controller", BookingControllerName },
                {"action", action }
            });
        }

        public RedirectToRouteResult RedirectToNextStep()
        {
            return Redirect(NextStep.ActionName);
        }

        public RedirectToRouteResult RedirectToPreviousStep()
        {
            return Redirect(PreviousStep.ActionName);
        }

        public BookingWorkflowController<TStep> SkipNextStep()
        {
            var target = _currentNode.With(n => n.Next).With(n => n.Next).With(n => n.Value);
            if (target == null)
                throw new IndexOutOfRangeException("Cannot skip step because it does not exist");

            NextStep = Activator.CreateInstance(target) as IBookingStep;
            return this;
        }
    }
}