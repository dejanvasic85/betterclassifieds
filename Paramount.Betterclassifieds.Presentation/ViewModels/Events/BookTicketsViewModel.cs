using System.Collections.Generic;
using System.Linq;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Business.Search;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    /// <summary>
    /// Used to load the ticketing booking page
    /// </summary>
    public class BookTicketsViewModel
    {
        public BookTicketsViewModel()
        { }

        public BookTicketsViewModel(AdSearchResult onlineAdModel, EventModel eventDetails, IClientConfig clientConfig, 
            IApplicationConfig appConfig, ApplicationUser applicationUser, List<EventTicketReservation> ticketReservations)
        {
            EventId = eventDetails.EventId;
            TotelReservationExpiryMinutes = clientConfig.EventTicketReservationExpiryMinutes;
            Title = onlineAdModel.Heading;
            AdId = onlineAdModel.AdId;
            Description = onlineAdModel.Description;
            CategoryAdType = onlineAdModel.CategoryAdType;
            EventPhoto = onlineAdModel.PrimaryImage;
            Location = eventDetails.Location;
            TicketFields = eventDetails.TicketFields.Select(f => new EventTicketFieldViewModel { FieldName = f.FieldName, IsRequired = f.IsRequired }).ToList();
            SuccessfulReservationCount = ticketReservations.Count(r => r.Status == EventTicketReservationStatus.Reserved);
            LargeRequestCount = ticketReservations.Count(r => r.Status == EventTicketReservationStatus.RequestTooLarge);
            SendEmailToGuests = true;
            TotalCost = ticketReservations.Sum(r => r.TotalPriceWithTxnFee);

            if (applicationUser != null)
            {
                IsUserLoggedIn = true;
                FirstName = applicationUser.FirstName;
                LastName = applicationUser.LastName;
                Phone = applicationUser.Phone;
                PostCode = applicationUser.Postcode;
                Email = applicationUser.Email;
            }
            BrandName = appConfig.Brand;
        }

        public List<EventTicketFieldViewModel> TicketFields { get; set; }

        public int AdId { get; set; }
        public string Title { get; set; }

        public List<EventTicketReservedViewModel> Reservations { get; set; }
        public bool OutOfTime { get; set; }
        public int ReservationExpiryMinutes { get; set; }
        public int ReservationExpirySeconds { get; set; }
        public int TotelReservationExpiryMinutes { get; set; }
        public string CategoryAdType { get; set; }
        public int SuccessfulReservationCount { get; set; }
        public int LargeRequestCount { get; set; }
        public string Description { get; set; }
        public bool IsUserLoggedIn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public int? EventId { get; set; }
        public string EventPhoto { get; set; }
        public string Location { get; set; }
        public bool SendEmailToGuests { get; set; }
        public bool? PaymentCancelled { get; set; }
        public decimal TotalCost { get; set; }

        public int TotalCostCents => (int)(TotalCost * 100);
        public string BrandName { get; set; }
    }
}