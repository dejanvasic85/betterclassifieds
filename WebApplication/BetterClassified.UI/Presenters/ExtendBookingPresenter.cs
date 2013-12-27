using Paramount;
using Paramount.Betterclassifieds.Business.Managers;
using Paramount.Betterclassifieds.Business.Models;
using Paramount.Betterclassifieds.Business.Repository;

namespace BetterClassified.UI.Presenters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Views;

    public class ExtendBookingPresenter : Controller<IExtendBookingView>
    {
        private readonly IBookingRepository BookingRepository;
        private readonly IConfigManager ConfigSettings;
        private readonly ExtensionManager ExtensionManager;
        private readonly RateCalculator RateCalculator;

        public ExtendBookingPresenter(IExtendBookingView view,
            IBookingRepository bookingRepository,
            ExtensionManager extensionManager,
            IConfigManager configSettings,
            RateCalculator rateCalculator)
            : base(view)
        {
            this.BookingRepository = bookingRepository;
            this.ExtensionManager = extensionManager;
            this.ConfigSettings = configSettings;
            this.RateCalculator = rateCalculator;
        }

        public void Load()
        {
            // Fetch the original booking
            AdBookingModel booking = BookingRepository.GetBooking(View.AdBookingId, withLineAd: true);

            // Ensure booking exists
            if (booking == null || 
                booking.IsExpired || 
                booking.BookingStatus != BookingStatusType.Booked ||
                booking.UserId.DoesNotEqual(View.LoggedInUserName))
            {
                View.DisplayBookingDoesNotExist();
                return;
            }

            // Load the screen as per usual (depending on the type of booking)
            View.DataBindOptions(booking.BookingType == BookingType.Bundled
                ? Enumerable.Range(1, ConfigSettings.RestrictedEditionCount)
                : Enumerable.Range(1, ConfigSettings.RestrictedOnlineDaysCount));

            // Load for a single edition first
            Load(insertions: 1, booking: booking);
        }

        public void Load(int insertions, AdBookingModel booking = null)
        {
            // Fetch the original booking
            if (booking == null)
                booking = BookingRepository.GetBooking(View.AdBookingId, withLineAd: true);

            View.PaymentReference = booking.ExtensionReference;

            // Check whether the booking is online only ( for scheduling )
            if (booking.BookingType == BookingType.Bundled)
            {
                // Fetch and display the list of editions
                List<PublicationEditionModel> editions = ExtensionManager.ExtensionDates(View.AdBookingId, insertions).ToList();

                // Fetch the online end date
                DateTime onlineAdEndDate = editions
                    .SelectMany(e => e.Editions)
                    .OrderBy(date => date.EditionDate)
                    .Last()
                    .EditionDate
                    .AddDays(ConfigSettings.NumberOfDaysAfterLastEdition);

                decimal pricePerEdition = 0;

                foreach (var publication in editions)
                {
                    pricePerEdition += RateCalculator.Calculate(
                        publication.Editions.OrderBy(e => e.EditionDate).Last().BaseRateId,
                        booking.LineAd,
                        isOnlineAd: true);
                }

                decimal totalPrice = pricePerEdition * insertions;

                View.IsOnlineOnly = false;
                View.DataBindEditions(editions, onlineAdEndDate, pricePerEdition, totalPrice);
            }
            else
            {
                View.SetupOnlineOnlyView();
                View.IsOnlineOnly = true;
                View.DataBindEditions(null, booking.EndDate.AddDays(insertions), booking.TotalPrice, booking.TotalPrice);
            }
        }

        public void ProcessExtension()
        {
            var extension = ExtensionManager.CreateExtension(View.AdBookingId,
                    View.SelectedInsertionCount,
                    View.LoggedInUserName,
                    View.TotalPrice,
                    View.IsPaymentRequired ? ExtensionStatus.Pending : ExtensionStatus.Complete,
                    View.IsOnlineOnly);

            if (View.IsPaymentRequired)
            {
                // Save the extension id for completing later and redirect to the payment processing views
                View.NavigateToPayment(extension.AdBookingExtensionId, View.PaymentReference);
            }
            else
            {
                // Extend the booking details
                ExtensionManager.Extend(extension);
                View.NavigateToBookings(true);
            }
        }
    }
}
