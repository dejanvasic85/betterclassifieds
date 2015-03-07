using System.Collections.Generic;
using System.Linq;

namespace Paramount.Betterclassifieds.Business
{
    /// <summary>
    /// Represents the total booking items with ads as children and their line items
    /// </summary>
    public class BookingRateResult
    {
        public BookingRateResult(string bookingReference)
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
                var total = OnlineBookingAdRate.Total;

                if (PrintRates.Count == 0)
                    return total;

                total += PrintRates.Sum(p => p.Total);

                return total;
            }
        }

        public void AddOnlineRate(params OnlineChargeItem[] onlineLineItems)
        {
            // Create the online 'publication'
            if(this.OnlineBookingAdRate == null)
                this.OnlineBookingAdRate = new BookingAdRateResult("Online", BookingReference);

            this.OnlineBookingAdRate.AddRange(onlineLineItems);
        }

        public void AddPublicationWithRates(string publication,  params PrintAdChargeItem[] printItems)
        {
            if(this.PrintRates == null)
                this.PrintRates = new List<BookingAdRateResult>();

            var bookingAdRateResult = new BookingAdRateResult(publication, BookingReference);
            bookingAdRateResult.AddRange(printItems);

            PrintRates.Add(bookingAdRateResult);
        }
    }
}