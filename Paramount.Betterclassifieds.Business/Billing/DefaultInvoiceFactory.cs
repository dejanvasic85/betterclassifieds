using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    public class DefaultInvoiceFactory : IInvoiceFactory
    {
        private readonly IClientConfig _clientConfig;

        public DefaultInvoiceFactory(IClientConfig clientConfig)
        {
            _clientConfig = clientConfig;
        }

        public Invoice CreateInvoice(List<InvoiceGroup> invoiceGroups)
        {
            var groupRef = "";
            var invoiceDate = DateTime.Now;

            var firstInvoiceGroup = invoiceGroups.FirstOrDefault();
            if (firstInvoiceGroup != null)
            {
                groupRef = firstInvoiceGroup.Reference;
            }

            return new Invoice
            {
                InvoiceGroups = invoiceGroups,
                CompanyAddress = _clientConfig.ClientAddress.ToString(),
                CompanyName = _clientConfig.ClientName,
                CompanyPhone = _clientConfig.ClientPhoneNumber,
                GrandTotal = invoiceGroups.Sum(t => t.OrderTotal),
                Reference = groupRef
            };
        }
    }
}