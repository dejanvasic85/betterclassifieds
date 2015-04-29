﻿using System.Web.Mvc;
using AutoMapper;
using Paramount.Betterclassifieds.Business;
using Paramount.Betterclassifieds.Presentation.ViewModels;

namespace Paramount.Betterclassifieds.Presentation.Controllers
{
    [Authorize]
    public class InvoiceController : Controller, IMappingBehaviour
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        [AuthorizeBookingIdentity]
        public ActionResult Booking(string id)
        {
            // Generates the invoice based on the order items for the booking Id
            int bookingId;
            int.TryParse(id, out bookingId);

            var invoice = _invoiceService.GenerateBookingInvoice(bookingId);

            var invoiceViewModel = this.Map<Invoice, InvoiceView>(invoice);

            return View(invoiceViewModel);
        }

        public void OnRegisterMaps(IConfiguration configuration)
        {
            configuration.CreateProfile("invoieControllerMappings");
            configuration.CreateMap<Invoice, InvoiceView>();
            configuration.CreateMap<InvoiceGroup, InvoiceGroupView>();
            configuration.CreateMap<InvoiceLineItem, InvoiceLineItemView>();
        }
    }
}