using System;
using System.Collections.Generic;
using Paramount.Betterclassifieds.Business.Events;
using Paramount.Betterclassifieds.DataService;

namespace Paramount.Betterclassifieds.SeatGen
{
    public class Generator
    {
        private readonly int _eventId;
        private readonly IEventRepository _eventRepository;

        public Generator(int eventId, IEventRepository eventRepository)
        {
            _eventId = eventId;
            _eventRepository = eventRepository;
        }

        public void Start()
        {
            var eventModel = _eventRepository.GetEventDetails(_eventId);

            if (eventModel == null)
                throw new NullReferenceException("The required event does not exist");

            eventModel.IsSeatedEvent = true;
            _eventRepository.UpdateEvent(eventModel);

            DropSeats();

            var vip = DropCreateEventTicket("VIP", price: 40, availableQty: 102);
            var generalTicket = DropCreateEventTicket("General", price: 30, availableQty: 335);
            var balconyTicket = DropCreateEventTicket("Balcony", price: 25, availableQty: 520);

            CreateSeats(vip, generalTicket, balconyTicket);
        }

        private EventTicket DropCreateEventTicket(string ticketName, decimal price, int availableQty)
        {
            _eventRepository.DeleteEventTicket(_eventId, ticketName);
            var eventTicket = new EventTicket
            {
                TicketName = ticketName,
                Price = price,
                AvailableQuantity = availableQty,
                RemainingQuantity = availableQty,
                EventId = _eventId,
                IsActive = true
            };
            _eventRepository.CreateEventTicket(eventTicket);
            return eventTicket;
        }

        void DropSeats()
        {
            var seats = _eventRepository.GetEventSeats(_eventId);
            foreach (var eventSeat in seats)
            {
                _eventRepository.DeleteEventSeat(eventSeat);
            }
        }

        void CreateSeats(EventTicket vipTicket, EventTicket generalTicket, EventTicket balconyTicket)
        {
            var vipTicketId = vipTicket.EventTicketId.GetValueOrDefault();
            var balconyTicketId = balconyTicket.EventTicketId.GetValueOrDefault();
            var generalTicketId = generalTicket.EventTicketId.GetValueOrDefault();

            using (var context = new DbContextFactory().CreateEventContext())
            {
                var rowStructure = new Dictionary<int, Row>
                {
                    {1, new Row("A", 22, 1, vipTicketId) },
                    {2, new Row("B", 25, 1, vipTicketId) },
                    {3, new Row("C", 26, 1, vipTicketId) },
                    {4, new Row("D", 29, 1, vipTicketId) },
                    {5, new Row("E", 30, 1, generalTicketId) },
                    {6, new Row("F", 30, 1, generalTicketId) },
                    {7, new Row("G", 32, 1, generalTicketId) },
                    {8, new Row("H", 33, 1, generalTicketId) },
                    {9, new Row("I", 34, 1, generalTicketId) },
                    {10, new Row("J", 35, 1, generalTicketId) },
                    {11, new Row("K", 35, 1, generalTicketId) },
                    {12, new Row("L", 36, 1, generalTicketId) },
                    {13, new Row("M", 34, 1, generalTicketId) },
                    {14, new Row("N", 36, 1, generalTicketId) },
                    {15, new Row("O", 40, 1, balconyTicketId) },
                    {16, new Row("P", 41, 1, balconyTicketId) },
                    {17, new Row("Q", 42, 1, balconyTicketId) },
                    {18, new Row("R", 43, 1, balconyTicketId) },
                    {19, new Row("S", 44, 1, balconyTicketId) },
                    {20, new Row("T", 43, 1, balconyTicketId) },

                    {21, new Row("U", 35, 1, balconyTicketId) },
                    {22, new Row("V", 36, 1, balconyTicketId) },
                    {23, new Row("W", 37, 1, balconyTicketId) },
                    {24, new Row("X", 38, 1, balconyTicketId) },
                    {25, new Row("Y", 39, 1, balconyTicketId) },
                    {26, new Row("Z", 40, 1, balconyTicketId) },
                    {27, new Row("ZZ", 42, 1, balconyTicketId) }
                };

                foreach (var row in rowStructure)
                {
                    var rowSeats = row.Value;
                    var seatOrder = 0;
                    for (int i = rowSeats.Start; i >= row.Value.End; i--)
                    {
                        var seatNumber = $"{rowSeats.Name}{i}";
                        Console.WriteLine("Creating Seat " + seatNumber);

                        var eventSeat = new EventSeat
                        {
                            EventTicketId = rowSeats.TicketId,
                            SeatNumber = seatNumber,
                            RowOrder = row.Key,
                            RowNumber = rowSeats.Name,
                            SeatOrder = seatOrder++
                        };

                        context.EventSeats.Add(eventSeat);
                        context.SaveChanges();
                    }
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
