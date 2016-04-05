using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Dapper;

namespace Paramount.Betterclassifieds.Tests.Functional.Mocks
{
    internal partial class DapperDataRepository
    {
        public int AddEventIfNotExists(int onlineAdId)
        {
            var eventId =
                _classifiedDb.Query<int?>("select EventId from [Event] where OnlineAdId = @onlineAdId", new { onlineAdId })
                    .SingleOrDefault();

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
                    Suburb = "Collingwood",
                    State = "Victoria",
                    Postcode  = "3000",
                    Country = "Australia"
                });

                // Create the event now
                eventId = _classifiedDb.AddIfNotExists(Constants.Table.Event,
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
}
