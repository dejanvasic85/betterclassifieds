using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Booking;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation;
using Paramount.Betterclassifieds.Presentation.Controllers;

namespace Paramount.Betterclassifieds.Tests.Controllers
{
    [TestFixture]
    public class BookingWorkflowRegistrationTests : ControllerTest<BookingController>
    {
        private UrlHelper _urlHelper;
        private IBookingCart _bookingCart;
       
        [TestFixtureSetUp]
        public void Setup()
        {
            var request = new Mock<HttpContextBase>();
            var routeData = new RouteData();
            var mockRequestContext = new RequestContext(request.Object, routeData);

            BookingWorkflowConfig.Register(BookingWorkflowTable.Workflows);
            _urlHelper = new UrlHelper(mockRequestContext);
            _bookingCart = BookingCart.Create("session_123", "foo_bar");
        }

        [Test]
        public void DefaultRegistration_CategorySelectionStep()
        {
            var bookingWorkflowController = new BookingWorkflowController<CategorySelectionStep>(_urlHelper, _bookingCart);

            Assert.That(bookingWorkflowController.CurrentStep.ViewName, Is.EqualTo("Step1"));
            Assert.That(bookingWorkflowController.NextStep.ViewName, Is.EqualTo("Step2"));
            Assert.That(bookingWorkflowController.PreviousStep, Is.Null);
        }

        [Test]
        public void DefaultRegistration_DesignOnlineAdStep_()
        {
            var bookingWorkflowController = new BookingWorkflowController<DesignOnlineAdStep>(_urlHelper, _bookingCart);

            Assert.That(bookingWorkflowController.CurrentStep.ViewName, Is.EqualTo("Step2"));
            Assert.That(bookingWorkflowController.NextStep.ViewName, Is.EqualTo("Step3"));
            Assert.That(bookingWorkflowController.PreviousStep.ViewName, Is.EqualTo("Step1"));
        }

        [Test]
        public void DefaultRegistration_Confirmationtep()
        {
            var bookingWorkflowController = new BookingWorkflowController<ConfirmationStep>(_urlHelper, _bookingCart);

            Assert.That(bookingWorkflowController.CurrentStep.ViewName, Is.EqualTo("Step3"));
            Assert.That(bookingWorkflowController.NextStep.ViewName, Is.EqualTo("Success"));
            Assert.That(bookingWorkflowController.PreviousStep.ViewName, Is.EqualTo("Step2"));
        }

        [Test]
        public void DefaultRegistration_SuccessStep()
        {
            var bookingWorkflowController = new BookingWorkflowController<SuccessStep>(_urlHelper, _bookingCart);

            Assert.That(bookingWorkflowController.CurrentStep.ViewName, Is.EqualTo("Success"));
            Assert.That(bookingWorkflowController.NextStep, Is.Null);
            Assert.That(bookingWorkflowController.PreviousStep.ViewName, Is.EqualTo("Step3"));
        }

        [Test]
        public void EventRegistration_CategorySelectionStep()
        {
            _bookingCart.CategoryAdType = "Event";
            var bookingWorkflowController = new BookingWorkflowController<CategorySelectionStep>(_urlHelper, _bookingCart);

            Assert.That(bookingWorkflowController.CurrentStep.ViewName, Is.EqualTo("Step1"));
            Assert.That(bookingWorkflowController.NextStep.ViewName, Is.EqualTo("Step2_Event"));
            Assert.That(bookingWorkflowController.PreviousStep, Is.Null);
        }

        [Test]
        public void EventRegistration_DesignEventStep()
        {
            _bookingCart.CategoryAdType = "Event";
            var bookingWorkflowController = new BookingWorkflowController<DesignEventStep>(_urlHelper, _bookingCart);

            Assert.That(bookingWorkflowController.CurrentStep.ViewName, Is.EqualTo("Step2_Event"));
            Assert.That(bookingWorkflowController.NextStep.ViewName, Is.EqualTo("Step2_EventTickets"));
            Assert.That(bookingWorkflowController.PreviousStep.ViewName, Is.EqualTo("Step1"));
        }

        [Test]
        public void EventRegistration_DesignEventTicketsStep()
        {
            _bookingCart.CategoryAdType = "Event";
            var bookingWorkflowController = new BookingWorkflowController<DesignEventTicketingStep>(_urlHelper, _bookingCart);

            Assert.That(bookingWorkflowController.CurrentStep.ViewName, Is.EqualTo("Step2_EventTickets"));
            Assert.That(bookingWorkflowController.NextStep.ViewName, Is.EqualTo("Step3"));
            Assert.That(bookingWorkflowController.PreviousStep.ViewName, Is.EqualTo("Step2_Event"));
        }

        [Test]
        public void EventRegistration_ConfirmationStep()
        {
            _bookingCart.CategoryAdType = "Event";
            var bookingWorkflowController = new BookingWorkflowController<ConfirmationStep>(_urlHelper, _bookingCart);

            Assert.That(bookingWorkflowController.CurrentStep.ViewName, Is.EqualTo("Step3"));
            Assert.That(bookingWorkflowController.NextStep.ViewName, Is.EqualTo("Success"));
            Assert.That(bookingWorkflowController.PreviousStep.ViewName, Is.EqualTo("Step2_EventTickets"));
        }
    }
}
