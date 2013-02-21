namespace Paramount.Common.DataTransferObjects.EventService.Messages
{
    using System;

    [Serializable]
    public class CreateEventResponse
    {
        public Guid? EventId { get; set; }
    }
}
