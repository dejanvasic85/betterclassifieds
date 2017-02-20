using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Transactions;
using Dapper;
using Paramount.Betterclassifieds.Tests.Functional.Base;
using Paramount.Betterclassifieds.Tests.Functional.Mocks.Models;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    internal partial class DapperDataRepository
    {
        public List<string> GetEventTicketFieldNames(int eventTicketId)
        {
            using (var connection = _connectionFactory.CreateClassifieds())
            {
                return connection.Query<string>("select f.FieldName from EventTicketField f where f.EventTicketId = @eventTicketId", new { eventTicketId }).ToList();
            }
        }

        public string GetEventBookingStatus(int eventId, string guestEmail)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                return db.Query<string>(@"
                    select eb.Status from EventBookingTicket ebt
                    join EventBooking eb on eb.EventBookingId = ebt.EventBookingId
                    join [Event] e on e.EventId = eb.EventId and e.EventId = @eventId
                    where ebt.GuestEmail = @guestEmail",
                    new { eventId, guestEmail }).Single();
            }
        }

        public bool GetEventBookingTicketStatus(int eventId, string guestEmail)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                return db.Query<bool>(@"
                    select IsActive from EventBookingTicket ebt
                    join EventBooking eb on eb.EventBookingId = ebt.EventBookingId
                    join [Event] e on e.EventId = eb.EventId and e.EventId = @eventId
                    where ebt.GuestEmail = @guestEmail",
                    new { eventId, guestEmail }).Single();
            }
        }

        public int AddEventIfNotExists(int onlineAdId)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {

                var eventId = db.Query<int?>("select EventId from [Event] where OnlineAdId = @onlineAdId", new { onlineAdId }).SingleOrDefault();

                if (eventId.HasValue)
                {
                    return eventId.Value;
                }

                using (var scope = new TransactionScope())
                {
                    // first add the address
                    var addressId = AddAddress(new
                    {
                        StreetNumber = 10,
                        StreetName = "Smith Street",
                        Suburb = "Collingwood",
                        State = "Victoria",
                        Postcode = "3000",
                        Country = "Australia"
                    });

                    // Create the event now
                    eventId = db.AddIfNotExists(Constants.Table.Event,
                        new
                        {
                            onlineAdId,
                            Location = "10 Smith St, Collingwood VIC 3066, Australia",
                            LocationLatitude = "-37.80",
                            LocationLongitude = "144.98",
                            EventStartDate = DateTime.Now.AddDays(30).ToString("yyyy-MM-dd"),
                            EventEndDate = DateTime.Now.AddDays(32).ToString("yyyy-MM-dd"),
                            addressId,
                            displayGuests = true
                        },
                        filterByValue: onlineAdId,
                        findByColumnName: "OnlineAdId");

                    scope.Complete();
                }

                return eventId.GetValueOrDefault();
            }
        }

        public void AddEventTicketType(int eventId, string ticketName, decimal price, int availableQuantity)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                db.Add(Constants.Table.EventTicket, new
                {
                    eventId,
                    ticketName,
                    price,
                    availableQuantity,
                    RemainingQuantity = availableQuantity,
                    isActive = true
                });
            }
        }

        public int AddEventOrganiser(int eventId, string username)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                return db.Add(Constants.Table.EventOrganiser, new
                {
                    eventId,
                    UserId = username,
                    LastModifiedBy = "eventAdmin",
                    LastModifiedDate = DateTime.Now,
                    LastModifiedDateUtc = DateTime.UtcNow,
                    IsActive = true
                });
            }
        }

        public void SetEventIncludeTransactionFee(int eventId, bool include)
        {
            using (var connection = _connectionFactory.CreateClassifieds())
            {
                connection.ExecuteSql("UPDATE [Event] SET [IncludeTransactionFee] = @include WHERE EventId = @eventId", new { eventId, include });
            }
        }

        public void SetEventGroupsRequired(int eventId)
        {
            using (var connection = _connectionFactory.CreateClassifieds())
            {
                connection.ExecuteSql("UPDATE [Event] SET [GroupsRequired] = 1 WHERE EventId = @eventId", new { eventId });
            }
        }

        public EventTestData GetEventByName(string eventTitle)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                return db.Query<EventTestData>(
                    "select bk.AdBookingId as AdId, o.Heading as Title, e.EventId" +
                    "from dbo.[Event] e" +
                    "join dbo.OnlineAd o on o.OnlineAdId = e.OnlineAdId" +
                    "join dbo.AdDesign d on d.AdDesignId = o.AdDesignId" +
                    "join dbo.AdBooking bk on bk.AdId = d.AdId" +
                    "where o.Heading = '" + eventTitle + "'").SingleOrDefault();
            }
        }

        public EventBookingData GetEventBooking(int eventId)
        {
            using (var connection = _connectionFactory.CreateClassifieds())
            {
                return connection.Query<EventBookingData>(
                    "SELECT * FROM EventBooking WHERE EventId = @eventId", new { eventId })
                    .SingleOrDefault();
            }
        }

        public List<EventBookingTicketData> GetPurchasedTickets(int eventBookingId)
        {
            using (var connection = _connectionFactory.CreateClassifieds())
            {
                return connection.Query<EventBookingTicketData>(
                    "SELECT * FROM EventBookingTicket WHERE EventBookingId = @eventBookingId", new { eventBookingId }).ToList();
            }
        }

        public List<EventBookingTicketData> GetPurchasedTicketsForEvent(int eventId)
        {
            using (var connection = _connectionFactory.CreateClassifieds())
            {
                return connection.Query<EventBookingTicketData>(
                    "SELECT ebt.* " +
                    "FROM EventBookingTicket ebt " +
                    "JOIN EventBooking eb ON eb.EventBookingId = ebt.EventBookingId " +
                    "JOIN [Event] e ON e.EventId = eb.EventId and e.EventId = @eventId ", new { eventId }).ToList();
            }
        }

        public TicketDetails GetEventTicketByName(int eventId, string ticketName)
        {
            using (var connection = _connectionFactory.CreateClassifieds())
            {
                return connection.Query<TicketDetails>("SELECT t.* FROM EventTicket t WHERE t.EventId = @eventId and t.TicketName = @ticketName",
                    new { eventId, ticketName }).SingleOrDefault();
            }
        }

        public int AddEventInvitationIfNotExists(int eventId, int userNetworkId)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                var invitation = db.Query<int?>("SELECT EventInvitationId FROM EventInvitation WHERE EventId = @eventId AND UserNetworkId = @userNetworkId",
                    new { eventId, userNetworkId }).SingleOrDefault();

                if (invitation != null)
                    return invitation.Value;

                return db.Add(Constants.Table.EventInvitation,
                    new { eventId, userNetworkId });
            }
        }

        public void AddEventGroup(int eventId, string groupName, string ticketName, int maxGuests)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                // Fetch the ticket id first
                var ticketId = db.Query<int>("SELECT EventTicketId FROM EventTicket WHERE TicketName = @ticketName And EventId = @eventId", new { ticketName, eventId })
                    .Single();

                // Call proc directly to create the event group
                db.Execute("EventGroup_Create", new
                {
                    eventId,
                    groupName,
                    maxGuests,
                    createdDate = DateTime.Now,
                    createdDateUtc = DateTime.UtcNow,
                    createdBy = "bdduser",
                    availableToAllTickets = false,
                    tickets = ticketId.ToString()
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public int AddGuestToEvent(string username, string guestFullName, string guestEmail, string ticketName, int eventId)
        {
            using (var db = _connectionFactory.CreateClassifieds())
            {
                // Fetch the ticket id first
                var ticketId = db.Query<int>("SELECT EventTicketId FROM EventTicket WHERE TicketName = @ticketName And EventId = @eventId", new { ticketName, eventId })
                    .Single();

                var eventBookingId = db.Add(Constants.Table.EventBooking, new
                {
                    UserId = username,
                    EventId = eventId,
                    Email = guestEmail,
                    FirstName = guestFullName.Split(' ')[0],
                    LastName = guestFullName.Split(' ')[1],
                    TotalCost = 0,
                    PaymentMethod = "None",
                    Status = "Active",
                    CreatedDateTime = DateTime.Now,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    TransactionFee = 0,
                    Cost = 0
                });

                return db.Add(Constants.Table.EventBookingTicket, new
                {
                    EventBookingId = eventBookingId,
                    EventTicketId = ticketId,
                    TicketName = ticketName,
                    CreatedDateTime = DateTime.Now,
                    CreatedDateTimeUtc = DateTime.UtcNow,
                    Price = 0,
                    GuestEmail = guestEmail,
                    GuestFullName = guestFullName,
                    TransactionFee = 0,
                    TotalPrice = 0,
                    IsActive = 1,
                    IsPublic = false
                });
            }
        }
    }
}
