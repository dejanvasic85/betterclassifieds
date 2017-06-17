using System;
using System.Collections.Generic;
using System.Linq;
using System.Monads;
using OfficeOpenXml;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class ExcelGuestGeneratorService : IDisposable
    {
        private readonly EventTicketField[] _eventTicketFields;
        private readonly IEnumerable<EventGuestListViewModel> _data;
        private readonly Lazy<ExcelPackage> _lazyPackage;

        public ExcelGuestGeneratorService(IEnumerable<EventTicket> eventTickets, IEnumerable<EventGuestListViewModel> data)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Guard.NotNull(data);
            // ReSharper disable once PossibleMultipleEnumeration
            _eventTicketFields = eventTickets.SelectMany(t => t.EventTicketFields).ToArray();
            _data = data;

            _lazyPackage = new Lazy<ExcelPackage>(GetPackage);
        }

        private ExcelPackage GetPackage()
        {
            var package = new ExcelPackage();
            var sheet = package.Workbook.Worksheets.Add("Guests");

            // Header Row
            sheet.Cells[1, 1].Value = "Ticket Number";
            sheet.Cells[1, 2].Value = "Ticket Name";
            sheet.Cells[1, 3].Value = "Guest Email";
            sheet.Cells[1, 4].Value = "Guest Full Name";
            sheet.Cells[1, 5].Value = "Seat";
            sheet.Cells[1, 6].Value = "Promo";
            sheet.Cells[1, 7].Value = "Group";
            sheet.Cells[1, 8].Value = "Ticket Price";
            sheet.Cells[1, 9].Value = "Booking Date";
            sheet.Cells[1, 10].Value = "Booking Time";

            for (int i = 0; i < _eventTicketFields.Length; i++)
            {
                var field = _eventTicketFields[i];
                sheet.Cells[1, i + 11].Value = field.FieldName;
            }

            // Data rows
            var dataRowIndex = 2;
            foreach (var guest in _data)
            {
                sheet.Cells[dataRowIndex, 1].Value = guest.TicketNumber;
                sheet.Cells[dataRowIndex, 2].Value = guest.TicketName;
                sheet.Cells[dataRowIndex, 3].Value = guest.GuestEmail;
                sheet.Cells[dataRowIndex, 4].Value = guest.GuestFullName;
                sheet.Cells[dataRowIndex, 5].Value = guest.SeatNumber;
                sheet.Cells[dataRowIndex, 6].Value = guest.PromoCode;
                sheet.Cells[dataRowIndex, 7].Value = guest.GroupName;
                sheet.Cells[dataRowIndex, 8].Value = guest.TicketTotalPrice;
                sheet.Cells[dataRowIndex, 9].Value = guest.DateOfBooking.ToString("dd/MM/yyyy");
                sheet.Cells[dataRowIndex, 10].Value = guest.DateOfBooking.ToString("HH:mm");


                // Print out the dynamic field (values)
                for (int i = 0; i < _eventTicketFields.Length; i++)
                {
                    var field = _eventTicketFields[i];
                    var guestField = guest.DynamicFields.FirstOrDefault(f => f.FieldName.Equals(field.FieldName, StringComparison.OrdinalIgnoreCase));
                    if (guestField != null)
                        sheet.Cells[dataRowIndex, i + 11].Value = guestField.FieldValue;
                }
                dataRowIndex++;
            }

            return package;
        }

        public byte[] GetBytes()
        {
            return _lazyPackage.Value.GetAsByteArray();
        }

        public void Dispose()
        {
            if (_lazyPackage.IsValueCreated)
            {
                _lazyPackage.Value.Dispose();
            }
        }
    }
}