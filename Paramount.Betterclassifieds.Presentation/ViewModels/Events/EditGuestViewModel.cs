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

        public EditGuestViewModel(int adId, EventTicket eventTicket, EventBookingTicket eventBookingTicket)
        {
            AdId = adId;
            EventTicketId = eventTicket.EventTicketId.GetValueOrDefault();
            EventBookingId = eventBookingTicket.EventBookingId;
            EventBookingTicketId = eventBookingTicket.EventBookingTicketId;
            GuestFullName = eventBookingTicket.GuestFullName;
            GuestEmail = eventBookingTicket.GuestEmail;
            TicketName = eventTicket.TicketName;
            TicketPrice = eventTicket.Price;
            TicketPurchaseDate = eventBookingTicket.CreatedDateTime.GetValueOrDefault();
            GroupId = eventBookingTicket.EventGroupId;

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
        }

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

        public int? GroupId { get; set; }

        public List<EventTicketFieldViewModel> Fields { get; set; }

        public string TicketName { get; set; }
        public decimal TicketPrice { get; set; }
        public DateTime TicketPurchaseDate { get; set; }

    }
}