using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.DataService;
using Paramount.Betterclassifieds.DataService.Events;

namespace SeatGen
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Seat Generator");

                Console.Write("What is the event id? ");
                var eventIdRes = Console.ReadLine();
                var eventId = int.Parse(eventIdRes);

                Console.WriteLine("Looking up the tickets");

                var eventRepository = new EventRepository(new DbContextFactory());
                var eventDetails = eventRepository.GetEventDetails(eventId);

                var vipTicket = GetTicketAndUpdateColour(eventRepository, eventDetails, "VIP", "#bfd7ff");
                var goldTicket = GetTicketAndUpdateColour(eventRepository, eventDetails, "Gold", "#fff7bf");
                var generalTicket = GetTicketAndUpdateColour(eventRepository, eventDetails, "General", "#c1ffe7");
                var balconyTicket = GetTicketAndUpdateColour(eventRepository, eventDetails, "Balcony", "#eac1ff");

                eventRepository.UpdateEventTicket(goldTicket);
                eventRepository.UpdateEventTicket(vipTicket);
                eventRepository.UpdateEventTicket(generalTicket);
                eventRepository.UpdateEventTicket(balconyTicket);

                CreateSeats(vipTicket, goldTicket, generalTicket, balconyTicket);

                Console.WriteLine("Done");
            }
            catch (Exception exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(exception);
            }
        }

        static EventTicket GetTicketAndUpdateColour(IEventRepository eventRepository, EventModel eventModel, string name, string colourCode)
        {
            var ticket = eventModel.Tickets.Single(t => t.TicketName.Equals(name, StringComparison.OrdinalIgnoreCase));
            ticket.ColourCode = colourCode;
            eventRepository.UpdateEventTicket(ticket);
            return ticket;
        }

        static void CreateSeats(EventTicket vipTicket, EventTicket goldTicket, EventTicket generalTicket, EventTicket balconyTicket)
        {
            var vipTicketId = vipTicket.EventTicketId.GetValueOrDefault();
            var balconyTicketId = balconyTicket.EventTicketId.GetValueOrDefault();
            var generalTicketId = generalTicket.EventTicketId.GetValueOrDefault();
            var goldTicketId = goldTicket.EventTicketId.GetValueOrDefault();

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
                    {15, new Row("O", 1, 40, generalTicketId) },
                    {16, new Row("P", 1, 41, generalTicketId) },
                    {17, new Row("Q", 1, 42, generalTicketId) },
                    {18, new Row("R", 1, 43, generalTicketId) },
                    {19, new Row("S", 1, 44, generalTicketId) },
                    {20, new Row("T", 1, 43, generalTicketId) },

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

                        var eventSeat = new EventSeat
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


                Console.WriteLine("Updating VIP tickets");
                var vipTicketRows = new List<Row>
                {
                    new Row("E", 10, 21, vipTicketId),
                    new Row("F", 10, 21, vipTicketId),
                    new Row("G", 11, 22, vipTicketId),
                    new Row("H", 11, 23, vipTicketId),
                    new Row("I", 11, 23, vipTicketId),
                };

                foreach (var vipTicketRow in vipTicketRows)
                {
                    var seats = context.EventSeats
                        .Where(s => s.RowNumber == vipTicketRow.Name)
                        .Where(s => s.SeatOrder >= vipTicketRow.Start)
                        .Where(s => s.SeatOrder <= vipTicketRow.End)
                        .ToList();

                    foreach (var seat in seats)
                    {
                        Console.WriteLine("Updating seat {0}", seat.SeatNumber);
                        seat.EventTicketId = vipTicketId;
                        context.Entry(seat).State = EntityState.Modified;
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