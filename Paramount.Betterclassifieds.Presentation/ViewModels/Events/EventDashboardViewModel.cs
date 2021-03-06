﻿using System;
using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Paramount.Betterclassifieds.Business.Events;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventDashboardViewModel
    {
        public EventDashboardViewModel()
        { }

        public EventDashboardViewModel(int adId, int pageViews,
            int enquiryCount, string eventName, EventModel eventModel, 
            EventPaymentSummary paymentSummary, 
            EventPaymentRequestStatus status, List<EventTicketViewModel> tickets, 
            List<EventGuestListViewModel> guests)
        {
            EventName = eventName;
            AdId = adId;
            PageViews = pageViews;
            EnquiryCount = enquiryCount;
            EventId = eventModel.EventId.GetValueOrDefault();
            Tickets = tickets;
            Guests = guests;
            EventPaymentRequestStatus = status.Humanize(LetterCasing.Title);
            IsClosed = eventModel.IsClosed;
            OrganiserAbsorbsTransactionFee = !eventModel.IncludeTransactionFee;

            if (eventModel.EventBookings != null)
            {
                TotalRemainingQty = eventModel.Tickets.Sum(t => t.RemainingQuantity);

                var bookedTickets = eventModel.EventBookings
                    .SelectMany(m => m.EventBookingTickets)
                    .Where(t => t.IsActive)
                    .ToList();

                TotalSoldQty = bookedTickets.Count;
                TotalSoldAmount = paymentSummary.TotalTicketSalesAmount;
                EventOrganiserOwedAmount = paymentSummary.EventOrganiserOwedAmount;
                TotalTicketFees = paymentSummary.EventOrganiserFeesTotalFeesAmount.ToString("C");


                // Get the sold quantity for each ticket type
                foreach (var eventTicketType in tickets)
                {
                    eventTicketType.SoldQty = bookedTickets.Count(t => t.EventTicketId == eventTicketType.EventTicketId);
                }
                
                SurveyStatistics = SurveyStatisticsViewFactory.CreateStatisticsForEvent(eventModel, eventModel.EventBookings);
            }
        }

        public int EnquiryCount { get; set; }

        public bool? OrganiserAbsorbsTransactionFee { get; set; }

        public string EventPaymentRequestStatus { get; set; }

        public int AdId { get; set; }
        public string EventName { get; set; }
        public int PageViews { get; set; }
        public int EventId { get; set; }
        public List<EventTicketViewModel> Tickets { get; set; } // Ticket definitions
        public List<EventGuestListViewModel> Guests { get; set; } // Tickets purchased (guests)
        public int TotalSoldQty { get; set; }
        public int TotalRemainingQty { get; set; }
        public decimal? TotalSoldAmount { get; set; }
        public decimal? EventOrganiserOwedAmount { get; set; }
        public string TotalTicketFees { get; set; }
        public bool IsClosed { get; set; }
        public bool? RequiresEventOrganiserConfirmation { get; set; }
        public string EventUrl { get; set; }
        public IEnumerable<HowYouHeardAboutEventStat> SurveyStatistics { get; set; }
        public int SurveyStaticsTotalAnswers => SurveyStatistics.Sum(t => t.Count);
    }

    public class HowYouHeardAboutEventStat
    {
        public string OptionName { get; set; }
        public int Count { get; set; }
    }

    public class SurveyStatisticsViewFactory
    {
        public static IEnumerable<HowYouHeardAboutEventStat> CreateStatisticsForEvent(
            EventModel eventModel, IEnumerable<EventBooking> bookings)
        {
            var stats = bookings
                .Where(b => b.HowYouHeardAboutEvent.HasValue())
                .GroupBy(b => b.HowYouHeardAboutEvent)
                .Select(g => new HowYouHeardAboutEventStat { Count = g.Count(), OptionName = g.Key })
                .ToList();


            var currentOptions = eventModel.GetHowYouHeardAboutEventValues()
                .Select(o => new HowYouHeardAboutEventStat { OptionName = o });

            foreach (var option in currentOptions)
            {
                if (stats.Exists(s => s.OptionName.Equals(option.OptionName, StringComparison.OrdinalIgnoreCase)))
                    continue;

                stats.Add(new HowYouHeardAboutEventStat
                {
                    OptionName = option.OptionName
                });
            }

            return stats;
        }
    }
}