namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class AddressViewModel
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CategoryViewModel
    {
        public int? CategoryId { get; set; }

        public int? ParentId { get; set; }

        public string Title { get; set; }
    }
}