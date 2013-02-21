namespace Paramount.Common.ServiceContracts
{
    using System.ServiceModel;
    using DataTransferObjects.EventService.Messages;

    [ServiceContract]
    public interface IEventService
    {
        [OperationContract]
        CreateEventResponse CreateEvent(CreateEventRequest request);

        [OperationContract]
        UpdateHostDetailResponse UpdateHostDetail(UpdateHostDetailRequest detailRequest);

        [OperationContract]
        CreateEventTicketResponse CreateEventTicket(CreateEventTicketRequest request);

        [OperationContract]
        UpdateTicketResponse UpdateTicket(UpdateTicketRequest request);

        [OperationContract]
        UpdateEventResponse UpdateEvent(UpdateEventRequest request);

        [OperationContract]
        GetEventsByRegionResponse GetEventsByRegion(GetEventsByRegionRequest request);

        [OperationContract]
        GetEventsByCategoryResponse GetEventsByCategory(GetEventsByCategoryRequest request);

        [OperationContract]
        GetGenreResponse GetGenre(GetGenreRequest request);
    }
}