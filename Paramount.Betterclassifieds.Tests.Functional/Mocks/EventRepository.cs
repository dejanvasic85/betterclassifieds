using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using Dapper;
using Paramount.Betterclassifieds.Tests.Functional.Features.Events;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    internal partial class DapperDataRepository
    {
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
                            addressId
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
                    RemainingQuantity = availableQuantity
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

        public EventBookingData GetEventBooking(int eventId)
        {
            using (var connection = _connectionFactory.CreateClassifieds())
            {
                return connection.Query<EventBookingData>(
                    "SELECT * FROM EventBooking WHERE EventId = @eventId", new { eventId })
                    .SingleOrDefault();
            }
        }

        public List<EventBookingTicketData> GetEventBookingTickets(int eventBookingId)
        {
            using (var connection = _connectionFactory.CreateClassifieds())
            {
                return connection.Query<EventBookingTicketData>(
                    "SELECT * FROM EventBookingTicket WHERE EventBookingId = @eventBookingId", new { eventBookingId })
                    .ToList();
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
    }
}
