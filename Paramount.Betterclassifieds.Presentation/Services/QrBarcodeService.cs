using System;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Paramount.Betterclassifieds.Business.Events;
using ZXing;
using ZXing.Common;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class QrBarcodeService : IEventBarcodeManager
    {
        private readonly IEventBarcodeManager _businessBarcodeManager;
        private readonly HttpContextBase _httpContext;

        public QrBarcodeService(IEventRepository eventRepository, HttpContextBase httpContext)
        {
            _httpContext = httpContext;
            _businessBarcodeManager = new EventBarcodeManager(eventRepository);
        }

        public string GenerateBarcodeData(EventModel eventDetails, EventBookingTicket eventBookingTicket)
        {
            return _businessBarcodeManager.GenerateBarcodeData(eventDetails, eventBookingTicket);
        }

        public string GenerateBase64StringImageData(string barcodeData, int height, int width, int margin = 0)
        {
            // Use the third party library here
            var barcodeWriter = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new EncodingOptions { Height = height, Width = width, Margin = margin }
            };

            using (var bitmap = barcodeWriter.Write(barcodeData))
            {
                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Gif);
                    return Convert.ToBase64String(stream.ToArray());
                }
            }
        }

        public string GenerateBase64StringImageData(EventModel eventModel, EventBookingTicket eventTicket, int height, int width,
            int margin = 0)
        {
            var barcodeData = GenerateBarcodeData(eventModel, eventTicket);
            
            // When generating the image data, we embed a link!
            var validationLink = new UrlHelper(_httpContext.Request.RequestContext).ValidateBarcode(barcodeData).WithFullUrl();

            return GenerateBase64StringImageData(validationLink, height, width, margin);
        }

        public EventBookingTicketValidationResult ValidateTicket(string barcodeData)
        {
            // Simply forward the call
            return _businessBarcodeManager.ValidateTicket(barcodeData);
        }
    }
}