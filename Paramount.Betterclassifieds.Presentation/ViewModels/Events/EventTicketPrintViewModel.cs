﻿namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventTicketPrintViewModel
    {
        public string GroupName { get; set; }
        public string TicketNumber { get; set; }
        public string EventName { get; set; }
        public string EventPhoto { get; set; }
        public string Location { get; set; }
        public string StartDateTime { get; set; }
        public string TicketName { get; set; }
        public decimal Price { get; set; }
        public string ContactNumber { get; set; }
        public string TickTypeAndPrice
        {
            get
            {
                if (Price == 0)
                    return $"TicketName - No {TicketNumber}";

                return $"{TicketName} {Price:C} - No {TicketNumber}";
            }
        }

        public string GuestFullName { get; set; }
        public string BarcodeImageDocumentId { get; set; }
        public string GuestEmail { get; set; }
        public string BrandName { get; set; }
        public string BrandUrl { get; set; }
        public string SeatNumber { get; set; }
        public string OrganiserName { get; set; }
        public string TicketImage { get; set; }
    }
}