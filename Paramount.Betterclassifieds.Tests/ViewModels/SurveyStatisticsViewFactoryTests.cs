using System.Linq;
using NUnit.Framework;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Tests.ViewModels
{
    [TestFixture]
    public class SurveyStatisticsViewFactoryTests
    {
        [Test]
        public void CreateStatisticsForEvent_ReturnsViewModel()
        {
            var bookingBuilder = new EventBookingMockBuilder().Default();
            var mockEventBookings = new []
            {
                bookingBuilder.WithHowYouHeardAboutEvent("Friend").Build(),
                bookingBuilder.WithHowYouHeardAboutEvent("Friend").Build(),
                bookingBuilder.WithHowYouHeardAboutEvent("Online Advertising").Build(),
                bookingBuilder.WithHowYouHeardAboutEvent("").Build()
            };
            var mockEvent= new EventModelMockBuilder().Default()
                .WithHowYouHeardAboutEventOptions("somewhere,over,the,rainbow,friend")
                .Build();


            var result = SurveyStatisticsViewFactory.CreateStatisticsForEvent(mockEvent, mockEventBookings)
                .ToArray();

            result.Length.IsEqualTo(6);

        }
    }
}
