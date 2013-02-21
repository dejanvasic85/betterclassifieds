    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paramount.ApplicationBlock.Data;
using Paramount.Common.DataTransferObjects.Banner;
using Paramount.Common.DataTransferObjects.Billing;
using Paramount.DataService.LinqObjects;

namespace Paramount.DataService
{
    public class InvoiceDataProvider : IDisposable
    {
        private BillingDatabaseModelDataContext _context;
        private const string ConfigSection = "paramount/services";
        private const string SourceKey = "ConnectionString";

        public InvoiceDataProvider(string clientCode)
        {
            _context = new BillingDatabaseModelDataContext(ConnectionString);
            ClientCode = clientCode;
        }

        public string ClientCode { get; private set; }

        public string ConnectionString
        {
            get { return ConfigReader.GetConnectionString(ConfigSection, SourceKey); }
        }

        public IQueryable<Invoice> AsRawQueryable()
        {
            return _context.Invoices.Where(a => a.ClientCode == ClientCode);
        }

        public InvoiceEntity GetInvoice(Guid invoiceId)
        {
            return _context.Invoices.FirstOrDefault(a => a.ClientCode == ClientCode && a.InvoiceId == invoiceId).Convert();
        }

        public IQueryable<InvoiceItem> ItemsAsRawQueryable(Guid invoiceId)
        {
            //return _context.InvoiceItems.Join().Where(a => a.ClientCode == ClientCode & a.InvoiceId == InvoiceId);
            return from item in _context.InvoiceItems
                   join cart in _context.Invoices.Where(a => a.ClientCode == ClientCode && a.InvoiceId == invoiceId) on new { item.InvoiceId } equals new { InvoiceId = (Guid?)cart.InvoiceId }
                   select item;
        }

        public IEnumerable<InvoiceEntity> AsQueryable()
        {
            return AsRawQueryable().ToList().Select(a => a.Convert());
        }

        public IEnumerable<InvoiceItemEntity> ItemsAsQueryable(Guid invoiceId)
        {
            return ItemsAsRawQueryable(invoiceId).ToList().Select(a => a.Convert());
        }

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
                _context = null;
            }
            GC.SuppressFinalize(this);
        }

        public Guid GenerateInvoice(Guid shoppingCartId, string status)
        {
            var id = Guid.NewGuid();
            var invoice = new ShoppingCartDataProvider(ClientCode).GetShoppingCart(shoppingCartId).ToInvoice();
            var cartItems = new ShoppingCartDataProvider(ClientCode).ItemsAsQueryable(shoppingCartId);

            invoice.InvoiceId = id;
            invoice.Status = status;
            invoice.DateTimeCreated = DateTime.Now;
            invoice.DateTimeUpdated = DateTime.Now;
            invoice.ClientCode = ClientCode;
            invoice.TotalAmount = cartItems.Sum(item => (item.Price*item.Quantity));
            _context.Invoices.InsertOnSubmit(invoice);
            
            foreach (var invoiceItem in
                cartItems.Select(shoppingCartItemEntity => shoppingCartItemEntity.ToInvoiceItem(id)))
            {
                _context.InvoiceItems.InsertOnSubmit(invoiceItem);
            }

            return id;
        }


        public bool UpdateInvoiceAddressDetails(Guid invoiceId, AddressDetails billingAddress, AddressDetails deliveryAddress)
        {
            var invoice = _context.Invoices.Single(a => a.ClientCode == ClientCode && a.InvoiceId == invoiceId);
            invoice = invoice.FillBillingAddress(billingAddress).FillDeliveryAddress(deliveryAddress);
            invoice.DateTimeUpdated = DateTime.Now;
            invoice.ClientCode = ClientCode;
            return true;
        }

        public bool UpdateInvoiceStatus(Guid invoiceId, string status)
        {
            var invoice = _context.Invoices.FirstOrDefault(a => a.ClientCode == ClientCode && a.InvoiceId == invoiceId);
            invoice.DateTimeUpdated = DateTime.Now;
            invoice.ClientCode = ClientCode;
            invoice.Status = status;
            return true;
        }

        public bool UpdateInvoiceStatus(Guid invoiceId, string status, string sessionId, decimal totalAmount)
        {
            var invoice = _context.Invoices.FirstOrDefault(a => a.ClientCode == ClientCode && a.InvoiceId == invoiceId && a.SessionId == sessionId && a.TotalAmount == totalAmount);
            invoice.DateTimeUpdated = DateTime.Now;
            invoice.ClientCode = ClientCode;
            invoice.Status = status;
            return true;
        }
        

        public List<BillingBankEntity> GetBankList()
        {
            return _context.Billing_Banks.Select(a => a.Convert()).ToList();
        }

        public void Commit()
        {
            _context.SubmitChanges();
        }

        public List<InvoiceItemEntity> GetItems(Guid invoiceId)
        {
            return ItemsAsRawQueryable(invoiceId).Select(a => a.Convert()).ToList();
        }

        public void UpdatePaymentType(Guid invoiceId, string paymentType)
        {
            var invoice = _context.Invoices.FirstOrDefault(a => a.ClientCode == ClientCode && a.InvoiceId == invoiceId);
            invoice.DateTimeUpdated = DateTime.Now;
            invoice.ClientCode = ClientCode;
            invoice.PaymentType = paymentType;
        }
    }
}
