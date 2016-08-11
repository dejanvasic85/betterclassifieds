using System;
using System.Collections.Generic;
using System.Linq;
using System.Monads;
using OfficeOpenXml;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Presentation.Services
{
    public class ExcelGuestGeneratorService : IDisposable
    {
        private readonly IEnumerable<EventGuestListViewModel> _data;
        private readonly Lazy<ExcelPackage> _lazyPackage;

        public ExcelGuestGeneratorService(IEnumerable<EventGuestListViewModel> data)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            Guard.NotNull(data);
            // ReSharper disable once PossibleMultipleEnumeration
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
            sheet.Cells[1, 5].Value = "Ticket Price";
            sheet.Cells[1, 6].Value = "Booking Date";
            sheet.Cells[1, 7].Value = "Booking Time";


            // Fetch the dynamic fields (headers)
            var firstEntry = _data.First();
            var numberOfDynamicFields = firstEntry.With(f => f.DynamicFields).With(f => f.Count());

            for (int i = 0; i < numberOfDynamicFields; i++)
            {
                sheet.Cells[1, i + 8].Value = firstEntry.DynamicFields[i].FieldName;
            }

            // Data rows
            var dataRowIndex = 2;
            foreach (var guest in _data)
            {
                sheet.Cells[dataRowIndex, 1].Value = guest.TicketNumber;
                sheet.Cells[dataRowIndex, 2].Value = guest.TicketName;
                sheet.Cells[dataRowIndex, 3].Value = guest.GuestEmail;
                sheet.Cells[dataRowIndex, 4].Value = guest.GuestFullName;
                sheet.Cells[dataRowIndex, 5].Value = guest.TicketTotalPrice;
                sheet.Cells[dataRowIndex, 6].Value = guest.DateOfBooking.ToString("dd/MM/yyyy");
                sheet.Cells[dataRowIndex, 7].Value = guest.DateOfBooking.ToString("HH:mm");
                

                // Print out the dynamic field (values)
                for (int i = 0; i < guest.With(g => g.DynamicFields).With(d => d.Length); i++)
                {
                    var cell = sheet.Cells[dataRowIndex, i + 9];
                    cell.Value = guest.DynamicFields[i].FieldValue;
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