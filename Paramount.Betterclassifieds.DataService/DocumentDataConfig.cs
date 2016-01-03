using System;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using Paramount.Betterclassifieds.Business.Booking;

namespace Paramount.Betterclassifieds.DataService
{
    public class DocumentDataConfig
    {
        public static void RegisterMappings()
        {
            BsonClassMap.RegisterClassMap<BookingCart>(c =>
            {
                c.AutoMap();
                c.MapMember(member => member.StartDate).SetSerializationOptions(new DateTimeSerializationOptions { DateOnly = true });
                c.MapMember(member => member.EndDate).SetSerializationOptions(new DateTimeSerializationOptions { DateOnly = true });
                c.MapMember(member => member.PrintFirstEditionDate).SetSerializationOptions(new DateTimeSerializationOptions { DateOnly = true });
            });

            BsonClassMap.RegisterClassMap<Business.Events.EventModel>(model =>
            {
                model.AutoMap();
                model.MapMember(member => member.EventStartDate).SetSerializationOptions(new DateTimeSerializationOptions(DateTimeKind.Local));
                model.MapMember(member => member.EventEndDate).SetSerializationOptions(new DateTimeSerializationOptions(DateTimeKind.Local));
            });
        }
    }
}