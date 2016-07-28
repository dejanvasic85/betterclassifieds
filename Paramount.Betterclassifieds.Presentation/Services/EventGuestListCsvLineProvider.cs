using System.Monads;
using System.Text;
using Paramount.Betterclassifieds.Business.Csv;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    /// <summary>
    /// Produces a CSV line for the EventGuestListViewModel class
    /// </summary>
    public class EventGuestListCsvLineProvider : ICsvLineProvider<EventGuestListViewModel>
    {
        public string GetHeader(EventGuestListViewModel guest)
        {
            var builder = new StringBuilder($"Ticket Number,Ticket Name,Guest Email,Guest Full Name");
            foreach (var field in guest.DynamicFields)
            {
                builder.AppendFormat(",{0}", field.FieldName);
            }
            return builder.ToString();
        }

        public string GetCsvLine(EventGuestListViewModel guest)
        {
            var builder = new StringBuilder($"{guest.TicketNumber},{guest.TicketName},{guest.GuestEmail},{guest.GuestFullName}");
            foreach (var field in guest.With(g => g.DynamicFields))
            {
                builder.AppendFormat(",{0}", field.With(f => f.FieldValue));
            }
            return builder.ToString();
        }
    }
}