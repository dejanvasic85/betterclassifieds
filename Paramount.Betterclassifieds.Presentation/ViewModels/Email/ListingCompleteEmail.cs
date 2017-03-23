using System;

namespace Paramount.Betterclassifieds.Presentation.ViewModels.Email
{
    public class ListingCompleteEmail
    {
        public string ListingUrl { get; set; }
        public string Heading { get; set; }
        public int Id { get; set; }
        public string Email { get; set; }
        public DateTime ListingDate { get; set; }
    }
}