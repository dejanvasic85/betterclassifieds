﻿using System.ComponentModel.DataAnnotations;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Events
{
    public class EventTicketViewModel
    {
        public int? EventTicketId { get; set; }
        [Required]
        public int? EventId { get; set; }
        [Required]
        public string TicketName { get; set; }
        public int AvailableQuantity { get; set; }
        public int RemainingQuantity { get; set; }
        public decimal Price { get; set; }
        public int SoldQty { get; set; }
    }
}