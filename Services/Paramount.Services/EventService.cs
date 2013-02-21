namespace Paramount.Services
{
    using System;
    using System.Data;
    using Common.ServiceContracts;
    using DataService;
    using Common.DataTransferObjects.EventService.Messages;
    using Paramount.Common.DataTransferObjects;

    public class EventService:IEventService
    {

        public UpdateHostDetailResponse UpdateHostDetail(UpdateHostDetailRequest detailRequest)
        {
            throw new NotImplementedException();
        }

        public CreateEventTicketResponse CreateEventTicket(CreateEventTicketRequest request)
        {
            throw new NotImplementedException();
        }

        public UpdateTicketResponse UpdateTicket(UpdateTicketRequest request)
        {
            throw new NotImplementedException();
        }

        public UpdateEventResponse UpdateEvent(UpdateEventRequest request)
        {
            throw new NotImplementedException();
        }

        public GetEventsByRegionResponse GetEventsByRegion(GetEventsByRegionRequest request)
        {
            throw new NotImplementedException();
        }

        public GetEventsByCategoryResponse GetEventsByCategory(GetEventsByCategoryRequest request)
        {
            throw new NotImplementedException();
        }

        public GetGenreResponse GetGenre(GetGenreRequest request)
        {
            using ( var db= new EventDataProvider(request.ClientCode))
            {
                return new GetGenreResponse {GenreList = db.GetCategories()};
            }
        }

        public static void CreateEventGenre(CreateEventGenreRequest request)
        {
            throw new NotImplementedException();
        }

        CreateEventResponse IEventService.CreateEvent(CreateEventRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
