using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.DataService;

namespace Paramount.Betterclassifieds.Tests.Integration
{
    [TestFixture]
    public class SeatCreator
    {
        [Test]
        public void CreateSeats()
        {
            var balconyTicketId = 1008;
            var silverTicketId = 1007;
            var goldTicketId = 1006;

            using (var context = new DbContextFactory().CreateEventContext())
            {
                var rowStructure = new Dictionary<int, Row>
                {
                    {1, new Row("A", 1, 22, goldTicketId) },
                    {2, new Row("B", 1, 25, goldTicketId) },
                    {3, new Row("C", 1, 26, goldTicketId) },
                    {4, new Row("D", 1, 29, goldTicketId) },
                    {5, new Row("E", 1, 30, goldTicketId) },
                    {6, new Row("F", 1, 30, goldTicketId) },
                    {7, new Row("G", 1, 32, goldTicketId) },
                    {8, new Row("H", 1, 33, goldTicketId) },
                    {9, new Row("I", 1, 34, goldTicketId) },
                    {10, new Row("J", 1, 35, goldTicketId) },
                    {11, new Row("K", 1, 35, goldTicketId) },
                    {12, new Row("L", 1, 36, goldTicketId) },
                    {13, new Row("M", 1, 34, goldTicketId) },
                    {14, new Row("N", 1, 36, goldTicketId) },
                    {15, new Row("O", 1, 40, silverTicketId) },
                    {16, new Row("P", 1, 41, silverTicketId) },
                    {17, new Row("Q", 1, 42, silverTicketId) },
                    {18, new Row("R", 1, 43, silverTicketId) },
                    {19, new Row("S", 1, 44, silverTicketId) },
                    {20, new Row("T", 1, 43, silverTicketId) },

                    {21, new Row("U", 1, 35, balconyTicketId) },
                    {22, new Row("V", 1, 36, balconyTicketId) },
                    {23, new Row("W", 1, 37, balconyTicketId) },
                    {24, new Row("X", 1, 38, balconyTicketId) },
                    {25, new Row("Y", 1, 39, balconyTicketId) },
                    {26, new Row("Z", 1, 40, balconyTicketId) },
                    {27, new Row("ZZ", 1, 42, balconyTicketId) }
                };
                
                foreach (var row in rowStructure)
                {
                    for (int i = row.Value.Start; i <= row.Value.End; i++)
                    {
                        var seatNumber = $"{row.Value.Name}{i}";
                        Console.WriteLine("Creating Seat " + seatNumber);

                        var eventSeat = new EventSeatBooking
                        {
                            EventTicketId = row.Value.TicketId,
                            SeatNumber = seatNumber,
                            RowOrder = row.Key,
                            RowNumber = row.Value.Name,
                            SeatOrder = i
                        };

                        context.EventSeats.Add(eventSeat);
                        context.SaveChanges();
                    }
                }
            }
        }

        class Row
        {
            public Row(string name, int start, int end, int ticketId)
            {
                Name = name;
                Start = start;
                End = end;
                TicketId = ticketId;
            }

            public string Name { get; private set; }
            public int Start { get; private set; }
            public int End { get; private set; }
            public int TicketId { get; private set; }
        }
    }
}
