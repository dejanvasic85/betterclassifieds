using System;
using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    /// <summary>
    /// Represents the total booking items with ads as children and their line items
    /// </summary>
    public class BookingOrderResult
    {
        public BookingOrderResult(string bookingReference)
        {
            BookingReference = bookingReference;
        }

        public string BookingReference { get; private set; }

        public BookingAdRateResult OnlineBookingAdRate { get; private set; }

        public List<BookingAdRateResult> PrintRates { get; private set; }

        public decimal Total
        {
            get
            {
                var total = OnlineBookingAdRate.OrderTotal;

                if (PrintRates == null)
                    return total;

                total += PrintRates.Sum(p => p.OrderTotal);

                return total;
            }
        }

        public void AddOnlineRate(params OnlineChargeItem[] onlineLineItems)
        {
            // Create the online 'publication'
            if (this.OnlineBookingAdRate == null)
                this.OnlineBookingAdRate = new BookingAdRateResult("Online", BookingReference, null);

            this.OnlineBookingAdRate.AddRange(onlineLineItems);
        }

        public void AddPublicationWithRates(string publicationName, int publicationId, int? rateId, params PrintAdChargeItem[] printItems)
        {
            if (this.PrintRates == null)
                this.PrintRates = new List<BookingAdRateResult>();

            var bookingAdRateResult = new BookingAdRateResult(publicationName, BookingReference, rateId, publicationId);
            bookingAdRateResult.AddRange(printItems);

            PrintRates.Add(bookingAdRateResult);
        }

        public BookingAdRateResult GetPrintRateForPublication(int publication)
        {
            if (PrintRates == null)
                return null;

            var printRate = this.PrintRates.FirstOrDefault(p => p.PublicationId == publication);
            
            if (printRate == null)
                throw new ArgumentException("The required publication is not available in the calculated print rates");

            return printRate;
        }
    }
}