using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Monads;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EditGuestViewModel
    {
        public EditGuestViewModel()
        {
            Fields = new List<EventTicketFieldViewModel>();
        }

        public EditGuestViewModel(int adId, EventTicket eventTicket, EventBooking eventBooking, EventBookingTicket eventBookingTicket, IEnumerable<EventGroup> groups)
        {
            AdId = adId;
            EventId = eventTicket.EventId;
            EventTicketId = eventTicket.EventTicketId.GetValueOrDefault();
            EventBookingId = eventBookingTicket.EventBookingId;
            EventBookingTicketId = eventBookingTicket.EventBookingTicketId;
            GuestFullName = eventBookingTicket.GuestFullName;
            GuestEmail = eventBookingTicket.GuestEmail;
            GroupId = eventBookingTicket.EventGroupId;
            CurrentGroupId = eventBookingTicket.EventGroupId;
            TicketName = eventTicket.TicketName;
            TicketPrice = eventTicket.Price;
            TicketPurchaseDate = eventBookingTicket.CreatedDateTime.GetValueOrDefault();
            RemovingGuestWillCancelBooking = eventBooking.EventBookingTickets.Count(b => b.IsActive) == 1;
            Fields = new List<EventTicketFieldViewModel>();
            eventTicket.EventTicketFields?.Do(f =>
            {
                // Match on name
                var val = eventBookingTicket.TicketFieldValues.Single(v => v.FieldName.Equals(f.FieldName));
                Fields.Add(new EventTicketFieldViewModel
                {
                    FieldName = f.FieldName,
                    FieldValue = val.FieldValue,
                    IsRequired = f.IsRequired,
                    EventTicketId = f.EventTicketId.GetValueOrDefault()
                });
            });

            if (groups != null)
            {
                Groups = groups.Select(g => new EventGroupViewModel
                {
                    EventId = g.EventId,
                    MaxGuests = g.MaxGuests,
                    GuestCount = g.GuestCount,
                    EventGroupId = g.EventGroupId,
                    GroupName = g.GroupName

                }).ToList();
            }
        }

        public bool RemovingGuestWillCancelBooking { get; set; }

        public int? EventId { get; set; }

        public int AdId { get; set; }

        [Required]
        public int EventBookingTicketId { get; set; }

        [Required]
        public int EventBookingId { get; set; }

        [Required]
        public int EventTicketId { get; set; }

        [MaxLength(100)]
        public string GuestFullName { get; set; }

        [EmailAddress]
        [MaxLength(100)]
        public string GuestEmail { get; set; }

        public List<EventTicketFieldViewModel> Fields { get; set; }
        public List<EventGroupViewModel> Groups { get; set; }
        public int? GroupId { get; set; }
        public int? CurrentGroupId { get; set; }

        public string TicketName { get; set; }
        public decimal TicketPrice { get; set; }
        public DateTime TicketPurchaseDate { get; set; }

        public bool SendEmailToGuest { get; set; }
    }
}