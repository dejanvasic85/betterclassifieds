using System.Collections.Generic;

namespace Paramount.Betterclassifieds.Business
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceGroups = new List<InvoiceGroup>();
        }

        public string Reference { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GrandTotal { get; set; }
        public List<InvoiceGroup> InvoiceGroups { get; set; }
    }
}