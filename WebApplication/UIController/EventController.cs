namespace Paramount.Common.UIController
{
    using System;
    using System.Collections.ObjectModel;
    using ApplicationBlock;
    using ViewObjects;
    //using WebServiceInterface.EventsWebService;
    using ListItemView=Paramount.Common.UIController.ViewObjects.ListItemView;

    public static class EventController
    {
       // public static string CreateEvent(EventView eventView)
       // {
       //     var request = new CreateEventRequest
       //                       {
       //                           CategoryId = eventView.CategoryId.Value,
       //                           Description = eventView.Description,
       //                           Domain = "Domain",
       //                           ImageUrl = eventView.ImageUrl,
       //                           Title = eventView.Title,
       //                           StandardEvent = eventView.StandardEvent,
       //                           AccessCode = "",
       //                           ScheduleList = eventView.ScheduleList.Convert()
       //                       };

       //     var grouping = Guid.NewGuid().ToString();
       //     AuditLogManager.Log(new AuditLog { Data = XmlUtilities.SerialiseObjectPureXml(request), SecondaryData = grouping, TransactionName = TransactionNames.CreateEventRequest});
       //     try
       //     {
       //         var response = WebServiceInterface.WebServiceHostManager.EventServiceClient().CreateEvent(request);
       //         AuditLogManager.Log(new AuditLog { Data = XmlUtilities.SerialiseObjectPureXml(response), SecondaryData = grouping, TransactionName = TransactionNames.CreateEventResponse });
       //     }
       //     catch (Exception ex)
       //     {
       //         EventLogManager.Log(ex);
       //         throw;
       //     }
            
       //     return string.Empty;
       //}

       // public static Collection<ListItemView> GetGenreList()
       // {
       //     var request = new GetGenreRequest{Domain ="Domain"};
       //     GetGenreResponse response;
       //     var grouping = Guid.NewGuid().ToString();
       //     AuditLogManager.Log(new AuditLog { Data = XmlUtilities.SerialiseObjectPureXml(request), SecondaryData = grouping, TransactionName = TransactionNames.GetEventGenreRequest });

       //     try
       //     {
       //         response = WebServiceInterface.WebServiceHostManager.EventServiceClient().GetEventGenre(request);
       //     }
       //     catch (Exception ex)
       //     {

       //         EventLogManager.Log(ex);
       //         throw;
       //     }
       //     AuditLogManager.Log(new AuditLog { Data = XmlUtilities.SerialiseObjectPureXml(response), SecondaryData = grouping, TransactionName = TransactionNames.GetEventGenreResponse });
       //     return response.GenreList.Convert();
       // }
      
    }
}
