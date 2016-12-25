using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.Presentation.Services;
using Paramount.Betterclassifieds.Presentation.ViewModels.Events;

namespace Paramount.Betterclassifieds.Tests.Csv
{
    [TestFixture]
    public class ExcelGuestGeneratorServiceTests
    {
        [Test]
        public void GetBytes_CreatesExcelSpreadsheet_ToOutXlsx()
        {
            var dynamicFields = new[]
            {
                new EventTicketFieldViewModel { FieldName = "Field 1", FieldValue = "Value1"}, 
                new EventTicketFieldViewModel { FieldName = "Field 2", FieldValue = "Value2"}, 
                new EventTicketFieldViewModel { FieldName = "Field 3", FieldValue = "Value3"}, 
            };

            var data = new []
            {
                new EventGuestListViewModel { TicketNumber =  1, TicketName = "Free Entry", GuestEmail = "guest1@email.com", GuestFullName = "Foo Bar", DynamicFields = dynamicFields},
                new EventGuestListViewModel { TicketNumber =  2, TicketName = "Free Entry", GuestEmail = "guest2@email.com", GuestFullName = "Foo Bar 2", DynamicFields = dynamicFields},
            };

            var eventTicketMockBuilder = new EventTicketMockBuilder().Default().WithEventTicketId(1);

            var eventTicketFields = new List<EventTicketField>
            {
                new EventTicketField {FieldName = "Field 1"},
                new EventTicketField {FieldName = "Field 2"},
                new EventTicketField {FieldName = "Field 3"},
            };
            var tickets = new List<EventTicket>
            {
                eventTicketMockBuilder.WithEventTicketId(1).WithEventTicketFields(eventTicketFields).Build(),
                eventTicketMockBuilder.WithEventTicketId(1).WithEventTicketFields(eventTicketFields).Build(),
            };

            using (var excelService = new ExcelGuestGeneratorService(tickets, data))
            {
                var bytes = excelService.GetBytes();
                Assert.That(bytes, Is.Not.Null);
                Assert.That(bytes.Length, Is.GreaterThan(1));

                // File.WriteAllBytes(TestContext.CurrentContext.Test.Name + ".xlsx", bytes);
            }
        }
    }
}