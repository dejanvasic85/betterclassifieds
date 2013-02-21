namespace Paramount.Common.DataTransferObjects.EventService.Messages
{
    using System;

    [Serializable]
    public class CreateEventGenreRequest : BaseRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public override string TransactionName
        {
            get { return AuditTransaction.CreateEventGenre; }
        }
    }
}
