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
            using (var context = new DbContextFactory().CreateEventContext())
            {
                var rowStructure = new Dictionary<int, Row>
                {
                    {1, new Row("A", 1, 22) },
                    {2, new Row("B", 1, 25) },
                    {3, new Row("C", 1, 26) },
                    {4, new Row("D", 1, 29) },
                    {5, new Row("E", 1, 30) },
                    {6, new Row("F", 1, 30) },
                    {7, new Row("G", 1, 32) },
                    {8, new Row("H", 1, 33) },
                    {9, new Row("I", 1, 34) },
                    {10, new Row("J", 1, 35) },
                    {11, new Row("K", 1, 35) },
                    {12, new Row("L", 1, 36) },
                    {13, new Row("M", 1, 34) },
                    {14, new Row("N", 1, 36) },
                    {15, new Row("O", 1, 40) },
                    {16, new Row("P", 1, 41) },
                    {17, new Row("Q", 1, 42) },
                    {18, new Row("R", 1, 43) },
                    {19, new Row("S", 1, 44) },
                    {20, new Row("T", 1, 43) },

                    {21, new Row("U", 1, 35) },
                    {22, new Row("V", 1, 36) },
                    {23, new Row("W", 1, 37) },
                    {24, new Row("X", 1, 38) },
                    {25, new Row("Y", 1, 39) },
                    {26, new Row("Z", 1, 40) },
                    {27, new Row("ZZ", 1, 42) }
                };
                
                foreach (var row in rowStructure)
                {
                    for (int i = row.Value.Start; i <= row.Value.End; i++)
                    {
                        var seatNumber = $"{row.Value.Name}{i}";
                        Console.WriteLine("Creating Seat " + seatNumber);

                        var eventSeat = new EventSeatBooking
                        {
                            EventTicketId = 2,
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
            public Row(string name, int start, int end)
            {
                Name = name;
                Start = start;
                End = end;
            }

            public string Name { get; set; }
            public int Start { get; set; }
            public int End { get; set; }
        }
    }
}
