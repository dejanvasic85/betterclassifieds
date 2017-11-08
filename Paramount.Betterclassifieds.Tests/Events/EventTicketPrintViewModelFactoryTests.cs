using System;
using NUnit.Framework;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Tests.Events
{
    [TestFixture]
    public class EventTicketPrintViewModelFactoryTests
    {
        [Test]
        public void Create_ReturnsNewViewModel()
        {
            var mockAd = new AdSearchResultMockBuilder().Default()
                .WithHeading("Great Event that will ensure you get your money's worth")
                .WithContactPhone("4444 9999")
                .WithContactName("Organiser 123")
                .WithImageUrls(new[] { "111.jpg" }).Build();

            var mockEvent = new EventModelMockBuilder().Default()
                .WithLocation("1 melbourne place")
                .WithEventStartDate(new DateTime(2018, 1, 2, 18, 30, 0))
                .Build();

            var mockBookingTicket = new EventBookingTicketMockBuilder().Default()
                .WithSeatNumber("A1")
                .WithEventBookingTicketId(1002)
                .WithTicketName("Gold")
                .WithTotalPrice(20.50M)
                .WithGuestEmail("foo@bar.com")
                .WithGuestFullName("Foo Bar")
                .WithTicketImage("http://image.png")
                .Build();

            var mockBrandName = "mock-brand";   
            var mockBrandUrl = "http://mock-brand.com";

            // act
            var result = new EventTicketPrintViewModelFactory().Create(
                mockAd, mockEvent, mockBookingTicket, mockBrandName, mockBrandUrl, groupsForEvent: null);

            // assert
            result.TicketName.IsEqualTo("Gold");
            result.TicketNumber.IsEqualTo("1002");
            result.EventPhoto.IsEqualTo("111.jpg");
            result.EventName.IsEqualTo("Great Event that will ensure you get...");
            result.SeatNumber.IsEqualTo("A1");
            result.Location.IsEqualTo("1 melbourne place");
            result.StartDateTime.IsEqualTo("Tuesday 2 Jan 6:30PM");
            result.TickTypeAndPrice.IsEqualTo("Gold $20.50 - No 1002");
            result.ContactNumber.IsEqualTo("4444 9999");
            result.GuestEmail.IsEqualTo("foo@bar.com");
            result.GuestFullName.IsEqualTo("Foo Bar");
            result.OrganiserName.IsEqualTo("Organiser 123");
            result.TicketImage.IsEqualTo("http://image.png");
        }
    }
}
